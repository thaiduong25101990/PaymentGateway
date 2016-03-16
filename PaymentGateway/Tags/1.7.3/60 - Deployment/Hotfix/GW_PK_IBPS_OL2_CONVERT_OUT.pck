create or replace package GW_PK_IBPS_OL2_CONVERT_OUT is
  Procedure GetMessageFromSIbs;
  Procedure ConvertIBPS_OL2;
  FUNCTION IBPS_GetCurRef Return varchar2;
  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2);
  FUNCTION GetTranCode(vAmount number) return varchar2;
  FUNCTION RECEIPVER_BANK_MAP(vF19 Varchar2) return varchar2;
end GW_PK_IBPS_OL2_CONVERT_OUT;
/
create or replace package body GW_PK_IBPS_OL2_CONVERT_OUT is
  m_IBPS_Type IBPS_MSG_LOG%rowtype;
   vSIBS_HOST  varchar2(20) := '10.2.1.1';
  vSIBS_USER  varchar2(20) := 'svoprdwh';
  vSIBS_PASS  varchar2(20) := 'svoprdwh';
  vSIBS_SQL   varchar2(4000) := 'select * from(

Select       a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) RMMTIM,
       b.tlbf09,
       trim(d.CFCIFN) CFCIFN,
       trim(d.CFNA1) CFNA1,
       trim(d.CFNA1A) CFNA1A,
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
       '''' as DEFAULT1,
       '''' as DEFAULT2,
       '''' as DEFAULT3,
       '''' as DEFAULT4,
       ''TAX'' as DEFAULT5
  FROM SVDATPV51.RMMAST A,
       SVDATPV51.TLLOG  B,
       SVDATPV51.RMDETL C,
       SVDATPV51.CFMAST d
WHERE b.TLBTCD = ''TP8277''
   and a.rmprdc = ''OL2''
    and b.TLTXOK=''Y''
   and (c.RDRACT = ''280701018'' or c.RDRACT = ''120101001'')
   and b.tlbf01 = a.rmacno
   and c.rdacct = b.tlbf01
   and a.RMACIF = d.CFCIFN
UNION ALL
Select       a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) RMMTIM,
       b.tlbf09,
       '''' CFCIFN,
       trim(a.rmsnme) rmsnme,
       '''' CFNA1A,
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
       '''' as DEFAULT1,
       '''' as DEFAULT2,
       '''' as DEFAULT3,
       '''' as DEFAULT4,
       ''TAX'' as DEFAULT5
  FROM SVDATPV51.RMMAST A,
       SVDATPV51.TLLOG  B,
       SVDATPV51.RMDETL C       
WHERE b.TLBTCD = ''TP8278''
 and b.TLTXOK=''Y''
   and a.rmprdc = ''OL2''
   and (c.RDRACT = ''280701018'' or c.RDRACT = ''120101001'')
   and b.tlbf01 = a.rmacno
   and c.rdacct = b.tlbf01) a
  Where (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) > ''@CHECKPOINT@''
  or (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) = ''@CHECKPOINT@'' and a.rmacno>  ''@CHECKPOINT1@'')) order by RMMTIM ASC';

  Procedure GetMessageFromSIbs is
    vResult      varchar2(4000);
    vcheckpoint  varchar2(200);
    vcheckpoint1 varchar2(200);
  Begin
    select value
      into vcheckpoint
      from sysvar sysv
     where sysv.varname = 'IBPS_OL2_CHECKPOINT'
       and sysv.gwtype = 'IBPS'
       and rownum = 1;
    select value
      into vcheckpoint1
      from sysvar sysv
     where sysv.varname = 'IBPS_OL2_CHECKPOINT1'
       and sysv.gwtype = 'IBPS'
       and rownum = 1;
    vSIBS_SQL := replace(vSIBS_SQL, '@CHECKPOINT@', vcheckpoint);
    vSIBS_SQL := replace(vSIBS_SQL, '@CHECKPOINT1@', vcheckpoint1);
    vResult   := getol2(vSIBS_SQL, vSIBS_HOST, vSIBS_USER, vSIBS_PASS);
    --Insert into Test (Text, Content) values (vResult, vSIBS_SQL);
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
  #028CTY TNHH NGUYEN LAP
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
    vSTATUS            NUMBER(3);
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
  
    Cursor vMSGOUT is
      Select *
        from IBPS_SIBS_OL2 IO
       Where IO.STATUS = 0
         and IO.ATTRIBUTE5 = 'TAX'
         and Rownum < 100
       order by IO.MSGID asc;
    m_IBPSTypeOutIN IBPS_TYPE_CONVERTOUT_OL3;
    m_VCBTypeOutIN  VCB_TYPE_CONVERTOUT_OL2;
    m_vMSGOUT       vMSGOUT%Rowtype;
    mError          varchar2(2000);
    vremark         varchar2(4000);
    isWeekendWorking number(1):=0;
    isWorking_date number(1) := 0;
  BEGIN
  
    select count(1)
      into isWorking_date
      from GW_WORKING_DAY GWD
     where to_char(GWD.OFFDAY, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD');
  
    if (isWorking_date > 0) then
      return;
    end if;    
  
    if (to_char(sysdate, 'HH24Mi') < '0730') then
      return;
    end if;
    if (to_number(to_char(sysdate, 'HH24Mi')) > 1500) then
      return;
    end if;
    
    select count(1)
      into isWeekendWorking
      from GW_WEEKEND_WORKING GWW
     where to_char(GWW.WORKING_DATE, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD');
    
    if trim(upper(to_char(sysdate, 'Day'))) = 'SUNDAY' and isWeekendWorking=0 then
      return;
    end if;
    if trim(upper(to_char(sysdate, 'Day'))) = 'SATURDAY' and isWeekendWorking=0 then
      return;
    end if;
    
    OPEN vMSGOUT;
    LOOP
      Fetch vMSGOUT
        into m_vMSGOUT;
      EXIT WHEN vMSGOUT %notfound;
      pQUERY_ID := m_vMSGOUT.Msgid;
     update IBPS_SIBS_OL2
         set status = 2, senddate = to_char(sysdate, 'YYYYMMDD')
       where MSGID = pQUERY_ID;
      COMMIT;
      /*if to_char(sysdate, 'YYYYMMDD') =
         to_char(m_vMSGOUT.Transdate, 'YYYYMMDD') And  then
        return;
      end if;*/
    
      IF (GW_PK_IBPS_PROCESS.IBPS_MSG_DUP_CHECK(2, m_vMSGOUT.RM_NUMBER) > 0) then
      
        update IBPS_SIBS_OL2 set status = 2 where MSGID = pQUERY_ID;
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
        vF19               := Trim(substr(m_vMSGOUT.Content, 640, 10));
        vF21               := Trim(substr(m_vMSGOUT.Content, 680, 10));
        -- Get R-Proxy CI Code from R-CI Code.
        vF22               := RECEIPVER_BANK_MAP(vF19);
        TRANSDATE          := to_number(to_char(sysdate, 'YYYYMMDD'));
        MSG_SRC            := 4;
        PRODUCT_TYPE       := 'OL2';
        vTAD               := GW_PK_IBPS_PROCESS.GETSIBSTADCODE(vF21);
        SIBS_TELLERID      := m_vMSGOUT.Tellerid;
        vTELLERID          := m_vMSGOUT.Tellerid;
        vremark            := trim(m_vMSGOUT.Remark) || ' ' ||
                              trim(m_vMSGOUT.Rmpb40) || ' ' ||
                              trim(Substr(m_vMSGOUT.Content, 884, 11));
        vCONTENT           := '#110' || LPAD(vGW_TRANS_NUM, 6, '0') ||
                              '#003' || vTRANS_CODE || '#007' || vF07 ||
                              '#012' ||
                              to_char(Sysdate, 'YYYYMMDDHH24miSS') ||
                              '#019' || vF19 || '#021' || vF21 || '#022' || vF22 ||
                              '#0252000#026' || vCCYCD || '#027' ||
                              vAMOUNT * 100 || '#028' ||
                              m_vMSGOUT.Attribute4 || '#029' || '' ||
                              '#030' || trim(m_vMSGOUT.Senderacc) || '#031' ||
                              trim(Substr(m_vMSGOUT.Content, 557, 70)) ||
                              '#032' || '#033' ||
                              trim(Substr(m_vMSGOUT.Content, 538, 20)) ||
                              '#034' || trim(vremark) || '#03630' ||
                              '#037100';
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
        GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                               'IBPS_Job_CONVERT_OUT',
                                               vMSG_DIRECTION);
      
        m_IBPS_Type.Log_Date      := sysdate;
        m_IBPS_Type.Query_Id      := pQUERY_ID;
        m_IBPS_Type.Status        := vSTATUS;
        m_IBPS_Type.Descriptions  := 'Convert Message success:' || SQLCODE ||
                                     ' -ERROR- ' || SQLERRM;
        m_IBPS_Type.Area          := ' ';
        m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
        m_IBPS_Type.Msg_Direction := vMSG_DIRECTION;
        GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
        if (vSTATUS = 0) then
          update IBPS_SIBS_OL2 set status = 1 where MSGID = pQUERY_ID;
        else
          update IBPS_SIBS_OL2 set status = -1 where MSGID = pQUERY_ID;
        end if;
      
      else
        update IBPS_SIBS_OL2 set status = -2 where MSGID = pQUERY_ID;
      
      end if;
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
       WHERE SYSVAR.VARNAME = 'IBPSSIBSSeqNumOL2'
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
             'IBPSSIBSSeqNumOL2',
             '1',
             TO_CHAR(SYSDATE, 'YYYYMMDD'),
             'STRING ');
        EXCEPTION
          WHEN Others Then
            pinRef := 1;
        END;
        pinRef := 1;
    END;
  
    Return '06' || LPAD(pinRef, 4, '0');
  end IBPS_GetCurRef;

  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2) IS
  BEGIN
    UPDATE SYSVAR
       SET VALUE = pinRef, SYSVAR.NOTE = pvCurDate
     WHERE VARNAME = 'IBPSSIBSSeqNumOL2'
       AND GWTYPE = 'IBPS';
    commit;
  EXCEPTION
    WHEN OTHERS THEN
      Dbms_Output.put_line(SQLCODE);
      Return;
  END IPBS_UPDATESYSVAR;

  --Them moi map ma gian tiep truc tiep
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
  --------------------------------------


  FUNCTION GetTranCode(vAmount number) return varchar2 IS
  BEGIN
    if vAmount > 499999999 then
      return '201001';
    else
      return '101001';
    
    end if;
  
  END;
end GW_PK_IBPS_OL2_CONVERT_OUT;
/
