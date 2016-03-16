create or replace package GW_PK_IBPS_Q_CONVERTIN is

  -- Author  : QuanLD
  -- Created : 5/26/2008 4:57:05 PM
  -- Purpose : Dinh nghia cac ham xu ly dien cho Queue IBPS_CONVERT_IN

  PROCEDURE IBPS_EN_CONVERTIN(pFILE_NAME      varchar2,
                              pMSG_TYPE       varchar2,
                              pSIBS_TRANS_NUM varchar2,
                              pIBPS_CONTENT   varchar2,
                              pCONTENT_TAD    varchar2,
                              pSYSTEMDATE     DATE);

  PROCEDURE IBPS_DE_CONVERTIN;

  Procedure IBPS_UPDATE_SIBS_IN(pRowConvertIN IBPS_SIBS_MSG_IN%Rowtype);

  FUNCTION CONVERT_IBPS_SIBS(my_message ibps_type_convertin) REturn Varchar2;

  FUNCTION IBPS_CONVERTFIELD(pFILE_NAME     varchar2,
                             pDATATYPE      NUMBER,
                             pCONTENT       VARCHAR2,
                             pFILECODE      VARCHAR2,
                             pSIBSFieldCode Varchar2,
                             pLENGHT        NUMBER) RETURN VARCHAR2;

  FUNCTION IBPS_GET_ScenaryNum(pGWTYPE Varchar2, pDepartment varchar2)
    RETURN VARCHAR2;

  FUNCTION IBPS_GET_FILECONTENT(pFilecode Varchar2, pMSGContent Varchar2)
    RETURN VARCHAR2;

  FUNCTION IBPS_GET_MSGHEADLen RETURN Number;

  FUNCTION IBPS_Set_Receipt_Branch(pBranCode Varchar2,
                                   iSIBSCode number default 1)
    RETURN VARCHAR2;

  FUNCTION PUMPIN(pFileContent varchar2,
                  pGWPos       integer,
                  pLength      Integer,
                  pDataType    Integer,
                  pMSGContent  varchar2) return Varchar2;
  FUNCTION GET_IBPS_Field(pCOntent varchar2, FiledCode varchar2)
    Return Varchar2;

  FUNCTION GET_RMNUMBER(pbankcode varchar2) return varchar2;
  Function Check_Branch(pBrach_code varchar2) return boolean;
  Function Check_BIDVBranch(pBrach_code varchar2) return boolean;
  Function DefineAccType(szInput varchar2) return varchar2;
end GW_PK_IBPS_Q_CONVERTIN;
/
create or replace package body gw_pk_ibps_q_convertin is

  -- cac bien luu lai gia tri cua type input IBPS
  pIBPS_CONTENT   varchar2(2000);
  pFILE_NAME      varchar2(50);
  pMSG_TYPE       varchar2(10);
  pSIBS_TRANS_NUM varchar2(12);

  m_IBPS_Type IBPS_MSG_LOG%rowtype;
  --cap nhat theo CITAD moi chyuen gia tri nay tu 6--8
  -- Het cap nhat theo CITAD moi chyuen gia tri nay tu 6--8
  GW_BANK_CODE_LEN        Number(3) := 8;
  m_vGW_MSGID_CODE        varchar2(3) := '003';
  m_vGW_RELNUM_CODE       varchar2(3) := '110';
  m_vGW_SENDING_BANK_CODE varchar2(3) := '007';

  m_vGW_CREATION_DATE_CODE      varchar2(3) := '012';
  m_vGW_TRANS_AMOUNT_CODE       varchar2(3) := '027';
  m_vGW_CURRENCY_CODE           varchar2(3) := '026';
  m_vGW_SENDING_ADD_CODE        varchar2(3) := '029';
  m_vGW_ACC_SENDING_CODE        varchar2(3) := '030';
  m_vGW_RECEIVING_ADD_CODE      varchar2(3) := '032';
  m_vGW_RECEIVING_CUSTOMER_CODE varchar2(3) := '032';
  m_GW_INTER_RECEIVER_BANK_CODE varchar2(3) := '022';
  m_GW_RECEIVING_ACCOUNT        varchar2(3) := '033';
  m_GW_DESCRIPTION_CODE         varchar2(3) := '034';

  m_GW_SOCK_HEAD_CODE    varchar2(10) := 'SKTMLEN';
  m_GW_TELLER_ID_CODE    varchar2(10) := 'TLBID';
  m_GW_CONTROL_UNIT_CODE varchar2(10) := 'TLBCUD';
  m_GW_SINGLE_FIELD_CODE varchar2(10) := 'TLBF01';
  m_GW_AMOUNT_CODE       varchar2(10) := 'XRMRAM';
  m_GW_DATA_FORMAT_CODE  varchar2(10) := 'I13FMID';
  m_GW_USER_ID_CODE      varchar2(10) := 'I13USER';
  m_GW_SCENARIO_NUM_CODE varchar2(10) := 'I13SSNO';
  m_Direction            varchar2(10) := 'IBPS-SIBS';
  m_GWtype               varchar2(5) := 'IBPS';
  m_Depar                varchar2(3) := 'RM';
  GW_IBPS_DETAIL         varchar2(10) := 'ABCS_IN';
  mszTLBF09              varchar2(25);
  v_F179                 varchar2(100);
  GW_SA_IND              varchar2(3) := '07';

  GW_CA_IND       varchar2(3) := '01';
  GW_CA_IND_KN    varchar2(3) := '18';
  GW_CA_IND_N1    varchar2(3) := '16';
  mszClosedStatus varchar2(3);

  GW_LA_FIELD     varchar2(10) := 'TLBF04';
  GW_SA_FIELD     varchar2(10) := 'TLBF02';
  GW_CA_FIELD     varchar2(10) := 'TLBF03';
  GW_ACCOUNT_CODE varchar2(10) := 'TLBF09';
  mszAccTypeField varchar2(10);
  -- Phan dinh ngia cua cac trung Insert vao bang content

  m_QUERY_ID NUMBER(20) := 0;
  --m_FILE_NAME         VARCHAR2(50);
  m_MSG_DIRECTION     VARCHAR2(1);
  m_TRANS_CODE        VARCHAR2(10);
  m_GW_TRANS_NUM      NUMBER(16);
  m_SIBS_TRANS_NUM    NUMBER(16);
  m_SENDER            VARCHAR2(8);
  m_RECEIVER          VARCHAR2(8);
  m_TRANS_DATE        DATE;
  m_AMOUNT            NUMBER(19, 2);
  m_CCYCD             VARCHAR2(3);
  m_TRANS_DESCRIPTION VARCHAR2(900);
  m_TAD               VARCHAR2(50);
  m_RM_NUMBER         VARCHAR2(20);
  m_Trans_ref_11      varchar2(20);
  ex Exception;
  m_GW_LA_FIELD             varchar2(10) := 'TLBF04';
  m_GW_CLOSED_STATUS_REASON varchar2(10) := 'XRMSCLS';
  m_IBPS_CONTENT            ibps_msg_content%Rowtype;
  m_IBPS_IN                 IBPS_SIBS_MSG_IN%Rowtype;
  m_nErr                    Integer;
  m_pErrhan                 ERROR_HANDLE%RowType;
  gAcc_num                  varchar2(20) := '';

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
/*
Cap nhat cho CITAD 2.1
Them truong noi dung thue vao Queue

*/
  PROCEDURE IBPS_EN_CONVERTIN(pFILE_NAME      varchar2,
                              pMSG_TYPE       varchar2,
                              pSIBS_TRANS_NUM varchar2,
                              pIBPS_CONTENT   varchar2,
                              pCONTENT_TAD    varchar2,
                              pSYSTEMDATE     DATE) IS
  
    queue_options      DBMS_AQ.ENQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(16);
    my_message         ibps_type_convertin;
  
    ipos integer := 0;
  BEGIN
  
    DBMS_AQADM.start_queue('IBPS_Q_CONVERT_IN', TRUE, TRUE);
  
    my_message := ibps_type_convertin(pFILE_NAME,
                                      pMSG_TYPE,
                                      pSIBS_TRANS_NUM,
                                      pIBPS_CONTENT,
                                      pCONTENT_TAD,
                                      pSYSTEMDATE);
    ipos       := 0;
  
    if ipos = 0 then
      DBMS_AQ.ENQUEUE(queue_name         => 'IBPS_Q_CONVERT_IN',
                      enqueue_options    => queue_options,
                      message_properties => message_properties,
                      payload            => my_message,
                      msgid              => message_id);
    End if;
    COMMIT;
  
  Exception
    when OTHERS THEN
      --Rollback;
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := 0;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error When Enqueue content Message File:' ||
                                   pFILE_NAME || SQLCODE || ' -ERROR- ' ||
                                   SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
      m_IBPS_Type.Msg_Direction := m_Direction;
     -- GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
     -- Raise ex;
    
  END IBPS_EN_CONVERTIN;

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
  PROCEDURE IBPS_DE_CONVERTIN IS
  
    queue_options      DBMS_AQ.DEQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(2000);
    my_message         ibps_type_convertin;
    my_message11       ibps_type_convertin;
  
    new_messages Boolean := True;
    next_trans EXCEPTION;
    no_messages EXCEPTION;
    pragma exception_init(next_trans, -25235);
    pragma exception_init(no_messages, -25228);
    i integer;
    -- Dinh nghia cac bien lay du lieu ra tu queue 
  
    pSYSTEMDATE  Date;
    ipos         Integer;
    pvConverted  Varchar(5000);
    pvMSGHead    Varchar2(1000);
    pvMSgContent Varchar2(4000);
     pvContentTax Varchar2(4000);
    pnHeadLen    Number(4);
  
  BEGIN
  
    DBMS_AQADM.start_queue('IBPS_Q_CONVERT_IN', TRUE, TRUE); -- start queue
  
    queue_options.wait := 1;
    i                  := 0;
  
    Select count(1) Into ipos From IBPS_TB_Q_CONVERT_IN;
    If (ipos > 0) then
      While ((new_messages) and i < 100) LOOP
        Begin
        
          i      := i + 1;
          m_nErr := 0;
          DBMS_AQ.DEQUEUE(queue_name         => 'IBPS_Q_CONVERT_IN',
                          dequeue_options    => queue_options,
                          message_properties => message_properties,
                          payload            => my_message,
                          msgid              => message_id);
          queue_options.navigation := DBMS_AQ.NEXT;
        
          pFILE_NAME      := '';
          pMSG_TYPE       := '';
          pSIBS_TRANS_NUM := '';
          pIBPS_CONTENT   := '';
        
          pFILE_NAME      := my_message.FILE_NAME;
          pMSG_TYPE       := my_message.MSG_TYPE;
          pSIBS_TRANS_NUM := my_message.SIBS_TRANS_NUM;
          pIBPS_CONTENT   := my_message.IBPS_CONTENT;
          -- -- 20140911 Nang cap Citad 2.1
          pvContentTax    :=my_message.CONTENT_TAX;
          -- Het nang cap
          pSYSTEMDATE     := my_message.SYSTEMDATE;
          mszTLBF09       := DefineAccType(pIBPS_CONTENT);
          my_message11    := ibps_type_convertin(to_char(pFILE_NAME),
                                                 to_char(pMSG_TYPE),
                                                 to_char(pSIBS_TRANS_NUM),
                                                 to_char(pIBPS_CONTENT),
                                                 to_char(pvContentTax),    ---- 20140911 Nang cap Citad 2.1
                                                 to_char(pSYSTEMDATE));
        
          select SEQ_IBPS_QUERY.NEXTVAL INTO m_QUERY_ID From dual;
          -- Gan so RM tu tang (chi lay phan tu tang thang service tao ra)
          m_Sibs_Trans_Num := pSIBS_TRANS_NUM;
          pnHeadLen        := IBPS_GET_MSGHEADLen();
          -- Dua cac ham su ly dien, convert dien,
        
          pvConverted := Convert_IBPS_SIBS(my_message11);
        
          if m_nErr = 0 and (mszAccTypeField = GW_CA_FIELD or
             mszAccTypeField = GW_SA_FIELD) then
            If (GW_PK_LIB.CheckCurrency(m_CCYCD, Substr(gAcc_num, 6, 2)) = 0) Then
              m_nErr := 25;
            End if;
          end if;
        
          If (pvConverted <> ' ') or (pvConverted is not null) then
          
            pvMSGHead    := substr(pvConverted, 1, pnHeadLen);
            pvMSgContent := substr(pvConverted, pnHeadLen + 1);
          
            -- insert dien vao day
            m_IBPS_IN.Msg_Type     := m_Direction;
            m_IBPS_IN.Ref_No       := m_Gw_Trans_Num;
            m_IBPS_IN.Msg_Def_In   := 'ABCS_IN';
            m_IBPS_IN.Msg_Def_Out  := 'TMQ';
            m_IBPS_IN.Trans_Date   := Sysdate;
            m_IBPS_IN.Head_Content := pvMSGHead;
            m_IBPS_IN.Content      := pvMSgContent;
            m_IBPS_IN.Status       := 0;
            m_IBPS_IN.Seq_No       := '';
            m_IBPS_IN.Link_Ref     := '';
            m_IBPS_IN.Msg_Id       := m_QUERY_ID;
            m_Msg_Direction        := 1;
          
            -- gan gia tri co viec Insert dien vao bang IBPS_MSG_CONTENT
            if m_nErr = 0 then
              If (GW_PK_IBPS_PROCESS.IBPS_CCYCD_CHECK(m_CCYCD, m_GWtype) = 0) Then
                m_nErr := 3;
              End if;
            end if;
          
            if m_nErr > 0 then
              m_IBPS_IN.Status := -1;
            
            end if;
          
            if (NVL(m_nErr, 0) >= 0) then
              IBPS_UPDATE_SIBS_IN(m_IBPS_IN);
            end if;
            m_IBPS_CONTENT.Print_Sts      := 0;
            m_IBPS_CONTENT.Query_Id       := m_QUERY_ID;
            m_IBPS_CONTENT.File_Name      := pFILE_NAME;
            m_IBPS_CONTENT.Trans_Code     := m_TRANS_CODE;
            m_IBPS_CONTENT.Msg_Direction  := m_Direction;
            m_IBPS_CONTENT.Gw_Trans_Num   := m_GW_TRANS_NUM;
            m_IBPS_CONTENT.Sibs_Trans_Num := m_SIBS_TRANS_NUM;
            m_IBPS_CONTENT.F07            := GET_IBPS_Field(pIBPS_CONTENT,
                                                            '007');
            m_IBPS_CONTENT.F19            := GET_IBPS_Field(pIBPS_CONTENT,
                                                            '019');
            m_IBPS_CONTENT.F21            := GET_IBPS_Field(pIBPS_CONTENT,
                                                            '021');
            m_IBPS_CONTENT.F22            := GET_IBPS_Field(pIBPS_CONTENT,
                                                            '022');
        
        
      
            m_IBPS_CONTENT.Trans_Date     := sysdate;
            -- h?t comment
            m_IBPS_CONTENT.Transdate      := to_number(to_char(m_IBPS_CONTENT.Trans_Date,
                                                               'YYYYMMDD'));
        
        
        
        
            m_IBPS_CONTENT.Ccycd          := m_CCYCD;
            -- 20140911 Nang cap Citad 2.1
            m_IBPS_CONTENT.Content_Tax:=pvContentTax;
          -- 20140911 Het Nang cap
            if m_nErr > 0 then
              m_IBPS_CONTENT.Status := -1;
            else
              m_IBPS_CONTENT.Status := 0;
            
            end if;
            m_Trans_ref_11                   := GET_IBPS_Field(pIBPS_CONTENT,
                                                               '011');
            m_IBPS_CONTENT.Amount            := m_AMOUNT;
            m_IBPS_CONTENT.Err_Code          := NVL(m_nErr, 0);
            m_IBPS_CONTENT.Trans_Description := m_TRANS_DESCRIPTION;
            m_IBPS_CONTENT.Department        := m_Depar;
            m_IBPS_CONTENT.Content           := pIBPS_CONTENT;
            m_IBPS_CONTENT.Source_Branch     := IBPS_Set_Receipt_Branch(m_IBPS_CONTENT.F22);
            m_IBPS_CONTENT.Tad               := IBPS_Set_Receipt_Branch(m_IBPS_CONTENT.F22,
                                                                        2);
            m_IBPS_CONTENT.Pre_Tad           := '';
            m_IBPS_CONTENT.Rm_Number         := m_RM_NUMBER;
            m_IBPS_CONTENT.Receiving_Time    := pSYSTEMDATE;
            m_IBPS_CONTENT.Trans_Ref         := m_Trans_ref_11;
            m_IBPS_CONTENT.Content_Tax       := my_message.CONTENT_TAX;
            if (NVL(m_nErr, 0) >= -2) then
              GW_PK_IBPS_PROCESS.IBPS_Update_Content(m_IBPS_CONTENT,
                                                     'IBPS_Job_CONVERT_IN',
                                                     m_Direction);
            
              m_IBPS_Type.Log_Date      := sysdate;
              m_IBPS_Type.Query_Id      := m_QUERY_ID;
              m_IBPS_Type.Status        := 1;
              m_IBPS_Type.Descriptions  := 'Convert Message File:' ||
                                           pFILE_NAME || SQLCODE ||
                                           ' Successed ' || SQLERRM;
              m_IBPS_Type.Area          := ' ';
              m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
              m_IBPS_Type.Msg_Direction := m_Direction;
              GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
            End if;
          
          End if;
          commit;
        
        EXCEPTION
          WHEN next_trans THEN
            queue_options.navigation := DBMS_AQ.NEXT_TRANSACTION;
          WHEN no_messages THEN
            new_messages := FALSE;
          WHEN others then
          
            m_IBPS_Type.Log_Date      := sysdate;
            m_IBPS_Type.Query_Id      := m_QUERY_ID;
            m_IBPS_Type.Status        := 0;
            m_IBPS_Type.Descriptions  := 'Error When Enqueue content Message File:' ||
                                         pFILE_NAME || SQLCODE ||
                                         ' -ERROR- ' || SQLERRM;
            m_IBPS_Type.Area          := ' ';
            m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
            m_IBPS_Type.Msg_Direction := m_Direction;
            GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
          
            Begin
              
            m_IBPS_Type.Area:='';
             /* IBPS_EN_CONVERTIN(pFILE_NAME,
                                pMSG_TYPE,
                                pSIBS_TRANS_NUM,
                                pIBPS_CONTENT,
                                pSYSTEMDATE);*/
            EXCEPTION
              WHEN others then
                m_pErrhan.Query_Id      := m_QUERY_ID;
                m_pErrhan.Filename      := my_message11.FILE_NAME;
                m_pErrhan.Ref_Number    := m_Gw_Trans_Num;
                m_pErrhan.Content       := pIBPS_CONTENT;
                m_pErrhan.Msg_Direction := m_Direction;
                m_pErrhan.Department    := m_Depar;
                m_pErrhan.Trans_Date    := Sysdate;
                m_pErrhan.Msg_Id        := 0;
                m_pErrhan.Status        := -1;
                Gw_Pk_Lib.INSERT_ERR_HANDER(m_pErrhan);
            END;
        END;
      
        commit;
      end loop;
    
    end if;
  Exception
    when OTHERS THEN
      -- Rollback;
    
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := m_QUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error When Enqueue content Message File:' ||
                                   pFILE_NAME || SQLCODE || ' -ERROR- ' ||
                                   SQLERRM || pIBPS_CONTENT;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    
  END IBPS_DE_CONVERTIN;

  /* Nguoi tao          :HaNTT10
  -- Muc dich           :Update du lieu cho bang IBPS_SIBS_MSG_IN
  -- Ten ham            :IBPS_UPDATE_SIBS_IN()
  -- Tham so                           
  -- Gis tri tra ve: Day du lieu vao bang table IBPS_SIBS_MSG_IN
  -- Ngay tao:30/05/2008 */
  Procedure IBPS_UPDATE_SIBS_IN(pRowConvertIN IBPS_SIBS_MSG_IN%Rowtype) is
  Begin
    Insert Into IBPS_SIBS_MSG_IN Values pRowConvertIN;
    Commit;
    -- select Max(MSG_ID) Into m_Query_ID From IBPS_SIBS_MSG_IN;
  Exception
    when OTHERS THEN
     -- Rollback;
    
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := m_QUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Error : Insert Message into IBPS_SIBS_MSG_IN' ||
                                   SQLCODE || ' -ERROR- ' || SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    
  end;

  /* Nguoi tao          :QuanLD
  -- Muc dich           :Conevrt dien tu IBPS sang SIBS
  -- Ten ham            :Convert_IBPS_SIBS()
  -- Tham so            pIBPS_CONTENT varchar2,
                             pFILE_NAME    varchar2,
                             pSYSTEMDATE   date                
  -- Gis tri tra ve: mot dien da duoc convert thanh chuan SIBS
  -- Ngay tao:30/05/2008 */

  FUNCTION CONVERT_IBPS_SIBS(my_message ibps_type_convertin) REturn Varchar2 IS
    vHEADCONDITION varchar2(2000);
    --Dinh nghia 1 cusor de lay cau truc dien cua SIBS laoi cho IBPS
  
    pvRetuenContent varchar2(10000) := ' ';
    pv_FileContent  Varchar(3000);
    pvFileConverted Varchar(3000);
    pv_FileCoce     varchar2(4);
    pv_FileName     varchar2(12);
    pv_SIBSFilecode varchar2(10);
    pv_DefaulValue  varchar2(255);
    pv_Datatype     number(1);
    pv_FileLength   number(4);
    ipos            Integer;
    pGWtrans_num    varchar2(9);
    igwpos          integer := 0;
    vchk            varchar2(2);
    pvIBPS_CONTENT  varchar2(2000);
    pvFILE_NAME     varchar2(50);
    pvF11           varchar2(12);
    cursor curMSG_DEF IS
      SELECT distinct SVL.FieldID         SVLID,
                      SVL.Field_Code      SVLFieldCode,
                      SVL.Field_Name      SVLFieldName,
                      SVL.Default_Value   SVLDefaultValue,
                      SVL.GW_Pos          SVLPos,
                      SVL.Length          SVLLength,
                      SVL.Data_Type       SVLDataType,
                      SVL.SIBS_Field_Code SIBSFieldCode,
                      TAD.Chk
        FROM (SELECT *
                FROM Msg_Def
               WHERE Msg_Def_ID = 'IBPS'
               order by Msg_Def.Sibs_Pos) TAD,
             (SELECT *
                FROM Msg_Def
               WHERE instr(vHEADCONDITION, Msg_Def_ID) > 0
                  or Msg_Def_ID = upper(GW_IBPS_DETAIL)
               Order by Msg_Def.Sibs_Pos) SVL
       WHERE TAD.Field_Code(+) = SVL.Field_Code;
    V_curMSG_DEF curMSG_DEF%RowType;
  
  BEGIN
    -- Lay gia tri chuan convert phan Head cua dien theo kenh TT va phan he
    SELECT GD.HEADMBASE_IN
      into vHEADCONDITION
      FROM GWTYPE_DEPT GD
     WHERE GD.GWTYPE = m_GWtype
       and GD.DEPARTMENT = m_Depar
       And rownum = 1;
  
    pvIBPS_CONTENT  := my_message.IBPS_CONTENT;
    pvFILE_NAME     := my_message.FILE_NAME;
    pvRetuenContent := LPAD(pvRetuenContent, 5000, ' ');
    -- Lay ra So Refnum de kiem tra dien trung
    -- Lay vi tri cua so ref trong dien
    pGWtrans_num   := GET_IBPS_Field(pvIBPS_CONTENT, '110'); --Substr(pvIBPS_CONTENT, ipos + 4, GW_IBPS_REL_LEN);
    m_Gw_Trans_Num := To_number(pGWtrans_num);
    --lay ra ma ngan hang gui
    ipos     := Instr(pvIBPS_CONTENT, '#' || m_vGW_SENDING_BANK_CODE);
    m_Sender := Substr(pvIBPS_CONTENT, ipos + 4, GW_BANK_CODE_LEN);
    /* if Check_Branch(m_Sender) = false then
      m_nErr := 19;
    end if;*/
    -- lay ra ma Ngan hang nhan
  
    --lay ra ma ngan hang gui
    ipos       := Instr(pvIBPS_CONTENT, '#' || '022');
    m_RECEIVER := Substr(pvIBPS_CONTENT, ipos + 4, GW_BANK_CODE_LEN);
  
    /* if Check_Branch(m_RECEIVER) = false then
      m_nErr := 21;
    end if;*/
    m_AMOUNT := GET_IBPS_Field(pvIBPS_CONTENT, '027') / 100;
  
    if Check_BIDVBranch(m_RECEIVER) = false then
      m_nErr := 24; --Ngan hang nhan dien  cua dien ve lai khac BIDV
      m_nErr := 0;
    end if;
    --lay ra so hieu giao dich
    ipos := Instr(pvIBPS_CONTENT, '#' || '011');
    if ipos > 0 then
      pvF11 := GET_IBPS_Field(pvIBPS_CONTENT, '011');
    else
      pvF11 := '';
    end if;
    -- Cap nhat Citad moi
    ipos := Instr(pvIBPS_CONTENT, '#' || '179');
    if ipos > 0 then
      v_F179 := GET_IBPS_Field(pvIBPS_CONTENT, '179');
    else
      v_F179 := '';
    end if;
    -- Het c?p nhât
    -- Check dien trung 
  
    if (GW_PK_IBPS_PROCESS.IBPS_MSG_DUP_CHECK(1,
                                              m_RECEIVER || m_Gw_Trans_Num ||
                                              pvF11) < 1) Then
      --dien bi trung
      m_nErr := 2;
    
    end if;
    OPEN curMSG_DEF;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH curMSG_DEF
        INTO V_curMSG_DEF;
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      EXIT WHEN curMSG_DEF %notfound;
      pv_FileCoce     := V_curMSG_DEF.Svlfieldcode;
      pv_FileName     := V_curMSG_DEF.Svlfieldname;
      pv_SIBSFilecode := V_curMSG_DEF.Sibsfieldcode;
      pv_DefaulValue  := V_curMSG_DEF.Svldefaultvalue;
      pv_Datatype     := V_curMSG_DEF.Svldatatype;
      pv_FileLength   := V_curMSG_DEF.Svllength;
      igwpos          := V_curMSG_DEF.Svlpos;
      vchk            := V_curMSG_DEF.Chk;
      if pv_FileCoce is not null then
        pv_FileContent := GET_IBPS_Field(pvIBPS_CONTENT, pv_FileCoce);
      
        /* if (pv_SIBSFilecode = 'TLBBRC') Then
           pv_FileContent :=GW_PK_IBPS_PROCESS.GETSIBS_BANKCODE(pv_FileContent);
        
        End if;*/
        -- ghi log loi khi 1 field bat buoc phai ton tai nhung lai khong ton tai
        if vchk = 'M' and pv_FileContent is null then
          m_IBPS_Type.Log_Date      := sysdate;
          m_IBPS_Type.Query_Id      := m_QUERY_ID;
          m_IBPS_Type.Status        := 0;
          m_IBPS_Type.Descriptions  := 'Convert_IBPS_SIBS: Filename' ||
                                       pvFILE_NAME || ' is null ' || '--' ||
                                       SQLERRM;
          m_IBPS_Type.Area          := ' ';
          m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
          m_IBPS_Type.Msg_Direction := m_Direction;
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
        
        end if;
      else
        pv_FileContent := pv_DefaulValue;
      End IF;
    
      /*  If pv_SIBSFilecode = 'TLCUR2' or pv_SIBSFilecode = 'TLCUR1' or
         pv_SIBSFilecode = 'TLBCUR' then
        pv_FileContent := GET_IBPS_Field(pvIBPS_CONTENT, '026');
      End if;*/
    
      pv_FileContent := NVL(pv_FileContent, ' ');
    
      pvFileConverted := Ibps_ConvertFIELD(pvFILE_NAME,
                                           pv_Datatype,
                                           pv_FileContent,
                                           pv_FileCoce,
                                           pv_SIBSFilecode,
                                           pv_FileLength);
    
      if (pv_SIBSFilecode = 'XRMRAM') Then
        pvFileConverted := LPAD(Trim(pvFileConverted), pv_FileLength, '0');
      End if;
    
      pvRetuenContent := PUMPIN(pvFileConverted,
                                igwpos,
                                pv_FileLength,
                                pv_Datatype,
                                pvRetuenContent);
      --pvRetuenContent || pvFileConverted;
      ipos := Length(pvRetuenContent);
    
    END LOOP;
    CLOSE curMSG_DEF;
    -- lay ra ma Tran code trong dien
    m_TRANS_CODE := IBPS_GET_FILECONTENT('003', pvIBPS_CONTENT);
    --ipos := Length(Trim(pvRetuenContent));
    -- Lay ra Description
    -- m_TRANS_DESCRIPTION := IBPS_GET_FILECONTENT(m_GW_DESCRIPTION_CODE, my_message.IBPS_CONTENT);    
    return Trim(pvRetuenContent);
  Exception
    when OTHERS THEN
     -- Rollback;
    
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := m_QUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'Convert_IBPS_SIBS: Filename' ||
                                   pvFILE_NAME || Sqlcode || '--' ||
                                   SQLERRM;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    
      m_pErrhan.Query_Id      := m_QUERY_ID;
      m_pErrhan.Filename      := pvFILE_NAME;
      m_pErrhan.Ref_Number    := m_Gw_Trans_Num;
      m_pErrhan.Content       := pvIBPS_CONTENT;
      m_pErrhan.Msg_Direction := m_Direction;
      m_pErrhan.Department    := m_Depar;
      m_pErrhan.Trans_Date    := Sysdate;
    
      if m_QUERY_ID is null then
        m_QUERY_ID := 0;
      
      end if;
      m_pErrhan.Msg_Id := m_QUERY_ID;
      m_pErrhan.Status := -1;
    
      Gw_Pk_Lib.INSERT_ERR_HANDER(m_pErrhan);
      Raise ex;
      return ' ';
  END Convert_IBPS_SIBS;

  /* Nguoi tao          :QuanLD
  -- Muc dich           :Conevrt dien tu IBPS sang SIBS
  -- Ten ham            :Convert_Field()
  -- Tham so            pIBPS_CONTENT varchar2,
                             pFILE_NAME    varchar2            
  -- Chuan hoa lai do dai, va mot so truong dac biet
  -- Gis tri tra ve: mot dien da duoc convert thanh chuan SIBS
  -- Ngay tao:30/05/2008 */

  FUNCTION IBPS_CONVERTFIELD(pFILE_NAME     varchar2,
                             pDATATYPE      NUMBER,
                             pCONTENT       VARCHAR2,
                             pFILECODE      VARCHAR2,
                             pSIBSFieldCode Varchar2,
                             pLENGHT        NUMBER) RETURN VARCHAR2 IS
  
    pReturn    Varchar2(3000);
    pReturnold varchar2(3000);
  BEGIN
    --Kiem tra
  
    Case
      When pFILECODE = m_vGW_RELNUM_CODE Then
        pReturn        := pCONTENT;
        m_Gw_Trans_Num := pReturn;
        /* if Length(Trim(m_Gw_Trans_Num)) < 1 Then
         -- m_nErr := GW_ERR_REF_NUM;
        end if;*/
      When (pFILECODE = m_vGW_MSGID_CODE) THEN
        -- 003: Transaction Code
        m_Trans_Code := pCONTENT;
      
        if pCONTENT = '201001' then
          pReturn := '8124';
        else
          pReturn := '8125';
        end if;
        /* pReturn := GW_PK_IBPS_PROCESS.IBPS_TRGetTransCode(m_Sender,
        m_Amount,
        1);*/
        if Length(Trim(pReturn)) < 1 Then
          m_nErr := -1; --GW_ERR_TRANS_CODE;
        end if;
      When (pFILECODE = m_vGW_SENDING_BANK_CODE) THEN
        -- 007: Sending Bank Code - donot convert sending bank, keep 8 digit value
        pReturn := Trim(pCONTENT);
        if (LENGTH(Trim(pCONTENT)) <= 0) THEN
          m_nErr := -1; --GW_ERR_SENDINGBANK_FALSE;
        End if;
        --019
      When (pFILECODE = m_GW_INTER_RECEIVER_BANK_CODE) THEN
      
        pReturn    := IBPS_Set_Receipt_Branch(trim(pCONTENT));
        m_Tad      := pReturn;
        m_Receiver := trim(pCONTENT);
        if (LENGTH(Trim(pReturn)) <= 0) THEN
          m_nErr := -1; --GW_ERR_RECEIVINGBANK_FALSE;
        End if;
      When (pFILECODE = m_vGW_CREATION_DATE_CODE) THEN
        -- 012: Creation Date
        m_Trans_Date := To_Date(substr(pCONTENT, 1, 8), 'YYYYMMDD');
        pReturn      := To_char(To_Date(substr(pCONTENT, 1, 8), 'YYYYMMDD'),
                                'DDMMYY');
      
      When (pFILECODE = m_vGW_TRANS_AMOUNT_CODE) THEN
        -- 027: Transaction Amount
        m_Amount := To_NUMBER(pCONTENT) / 100;
      
        if (m_Amount <= 0) THEN
        
          m_nErr := -1;
        end if;
        pReturn := To_Char(pCONTENT);
      
        if (LENGTH(Trim(pReturn)) <= 0) THEN
          m_nErr := -1; --GW_ERR_MSGAMOUNT_FALSE;
        End if;
      When (pSIBSFieldCode = m_GW_AMOUNT_CODE) THEN
        pReturn := To_NUMBER(pCONTENT);
        -- -  moi them ngay 280808 C?u truc s? Transaction Reference: DDMMYYYY+F011, trong do:
    --i.   YYYYMMDD: Nam, thang , ngay
    --ii. F11: S? hi?u giao d?ch trong di?n IBPS d?n.
    
      When (pFILECODE = '011') THEN
        pReturn        := to_char(sysdate, 'DDMMYYYY') || pCONTENT;
        m_Trans_ref_11 := pCONTENT;
        -- 026: Currency Code
      When (pFILECODE = m_vGW_CURRENCY_CODE) THEN
        m_Ccycd := Trim(pCONTENT);
      
        if (LENGTH(Trim(pCONTENT)) <= 0) THEN
          m_Ccycd := LPAD(m_Ccycd, pLENGHT, ' ');
          m_nErr  := -1; --GW_ERR_CURRENCY_FALSE;
        
        End if;
        pReturn := m_Ccycd;
      
    -- 029, 032: Address code
    -- 029, 032: Address code
      When (pFILECODE = m_vGW_SENDING_ADD_CODE) OR
           (pFILECODE = m_vGW_RECEIVING_ADD_CODE) Then
      
        -- Concatenate string here.
        -- Input string we have a 75 characters array, we must convert it to 70.
      
        if (LENGTH(pCONTENT) < 75) then
          pReturn := substr(pCONTENT, 1, 35);
          pReturn := RPAD(pReturn, 40, ' ') || substr(pCONTENT, 35);
        
        end if;
        -- 030: Sending Account 
      When (pFILECODE = m_vGW_ACC_SENDING_CODE) THEN
        pReturn := trim(pContent);
        return trim(pReturn);
      When (pFILECODE = m_vGW_RECEIVING_CUSTOMER_CODE) THEN
        -- Thay 4ky tu dau =' '
        pReturn := substr(pCONTENT, 5, LENGTH(pCONTENT) - 4);
        pReturn := pReturn || LPAD(pReturn, LENGTH(pCONTENT), ' ');
      When (pFILECODE = m_GW_RECEIVING_ACCOUNT) THEN
        -- 033: Receiving account
        pReturn := trim(pContent);
      
      When -- 034: Msg Description
       (pFILECODE = m_GW_DESCRIPTION_CODE) THEN
      
        m_Trans_Description := Trim(pContent);
        -- Cap nhat cho citad moi
        if trim(v_F179) is not null then
        
          pReturn := Trim(v_F179 || '%% ' || m_Trans_Description);
        else
          pReturn := Trim(m_Trans_Description);
        end if;
        if length(pReturn) > pLENGHT then
          pReturn := substr(pReturn, 1, pLENGHT);
        end if;
        -- Het cap nhat cho citad moi
      When (pSIBSFieldCode = m_GW_SOCK_HEAD_CODE) then
        pReturn := '4092';
        --Lay ra gia tri TELLER_ID_CODE
      When (pSIBSFieldCode = m_GW_TELLER_ID_CODE) Then
        pReturn := GW_PK_IBPS_PROCESS.GETBANKCODE(GET_IBPS_Field(pIBPS_CONTENT,
                                                                 '022'),
                                                  3);
      
      When (pSIBSFieldCode = m_GW_CONTROL_UNIT_CODE) Then
        pReturn := GW_PK_IBPS_PROCESS.GETBANKCODE(m_Sender, 4);
      
      When (pSIBSFieldCode = m_GW_SINGLE_FIELD_CODE) Then
        -- So RM sinh trong GU
      
        -- lay ma 3 so cua ngan hang nhan dien
        /* vtemp := LTrim(Trim(GW_PK_IBPS_PROCESS.GETBANKCODE(m_RECEIVER, 1)),
                       '0');
        if vtemp = '-1' or vtemp is null or vtemp = ' ' then
          vtemp := '901';
        end if;
        
        pReturn     := vtemp || '1' || to_char(Sysdate, 'YYMMDD') ||
                       LPAD(m_Sibs_Trans_Num, 5, '0');*/
      
        pReturn     := GET_RMNUMBER(m_RECEIVER);
        m_Rm_Number := pReturn;
        if trim(m_Rm_Number) is null then
          m_nErr := 16;
        end if;
      
    /*    When (pSIBSFieldCode = m_GW_LA_FIELD) Then
                                      
                                        pReturn := m_Amount * 100;
                                  */
      When (pSIBSFieldCode = mszAccTypeField) Then
      
        pReturn := m_Amount * 100;
      When (pSIBSFieldCode = m_GW_DATA_FORMAT_CODE) Then
        pReturn := 'ABCS';
      When (pSIBSFieldCode = m_GW_USER_ID_CODE) Then
        pReturn := 'FPTIBPS501';
      When (pSIBSFieldCode = m_GW_SCENARIO_NUM_CODE) then
      
        pReturn := IBPS_GET_ScenaryNum(m_GWtype, m_Depar);
      
      When (pSIBSFieldCode = m_GW_CLOSED_STATUS_REASON) Then
      
        pReturn := mszClosedStatus;
      WHEN pSIBSFieldCode = 'TLBIDC' then
        pReturn := 'F';
      
      WHEN pSIBSFieldCode = GW_ACCOUNT_CODE then
        pReturn := mszTLBF09;
      
    /* WHEN pSIBSFieldCode = 'TLBRFN' then
                                                                                                                pReturn :='';  */
      WHEN pSIBSFieldCode = 'TLCUR2' or pSIBSFieldCode = 'TLCUR1' or
           pSIBSFieldCode = 'TLBCUR' then
        pReturn := GET_IBPS_Field(pIBPS_CONTENT, '026');
      Else
        pReturn := Trim(pContent);
    End case;
  
    pReturn := Trim(pReturn);
    if pDATATYPE = 0 Then
      if pReturn is null then
        pReturn := ' ';
      end if;
      pReturn := NVL(pReturn, ' ');
      pReturn := RPAD(pReturn, pLENGHT, ' ');
    else
      if pReturn is null then
        pReturn := '0';
      end if;
      pReturn := NVL(pReturn, '0');
      pReturn := LPAD(pReturn, pLENGHT, '0');
    End if;
    pReturnold := pReturn;
    if (pFILECODE is not null) Then
    
      pReturn := GW_PK_IBPS_PROCESS.IBPS_REPLACE_Char(pReturn,
                                                      m_GWtype,
                                                      m_Depar,
                                                      ' ',
                                                      pFILECODE,
                                                      m_Direction,
                                                      m_QUERY_ID,
                                                      'IBPS_JOB_CONVERT_IN');
    
      pIBPS_CONTENT := Replace(pIBPS_CONTENT,
                               pFILECODE || pReturnold,
                               pFILECODE || pReturn);
    End if;
    return convertfont(pReturn);
  Exception
    when OTHERS THEN
      --Rollback;
      m_nErr := -1;
    
      m_IBPS_Type.Log_Date      := sysdate;
      m_IBPS_Type.Query_Id      := m_QUERY_ID;
      m_IBPS_Type.Status        := 0;
      m_IBPS_Type.Descriptions  := 'IBPS_Job_CONVERT_OUT GW_PK_IBPS_Q_ConvertOut.IBPS_CONVERTFIELD (' ||
                                   pFILECODE || ': ' || pSIBSFieldCode ||
                                   pFILE_NAME ||
                                   ') : ERROR WHEN Convert Message' ||
                                   SQLCODE;
      m_IBPS_Type.Area          := ' ';
      m_IBPS_Type.Job_Name      := 'IBPS_Q_CONVERT_IN';
      m_IBPS_Type.Msg_Direction := m_Direction;
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_IBPS_Type);
    
      if pDATATYPE = 0 Then
        pReturn := NVL(pCONTENT, ' ');
        pReturn := RPAD(pReturn, pLENGHT, ' ');
      else
        pReturn := NVL(pCONTENT, '0');
        pReturn := LPAD(pReturn, pLENGHT, '0');
      End if;
    
      Return pReturn;
  END IBPS_CONVERTFIELD;

  FUNCTION IBPS_GET_ScenaryNum(pGWTYPE Varchar2, pDepartment varchar2)
    RETURN VARCHAR2 IS
    pvReturn varchar2(16);
  Begin
    Select GD.FUNCTION_SEND
      INTO pvReturn
      from GWTYPE_DEPT GD
     Where GD.GWTYPE = pGWTYPE
       And GD.DEPARTMENT = pDepartment;
    if (Length(pvReturn) > 0) Then
      pvReturn := 'BBHTLMONEYFNC   ';
    End if;
    return pvReturn;
  Exception
    When others then
      Return 'BBHTLMONEYFNC   ';
  End;

  FUNCTION IBPS_GET_FILECONTENT(pFilecode Varchar2, pMSGContent Varchar2)
    RETURN VARCHAR2 IS
    pvFilecode varchar2(4);
    pvContent  varchar2(3000);
    i          Integer := 1;
    ipos2      Integer := 2;
    pvChar     varchar2(1);
    ipos1      Integer := 2;
  
  BEGIN
    pvContent := ' ';
    ipos2     := instr(pMSGContent, '#' || pFilecode) + 1;
    ipos1     := ipos2;
    for j in ipos2 .. length(pMSGContent) loop
      pvChar := substr(pMSGContent, j, 1);
      if (pvChar = '#') Then
        i          := i + 1;
        pvContent  := substr(pMSGContent, ipos1 + 3, j - ipos1 - 3);
        pvFilecode := substr(pMSGContent, ipos1, 3);
      
        Return Trim(pvContent);
      End if;
    End Loop;
    return Trim(pvContent);
  Exception
    When others Then
      Return ' ';
  END IBPS_GET_FILECONTENT;

  FUNCTION IBPS_GET_MSGHEADLen RETURN Number IS
    pnHeadlen Number(4);
  BEGIN
    -- cho nay hoi chuoi 1 ti vi phan head cau dien IBPS co 
    -- do dai qua nho de dam bao an toan cho trung Content khong day full 4000 ky tu nen coong them 1 so gia tri vao
    -- lay den File "Single Fields 30  " " TLBF30"
    --Phan header + them 583 ky tu de lay den truong Single Fields 30   
    Select Sum(MD.LENGTH) + 659
      INTO pnHeadlen
      from MSG_DEF MD
     Where (MD.MSG_DEF_ID in ('SOCKHEAD', 'DSPHEAD', 'ABCSHEAD'));
    if (pnHeadlen > 1000) Then
      pnHeadlen := 996;
    End if;
    return pnHeadlen;
  Exception
    When others then
      Return 337 + 659;
  END IBPS_GET_MSGHEADLen;

  FUNCTION IBPS_Set_Receipt_Branch(pBranCode Varchar2,
                                   iSIBSCode number default 1)
    RETURN VARCHAR2 IS
    pReturnCode Varchar2(10);
  BEGIN
    if iSIBSCode = 1 then
      select TAD.SIBS_BANK_CODE
        INTO pReturnCode
        from TAD
       Where TAD.GW_BANK_CODE = pBranCode
         And Rownum = 1;
      Return pReturnCode;
    else
      select TAD.SIBS_CODE
        INTO pReturnCode
        from TAD
       Where TAD.GW_BANK_CODE = pBranCode
         And Rownum = 1;
      Return pReturnCode;
    
    end if;
  EXCEPTION
    When Others Then
      BEGIN
        select TAD.SIBS_BANK_CODE
          INTO pReturnCode
          From TAD
         Where TAD.Function in ('1', '3')
           And Rownum = 1;
        Return pReturnCode;
      EXCEPTION
        When Others Then
          Return '00011';
      END;
  END;

  FUNCTION PUMPIN(pFileContent varchar2,
                  pGWPos       integer,
                  pLength      Integer,
                  pDataType    Integer,
                  pMSGContent  Varchar2) return Varchar2 IS
    pclobMSGReturn clob;
    clobPart1      clob;
    clobPart2      clob;
    ilen           integer := 0;
    ilen1          Integer := 0;
    chrReturn      varchar2(4000);
  
  BEGIN
  
    chrReturn := pFileContent;
    chrReturn := Replace(chrReturn, chr(10), ' ');
    chrReturn := Replace(chrReturn, chr(13), ' ');
  
    if pDataType = 0 then
      if trim(pFileContent) is null then
        --neu la kieu chuoi them ky tu rong vao phia sau
        chrReturn := ' ';
        chrReturn := RPAD(chrReturn, pLength, ' ');
      else
        --neu la kieu chuoi them ky tu rong vao phia sau
        chrReturn := Trim(chrReturn);
        chrReturn := RPAD(chrReturn, pLength, ' ');
      end if;
    
    else
      if TRim(pFileContent) is null then
        --neu la kieu chuoi them ky tu rong vao phia sau
        chrReturn := '0';
        chrReturn := LPAD(chrReturn, pLength, '0');
      else
        chrReturn := Trim(chrReturn);
        --neu la kieu chuoi them ky tu rong vao phia sau
        chrReturn := LPAD(chrReturn, pLength, '0');
      end if;
    end if;
  
    ilen := length(pMSGContent);
  
    If (pGWPos = 1) then
      clobPart2      := DBMS_LOB.substr(pMSGContent,
                                        ilen - pLength,
                                        pGWPos + pLength);
      pclobMSGReturn := To_clob(chrReturn) || clobPart2;
    
    else
      clobPart2 := DBMS_LOB.substr(pMSGContent,
                                   ilen - pGwPos - pLength,
                                   pGWPos + pLength);
    
      clobPart1      := DBMS_LOB.substr(pMSGContent, pGWPos - 1, 1);
      ilen1          := Length(clobPart1);
      pclobMSGReturn := clobPart1 || To_clob(chrReturn) || clobPart2;
      if length(chrReturn) <> pLength then
        clobPart1 := 'loi mat roi';
      end if;
      if ilen1 <> pGWPos - 1 then
        clobPart1 := 'loi mat roi';
      end if;
    end if;
  
    return pclobMSGReturn;
  EXCEPTION
    WHEN OTHERS Then
      clobPart1 := Sqlcode || sqlerrm;
  END PUMPIN;

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

  -- lay gia tri tu tang cua so RM
  FUNCTION GET_RMNUMBER(pbankcode varchar2) return varchar2 IS
    nSIBSNum integer;
    RefNum   varchar2(20) := '0';
    vDate    varchar2(8);
  
  BEGIN
    Begin
    
      SELECT Value, Note
        Into RefNum, vDate
        FROM SYSVAR
       WHERE VARNAME = 'IBPSSeqNum_New'
         and GWTYPE = 'IBPS';
    
      if (vDate = to_char(sysdate, 'YYYYMMDD')) then
        nSIBSNum := to_number(RefNum) + 1;
      else
        nSIBSNum := 1;
        vDate    := to_char(sysdate, 'YYYYMMDD');
      end if;
    
      Update Sysvar
         set Value = nSIBSNum, Note = vDate
       where VARNAME = 'IBPSSeqNum_New'
         and GWTYPE = 'IBPS';
      commit;
    exception
      when others then
        nSIBSNum := 1;
        vDate    := to_char(sysdate, 'YYYYMMDD');
        Update Sysvar
           set Value = nSIBSNum, Note = vDate
         where VARNAME = 'IBPSSeqNum_New'
           and GWTYPE = 'IBPS';
        commit;
    end;
    return '9011' || to_char(Sysdate, 'YYMMDD') || LPAD(nSIBSNum, 5, '0');
  
  exception
    when others then
      return '';
  end;

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

  -- Ham kiem tra xem ma chi nhanh co phai la BIDV khong

  Function Check_BIDVBranch(pBrach_code varchar2) return boolean IS
    vBIDVCode varchar2(6);
  
  Begin
    select sysvar.value
      into vBIDVCode
      from sysvar
     where sysvar.varname = 'MCOVCODE';
    if Trim(substr(vBIDVCode, 1, 3)) = substr(pBrach_code, 3, 3) then
      return true;
    else
      return false;
    end if;
  exception
    when others then
      return true;
  end Check_BIDVBranch;

  Function DefineAccType(szInput varchar2) return varchar2 IS
  
    szAccount varchar2(25);
    szAccType varchar2(3);
    dAccount  number(19, 2);
  
  Begin
  
    szAccount := IBPS_GET_FILECONTENT(m_GW_RECEIVING_ACCOUNT, szInput);
    szAccount := GW_PK_IBPS_PROCESS.IBPS_REPLACE_Char(szAccount,
                                                      m_GWtype,
                                                      m_Depar,
                                                      ' ',
                                                      '033',
                                                      m_Direction,
                                                      m_QUERY_ID,
                                                      'IBPS_JOB_CONVERT_IN');
  
    szAccount := Trim(szAccount);
  
    szAccount := replace(szAccount, '.');
    szAccount := replace(szAccount, ',');
    szAccount := replace(szAccount, '-');
    szAccount := replace(szAccount, '_');
    szAccount := replace(szAccount, ' ');
  
    Begin
      if substr(szAccount, 1, 1) <> '0' and Length(szAccount) = 13 then
        szAccount := '0' || szAccount;
      end if;
    EXCEPTION
      when Others then
        mszTLBF09 := szAccount;
    end;
    mszTLBF09 := szAccount;
    gAcc_num  := szAccount;
  
    if (length(szAccount) < 14) then
      mszTLBF09       := '0000000000000000000';
      mszAccTypeField := GW_LA_FIELD;
      mszClosedStatus := ' ';
    else
      mszClosedStatus := 'Z';
      szAccType       := substr(szAccount, 4, 2);
      if (szAccType = GW_CA_IND or szAccType = GW_CA_IND_KN or
         szAccType = GW_CA_IND_N1) then
        if GW_PK_VCB_CONVERTIN_ABCS.CheckAccountStruct(szAccount) =
           substr(szAccount, 14, 1) then
          mszAccTypeField := GW_CA_FIELD;
        else
          mszAccTypeField := GW_LA_FIELD;
          mszClosedStatus := ' ';
        end if;
      
      else
        if (szAccType = GW_SA_IND) then
          if GW_PK_VCB_CONVERTIN_ABCS.CheckAccountStruct(szAccount) =
             substr(szAccount, 14, 1) then
            mszAccTypeField := GW_SA_FIELD;
          else
            mszAccTypeField := GW_LA_FIELD;
            mszClosedStatus := ' ';
          end if;
        
        else
          mszTLBF09       := '0000000000000000000';
          mszAccTypeField := m_GW_LA_FIELD;
          mszClosedStatus := ' ';
        end if;
      end if;
    
    end if;
  
    return mszTLBF09;
  
  End DefineAccType;

end GW_PK_IBPS_Q_CONVERTIN;
/
