create or replace package GW_PK_VCB_OL2_CONVERTOUT is

  Procedure VCB_EQ_CONVERT_OUT(vQUERY_ID NUMBER,
                               vRDBR     VARCHAR2,
                               vTLBF01   VARCHAR2,
                               vRMBENA   VARCHAR2,
                               vRMAMT    VARCHAR2,
                               vRMCURR   VARCHAR2,
                               vRMSNME   VARCHAR2,
                               vRMACNO   VARCHAR2,
                               vTLBAFM   VARCHAR2,
                               vTLBRMK   VARCHAR2,
                               vTLBF09   VARCHAR2,
                               vTLBID    VARCHAR2,
                               vRMPRDC   VARCHAR2,
                               vRDEFTH   VARCHAR2,
                               vRMPB40   VARCHAR2,
                               vTLBDEL   VARCHAR2,
                               vTLBCOR   VARCHAR2,
                               vRMDIS7   VARCHAR2,
                               vRMSTS7   VARCHAR2,
                               vRDRACT   VARCHAR2,
                               vDEFAULT1 VARCHAR2,
                               vDEFAULT2 VARCHAR2,
                               vDEFAULT3 VARCHAR2,
                               vDEFAULT4 VARCHAR2,
                               vDEFAULT5 VARCHAR2);
  PROCEDURE VCB_DE_CONVERTOUT;

  FUNCTION GET_FIELD20 return varchar2;
  PROCEDURE VCB_INSERT_TABLE(pVCB_CONTENT IN VCB_MSG_CONTENT%ROWTYPE);
  FUNCTION VCBCheckDup(pRMACNO NUMBER, pDATE_NOW varchar2) return NUMBER;
  Function BIDV_ACCOUNT(pCCYCD varchar2) return varchar2;
  Function VCB_currency(pCCYCD varchar2) return number;

end GW_PK_VCB_OL2_CONVERTOUT;
/
create or replace package body GW_PK_VCB_OL2_CONVERTOUT is

  RPLChar        GW_PK_VCB_PROCESS.RPLCharType;
  pMSG_TYPE      varchar2(5) := 'MT103';
  pMSG_DIRECTION varchar2(8) := 'SIBS-VCB';
  pDEPARTMENT    varchar2(2);
  mChar          char(2) := CHR(13) || chr(10);

  Procedure VCB_EQ_CONVERT_OUT(vQUERY_ID NUMBER,
                               vRDBR     VARCHAR2,
                               vTLBF01   VARCHAR2,
                               vRMBENA   VARCHAR2,
                               vRMAMT    VARCHAR2,
                               vRMCURR   VARCHAR2,
                               vRMSNME   VARCHAR2,
                               vRMACNO   VARCHAR2,
                               vTLBAFM   VARCHAR2,
                               vTLBRMK   VARCHAR2,
                               vTLBF09   VARCHAR2,
                               vTLBID    VARCHAR2,
                               vRMPRDC   VARCHAR2,
                               vRDEFTH   VARCHAR2,
                               vRMPB40   VARCHAR2,
                               vTLBDEL   VARCHAR2,
                               vTLBCOR   VARCHAR2,
                               vRMDIS7   VARCHAR2,
                               vRMSTS7   VARCHAR2,
                               vRDRACT   VARCHAR2,
                               vDEFAULT1 VARCHAR2,
                               vDEFAULT2 VARCHAR2,
                               vDEFAULT3 VARCHAR2,
                               vDEFAULT4 VARCHAR2,
                               vDEFAULT5 VARCHAR2) IS
    queue_options      DBMS_AQ.ENQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(16);
    my_message         VCB_TYPE_CONVERTOUT_OL2;
  BEGIN
  
    --start cai queue len
    DBMS_AQADM.start_queue('VCB_Q_CONVERTOUT_OL2', TRUE, TRUE);
  
    my_message := VCB_TYPE_CONVERTOUT_OL2(vQUERY_ID,
                                          vRDBR,
                                          vTLBF01,
                                          vRMBENA,
                                          vRMAMT,
                                          vRMCURR,
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
                                          vRDRACT,
                                          vDEFAULT1,
                                          vDEFAULT2,
                                          vDEFAULT3,
                                          vDEFAULT4,
                                          vDEFAULT5);
  
    DBMS_AQ.ENQUEUE(queue_name         => 'VCB_Q_CONVERTOUT_OL2',
                    enqueue_options    => queue_options,
                    message_properties => message_properties,
                    payload            => my_message,
                    msgid              => message_id);
    COMMIT;
  
  Exception
    when OTHERS THEN
      GW_PK_VCB_PROCESS.VCB_Msg_Trace(sysdate,
                                      vQUERY_ID,
                                      -1,
                                      'VCB_EQ_CONVERTOUT' || SQLERRM,
                                      'IBPS_OL3_JOB_INQUITYOUT');
    
  END VCB_EQ_CONVERT_OUT;

  PROCEDURE VCB_DE_CONVERTOUT IS
  
    queue_options      DBMS_AQ.DEQUEUE_OPTIONS_T;
    message_properties DBMS_AQ.MESSAGE_PROPERTIES_T;
    message_id         RAW(5000);
    my_message         VCB_type_convertout_ol2;
  
    new_messages Boolean := True;
    next_trans EXCEPTION;
    no_messages EXCEPTION;
    pragma exception_init(next_trans, -25235);
    pragma exception_init(no_messages, -25228);
    i integer;
    --Bien lay du lieu tu Queue   
    m_vRDBR         VARCHAR2(10);
    m_vTLBF01       VARCHAR2(20);
    m_vRMBENA       VARCHAR2(20);
    m_vRMAMT        VARCHAR2(20);
    m_vRMCURR       VARCHAR2(3);
    m_vRMSNME       VARCHAR2(100);
    m_vRMACNO       VARCHAR2(20);
    m_vTLBAFM       VARCHAR2(2000);
    m_vTLBRMK       VARCHAR2(2);
    m_vTLBF09       VARCHAR2(14);
    m_vTLBID        VARCHAR2(8);
    m_vRMPRDC       VARCHAR2(1);
    m_vRDEFTH       VARCHAR2(250);
    m_vRMPB40       VARCHAR2(50);
    m_vTLBDEL       VARCHAR2(15);
    m_vTLBCOR       VARCHAR2(1);
    m_vRMDIS7       VARCHAR2(8);
    m_vRMSTS7       VARCHAR2(8);
    m_vPRODUCT_TYPE varchar2(5);
    m_vRDRACT       VARCHAR2(14);
    --Cac truong default co the them vao
    m_vDEFAULT1 VARCHAR2(200);
    m_vDEFAULT2 VARCHAR2(200);
    m_vDEFAULT3 VARCHAR2(200);
    m_vDEFAULT4 VARCHAR2(200);
    m_vDEFAULT5 VARCHAR2(200);
  
    pRowVCB VCB_MSG_CONTENT%Rowtype;
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
    
    --Minhhb them truong 57E cho cac dien di VCB
    m_vF57E varchar2(70);
  
  BEGIN
    DBMS_AQADM.start_queue('VCB_Q_CONVERTOUT_OL2', TRUE, TRUE);
    queue_options.wait := 1;
    i                  := 0;
    select to_char(sysdate, 'YYMMDD') into m_vDATEYMD from dual;
    select count(1) into i from VCB_TB_Q_OL2;
    if i = 0 then
      return;
    end if;
    i := 0;
    While ((new_messages) and i < 1000) LOOP
      Begin
        DBMS_AQ.DEQUEUE(queue_name         => 'VCB_Q_CONVERTOUT_OL2',
                        dequeue_options    => queue_options,
                        message_properties => message_properties,
                        payload            => my_message,
                        msgid              => message_id);
        queue_options.navigation := DBMS_AQ.NEXT;
        RPLChar                  := GW_PK_VCB_PROCESS.LoadReplaceChar(pMSG_TYPE,
                                                                      'SIBS-VCB'); --Ky tu la
      
        select SEQ_IBPS_QUERY.NEXTVAL INTO m_vQUERY_ID From dual;
        m_vNOSTRO := '';
      
        m_vRDBR    := my_message.RDBR;
        m_vTLBF01  := my_message.TLBF01;
        m_vRMBENA  := replace(my_message.RMBENA, ' ', '');
        m_vRMAMT   := my_message.RMAMT;
        m_vRMCURR  := my_message.rmcurr;
        m_vRMSNME  := my_message.RMSNME;
        m_vRMACNO  := my_message.RMACNO;
        m_vTLBAFM  := my_message.TLBAFM;
        m_vTLBRMK  := my_message.TLBRMK;
        m_vTLBF09  := my_message.TLBF09;
        m_vTLBID   := my_message.TLBID;
        m_vRMPRDC  := my_message.RMPRDC;
        m_vRDEFTH  := my_message.RDEFTH;
        m_vRMPB40  := GW_PK_VCB_PROCESS.RplaceChar(my_message.RMPB40,
                                                   '57',
                                                   1,
                                                   RPLChar);
        m_vRMPB40  := my_message.RMPB40;
        m_vTLBDEL  := my_message.TLBDEL;
        m_vTLBCOR  := my_message.TLBCOR;
        m_vRMDIS7  := my_message.RMDIS7;
        m_vRMSTS7  := my_message.RMSTS7;
        m_vRDRACT  := my_message.RDRACT;
        m_vPRODUCT := trim(substr(my_message.TLBAFM, 38, 5));
        --Phan default
        m_vDEFAULT1 := my_message.DEFAULT1;
        m_vDEFAULT2 := my_message.DEFAULT2;
        m_vDEFAULT3 := my_message.DEFAULT3;
        m_vDEFAULT4 := my_message.DEFAULT4;
        m_vDEFAULT5 := my_message.DEFAULT5;
        -----------------------------------------------
        --CONVERT
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
      
        /*else
          m_vF50K := ':50K:' || substr(m_vTLBAFM, 282, 40) || mChar;
        end if;*/
        /*if trim(m_vRMCURR) = 'VND' then
          m_vF53B := ':53B:/0681000012578' || mChar;
        end if;
        if trim(m_vRMCURR) = 'USD' then
          m_vF53B := ':53B:/0681370012414' || mChar;
        end if;
        if trim(m_vRMCURR) = 'EUR' then
          m_vF53B := ':53B:/0681140012429' || mChar;
        end if;
        if trim(m_vRMCURR) = 'GBP' then
          m_vF53B := ':53B:/0681356666661' || mChar;
        end if;
        if trim(m_vRMCURR) = 'AUD' then
          m_vF53B := ':53B:/0681520012461' || mChar;
        end if;*/
      
        m_vF57D := ':57D:' || m_vRMPB40 || mChar;
        vF591   := trim(substr(m_vTLBAFM, 558, 40));
      
        vF592 := replace(trim(substr(m_vTLBAFM, 641, 75)), '     ', '');
        begin
          vF593 := trim(substr(M_vTLBAFM, 978, 40));
        exception
          when others then
            vF593 := '';
        end;
        -- 59
        m_vRMBENA := GW_PK_VCB_PROCESS.RplaceChar(m_vRMBENA,
                                                  '59',
                                                  1,
                                                  RPLChar);
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
          --Minhhb comment out truong 59 chi chua tai khoan va ten nguoi huong
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
        m_vRDEFTH := GW_PK_VCB_PROCESS.RplaceChar(m_vRDEFTH,
                                                  '70',
                                                  3,
                                                  RPLChar);
        vF70      := substr(trim(m_vRDEFTH), 4, 217);
        vF702     := trim(vSCMT || ' ' || vNC_NC);
      
        if trim(vF702) is null then
          /*m_vF70 := ':70:(' || m_vTLBF01 || ')' || trim(vF70) || mChar;*/
          m_vF70 := ':70:' || trim(vF70) || mChar;
        else
          /*m_vF70 := ':70:(' || m_vTLBF01 || ')' || trim(vF70) || '(' ||
          vF702 || ')' || mChar;*/
          m_vF70 := ':70:' || trim(vF70) || '(' || vF702 || ')' || mChar;
        end if;
        m_vF71A    := ':71A:OUR';
        -- Minhhb Them truong 57E
        m_vCONTENT := m_vF20 || m_vF32A || m_vF50K || m_vF53B || m_vF57D ||m_vF57E||
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
            if length(trim(m_vF70)) > 128 then
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
        pRowVCB.MSG_SRC           := 1;
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
        WHEN next_trans THEN
          queue_options.navigation := DBMS_AQ.NEXT_TRANSACTION;
        WHEN no_messages THEN
          new_messages := FALSE;
        When others then
          GW_PK_VCB_PROCESS.VCB_Msg_Trace(sysdate,
                                          m_vQUERY_ID,
                                          -1,
                                          'VCB_DE_CONVERTOUT' || SQLERRM,
                                          'VCB_OL2_JOB_CONVERTOUT');
          GW_PK_OL3_CONVERTOUT.IBPS_OL3_Update(m_vQUERY_ID, -1, 'OL2');
      end;
    
    end loop;
    commit;
  Exception
    when OTHERS THEN
      GW_PK_VCB_PROCESS.VCB_Msg_Trace(sysdate,
                                      m_vQUERY_ID,
                                      -1,
                                      'VCB_EQ_CONVERTOUT' || SQLERRM,
                                      'VCB_OL2_JOB_CONVERTOUT');
      GW_PK_OL3_CONVERTOUT.IBPS_OL3_Update(m_vQUERY_ID, -1, 'OL2');
    
  END VCB_DE_CONVERTOUT;

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

end GW_PK_VCB_OL2_CONVERTOUT;
/
