create or replace package GW_PK_IBPS_TR_CONVERTOUT is

  -- Author  : QuanLD
  -- Created : 5/26/2008 7:34:52 PM
  -- Purpose : Dinh nghia cac ham xu ly dien khi lay dien ra tu bang IBPS_TR

  -- Public type declarations

  PROCEDURE IBPS_TR_EN_CONVERTOUT(pMSG_ID         Number,
                                  pMSG_DIRECTION  VARCHAR2,
                                  pTRANS_CODE     VARCHAR2,
                                  pIBPS_TRANS_NUM VARCHAR2,
                                  pSIBS_TRANS_NUM VARCHAR2,
                                  pSENDER         VARCHAR2,
                                  pRECEIVER       VARCHAR2,
                                  pTRANS_DATE     DATE,
                                  pAMOUNT         NUMBER,
                                  pCCY            VARCHAR2,
                                  pDESCRIPTION    VARCHAR2,
                                  pSTATUS         NUMBER,
                                  pCONTENT        clob);

  PROCEDURE IBPS_TR_GET_MSG_OUT;

  PROCEDURE IBPS_TR_DE_CONVERTOUT;

  PROCEDURE IBPS_Msg_UpdateTR(pMSG_ID NUMBER, pSTATUS NUMBER);

  FUNCTION IBPS_TR_CONVERTOUT(pvSENDER   Varchar2,
                              pvReceiver varchar2,
                              pvContent  clob,
                              pnQuery_ID Number) RETURN VARCHAR2;
  FUNCTION GET_IBPS_Field(pCOntent varchar2, FiledCode varchar2)
    Return Varchar2;

  FUNCTION IBPS_GetCurRef RETURN NUMBER;
  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2);

end GW_PK_IBPS_TR_CONVERTOUT;
/
create or replace package body GW_PK_IBPS_TR_CONVERTOUT is

  m_Direction     varchar2(10) := 'SIBS-IBPS';
  
  -- QuanLD: Update laij so but toan do dai =8
  pIBPS_TRANS_NUM VARCHAR2(8);
  m_IBPS_Type     IBPS_MSG_LOG%rowtype;
  -- Cac bien chung cua Packet
  --  Lay ma Chi nhanh di chuyen dien #021
  m_pvTad    varchar2(12);
  m_Err      number(1);
  m_AMOUNT   number(20);
  m_query_ID number(20);
  --
  -- Gis tri tra ve: Day du lieu vao 1 bang table queue

  -- Ngay tao:23/05/2008

  Procedure IBPS_TR_EN_CONVERTOUT(pMSG_ID         Number,
                                  pMSG_DIRECTION  VARCHAR2,
                                  pTRANS_CODE     VARCHAR2,
                                  pIBPS_TRANS_NUM VARCHAR2,
                                  pSIBS_TRANS_NUM VARCHAR2,
                                  pSENDER         VARCHAR2,
                                  pRECEIVER       VARCHAR2,
                                  pTRANS_DATE     DATE,
                                  pAMOUNT         NUMBER,
                                  pCCY            VARCHAR2,
                                  pDESCRIPTION    VARCHAR2,
                                  pSTATUS         NUMBER,
                                  pCONTENT        clob) IS
  
    queue_options      DBMS_AQ.ENQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(16);
    my_message         ibps_tr_type_convert_out;
  BEGIN
  
    --start cai queue len
    DBMS_AQADM.start_queue('IBPS_TR_Q_CONVERTOUT', TRUE, TRUE);
  
    m_IBPS_Type.Log_Date     := sysdate;
    m_IBPS_Type.Query_Id     := pMSG_ID;
    m_IBPS_Type.Status       := 1;
    m_IBPS_Type.Descriptions := 'Start queue, Put Message into queue IBPS_TR_EN_CONVERTOUT';
  
    m_IBPS_Type.Area          := ' ';
    m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_INQUIRYOUT';
    m_IBPS_Type.Msg_Direction := m_Direction;
    GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
  
    my_message := ibps_tr_type_convert_out(pMSG_ID,
                                           pMSG_DIRECTION,
                                           pTRANS_CODE,
                                           pIBPS_TRANS_NUM,
                                           pSIBS_TRANS_NUM,
                                           pSENDER,
                                           pRECEIVER,
                                           pTRANS_DATE,
                                           pAMOUNT,
                                           pCCY,
                                           pDESCRIPTION,
                                           pSTATUS,
                                           To_char(pCONTENT));
  
    DBMS_AQ.ENQUEUE(queue_name         => 'IBPS_TR_Q_CONVERTOUT',
                    enqueue_options    => queue_options,
                    message_properties => message_properties,
                    payload            => my_message,
                    msgid              => message_id);
    COMMIT;
  
  Exception
    when OTHERS THEN
      --Rollback;
    
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pMSG_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error when enqueue IBPS_TR_CONVERT_OUT' ||
                                   SQLCODE;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_INQUIRYOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    
  END IBPS_TR_EN_CONVERTOUT;

  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: Loai bo cac ky tu la the tung field tung loai
  Ten ham:  IBPS_TR_GET_MSG_OUT()
  Tham so:
  Mo ta: Ham nay xac laays noi dung tat ca cac dien trong bang IBPS_TR voi trang thia dien Status=0
  
  Ngay khoi tao:  31/05/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  **********************************************************************/

  Procedure IBPS_TR_GET_MSG_OUT IS
    cursor curMSG_OUT is
      SELECT MSG_ID,
             MSG_DIRECTION,
             TRANS_CODE,
             IBPS_TRANS_NUM,
             SIBS_TRANS_NUM,
             SENDER,
             RECEIVER,
             TRANS_DATE,
             AMOUNT,
             CCY,
             DESCRIPTION,
             STATUS,
             decode(IS_LNH,
                    'Y',
                    CONTENT || '##099#666' || LNH_TRANS_TYPE || '#667' ||
                    LNH_INTEREST || '#668' || SMO.LNH_CYCLE || '#669' ||
                    SMO.LNH_GTCG || '#670' ||
                    to_char(SMO.LNH_TRANSDATE,'ddMMyyyy'),
                    CONTENT) CONTENT
        FROM IBPS_TR SMO
       Where SMO.Status = 0;
  
    v_MSG_OUT   curMSG_OUT%rowtype;
    v_non_sales integer;
    pi_Dup      INTEGER;
  
  BEGIN
  
    OPEN curMSG_OUT;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly
      FETCH curMSG_OUT
        INTO v_MSG_OUT;
    
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u
      EXIT WHEN curMSG_OUT %notfound;
      --Kiem tra dien trung
      pi_Dup := GW_PK_IBPS_PROCESS.IBPS_MSG_DUP_CHECK(2,
                                                      v_MSG_OUT.Sibs_Trans_Num);
    
      -- Xac dinh dien co trung hay khong
      if (pi_Dup = 1) Then
        -- dien khong trung
        Begin
          --Day dien vao hang doi.
          IBPS_TR_EN_CONVERTOUT(v_MSG_OUT.Msg_Id,
                                v_MSG_OUT.Msg_Direction,
                                v_MSG_OUT.Trans_Code,
                                v_MSG_OUT.Ibps_Trans_Num,
                                v_MSG_OUT.Sibs_Trans_Num,
                                v_MSG_OUT.Sender,
                                v_MSG_OUT.Receiver,
                                v_MSG_OUT.Trans_Date,
                                v_MSG_OUT.Amount,
                                v_MSG_OUT.Ccy,
                                v_MSG_OUT.Description,
                                v_MSG_OUT.Status,
                                v_MSG_OUT.Content);
        
          -- Cap nhat trang thais dien tai bang IBPS_TR truong Status=1
          IBPS_Msg_UpdateTR(v_MSG_OUT.Msg_Id, 2);
        
        end;
      
      else
        --dien trung
        -- Cap nhat trang thais dien tai bang IBPS_TR truong Status=-2
        IBPS_Msg_UpdateTR(v_MSG_OUT.Msg_Id, -2);
      
      end if;
    
      -- Tang bien dem len 1 gia tri
      v_non_sales := v_non_sales + 1;
    
    END LOOP;
    -- ?ong cursor
    CLOSE curMSG_OUT;
  
  END IBPS_TR_GET_MSG_OUT;

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
  PROCEDURE IBPS_TR_DE_CONVERTOUT IS
  
    queue_options      DBMS_AQ.DEQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(2000);
    my_message         ibps_tr_type_convert_out;
  
    new_messages Boolean := True;
    next_trans  EXCEPTION;
    no_messages EXCEPTION;
    pragma exception_init(next_trans, -25235);
    pragma exception_init(no_messages, -25228);
    i integer;
    -- Dinh nghia cac bien lay du lieu ra tu queue
  
    pMSG_ID        Number(19);
    pMSG_DIRECTION VARCHAR2(10);
    pTRANS_CODE    VARCHAR2(10);
  
    pSIBS_TRANS_NUM VARCHAR2(16);
    pSENDER         VARCHAR2(12);
    pRECEIVER       VARCHAR2(12);
    pTRANS_DATE     DATE;
    pAMOUNT         NUMBER(19, 2);
    pCCY            VARCHAR2(4);
    pDESCRIPTION    VARCHAR2(210);
    pSTATUS         NUMBER(3);
    pCONTENT        Varchar2(4000);
    m_SourceBranch  varchar(12);
    pnTbqCount      Number(10);
    pIbpsContent    Varchar2(4000);
    pRowIBPS        IBPS_MSG_CONTENT%Rowtype;
  BEGIN
  
    DBMS_AQADM.start_queue('IBPS_TR_Q_CONVERTOUT', TRUE, TRUE); -- start queue
  
    queue_options.wait := 1;
    i                  := 0;
    While ((new_messages) and i < 500) LOOP
      Begin
        select Count(*) into pnTbqCount From IBPS_TR_TB_Q_CONVERTOUT;
        if (pnTbqCount = 0) then
          Return;
        End if;
        BEGIN
          DBMS_AQ.DEQUEUE(queue_name         => 'IBPS_TR_Q_CONVERTOUT',
                          dequeue_options    => queue_options,
                          message_properties => message_properties,
                          payload            => my_message,
                          msgid              => message_id);
          queue_options.navigation := DBMS_AQ.NEXT;
        
          select Seq_Ibps_Query.Nextval into m_query_ID from dual;
        
          pMSG_ID         := my_message.MSG_ID;
          pMSG_DIRECTION  := my_message.MSG_DIRECTION;
          pTRANS_CODE     := my_message.TRANS_CODE;
          pIBPS_TRANS_NUM := my_message.IBPS_TRANS_NUM;
          pSIBS_TRANS_NUM := my_message.SIBS_TRANS_NUM;
          pSENDER         := my_message.SENDER;
          pRECEIVER       := my_message.RECEIVER;
          pTRANS_DATE     := my_message.TRANS_DATE;
          pAMOUNT         := my_message.AMOUNT;
          m_AMOUNT        := pAMOUNT;
          pCCY            := my_message.CCY;
          pDESCRIPTION    := my_message.DESCRIPTION;
          pSTATUS         := my_message.STATUS;
          pCONTENT        := my_message.CONTENT;
        
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := m_query_ID;
          m_IBPS_Type.Status        := 1;
          m_IBPS_Type.Descriptions  := 'Start Dequeue, Get Message From queue IBPS_TR_EN_CONVERTOUT';
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
        
          -- Dua cac ham su ly dien, convert dien,
          --Convert dien tu IBPS_TR sang IBPS
          --if pIBPS_TRANS_NUM is null then
          
           -- 20141104: QuanLD: Sua lai so but toan tu 6 so sang 8 so
          pIBPS_TRANS_NUM := '09' || Lpad(IBPS_GetCurRef, 6, '0');
          -- end if;
          pIbpsContent := IBPS_TR_CONVERTOUT(pSENDER,
                                             pRECEIVER,
                                             pCONTENT,
                                             pMSG_ID);
          -- Lay ma transaction code
          -- cai nay phai xem
          pTRANS_CODE := NVL(GW_PK_IBPS_PROCESS.IBPS_TRGetTransCode(pSENDER,
                                                                    pAMOUNT,
                                                                    2),
                             pTRANS_CODE);
        
          -- Lay ma 3 so hoi so chinh
          Begin
          
            select sysvar.value
              into m_SourceBranch
              from sysvar
             where upper(Sysvar.Gwtype) = 'SYSTEM'
               and upper(varname) = 'SENDER_BRAN_TR';
          
          Exception
            When others Then
              m_SourceBranch := '00110';
          END;
          m_pvTad := gw_pk_ibps_q_convertin.GET_IBPS_Field(pCONTENT, '021');
          m_pvTad := GW_PK_IBPS_PROCESS.GETBANKCODE(m_pvTad, 1);
        
          pRowIBPS.Query_Id       := m_query_ID;
          pRowIBPS.File_Name      := '';
          pRowIBPS.Msg_Direction  := 'SIBS-IBPS';
          pRowIBPS.Trans_Code     := pTRANS_CODE;
          pRowIBPS.Gw_Trans_Num   := pIBPS_TRANS_NUM;
          pRowIBPS.Sibs_Trans_Num := pSIBS_TRANS_NUM;
          pRowIBPS.F07            := GET_IBPS_Field(pIbpsContent, '007');
          pRowIBPS.F19            := GET_IBPS_Field(pIbpsContent, '019');
          pRowIBPS.F21            := GET_IBPS_Field(pIbpsContent, '021');
          pRowIBPS.F22            := GET_IBPS_Field(pIbpsContent, '022');
          pRowIBPS.Trans_Date     := sysdate;
          pRowIBPS.Transdate      := to_char(pRowIBPS.Trans_Date,
                                             'YYYYMMDD');
          pRowIBPS.Amount         := pAMOUNT / 100;
          pRowIBPS.Ccycd          := pCCY;
          if (NVL(m_Err, 0) > 0) Then
            pRowIBPS.Status := -1;
          else
            pRowIBPS.Status := 0;
          end if;
        
          pRowIBPS.Err_Code          := NVL(m_Err, 0);
          pRowIBPS.Trans_Description := pDESCRIPTION;
          pRowIBPS.Department        := 'TR';
          pRowIBPS.Content           := pIbpsContent;
          pRowIBPS.Source_Branch     := m_SourceBranch;
          pRowIBPS.Tad               := m_pvTad;
          pRowIBPS.Pre_Tad           := null;
          pRowIBPS.Rm_Number         := pSIBS_TRANS_NUM;
          pRowIBPS.Receiving_Time    := sysdate;
          --  pIbpsContent := To_char(pCONTENT);
          -- Xem lai cai nay
          if m_Err = 0 or m_Err is null then
            GW_PK_IBPS_PROCESS.IBPS_Update_Content(pRowIBPS,
                                                   'IBPS_TR_JOB_CONVERTOUT',
                                                   m_Direction);
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := m_query_ID;
            m_IBPS_Type.Status        := 1;
            m_IBPS_Type.Descriptions  := 'Convert success';
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_CONVERTOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          
          end if;
        EXCEPTION
          WHEN Others THEN
          
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := m_query_ID;
            m_IBPS_Type.Status        := 0;
            m_IBPS_Type.Descriptions  := 'ERROR,When get Message from queue IBPS_TR_EN_CONVERTOUT' ||
                                         SQLERRM;
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_CONVERTOUT';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
            --ghi log dien
          -- insert dien vao day
        
        END;
      EXCEPTION
        WHEN next_trans THEN
          queue_options.navigation := DBMS_AQ.NEXT_TRANSACTION;
        WHEN no_messages THEN
          new_messages              := FALSE;
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := m_query_ID;
          m_IBPS_Type.Status        := 0;
          m_IBPS_Type.Descriptions  := 'ERROR,When get Message from queue IBPS_TR_EN_CONVERTOUT' ||
                                       SQLERRM;
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_CONVERTOUT';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
        
      END;
      i := i + 1;
    end loop;
  
    commit;
  
  Exception
    when OTHERS THEN
      -- Rollback;
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := pMSG_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'ERROR,When get Message from queue IBPS_TR_EN_CONVERTOUT' ||
                                   SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_TR_Job_CONVERTOUT';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    
  END IBPS_TR_DE_CONVERTOUT;

  /**********************************************************************
  Nguoi tao:  HaNTT10
  Muc dich: Cap nhat trang thai dien cho bang IBPS_TR
  Ten ham:  IBPS_Msg_UpdateTR()
  Tham so:  pMSG_ID NUMBER, pSTATUS NUMBER
  Mo ta: Cap nhat lai tra trang thai cho bang IBPS_TR
  Ngay khoi tao:  31/05/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  **********************************************************************/

  Procedure IBPS_Msg_UpdateTR(pMSG_ID NUMBER, pSTATUS NUMBER) is
  Begin
    Update IBPS_TR set STATUS = pSTATUS Where MSG_ID = pMSG_ID;
    COMMIT;
  Exception
    when OTHERS THEN
      Rollback;
  end IBPS_Msg_UpdateTR;

  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: Convert dien tu IBPS_TR sang IBPS
  Ten ham:  IBPS_TR_CONVERTOUT()
  Tham so:
  Mo ta:
  
  Ngay khoi tao:  31/05/2008
  Nguoi sua:
  Ngay sua:
  Mo ta
  Version:    v1.0
  **********************************************************************/

  FUNCTION IBPS_TR_CONVERTOUT(pvSENDER   Varchar2,
                              pvReceiver varchar2,
                              pvContent  Clob,
                              pnQuery_ID Number) RETURN VARCHAR2 IS
  
    pvBranchSend021 varchar2(12);
    piPos           Integer;
    pvReturnContent varchar2(2000);
    pvTemp          varchar2(20);
    pvRefnum        varchar2(20);
    i               boolean;
    pvBranchSender  varchar2(12);
  
  BEGIN
    pvBranchSender  := pvBranchSender;
    pvReturnContent := pvContent;
    -- lay ma chi nhanh truot cong
    /*  pvBranchSend021 := GW_PK_IBPS_PROCESS.IBPS_GETOPROXYCI(pvBranchSender,
    pvReceiver,
    pnQuery_ID,
    m_AMOUNT,
    i);*/
    /*
    m_pvTad         := pvBranchSend021;
    pvBranchSend021 := '#021' || pvBranchSend021;
    piPos           := inStr(pvContent, '#021');
    
    if (piPos = 0) then
      piPos := inStr(pvContent, '#026');
      if (pipos > 0) Then
        pvTemp := substr(pvContent, pipos, 12);
      
        pvReturnContent := REPLACE(pvContent, pvTemp, pvBranchSend021);
      end if;
    else
    
      pvTemp          := substr(pvContent, pipos, 12);
      pvReturnContent := REPLACE(pvContent, pvTemp, pvBranchSend021);
    
    End if;*/
  
    pvRefnum := LPAD(pIBPS_TRANS_NUM, 8, '0');
    pvRefnum := '#110' || pvRefnum;
  
    pvReturnContent := pvRefnum || substr(pvReturnContent, 11);
  
    Return pvReturnContent;
  
  EXCEPTION
    WHEN Others THEN
      m_Err := 1;
      REturn pvContent;
    
  END IBPS_TR_CONVERTOUT;

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
      return v_Value;
    else
      return '';
    end if;
  
  Exception
    when others then
      Return '';
  END GET_IBPS_Field;

  FUNCTION IBPS_GetCurRef Return NUMBER IS
    pinRef    Number(5);
    pvCurDate varchar2(8);
  Begin
    -- lay so RM ra theo ngay
    BEGIN
      SELECT SYSVAR.VALUE, SYSVAR.NOTE
        INTO pinRef, pvCurDate
        FROM SYSVAR
       WHERE SYSVAR.VARNAME = 'IBPSTRSeqNum'
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
             'IBPSTRSeqNum',
             '1',
             TO_CHAR(SYSDATE, 'YYYYMMDD'),
             'STRING ');
        EXCEPTION
          WHEN Others Then
            Return '1';
        END;
        Return '1';
    END;
  
    Return pinRef;
  end IBPS_GetCurRef;

  PROCEDURE IPBS_UPDATESYSVAR(pinRef Varchar2, pvCurDate Varchar2) IS
  BEGIN
    UPDATE SYSVAR
       SET VALUE = pinRef, SYSVAR.NOTE = pvCurDate
     WHERE VARNAME = 'IBPSTRSeqNum'
       AND GWTYPE = 'IBPS';
    commit;
  EXCEPTION
    WHEN OTHERS THEN
      Dbms_Output.put_line(SQLCODE);
      Return;
  END IPBS_UPDATESYSVAR;

end GW_PK_IBPS_TR_CONVERTOUT;
/
