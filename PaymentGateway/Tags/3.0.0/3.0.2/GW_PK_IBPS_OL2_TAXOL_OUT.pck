create or replace package GW_PK_IBPS_OL2_TAXOL_OUT is

  Procedure GetMessageFromSIbs;
  Procedure ConvertIBPS_OL2;
  FUNCTION IBPS_GetCurRef Return varchar2;
  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2);
  FUNCTION GetTranCode(vAmount number) return varchar2;
  FUNCTION RECEIPVER_BANK_MAP(vF19 Varchar2) return varchar2;

end GW_PK_IBPS_OL2_TAXOL_OUT;
/
create or replace package body GW_PK_IBPS_OL2_TAXOL_OUT is

  m_IBPS_Type IBPS_MSG_LOG%rowtype;
  vSIBS_HOST  varchar2(20) := '10.2.1.1';
  vSIBS_USER  varchar2(20) := 'svoprdwh';
  vSIBS_PASS  varchar2(20) := 'svoprdwh';
  vHo011      varchar2(20) := '01302001';
  vHo040      varchar2(20) := '79302001';
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
       ''TAXONLINE'' as DEFAULT5
  FROM SVDATPV51.RMMAST A,
       SVDATPV51.TLLOG  B,
       SVDATPV51.RMDETL C,
       SVDATPV51.CFMAST d,
       SVDATPV51.DDMEMO M
 WHERE  
   b.TLBTCD = ''TO8277''
   and RMRCID <>''D''
   and a.rmprdc = ''OL2''
   and (c.RDRACT = ''120101001'' or c.RDRACT = ''280898015'' )
   and b.tlbf01 = a.rmacno
   and c.rdacct = b.tlbf01
   and b.tlbf09=m.ACCTNO
   and m.CIFNO = d.CFCIFN
   and (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) > ''@CHECKPOINT@''
   or (a.rmdis7 || RIGHT(''000000''||a.RMMTIM ,6) = ''@CHECKPOINT@'' and INT(substr(a.rmacno,length(c.rdbr)+8))  >  INT(substr(''@CHECKPOINT1@'',length(c.rdbr)+8)))) order by RMMTIM ASC';

Procedure GetMessageFromSIbs is
    vResult      varchar2(4000);
    vcheckpoint  varchar2(200);
    vcheckpoint1 varchar2(200);
  Begin
     Begin
      select value
        into vcheckpoint
        from sysvar sysv
       where sysv.varname = 'IBPS_OL2_TAXOL_CHECKPOINT'
         and sysv.gwtype = 'IBPS'
         and rownum = 1;
    
    Exception
      when others then
        INSERT INTO SYSVAR
          (GWTYPE, VARNAME, VALUE, NOTE, TYPE)
        VALUES
          ('IBPS',
           'IBPS_OL2_TAXOL_CHECKPOINT',
           '1',
           TO_CHAR(SYSDATE, 'YYYYMMDD'),
           'STRING ');
        commit;
    end;
    begin
      select value
        into vcheckpoint1
        from sysvar sysv
       where sysv.varname = 'IBPS_OL2_TAXOL_CHECKPOINT1'
         and sysv.gwtype = 'IBPS'
         and rownum = 1;
    Exception
      when others then
        INSERT INTO SYSVAR
          (GWTYPE, VARNAME, VALUE, NOTE, TYPE)
        VALUES
          ('IBPS',
           'IBPS_OL2_TAXOL_CHECKPOINT1',
           '1',
           TO_CHAR(SYSDATE, 'YYYYMMDD'),
           'STRING ');
        commit;
    end;
    vSIBS_SQL := replace(vSIBS_SQL, '@CHECKPOINT@', vcheckpoint);
    vSIBS_SQL := replace(vSIBS_SQL, '@CHECKPOINT1@', vcheckpoint1);
    DBMS_OUTPUT.put_line(vSIBS_SQL);
    vResult := getol2(vSIBS_SQL, vSIBS_HOST, vSIBS_USER, vSIBS_PASS);
    Insert into Test (Text, Content) values (vResult, vSIBS_SQL);
    commit;
  End GetMessageFromSIbs;
  -- Convert dien

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
    vF28               VARCHAR2(40);
    TRANSDATE          NUMBER(8);
    MSG_SRC            NUMBER(1) := 4;
    PRODUCT_TYPE       VARCHAR2(5) := 'OL2';
    SIBS_TELLERID      VARCHAR2(10);
    iErrTax            varchar2(10) := '';
  
    -- Ngay 25/09/2014  cap nhat cho phan citad 2.1
    -- them doan d? li?u liên quan d?n mã s? thu?
    vf33       varchar2(100) := '';
    vremarkTax varchar2(3000) := '';
    iposTa     number(10) := 0;
    vMasot     varchar2(30) := '';
  
    vTaxAccCode     varchar2(2000);
    vTaxAccCode7111 varchar2(2000);
  
    -- het them
    Cursor vMSGOUT is
      Select *
        from IBPS_SIBS_OL2 IO
       Where IO.STATUS = 0
         and IO.ATTRIBUTE5 = 'TAXONLINE'
         and Rownum < 100
       order by IO.MSGID asc;
    m_IBPSTypeOutIN  IBPS_TYPE_CONVERTOUT_OL3;
    m_VCBTypeOutIN   VCB_TYPE_CONVERTOUT_OL2;
    m_vMSGOUT        vMSGOUT%Rowtype;
    mError           varchar2(2000);
    vremark          varchar2(4000);
    isWeekendWorking number(1) := 0;
    isWorking_date   number(1) := 0;
  BEGIN
  
    -- lay thong tin cac tai khoan thuoc dien NSNN
  
    Begin
      select value
        into vTaxAccCode
        from sysvar sysv
       where sysv.varname = 'IBPS_ACC_TAX_3942'
         and sysv.gwtype = 'IBPS'
         and rownum = 1;
    Exception
      when others then
        vTaxAccCode := ';3942;';
    end;
  
    Begin
      select value
        into vTaxAccCode7111
        from sysvar sysv
       where sysv.varname = 'IBPS_ACC_TAX_7111'
         and sysv.gwtype = 'IBPS'
         and rownum = 1;
    Exception
      when others then
        vTaxAccCode := ';3942;';
    end;
    ----------------------------
  
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
    
  
  /*  if (to_number(to_char(sysdate, 'HH24Mi')) > 1630) then
      return;
    end if;
  */
  
  
    select count(1)
      into isWeekendWorking
      from GW_WEEKEND_WORKING GWW
     where to_char(GWW.WORKING_DATE, 'YYYYMMDD') =
           to_char(sysdate, 'YYYYMMDD');
  
    if trim(upper(to_char(sysdate, 'Day'))) = 'SUNDAY' and
       isWeekendWorking = 0 then
      return;
    end if;
    if trim(upper(to_char(sysdate, 'Day'))) = 'SATURDAY' and
       isWeekendWorking = 0 then
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
        vF19               := Trim(substr(m_vMSGOUT.Content, 598, 10));
        vF21               := '01302001';
      
        -- Get R-Proxy CI Code from R-CI Code.
        vF22          := RECEIPVER_BANK_MAP(vF19);
        TRANSDATE     := to_number(to_char(sysdate, 'YYYYMMDD'));
        MSG_SRC       := 9;
        PRODUCT_TYPE  := 'OL2';
        vTAD          := GW_PK_IBPS_PROCESS.GETSIBSTADCODE(vF21);
        SIBS_TELLERID := m_vMSGOUT.Tellerid;
        vTELLERID     := m_vMSGOUT.Tellerid;
        -- Ngay 25/09/2014  cap nhat cho phan citad 2.1
        -- them doan d? li?u liên quan d?n mã s? thu?
        vf33 := trim(Substr(m_vMSGOUT.Content, 538, 20));
      
      vremarkTax := Rtrim(trim(Substr(m_vMSGOUT.Content, 900, 150)), '!') || '!' ||
                      Ltrim(trim(m_vMSGOUT.Remark), '!');
      vremark := replace( trim(m_vMSGOUT.Remark),'!');
      vremark := 'NTDT+'|| substr(vremark, instr(vremark,')')+1);
     
        -- 20141105: QuanLD: Nang cap cho citad 2.1
      
        --- vremark :=substr(vremark,instr(vremark,'.MST:')+1) ;        
        --------------------------------------------------------
        if instr(vTaxAccCode, ';' || vf33 || ';') > 0 then
        
          vremark := gw_pk_ibps_dblink_citad_2_1.convertmsgremark(vremarkTax,
                                                                  trim(m_vMSGOUT.Rmpb40),
                                                                  to_char(Sysdate,
                                                                          'YYYYMMDD'),
                                                                  4,
                                                                  iErrTax);
          -- Lay ma so thue:
        
        end if;
      
        if length(vremark) > 210 then
          vERR_CODE := 1;
        end if;
      
        -- Lay cho cac dien di thue co noi dung thu NSNN
      
        if instr(vTaxAccCode7111, ';' || vf33 || ';') > 0 then
          vremark   := 'NSNN,' || trim(m_vMSGOUT.Rmpb40);
          vERR_CODE := 0;
        
        end if;
      
        -- QuanLD: 20141104 update cho citad 2.1
        --- vf28 := trim(Substr(m_vMSGOUT.Content, 282, 40));
      
        vf28 := m_vMSGOUT.Attribute4;
        if length(trim(m_vMSGOUT.Senderacc)) < 13 then
          vf28 := trim(Substr(m_vMSGOUT.Content, 282, 40));
        end if;
     
        -- het cap nhat;
      
        vCONTENT := '#110' || LPAD(vGW_TRANS_NUM, 8, '0') || '#003' ||
                    vTRANS_CODE || '#007' || vF07 || '#012' ||
                    to_char(Sysdate, 'YYYYMMDDHH24miSS') || '#019' || vF19 ||
                    '#021' || vF21 || '#022' || vF22 || '#0252000#026' ||
                    vCCYCD || '#027' || vAMOUNT * 100 || '#028' || vf28 ||
                    '#029' || '' || '#030' || trim(m_vMSGOUT.Senderacc) ||
                    '#031' || trim(Substr(m_vMSGOUT.Content, 641, 40)) ||
                    '#032' || '#033' ||
                    trim(Substr(m_vMSGOUT.Content, 538, 20)) || '#034' ||
                    trim(vremark) || '#03630' || '#037100';
      
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
      
        pRowIBPS.Sibs_Tellerid := vTellerID;
      
        pRowIBPS.Query_Id       := pQUERY_ID;
        pRowIBPS.File_Name      := ' ';
        pRowIBPS.Msg_Direction  := vMSG_DIRECTION;
        pRowIBPS.Trans_Code     := vTRANS_CODE;
        pRowIBPS.Gw_Trans_Num   := vGW_TRANS_NUM;
        pRowIBPS.Sibs_Trans_Num := vSIBS_TRANS_NUM;
        pRowIBPS.F07            := vF07;
        pRowIBPS.F19            := vF19;
        pRowIBPS.F21            := vF21;
        pRowIBPS.F22            := vF22;
        pRowIBPS.Trans_Date     := sysdate;
        pRowIBPS.Transdate      := to_char(pRowIBPS.Trans_Date, 'YYYYMMDD');
        pRowIBPS.Amount         := vAMOUNT;
        pRowIBPS.Ccycd          := vCCYCD;
        pRowIBPS.Product_Type   := 'OL2';
        pRowIBPS.Print_Sts      := 0;
      
        pRowIBPS.Trans_Description := vremark;
        pRowIBPS.Department        := vDEPARTMENT;
        pRowIBPS.Content           := vCONTENT;
        pRowIBPS.Source_Branch     := LPAD(vSOURCE_BRANCH, 5, '0');
      
        pRowIBPS.Tad := vTAD;
      
        pRowIBPS.Pre_Tad        := '';
        pRowIBPS.Rm_Number      := vRM_NUMBER;
        pRowIBPS.Receiving_Time := Sysdate;
        pRowIBPS.Msg_Src        := MSG_SRC;
        --25/09/2014: QuanLD:  Them moi truong nay phuc vu citad 2.1
        pRowIBPS.Desc_Tax := vremarkTax;
      
        pRowIBPS.Content_Tax := GW_PK_IBPS_DBLINK_CITAD_2_1.ConvertMSGTax(vremarkTax,
                                                                          trim(Substr(m_vMSGOUT.Content,
                                                                                      557,
                                                                                      70)),
                                                                          to_char(Sysdate,
                                                                                  'YYYYMMDD'),
                                                                          4,
                                                                          iErrTax);
        -- Het them
        if (iErrTax is not null) then
          vERR_CODE := iErrTax;
        end if;
        pRowIBPS.Err_Code := NVL(vERR_CODE, 0);
        if pRowIBPS.Err_Code > 0 then
          vSTATUS := -1;
        else
          vSTATUS := 0;
        end if;
        pRowIBPS.Status := to_number(vSTATUS);
        GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                               'IBPS_OL2_JOB_INQUITYOUT_TAXOL',
                                               vMSG_DIRECTION);
      
        m_IBPS_Type.Log_Date      := sysdate;
        m_IBPS_Type.Query_Id      := pQUERY_ID;
        m_IBPS_Type.Status        := vSTATUS;
        m_IBPS_Type.Descriptions  := 'Convert Message success:' || SQLCODE ||
                                     ' -ERROR- ' || SQLERRM;
        m_IBPS_Type.Area          := ' ';
        m_IBPS_Type.Job_Name      := 'IBPS_OL2_JOB_INQUITYOUT_TAXOL';
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
    commit;
  Exception
    when others then
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := -1;
      m_IBPS_Type.Descriptions  := 'Error When getmessage from MSG content Message' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_OL2_JOB_INQUITYOUT_TAXOL';
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
       WHERE SYSVAR.VARNAME = 'IBPS_TAX_OLSeqNumOL2'
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
             'IBPS_TAX_OLSeqNumOL2',
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
    Return '065' || LPAD(pinRef, 5, '0');
  end IBPS_GetCurRef;

  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2) IS
  BEGIN
    UPDATE SYSVAR
       SET VALUE = pinRef, SYSVAR.NOTE = pvCurDate
     WHERE VARNAME = 'IBPS_TAX_OLSeqNumOL2'
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
    if vAmount >= 500000000 then
      return '201001';
    else
      if to_number(to_char(sysdate, 'HH24Mi')) > 1530 then
        return '201001';
      else
        return '101001';
      end if;
    
    end if;
  
  END;
end GW_PK_IBPS_OL2_TAXOL_OUT;
/
