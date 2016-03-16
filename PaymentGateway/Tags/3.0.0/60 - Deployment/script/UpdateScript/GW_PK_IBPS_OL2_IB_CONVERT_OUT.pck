create or replace package GW_PK_IBPS_OL2_IB_CONVERT_OUT is
  Procedure GetMessageFromSIbs;
  Procedure ConvertIBPS_OL2;
  FUNCTION IBPS_GetCurRef Return varchar2;
  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2);
  FUNCTION GetTranCode(vAmount number) return varchar2;
  Function CheckISVCB(vAmount number, receiverBranch varchar) return int;
  FUNCTION RECEIPVER_BANK_MAP(vF19 Varchar2) return varchar2;
  PROCEDURE VCB_CONVERTOUT_OL2(m_vRDBR     VARCHAR2,
                               m_vTLBF01   VARCHAR2,
                               vRMBENA     VARCHAR2,
                               m_vRMAMT    VARCHAR2,
                               m_vRMCURR   VARCHAR2,
                               m_vRMSNME   VARCHAR2,
                               m_vRMACNO   VARCHAR2,
                               m_vTLBAFM   VARCHAR2,
                               m_vTLBRMK   VARCHAR2,
                               m_vTLBF09   VARCHAR2,
                               m_vTLBID    VARCHAR2,
                               m_vRMPRDC   VARCHAR2,
                               vRDEFTH     VARCHAR2,
                               vRMPB40     VARCHAR2,
                               m_vTLBDEL   VARCHAR2,
                               m_vTLBCOR   VARCHAR2,
                               m_vRMDIS7   VARCHAR2,
                               m_vRMSTS7   VARCHAR2,
                               m_vRDRACT   VARCHAR2,
                               m_vDEFAULT1 VARCHAR2,
                               m_vDEFAULT2 VARCHAR2,
                               m_vDEFAULT3 VARCHAR2,
                               m_vDEFAULT4 VARCHAR2,
                               m_vDEFAULT5 VARCHAR2);

  FUNCTION GET_FIELD20 return varchar2;
  PROCEDURE VCB_INSERT_TABLE(pVCB_CONTENT IN VCB_MSG_CONTENT%ROWTYPE);
  FUNCTION VCBCheckDup(pRMACNO NUMBER, pDATE_NOW varchar2) return NUMBER;
  Function BIDV_ACCOUNT(pCCYCD varchar2) return varchar2;
  Function VCB_currency(pCCYCD varchar2) return number;
end GW_PK_IBPS_OL2_IB_CONVERT_OUT;
/
create or replace package body GW_PK_IBPS_OL2_IB_CONVERT_OUT is
  m_IBPS_Type IBPS_MSG_LOG%rowtype;
  vSIBS_HOST  varchar2(20) := '10.2.1.1';
  vSIBS_USER  varchar2(20) := 'svoprdwh';
  vSIBS_PASS  varchar2(20) := 'svoprdwh';
  vHo011     varchar2(20) := '01302001';
  vHo040     varchar2(20) := '79302001';
  vSIBS_SQL   varchar2(4000) := 'Select
       a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) RMMTIM,
       b.tlbf09,
       d.CFCIFN,
       d.CFNA1,
       d.CFNA1A,
       c.rdbr,
       b.tlbf01,
       a.rmbena,
       a.rmamt rmamt,
       a.rmsnme rmsnme,
       a.rmacno,
       a.rmcurr,
       b.tlbafm,
       b.tlbrmk,
       b.TLBID,
       substring(a.rmprdc, 3, 1) rmprdc,
       c.rdefth,
       a.rmpb40,
       b.Tlbdel,
       b.TlbCor,
       a.rmdis7,
       a.rmsts7,
       c.RDRACT,
       c.rdtxid,
       '' '' as DEFAULT1,
       '' '' as DEFAULT2,
       '' '' as DEFAULT3,
       '' '' as DEFAULT4,
       ''IB'' as DEFAULT5
  FROM SVDATPV51.RMMAST A,
       SVDATPV51.TLLOG  B,
       SVDATPV51.RMDETL C,
       SVDATPV51.CFMAST d
 WHERE  
   b.TLBTCD = ''EB8277''
   and RMRCID <>''D''
   and b.TLTXOK=''Y''
   and a.rmprdc = ''OL2''
   and c.RDRACT = ''280898010''
   and b.tlbf01 = a.rmacno
   and c.rdacct = b.tlbf01
   and a.RMACIF = d.CFCIFN
   and (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) > ''@CHECKPOINT@''and (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6))< VARCHAR_FORMAT(CURRENT TIMESTAMP -2 minutes, ''YYYYDDDHH24MIMISS'')
   or (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) = ''@CHECKPOINT@'' and INT(substr(a.rmacno,length(trim(c.rdbr))+8))  >  INT(substr(''@CHECKPOINT1@'',length(trim(c.rdbr))+8)))) order by RMMTIM ASC';
   -- or (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) = ''@CHECKPOINT@'' and a.rmacno >  ''@CHECKPOINT1@'')) order by RMMTIM ASC';


  Procedure GetMessageFromSIbs is
    vResult      varchar2(4000);
    vcheckpoint  varchar2(200);
    vcheckpoint1 varchar2(200);
  Begin
    Begin
      select value
        into vcheckpoint
        from sysvar sysv
       where sysv.varname = 'IBPS_OL2_CHECKPOINT_IB'
         and sysv.gwtype = 'IBPS'
         and rownum = 1;
    
    Exception
      when others then
        INSERT INTO SYSVAR
          (GWTYPE, VARNAME, VALUE, NOTE, TYPE)
        VALUES
          ('IBPS',
           'IBPS_OL2_CHECKPOINT_IB',
           '1',
           TO_CHAR(SYSDATE, 'YYYYMMDD'),
           'STRING ');
        commit;
    end;
    begin
      select value
        into vcheckpoint1
        from sysvar sysv
       where sysv.varname = 'IBPS_OL2_CHECKPOINT_IB1'
         and sysv.gwtype = 'IBPS'
         and rownum = 1;
    Exception
      when others then
        INSERT INTO SYSVAR
          (GWTYPE, VARNAME, VALUE, NOTE, TYPE)
        VALUES
          ('IBPS',
           'IBPS_OL2_CHECKPOINT_IB1',
           '1',
           TO_CHAR(SYSDATE, 'YYYYMMDD'),
           'STRING ');
        commit;
    end;
    vSIBS_SQL := replace(vSIBS_SQL, '@CHECKPOINT@', vcheckpoint);
    vSIBS_SQL := replace(vSIBS_SQL, '@CHECKPOINT1@', vcheckpoint1);		
    vResult   := getol2(vSIBS_SQL, vSIBS_HOST, vSIBS_USER, vSIBS_PASS);
    -- Insert into Test (Text, Content) values (vResult, vSIBS_SQL);
    commit;
  End GetMessageFromSIbs;
  -- Convert dien

  /*
  #110000823
  #003101001
  #00701302004
  #01220100531000000
  #01901202002
  #02131302001
  #02201202002
  #0252000
  #026VND
  #02714650643300
  #028CTY TNHH NGUYEN LAP8627
  #029
  #0305199
  #031CHI CUC HQ QUAN LY HANG DAU TU GIA CONG
  #032
  #033741010200004
  #034REM       CTY TNHH NGUYEN LAP MST: 0101565775 NOP THUE GTGT HANG NK MC754 K191 TM1702 KY THUE 05/2010  THEO TO KHAI HQ, QD SO 9480 NGAY 17/05/10 , LOAI HINH XNK: NHAP KINH DOANH NH HUONG: KBNN NGO QUYEN - TP HAI PHONG ( )
  #03630
  #037100
  
  */
  Procedure ConvertIBPS_OL2 IS
    pRowIBPS           IBPS_MSG_CONTENT%Rowtype;
    pQUERY_ID          NUMBER(20);
    vMSG_DIRECTION     VARCHAR2(10) := 'SIBS-IBPS';
    vTRANS_CODE        VARCHAR2(10);
    vGW_TRANS_NUM      NUMBER(8);
    vSIBS_TRANS_NUM    NUMBER(16);
    vTRANS_DATE        DATE;
    vAMOUNT            NUMBER(19, 2);
    vCCYCD             VARCHAR2(3);
    vSTATUS            NUMBER(3) := 0;
    vERR_CODE          NUMBER(3);
    vTRANS_DESCRIPTION VARCHAR2(512);
    vDEPARTMENT        VARCHAR2(10);
    vCONTENT           VARCHAR2(4000);
    vSOURCE_BRANCH     VARCHAR2(5);
    vTAD               VARCHAR2(50);
    vRM_NUMBER         VARCHAR2(20);
    vFWSTS             NUMBER(1) := 0;
    vTELLERID          VARCHAR2(20);
    vRECEIVING_TIME    DATE;
    vF07               VARCHAR2(12);
    vF19               VARCHAR2(12);
    vF21               VARCHAR2(12);
    vF22               VARCHAR2(12);
    TRANSDATE          NUMBER(8);
    MSG_SRC            NUMBER(1) := 4;
    PRODUCT_TYPE       VARCHAR2(5) := 'OL2';
    SIBS_TELLERID      VARCHAR2(10);
    iCount             number(20) := 0;
    IB_CutOff_time     number(10) := 1457;
    --
    IBPS_Start_time number(10) :=0300;
    
    Cursor vMSGOUT is
      Select *
        from IBPS_SIBS_OL2 IO
       Where IO.STATUS = 0
         and Rownum < 100
         and IO.Attribute5 = 'IB'
         And to_number(to_char(IO.Transdate, 'YYYYMMDDHH24MI')) <
             to_number(to_char(sysdate, 'YYYYMMDDHH24MI')) 
      
       order by IO.MSGID asc;
    --m_IBPSTypeOutIN IBPS_TYPE_CONVERTOUT_OL3;
    --m_VCBTypeOutIN  VCB_TYPE_CONVERTOUT_OL2;
    m_vMSGOUT vMSGOUT%Rowtype;
    --mError          varchar2(2000);
    vremark        varchar2(4000);
    iLV_HV         boolean;
    isWorking_date number(1) := 0;
    isWeekendWorking number(1):=0;
  BEGIN
   --
    begin
      select to_number(value)
        into IBPS_Start_time
        from sysvar
       where varname = 'IBPS_START_TIME'
         and GWTYPE = 'IBPS'
         and rownum = 1;
    exception
      when others then
        IBPS_Start_time := 0400;
    end;
  
    begin
      select to_number(value)
        into IB_CutOff_time
        from sysvar
       where varname = 'IBPS_IB_CUTOFF_TIME'
         and GWTYPE = 'IBPS'
         and rownum = 1;
    exception
      when others then
        IB_CutOff_time := 1457;
    end;
  
    Select count(1)
      into iCount
      from IBPS_SIBS_OL2 IO
     Where IO.STATUS = 0
       and Rownum < 100
       and IO.Attribute5 = 'IB';
    if iCount > 0 then
      -- 03-11-2011 Update Goi ham check lai cac dien huy truoc khi thuc hien convert dien
      GW_UPDATE_MSG_REVERT.Get_ibps_cancel;
      -- Het update
    end if;
  
    OPEN vMSGOUT;
  
    select count(1)
      into isWorking_date
      from GW_WORKING_DAY GWD
     where to_char(GWD.OFFDAY, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD');
  
    if (isWorking_date > 0) then
      return;
    end if;
  
    --if (to_char(sysdate, 'HH24Mi') < '0730') then
    --if (to_number(to_char(sysdate, 'HH24Mi')) < IBPS_Start_time) then
		if (to_number(substr(m_vMSGOUT.Attribute2,8,4)) < IBPS_Start_time) then
      return;
    end if;
    --if (to_number(to_char(sysdate, 'HH24Mi')) > IB_CutOff_time) then
		if (to_number(substr(m_vMSGOUT.Attribute2,8,4)) > IB_CutOff_time) then
      return;
    end if;
    
    select count(1)
      into isWeekendWorking
      from GW_WEEKEND_WORKING GWW
     where to_char(GWW.WORKING_DATE, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD');
     
    if trim(upper(to_char(sysdate, 'Day'))) = 'SUNDAY' AND isWeekendWorking=0 then
      return;
    end if;
    if trim(upper(to_char(sysdate, 'Day'))) = 'SATURDAY' AND isWeekendWorking=0 then
      return;
    end if;
  
    LOOP
      Fetch vMSGOUT
        into m_vMSGOUT;
      EXIT WHEN vMSGOUT %notfound;
      pQUERY_ID := m_vMSGOUT.Msgid;
			--check cutoff time
			if (to_number(to_char(sysdate, 'HH24Mi')) < IBPS_Start_time) then
        goto continue_cursor;
      end if;
   
		  if (to_number(m_vMSGOUT.Attribute2) > to_number(to_char(sysdate, 'yyyyDDD')||'000000')+ IB_CutOff_time) then
        goto continue_cursor;
      end if;
			
      update IBPS_SIBS_OL2
         set status = 2, senddate = to_char(sysdate, 'YYYYMMDD')
       where MSGID = pQUERY_ID;
      COMMIT;
      vF19 := Trim(substr(m_vMSGOUT.Content, 598, 10));
      vF22 := RECEIPVER_BANK_MAP(vF19);
      IF (GW_PK_IBPS_PROCESS.IBPS_MSG_DUP_CHECK(2, m_vMSGOUT.RM_NUMBER) > 0) then
      
        if CheckISVCB(m_vMSGOUT.Amount, vF22) = 0 then
          -- Dien di citad
        
          vAMOUNT            := to_number(m_vMSGOUT.Amount);
          vCCYCD             := trim(m_vMSGOUT.Ccycd);
          vTRANS_CODE        := GetTranCode(vAMOUNT);
          vGW_TRANS_NUM      := IBPS_GetCurRef;
          vSIBS_TRANS_NUM    := 1;
          vTRANS_DATE        := Sysdate;
          vSTATUS            := 0;
          vERR_CODE          := 0;
          vTRANS_DESCRIPTION := m_vMSGOUT.Remark;
          vDEPARTMENT        := 'RM';
          vSOURCE_BRANCH     := m_vMSGOUT.Branchcreate;
          vRM_NUMBER         := m_vMSGOUT.Rm_Number;
          vFWSTS             := 0;
          vRECEIVING_TIME    := Sysdate;
          vF07               := GW_PK_IBPS_PROCESS.GETBANKCODE(LPAD(vSOURCE_BRANCH,
                                                                    5,
                                                                    '0'),
                                                               2);
        
          --vF21               := '01302001';
         /* if vTRANS_CODE = '201001' then
            iLV_HV := false;
          else
            iLV_HV := true;
          end if;*/
            --vF21               := '01302001';
          if vTRANS_CODE = '201001' then
            iLV_HV := false;
						if (substr(vF19, 3, 3) = '204') then
              -- chi nhanh thuoc agribank
              vF21 := vHo011;
            else
            vF21   := GW_PK_IBPS_PROCESS.IBPS_GETOPROXYCI(vF07,
                                                          vF19,
                                                          pQUERY_ID,
                                                          vAMOUNT,
                                                          iLV_HV,
                                                          vSOURCE_BRANCH);
						end if;																							
          else
            iLV_HV := true;
            if (substr(vF19, 3, 3) = '204') then
              -- chi nhanh thuoc agribank
              vF21 := vHo011;
            else
              vF21 := vHo040;
            end if;
          
          end if;
        
         /* vF21 := GW_PK_IBPS_PROCESS.IBPS_GETOPROXYCI(vF07,
                                                      vF19,
                                                      pQUERY_ID,
                                                      vAMOUNT,
                                                      iLV_HV,
                                                      vSOURCE_BRANCH);*/
        
          TRANSDATE     := to_number(to_char(sysdate, 'YYYYMMDD'));
          MSG_SRC       := 5;
          PRODUCT_TYPE  := 'OL2';
          vTAD          := GW_PK_IBPS_PROCESS.GETBANKCODE(vF21, 1);
          SIBS_TELLERID := m_vMSGOUT.Tellerid;
          vTELLERID     := m_vMSGOUT.Tellerid;
          vremark       := trim(m_vMSGOUT.Remark) || ' ' ||
                          
                           trim(m_vMSGOUT.Rmpb40) || ' (NHH: ' ||
                           trim(substr(m_vMSGOUT.Content, 640, 40)) || '-' ||
                           trim(substr(m_vMSGOUT.Content, 681, 40)) || ')';
          vCONTENT      := '#110' || LPAD(vGW_TRANS_NUM, 8, '0') || '#003' ||
                           vTRANS_CODE || '#007' || vF07 || '#012' ||
                           to_char(Sysdate, 'YYYYMMDDHH24miSS') || '#019' || vF19 ||
                           '#021' || vF21 || '#022' || vF22 ||
                           '#0252000#026' || vCCYCD || '#027' ||
                           vAMOUNT * 100 || '#028' || m_vMSGOUT.Attribute4 ||
                           '#029' || '' || '#030' ||
                           trim(LPAD(m_vMSGOUT.Senderacc, 14, '0')) ||
                           '#031' ||
                           trim(Substr(m_vMSGOUT.Content, 558, 40)) || ' ' ||
                           trim(Substr(m_vMSGOUT.Content, 972, 30)) ||
                           '#032' || '#033' ||
                           trim(Substr(m_vMSGOUT.Content, 538, 20)) ||
                           '#034' || trim(vremark) || '#03630' || '#037100';
        
          if length(vremark) > 210 then
            vERR_CODE := 1;
          end if;
          if GW_PK_IBPS_Q_CONVERTOUT.Check_Branch(TRim(vF19)) = false then
            vERR_CODE := 14;
          end if;
          if GW_PK_IBPS_Q_CONVERTOUT.Check_Branch(TRim(vF21)) = false then
            vERR_CODE := 20;
          end if;
          if GW_PK_IBPS_Q_CONVERTOUT.Check_Branch(TRim(vF22)) = false then
            vERR_CODE := 21;
          end if;
          if GW_PK_IBPS_Q_CONVERTOUT.Check_Branch(TRim(vF07)) = false then
            vERR_CODE := 19;
          end if;
        
          if length(vremark) > 210 then
            vERR_CODE := 1;
          end if;
          if vERR_CODE > 0 then
            vSTATUS := -1;
          else
            vSTATUS := 0;
          end if;
        
          pRowIBPS.Sibs_Tellerid     := vTellerID;
          pRowIBPS.Status            := to_number(vSTATUS);
          pRowIBPS.Query_Id          := pQUERY_ID;
          pRowIBPS.File_Name         := ' ';
          pRowIBPS.Msg_Direction     := vMSG_DIRECTION;
          pRowIBPS.Trans_Code        := vTRANS_CODE;
          pRowIBPS.Gw_Trans_Num      := vGW_TRANS_NUM;
          pRowIBPS.Sibs_Trans_Num    := vSIBS_TRANS_NUM;
          pRowIBPS.F07               := vF07;
          pRowIBPS.F19               := vF19;
          pRowIBPS.F21               := vF21;
          pRowIBPS.F22               := vF22;
          pRowIBPS.Trans_Date        := sysdate;
          pRowIBPS.Transdate         := to_char(pRowIBPS.Trans_Date,
                                                'YYYYMMDD');
          pRowIBPS.Amount            := vAMOUNT;
          pRowIBPS.Ccycd             := vCCYCD;
          pRowIBPS.Product_Type      := 'OL2';
          pRowIBPS.Print_Sts         := 0;
          pRowIBPS.Err_Code          := NVL(vERR_CODE, 0);
          pRowIBPS.Trans_Description := vremark;
          pRowIBPS.Department        := vDEPARTMENT;
          pRowIBPS.Content           := vCONTENT;
          pRowIBPS.Source_Branch     := LPAD(vSOURCE_BRANCH, 5, '0');
        
          pRowIBPS.Tad := vTAD;
        
          pRowIBPS.Pre_Tad        := '';
          pRowIBPS.Rm_Number      := vRM_NUMBER;
          pRowIBPS.Receiving_Time := Sysdate;
          pRowIBPS.Msg_Src        := MSG_SRC;
          --QuanLD:  Them moi truong nay phuc vu citad 2.1
           pRowIBPS.Desc_Tax := vremark;
        -- Het them
          GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                                 'IBPS_Job_CONVERT_OUT',
                                                 vMSG_DIRECTION);
        
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := pQUERY_ID;
          m_IBPS_Type.Status        := vSTATUS;
          m_IBPS_Type.Descriptions  := 'Convert Message success:' ||
                                       SQLCODE || ' -ERROR- ' || SQLERRM;
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := vMSG_DIRECTION;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          if (vSTATUS = 0) then
            update IBPS_SIBS_OL2
               set status = 1, RECEIVER = 'IBPS'
             where MSGID = pQUERY_ID;
          else
            update IBPS_SIBS_OL2
               set status = -1, RECEIVER = 'IBPS'
             where MSGID = pQUERY_ID;
          end if;
        else
          -- Dien di VCB
          update IBPS_SIBS_OL2 set status = 2 where MSGID = pQUERY_ID;
        
          VCB_CONVERTOUT_OL2(m_vMSGOUT.Branchcreate,
                             m_vMSGOUT.Senderacc,
                             
                             m_vMSGOUT.Rmbena,
                             m_vMSGOUT.Amount,
                             m_vMSGOUT.Ccycd,
                             m_vMSGOUT.Attribute4,
                             m_vMSGOUT.Rm_Number,
                             m_vMSGOUT.Content,
                             '01',
                             m_vMSGOUT.Senderacc,
                             m_vMSGOUT.Tellerid,
                             '',
                             '123' || m_vMSGOUT.Remark,
                             substr(m_vMSGOUT.Content, 640, 40),
                             m_vMSGOUT.Rmpb40,
                             '1',
                             ' ',
                             ' ',
                             m_vMSGOUT.Senderacc,
                             ' ',
                             ' ',
                             ' ',
                             ' ',
                             ' ');
        
          if (vSTATUS = 0) then
            update IBPS_SIBS_OL2
               set status = 1, RECEIVER = 'VCB'
             where MSGID = pQUERY_ID;
          else
            update IBPS_SIBS_OL2
               set status = -1, RECEIVER = 'VCB'
             where MSGID = pQUERY_ID;
          end if;
        end if;
      
      else
        update IBPS_SIBS_OL2 set status = -2 where MSGID = pQUERY_ID;
      
      end if;
      COMMIT;
						
			--Lable khong lam gi ca. Tuong duong voi viec continue trong for
      <<continue_cursor>>
      null;
			
    End loop;
  Exception
    when others then
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := -1;
      m_IBPS_Type.Descriptions  := 'Error When getmessage from MSG content Message' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_INQUITYOUT';
      m_IBPS_Type.Msg_Direction := 'SIBS-IBPS';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      update IBPS_SIBS_OL2 set status = -1 where MSGID = pQUERY_ID;
      COMMIT;
  END ConvertIBPS_OL2;

  FUNCTION IBPS_GetCurRef Return varchar2 IS
    pinRef    Number(7);
    pvCurDate varchar2(8);
  Begin
    -- lay so RM ra theo ngay
    BEGIN
      SELECT SYSVAR.VALUE, SYSVAR.NOTE
        INTO pinRef, pvCurDate
        FROM SYSVAR
       WHERE SYSVAR.VARNAME = 'IBPSSIBSSeqNumOL2_IB'
         AND SYSVAR.GWTYPE = 'IBPS'
         And rownum = 1;
    
      if (pvCurDate = To_char(SYSDATE, 'YYYYMMDD')) Then
        pinRef := pinRef + 1;
      else
        pinRef    := 1;
        pvCurDate := To_char(SYSDATE, 'YYYYMMDD');
      end if;
    
      -- cap nhat lai
      IPBS_UPDATESYSVAR(To_char(pinRef), pvCurDate);
    
    EXCEPTION
      WHEN Others Then
        BEGIN
          INSERT INTO SYSVAR
            (GWTYPE, VARNAME, VALUE, NOTE, TYPE)
          VALUES
            ('IBPS',
             'IBPSSIBSSeqNumOL2_IB',
             '1',
             TO_CHAR(SYSDATE, 'YYYYMMDD'),
             'STRING ');
        EXCEPTION
          WHEN Others Then
            pinRef := 1;
        END;
        pinRef := 1;
    END;
  -- 20141104: QuanLD: Sua lai so but toan tu 6 so sang 8 so
    Return '05' || LPAD(pinRef, 6, '0');
  end IBPS_GetCurRef;

  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2) IS
  BEGIN
    UPDATE SYSVAR
       SET VALUE = pinRef, SYSVAR.NOTE = pvCurDate
     WHERE VARNAME = 'IBPSSIBSSeqNumOL2_IB'
       AND GWTYPE = 'IBPS';
    commit;
  EXCEPTION
    WHEN OTHERS THEN
      Dbms_Output.put_line(SQLCODE);
      Return;
  END IPBS_UPDATESYSVAR;

  FUNCTION GetTranCode(vAmount number) return varchar2 IS
  BEGIN
    if vAmount >= 500000000 then
      return '201001';
    else
      if to_number(to_char(sysdate, 'HH24')) > 15 then
        return '201001';
      else
        return '101001';
      end if;
    
    end if;
  
  END;
  -- 1 di VCB, 0 di citad
  Function CheckISVCB(vAmount number, receiverBranch varchar) return int is
  Begin
    if (substr(receiverBranch, 3, 3) <> '203') then
      return 0;
    else
      if vAmount >= 500000000 then        
        return 1;
      else
        /*if to_number(to_char(sysdate, 'HH24mi')) > 1513 then        
          return 1;
        else
          return 0;
        end if;*/
        return 0;
      end if;
    end if;
  end CheckISVCB;

  -- Map Ma chi nhanh nhan
  FUNCTION RECEIPVER_BANK_MAP(vF19 Varchar2) return varchar2 IS
    vF22 varchar2(30);
  Begin
    vF22 := '';
  
    select RBM.RECEIVERCODE
      into vF22
      from receiptbankmap RBM
     where RBM.BENEFITCODE = vF19
       and rownum = 1;
    return vF22;
  Exception
    when others then
      vF22 := vF19;
      return vF22;
    
  END RECEIPVER_BANK_MAP;

  PROCEDURE VCB_CONVERTOUT_OL2(m_vRDBR     VARCHAR2,
                               m_vTLBF01   VARCHAR2,
                               vRMBENA     VARCHAR2,
                               m_vRMAMT    VARCHAR2,
                               m_vRMCURR   VARCHAR2,
                               m_vRMSNME   VARCHAR2,
                               m_vRMACNO   VARCHAR2,
                               m_vTLBAFM   VARCHAR2,
                               m_vTLBRMK   VARCHAR2,
                               m_vTLBF09   VARCHAR2,
                               m_vTLBID    VARCHAR2,
                               m_vRMPRDC   VARCHAR2,
                               vRDEFTH     VARCHAR2,
                               vRMPB40     VARCHAR2,
                               m_vTLBDEL   VARCHAR2,
                               m_vTLBCOR   VARCHAR2,
                               m_vRMDIS7   VARCHAR2,
                               m_vRMSTS7   VARCHAR2,
                               m_vRDRACT   VARCHAR2,
                               m_vDEFAULT1 VARCHAR2,
                               m_vDEFAULT2 VARCHAR2,
                               m_vDEFAULT3 VARCHAR2,
                               m_vDEFAULT4 VARCHAR2,
                               m_vDEFAULT5 VARCHAR2) IS
  
    i integer;
    --Bien lay du lieu tu Queue   
    pRowVCB         VCB_MSG_CONTENT%Rowtype;
    RPLChar         GW_PK_VCB_PROCESS.RPLCharType;
    pMSG_TYPE       varchar2(5) := 'MT103';
    pMSG_DIRECTION  varchar2(8) := 'SIBS-VCB';
    pDEPARTMENT     varchar2(2);
    mChar           char(2) := CHR(13) || chr(10);
    m_vPRODUCT_TYPE varchar2(5);
    -- Du lieu can lay   
    m_vQUERY_ID Number(10);
    m_vF20      VARCHAR2(30);
    --m_vF20      VARCHAR2(1);
    m_vF32A    VARCHAR2(50);
    m_vF50K    VARCHAR2(190);
    m_vF53B    VARCHAR2(30);
    m_vF57D    VARCHAR2(100);
    m_vF59     VARCHAR2(220);
    m_vF70     VARCHAR2(300);
    m_vF71A    VARCHAR2(10);
    m_vCONTENT VARCHAR2(2000);
    m_vDATEYMD VARCHAR2(6);
    vF20       varchar2(12);
    vF70       varchar2(260);
    vF591      varchar2(120); --Ten nguoi huong
    vF592      varchar2(70); ---Dia chi nguoi nhan
    vF593      varchar2(50); ---ten
    vSCMT      varchar2(40); --So CMT
    vNC_NC     varchar2(50); --Ngay cap Noi cap
    vF702      varchar2(90);
    vTemp      varchar2(50);
    --Them vao truong 50K;
    m_vTKNG    varchar2(14); --Tai khoan nguoi gui
    m_vDCNG    varchar2(70); --dia chi nguoi gui
    m_vPRODUCT varchar2(5); --Tinh xem la OL2,OL3
    m_vNOSTRO  varchar2(20);
    m_vRMPB40  varchar2(4000);
    m_vRMBENA  varchar2(500);
    m_vRDEFTH  varchar2(4000);
    
    --Minhhb them truong 57E cho cac dien di VCB
    m_vF57E varchar2(90);
  
  BEGIN
  
    select to_char(sysdate, 'YYMMDD') into m_vDATEYMD from dual;
  
    Begin
    
      RPLChar := GW_PK_VCB_PROCESS.LoadReplaceChar(pMSG_TYPE, 'SIBS-VCB'); --Ky tu la
    
      select SEQ_IBPS_QUERY.NEXTVAL INTO m_vQUERY_ID From dual;
      m_vNOSTRO := '';
    
      -----------------------------------------------
      --CONVERT
    
      m_vRMPB40   := GW_PK_VCB_PROCESS.RplaceChar(vRMPB40, '57', 1, RPLChar);
      pDEPARTMENT := substr(m_vTLBAFM, 0, 2); --Loai dien
      vSCMT       := trim(substr(m_vTLBAFM, 598, 40));
      vNC_NC      := Replace(trim(substr(m_vTLBAFM, 885, 93)), '000000');
      vF20        := GET_FIELD20;
      m_vF20      := ':20:A' || vF20 || mChar;
      m_vF32A     := ':32A:' || m_vDATEYMD || trim(m_vRMCURR) ||
                     Replace(trim(m_vRMAMT), '.', ',') || mChar;
      vTemp       := trim(substr(m_vTLBAFM, 282, 40));
      --m_vF50K     := ':50K:' || substr(m_vTLBAFM, 282, 40) || mChar;
      --Them hai dong nay vao truong 50K
      if m_vPRODUCT = 'OL2' then
        m_vTKNG := trim(m_vTLBF09); --TK
      else
        m_vTKNG := trim(m_vRDRACT); --TK
      end if;
      m_vDCNG := replace(trim(substr(m_vTLBAFM, 365, 75)), '     ', ''); --Dia chi
      /*if vTemp is NULL then*/
      m_vF50K := ':50K:' || trim(m_vRMSNME) || mChar || m_vTKNG || mChar;
      if trim(m_vDCNG) is not null then
        m_vF50K := m_vF50K || m_vDCNG || mChar;
      end if;
    
      m_vNOSTRO := BIDV_ACCOUNT(trim(m_vRMCURR));
      m_vF53B   := ':53B:/' || m_vNOSTRO || mChar;
    
      m_vF57D := ':57D:' || m_vRMPB40 || mChar;
      vF591   := trim(substr(m_vTLBAFM, 558, 40)) || ' ' ||
                 trim(Substr(m_vTLBAFM, 972, 30));
      --trim(Substr(m_vMSGOUT.Content, 538, 20))
    
      vF592 := replace(trim(substr(m_vTLBAFM, 641, 75)), '     ', '');
      begin
        vF593 := trim(substr(M_vTLBAFM, 978, 40));
      exception
        when others then
          vF593 := '';
      end;
      -- 59
      m_vRMBENA := GW_PK_VCB_PROCESS.RplaceChar(vRMBENA, '59', 1, RPLChar);
      vF591     := GW_PK_VCB_PROCESS.RplaceChar(vF591, '59', 2, RPLChar);
      vF592     := GW_PK_VCB_PROCESS.RplaceChar(vF592, '59', 3, RPLChar);
    
      if trim(vF591) is null then
        if trim(vF593) is not null then
          vF591 := vF593;
        end if;
      else
        if trim(vF593) is not null then
          vF591 := vF591 || ' ' || vF593;
        end if;
      end if;
    
      if trim(vF591) is null then
        if trim(vF592) is null then
          m_vF59 := ':59:/' || m_vRMBENA || mChar;
        else
          m_vF59 := ':59:/' || m_vRMBENA || mChar || vF592 || mChar;
        end if;
      else
        --Minhhb comment out. truong 59 chi chua tai khoan va dia chi nguoi huong
        /*if trim(vF592) is null then
          m_vF59 := ':59:/' || m_vRMBENA || mChar || vF591 || mChar;
        else
          m_vF59 := ':59:/' || m_vRMBENA || mChar || vF591 || mChar ||
                    vF592 || mChar;
        end if;*/
         m_vF59 := ':59:/' || m_vRMBENA || mChar || vF591 || mChar;
      end if;
      
      -- Minhhb them truong 57E luu dia chi nguoi huong voi cac dien di VCB
        if trim(vF592) is null then 
          m_vF57E :=':57E:' || mChar;
        else
          m_vF57E :=':57E:' || vF592 || mChar;  
        end if;  
      
      --TEST
      /*m_vF59 := ':59:/' || '0071004145165' || mChar ||
      'Quynd test test test test' || mChar ||
      'Quynd test test test test' || mChar ||
      'Quynd test test test test' || mChar ;*/
      ----Het 59    
      --Ko bo ky tu la    
      m_vRDEFTH := GW_PK_VCB_PROCESS.RplaceChar(vRDEFTH, '70', 3, RPLChar);
      vF70      := substr(trim(m_vRDEFTH), 4, 217);
      vF702     := '';
    
      if trim(vF702) is null then
        /*m_vF70 := ':70:(' || m_vTLBF01 || ')' || trim(vF70) || mChar;*/
        m_vF70 := ':70:' || trim(vF70) || mChar;
      else
        /*m_vF70 := ':70:(' || m_vTLBF01 || ')' || trim(vF70) || '(' ||
        vF702 || ')' || mChar;*/
        m_vF70 := ':70:' || trim(vF70) || '(' || vF702 || ')' || mChar;
      end if;
      m_vF71A    := ':71A:OUR';
      --Minhhb them truong 57E
      m_vCONTENT := m_vF20 || m_vF32A || m_vF50K || m_vF53B || m_vF57D || m_vF57E ||
                    m_vF59 || m_vF70 || m_vF71A;
      if VCB_currency(trim(m_vRMCURR)) = 0 then
        --Neu loai tien nay chua duoc khai bao cho VCB
        pRowVCB.STATUS   := -1;
        pRowVCB.ERR_CODE := 4; --GW not accept CCYCD
      else
        if trim(m_vNOSTRO) = '' or trim(m_vNOSTRO) is null then
          pRowVCB.STATUS   := -1;
          pRowVCB.ERR_CODE := 7; --Ko co tai khoan NT
        else
          -- Minh check them length cua trong 57E neu lon hon 40 ky tu se bao loi.
          if length(trim(m_vF70)) > 128 or length(trim(vF592))>40 then
            pRowVCB.STATUS   := -1;
            pRowVCB.ERR_CODE := 1;
          else
            pRowVCB.STATUS   := 0;
            pRowVCB.ERR_CODE := VCBCheckDup(m_vRMACNO,
                                            to_char(sysdate, 'YYYYMMDD'));
          end if;
        end if;
      end if;
    
      m_vPRODUCT_TYPE := trim(substr(m_vTLBAFM, 38, 5)); --Loai SP
      ---
      pRowVCB.Msg_Id        := 1;
      pRowVCB.QUERY_ID      := m_vQUERY_ID;
      pRowVCB.MSG_TYPE      := pMSG_TYPE; --MT103
      pRowVCB.MSG_DIRECTION := pMSG_DIRECTION; --SIBS-VCB
      if m_vPRODUCT_TYPE = 'OL2' then
        pRowVCB.BRANCH_A := lpad(substr(m_vTLBF01,
                                        0,
                                        length(m_vTLBF01) - 12),
                                 5,
                                 '0');
      elsif m_vPRODUCT_TYPE = 'OL3' then
        pRowVCB.BRANCH_A := '00011';
      end if;
    
      --Sua moi
      /* if m_vPRODUCT_TYPE = 'OL3' then
        \* pRowVCB.BRANCH_A      := lpad(substr(m_vTLBF01,
               0,
               length(m_vTLBF01) - 12),
        5,
        '0');*\
        pRowVCB.BRANCH_A := '00011';
      else
      
      end if;*/
    
      pRowVCB.BRANCH_B   := 'BFTVVNVXXXX';
      pRowVCB.TRANS_DATE := sysdate;
      pRowVCB.VALUE_DATE := sysdate;
      pRowVCB.FIELD20    := Replace(replace(m_vF20, ':20:'), mChar);
      pRowVCB.FIELD21    := ''; --truong nay = rong
      pRowVCB.AMOUNT     := replace(m_vRMAMT, ',', '.'); --Lay ra so tien
      pRowVCB.CCYCD      := m_vRMCURR; --Lay ra loai tien
      -- 0 CONVERTED/1 SENT/2 PENDING/-1 ERROR
    
      pRowVCB.DEPARTMENT        := pDEPARTMENT; --RM
      pRowVCB.HEADER_CONTENT    := '';
      pRowVCB.CONTENT           := replace(m_vCONTENT, '#', mChar);
      pRowVCB.FILE_NAME         := '';
      pRowVCB.FOREIGN_BANK      := '';
      pRowVCB.TRANS_NO          := '';
      pRowVCB.RM_NUMBER         := '00000' || m_vRMACNO;
      pRowVCB.RECEIVING_TIME    := sysdate;
      pRowVCB.SENDING_TIME      := null;
      pRowVCB.MSG_SRC           := 5;
      pRowVCB.FOREIGN_BANK_NAME := '';
      pRowVCB.PRIORITY          := 0;
      pRowVCB.Product_Type      := m_vPRODUCT_TYPE; --m_vTLBID
      pRowVCB.Sibs_Tellerid     := m_vTLBID;
      --VCB_INSERT_TABLE(pRowVCB); --Insert du lieu vao bang content va bang outward
      gw_pk_vcb_q_convert_out.VCB_Insert_TABLE(pRowVCB);
      if pRowVCB.ERR_CODE = 1 then
        GW_PK_VCB_PROCESS.VCB_Msg_Trace(sysdate,
                                        m_vQUERY_ID,
                                        -1,
                                        'Error :Message field too long',
                                        'VCB_OL2_JOB_CONVERTOUT');
      end if;
    
    EXCEPTION
    
      When others then
        GW_PK_VCB_PROCESS.VCB_Msg_Trace(sysdate,
                                        m_vQUERY_ID,
                                        -1,
                                        'VCB_DE_CONVERTOUT' || SQLERRM,
                                        'VCB_OL2_JOB_CONVERTOUT');
        GW_PK_OL3_CONVERTOUT.IBPS_OL3_Update(m_vQUERY_ID, -1, 'OL2');
    end;
  
    commit;
  Exception
    when OTHERS THEN
      GW_PK_VCB_PROCESS.VCB_Msg_Trace(sysdate,
                                      m_vQUERY_ID,
                                      -1,
                                      'VCB_EQ_CONVERTOUT' || SQLERRM,
                                      'VCB_OL2_JOB_CONVERTOUT');
      GW_PK_OL3_CONVERTOUT.IBPS_OL3_Update(m_vQUERY_ID, -1, 'OL2');
    
  END VCB_CONVERTOUT_OL2;

  FUNCTION GET_FIELD20 return varchar2 is
    nSIBSNum integer;
    RefNum   varchar2(20) := '0';
    vDate    varchar2(8);
    vResult  varchar2(12);
  BEGIN
    Begin
    
      SELECT Value, Note
        Into RefNum, vDate
        FROM SYSVAR
       WHERE VARNAME = 'VCBSeqNum_New'
         and GWTYPE = 'VCB';
    
      if (vDate = to_char(sysdate, 'DDMMYY')) then
        nSIBSNum := to_number(RefNum) + 1;
      else
        nSIBSNum := 1;
        vDate    := to_char(sysdate, 'DDMMYY');
      end if;
    
      Update Sysvar
         set Value = nSIBSNum, Note = vDate
       where VARNAME = 'VCBSeqNum_New'
         and GWTYPE = 'VCB';
      commit;
    exception
      when others then
        nSIBSNum := 1;
        vDate    := to_char(sysdate, 'DDMMYY');
        Update Sysvar
           set Value = nSIBSNum, Note = vDate
         where VARNAME = 'VCBSeqNum_New'
           and GWTYPE = 'VCB';
        commit;
    end;
    vResult := to_char(sysdate, 'DDMMYY') || LPAD(nSIBSNum, 4, '0');
    return vResult;
  
  exception
    when others then
      return '';
  end;

  PROCEDURE VCB_INSERT_TABLE(pVCB_CONTENT IN VCB_MSG_CONTENT%ROWTYPE) IS
    mVCB_CT      VCB_MSG_CONTENT%ROWTYPE;
    pVCB_outward VCB_MSG_outward%ROWTYPE;
    --pv           varchar2(200) := ''; exception
  BEGIN
    mVCB_CT           := pVCB_CONTENT;
    mVCB_CT.transdate := to_char(mVCB_CT.trans_date, 'YYYYMMDD');
  
    SAVEPOINT SP1;
    INSERT INTO VCB_MSG_CONTENT VALUES mVCB_CT;
  
    pVCB_outward.MSG_ID         := pVCB_content.MSG_ID;
    pVCB_outward.QUERY_ID       := pVCB_content.QUERY_ID;
    pVCB_outward.TRANS_DATE     := pVCB_content.TRANS_DATE;
    pVCB_outward.MSG_TYPE       := pVCB_content.MSG_TYPE;
    pVCB_outward.REF_NO         := pVCB_content.FIELD20;
    pVCB_outward.BRANCH_A       := pVCB_content.BRANCH_A; --chua lay duoc
    pVCB_outward.BRANCH_B       := pVCB_content.BRANCH_B; -- chua lay duoc
    pVCB_outward.FOREIGN_BANK   := pVCB_content.FOREIGN_BANK;
    pVCB_outward.HEADER_CONTENT := '';
    pVCB_outward.CONTENT        := pVCB_content.Content;
    pVCB_outward.FILE_NAME      := '';
    pVCB_outward.Status         := 0;
  
    SAVEPOINT SP2;
    INSERT INTO VCB_MSG_OUTWARD VALUES pVCB_OUTWARD;
    Update VCB_INQUIRY_HOST VIH
       set VIH.Status = 1
     where VIH.MSG_ID = pVCB_content.Query_Id;
  
    commit;
  Exception
    when OTHERS THEN
      RAISE_APPLICATION_ERROR(-20001, 'VCB_Insert_TABLE:' || SQLERRM);
    
  END VCB_INSERT_TABLE;

  FUNCTION VCBCheckDup(pRMACNO NUMBER, pDATE_NOW varchar2) return NUMBER IS
  
    pDATE_SYS varchar2(8);
    iCount    number(5);
  BEGIN
    iCount    := 0;
    pDATE_SYS := '20100721';
    /*insert into vcb_ol2_index
      (RMACNO, DATE_NOW)
    values
      (pRMACNO, pDATE_SYS);
    commit;*/
    select count(1)
      into iCount
      from vcb_ol2_index
     where RMACNO = pRMACNO
       and trim(DATE_NOW) = '20100721';
    if iCount > 0 then
      Return 2;
    else
      insert into vcb_ol2_index
        (RMACNO, DATE_NOW)
      values
        (pRMACNO, pDATE_SYS);
      commit;
      Return 0;
    end if;
  Exception
    when OTHERS THEN
      Return 2;
  END VCBCheckDup;

  Function BIDV_ACCOUNT(pCCYCD varchar2) return varchar2 is
  
    m_vReturn varchar2(20);
  begin
    m_vReturn := '';
    select account
      into m_vReturn
      from BIDV_ACCOUNT
     where trim(CCYCD) = trim(pCCYCD)
       and rownum = 1;
    return m_vReturn;
  exception
    when others then
      return '';
  end;

  Function VCB_currency(pCCYCD varchar2) return number is
  
    iCount number(5);
  begin
    iCount := 0;
    select count(1)
      into iCount
      from currency
     where trim(CCYCD) = trim(pCCYCD)
       and trim(GWTYPE) = 'VCB'
       and STATUS = 1;
    return iCount;
  exception
    when others then
      return 0;
  end VCB_currency;

end GW_PK_IBPS_OL2_IB_CONVERT_OUT;
/
