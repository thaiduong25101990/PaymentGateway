create or replace package GW_PK_REC_VCB is

  type RefCurType is ref cursor;

  Procedure GetVCB_MSG_REC(pDate       in date,
                           vRec_type   varchar2,
                           vDirection  varchar2,
                           vDepartment varchar2,
                           vExp        varchar2,
                           vMsgtype    varchar2,
                           vReturn     in out RefCurType);

  Procedure GetVCB_MSG_CONTENT(pDate in date, vReturn in out RefCurType);

  Procedure Rec_Index(pDate    in date,
                      rec_type varchar2,
                      vIndex   in out varchar2);

  Procedure InsertVCB_MSG_CONTENT_TEMP(pDate in date);

  Procedure InsertVCB_MSG_REC_TEMP(pDate in date);

  procedure ReadFileRMTFRCON_VCB(vLine varchar2);
  procedure ReadFileRMOL3_VCB(vLine varchar2);

  Procedure ReadFileStatement_VCB(vLine varchar2, vCCYCD varchar2);

  Procedure RECONCILE_SIBS(pDate in date, pTellerID varchar2);

  Procedure RECONCILE_VCB(pDate in date, pTellerID varchar2);

  Procedure RECONCILE_TR(pDate in date, pTellerID varchar2);

  Procedure Reconcile_trace(pREC_NAME varchar2, pDescription varchar2);

  FUNCTION MSG_OF_VCB(vLine varchar2) RETURN BOOLEAN;

  TYPE MAPFILEROW IS RECORD(
    FNAME VARCHAR2(30),
    POS   NUMBER,
    LEN   NUMBER);

  TYPE MAPFILETABLE IS TABLE OF MAPFILEROW INDEX BY BINARY_INTEGER;

  FUNCTION GETVCBPARA(pDate in date) RETURN VARCHAR2;

end GW_PK_REC_VCB;
/
create or replace package body GW_PK_REC_VCB is

  -----------------------------------------------------------------------------------------
  -- GetData : cua bang SWIFT_MSG_REC
  -- HoangLA
  -----------------------------------------------------------------------------------------
  Procedure GetVCB_MSG_REC(pDate       in date,
                           vRec_type   varchar2,
                           vDirection  varchar2,
                           vDepartment varchar2,
                           vExp        varchar2,
                           vMsgtype    varchar2,
                           vReturn     in out RefCurType) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    open vReturn for
    --select * from VCB_MSG_REC
      select MSG_TYPE,
             rm_number ORG_ref_number,
             ref_number REF_NUMBER,
             to_char(AMOUNT, '999,999,999,999,999.99') AMOUNT,
             CCY,
             APP_CODE MODUL,
             (SELECT CONTENT
                FROM ALLCODE
               WHERE CDNAME LIKE 'RecView'
                 AND GWTYPE = 'VCB'
                 AND EXCEPTION_TYPE = CDVAL) REC_VIEW,
             VALUE_DATE,
             TRANS_DATE,
             REC_TYPE
        from VCB_MSG_REC
       where decode(vMsgtype, 'ALL', '1', msg_type) =
             decode(vMsgtype, 'ALL', '1', vMsgtype)
         and decode(vExp, 'ALL', '1', exception_type) =
             decode(vExp, 'ALL', '1', vExp)
         and decode(vDepartment, 'ALL', '1', app_code) =
             decode(vDepartment, 'ALL', '1', vDepartment)
         and msg_direction =
             decode(vDirection, 'ALL', msg_direction, vDirection)
         and rec_type = decode(vRec_type, 'ALL', rec_type, vRec_type)
         and NDATE = iNDATE;
  End GetVCB_MSG_REC;
  -----------------------------------------------------------------------------------------
  -- HoangLA
  -----------------------------------------------------------------------------------------
  Procedure GetVCB_MSG_CONTENT(pDate in date, vReturn in out RefCurType) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    open vReturn for
      select gw.msg_direction,
             gw.department,
             gw.status,
             GW.MSG_SRC,
             count(1)
        from ((select msg_direction, department, status, MSG_SRC
                 from vcb_msg_content
                where transdate = iNDATE) union all
              (select msg_direction, department, status, MSG_SRC
                 from vcb_msg_all
                where transdate = iNDATE) union all
              (select msg_direction, department, status, MSG_SRC
                 from vcb_msg_all_his
                where transdate = iNDATE)) gw
       group by gw.msg_direction, gw.department, gw.status, GW.MSG_SRC
       order by gw.msg_direction, gw.department, gw.status, GW.MSG_SRC;
  End GetVCB_MSG_CONTENT;
  -----------------------------------------------------------------------------------------
  -- HoangLA
  -----------------------------------------------------------------------------------------
  Procedure Rec_Index(pDate    in date,
                      rec_type varchar2,
                      vIndex   in out varchar2) Is
    iRecA   integer;
    iTotalA integer;
    iRecB   integer;
    iTotalB integer;
    iNDATE  number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    -- SIBS->BR
    if rec_type = 'SIBS->BR' then
      -- iTotalA
      select count(1)
        into iTotalA
        from vcb_msg_rec_temp
       where msg_direction = 'O'
         and rec_type = 'SIBS-BR'
         and NDATE = iNDATE;
      -- iTotalB
      select count(1)
        into iTotalB
        from vcb_msg_content_temp
       where status > -2
         and msg_direction = 'SIBS-VCB'
         AND DEPARTMENT <> 'TR'
         and NDATE = iNDATE
         and msg_src in (1, 5);
      -- iRecA
      select count(1)
        into iRecA
        from vcb_msg_rec
       where msg_direction = 'O'
         and rec_type = 'SIBS-BR'
         and exception_type = 'S'
         and NDATE = iNDATE;
      -- iRecB
      select count(1)
        into iRecB
        from vcb_msg_rec
       where msg_direction = 'O'
         and rec_type = 'SIBS-BR'
         and exception_type = 'GS'
         AND APP_CODE <> 'TR'
         and NDATE = iNDATE;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
    -- SIBS<-BR ()
    if rec_type = 'SIBS<-BR' then
      -- iTotalA
      select count(1)
        into iTotalA
        from vcb_msg_rec_temp
       where msg_direction = 'I'
         and rec_type = 'SIBS-BR'
         and NDATE = iNDATE;
      -- iTotalB
      select count(1)
        into iTotalB
        from vcb_msg_content_temp
       where status > 0
         and msg_direction = 'VCB-SIBS'
         and NDATE = iNDATE;
      -- iRecA
      select count(1)
        into iRecA
        from vcb_msg_rec
       where msg_direction = 'I'
         and rec_type = 'SIBS-BR'
         and exception_type = 'S'
         and NDATE = iNDATE;
      -- iRecB
      select count(1)
        into iRecB
        from vcb_msg_rec
       where msg_direction = 'I'
         and rec_type = 'SIBS-BR'
         and exception_type = 'GS'
         and NDATE = iNDATE;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
    -- GW -> VCB
    if rec_type = 'BR->VCB' then
      -- iTotalA
      select count(1)
        into iTotalA
        from vcb_msg_content_temp
       where status > 0
         and msg_direction = 'SIBS-VCB'
         and NDATE = iNDATE;
      -- iTotalB
      select count(1)
        into iTotalB
        from vcb_msg_rec_temp
       where msg_direction = 'O'
         and rec_type = 'VCB-BR'
         and NDATE = iNDATE;
      -- iRecA
      select count(1)
        into iRecA
        from vcb_msg_rec
       where msg_direction = 'O'
         and rec_type = 'VCB-BR'
         and exception_type = 'GV'
         and NDATE = iNDATE;
      -- iRecB
      select count(1)
        into iRecB
        from vcb_msg_rec
       where msg_direction = 'O'
         and rec_type = 'VCB-BR'
         and exception_type = 'V'
         and NDATE = iNDATE;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
    -- GW<-VCB
    if rec_type = 'BR<-VCB' then
      -- iTotalA
      select count(1)
        into iTotalA
        from vcb_msg_content_temp
       where status > -1
         and msg_direction = 'VCB-SIBS'
         and NDATE = iNDATE;
      -- iTotalB
      select count(1)
        into iTotalB
        from VCB_msg_rec_temp
       where msg_direction = 'I'
         and rec_type = 'VCB-BR'
         and NDATE = iNDATE;
      -- iRecA
      select count(1)
        into iRecA
        from vcb_msg_rec
       where msg_direction = 'I'
         and rec_type = 'VCB-BR'
         and exception_type = 'GV'
         and NDATE = iNDATE;
      -- iRecB
      select count(1)
        into iRecB
        from vcb_msg_rec
       where msg_direction = 'I'
         and rec_type = 'VCB-BR'
         and exception_type = 'V'
         and NDATE = iNDATE;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
    -- TR->GW
    if rec_type = 'TR->BR' then
      -- iTotalA
      select count(1)
        into iTotalA
        from vcb_msg_rec_temp
       where msg_direction = 'O'
         and rec_type = 'TR-BR'
         and NDATE = iNDATE;
      -- iTotalB
      select count(1)
        into iTotalB
        from vcb_msg_content_temp
       where status > 0
         and msg_direction = 'SIBS-VCB'
         and department = 'TR'
         and NDATE = iNDATE;
      -- iRecA
      select count(1)
        into iRecA
        from vcb_msg_rec
       where msg_direction = 'O'
         and rec_type = 'TR-BR'
         and exception_type = 'R'
         and NDATE = iNDATE;
      -- iRecB
      select count(1)
        into iRecB
        from vcb_msg_rec
       where msg_direction = 'O'
         and rec_type = 'TR-BR'
         and exception_type = 'GR'
         and NDATE = iNDATE;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
  End Rec_Index;
  -----------------------------------------------------------------------------------------
  -- HoangLA
  -----------------------------------------------------------------------------------------
  Procedure InsertVCB_MSG_CONTENT_TEMP(pDate in date) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    -- delete old msg if have
    delete from VCB_MSG_CONTENT_TEMP where NDATE = iNDATE;
    -- content
    Insert into VCB_MSG_CONTENT_TEMP
      (MSG_ID,
       Amount,
       CCYCD,
       msg_type,
       msg_direction,
       branch_a,
       branch_b,
       trans_date,
       field20,
       department,
       rm_number,
       status,
       NDATE,
       MSG_SRC,
       ORG_REF,
       QUERY_ID)
      (select MSG_ID,
              AMOUNT,
              CCYCD,
              msg_type,
              msg_direction,
              LPAD(BRANCH_A, 12, '0'),
              LPAD(BRANCH_B, 12, '0'),
              trans_date,
              field20,
              department,
              rm_number,
              status,
              iNDATE,
              MSG_SRC,
              TRANS_NO,
              QUERY_ID
         from VCB_MSG_CONTENT -- CONTENT
        where status = 1
          and TRANSDATE = iNDATE);
    -- all
    Insert into VCB_MSG_CONTENT_TEMP
      (MSG_ID,
       Amount,
       CCYCD,
       msg_type,
       msg_direction,
       branch_a,
       branch_b,
       trans_date,
       field20,
       department,
       rm_number,
       status,
       NDATE,
       MSG_SRC,
       ORG_REF,
       QUERY_ID)
      (select MSG_ID,
              Amount,
              CCYCD,
              msg_type,
              msg_direction,
              LPAD(BRANCH_A, 12, '0'),
              LPAD(BRANCH_B, 12, '0'),
              trans_date,
              field20,
              department,
              rm_number,
              status,
              iNDATE,
              MSG_SRC,
              TRANS_NO,
              QUERY_ID
         from VCB_MSG_ALL -- ALL
        where status = 1
          and TRANSDATE = iNDATE);
   
  
    commit;
  End InsertVCB_MSG_CONTENT_TEMP;
  -----------------------------------------------------------------------------------------
  -- HoangLA
  -----------------------------------------------------------------------------------------
  Procedure InsertVCB_MSG_REC_TEMP(pDate in date) Is
    mSTRSQL VARCHAR2(4000);
    iNDATE  number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
    jdbc    VARCHAR2(4000);
  Begin
  
    delete from vcb_msg_rec_temp
     where rec_type = 'TR-BR'
       and NDATE = iNDATE;
        delete from vcb_msg_rec_temp
     where rec_type = 'TR-VCB'
       and NDATE = iNDATE;
  
    if to_char(pDate, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD') then
    
      delete from vcb_msg_rec_temp
       where /*msg_direction = 'O'
                                     and */
       REC_TYPE = 'SIBS-BR'
       and ndate = iNDATE;
    
      commit;
      -- doi chieu bang lay du lieu qua jdbc
      -- dien ol3
      jdbc := reconile_readas400('select ''VCBOUT'',
            T1.RMPRDC,
            T3.RDBR,
            T1.RMAMT,
            T1.RMCURR,
            T1.RMACNO,
            T1.RMDIS6
       from SVDATPV51.RMMAST T1, SVDATPV51.TLLOG T2, SVDATPV51.RMDETL T3
      where ((T2.TLBF09 = ''120201005'' AND
            (T2.TLBTCD = ''8163'' OR T2.TLBTCD = ''8173'' OR T2.TLBTCD = ''8178'')) OR
            (T1.RMPRDC = ''OL2'' AND (T1.RMCBNK = ''30005'' OR T1.RMCBNK = ''99001'')))
        AND T2.TLBF01 = T1.RMACNO
        AND T3.RDACCT = T2.TLBF01
        AND T3.RDTXID = ''CRRM''
        AND T2.TLTXOK = ''Y''
        AND T2.TLBDEL = '' ''
        AND T2.TLBCOR = ''N''',
                                 '10.2.1.1',
                                 'svoprdwh',
                                 'svoprdwh',
                                 'VCB',
                                 'O',
                                 'OL3',
                                 to_char(pDate, 'YYYYMMDD'));
    
    
    
    end if;
    -- Xoa truoc du lieu
    delete from vcb_msg_rec_temp
     where /*msg_direction = 'O'
                                 and */
     REC_TYPE = 'SIBS-BR'
     and running_seg_no = '99999999'
     and ndate = iNDATE;
  
    --      lay dien OL2
    insert into vcb_msg_rec_temp
      (gw_type,
       rm_number,
       acc_type,
       running_seg_no,
       ref_number,
       msg_type,
       seg_no,
       msg_no,
       ccy,
       amount,
       app_code,
       sender,
       org_bank,
       receiving_branch,
       receiver,
       value_date,
       journal_seg_no,
       msg_direction,
       from_system,
       to_system,
       trans_date,
       rec_type,
       NDATE)
      (select 'VCB',
              ISO.RM_NUMBER,
              'X',
              '99999999',
              null,
              'XX',
              '',
              '',
              ccycd,
              amount,
              'RM',
              '',
              'XX',
              'XX',
              '',
              transdate,
              '',
              'O',
              'RM',
              'VCB',
              transdate,
              'SIBS-BR',
              iNDATE -- 12 so
         from IBPS_SIBS_OL2 ISO -- IBPS_TR
        where (status = 1 or status = -1)
          and ISO.Senddate = to_char(pdate, 'YYYYMMDD')
          and ISO.RECEIVER = 'VCB');
    -- dien ve il8
    jdbc := reconile_readas400('select RMACNO,RMAMT,RMCURR,RMCBNK,RMIBR,RMSTAT,RMPRDC  from SVDATPV51.RMMAST Where rmsts6=' ||
                               LTRIM(to_char(pDate, 'DDMMYY'), '0') ||
                               ' and rmprdc=''IL8''',
                               '10.2.1.1',
                               'svoprdwh',
                               'svoprdwh',
                               'VCB',
                               'I',
                               'IL8',
                               to_char(pDate, 'YYYYMMDD'));
    -- het doi chieu bang lay du lieu qua jdbc
  
    --Cap nhat doi chieu dien TR
    INSERT INTO vcb_msg_rec_temp
      (MSG_ID,
       GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       TRANS_DATE,
       REC_TYPE,
       NOTE,
       ndate)
      (select MSG_ID,
              'VCB',
              '',
              '',
              '0',
              FIELD20,
              MSG_TYPE,
              '0',
              '0',
              CCY,
              AMOUNT,
              'TR',
              BRANCH_A,
              BRANCH_B,
              '',
              '',
              VALUE_DATE,
              '0',
              DECODE(MSG_DIRECTION, 'SIBS-VCB', 'O', 'VCB-SIBS', 'I'),
              'SIBS',
              'VCB',
              TRANS_DATE,
              'TR-BR',
              '',
              iNDATE
         from SWIFT_TR
        where status > -1
          and to_char(TRANS_DATE, 'YYYYMMDD') = to_char(pDate, 'YYYYMMDD')
          AND MSG_DIRECTION = 'SIBS-VCB'
          and SWIFT_TR.DELIVER_TYPE = 'VCB');
  
    mSTRSQL := GETVCBPARA(pDate);
    IF mSTRSQL IS NOT NULL THEN
      mSTRSQL := mSTRSQL || 'and to_char(TRANS_DATE,''YYYYMMDD'')=' ||
                 chr(39) || to_char(pDate, 'YYYYMMDD') || chr(39) || ')';
      EXECUTE IMMEDIATE mSTRSQL;
    END IF;
  End;
  ------------------------------------------------------------------------
  -- HoangLA
  ------------------------------------------------------------------------
  FUNCTION GETVCBPARA(pDate in date) RETURN VARCHAR2 IS
    CURSOR C1 IS
      SELECT * FROM VCB_PARAMETER WHERE DEPARTMENT = 'TR';
    C1ROW  C1%ROWTYPE;
    vVALUE VARCHAR2(1000);
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  BEGIN
    OPEN C1;
    LOOP
      FETCH C1
        INTO c1ROW;
      EXIT WHEN C1%NOTFOUND;
      vVALUE := vVALUE || CASE
                  WHEN vVALUE IS NULL THEN
                   'AND '
                  ELSE
                   'OR '
                END || '(MSG_TYPE =' || CHR(39) || 'MT' || c1ROW.MSG_TYPE ||
                CHR(39) || ' AND SUBSTR(BRANCH_B,1,' || LENGTH(c1ROW.BANK_CODE) || ')=' ||
                CHR(39) || C1ROW.BANK_CODE || CHR(39) || ')';
    
    END LOOP;
    CLOSE C1;
    IF vVALUE IS NOT NULL THEN
      vVALUE := 'INSERT INTO vcb_msg_rec_temp ' ||
                '(MSG_ID,GW_TYPE,rm_number,ACC_TYPE,RUNNING_SEG_NO,ref_number,MSG_TYPE,SEG_NO,MSG_NO,CCY,AMOUNT,APP_CODE,SENDER,ORG_BANK,' ||
                'RECEIVING_BRANCH,RECEIVER,VALUE_DATE,JOURNAL_SEG_NO,MSG_DIRECTION,FROM_SYSTEM,TO_SYSTEM,TRANS_DATE,REC_TYPE,NOTE,NDATE)' ||
                '(select MSG_ID,''VCB'',NULL,NULL,''0'',FIELD20,MSG_TYPE,''0'',''0'',CCY,AMOUNT,''TR'',BRANCH_A,BRANCH_B,NULL,NULL,' ||
                'VALUE_DATE,0,DECODE(MSG_DIRECTION,''SIBS-SWIFT'',''O'',''SWIFT-SIBS'',''I''),''SIBS'',''VCB'',TRANS_DATE,''TR-BR'',NULL,' ||
                iNDATE || ' from SWIFT_TR where status>-1 ' || vVALUE || ' ';
    END IF;
    return vVALUE;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN NULL;
  END;
  -----------------------------------------------------------------------------------------
  -- HoangLA
  -----------------------------------------------------------------------------------------
  procedure ReadFileRMTFRCON_VCB(vLine varchar2) Is
    m_VCB_MSG_REC_TEMP VCB_MSG_REC_TEMP%Rowtype;
    vAmount            varchar2(20);
    vDirection         varchar2(20);
    vSender            varchar2(20);
    vOrgBank           varchar2(20);
    vReceivingBranch   varchar2(20);
    vValueDate         varchar2(20);
    vTransDate         varchar2(20);
    vTrans_date        varchar2(20);
    vTemp              varchar2(20);
    dDate              date;
    vBranchVCB         varchar2(20);
    vReceiving_Branch  varchar2(20);
  Begin
    -- Check SWIFT
    if (trim(substr(vLine, 1, 10)) = 'IBPS') THEN
    
      vDirection                 := trim(substr(vLine, 145, 1));
      m_VCB_MSG_REC_TEMP.Gw_Type := 'VCB';
    
      m_VCB_MSG_REC_TEMP.RM_NUMBER  := trim(substr(vLine, 12, 19));
      m_VCB_MSG_REC_TEMP.Ref_number := trim(substr(vLine, 36, 16));
      m_VCB_MSG_REC_TEMP.Msg_Type   := trim(substr(vLine, 52, 5));
      m_VCB_MSG_REC_TEMP.Ccy        := trim(substr(vLine, 65, 4));
      m_VCB_MSG_REC_TEMP.App_Code   := trim(substr(vLine, 88, 2));
    
      vAmount := ltrim(trim(substr(vLine, 70, 17)), '0');
    
      m_VCB_MSG_REC_TEMP.Amount := To_number(vAmount);
    
      Begin
        vTemp := ltrim(trim(substr(vLine, 96, 11)), '0'); -- 3 so
        select gw_bank_code
          into vOrgBank
          from ibps_bank_map
         where ltrim(sibs_bank_code, '0') = vTemp
           and rownum <= 1;
      exception
        when others then
          vOrgBank := '';
      End;
    
      vDirection := trim(substr(vLine, 153, 1));
      if (vDirection = 'O') then
      
        m_VCB_MSG_REC_TEMP.Sender   := trim(substr(vLine, 102, 11));
        m_VCB_MSG_REC_TEMP.Receiver := trim(substr(vLine, 124, 12));
        vBranchVCB                  := m_VCB_MSG_REC_TEMP.Receiver;
        Begin
        
          select gw_bank_code
            into vReceiving_Branch
            from ibps_bank_map
           where ltrim(sibs_bank_code, '0') =
                 ltrim(m_VCB_MSG_REC_TEMP.Sender, '0')
             and rownum = 1;
          m_VCB_MSG_REC_TEMP.Sender := vReceiving_Branch;
        exception
          when others then
            vReceiving_Branch := '';
        End;
      
      else
        m_VCB_MSG_REC_TEMP.Sender   := trim(substr(vLine, 102, 11));
        m_VCB_MSG_REC_TEMP.Receiver := trim(substr(vLine, 113, 11));
        Begin
        
          select gw_bank_code
            into vReceiving_Branch
            from ibps_bank_map
           where ltrim(sibs_bank_code, '0') =
                 ltrim(m_VCB_MSG_REC_TEMP.Receiver, '0')
             and rownum = 1;
          m_VCB_MSG_REC_TEMP.Receiver := trim(substr(vLine, 102, 11));
        exception
          when others then
            vReceiving_Branch := '';
        End;
        vBranchVCB := m_VCB_MSG_REC_TEMP.Receiver;
      end if;
    
      /*  m_VCB_MSG_REC_TEMP.Value_Date := to_date(LPAD(substr(vLine, 137, 6),
                                                      6,
                                                      '0'),
                                                 'DDMMYY');
      */
      m_VCB_MSG_REC_TEMP.Msg_Direction := vDirection;
      m_VCB_MSG_REC_TEMP.From_System   := trim(substr(vLine, 154, 10));
      m_VCB_MSG_REC_TEMP.To_System     := trim(substr(vLine, 164, 10));
      vtemp                            := LPAD(trim(substr(vLine, 175, 6)),
                                               6,
                                               '0');
      m_VCB_MSG_REC_TEMP.Trans_Date    := to_date(vtemp, 'DDMMYY');
      vTrans_date                      := to_char(m_VCB_MSG_REC_TEMP.Trans_Date,
                                                  'YYYYMMDD');
      m_VCB_MSG_REC_TEMP.Ndate         := to_number(vTrans_date);
      m_VCB_MSG_REC_TEMP.Rec_Type      := 'SIBS-BR';
    
      if (vBranchVCB = '01203001') then
        if (vDirection = 'I') and
           (substr(trim(m_VCB_MSG_REC_TEMP.Rm_Number), 1, 11) <>
           '9011' || substr(vTrans_date, 3) || '0') then
          insert into VCB_MSG_REC_TEMP values m_VCB_MSG_REC_TEMP;
          commit;
        end if;
      
      end if;
    
      --Reconcile_trace('[SIBS-BR][VCB]','=> Success');
    End If;
  Exception
    When others then
      Reconcile_trace('[SIBS-BR][VCB]', '=> miss one message');
  End ReadFileRMTFRCON_VCB;

  -----------------------------------------------------------------------------------------
  -- HoangLA
  -----------------------------------------------------------------------------------------
  procedure ReadFileRMOL3_VCB(vLine varchar2) Is
    m_VCB_MSG_REC_TEMP VCB_MSG_REC_TEMP%Rowtype;
    vAmount            varchar2(20);
    vDirection         varchar2(20);
    vSender            varchar2(20);
    vOrgBank           varchar2(20);
    vReceivingBranch   varchar2(20);
    vValueDate         varchar2(20);
    vTransDate         varchar2(20);
    vTrans_date        varchar2(20);
    vTemp              varchar2(20);
    dDate              date;
    vBranchVCB         varchar2(20);
    vReceiving_Branch  varchar2(20);
    icount             number(6);
  Begin
    -- Check SWIFT
    if (trim(substr(vLine, 1, 6)) = 'VCBOUT') THEN
    
      vDirection                 := 'O';
      m_VCB_MSG_REC_TEMP.Gw_Type := 'VCB';
    
      m_VCB_MSG_REC_TEMP.RM_NUMBER  := trim(substr(vLine, 48, 19));
      m_VCB_MSG_REC_TEMP.Ref_number := trim(substr(vLine, 48, 19));
      m_VCB_MSG_REC_TEMP.Trans_no   := m_VCB_MSG_REC_TEMP.Ref_Number;
      m_VCB_MSG_REC_TEMP.Msg_Type   := 'MT103';
      m_VCB_MSG_REC_TEMP.Ccy        := LTRIM(trim(substr(vLine, 43, 4)),
                                             '0');
      m_VCB_MSG_REC_TEMP.App_Code   := 'RM';
      m_VCB_MSG_REC_TEMP.Sender     := trim(substr(vLine, 20, 5));
      vAmount                       := ltrim(trim(substr(vLine, 26, 17)),
                                             '0');
    
      m_VCB_MSG_REC_TEMP.Amount := To_number(vAmount);
      begin
        vtemp := LPAD(trim(substr(vLine, 69, 7)), 6, '0');
      
        m_VCB_MSG_REC_TEMP.Trans_Date := to_date(vtemp, 'DDMMYY');
      Exception
        when others then
        
          vTrans_date := to_char(sysdate, 'YYYYMMDD');
      end;
    
      m_VCB_MSG_REC_TEMP.Value_Date := sysdate;
    
      m_VCB_MSG_REC_TEMP.Msg_Direction := vDirection;
      m_VCB_MSG_REC_TEMP.From_System   := vTrans_date;
      m_VCB_MSG_REC_TEMP.To_System     := vTrans_date;
      --m_VCB_MSG_REC_TEMP.Trans_Date    := sysdate;
    
      m_VCB_MSG_REC_TEMP.Ndate    := to_number(to_char(m_VCB_MSG_REC_TEMP.Trans_Date,
                                                       'YYYYMMDD'));
      m_VCB_MSG_REC_TEMP.Rec_Type := 'SIBS-BR';
      --if trim(m_VCB_MSG_REC_TEMP.Sender) = '11' then
      select count(1)
        into icount
        from VCB_MSG_REC_TEMP vc
       where vc.rm_number = m_VCB_MSG_REC_TEMP.Rm_Number
         and m_VCB_MSG_REC_TEMP.Rec_Type = 'SIBS-BR'
         AND VC.NDATE = m_VCB_MSG_REC_TEMP.Ndate;
      if icount = 0 then
        insert into VCB_MSG_REC_TEMP values m_VCB_MSG_REC_TEMP;
      end if;
      commit;
      --end if;
    
      --Reconcile_trace('[SIBS-BR][VCB]','=> Success');
    End If;
  Exception
    When others then
      Reconcile_trace('[SIBS-BR][VCB]', '=> miss one message');
  End ReadFileRMOL3_VCB;
  -------------------------------------------------------------------------------------------------
  -- Doc file Statement_VCB
  -- hienntm
  -------------------------------------------------------------------------------------------------
  FUNCTION MSG_OF_VCB(vLine varchar2) RETURN BOOLEAN IS
    CURSOR C1 IS
      SELECT *
        FROM VCB_PARAMETER
       WHERE DEPARTMENT <> 'TR'
         and gwtype = 'VCB';
    C1ROW     C1%ROWTYPE;
    SQLSTR    VARCHAR2(4000);
    mMSGTYPE  VARCHAR2(5);
    mDEPT     VARCHAR2(2);
    mBRANCH_A VARCHAR2(12);
    mBRANCH_B VARCHAR2(12);
    mDIRECT   VARCHAR2(1);
  
    n NUMBER := 0;
  BEGIN
    mMSGTYPE  := SUBSTR(vLine, 50, 5);
    mDEPT     := SUBSTR(vLine, 82, 2);
    mBRANCH_A := SUBSTR(vLine, 118, 12);
    mBRANCH_B := SUBSTR(vLine, 84, 8);
    mDIRECT   := SUBSTR(vLine, 145, 1);
  
    OPEN C1;
    LOOP
      FETCH C1
        INTO c1ROW;
      EXIT WHEN C1%NOTFOUND;
      SQLSTR := SQLSTR || CASE
                  WHEN SQLSTR IS NULL THEN
                   ''
                  ELSE
                   'OR '
                END || '(' || CHR(39) || mMSGTYPE || CHR(39) || '=' || CHR(39) || 'MT' ||
                c1ROW.MSG_TYPE || CHR(39) || ' AND SUBSTR(' || CHR(39) ||
                mBRANCH_A || CHR(39) || ',1,' || LENGTH(C1ROW.BANK_CODE) || ')=' ||
                CHR(39) || C1ROW.BANK_CODE || CHR(39) || ' AND ' || CHR(39) ||
                mDEPT || CHR(39) || '=' || CHR(39) || C1ROW.DEPARTMENT ||
                CHR(39) || ')';
    
    END LOOP;
    CLOSE C1;
    IF SQLSTR IS NOT NULL THEN
      SQLSTR := 'SELECT COUNT(*) FROM DUAL WHERE (' || SQLSTR || ' AND ' ||
                CHR(39) || mDIRECT || CHR(39) || '=''O'')' || ' OR (' ||
                CHR(39) || mDIRECT || CHR(39) || '=''I'' AND ' || CHR(39) ||
                mBRANCH_B || CHR(39) || '=''BFTVVNVX''' || ' AND ' ||
                CHR(39) || mMSGTYPE || CHR(39) || '=''MT103'')';
      EXECUTE IMMEDIATE SQLSTR
        INTO n;
    END IF;
  
    IF n > 0 THEN
      RETURN TRUE;
    ELSE
      RETURN FALSE;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN FALSE;
  END;
  -------------------------------------------------------------------------------------------------
  -- Doc file Statement_VCB
  -- HoangLA
  -------------------------------------------------------------------------------------------------
  Procedure ReadFileStatement_VCB(vLine varchar2, vCCYCD varchar2) Is
  
    mVCBREC    VCB_MSG_REC_TEMP%Rowtype;
    tblMF      MAPFILETABLE;
    mVAL       VARCHAR2(500);
    n          number;
    ii         number;
    vTransDate varchar(10);
  BEGIN
    tblMF(1).FNAME := 'TRANSDATE';
    tblMF(1).POS := 1;
    tblMF(1).LEN := 10;
    tblMF(2).FNAME := 'REFNO';
    tblMF(2).POS := 12;
    tblMF(2).LEN := 16;
    tblMF(3).FNAME := 'F21';
    tblMF(3).POS := 29;
    tblMF(3).LEN := 16;
    tblMF(4).FNAME := 'TYPE';
    tblMF(4).POS := 46;
    tblMF(4).LEN := 3;
    tblMF(5).FNAME := 'AMOUNT';
    tblMF(5).POS := 50;
    tblMF(5).LEN := 17;
    tblMF(6).FNAME := 'DESC';
    tblMF(6).POS := 78;
    tblMF(6).LEN := 0;
  
    mVAL := TRIM(SUBSTR(vLine, 1, 4));
    IF mVAL <> 'Date' and NVL(mVAL, ' ') <> ' ' THEN
    
      FOR i IN tblMF.FIRST .. tblMF.LAST LOOP
        IF tblMF(i).LEN = 0 THEN
          mVAL := TRIM(SUBSTR(vLine, tblMF(i).POS));
        ELSE
          mVAL := TRIM(SUBSTR(vLine, tblMF(i).POS, tblMF(i).len));
        END IF;
        IF tblMF(i).FNAME = 'TRANSDATE' THEN
          mVCBREC.TRANS_DATE := CASE
                                  WHEN TO_DATE(mVAL, 'DD/MM/YYYY') = '09-SEP-2008' THEN
                                   SYSDATE
                                  ELSE
                                   TO_DATE(mVAL, 'DD/MM/YYYY')
                                END;
          mVCBREC.VALUE_DATE := TO_DATE(mVAL, 'DD/MM/YYYY');
          vTransDate         := to_char(mVCBREC.TRANS_DATE, 'YYYYMMDD');
          mVCBREC.NDATE      := to_number(vTransDate);
        ELSIF tblMF(i).FNAME = 'REFNO' THEN
          mVCBREC.ref_number := replace(mVAL, '.', '') ||
                                to_char(mVCBREC.VALUE_DATE, 'DDMMYY');
        ELSIF tblMF(i).FNAME = 'TYPE' THEN
          if mVal = 'C' then
            mVCBREC.MSG_DIRECTION := 'I';
            mVCBREC.FROM_SYSTEM   := 'VCB';
            mVCBREC.TO_SYSTEM     := 'SIBS';
            mVCBREC.SENDER        := 'BFTVVNVX';
            mVCBREC.RECEIVER      := '011';
          else
            mVCBREC.MSG_DIRECTION := 'O';
            mVCBREC.FROM_SYSTEM   := 'SIBS';
            mVCBREC.TO_SYSTEM     := 'VCB';
            mVCBREC.SENDER        := '011';
            mVCBREC.RECEIVER      := 'BFTVVNVX';
          end if;
        ELSIF tblMF(i).FNAME = 'AMOUNT' THEN
          mVCBREC.AMOUNT := mVAL;
        ELSIF tblMF(i).FNAME = 'DESC' THEN
          IF mVCBREC.MSG_DIRECTION = 'O' then
            -- n                  := instr(mval, '{//}');
            n                  := 6;
            mVCBREC.ref_number := Rtrim(substr(mval, n + 1, 15), '{//}');
          end if;
          mVCBREC.note := SUBSTR(vLine, 1, 4000);
        END IF;
      end loop;
      ii := 0;
      ii := instr(mVCBREC.ref_number, '{');
      if ii > 0 then
        mVCBREC.ref_number := substr(mVCBREC.ref_number, 1, ii);
      
      end if;
      mVCBREC.ref_number     := replace(mVCBREC.ref_number, '{');
      mVCBREC.GW_TYPE        := 'VCB';
      mVCBREC.ACC_TYPE       := REPLACE(substr(vLine, 12, 16), '.');
      mVCBREC.RUNNING_SEG_NO := 1;
      mVCBREC.MSG_TYPE       := 'MT103';
      mVCBREC.SEG_NO         := '000';
      mVCBREC.MSG_NO         := '000';
      mVCBREC.CCY            := trim(vCCYCD);
      mVCBREC.APP_CODE       := 'RM';
      mVCBREC.JOURNAL_SEG_NO := 0;
      mVCBREC.REC_TYPE       := 'VCB-BR';
      mVCBREC.Trans_no       := mVCBREC.Ref_Number;
    
      INSERT INTO VCB_MSG_REC_TEMP VALUES mVCBREC;
      COMMIT;
      commit;
    end if;
  exception
    when others then
      Reconcile_trace('[VCB-BR][VCB]', '=> miss one message');
  End ReadFileStatement_VCB;
  -----------------------------------------------------------------------------------------------------
  --ten ham : RECONCILE_SIBS()
  -----------------------------------------------------------------------------------------------------
  Procedure RECONCILE_SIBS(pDate in date, pTellerID varchar2) Is
    iNDATE   number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
    pRecTime date := sysdate;
    -- QuanLD Doi chieu 1-1
    cursor Recone_SIBS_GW_Content is
      SELECT A.MSG_ID,
             A.BRANCH_A,
             A.BRANCH_B,
             A.TRANS_DATE,
             A.FIELD20,
             A.DEPARTMENT,
             A.RM_NUMBER,
             A.AMOUNT,
             A.MSG_TYPE,
             A.MSG_DIRECTION
        FROM VCB_MSG_CONTENT_TEMP A
       Where A.MSG_DIRECTION = 'SIBS-VCB'
         AND NDATE = iNDATE
         AND A.MSG_SRC in (1, 5)
            --20100610
         and status = 1; -- Source
  
    cursor Recone_GW_SIBS_Content is
      SELECT A.MSG_ID,
             A.BRANCH_A,
             A.BRANCH_B,
             A.TRANS_DATE,
             A.FIELD20,
             A.DEPARTMENT,
             A.RM_NUMBER,
             A.AMOUNT,
             A.MSG_TYPE,
             A.MSG_DIRECTION
        FROM VCB_MSG_CONTENT_TEMP A
       Where A.MSG_DIRECTION = 'VCB-SIBS'
         AND NDATE = iNDATE
         and status = 1;
  
    vBranch_A    varchar2(12);
    vBranch_B    varchar2(12);
    vref_no      varchar2(30);
    vRmNumber    varchar2(30);
    dTrans_date  date;
    vDepartment  varchar2(40);
    vMSG_TYPE    varchar2(12);
    nMSG_ID_GW   number(20);
    nMSG_ID_HOST number(20);
    amount       number(19, 2);
  
    v_MSG_SISB_GW Recone_SIBS_GW_Content%rowtype;
    v_MSG_GW_SISB Recone_GW_SIBS_Content%rowtype;
  
  Begin
    -- delete old result if have
    delete from VCB_REC_ONE;
    delete from VCB_MSG_REC
     where rec_type = 'SIBS-BR'
       and ndate = iNDATE;
    commit;
    -- Content
    -- SIBS->GW : GS,S
  
    -- Doi chieu 1-1
    OPEN Recone_SIBS_GW_Content;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH Recone_SIBS_GW_Content
        INTO v_MSG_SISB_GW;
      EXIT WHEN Recone_SIBS_GW_Content %notfound;
      vBranch_A   := '';
      vBranch_B   := '';
      vref_no     := '';
      vDepartment := '';
      vMSG_TYPE   := '';
      nMSG_ID_GW  := 0;
      vRmNumber   := '';
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      vBranch_A   := trim(v_MSG_SISB_GW.Branch_a);
      vBranch_B   := trim(v_MSG_SISB_GW.Branch_b);
      vref_no     := LTRIM(trim(v_MSG_SISB_GW.Field20), '0');
      dTrans_date := v_MSG_SISB_GW.Trans_Date;
      vDepartment := trim(v_MSG_SISB_GW.Department);
      vMSG_TYPE   := trim(v_MSG_SISB_GW.Msg_Type);
      nMSG_ID_GW  := v_MSG_SISB_GW.Msg_Id;
      vRmNumber   := LTRIM(trim(v_MSG_SISB_GW.Rm_Number), '0');
      amount      := v_MSG_SISB_GW.Amount;
      begin
        nMSG_ID_HOST := 0;
        Select MSG_ID
          into nMSG_ID_HOST
          from VCB_MSG_REC_TEMP B
         where ltrim(trim(vRmNumber), '0') = ltrim(trim(B.rm_number), '0') -- F
           AND Upper(B.APP_CODE) = Upper(vDepartment) -- A=B
           AND (Upper(vDepartment) = 'RM' or Upper(vDepartment) = 'TF') -- RM=TF
           AND Upper(B.MSG_DIRECTION) = 'O' -- Out
           AND B.REC_TYPE = 'SIBS-BR'
           AND B.Amount = amount
           AND B.NDATE = iNDATE
           And MSG_ID not in
               (select MSG_ID_HOST
                  from VCB_REC_one
                 where MSG_DIRECTION = 'SIBS-BR')
           And Rownum = 1;
      
        if (nMSG_ID_HOST > 0) then
          insert into VCB_REC_ONE
            (MSG_ID_GW, MSG_DIRECTION, MSG_ID_HOST)
          Values
            (nMSG_ID_GW, 'SIBS-BR', nMSG_ID_HOST);
          commit;
        end if;
      
      Exception
        when others then
          nMSG_ID_HOST := 0;
      end;
    
    End loop;
    CLOSE Recone_SIBS_GW_Content;
  
    OPEN Recone_GW_SIBS_Content;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH Recone_GW_SIBS_Content
        INTO v_MSG_GW_SISB;
      EXIT WHEN Recone_GW_SIBS_Content %notfound;
    
      vBranch_A   := '';
      vBranch_B   := '';
      vref_no     := '';
      vDepartment := '';
      vMSG_TYPE   := '';
      nMSG_ID_GW  := 0;
      vRmNumber   := '';
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      vBranch_A   := trim(v_MSG_GW_SISB.Branch_a);
      vBranch_B   := trim(v_MSG_GW_SISB.Branch_b);
      vref_no     := LTRIM(trim(v_MSG_GW_SISB.Field20), '0');
      dTrans_date := v_MSG_GW_SISB.Trans_Date;
      vDepartment := trim(v_MSG_GW_SISB.Department);
      vMSG_TYPE   := trim(v_MSG_GW_SISB.Msg_Type);
      nMSG_ID_GW  := v_MSG_GW_SISB.Msg_Id;
      vRmNumber   := LTRIM(trim(v_MSG_GW_SISB.Rm_Number), '0');
      amount      := v_MSG_GW_SISB.Amount;
    
      begin
        nMSG_ID_HOST := 0;
        Select MSG_ID
          into nMSG_ID_HOST
          from VCB_MSG_REC_TEMP B
         WHERE ltrim(trim(vRmNumber), '0') = ltrim(trim(B.rm_number), '0') -- F
           AND Upper(B.APP_CODE) = Upper(vDepartment) -- A=B
           AND (Upper(vDepartment) = 'RM' or Upper(vDepartment) = 'TF') -- RM=TF
           And B.Amount = amount
           AND Upper(B.MSG_DIRECTION) = 'I' -- Out
           AND B.REC_TYPE = 'SIBS-BR'
           AND B.NDATE = iNDATE
           And B.MSG_ID not in
               (select MSG_ID_HOST
                  from VCB_REC_one
                 where MSG_DIRECTION = 'BR-SIBS')
           and rownum = 1;
      
        if (nMSG_ID_HOST > 0) then
          insert into VCB_REC_ONE
            (MSG_ID_GW, MSG_DIRECTION, MSG_ID_HOST)
          Values
            (nMSG_ID_GW, 'BR-SIBS', nMSG_ID_HOST);
          commit;
        end if;
      
      Exception
        when others then
          nMSG_ID_HOST := 0;
      end;
    
    End loop;
    CLOSE Recone_GW_SIBS_Content;
  
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       QUERY_ID,
       TRANS_NO)
      (SELECT 'VCB',
              RM_NUMBER,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              FIELD20 ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCYCD,
              NVL(AMOUNT, 0),
              DEPARTMENT,
              BRANCH_A,
              BRANCH_A,
              BRANCH_B,
              BRANCH_B,
              TRANS_DATE,
              '0',
              'O' MSG_DIRECTION,
              'SIBS',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'GS',
              'SIBS-BR',
              pRecTime,
              pTellerid,
              QUERY_ID,
              ORG_REF
         FROM VCB_MSG_CONTENT_TEMP -- CONTENT
        WHERE MSG_ID not in (SELECT A.MSG_ID_GW
                               FROM VCB_REC_ONE A
                              where trim(A.MSG_DIRECTION) = 'SIBS-BR'
                             union
                             select -1 from dual)
          AND status > -2
          AND MSG_DIRECTION = 'SIBS-VCB' -- Out
          AND (Upper(DEPARTMENT) = 'RM' OR Upper(DEPARTMENT) = 'TF') -- RM
          AND MSG_SRC in (1, 5)
          AND VCB_MSG_CONTENT_TEMP.NDATE = iNDATE);
  
    -- SIBS-> GW : S
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       TRANS_NO)
      (SELECT 'VCB',
              rm_number,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCY,
              NVL(AMOUNT, 0),
              APP_CODE,
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              '0',
              'O' MSG_DIRECTION,
              'SIBS',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'S',
              'SIBS-BR',
              pRecTime,
              pTellerid,
              TRANS_NO
         FROM VCB_MSG_REC_TEMP -- REC_TEMP
        WHERE MSG_ID not in (SELECT A.MSG_ID_HOST
                               FROM VCB_REC_ONE A
                              where trim(A.MSG_DIRECTION) = 'SIBS-BR'
                             union
                             select -1 from dual)
             --AND status>-2
          AND MSG_DIRECTION = 'O' -- Out
          AND REC_TYPE = 'SIBS-BR'
          AND NDATE = iNDATE);
  
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       QUERY_ID,
       TRANS_NO)
      (SELECT 'VCB',
              RM_NUMBER,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              FIELD20 ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCYCD,
              NVL(AMOUNT, 0),
              DEPARTMENT,
              BRANCH_A,
              BRANCH_A,
              BRANCH_B,
              BRANCH_B,
              TRANS_DATE,
              '0',
              'I',
              'VCB',
              'SIBS',
              iNDATE,
              TRANS_DATE,
              'GS',
              'SIBS-BR',
              pRecTime,
              pTellerid,
              QUERY_ID,
              ORG_REF
         FROM VCB_MSG_CONTENT_TEMP -- CONTENT
        WHERE MSG_ID not in (SELECT A.MSG_ID_GW
                               FROM VCB_REC_ONE A
                              where trim(A.MSG_DIRECTION) = 'BR-SIBS'
                             union
                             select -1 from dual)
          AND status > 0
          AND MSG_DIRECTION = 'VCB-SIBS' -- In
          AND Upper(DEPARTMENT) = 'RM' -- RM
          AND ndate = iNDATE);
  
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       TRANS_NO)
      (SELECT 'VCB',
              rm_number,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCY,
              NVL(AMOUNT, 0),
              APP_CODE,
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              '0',
              'I' MSG_DIRECTION,
              'VCB',
              'SIBS',
              iNDATE,
              TRANS_DATE,
              'S',
              'SIBS-BR',
              pRecTime,
              pTellerid,
              TRANS_NO
         FROM VCB_MSG_REC_TEMP -- REC_TEMP
        WHERE MSG_ID not in (SELECT A.MSG_ID_HOST
                               FROM VCB_REC_ONE A
                              where trim(A.MSG_DIRECTION) = 'BR-SIBS'
                             union
                             select -1 from dual)
             --AND status>-2
          AND MSG_DIRECTION = 'I' -- In
          AND NVL(APP_CODE, 'XX') = 'RM' -- RM
          AND REC_TYPE = 'SIBS-BR'
          AND NDATE = iNDATE);
    --- Het Doi chieu 1-1
  
    commit;
  
  End RECONCILE_SIBS;
  --------------------------------------------------------------
  -- Doi chieu dien voi he thong VCB
  --------------------------------------------------------------
  Procedure RECONCILE_VCB(pDate in date, pTellerID varchar2) Is
    pRecTime date := sysdate;
    iNDATE   number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    -- delete old result if have
    delete from VCB_MSG_REC
     where rec_type = 'VCB-BR'
       and NDATE = iNDATE;
    -- BR->VCB
    -- Content
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       TRANS_NO,
       Query_id)
      (SELECT 'VCB',
              RM_NUMBER,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              FIELD20 ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCYCD,
              NVL(AMOUNT, 0),
              DEPARTMENT,
              BRANCH_A,
              BRANCH_A,
              BRANCH_B,
              BRANCH_B,
              TRANS_DATE,
              '0',
              'O',
              'SIBS',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'GV',
              'VCB-BR',
              pRecTime,
              pTellerid,
              ORG_REF,
              Query_id
         FROM VCB_MSG_CONTENT_TEMP -- CONTENT
        WHERE MSG_ID not in
              (SELECT A.MSG_ID
                 FROM VCB_MSG_CONTENT_TEMP A, VCB_MSG_REC_TEMP B
                WHERE ltrim(trim(A.FIELD20), '0') =
                      ltrim(trim(B.ref_number), '0') -- F
                  AND Upper(A.MSG_DIRECTION) = 'SIBS-VCB'
                  AND Upper(B.MSG_DIRECTION) = 'O' -- Out
                  AND B.REC_TYPE = 'VCB-BR'
                  AND A.NDATE = iNDATE
                  AND B.NDATE = iNDATE
                  And B.Amount = A.amount
                     --20100610
                  And A.status = 1
               UNION
               SELECT -1 FROM DUAL)
          AND status > 0
          AND MSG_DIRECTION = 'SIBS-VCB' -- Out
             --AND Upper(DEPARTMENT)='RM' -- RM
          AND ndate = iNDATE);
   
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       TRANS_NO)
      (SELECT 'VCB',
              rm_number,
              'R',
              '0',
              ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCY,
              NVL(AMOUNT, 0),
              APP_CODE,
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              '0',
              'O',
              'SIBS',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'V',
              'VCB-BR',
              pRecTime,
              pTellerid,
              ACC_TYPE
       --TRANS_NO
         FROM VCB_MSG_REC_TEMP
        WHERE MSG_ID not in
              (SELECT B.MSG_ID
                 FROM VCB_MSG_CONTENT_TEMP A, VCB_MSG_REC_TEMP B
                WHERE ltrim(trim(A.FIELD20), '0') =
                      ltrim(trim(B.ref_number), '0') -- F
                  AND A.status > 0
                  AND Upper(A.MSG_DIRECTION) = 'SIBS-VCB'
                  AND Upper(B.MSG_DIRECTION) = 'O' -- Out
                  AND B.REC_TYPE = 'VCB-BR'
                  AND A.NDATE = iNDATE
                  AND B.NDATE = iNDATE
                  And B.Amount = A.amount
               UNION
               SELECT -1 FROM DUAL)
          
          AND MSG_DIRECTION = 'O' -- Out
   
          AND rec_type = 'VCB-BR'
          AND NDATE = iNDATE);
    -- GW<-VCB
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       TRANS_NO,
       Query_id)
      (SELECT 'VCB',
              RM_NUMBER,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              FIELD20 ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCYCD,
              NVL(AMOUNT, 0),
              DEPARTMENT,
              BRANCH_A,
              BRANCH_A,
              BRANCH_B,
              BRANCH_B,
              TRANS_DATE,
              '0',
              'I',
              'SIBS',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'GV',
              'VCB-BR',
              pRecTime,
              pTellerid,
              ORG_REF,
              Query_id
         FROM VCB_MSG_CONTENT_TEMP -- CONTENT
        WHERE MSG_ID not in
              (SELECT A.MSG_ID
                 FROM VCB_MSG_CONTENT_TEMP A, VCB_MSG_REC_TEMP B
                WHERE (ltrim(trim(A.ORG_REF), '0') =
                      ltrim(trim(SUBSTR(B.ref_number, 1, 4) ||
                                 SUBSTR(B.ref_number, 7)),
                            '0') OR ltrim(trim(A.ORG_REF), '0') =
                      ltrim(trim(SUBSTR(B.ref_number, 1, 4) ||
                                 SUBSTR(B.ref_number, 6)),
                            '0')) -- F
                  AND Upper(A.MSG_DIRECTION) = 'VCB-SIBS'
                  AND Upper(B.MSG_DIRECTION) = 'I' -- Out
                  AND B.REC_TYPE = 'VCB-BR'
                  AND A.NDATE = iNDATE
                  AND B.NDATE = iNDATE
                  And B.Amount = A.amount
               UNION
               SELECT -1 FROM DUAL)
          AND status > -1
          AND MSG_DIRECTION = 'VCB-SIBS' -- In
             --AND Upper(DEPARTMENT)='RM' -- RM
          AND ndate = iNDATE);
   
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       TRANS_NO)
      (SELECT 'VCB',
              rm_number,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              '',
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCY,
              NVL(AMOUNT, 0),
              APP_CODE,
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              '0',
              'I',
              'SIBS',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'V',
              'VCB-BR',
              pRecTime,
              pTellerid,
              Ref_number
       /*TRANS_NO*/
         FROM VCB_MSG_REC_TEMP -- REC_TEMP
        WHERE MSG_ID not in
              (SELECT B.MSG_ID
                 FROM VCB_MSG_CONTENT_TEMP A, VCB_MSG_REC_TEMP B
                WHERE (ltrim(trim(A.ORG_REF), '0') =
                      ltrim(trim(SUBSTR(B.ref_number, 1, 4) ||
                                 SUBSTR(B.ref_number, 7)),
                            '0') OR ltrim(trim(A.ORG_REF), '0') =
                      ltrim(trim(SUBSTR(B.ref_number, 1, 4) ||
                                 SUBSTR(B.ref_number, 6)),
                            '0'))
                  AND Upper(A.MSG_DIRECTION) = 'VCB-SIBS'
                  AND Upper(B.MSG_DIRECTION) = 'I' -- Out
                  AND B.REC_TYPE = 'VCB-BR'
                  AND A.NDATE = iNDATE
                  AND B.NDATE = iNDATE
                  And B.Amount = A.amount
               UNION
               SELECT -1 FROM DUAL)
             --AND status>-2
          AND MSG_DIRECTION = 'I' -- In
             --AND Upper(APP_CODE)='RM' -- RM
          AND rec_type = 'VCB-BR'
          AND NDATE = iNDATE);
    commit; -- commit all
  End RECONCILE_VCB;
  ------------------------------------------------------------------------------------------
  -- Doi chieu voi he thong TR
  --------------------------------------------------------------------------------------------
  Procedure RECONCILE_TR(pDate in date, pTellerID varchar2) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    delete from VCB_MSG_REC
     where rec_type = 'TR-BR'
       and NDATE = iNDATE;
    --
    -- Content
  
    -- TR->GW : GR ,S
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID)
      (SELECT 'VCB',
              RM_NUMBER,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              FIELD20 ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCYCD,
              NVL(AMOUNT, 0),
              DEPARTMENT,
              BRANCH_A,
              BRANCH_A,
              BRANCH_B,
              BRANCH_B,
              TRANS_DATE,
              '0',
              'O' MSG_DIRECTION,
              'TR',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'GR',
              'TR-BR',
              sysdate,
              pTellerid
         FROM VCB_MSG_CONTENT_TEMP -- CONTENT
        WHERE MSG_ID not in
              (SELECT A.MSG_ID
                 FROM VCB_MSG_CONTENT_TEMP A, VCB_MSG_REC_TEMP B
                WHERE ltrim(trim(A.field20), '0') =
                      ltrim(trim(B.ref_number), '0') -- F
                  AND Upper(B.APP_CODE) = Upper(A.DEPARTMENT) -- A=B
                  AND Upper(A.DEPARTMENT) = 'TR' -- TR
                  AND Upper(A.MSG_DIRECTION) = 'SIBS-VCB'
                  AND Upper(B.MSG_DIRECTION) = 'O' -- Out
                  AND B.REC_TYPE = 'TR-BR'
                  AND A.NDATE = iNDATE
                  AND B.NDATE = iNDATE
               UNION
               SELECT -1 FROM DUAL)
             --AND status > -1
          AND MSG_DIRECTION = 'SIBS-VCB' -- Out
          AND (Upper(DEPARTMENT) = 'TR') -- RM
          AND ndate = iNDATE);
   
    insert into VCB_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       RUNNING_SEG_NO,
       ref_number,
       MSG_TYPE,
       SEG_NO,
       MSG_NO,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       JOURNAL_SEG_NO,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID)
      (SELECT 'VCB',
              rm_number,
              'R' ACC_TYPE,
              '0' RUNNING_SEG_NO,
              ref_number,
              MSG_TYPE,
              '000' SEG_NO,
              '000' MSG_NO,
              CCY,
              NVL(AMOUNT, 0),
              APP_CODE,
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              '0',
              'O' MSG_DIRECTION,
              'TR',
              'VCB',
              iNDATE,
              TRANS_DATE,
              'R',
              'TR-BR',
              SYSDATE,
              pTellerid
         FROM VCB_MSG_REC_TEMP -- REC_TEMP
        WHERE MSG_ID not in
              (SELECT B.MSG_ID
                 FROM VCB_MSG_CONTENT_TEMP A, VCB_MSG_REC_TEMP B
                WHERE ltrim(trim(A.FIELD20), '0') =
                      ltrim(trim(B.ref_number), '0') -- F
                  AND Upper(B.APP_CODE) = Upper(A.DEPARTMENT) -- A=B
                  AND (Upper(A.DEPARTMENT) = 'TR') -- RM=TF
                  AND Upper(A.MSG_DIRECTION) = 'SIBS-VCB'
                  AND Upper(B.MSG_DIRECTION) = 'O' -- Out
                  AND B.REC_TYPE = 'TR-BR'
                  AND A.NDATE = iNDATE
                  AND B.NDATE = iNDATE
               UNION
               SELECT -1 FROM DUAL)
             --AND status>-2
          AND MSG_DIRECTION = 'O' -- Out
          AND (NVL(APP_CODE, 'XX') = 'TR') -- RM+TF
          AND REC_TYPE = 'TR-BR'
          AND NDATE = iNDATE);
    commit;
  End RECONCILE_TR;

  -- ghi log
  Procedure Reconcile_trace(pREC_NAME varchar2, pDescription varchar2) Is
    i integer := 1;
  Begin
    insert into reconcile_log
      (rec_name, description, date_log)
    values
      (pREC_NAME, pDescription, sysdate);
    Commit;
  Exception
    When others then
      i := i;
  End Reconcile_trace;

end GW_PK_REC_VCB;
/
