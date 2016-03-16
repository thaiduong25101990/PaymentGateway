create or replace package GW_PK_OL3_CONVERTOUT is

  Procedure IBPS_EQ_CONVERT_OUT(vQUERY_ID  NUMBER,
                                vRDBR      VARCHAR2,
                                vTLBF01    VARCHAR2,
                                vRMBENA    VARCHAR2,
                                vRMAMT     VARCHAR2,
                                vRMSNME    VARCHAR2,
                                vRMACNO    VARCHAR2,
                                vTLBAFM    VARCHAR2,
                                vTLBRMK    VARCHAR2,
                                vTLBF09    VARCHAR2,
                                vTLBID     VARCHAR2,
                                vRMPRDC    VARCHAR2,
                                vRDEFTH    VARCHAR2,
                                vRMPB40    VARCHAR2,
                                vTLBDEL    VARCHAR2,
                                vTLBCOR    VARCHAR2,
                                vRMDIS7    VARCHAR2,
                                vRMSTS7    VARCHAR2,
                                vRDBR_HOST VARCHAR2,
                                vRDRACT    VARCHAR2,
                                vDEFAULT1  VARCHAR2,
                                vDEFAULT2  VARCHAR2,
                                vDEFAULT3  VARCHAR2,
                                vDEFAULT4  VARCHAR2,
                                vDEFAULT5  VARCHAR2);
  PROCEDURE GET_IBPS_OL3;
  procedure IBPS_OL3_Update(pRMACNO       NUMBER,
                            pSTATUS       NUMBER,
                            pMANUFACTURES varchar2);
  PROCEDURE IBPS_DE_CONVERTOUT;
  FUNCTION GET_BANK_MAP(pSIBS_BANK_CODE varchar2) return varchar2;
  FUNCTION GET_RELATION_NUMBER return varchar2;

  FUNCTION GET_TAD(pvSIBS_BANK_CODE varchar2, pvTLBID varchar2)
    return varchar2;
  FUNCTION GETTAD(vGWCode varchar2) return varchar2;

end GW_PK_OL3_CONVERTOUT;
/
create or replace package body GW_PK_OL3_CONVERTOUT is

  GW_ERR_CURRENCY_FALSE NUMBER(2) := 5;
  GW_ERR_SENDER         NUMBER(2) := 13;
  GW_ERR_RECEIVER       NUMBER(2) := 15;
  GW_ERR_RECEIVER19     NUMBER(2) := 14;
  GW_ERR_RMNUMBER       NUMBER(2) := 2;
  GW_ERR_LONG           NUMBER(2) := 1;
  GW_ERR_F31_LONG       NUMBER(2) := 30;
  GW_ERR_F31_34_LONG    NUMBER(2) := 31;

  m_IBPS_Type   IBPS_MSG_LOG%rowtype;
  m_Direction   varchar2(10) := 'SIBS-IBPS';
  m_Department  varchar2(3) := 'RM';
  m_nErr        NUMBER(3);
  pRDBR         VARCHAR2(10);
  pTLBF01       VARCHAR2(20);
  pRMBENA       VARCHAR2(20);
  pRMAMT        VARCHAR2(20);
  pRMSNME       VARCHAR2(100);
  pRMACNO       VARCHAR2(20);
  pTLBAFM       VARCHAR2(2000);
  pTLBRMK       VARCHAR2(2);
  pTLBF09       VARCHAR2(14);
  pTLBID        VARCHAR2(8);
  pRMPRDC       VARCHAR2(1);
  pRDEFTH       VARCHAR2(220);
  pRMPB40       VARCHAR2(50);
  pTLBDEL       VARCHAR2(15);
  pTLBCOR       VARCHAR2(1);
  pRMDIS7       VARCHAR2(8);
  pRMSTS7       VARCHAR2(8);
  pQUERY_ID     NUMBER(20);
  pMANUFACTURES VARCHAR2(3);
  pRMCURR       VARCHAR2(3);
  pRDBR_HOST    VARCHAR2(5);
  pRDRACT       VARCHAR2(14);
  pDEFAULT1     VARCHAR2(200);
  pDEFAULT2     VARCHAR2(200);
  pDEFAULT3     VARCHAR2(200);
  pDEFAULT4     VARCHAR2(200);
  pDEFAULT5     VARCHAR2(200);
  --Cac gia tri can lay cho truong content
  RELATION_NUMBER             Varchar2(6); --110
  TRANS_CODE                  varchar2(6); --003
  O_CI_CODE                   varchar2(8); --007
  TRANS_DATE                  varchar2(14); --012
  R_CI_CODE                   varchar2(8); --019
  O_PROXY_CI_CODE             varchar2(8); --021
  PROXY_CI_CODE               varchar2(8); --022
  XXX_025                     varchar2(4); --025
  CURRENCY_CODE               varchar2(3); --026
  TRANS_AMOUNT                number(19, 2); --027
  SENDING_CUS_NAME            varchar2(100); --028   
  SENDING_CUS_ADDR            varchar2(1); --029 value='';   
  SENDING_CUS_ACCOUNT         varchar2(4); --030   
  RECEIVING_CUS_NAME          varchar2(170); --031   
  RECEIVING_CUS_ADDR          varchar2(1); --032 value='';   
  RECEIVING_CUS_ACCOUNT       varchar2(20); --033    
  CONTENT_PAYMENT_INSTRUCTION varchar2(300); --034   
  OPRERATION1                 varchar2(2); --036    
  OPRERATION2                 varchar2(3); --037    

  Procedure IBPS_EQ_CONVERT_OUT(vQUERY_ID  NUMBER,
                                vRDBR      VARCHAR2,
                                vTLBF01    VARCHAR2,
                                vRMBENA    VARCHAR2,
                                vRMAMT     VARCHAR2,
                                vRMSNME    VARCHAR2,
                                vRMACNO    VARCHAR2,
                                vTLBAFM    VARCHAR2,
                                vTLBRMK    VARCHAR2,
                                vTLBF09    VARCHAR2,
                                vTLBID     VARCHAR2,
                                vRMPRDC    VARCHAR2,
                                vRDEFTH    VARCHAR2,
                                vRMPB40    VARCHAR2,
                                vTLBDEL    VARCHAR2,
                                vTLBCOR    VARCHAR2,
                                vRMDIS7    VARCHAR2,
                                vRMSTS7    VARCHAR2,
                                vRDBR_HOST VARCHAR2,
                                vRDRACT    VARCHAR2,
                                vDEFAULT1  VARCHAR2,
                                vDEFAULT2  VARCHAR2,
                                vDEFAULT3  VARCHAR2,
                                vDEFAULT4  VARCHAR2,
                                vDEFAULT5  VARCHAR2) IS
    queue_options      DBMS_AQ.ENQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(16);
    my_message         IBPS_TYPE_CONVERTOUT_OL3;
  BEGIN
  
    --start cai queue len
    DBMS_AQADM.start_queue('IBPS_Q_CONVERTOUT_OL3', TRUE, TRUE);
  
    my_message := IBPS_TYPE_CONVERTOUT_OL3(vQUERY_ID,
                                           vRDBR,
                                           vTLBF01,
                                           vRMBENA,
                                           vRMAMT,
                                           vRMSNME,
                                           vRMACNO,
                                           vTLBAFM,
                                           vTLBRMK,
                                           vTLBF09,
                                           vTLBID,
                                           vRMPRDC,
                                           vRDEFTH,
                                           vRMPB40,
                                           vTLBDEL,
                                           vTLBCOR,
                                           vRMDIS7,
                                           vRMSTS7,
                                           vRDBR_HOST,
                                           vRDRACT,
                                           vDEFAULT1,
                                           vDEFAULT2,
                                           vDEFAULT3,
                                           vDEFAULT4,
                                           vDEFAULT5);
  
    DBMS_AQ.ENQUEUE(queue_name         => 'IBPS_Q_CONVERTOUT_OL3',
                    enqueue_options    => queue_options,
                    message_properties => message_properties,
                    payload            => my_message,
                    msgid              => message_id);
    COMMIT;
  
  Exception
    when OTHERS THEN
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := vQUERY_ID;
      m_IBPS_Type.Status        := -1;
      m_IBPS_Type.Descriptions  := 'Error When getmessage from MSG content Message' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_INQUITYOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      Rollback;
    
  END IBPS_EQ_CONVERT_OUT;

  PROCEDURE GET_IBPS_OL3 is
  
    Cursor vMSGOUT is
      Select IO.TRANSDATE,
             IO.RDBR,
             IO.TLBF01,
             IO.RMBENA,
             IO.RMAMT,
             IO.RMCURR,
             IO.RMSNME,
             IO.RMACNO,
             IO.TLBAFM,
             IO.TLBRMK,
             IO.TLBF09,
             IO.TLBID,
             IO.RMPRDC,
             IO.RDEFTH,
             IO.RMPB40,
             IO.TLBDEL,
             IO.TLBCOR,
             IO.RMDIS7,
             IO.RMSTS7,
             IO.QUERY_ID,
             IO.STATUS,
             IO.MANUFACTURES,
             IO.RDBR_HOST,
             IO.RDRACT,
             IO.DEFAULT1,
             IO.DEFAULT2,
             IO.DEFAULT3,
             IO.DEFAULT4,
             IO.DEFAULT5
        from IBPS_OL3 IO
       Where IO.STATUS = 0
         and lpad(IO.RDBR, 5, '0') in
             (select lpad(SIBS_CODE, 5, '0')
                from tad
              union
              select '00VCB' as SIBS_CODE from dual)
         and Rownum < 100;
    m_IBPSTypeOutIN IBPS_TYPE_CONVERTOUT_OL3;
    m_VCBTypeOutIN  VCB_TYPE_CONVERTOUT_OL2;
    m_vMSGOUT       vMSGOUT%Rowtype;
    mError          varchar2(2000);
  BEGIN
    OPEN vMSGOUT;
    LOOP
      Fetch vMSGOUT
        into m_vMSGOUT;
      EXIT WHEN vMSGOUT %notfound;
    
      pRDBR         := m_vMSGOUT.Rdbr;
      pTLBF01       := m_vMSGOUT.TLBF01;
      pRMBENA       := m_vMSGOUT.RMBENA;
      pRMAMT        := m_vMSGOUT.RMAMT;
      pRMCURR       := m_vMSGOUT.RMCURR;
      pRMSNME       := m_vMSGOUT.RMSNME;
      pRMACNO       := m_vMSGOUT.RMACNO;
      pTLBAFM       := m_vMSGOUT.TLBAFM;
      pTLBRMK       := m_vMSGOUT.TLBRMK;
      pTLBF09       := m_vMSGOUT.TLBF09;
      pTLBID        := m_vMSGOUT.TLBID;
      pRMPRDC       := m_vMSGOUT.RMPRDC;
      pRDEFTH       := m_vMSGOUT.RDEFTH;
      pRMPB40       := m_vMSGOUT.RMPB40;
      pTLBDEL       := m_vMSGOUT.TLBDEL;
      pTLBCOR       := m_vMSGOUT.TLBCOR;
      pRMDIS7       := m_vMSGOUT.RMDIS7;
      pRMSTS7       := m_vMSGOUT.RMSTS7;
      pQUERY_ID     := m_vMSGOUT.QUERY_ID;
      pMANUFACTURES := m_vMSGOUT.Manufactures;
      pRDBR_HOST    := m_vMSGOUT.Rdbr_Host;
      pRDRACT       := m_vMSGOUT.RDRACT;
      pDEFAULT1     := m_vMSGOUT.Default1;
      pDEFAULT2     := m_vMSGOUT.Default2;
      pDEFAULT3     := m_vMSGOUT.Default3;
      pDEFAULT4     := m_vMSGOUT.Default4;
      pDEFAULT5     := m_vMSGOUT.Default5;
    
      if pMANUFACTURES = 'OL3' then
        begin
          m_IBPSTypeOutIN := IBPS_TYPE_CONVERTOUT_OL3(pQUERY_ID,
                                                      pRDBR,
                                                      pTLBF01,
                                                      pRMBENA,
                                                      pRMAMT,
                                                      pRMSNME,
                                                      pRMACNO,
                                                      pTLBAFM,
                                                      pTLBRMK,
                                                      pTLBF09,
                                                      pTLBID,
                                                      pRMPRDC,
                                                      pRDEFTH,
                                                      pRMPB40,
                                                      pTLBDEL,
                                                      pTLBCOR,
                                                      pRMDIS7,
                                                      pRMSTS7,
                                                      pRDBR_HOST,
                                                      pRDRACT,
                                                      pDEFAULT1,
                                                      pDEFAULT2,
                                                      pDEFAULT3,
                                                      pDEFAULT4,
                                                      pDEFAULT5);
          IBPS_EQ_CONVERT_OUT(pQUERY_ID,
                              pRDBR,
                              pTLBF01,
                              pRMBENA,
                              pRMAMT,
                              pRMSNME,
                              pRMACNO,
                              pTLBAFM,
                              pTLBRMK,
                              pTLBF09,
                              pTLBID,
                              pRMPRDC,
                              pRDEFTH,
                              pRMPB40,
                              pTLBDEL,
                              pTLBCOR,
                              pRMDIS7,
                              pRMSTS7,
                              pRDBR_HOST,
                              pRDRACT,
                              pDEFAULT1,
                              pDEFAULT2,
                              pDEFAULT3,
                              pDEFAULT4,
                              pDEFAULT5);
        
          IBPS_OL3_Update(pRMACNO, 2, 'OL3');
        Exception
          when others then
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := pQUERY_ID;
            m_IBPS_Type.Status        := -1;
            m_IBPS_Type.Descriptions  := 'Error When getmessage from MSG content Message' ||
                                         SQLCODE || ' -ERROR- ' || SQLERRM;
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_INQUITYOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
            IBPS_OL3_Update(pRMACNO, -1, 'OL3');
        end;
      
      Else
        begin
          m_VCBTypeOutIN := VCB_TYPE_CONVERTOUT_OL2(pQUERY_ID,
                                                    pRDBR,
                                                    pTLBF01,
                                                    pRMBENA,
                                                    pRMAMT,
                                                    pRMCURR,
                                                    pRMSNME,
                                                    pRMACNO,
                                                    pTLBAFM,
                                                    pTLBRMK,
                                                    pTLBF09,
                                                    pTLBID,
                                                    pRMPRDC,
                                                    pRDEFTH,
                                                    pRMPB40,
                                                    pTLBDEL,
                                                    pTLBCOR,
                                                    pRMDIS7,
                                                    pRMSTS7,
                                                    pRDRACT,
                                                    pDEFAULT1,
                                                    pDEFAULT2,
                                                    pDEFAULT3,
                                                    pDEFAULT4,
                                                    pDEFAULT5);
          GW_PK_VCB_OL2_CONVERTOUT.VCB_EQ_CONVERT_OUT(pQUERY_ID,
                                                      pRDBR,
                                                      pTLBF01,
                                                      pRMBENA,
                                                      pRMAMT,
                                                      pRMCURR,
                                                      pRMSNME,
                                                      pRMACNO,
                                                      pTLBAFM,
                                                      pTLBRMK,
                                                      pTLBF09,
                                                      pTLBID,
                                                      pRMPRDC,
                                                      pRDEFTH,
                                                      pRMPB40,
                                                      pTLBDEL,
                                                      pTLBCOR,
                                                      pRMDIS7,
                                                      pRMSTS7,
                                                      pRDRACT,
                                                      pDEFAULT1,
                                                      pDEFAULT2,
                                                      pDEFAULT3,
                                                      pDEFAULT4,
                                                      pDEFAULT5);
          IBPS_OL3_Update(pRMACNO, 1, 'OL2');
        Exception
          when others then
            mError                    := SQLERRM;
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := pQUERY_ID;
            m_IBPS_Type.Status        := -1;
            m_IBPS_Type.Descriptions  := 'Error When getmessage from MSG content Message' ||
                                         SQLCODE || ' -ERROR- ' || SQLERRM;
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_INQUITYOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
            IBPS_OL3_Update(pRMACNO, -1, 'OL2');
        end;
      
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
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
  END GET_IBPS_OL3;

  procedure IBPS_OL3_Update(pRMACNO       NUMBER,
                            pSTATUS       NUMBER,
                            pMANUFACTURES varchar2) is
  
  Begin
    UPDATE IBPS_OL3
       set STATUS = pSTATUS
     Where RMACNO = pRMACNO
       and MANUFACTURES = pMANUFACTURES;
    COMMIT;
  Exception
    when OTHERS THEN
      Rollback;
  end;

  PROCEDURE IBPS_DE_CONVERTOUT IS
  
    queue_options      DBMS_AQ.DEQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(5000);
    my_message         ibps_type_convertout_ol3;
  
    new_messages Boolean := True;
    next_trans EXCEPTION;
    no_messages EXCEPTION;
    pragma exception_init(next_trans, -25235);
    pragma exception_init(no_messages, -25228);
    i integer;
  
    m_vCONTENT      Varchar2(4000);
    m_vQUERY_ID     Number(10);
    m_vRDBR         VARCHAR2(10); --Cong TAD
    m_vTLBF01       VARCHAR2(20);
    m_vRMBENA       VARCHAR2(20);
    m_vRMAMT        VARCHAR2(20);
    m_vRMSNME       VARCHAR2(100);
    m_vRMACNO       VARCHAR2(15);
    m_vTLBAFM       VARCHAR2(2000);
    m_vTLBRMK       VARCHAR2(2);
    m_vTLBF09       VARCHAR2(14);
    m_vTLBID        VARCHAR2(8);
    m_vRMPRDC       VARCHAR2(1);
    m_vRDEFTH       VARCHAR2(250);
    m_vRMPB40       VARCHAR2(100);
    m_vTLBDEL       VARCHAR2(15);
    m_vTLBCOR       VARCHAR2(1);
    m_vRMDIS7       VARCHAR2(8);
    m_vRMSTS7       VARCHAR2(8);
    pRowIBPS        IBPS_MSG_CONTENT%Rowtype;
    vSOURCE_BRANCH  varchar2(5);
    pRM_NUMBER      varchar2(20);
    m_vCCYCD        varchar2(3);
    m_vRDBR_HOST    varchar2(5);
    m_vPRODUCT_TYPE varchar2(5);
    --Cac truong default co the them vao
    m_vDEFAULT1 VARCHAR2(200);
    m_vDEFAULT2 VARCHAR2(200);
    m_vDEFAULT3 VARCHAR2(200);
    m_vDEFAULT4 VARCHAR2(200);
    m_vDEFAULT5 VARCHAR2(200);
  
    vF593 varchar2(50); ---ten
    ----------------------------------------
    vSCMT                varchar2(40); --So CMT
    vNC_NC               varchar2(50); --Ngay cap Noi cap
    CONTENT_PAYMENT_TEMP varchar2(500);
  BEGIN
    DBMS_AQADM.start_queue('IBPS_Q_CONVERTOUT_OL3', TRUE, TRUE);
    queue_options.wait := 1;
    i                  := 0;
    select count(1) into i from IBPS_TB_Q_OL3;
    if i = 0 then
      return;
    end if;
    i := 0;
    While ((new_messages) and i < 1000) LOOP
      Begin
        DBMS_AQ.DEQUEUE(queue_name         => 'IBPS_Q_CONVERTOUT_OL3',
                        dequeue_options    => queue_options,
                        message_properties => message_properties,
                        payload            => my_message,
                        msgid              => message_id);
        queue_options.navigation := DBMS_AQ.NEXT;
      
        select SEQ_IBPS_QUERY.NEXTVAL INTO m_vQUERY_ID From dual;
        m_nErr := 0;
      
        --m_vQUERY_ID  := my_message.QUERY_ID;
        m_vRDBR      := my_message.RDBR; --TAD
        m_vTLBF01    := my_message.TLBF01;
        m_vRMBENA    := replace(my_message.RMBENA, ' ', '');
        m_vRMAMT     := my_message.RMAMT;
        m_vRMSNME    := my_message.RMSNME;
        m_vRMACNO    := my_message.RMACNO;
        m_vTLBAFM    := my_message.TLBAFM;
        m_vTLBRMK    := my_message.TLBRMK;
        m_vTLBF09    := my_message.TLBF09;
        m_vTLBID     := my_message.TLBID;
        m_vRMPRDC    := my_message.RMPRDC;
        m_vRDEFTH    := my_message.RDEFTH;
        m_vRMPB40    := my_message.RMPB40;
        m_vTLBDEL    := my_message.TLBDEL;
        m_vTLBCOR    := my_message.TLBCOR;
        m_vRMDIS7    := my_message.RMDIS7;
        m_vRMSTS7    := my_message.RMSTS7;
        pRM_NUMBER   := m_vRMACNO;
        m_vRDBR_HOST := my_message.RDBR_HOST; --SOURCE_BRANCH
      
        --Phan default
        m_vDEFAULT1 := my_message.DEFAULT1;
        m_vDEFAULT2 := my_message.DEFAULT2;
        m_vDEFAULT3 := my_message.DEFAULT3;
        m_vDEFAULT4 := my_message.DEFAULT4;
        m_vDEFAULT5 := my_message.DEFAULT5;
        --110
        RELATION_NUMBER := GW_PK_IBPS_PROCESS.IBPS_GetCurRef; --GET_RELATION_NUMBER; -- chua lay duoc
        --003
        if m_vTLBF09 = '120101001' then
          TRANS_CODE := '201001';
        end if;
        if m_vTLBF09 = '280601002' then
          TRANS_CODE := '101001';
        end if;
        ---Lay lai truong 007 va 021
        --007
        O_CI_CODE      := GET_TAD(m_vRDBR_HOST, ''); -- GET_BANK_MAP(m_vRDBR);
        vSOURCE_BRANCH := m_vRDBR_HOST; --GW_PK_IBPS_EXCEL_CONVERTOUT.GET_SIBS_BANK_CODE(O_CI_CODE);
        --012
        TRANS_DATE := Rpad(To_char(Sysdate, 'YYYYMMDD'), 14, '0');
        --019
        R_CI_CODE := '01202002'; -- Co the fix duoc, thay doi tuy thuoc ngan hang
        --021
        O_PROXY_CI_CODE := GET_TAD(m_vRDBR, m_vTLBID);
        --022
        PROXY_CI_CODE := '01202002'; -- Ma ngan hang nhan tin dien
        --        kiem tra lai cho lay truong 007 019 021 022 la ok
        if (trim(O_CI_CODE) is null) then
          m_nErr := GW_ERR_SENDER;
        end if;
        if (trim(PROXY_CI_CODE) is null) then
          m_nErr := GW_ERR_RECEIVER;
        end if;
        if (trim(R_CI_CODE) is null) then
          m_nErr := GW_ERR_RECEIVER19;
        end if;
        vSCMT  := trim(substr(m_vTLBAFM, 598, 40));
        vNC_NC := Replace(trim(substr(m_vTLBAFM, 885, 93)), '000000');
        --025
        XXX_025 := '2000';
        --026
        m_vCCYCD := substr(m_vTLBAFM, 228, 3);
        If (GW_PK_IBPS_PROCESS.IBPS_CCYCD_CHECK(m_vCCYCD, 'IBPS') < 1) Then
          m_nErr := GW_ERR_CURRENCY_FALSE;
        End if;
        CURRENCY_CODE := m_vCCYCD;
        --027
        TRANS_AMOUNT := m_vRMAMT;
        --028
        SENDING_CUS_NAME := trim(m_vRMSNME);
        begin
          if length(SENDING_CUS_NAME) > 70 then
            SENDING_CUS_NAME := substr(SENDING_CUS_NAME, 1, 70);
          end if;
        exception
          when others then
            SENDING_CUS_NAME := SENDING_CUS_NAME;
        end;
        -- Cap nhat ngay 12-03-2011 replace ky tu la
        SENDING_CUS_NAME := Gw_Pk_Ibps_Process.IBPS_REPLACE_Char(SENDING_CUS_NAME,
                                                                 'IBPS',
                                                                 'RM',
                                                                 '',
                                                                 '028',
                                                                 'SIBS-IBPS',
                                                                 m_vQUERY_ID,
                                                                 'IBPS_OL3_Convert_out');
        -- het cap nhat
      
        --029
        SENDING_CUS_ADDR := '';
        --030
        SENDING_CUS_ACCOUNT := '5199';
        --031   
        RECEIVING_CUS_NAME := trim(substr(m_vTLBAFM, 558, 40));
        begin
          vF593 := trim(substr(M_vTLBAFM, 978, 40));
        exception
          when others then
            vF593 := ' ';
        end;
        begin
          RECEIVING_CUS_NAME := RECEIVING_CUS_NAME || ' ' || vF593;
        exception
          when others then
            RECEIVING_CUS_NAME := RECEIVING_CUS_NAME;
        end;
        -- Cap nhat ngay 12-03-2011 replace ky tu la
        RECEIVING_CUS_NAME := Gw_Pk_Ibps_Process.IBPS_REPLACE_Char(RECEIVING_CUS_NAME,
                                                                   'IBPS',
                                                                   'RM',
                                                                   '',
                                                                   '031',
                                                                   'SIBS-IBPS',
                                                                   m_vQUERY_ID,
                                                                   'IBPS_OL3_Convert_out');
        -- Het cap nhat
        --Do dai 031
        -- Comment lai ngay 25-2-2011 
        /*  begin
          if length(RECEIVING_CUS_NAME) > 70 then
            RECEIVING_CUS_NAME := substr(RECEIVING_CUS_NAME, 1, 70);
          end if;
        exception
          when others then
            RECEIVING_CUS_NAME := RECEIVING_CUS_NAME;
        end;*/
        -- Het comment
        --032
        RECEIVING_CUS_ADDR := '';
        --033
        RECEIVING_CUS_ACCOUNT := m_vRMBENA;
        --034
        CONTENT_PAYMENT_INSTRUCTION := m_vRDEFTH || ' NH HUONG: ' ||
                                       m_vRMPB40 || ' (' || vSCMT || ' ' ||
                                       vNC_NC || ')';
        -- Cap nhat ngay 12-03-2011 replace ky tu la
        CONTENT_PAYMENT_INSTRUCTION := Gw_Pk_Ibps_Process.IBPS_REPLACE_Char(CONTENT_PAYMENT_INSTRUCTION,
                                                                            'IBPS',
                                                                            'RM',
                                                                            '',
                                                                            '034',
                                                                            'SIBS-IBPS',
                                                                            m_vQUERY_ID,
                                                                            'IBPS_OL3_Convert_out');
        -- Het cap nhat
      
        CONTENT_PAYMENT_TEMP := CONTENT_PAYMENT_INSTRUCTION;
        --036            
        OPRERATION1 := '30';
        --037
        OPRERATION2     := '100';
        m_vPRODUCT_TYPE := substr(m_vTLBAFM, 40, 3);
         -- 20141104: QuanLD: Sua lai so but toan tu 6 so sang 8 so
        m_vCONTENT      := '#110' || lpad(RELATION_NUMBER, 8, '0') ||
                           '#003' || TRANS_CODE || '#007' || O_CI_CODE ||
                           '#012' || TRANS_DATE || '#019' || R_CI_CODE ||
                           '#021' || O_PROXY_CI_CODE || '#022' ||
                           PROXY_CI_CODE || '#025' || XXX_025 || '#026' ||
                           CURRENCY_CODE || '#027' || TRANS_AMOUNT * 100 ||
                           '#028' || SENDING_CUS_NAME || '#029' ||
                           SENDING_CUS_ADDR || '#030' ||
                           SENDING_CUS_ACCOUNT || '#031' ||
                           Trim(RECEIVING_CUS_NAME) || '#032' ||
                           RECEIVING_CUS_ADDR || '#033' ||
                           RECEIVING_CUS_ACCOUNT || '#034' ||
                           CONTENT_PAYMENT_INSTRUCTION || '#036' ||
                           OPRERATION1 || '#037' || OPRERATION2;
        --Lay du lieu insert vao bang content
        pRowIBPS.Query_Id          := m_vQUERY_ID;
        pRowIBPS.File_Name         := '';
        pRowIBPS.Msg_Direction     := m_Direction;
        pRowIBPS.Trans_Code        := TRANS_CODE;
        pRowIBPS.Gw_Trans_Num      := RELATION_NUMBER; --xac dinh gia tri cao thap
        pRowIBPS.Sibs_Trans_Num    := '1'; --chua lay duoc truong nay
        pRowIBPS.F07               := O_CI_CODE;
        pRowIBPS.F19               := R_CI_CODE;
        pRowIBPS.F21               := O_PROXY_CI_CODE;
        pRowIBPS.F22               := PROXY_CI_CODE;
        pRowIBPS.Trans_Date        := sysdate;
        pRowIBPS.Transdate         := to_char(pRowIBPS.Trans_Date,
                                              'YYYYMMDD');
        pRowIBPS.Amount            := TRANS_AMOUNT;
        pRowIBPS.Ccycd             := CURRENCY_CODE;
        pRowIBPS.Msg_Src           := 1;
        pRowIBPS.Trans_Description := CONTENT_PAYMENT_TEMP;
      
        --pRowIBPS.Trans_Description := '';
        pRowIBPS.Department    := m_Department;
        pRowIBPS.Content       := m_vCONTENT;
        pRowIBPS.Source_Branch := vSOURCE_BRANCH;
        pRowIBPS.Tad           := GETTAD(O_PROXY_CI_CODE);
        pRowIBPS.Product_Type  := m_vPRODUCT_TYPE; --m_vTLBID
        pRowIBPS.Sibs_Tellerid := m_vTLBID;
        /* if O_PROXY_CI_CODE = '79302001' then
          pRowIBPS.Tad := '00040';
        elsif O_PROXY_CI_CODE = '01302001' then
          pRowIBPS.Tad := '00011';
        else
          pRowIBPS.Tad := lpad(m_vRDBR, 5, 0);
        end if;*/
        pRowIBPS.Pre_Tad        := '';
        pRowIBPS.Rm_Number      := pRM_NUMBER;
        pRowIBPS.Receiving_Time := sysdate;
      
        if NVL(m_nErr, 0) = 0 then
          IF not (GW_PK_IBPS_PROCESS.IBPS_MSG_DUP_CHECK(2, m_vRMACNO) > 0) then
            m_nErr := GW_ERR_RMNUMBER; --Trung so tien
          end if;
          --Neu khong bi trung
          if NVL(m_nErr, 0) = 0 then
            begin
              if length(RECEIVING_CUS_NAME) > 70 then
                m_nErr := GW_ERR_F31_LONG; --Truong 34 dai
                /* RECEIVING_CUS_NAME := substr(RECEIVING_CUS_NAME, 1, 70);*/
              end if;
            exception
              when others then
                RECEIVING_CUS_NAME := RECEIVING_CUS_NAME;
            end;
          
            if length(trim(CONTENT_PAYMENT_INSTRUCTION)) > 210 then
              if (m_nErr = GW_ERR_F31_LONG) then
                m_nErr := GW_ERR_F31_34_LONG;
              else
                m_nErr := GW_ERR_LONG; --Truong 34 dai
              end if;
            end if;
          
            /* if length(trim(CONTENT_PAYMENT_INSTRUCTION)) > 210 then
              m_nErr := GW_ERR_LONG; --Truong 34 dai
            end if;*/
          end if;
        end if;
        pRowIBPS.Err_Code := NVL(m_nErr, 0);
        if NVL(m_nErr, 0) > 0 then
          pRowIBPS.Status := -1;
          IBPS_OL3_Update(m_vRMACNO, -1, 'OL3');
        else
          pRowIBPS.Status := 0;
          IBPS_OL3_Update(m_vRMACNO, 1, 'OL3');
        end if;
      
        GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                               'IBPS_OL3_JOB_CONVERTOUT',
                                               m_Direction);
      
        --Ghi log
        m_IBPS_Type.Log_Date := sysdate;
        m_IBPS_Type.Query_Id := m_vQUERY_ID;
        m_IBPS_Type.Status   := 0;
        if NVL(m_nErr, 0) > 0 then
          m_IBPS_Type.Descriptions := 'Convert Message error:' || SQLCODE ||
                                      ' -ERROR- ' || SQLERRM;
        else
          m_IBPS_Type.Descriptions := 'Convert Message success:' || SQLCODE ||
                                      ' -ERROR- ' || SQLERRM;
        end if;
      
        m_IBPS_Type.Area          := ' ';
        m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_CONVERTOUT';
        m_IBPS_Type.Msg_Direction := m_Direction;
        GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      
        /*if length(trim(CONTENT_PAYMENT_INSTRUCTION)) > 210 then
          pRowIBPS.Err_Code := 1;
          pRowIBPS.Status   := -1;
        end if;*/
        --Update du lieu
        /*if (NVL(m_nErr, 0) >= 0) then
          GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                                 'IBPS_OL3_JOB_CONVERTOUT',
                                                 m_Direction);
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := m_vQUERY_ID;
          m_IBPS_Type.Status        := 0;
          m_IBPS_Type.Descriptions  := 'Convert Message success:' ||
                                       SQLCODE || ' -ERROR- ' || SQLERRM;
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
        end if;
        IF (m_nErr = 13) or (m_nErr = 14) or (m_nErr = 15) then
          pRowIBPS.Err_Code := 13;
          pRowIBPS.Status   := -1;
          GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                                 'IBPS_Job_CONVERT_OUT',
                                                 m_Direction);
          m_IBPS_Type.Log_Date := sysdate;
          m_IBPS_Type.Query_Id := m_vQUERY_ID;
          m_IBPS_Type.Status   := -1;
          case
            when m_nErr = 13 then
              m_IBPS_Type.Descriptions := 'Field 007 error';
            when m_nErr = 14 then
              m_IBPS_Type.Descriptions := 'Field 019 error';
            when m_nErr = 15 then
              m_IBPS_Type.Descriptions := 'Field 022 error';
            
          end case;
        
          m_IBPS_Type.Area          := '';
          m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          IBPS_OL3_Update(m_vRMACNO, -1, 'OL3');
        end if;*/
        /* DELETE From IBPS_INDEX
         Where IBPS_INDEX.RM_NUMBER = pRM_NUMBER
           AND IBPS_INDEX.DIRECTION = 'SIBS-IBPS';
        IBPS_OL3_Update(m_vRMACNO, -1, 'OL3');*/
        --end if;
      
      EXCEPTION
        WHEN next_trans THEN
          queue_options.navigation := DBMS_AQ.NEXT_TRANSACTION;
        WHEN no_messages THEN
          new_messages := FALSE;
        When others then
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := m_vQUERY_ID;
          m_IBPS_Type.Status        := -1;
          m_IBPS_Type.Descriptions  := 'Error When Dequeue content Message File:' ||
                                       SQLCODE || ' -ERROR- ' || SQLERRM;
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
           GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          IBPS_OL3_Update(m_vRMACNO, -1, 'OL3');
      END;
      i := i + 1;
    
    end loop;
    commit;
  Exception
    when OTHERS THEN
      --Rollback;
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := m_vQUERY_ID;
      m_IBPS_Type.Status        := -1;
      m_IBPS_Type.Descriptions  := 'Error When Dequeue content Message File:' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_OL3_JOB_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
      IBPS_OL3_Update(m_vRMACNO, -1, 'OL3');
    
  END IBPS_DE_CONVERTOUT;

  FUNCTION GET_BANK_MAP(pSIBS_BANK_CODE varchar2) return varchar2 is
  
    vReturn varchar2(8);
    vsql    varchar2(200);
  begin
    select IBM.GW_BANK_CODE
      into vReturn
      from IBPS_BANK_MAP IBM
     where IBM.SIBS_BANK_CODE = Lpad(pSIBS_BANK_CODE, 5, '0')
       and rownum = 1;
    return vReturn;
  exception
    when others then
      vsql := SQLERRM;
      vsql := SQLERRM;
      return '01302001';
  end;

  FUNCTION GET_RELATION_NUMBER return varchar2 is
    nSIBSNum integer;
    RefNum   varchar2(20) := '0';
    vDate    varchar2(8);
  
  BEGIN
    Begin
    
      SELECT Value, Note
        Into RefNum, vDate
        FROM SYSVAR
       WHERE VARNAME = 'IBPSSeqNum_Ol3'
         and GWTYPE = 'IBPS';
    
      if (vDate = to_char(sysdate, 'YYYYMMDD')) then
        nSIBSNum := to_number(RefNum) + 1;
      else
        nSIBSNum := 1000;
        vDate    := to_char(sysdate, 'YYYYMMDD');
      end if;
    
      Update Sysvar
         set Value = nSIBSNum, Note = vDate
       where VARNAME = 'IBPSSeqNum_Ol3'
         and GWTYPE = 'IBPS';
      commit;
    exception
      when others then
        nSIBSNum := 1000;
        vDate    := to_char(sysdate, 'YYYYMMDD');
        Update Sysvar
           set Value = nSIBSNum, Note = vDate
         where VARNAME = 'IBPSSeqNum_Ol3'
           and GWTYPE = 'IBPS';
        commit;
    end;
    return LPAD(nSIBSNum, 8, '0');
  
  exception
    when others then
      return '';
  end;

  FUNCTION GET_TAD(pvSIBS_BANK_CODE varchar2, pvTLBID varchar2)
    Return varchar2 is
  
    vReturn varchar2(8);
  begin
    if pvTLBID is null then
      --Lay TAD khong phai cua HSC    
      select GW_BANK_CODE
        into vReturn
        from IBPS_BANK_MAP
       where lpad(SIBS_BANK_CODE, 5, '0') = lpad(pvSIBS_BANK_CODE, 5, '0')
         and rownum = 1;
    else
      if lpad(pvSIBS_BANK_CODE, 5, '0') = '00011' then
        --HSC
        select GW_BANK_CODE
          into vReturn
          from IBPS_BANK_MAP IB
         where lpad(IB.Sibs_Bank_Code, 5, '0') =
               lpad(pvSIBS_BANK_CODE, 5, '0')
           and SHORT_NAME = substr(pvTLBID, 1, 6)
           and rownum = 1;
      else
        select GW_BANK_CODE
          into vReturn
          from IBPS_BANK_MAP IB
         where lpad(IB.Sibs_Bank_Code, 5, '0') =
               lpad(pvSIBS_BANK_CODE, 5, '0')
           and rownum = 1;
      end if;
    end if;
    return vReturn;
  exception
    when others then
      return '01302001';
    
  end GET_TAD;

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

end GW_PK_OL3_CONVERTOUT;
/
