CREATE OR REPLACE PACKAGE GW_PK_IBPS_Q_CONVERTOUT is

  -- Author  : QuanLD
  -- Created : 5/23/2008 9:54:40 AM
  -- Purpose : Dinh nghia cac ham xu ly dien trong IBPS

  -- Public function and procedure declarations
  Procedure IBPS_EQ_CONVERT_OUT(pQUERY_ID       NUMBER,
                                pHEADER_CONTENT VARCHAR2,
                                pCONTENT        Varchar2,
                                pRM_NUMBER      VARCHAR2,
                                pMSG_TYPE       VARCHAR2,
                                pTRANS_DATE     DATE,
                                pMSG_DEF        VARCHAR2,
                                pDEPARTMENT     VARCHAR2);

  Procedure IBPS_GET_MSG_OUT;

  PROCEDURE IBPS_DE_CONVERTOUT;

  procedure IBPS_Msg_Update(pSIBS_MSG_ID NUMBER, pSTATUS NUMBER);

  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: Ham convert dien tu chuan dien SIBS sang IBPS
  Ten ham:  IBPS_CONVERTOUT()
  Tham so:  pvBranchCode Varchar2 Ma chi nhanh 8 so
  Mo ta: -
  Ngay khoi tao:  6/6/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  **********************************************************************/
  FUNCTION IBPS_CONVERTOUT(pQUERY_ID     NUMBER,
                           pTRANS_DATE   DATE,
                           pMESSAGE_TEXT VARCHAR2,
                           pHeadContent  VARCHAR2) RETURN VARCHAR2;

CREATE OR REPLACE PACKAGE BODY GW_PK_IBPS_Q_CONVERTOUT is

  m_IBPS_Type               IBPS_MSG_LOG%rowtype;
  m_nGW_TRANS_AMOUNT_LEN    NUMBER(2) := 19;
  m_nGW_TRANS_AMOUNT_POS    NUMBER(4) := 1298;
  m_nGW_SIBS_REL_LEN_RM     NUMBER(1) := 9;
  m_nGW_SIBS_REL_POS_RM     NUMBER(3) := 839;
  v_F179                    varchar2(100);
  m_nGW_RM_BANK_CODE_LEN    NUMBER(1) := 5;
  m_nGW_RM_BANK_CODE_POS    NUMBER(3) := 922;
  m_nGW_RM_TELLER_ID_POS    NUMBER(3) := 829;
  vTellerID                 varchar2(10) := '';
  VHeadOF                   varchar2(10) := '00011';
  m_vGW_MSGID_CODE          varchar2(3) := '003';
  m_vGW_RELNUM_CODE         varchar2(3) := '110';
  m_vGW_SENDING_BANK_CODE   varchar2(3) := '007';
  m_vGW_RECEIVING_BANK_CODE varchar2(3) := '019';
  m_vGW_SEND_CORRES_CODE    varchar2(3) := '021';
  m_vGW_RECEIVE_CORRES_CODE varchar2(3) := '022';
  m_vGW_CREATION_DATE_CODE  varchar2(3) := '012';
  m_vGW_TRANS_AMOUNT_CODE   varchar2(3) := '027';
  m_vGW_CURRENCY_CODE       varchar2(3) := '026';
  m_vGW_SENDING_ADD_CODE    varchar2(3) := '029';
  m_vGW_ACC_SENDING_CODE    varchar2(3) := '030';
  m_vGW_BENNAME             varchar2(3) := '031';
  m_vGW_RECEIVING_ADD_CODE  varchar2(3) := '032';

  m_vGW_DESCRIPTION_CODE varchar2(3) := '034';
  m_vSendingbank         Varchar2(12);
  m_pnHeadfnPos          number(5);
  iLV_HV                 boolean := False;
  vField003New           varchar2(100);
  vHEADCONDITION         varchar2(2000);
  m_vGW_ABCS_OUT_NAME    Varchar2(10) := 'MB58903R';
  m_Department           varchar2(3) := 'RM';
  m_Direction            varchar2(10) := 'SIBS-IBPS';
  m_GWtype               varchar2(5) := 'IBPS';
  m_receiveTruot         varchar2(12);
  --Dinh nghia cac gia tri theo bang IBPS_MSG_Content de insert vao bang

  --m_vFILE_NAME         VARCHAR2(50);
  --m_vMSG_DIRECTION     VARCHAR2(1);
  m_vTRANS_CODE        VARCHAR2(10);
  m_nGW_TRANS_NUM      NUMBER(16);
  m_nSIBS_TRANS_NUM    NUMBER(16);
  m_vSENDER            VARCHAR2(12);
  m_vRECEIVER          VARCHAR2(12);
  m_dTRANS_DATE        DATE;
  m_nAMOUNT            NUMBER(19, 2);
  m_nErr               NUMBER(3);
  m_vCCYCD             VARCHAR2(3);
  m_vTRANS_DESCRIPTION VARCHAR2(255);
  m_vDEPARTMENT        VARCHAR2(10);
  m_vCONTENT           VARCHAR2(4000);
  m_vSOURCE_BRANCH     VARCHAR2(5);
  m_vTAD               VARCHAR2(50);
  m_vRM_NUMBER         VARCHAR2(20);
  m_vProxySender       VARCHAR2(12);
  m_vProxyReceiver     VARCHAR2(12);
  pvMbdetail           VARCHAR2(12);
  --m_IBPSTypeOut        ibps_type_convertout;
  GW_ERR_TRANS_CODE        NUMBER(2) := 2;
  GW_ERR_REF_NUM           NUMBER(2) := 3;
  GW_ERR_MSGAMOUNT_FALSE   NUMBER(2) := 4;
  GW_ERR_SENDINGBANK_FALSE NUMBER(2) := 5;
  vBenname2                varchar2(100) := '';
  GW_ERR_CURRENCY_FALSE    NUMBER(2) := 5;

  -- bien xac dinh kenh thanh toan co DBlink hay ????
  m_isDBLINK Integer := 0;
  gLValue    varchar(10) := '101001';
  gHValue    varchar(10) := '201001';
  isCMT      Boolean :=false;
  ---------------------------------------------------------------------------------------------------------
  -- Nguoi tao          :QuanLD
  -- Muc dich           :day du lieu vao queue IBPS_Q_ConvertOut
  -- Ten ham            :IBPS_EQ_CONVERT_OUT()
  -- Tham so            :pQUERY_ID     NUMBER,
  --                    :pGW_TYPE      VARCHAR2,
  --                    :pMSG_DEF      VARCHAR2,
  --                    :pTRANS_DATE   DATE,
  --                    :pMESSAGE_TEXT VARCHAR2,
  --                    :pHeadContent  VARCHAR2,
  --                    :pSTATUS       NUMBER
  --
  -- Gis tri tra ve: Day du lieu vao 1 bang table queue

  -- Ngay tao:23/05/2008
  ---------------------------------------------------------------------------------------------------------

  Procedure IBPS_EQ_CONVERT_OUT(pQUERY_ID       NUMBER,
                                pHEADER_CONTENT VARCHAR2,
                                pCONTENT        Varchar2,
                                pRM_NUMBER      VARCHAR2,
                                pMSG_TYPE       VARCHAR2,
                                pTRANS_DATE     DATE,
                                pMSG_DEF        VARCHAR2,
                                pDEPARTMENT     VARCHAR2) IS

    queue_options      DBMS_AQ.ENQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(16);
    my_message         IBPS_Type_Convertout;
  BEGIN

    --start cai queue len
    DBMS_AQADM.start_queue('IBPS_Q_CONVERTOUT', TRUE, TRUE);
    /*m_IBPS_Type.Log_Date      := sysdate;
    m_IBPS_Type.Query_Id      := pQUERY_ID;
    m_IBPS_Type.Status        := 1;
    m_IBPS_Type.Descriptions  := 'Start Enqueue Message:';
    m_IBPS_Type.Area          := ' ';
    m_IBPS_Type.Job_Name      := 'IBPS_Job_INQUIRYOUT';
    m_IBPS_Type.Msg_Direction := m_Direction;
    GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    */
    my_message := IBPS_Type_Convertout(pQUERY_ID,
                                       pHEADER_CONTENT,
                                       pCONTENT,
                                       pRM_NUMBER,
                                       pMSG_TYPE,
                                       pTRANS_DATE,
                                       pMSG_DEF,
                                       pDEPARTMENT);

    DBMS_AQ.ENQUEUE(queue_name         => 'IBPS_Q_CONVERTOUT',
                    enqueue_options    => queue_options,
                    message_properties => message_properties,
                    payload            => my_message,
                    msgid              => message_id);
    COMMIT;

  Exception
    when OTHERS THEN

      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error When Enqueue content Message File:' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_JOB_INQUIRYOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);

    -- Rollback;

  END IBPS_EQ_CONVERT_OUT;

  -- Nguoi tao          :QuanLD
  -- Ten ham            :FILLDATA_EQ_OUT_IBPS_RM()
  -- Tham so
  --
  -- Gis tri tra ve: Day du lieu vao 1 bang table queue
  -- Ngay tao:23/05/2008

  Procedure IBPS_GET_MSG_OUT IS
    cursor curMSG_OUT is

      SELECT ISQ.QUERY_ID,
             ISQ.RM_NUMBER,
             SMO.QUERY_ID MSG_ID,
             SMO.TRANS_DATE,
             SMO.HEAD_CONTENT,
             SMO.CONTENT,
             SMO.MSG_DEF,
             SMO.STATUS
        FROM IBPS_SIBS_MSG_OUT SMO, IBPS_SIBS_QUERY ISQ
       Where ISQ.QUERY_ID = SMO.QUERY_ID
         And SMO.STATUS = '0'
         And ISQ.STATUS = 1
         And to_number(to_char(SMO.TRANS_DATE, 'YYYYMMDDHH24MI')) <
             to_number(to_char(sysdate, 'YYYYMMDDHH24MI')) - 5
         And Rownum <= 1000;
  --28122012
  m_IBPSTypeOutIN ibps_type_convertout;

    v_MSG_OUT   curMSG_OUT%rowtype;
    v_non_sales integer;
    --So RM luu trong bang IBPS_QUERY

    --pi_Dup INTEGER;

    pQUERY_ID       Number(20);
    pHEADER_CONTENT Varchar2(1200);
    pCONTENT        Varchar2(4000);
    pRM_NUMBER      Varchar2(20);
    pMSG_TYPE       Varchar2(10);
    pTRANS_DATE     Date;
    pMSG_DEF        Varchar2(10);
    pDEPARTMENT     Varchar2(10);
    iCount          number(20) := 0;
    --bien Lay vi tri ket thuc phan Header

    OL12_CutOff_time number(10) :=0300;
    OL13_CutOff_time number(10) :=0300;
    IBPS_OL12_OL13_Start_time number(10) :=0700;
    isWorking_date number(1) := 0;
    isWeekendWorking number(1):=0;
    
  BEGIN

    begin
      select to_number(value)
        into OL12_CutOff_time
        from sysvar
       where varname = 'IBPS_OL12_CUTOFF_TIME'
         and GWTYPE = 'IBPS'
         and rownum = 1;
    exception
      when others then
        OL12_CutOff_time := 0300;
    end;

    begin
      select to_number(value)
        into OL13_CutOff_time
        from sysvar
       where varname = 'IBPS_OL13_CUTOFF_TIME'
         and GWTYPE = 'IBPS'
         and rownum = 1;
    exception
      when others then
        OL13_CutOff_time := 0400;
    end;

    begin
      select to_number(value)
        into IBPS_OL12_OL13_Start_time
        from sysvar
       where varname = 'IBPS_OL12_OL13_START_TIME'
         and GWTYPE = 'IBPS'
         and rownum = 1;
    exception
      when others then
        IBPS_OL12_OL13_Start_time := 0700;
    end;

    select count(1) into iCount from IBPS_SIBS_MSG_OUT where status = 0;
    if iCount > 0 then
      -- 03-11-2011 Update Goi ham check lai cac dien huy truoc khi thuc hien convert dien
      GW_UPDATE_MSG_REVERT.Get_ibps_cancel;
      -- Het update
    end if;
    
    select count(1)
      into isWorking_date
      from GW_WORKING_DAY GWD
     where to_char(GWD.OFFDAY, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD');
  
    if (isWorking_date > 0) then
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
    
    OPEN curMSG_OUT;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly
      FETCH curMSG_OUT
        INTO v_MSG_OUT;

      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u
      EXIT WHEN curMSG_OUT %notfound;

      -- Kiem tra neu truoc gio start time thi ko convert dien.
   
     IF (to_number(to_char(sysdate, 'HH24Mi')) < IBPS_OL12_OL13_Start_time) then
        goto continue_cursor;
      end if; 
      -- Kiem tra neu la dien OL12 sau 3h thi continue
      /*IF (substr(v_MSG_OUT.Content,2166,4)='OL12' and to_number(to_char(sysdate, 'HH24Mi')) > OL12_CutOff_time) and
        to_date(to_char(sysdate,'YYYYMMDD'),'YYYYMMDD')- to_date(to_char(v_MSG_OUT.Trans_Date,'YYYYMMDD'),'YYYYMMDD')>=1 then
        goto continue_cursor;
      end if  ;*/
      
      
      IF ((substr(v_MSG_OUT.Content,2166,4)='OL12') or (substr(v_MSG_OUT.Content,2166,4)='OL13')) and
        (to_date(to_char(sysdate,'YYYYMMDD'),'YYYYMMDD')- to_date(to_char(v_MSG_OUT.Trans_Date,'YYYYMMDD'),'YYYYMMDD')<1) then
        goto continue_cursor;
      end if  ; 
      
      
      -- Kiem tra neu la dien OL13 sau 4h thi continue
      /*IF (substr(v_MSG_OUT.Content,2166,4)='OL13' and to_number(to_char(sysdate, 'HH24Mi')) > OL13_CutOff_time) and
        to_date(to_char(sysdate,'YYYYMMDD'),'YYYYMMDD')- to_date(to_char(v_MSG_OUT.Trans_Date,'YYYYMMDD'),'YYYYMMDD')>=1 then
        goto continue_cursor;
      end if  ;    */

      IF (GW_PK_IBPS_PROCESS.IBPS_MSG_DUP_CHECK(2, v_MSG_OUT.RM_NUMBER) > 0) then
        -- neu dien khong trung: day dien vao hang doi, cap nhta lai bang MSG_OUT status=1
        --Day dien vao hang doi
        pQUERY_ID       := v_MSG_OUT.Query_Id;
        pHEADER_CONTENT := v_MSG_OUT.Head_Content;
        pCONTENT        := v_MSG_OUT.Content;
        pRM_NUMBER      := v_MSG_OUT.Rm_Number;
        pMSG_TYPE       := m_Direction;
        pTRANS_DATE     := v_MSG_OUT.Trans_Date;
        pMSG_DEF        := v_MSG_OUT.Msg_Def;
        pDEPARTMENT     := m_Department;
        m_IBPSTypeOutIN := ibps_type_convertout(pQUERY_ID,
                                                To_char(pHEADER_CONTENT),
                                                pCONTENT,
                                                pRM_NUMBER,
                                                pMSG_TYPE,
                                                pTRANS_DATE,
                                                pMSG_DEF,
                                                pDEPARTMENT);

        -- day du lieu vao Queue
        IBPS_EQ_CONVERT_OUT(pQUERY_ID,
                            To_char(pHEADER_CONTENT),
                            pCONTENT,
                            pRM_NUMBER,
                            pMSG_TYPE,
                            pTRANS_DATE,
                            pMSG_DEF,
                            pDEPARTMENT);
        -- Cap nhat lai dien vao bang IBPS_SIBS_MSG_OUT
        IBPS_Msg_Update(v_MSG_OUT.QUERY_ID, 1);

      else
        --Neu dien trung
        -- Cap nhat lai dien vao bang IBPS_SIBS_MSG_OUT
        IBPS_Msg_Update(v_MSG_OUT.QUERY_ID, -2);
      end if;

      --Lable khong lam gi ca. Tuong duong voi viec continue trong for
      <<continue_cursor>>
      null;

      v_non_sales := v_non_sales + 1;

    END LOOP;
    -- ?ong cursor
    CLOSE curMSG_OUT;
  Exception
    when others then
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error When getmessage from MSG content Message' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);

  END IBPS_GET_MSG_OUT;

  -- Nguoi tao          :QuanLD
  -- Muc dich           :day du lieu vao queue IBPS_Q_ConvertIN
  -- Ten ham            :IBPS_EN_CONVERTIN()
  -- Tham so            :pFILE_NAME      varchar2,
  --                    :pMSG_TYPE       varchar2,
  --                    :pSIBS_TRANS_NUM varchar2,
  --                    :pIBPS_CONTENT   varchar2,
  --                    :pSYSTEMDATE     DATE
  --
  -- Gis tri tra ve: Day du lieu vao 1 bang table queue

  -- Ngay tao:23/05/2008
  PROCEDURE IBPS_DE_CONVERTOUT IS

    queue_options      DBMS_AQ.DEQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(5000);
    my_message         IBPS_Type_Convertout;

    new_messages Boolean := True;
    next_trans  EXCEPTION;
    no_messages EXCEPTION;
    pragma exception_init(next_trans, -25235);
    pragma exception_init(no_messages, -25228);
    i integer;
    -- Dinh nghia cac bien lay du lieu ra tu queue
    pHEADER_CONTENT VARCHAR2(1000);
    pCONTENT        VARCHAR2(4000);
    pQUERY_ID       NUMBER(20);

    pRowIBPS IBPS_MSG_CONTENT%Rowtype;

  BEGIN

    -- Reset value of isCMT
    isCMT:=false;
    -- Lay cac chuan convert dien phan head
    SELECT GD.HEADMBASE_OUT
      into vHEADCONDITION
      FROM GWTYPE_DEPT GD
     WHERE GD.GWTYPE = m_GWtype
       and GD.DEPARTMENT = m_Department
       And rownum = 1;

    --  Xac dinh kenh Thanh toan co la DBLINK khong??
    Begin
      select GWTYPE.CONECTION
        into m_isDBLINK
        from GWTYPE
       where GWTYPE.GWTYPE = m_GWtype
         And rownum = 1;
    Exception
      when others then
        m_isDBLINK := 3;
    end;

    --het Xac dinh kenh Thanh toan co la DBLINK khong

    DBMS_AQADM.start_queue('IBPS_Q_CONVERTOUT', TRUE, TRUE); -- start queue

    queue_options.wait := 1;
    i                  := 0;
    select count(1) into i from Ibps_Tb_q_Converout;
    if i = 0 then
      return;
    end if;
    i := 0;

    While ((new_messages) and i < 1000) LOOP
      Begin
        m_vTRANS_CODE     := '';
        m_nGW_TRANS_NUM   := '';
        m_nSIBS_TRANS_NUM := '';
        m_vSENDER         := '';
        m_vRECEIVER       := '';

        m_nAMOUNT            := 0;
        m_vCCYCD             := '';
        m_nErr               := 0;
        m_vTRANS_DESCRIPTION := '';
        m_vCONTENT           := '';
        m_vSOURCE_BRANCH     := '';

        DBMS_AQ.DEQUEUE(queue_name         => 'IBPS_Q_CONVERTOUT',
                        dequeue_options    => queue_options,
                        message_properties => message_properties,
                        payload            => my_message,
                        msgid              => message_id);
        queue_options.navigation := DBMS_AQ.NEXT;
        pQUERY_ID                := my_message.QUERY_ID;
        pHEADER_CONTENT          := my_message.HEADER_CONTENT;
        pCONTENT                 := my_message.CONTENT;
        m_vRM_NUMBER             := my_message.RM_NUMBER;

        --m_vMSG_TYPE                := my_message.MSG_TYPE;
        m_dTRANS_DATE := my_message.TRANS_DATE;
        ---m_vMSG_DEF                 := my_message.MSG_DEF;
        m_vDEPARTMENT := my_message.DEPARTMENT;

        BEGIN
          --- Lay vi tri bat dau phan detail
          m_pnHeadfnPos := GW_PK_LIB.GET_MSG_HEAD_LENGHT(vHEADCONDITION);
          -- Dua cac ham su ly dien, convert dien,
          --Lay gia tri Source bank
          Begin
            select GD.MBTRANS_CODE_MAIN_OUT
              INTO pvMbdetail
              from Gwtype_Dept GD
             Where GD.GWTYPE = m_GWtype
               And gd.department = m_Department
               And Rownum = 1;
          Exception
            When others Then
              pvMbdetail := m_vGW_ABCS_OUT_NAME;
          end;
          m_vSOURCE_BRANCH := substr(pCONTENT,
                                     m_nGW_RM_BANK_CODE_POS - m_pnHeadfnPos,
                                     m_nGW_RM_BANK_CODE_LEN);
          vTellerID        := substr(pCONTENT,
                                     m_nGW_RM_TELLER_ID_POS - m_pnHeadfnPos,
                                     10);

          --        kiem tra ngay he thong va ngay transdate

          -- Lay ra truoc truong sendingbank

          vField003New := trim(substr(my_message.HEADER_CONTENT ||
                                      my_message.CONTENT,
                                      2872,
                                      10));
          vBenname2    := trim(substr(my_message.HEADER_CONTENT ||
                                      my_message.CONTENT,
                                      3810,
                                      40));

          vField003New := GW_PK_IBPS_PROCESS.IBPS_GetTransCode_OUT(vField003New);
          if vField003New = gLValue Then
            iLV_HV := true;
          else
            iLV_HV := false;
          end if;
          if LPAD(m_vSOURCE_BRANCH, 5, '0') = VHeadOF then
            m_vSendingbank := GETBANKCODE_TELLER(m_vSOURCE_BRANCH,
                                                 SUBSTR(vTellerID, 1, 6));
            if m_vSendingbank is null then
              m_vSendingbank := GW_PK_IBPS_PROCESS.GETBANKCODE(m_vSOURCE_BRANCH);
            end if;
          else
            m_vSendingbank := GW_PK_IBPS_PROCESS.GETBANKCODE(m_vSOURCE_BRANCH);
          End if;
          m_receiveTruot := substr(pCONTENT, 3048 - m_pnHeadfnPos, 12);
          -- Neu sendingbank khong co citad thi gan ve hoi so chinh
          -- Lay ra so relation Number
          m_nGW_TRANS_NUM := GW_PK_IBPS_PROCESS.IBPS_GetCurRef;
          -- Lay ra so m_nSIBS_TRANS_NUM
          m_nSIBS_TRANS_NUM := Substr(pCONTENT,
                                      m_nGW_SIBS_REL_POS_RM - m_pnHeadfnPos,
                                      m_nGW_SIBS_REL_LEN_RM);
          -- lay noi dung dien
          m_vCONTENT := IBPS_CONVERTOUT(pQUERY_ID,
                                        m_dTRANS_DATE,
                                        pCONTENT,
                                        pHEADER_CONTENT);

          -- INsert du lieu vao bang detail

          -- Lay ngan hang gui
          m_vSENDER := m_vSendingbank;
          -- Lay ngan hang nhan
          m_vRECEIVER := m_vRECEIVER;
          -- m_dTRANS_DATE := Sysdate;
          m_vDEPARTMENT := m_Department;

          m_vTAD := GW_PK_IBPS_PROCESS.GETBANKCODE(m_vProxySender, 1);
          --Kiem tra loai tien co duuo cnhan vao khong
          If (GW_PK_IBPS_PROCESS.IBPS_CCYCD_CHECK(m_vCCYCD, m_GWtype) < 1) Then
            m_nErr := GW_ERR_CURRENCY_FALSE;
          End if;
          pRowIBPS.Sibs_Tellerid := vTellerID;

          pRowIBPS.Query_Id       := pQUERY_ID;
          pRowIBPS.File_Name      := ' ';
          pRowIBPS.Msg_Direction  := m_Direction;
          pRowIBPS.Trans_Code     := m_vTRANS_CODE;
          pRowIBPS.Gw_Trans_Num   := m_nGW_TRANS_NUM;
          pRowIBPS.Sibs_Trans_Num := m_nSIBS_TRANS_NUM;
          pRowIBPS.F07            := GET_IBPS_Field(m_vCONTENT, '007');
          pRowIBPS.F19            := GET_IBPS_Field(m_vCONTENT, '019');
          pRowIBPS.F21            := GET_IBPS_Field(m_vCONTENT, '021');
          pRowIBPS.F22            := GET_IBPS_Field(m_vCONTENT, '022');
          pRowIBPS.Trans_Date     := sysdate;
          pRowIBPS.Transdate      := to_char(pRowIBPS.Trans_Date,
                                             'YYYYMMDD');
          pRowIBPS.Amount         := m_nAMOUNT;
          pRowIBPS.Ccycd          := m_vCCYCD;
          if m_nErr > 0 then
            pRowIBPS.Status := '-1';
          else
            pRowIBPS.Status := '0';
          end if;
          pRowIBPS.Print_Sts         := 0;
          pRowIBPS.Err_Code          := NVL(m_nErr, 0);
          pRowIBPS.Trans_Description := GET_IBPS_Field(m_vCONTENT, '034');
          pRowIBPS.Department        := m_vDEPARTMENT;
          pRowIBPS.Content           := m_vCONTENT;
          pRowIBPS.Source_Branch     := m_vSOURCE_BRANCH;
          --if Gettad(pRowIBPS.F21) is null then
          if GETTAD_BY_NEW_RULE(pRowIBPS.F21,pRowIBPS.F19,pRowIBPS.F22,isCMT) is null then
            pRowIBPS.Tad := m_vTAD;
          else
            --pRowIBPS.Tad := Gettad(pRowIBPS.F21);
            pRowIBPS.Tad := GETTAD_BY_NEW_RULE(pRowIBPS.F21,pRowIBPS.F19,pRowIBPS.F22,isCMT);
          end if;

          pRowIBPS.Pre_Tad        := '';
          pRowIBPS.Rm_Number      := m_vRM_NUMBER;
          pRowIBPS.Receiving_Time := m_dTRANS_DATE;

          pRowIBPS.Product_Type:='';
          --Them Product Type de phan biet
          if (substr(pCONTENT,2166,4)='OL12') then
            pRowIBPS.Product_Type:='OL12';
          end if;
          if (substr(pCONTENT,2166,4)='OL13') then
            pRowIBPS.Product_Type:='OL13';
          end if;

          -- insert dien vao day
          if (NVL(m_nErr, 0) >= 0) then
            GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                                   'IBPS_Job_CONVERT_OUT',
                                                   m_Direction);
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := pQUERY_ID;
            m_IBPS_Type.Status        := 0;
            m_IBPS_Type.Descriptions  := 'Convert Message success:' ||
                                         SQLCODE || ' -ERROR- ' || SQLERRM;
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          else
            IF (m_nErr = 13) or (m_nErr = 14) or (m_nErr = 15) then
              pRowIBPS.Err_Code := 13;
              pRowIBPS.Status   := -1;
              GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                                     'IBPS_Job_CONVERT_OUT',
                                                     m_Direction);
              m_IBPS_Type.Log_Date := sysdate;
              m_IBPS_Type.Query_Id := pQUERY_ID;
              m_IBPS_Type.Status   := 1;
              case
                when m_nErr = 13 then
                  m_IBPS_Type.Descriptions := 'Field 007 error';
                when m_nErr = 14 then
                  m_IBPS_Type.Descriptions := 'Field 019 error';
                when m_nErr = 15 then
                  m_IBPS_Type.Descriptions := 'Field 022 error';

              end case;

              m_IBPS_Type.Area          := ' ';
              m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
              m_IBPS_Type.Msg_Direction := m_Direction;
              --GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
            end if;

            -- neeu khi dien loi cap nhat lai vao bang MSG_log dung trang thai loi cua truong
            DELETE From IBPS_INDEX
             Where IBPS_INDEX.RM_NUMBER = m_vRM_NUMBER
               AND IBPS_INDEX.DIRECTION = 'SIBS-IBPS';
            IBPS_Msg_Update(pQUERY_ID, -1);
          End if;

          commit;

        EXCEPTION
          WHEN next_trans THEN
            queue_options.navigation := DBMS_AQ.NEXT_TRANSACTION;
          WHEN no_messages THEN
            new_messages := FALSE;
          When others then
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := pQUERY_ID;
            m_IBPS_Type.Status        := 0;
            m_IBPS_Type.Descriptions  := 'Error When Dequeue content Message:' ||
                                         SQLCODE || ' -ERROR- ' || SQLERRM;
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);

        END;
        commit;
      EXCEPTION
        WHEN next_trans THEN
          queue_options.navigation := DBMS_AQ.NEXT_TRANSACTION;
        WHEN no_messages THEN
          new_messages := FALSE;
        When others then
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := pQUERY_ID;
          m_IBPS_Type.Status        := 0;
          m_IBPS_Type.Descriptions  := 'Error When Dequeue content Message File:' ||
                                       SQLCODE || ' -ERROR- ' || SQLERRM;
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      END;
      i := i + 1;

    end loop;

    commit;

  Exception
    when OTHERS THEN
      --Rollback;
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error When Dequeue content Message File:' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);

  END IBPS_DE_CONVERTOUT;

  /*
  -- Nguoi tao          :HaNTT10
    -- Muc dich           :Cap nhat trang thai cho dien
    -- Ten ham            :IBPS_Msg_Update()
    -- Tham so            pSIBS_MSG_ID NUMBER,
                          pSTATUS NUMBER
    -- Gis tri tra ve: mot dong du lieu duoc cap nhat trang thai

    -- Ngay tao:30/05/2008
    */
  procedure IBPS_Msg_Update(pSIBS_MSG_ID NUMBER, pSTATUS NUMBER) is

  Begin
    UPDATE IBPS_SIBS_MSG_OUT
       set STATUS = pSTATUS
     Where QUERY_ID = pSIBS_MSG_ID;
    COMMIT;
  Exception
    when OTHERS THEN
      Rollback;
  end;

  /*
  -- Nguoi tao          :HaNTT10
    -- Muc dich           :Cap nhat trang thai cho dien
    -- Ten ham            :IBPS_Msg_Update()
    -- Tham so            pSIBS_MSG_ID NUMBER,
                          pSTATUS NUMBER
    -- Gis tri tra ve: mot dong du lieu duoc cap nhat trang thai

    -- Ngay tao:30/05/2008
    */
  procedure IBPS_QUERY_Update(pSIBS_MSG_ID NUMBER, pSTATUS NUMBER) is

  Begin
    UPDATE IBPS_SIBS_QUERY
       set STATUS = pSTATUS
     Where QUERY_ID = pSIBS_MSG_ID;
    COMMIT;
  Exception
    when OTHERS THEN
      Rollback;
  end;
  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: Ham convert dien tu chuan dien SIBS sang IBPS
  Ten ham:  IBPS_CONVERTOUT()
  Tham so:  pQUERY_ID NUMBER,
                           pMSG_DEF VARCHAR2,
                           pTRANS_DATE DATE,
                           pMESSAGE_TEXT VARCHAR2,
                           pHeadContent
  Mo ta: -
  Ngay khoi tao:  6/6/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  **********************************************************************/

  FUNCTION IBPS_CONVERTOUT(pQUERY_ID     NUMBER,
                           pTRANS_DATE   DATE,
                           pMESSAGE_TEXT VARCHAR2,
                           pHeadContent  VARCHAR2) RETURN VARCHAR2 IS

    pvMSGConverted Varchar2(5000);

    pMBTranCode     varchar2(10);
    pnGWPos         number(5);
    pnGWLenght      Number(4);
    pvSIBSFileName  varchar2(12);
    pvIBPSFilecode  varchar2(5);
    pnDatatype      Number(1);
    pvFieldCoverted Varchar2(3000);
    --dinh nghia bien luu noi dung cua truong dien
    pFieldContent Varchar2(4000);

    szID        varchar2(300);
    pnLength    Number(4);
    lLengthID   Number(2);
    b019existed Boolean;
    b021existed Boolean;
    b022existed Boolean;
    szIssDT     varchar2(1000);
    szBenAdd    varchar2(300);
    szIssPL     varchar2(300);
    --ipos029      Integer;
    ipos         int;
    szTmp        varchar2(200);
    szContentTmp varchar2(200);
    p029Tmp      varchar2(200);
    p029Tmp1     varchar2(200) := ' ';
    I029Count    Integer := 0;
    nLength      number(3);
    vchk         varchar2(2);
    VSCMT        Varchar2(40) := '';
    VOther       Varchar2(550) := '';
    V034         Varchar2(500) := '';
    vtemp11      Varchar2(500) := '';
    --Them de check ma gian tiep cho ngan hang huong
    V019         Varchar2(20):='';
    v021Tmp      Varchar2(20):='';
    tmpCount     number(20) := 0;
    V019New      Varchar2(20):='';

    Cursor pcurMSGDEF(pvMBDT VARCHAR2) IS
      SELECT SVL.*,
             TAD.FieldID       TADID,
             TAD.Field_Code    TADFieldCode,
             TAD.Field_Name    TADFieldName,
             TAD.Default_Value TADDefaultValue,
             TAD.Length        TADLength,
             TAD.Data_Type     TADDataType
        FROM (SELECT *
                FROM Msg_Def MD
               WHERE MD.Msg_Def_ID = m_GWtype
               order by MD.GW_POS) TAD,
             (SELECT *
                from Msg_Def MD1
               WHERE MD1.Msg_Def_ID = pvMbdetail
               order by MD1.GW_POS) SVL
       WHERE TAD.Field_Code(+) = SVL.Field_Code
       ORDER BY TAD.GW_Pos ASC;

    v_pCurMSGDEF pcurMSGDEF%ROWType;
    -- lay gia tri thu te cu tung field
    vFileRealValues varchar2(500);
    vField007old    varchar2(100);
    vLength         number(4);

  BEGIN

    -- Reset value of isCMT
    isCMT:=false;

    --Lay cau truc dien ve

    open pcurMSGDEF(pMBTranCode);
    LOOP
      FETCH pCurMSGDEF
        INTO v_pCurMSGDEF;
      EXIT WHEN pCurMSGDEF%NOTFOUND;
      -------------------
      --    gan gia tri tu Cursor cho cac bien

      pnGWPos        := v_pCurMSGDEF.Gw_Pos;
      pnGWLenght     := v_pCurMSGDEF.Length;
      pvSIBSFileName := v_pCurMSGDEF.Field_Name;
      pvIBPSFilecode := v_pCurMSGDEF.Field_Code;
      pnDatatype     := v_pCurMSGDEF.Data_Type;
      vchk           := v_pCurMSGDEF.Chk;
      --lay ra noi dung field dien
      pvFieldCoverted := '';
      nLength         := v_pCurMSGDEF.Chklength;
      pFieldContent   := Substr(pMESSAGE_TEXT,
                                pnGWPos - m_pnHeadfnPos,
                                pnGWLenght);

      pnGWLenght    := v_pCurMSGDEF.Tadlength;
      pFieldContent := Trim(pFieldContent);

      if pFieldContent is not null then

        pFieldContent := Trim(GW_PK_IBPS_PROCESS.IBPS_REPLACE_Char(pFieldContent,
                                                                   m_GWtype,
                                                                   m_Department,
                                                                   ' ',
                                                                   pvIBPSFilecode,
                                                                   m_Direction,
                                                                   pQUERY_ID,
                                                                   'IBPS_JOB_CONVERTOUT'));
      End if;

      If pvSIBSFileName = 'XRMBENID' then
        VSCMT := pFieldContent;
        if (LENGTH(Trim(pFieldContent)) > 0) Then
          isCMT:=true;
        end if;
      end if;

      If pvSIBSFileName = 'OTHER' then
        VOther := pFieldContent;
        --Cap nhat sua truong ngay cap CMT
        VOther:=GW_PK_IBPS_PROCESS.IBPS_FormatCMTDate(pFieldContent);
        -- Het cap nhat
      end if;

      if pvIBPSFilecode = '028' or pvIBPSFilecode = '030' or
         pvIBPSFilecode = '031' or pvIBPSFilecode = '033' or
         pvIBPSFilecode = '034' then
        if trim(pFieldContent) is null then
          pFieldContent := '.';
        end if;

      end if;

      -- Kiem tra neu field la truong 19 thi luu tam gia tri
      -- De phuc vu cho viec convert  truong 22
      if pvIBPSFilecode = '019' then
        V019:=pFieldContent;
        pFieldContent:=RECEIPVER_BANK_MAP(pFieldContent,19);
        V019New:= pFieldContent;
      end if;

      -- Kiem tra neu la truong 22 thi se dung ham recipver_bank_map
      -- de lay
      if pvIBPSFilecode = '022' then
        pFieldContent:=RECEIPVER_BANK_MAP(V019,22);
      end if;
      ---------

      vLength := v_pCurMSGDEF.Chklength;
      -- Kiem tra xem truong FileCode co gia tri khong
      if LengTH(pvIBPSFilecode) > 0 then
        -- Lay ra so tien
        m_nAmount := To_Number(Substr(pMESSAGE_TEXT,
                                      m_nGW_TRANS_AMOUNT_POS - m_pnHeadfnPos,
                                      m_nGW_TRANS_AMOUNT_LEN)) / 100;
        -- Neu cac truong Fileld_code trong bang du lieu co gia tri
        pFieldContent := IBPS_CONVERTFIELD(pQuery_ID,
                                           pnDatatype,
                                           pFieldContent,
                                           pvIBPSFilecode,
                                           v_pCurMSGDEF.Tadlength);

        if pvIBPSFilecode = '025' then
          pFieldContent := '2000';
        end if;
        if pvIBPSFilecode = m_vGW_BENNAME then
          pFieldContent := pFieldContent || ' ' || vBenname2;
        end if;
        vFileRealValues := pFieldContent;
        --        Day dien loi ra
        if m_nErr > 1 then
          IBPS_Msg_Update(pQUERY_ID, -1);
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := pQUERY_ID;
          m_IBPS_Type.Status        := 0;
          m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTOUT:Filecode #' ||
                                       pvIBPSFilecode ||
                                       ' field is not found ';
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          IBPS_Msg_Update(pQUERY_ID, -1);
          /* raise_application_error(-20001,
          'Your error message text goes here');*/
        end if;

        IF (pvIBPSFilecode = m_vGW_SENDING_ADD_CODE) then

          Case
            When Upper(pvSIBSFileName) = Upper('XCTADD1') then
              I029Count := I029Count + 1;
              p029Tmp   := Trim(pFieldContent) || trim(p029Tmp);
              -- cap nhat ngay 20/11/2009 noi gia tri 2 field cua truong 029
          /*if (LENGTH(Trim(pFieldContent)) > 30) Then
                                                                                                                                                                                          pnLength := 40 - LENGTH(Trim(p029Tmp));
                                                                                                                                                                                        end if;*/
            When Upper(pvSIBSFileName) = Upper('XCTADD2') then
              I029Count := I029Count + 1;
              -- cap nhat ngay 20/11/2009 noi gia tri 2 field cua truong 029
              /*  p029Tmp1   := p029Tmp ||
              Trim(substr(pFieldContent, 1, 40 - pnLength));*/
              p029Tmp := trim(p029Tmp) || Trim(pFieldContent);

              pnLength := 0;
            else
              pnLength := pnLength;
          end case;

          if (I029Count > 1) then
            pvFieldCoverted := '#' || pvIBPSFilecode || Trim(p029Tmp);
          end IF;
        Else

          -- ghep cac truong so CMT, ngay cap dia chi lai
          IF (pvIBPSFilecode = m_vGW_RECEIVING_ADD_CODE) then
            Case
              When pvSIBSFileName = 'XRMBENID' then

                if (LENGTH(Trim(pFieldContent)) > 0) Then
                  szID      := '' || Trim(pFieldContent);
                  lLengthID := 1;
                else
                  szID := '';

                end if;
                --- Can phai chuyen dinh dang ra kieu ngay
              When pvSIBSFileName = 'XRMBIDDT' then
                if (LENGTH(Trim(pFieldContent)) > 0) /*And lLengthID = 1*/
                 Then
                  szIssDT := '' || LTrim(pFieldContent, '0');
                end if;
              When pvSIBSFileName = 'XRMBIDPL' then
                if (LENGTH(Trim(pFieldContent)) > 0) /*And lLengthID = 1*/
                 Then
                  szIssPL := '' || LTrim(pFieldContent, '0');
                end if;
              When pvSIBSFileName = 'XRMBNAD1' then
                if (LENGTH(Trim(pFieldContent)) > 0) Then
                  szBenAdd := Trim(pFieldContent) || szBenAdd;
                end if;
              When pvSIBSFileName = 'XRMBNAD2' then
                if (LENGTH(Trim(pFieldContent)) > 0) Then
                  if (LENGTH(TRIM(szBenAdd)) > 0) Then
                    szBenAdd := szBenAdd || Trim(pFieldContent);
                  else
                    szBenAdd := Trim(pFieldContent);
                  End if;
                  pnLength := 0;
                End if;
              else
                pnLength := pnLength;
            end case;

          else

            if (pvIBPSFilecode = m_vGW_RECEIVING_BANK_CODE) Then
              m_vReceiver := Trim(pFieldContent);
              if (LENGTH(Trim(pFieldContent)) <= 0) then
                b019existed := true;
              end if;
              pvFieldCoverted := '#' || pvIBPSFilecode ||
                                 Trim(pFieldContent);
            else

              --cap nhat lai 18092008
              if (pvIBPSFilecode = m_vGW_RECEIVE_CORRES_CODE) Then

                m_vReceiver    := Trim(pFieldContent);
                m_receiveTruot := m_vReceiver;
              end if;
              -- het cap nhat
              if (pvIBPSFilecode = m_vGW_SEND_CORRES_CODE) then
                --lay gia tri truot cong truong #021
                vField007old := m_vSendingbank;

                /*m_vProxySender  := GW_PK_IBPS_PROCESS.IBPS_GETOPROXYCI(m_vSendingbank,
                                                                       m_receiveTruot,
                                                                       pQUERY_ID,
                                                                       m_nAMOUNT,
                                                                       iLV_HV,
                                                                       m_vSOURCE_BRANCH);*/

                -- Map Citad
                m_vProxySender := IBPS_GETOPROXYCI(pQUERY_ID,m_nAMOUNT,m_receiveTruot,iLV_HV);

                pFieldContent   := m_vProxySender;
                pvFieldCoverted := '#' || pvIBPSFilecode ||
                                   TRIM(pFieldContent);
                b021existed     := true;
                --Minhhb them luu tam gia tri truong 21 truoc khi update lai
                v021Tmp:= pvFieldCoverted;
              ELSE
                IF (pvIBPSFilecode = m_vGW_RECEIVE_CORRES_CODE) then
                  m_vProxyReceiver := pFieldContent;
                  pvFieldCoverted  := '#' || Trim(pvIBPSFilecode) ||
                                      pFieldContent;
                  b022existed      := true;
                else
                  pvFieldCoverted := '#' || pvIBPSFilecode || pFieldContent;
                End if;
              end if;
            End if;

          end if;

        End if;

        -- Kiem tra ky tu la:

        pvFieldCoverted := LTRIM(pvFieldCoverted, '#');
        pvFieldCoverted := Trim(GW_PK_IBPS_PROCESS.IBPS_REPLACE_Char(pvFieldCoverted,
                                                                     m_GWtype,
                                                                     m_Department,
                                                                     ' ',
                                                                     pvIBPSFilecode,
                                                                     m_Direction,
                                                                     pQUERY_ID,
                                                                     'IBPS_JOB_CONVERTOUT'));

        if substr(pvFieldCoverted, 1, 1) <> '#' then
          pvFieldCoverted := '#' || pvFieldCoverted;
        end if;
        pvMSGConverted := pvMSGConverted || Trim(pvFieldCoverted);
      Else
        -- Neu cac truong Fileld_code trong bang du lieu Khong co gia tri
        if LengTH(v_pCurMSGDEF.Tadfieldcode) > 0 then
          pvFieldCoverted := '#' || pvIBPSFilecode || pFieldContent;
        end IF;
        pvMSGConverted := pvMSGConverted || pvFieldCoverted;
      End IF;

      if ((LengTH(szID) + LengTH(szIssDT) + LengTH(szIssPL) +
         LengTH(szBenAdd)) > 70) then

        szBenAdd := substr(szBenAdd,
                           0,
                           70 -
                           (LengTH(szID) + LengTH(szIssDT) + LengTH(szIssPL)));
      end if;

      if (pvIBPSFilecode = '032' or pvIBPSFilecode = '033') then
        ipos := inStr(pvMSGConverted, '#032');

        if (ipos = 0) then
          szContentTmp := '#032' ||
                          LTrim(szID || '' || szIssDT || '' || szIssPL || '' ||
                                szBenAdd);
        end if;

        ipos := inStr(pvMSGConverted, '#033');
        if (ipos > 0) Then
          pvMSGConverted := substr(pvMSGConverted, 1, ipos - 1) ||
                            Trim(szContentTmp) ||
                            substr(pvMSGConverted, ipos);
        End if;
      end if;

      if (b019existed = true) Then
        szContentTmp := '';
        ipos         := inStr(pvMSGConverted, '#021');
        if (ipos > 0) Then
          m_vReceiver    := m_vProxyReceiver;
          pvMSGConverted := substr(pvMSGConverted, 1, ipos - 1) ||
                            Trim(m_vProxyReceiver) ||
                            substr(pvMSGConverted, ipos);

        END IF;
      END IF;

      if (b021existed = false) Then
        --O-proxy

        /*m_vProxySender := GW_PK_IBPS_PROCESS.IBPS_GETOPROXYCI(m_vSendingbank,
                                                              m_vReceiver,
                                                              pQUERY_ID,
                                                              m_nAMOUNT,
                                                              iLV_HV,
                                                              m_vSOURCE_BRANCH);*/

        m_vProxySender := IBPS_GETOPROXYCI(pQUERY_ID,m_nAMOUNT,m_vReceiver,iLV_HV);

        szTmp := '#021' || m_vProxySender;
        --
        v021Tmp:='#021' || m_vProxySender;
        --
        ipos  := inStr(pvMSGConverted, '#026');
        if (ipos > 0) then
          pvMSGConverted := substr(pvMSGConverted, 1, ipos - 1) ||
                            Trim(szTmp) || substr(pvMSGConverted, ipos);
        END IF;
      END IF;

      if (b022existed = false) Then
        --R_Proxy

        szTmp := '#022' || m_vProxyReceiver;
        ipos  := inStr(pvMSGConverted, '#026');
        if (ipos > 0) then

          pvMSGConverted := substr(pvMSGConverted, 1, ipos - 1) ||
                            Trim(szTmp) || substr(pvMSGConverted, ipos);
        END IF;
      END IF;
      -- gan gia tri cho truong #003
      if vLength + 5 < length(trim(pvFieldCoverted)) then
        m_nErr := -1;

        m_IBPS_Type.Log_Date      := sysdate;
        m_IBPS_Type.Query_Id      := pQUERY_ID;
        m_IBPS_Type.Status        := -1;
        m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTOUT:Filecode #' ||
                                     pvIBPSFilecode ||
                                     ' Length field is longer Standard  ';
        m_IBPS_Type.Area          := ' ';
        m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
        m_IBPS_Type.Msg_Direction := m_Direction;
        GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
        IBPS_Msg_Update(pQUERY_ID, -1);
      end if;

      if vchk = 'M' then
        if pvFieldCoverted is null then
          m_nErr := 1;

          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := pQUERY_ID;
          m_IBPS_Type.Status        := 0;
          m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTOUT:Filecode #' ||
                                       pvIBPSFilecode ||
                                       ' field is not found ';
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          IBPS_Msg_Update(pQUERY_ID, -1);
          -- raise_application_error(m_nErr, sqlerrm);
        end if;
      end if;

      begin
        if length(pvFieldCoverted) > nLength + 4 then
          m_nErr := 1;

          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := pQUERY_ID;
          m_IBPS_Type.Status        := -1;
          m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTOUT:Filecode #' ||
                                       pvIBPSFilecode || ' field too long ';
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          IBPS_Msg_Update(pQUERY_ID, -1);
          -- raise_application_error(m_nErr, sqlerrm);
        end if;
      exception
        when others then
          m_nErr := m_nErr;

      end;

    END LOOP;
    CLOSE pcurMSGDEF;
    -- gan lai gia tri cho #007;

    if trim(m_vSendingbank) is null then
      m_nErr                    := 13;
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := -1;
      m_IBPS_Type.Descriptions  := 'Truot cong loi khong nhan duoc truong #007';
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    end if;
    ipos := inStr(pvMSGConverted, '#007');

    if (ipos > 0) Then
      --      vField007old
      if not vField007old is null then
        pvMSGConverted := Replace(pvMSGConverted,
                                  '#007' || vField007old,
                                  '#007' || m_vSendingbank);
      end if;
    End if;
    ipos := inStr(pvMSGConverted, '#003');
    if (ipos > 0) Then

      pvMSGConverted := Replace(pvMSGConverted,
                                '#003' || m_vTRANS_CODE,
                                '#003' || vField003New);
      m_vTRANS_CODE  := vField003New;
    end if;

    -- Cap nhat Citad moi
    if trim(v_F179) is not null then
      pvMSGConverted := pvMSGConverted || '#036#037100' || '#' || v_F179;
    else
      pvMSGConverted := pvMSGConverted || '#03630#037100';
    end if;
    -- het cap nhat Citad moi
    -- pvMSGConverted := pvMSGConverted || '#';

    V034    := GET_IBPS_Field(pvMSGConverted, '034');
    VOther  := VSCMT || ' ' || VOther;
    vtemp11 := LTRIM(trim(VOther), '0');
    if not vtemp11 is null then

      if not trim(VOther) is null then
        pvMSGConverted := Replace(pvMSGConverted,
                                  '#034' || V034,
                                  '#034' || V034 || ' (' || VOther || ')');

        begin
          VOther := V034 || ' (' || VOther || ')';
          if length(VOther) > 210 then
            m_nErr := 1;

            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := pQUERY_ID;
            m_IBPS_Type.Status        := -1;
            m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTOUT:Filecode #' ||
                                         '034' || ' field too long ';
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
            IBPS_Msg_Update(pQUERY_ID, -1);
            -- raise_application_error(m_nErr, sqlerrm);
          end if;

        exception
          when others then
            m_nErr := m_nErr;

        end;
      end if;
    end if;
    VOther := '';

    if substr(pvMSGConverted, length(pvMSGConverted), 1) <> '#' then
      pvMSGConverted := pvMSGConverted || '#';
    end if;

    -- Cap nhat lai proxy senderBank
    if(isCMT) then
      pvMSGConverted:=replace(pvMSGConverted,v021Tmp,'#021'||'01302001');
    else
      select count(1) into tmpCount from IBPS_TAD_MAP where BANK_CODE=substr(V019New,3,3);
      if (Length(V019New)>6) and (tmpCount>0) then
        select t.gw_bank_code into m_vProxySender from IBPS_TAD_MAP t where BANK_CODE=substr(V019New,3,3) and rownum=1 ;
      End if;
      if (LENGTH(Trim(m_vProxySender)) > 0) then
        pvMSGConverted:=replace(pvMSGConverted,v021Tmp,'#021'||m_vProxySender);
      end if;
    end if;

    return pvMSGConverted;
  Exception
    when OTHERS THEN
      --Rollback;
      m_nErr := -1;

      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTOUT:Filecode #' ||
                                   pvIBPSFilecode ||
                                   ' ERROR WHEN Convert Message' || SQLCODE ||
                                   Sqlerrm;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      IBPS_Msg_Update(pQUERY_ID, -1);
      raise_application_error(m_nErr, sqlerrm);
  END IBPS_CONVERTOUT;

  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: Ham convert dien tu chuan dien SIBS sang IBPS
  Ten ham:  IBPS_CONVERTOUT()
  Tham so:  pvBranchCode Varchar2 Ma chi nhanh 8 so
  Mo ta: -
  Ngay khoi tao:  6/6/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  **********************************************************************/
  FUNCTION IBPS_CONVERTFIELD(pQUERY_ID NUMBER,
                             pDATATYPE NUMBER,
                             pCONTENT  VARCHAR2,
                             pFILECODE VARCHAR2,
                             pLENGHT   NUMBER) RETURN VARCHAR2 IS

    i       integer := 0;
    pReturn Varchar2(4000);
    v019New varchar2(20):='';
  BEGIN

    --Kiem tra
    pReturn := trim(pCONTENT);
    if pDATATYPE = 0 then
      pReturn := RPAD(pReturn, pLENGHT, ' ');
    else
      pReturn := LPAD(pReturn, pLENGHT, '0');
    end if;
    Case
    --110: Relation Number
      WHEN (pFILECODE = m_vGW_RELNUM_CODE) then
        m_nGW_TRANS_NUM := LPAD(m_nGW_TRANS_NUM, pLENGHT, 0);
        pReturn         := LPAD(m_nGW_TRANS_NUM, pLENGHT, 0);
        if Length(pReturn) < 1 Then
          m_nErr := -GW_ERR_REF_NUM;
        end if;
        --return pReturn;

    -- 003: Transaction Code
      WHEN (pFILECODE = m_vGW_MSGID_CODE) THEN

        m_vTRANS_CODE := pCONTENT;
        pReturn       := m_vTRANS_CODE;
        if Length(pReturn) < 1 Then
          m_nErr := -GW_ERR_TRANS_CODE;
        end if;
        --return(pReturn);
    --007: Sending Bank Code
      WHEN (pFILECODE = m_vGW_SENDING_BANK_CODE) Then
        if Length(m_vSendingbank) < 1 Then
          m_nErr := -GW_ERR_SENDINGBANK_FALSE;
        end if;
        pReturn := m_vSendingbank;
        -- return m_vSendingbank;
    -- 019: Receiving Bank Code

      WHEN (pFILECODE = m_vGW_RECEIVING_BANK_CODE) Then
        if Length(TRim(pCONTENT)) < 1 Then
          m_nErr := 14;
        else
          v019New :=RECEIPVER_BANK_MAP(pCONTENT,19);
          --if Check_Branch(TRim(pCONTENT)) = false then
          if Check_Branch(TRim(v019New)) = false then
            m_nErr := 14;
          end if;
        end if;

        pReturn := pCONTENT;
        -- Return pReturn;
    -- 022: Proxy Receiving Bank Code
      WHEN -- 012: Creation Date
       (pFILECODE = m_vGW_CREATION_DATE_CODE) Then
        pReturn := To_char(sysdate, 'YYYYmmddHHMISS');
        --return To_char(sysdate, 'YYYYmmdd');

      WHEN (pFILECODE = m_vGW_RECEIVE_CORRES_CODE) Then
        pReturn := pCONTENT;
        if LENGTH(pReturn) <= 0 then
          pReturn := m_vRECEIVER;
          /* else

          if Check_Branch(TRim(pCONTENT)) = false then
            m_nErr := 15;
          end if;*/
        end if;
        -- Return pReturn;
      WHEN -- 027: Transaction Amount
       (pFILECODE = m_vGW_TRANS_AMOUNT_CODE) Then

        m_nAmount := to_number(pCONTENT) / 100;
        if (m_nAmount <= 0) Then
          m_nErr := -GW_ERR_MSGAMOUNT_FALSE;
        end if;

        pReturn := to_char(pCONTENT);

    --return m_nAmount;
      WHEN (pFILECODE = m_vGW_CURRENCY_CODE) Then
        -- 026: Currency Code
        m_vCCYCD := pCONTENT;
        pReturn  := m_vCCYCD;

        if (Length(Trim(pReturn)) <= 0) then
          m_nErr := -GW_ERR_CURRENCY_FALSE;
        end if;
        -- return pReturn;
      When -- 029, 032: Address code
       (pFILECODE = m_vGW_SENDING_ADD_CODE) OR
       (pFILECODE = m_vGW_RECEIVING_ADD_CODE) Then
        if (LENGTH(pCONTENT) < 75) then
          pReturn := (substr(pCONTENT, 1, 35) || substr(pCONTENT, 41));
          --return pReturn;
        else
          pReturn := pCONTENT;
        end if;

      WHEN -- 030: Sending Account
       (pFILECODE = m_vGW_ACC_SENDING_CODE) then
        pReturn := trim(pContent);
        -- return trim(pReturn);
      WHEN (pFILECODE = m_vGW_DESCRIPTION_CODE) Then

        m_vTRANS_DESCRIPTION := pContent;
        pReturn              := m_vTRANS_DESCRIPTION;
        --return trim(pReturn);
        -- Cap nhat Citad moi
        i := instr(pCONTENT, '%%');
        if i > 0 then
          v_F179 := substr(pCONTENT, 1, i - 1);

        end if;
        if i > 0 then
          m_vTRANS_DESCRIPTION := substr(pCONTENT, i + 2);
        else
          m_vTRANS_DESCRIPTION := substr(pCONTENT, i + 1);
        end if;

    -- Het Cap nhat Citad moi

      else
        pReturn := pContent;
        --return trim(pContent);

    END CASE;

    return trim(pReturn);
  Exception
    when OTHERS THEN
      --Rollback;
      m_nErr := -1;

      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pQUERY_ID;
      m_IBPS_Type.Status        := m_nErr;
      m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTFIELD : ERROR WHEN Convert Message' ||
                                   SQLCODE || SQLERRM || '; Field :' ||
                                   pFILECODE;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Job_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      IBPS_Msg_Update(pQUERY_ID, -1);
      raise_application_error(m_nErr, sqlerrm);

  END IBPS_CONVERTFIELD;

  FUNCTION GET_IBPS_Field(pCOntent varchar2, FiledCode varchar2)
    Return Varchar2 IS
    iposStart integer := 0;
    iposEnd   integer := 0;
    v_Value   varchar2(500);
    vTemp     varchar2(4000);

  BEGIN

    iposStart := instr(pCOntent, '#' || FiledCode);
    if iposStart > 0 then
      vTemp   := substr(pCOntent, iposStart + 4);
      iposEnd := instr(vTemp || '#', '#');
      v_Value := substr(vTemp, 1, iposEnd - 1);
      return trim(v_Value);
    else
      return '';
    end if;
  Exception
    when others then
      Return '';
  END GET_IBPS_Field;
  /*-------------------------------------------------------------------------------------------------------------------
  Nguoi tao:  QuanLD--
  Muc dich: gan lai cong CITAD cho nhung dien cua cong CITAD bi loi
  Ten ham:  Forward_MSG_OUT()
  Ngay khoi tao:  6/6/2008
  Nguoi sua: hienntm
  Ngay sua: 01/08/2008
  Mo ta
  Version:    v1.0
  */ -------------------------------------------------------------------------------------------------------------------
  Procedure Forward_MSG_OUT(pQueryID  Number,
                            pTad      varchar2,
                            pReTad    varchar2,
                            pHL_VAL   number,
                            pTELLERID VARCHAR) IS
    IPBS_CT IBPS_MSG_CONTENT%rowtype;
    --    pMSGContent  varchar2(4000);
    pMSGContent1 varchar2(4000);
    pMSGContent2 varchar2(4000);
    iPosStart    integer := 0;
    iPosEnd      integer := 0;
    v_GWBankcode Varchar2(15);
    mHL          number;
    mAMOUNT      NUMBER;
    mTIME        VARCHAR(10);

  BEgin

    select *
      Into IPBS_CT
      from IBPS_MSG_CONTENT IMC
     Where IMC.MSG_ID = pQueryID
       And Rownum = 1;

    -- Thuc hien gan lai gia tri truot cong vao truonf #021
    --Lay  ma 8 so tu bang TAD
    select TAD.GW_BANK_CODE
      INTO v_GWBankcode
      FROM TAD
     Where TAD.Sibs_Bank_Code = LPAD(pTAD, 5, '0');

    iPosStart := instr(IPBS_CT.Content, '#021');
    For i in iPosStart + 4 .. Length(IPBS_CT.Content) Loop
      If Substr(IPBS_CT.Content, i, 1) = '#' Then
        iPosEnd := i;
        Exit;
      End if;
    End Loop;
    pMSGContent1 := Substr(IPBS_CT.Content, 1, iPosStart + 3);
    pMSGContent2 := Substr(IPBS_CT.Content, iPosEnd);

    IPBS_CT.Content := pMSGContent1 || v_GWBankcode || pMSGContent2;
    IPBS_CT.TAD     := pTAD;
    IPBS_CT.Pre_TAD := pReTad;

    -- Cap nhta lai so but toan:
    iPosStart := instr(IPBS_CT.Content, '#110');
    iPosEnd   := instr(IPBS_CT.Content, '#003');

    IPBS_CT.preTRANS_NUM := IPBS_CT.GW_TRANS_NUM;
    IPBS_CT.GW_TRANS_NUM := substr(IPBS_CT.CONTENT,
                                   iPosStart + 4,
                                   iPosEnd - (iPosStart + 4));
    IPBS_CT.GW_TRANS_NUM := '9' || IPBS_CT.GW_TRANS_NUM;

    pMSGContent1    := Substr(IPBS_CT.Content, 1, iPosStart + 3);
    pMSGContent2    := Substr(IPBS_CT.Content, iPosEnd);
    IPBS_CT.Content := pMSGContent1 || IPBS_CT.GW_TRANS_NUM || pMSGContent2;

    -- xac dinh gia tri thap cao---
    SELECT AMOUNT, TO_CHAR(TIME, 'HH:MI:SS')
      INTO mAMOUNT, mTIME
      FROM TAD
     WHERE SIBS_BANK_CODE = pTAD;

    case pHL_VAL
      when 0 then
        mHL := gHValue;
      when 1 then
        mHL := gLValue;
      else
        mHL := 2;
    end case;
    ---1*---
    if nvl(mHL, 2) <> 2 then
      ---2*---
      if mHL <> IPBS_CT.TRANS_code then
        IF mHL = gLValue THEN
          IF IPBS_CT.amount < mAMOUNT THEN
            IF TO_CHAR(sysdate, 'HH:MI:SS') < mTime THEN
              iPosStart            := instr(IPBS_CT.Content, '#003');
              pMSGContent1         := Substr(IPBS_CT.Content,
                                             1,
                                             iPosStart + 3);
              pMSGContent2         := Substr(IPBS_CT.Content, iPosStart + 9);
              IPBS_CT.Content      := pMSGContent1 || mHL || pMSGContent2;
              IPBS_CT.pretran_code := IPBS_CT.trans_code;
              IPBS_CT.trans_code   := mHL;
            END IF;
          END IF;
        ELSE
          iPosStart            := instr(IPBS_CT.Content, '#003');
          pMSGContent1         := Substr(IPBS_CT.Content, 1, iPosStart + 3);
          pMSGContent2         := Substr(IPBS_CT.Content, iPosStart + 9);
          IPBS_CT.Content      := pMSGContent1 || mHL || pMSGContent2;
          IPBS_CT.pretran_code := IPBS_CT.trans_code;
          IPBS_CT.trans_code   := mHL;
        END IF;
        ---2*---
      end if;
      ---1*---
    end if;

    case IPBS_CT.STATUS
      WHEN 0 THEN
        IPBS_CT.FWSTS := 1;
      WHEN 1 THEN
        IPBS_CT.FWSTS := 2;
    END CASE;

    -----UPDATE BANG CONTENT -----
    UPdate IBPS_MSG_CONTENT
       Set TAD          = IPBS_CT.TAD,
           Content      = IPBS_CT.Content,
           status       = 0,
           PRE_TAD      = IPBS_CT.PRE_TAD,
           pretran_code = IPBS_CT.pretran_code,
           trans_code   = IPBS_CT.trans_code,
           preTRANS_NUM = IPBS_CT.preTRANS_NUM,
           GW_TRANS_NUM = IPBS_CT.GW_TRANS_NUM,
           TELLERID     = pTELLERID,
           FWSTS        = IPBS_CT.FWSTS
     where MSG_ID = pQUERYID;

    -----GHI LOG -----
    INSERT INTO IBPS_MSG_LOG
      (LOG_DATE,
       QUERY_ID,
       STATUS,
       DESCRIPTIONS,
       AREA,
       JOB_NAME,
       MSG_DIRECTION)
    VALUES
      (SYSDATE,
       pQueryID,
       1,
       'USER: ' || pTELLERID || 'FORWARD TAD IS succesfully!',
       NULL,
       NULL,
       NULL);
    commit;

  end Forward_MSG_OUT;
  /*-------------------------------------------------------------------------------------------------------------------
  Nguoi tao:  hienntm
  Muc dich: chuyen tiep cac dien bi rot LV trong ngay
  Ten ham:  Forward_LowValue()
  Ngay khoi tao:  29/7/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  */ -------------------------------------------------------------------------------------------------------------------
  Procedure Forward_LowValue(pMSG_ID Number, pTELLERID VARCHAR) IS
    pMSGContent varchar2(4000);
    pTrans_code varchar2(10);
    pQueryID    varchar2(20);
    mPos        number;
    mSTATUS     NUMBER;
    rel_no      varchar2(10);
  begin
    select IMC.trans_code, IMC.Content, IMC.QUERY_ID, mSTATUS
      Into pTrans_code, pMSGContent, pQueryID, mSTATUS
      from IBPS_MSG_CONTENT IMC
     Where IMC.msg_ID = pMSG_ID
       And Rownum = 1;

    if pTrans_code = gLValue then
      mPos   := instr(pMSGContent, '#003');
      rel_no := substr(pMSGContent, 5, mPos - 5);

      pMSGContent := substr(pMSGContent, 1, 4) ||
                     concat('7', substr(rel_no, 2)) || '#003' || gHValue ||
                     substr(pMSGContent, mPos + 10);

      -----UPDATE LAI GIA TRI CHO BANG CONTENT -----
      update IBPS_MSG_CONTENT
         set content      = pMSGContent,
             PRETRAN_CODE = pTrans_code,
             Trans_code   = gHValue,
             STATUS       = 0,
             TELLERID     = pTELLERID,
             FWSTS        = DECODE(mSTATUS, 0, 3, 1, 4)
       Where msg_ID = pMSG_ID;
      -----GHI LOG -----
      INSERT INTO IBPS_MSG_LOG
        (LOG_DATE,
         QUERY_ID,
         STATUS,
         DESCRIPTIONS,
         AREA,
         JOB_NAME,
         MSG_DIRECTION)
      VALUES
        (SYSDATE,
         pQueryID,
         1,
         'USER: ' || pTELLERID ||
         'CHANGE LOW VALUE TO HIGH VALUE IS succesfully!',
         NULL,
         NULL,
         NULL);

    end if;
  end;
  /*-------------------------------------------------------------------------------------------------------------------
  Nguoi tao:  hienntm
  Muc dich: chuyen tiep cac dien bi rot ngay hom truoc
  Ten ham:  Forward_Failmsg()
  Ngay khoi tao:  29/7/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  */ -------------------------------------------------------------------------------------------------------------------
  PROCEDURE Forward_Failmsg(pMSG_ID   Number,
                            pHL_Val   number,
                            pTELLERID VARCHAR) IS
    IBPS_CONTENT IBPS_MSG_ALL%ROWTYPE;
    mPos         number;
    rel_no       varchar2(10);
    mTemp        number;
    ---h 0/ l 1
  begin
    select *
      Into IBPS_CONTENT
      from IBPS_MSG_ALL IMC
     Where IMC.MSG_ID = pMSG_ID;
    ------thay doi ngay hieu luc cua dien-----
    mPos                 := instr(IBPS_CONTENT.CONTENT, '#012');
    IBPS_CONTENT.CONTENT := substr(IBPS_CONTENT.CONTENT, 1, mPos + 3) ||
                            to_char(sysdate, 'YYYYMMDD') ||
                            substr(IBPS_CONTENT.CONTENT, mPos + 12);
    ------thay doi so but toan cua dien-----
    mPos   := instr(IBPS_CONTENT.CONTENT, '#003');
    rel_no := substr(IBPS_CONTENT.CONTENT, 5, mPos - 5);
    rel_no := concat('8', substr(rel_no, 2));

    ------thay doi Low-> Higth-----
    if pHL_Val = 1 then
      mtemp := gLValue;
    else
      mtemp := gHValue;
    end if;

    IBPS_CONTENT.PRETRANS_NUM := IBPS_CONTENT.GW_TRANS_NUM;
    IBPS_CONTENT.GW_TRANS_NUM := rel_no;
    --      IBPS_CONTENT.PRETRANS_DATE:=IBPS_CONTENT.TRANS_DATE;
    IBPS_CONTENT.TRANS_DATE := TO_CHAR(SYSDATE, 'DD-MON-YY');
    CASE IBPS_CONTENT.STATUS
      WHEN 0 THEN
        IBPS_CONTENT.FWSTS := 5;
      WHEN 1 THEN
        IBPS_CONTENT.FWSTS := 6;
    END CASE;

    if IBPS_CONTENT.TRANS_CODE <> mtemp then
      ---co thay doi gia tri
      IBPS_CONTENT.CONTENT := substr(IBPS_CONTENT.CONTENT, 1, 4) || rel_no ||
                              '#003' || mtemp ||
                              substr(IBPS_CONTENT.CONTENT, mPos + 10);

      IBPS_CONTENT.PRETRAN_CODE := IBPS_CONTENT.TRANS_CODE;
      IBPS_CONTENT.TRANS_CODE   := mtemp;
      IBPS_CONTENT.Transdate    := to_char(IBPS_CONTENT.Trans_Date,
                                           'YYYYMMDD');

      -----UPDATE LAI GIA TRI CHO BANG CONTENT -----
      --INSERT INTO IBPS_FORWARD VALUES IBPS_CONTENT;
      update IBPS_MSG_all
         set content      = IBPS_CONTENT.CONTENT,
             TRANS_CODE   = IBPS_CONTENT.TRANS_CODE,
             GW_TRANS_NUM = IBPS_CONTENT.GW_TRANS_NUM,
             PRETRANS_NUM = IBPS_CONTENT.PRETRANS_NUM,
             PRETRAN_CODE = IBPS_CONTENT.PRETRAN_CODE,
             STATUS       = 0,
             TELLERID     = pTELLERID,
             FWSTS        = IBPS_CONTENT.FWSTS
       Where MSG_ID = pMSG_ID;

    else
      IBPS_CONTENT.CONTENT := substr(IBPS_CONTENT.CONTENT, 1, 4) || rel_no ||
                              substr(IBPS_CONTENT.CONTENT, mPos);
      --INSERT INTO IBPS_FORWARD VALUES IBPS_CONTENT;
      update IBPS_MSG_all
         set content      = IBPS_CONTENT.CONTENT,
             GW_TRANS_NUM = IBPS_CONTENT.GW_TRANS_NUM,
             PRETRANS_NUM = IBPS_CONTENT.PRETRANS_NUM,
             STATUS       = 0,
             TELLERID     = pTELLERID,
             FWSTS        = IBPS_CONTENT.FWSTS
       Where MSG_ID = pMSG_ID;

    end if;
    -----GHI LOG -----
    INSERT INTO IBPS_MSG_LOG
      (LOG_DATE,
       QUERY_ID,
       STATUS,
       DESCRIPTIONS,
       AREA,
       JOB_NAME,
       MSG_DIRECTION)
    VALUES
      (SYSDATE,
       IBPS_CONTENT.Query_ID,
       1,
       'USER: ' || pTELLERID || 'Forward OLD MESSAGE IS succesfully!',
       NULL,
       NULL,
       NULL);
    COMMIT;

  end;

  ---------------------------------------
  --TEST
  ---------------------------------------

  ---------------------------------------
  -- kiem tra xem ma ngan hang co ton tai trng bang IBPS_BANK_MAp khong
  ---------------------------------------

  Function Check_Branch(pBrach_code varchar2) return boolean IS
    icheck integer := 0;
  Begin
    select count(1)
      into icheck
      from IBPS_BANK_MAP
     where IBPS_BANK_MAP.GW_BANK_CODE = trim(pBrach_code);
    if icheck > 0 then
      return true;
    else
      return false;
    end if;
  exception
    when others then
      return false;
  end;

  -- ham thuc hien chay tong hop ca IBPS_TR va IBPS_Rm
  PROCEDURE IBPS_RM_TR_DE_CONVERTOUT IS

  Begin
    GW_PK_IBPS_Q_CONVERTOUT.IBPS_DE_CONVERTOUT;
    Gw_Pk_Ibps_Tr_Convertout.IBPS_TR_DE_CONVERTOUT;
    GW_PK_OL3_CONVERTOUT.IBPS_DE_CONVERTOUT;

  End IBPS_RM_TR_DE_CONVERTOUT;
  -- Lay Ma 8 so theo chi nhanh hoi so theo dieu kien TellerID
  FUNCTION GETBANKCODE_TELLER(vBranch varchar2, vTellerID varchar2)
    Return varchar2 IS
    vRreturn Varchar2(30) := '';
  BEGIN
    Select IBM.GW_BANK_CODE
      into vRreturn
      from IBPS_BANK_MAP IBM
     where IBM.SIBS_BANK_CODE = vBranch
       and IBM.SHORT_NAME = vTellerID
       and Rownum = 1;

    Return vRreturn;
  EXCEPTION
    WHEN OTHERS THEN
      Return '';
  END GETBANKCODE_TELLER;
  FUNCTION GETTAD(vGWCode varchar2) return varchar2 IS
    vReturn varchar2(20);
  BEGIN

    select TAD.SIBS_CODE
      into vReturn
      from TAD
     where TAD.GW_BANK_CODE = trim(vGWCode)
       and rownum = 1;
    return vReturn;
  exception
    when others then
      return '';
  END GETTAD;

  -- Get cong TAD theo luat moi.
  FUNCTION GETTAD_BY_NEW_RULE(vGWCode varchar2,vF19 varchar2,vF22 varchar2,isCMT Boolean) return varchar2 IS
    vReturn varchar2(20);
    i number(20);
  BEGIN

    If(isCMT) then
    --Neu la CMT thi gan ve cong 011.
       vReturn:='00011';
       return vReturn;
    else
    -- Khong phai la dien CMT
    -- Lay tad trong bang map cong cho cac truong hop loai tru
       select count(1) into i from IBPS_TAD_MAP where BANK_CODE=substr(vF19,3,3) and rownum=1 ;
       if Length(vF19)>6 and i>0 then
         select Tad_Code into vReturn from IBPS_TAD_MAP where BANK_CODE=substr(vF19,3,3) and rownum=1 ;
       end if;

       if vReturn <>'' then
         return vReturn;
       /*else
         -- Neu truong hop dien gian tiep map ve cong 011.
         if(vF19<>vF22) then
             vReturn:='00011';
         end if;
         return vReturn;  */
       end if;
    End if;

    -- lay ma Tad dua vao F21
    vReturn:=GETTAD(vGWCode);

    return vReturn;
    exception
    when others then
      return '';
  END GETTAD_BY_NEW_RULE;

  --Them moi map ma gian tiep truc tiep
  FUNCTION RECEIPVER_BANK_MAP(vF19 Varchar2,fieldCode number) return varchar2 IS
    vResult varchar2(30);
  Begin
    vResult := '';

  if fieldCode=19 then
    select RBM.F19
      into vResult
      from IBPS_RECEIPTBANKMAP RBM
     where RBM.SIBS = vF19
       and rownum = 1;
  else if fieldCode=22 then
       select RBM.F22
      into vResult
      from IBPS_RECEIPTBANKMAP RBM
     where RBM.SIBS = vF19
       and rownum = 1;
       end if;
   end if;
    return vResult;
  Exception
    when others then
      vResult := vF19;
      return vResult;

  END RECEIPVER_BANK_MAP;

  --New Map Tad Field 21
  FUNCTION IBPS_GETOPROXYCI(pQUERY_ID       NUMBER,
                            pAmount         number,
                            pvBranchReceipt varchar2,
                            ISlow           in out boolean) RETURN VARCHAR2 IS
  vdaumoi         Varchar2(50) := '';
  iCount          integer;
  BEGIN
    -- IS Low
    IF(ISlow=TRUE) then
      return '79302001';
    else
      select count(1) into iCount from TAD WHERE substr(TAD.GW_BANK_CODE, 1, 2) = substr(pvBranchReceipt, 1, 2);
      if(iCount>0) then
            SELECT TAD.GW_BANK_CODE INTO vdaumoi FROM TAD
       WHERE substr(TAD.GW_BANK_CODE, 1, 2) = substr(pvBranchReceipt, 1, 2)
         AND TAD.FUNCTION in ('2', '3')
         AND TAD.STATUS = 1
         AND Rownum = 1;
         return vdaumoi;
      else
        return '01302001';
      end if;

    end if;
    Exception
    when others then
      vdaumoi := '01302001';
      return vdaumoi;

  END IBPS_GETOPROXYCI;


end GW_PK_IBPS_Q_ConvertOut;
