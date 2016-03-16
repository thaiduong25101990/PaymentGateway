create or replace package GW_PK_IBPS_DBLINK_CITAD_2_1 is

  -- Author  : ADMINISTRATOR
  -- Created : 7/23/2008 10:45:29 AM
  -- Purpose : DN cac ham xu ly dien di cua kenh TT IBPS DBLINK
  Procedure GET_IBPS_CONTENT;
  Procedure SendMessage_DBLINKD;

  FUNCTION GetDBLINKNAme(pGWBankcode varchar2) return varchar2;
  FUNCTION ExecuteSQLNonReturn(strSQL varchar2) RETURN boolean;
  FUNCTION GET_DBLINK_DATA(pCOntent varchar2) Return ibps_dblink_out;
  FUNCTION GET_IBPS_Field(pCOntent varchar2, FiledCode varchar2)
    Return Varchar2;
  PROCEDURE IBPS_CONTENT_UPDATE(pMSG_ID      number,
                                pstatus      number,
                                psendingtime date);

  PROCEDURE IBPS_CONTENT_UPDATE_Query(pMSG_ID      number,
                                      pstatus      number,
                                      psendingtime date);

  Function CheckDBLINKTAD(pDblink varchar2) return boolean;
  Procedure Drop_Job;
  /*Procedure CREATE_PROGRAM(pProName    varchar2,
  pFunction   varchar2,
  pChain_name varchar2,
  pStepChain  varchar2,
  pChainRule  varchar2);*/
  Procedure Create_Job;
  function build_char(p_str in varchar2) return varchar2;
  FUNCTION ConvertMSGTax(contentTax varchar2,
                         TCQT       varchar2,
                         transdate  varchar2,
                         msg_type   varchar2,
                         isErr      out number) return varchar2;
  FUNCTION GET_TAX_Field(pCOntent varchar2) Return Varchar2;
  FUNCTION ConvertMSGRemark(contentTax varchar2,
                            TCQT       varchar2,
                            transdate  varchar2,
                            msg_type   varchar2,
                            isErr      out number) return varchar2;
end GW_PK_IBPS_DBLINK_CITAD_2_1;
/
create or replace package body GW_PK_IBPS_DBLINK_CITAD_2_1 is
  -- Khai bao tham so
  m_Direction    varchar2(10) := 'SIBS-IBPS';
  m_Type_MSG_log IBPS_MSG_LOG%rowtype;
  --m_pnHeadfnPos  integer;
  iDBlink integer := 0;
  --pTad           varchar2(5);
  pQUERY_ID number(20);

  -- Nguoi tao          :QuanLD
  -- Muc dich           :lay du lieu tu tu bang content de day vao queue IBPS_DBLINK_Q_ConvertIN
  -- Ten ham            :GET_IBPS_CONTENT()
  -- Tham so           
  --                    
  -- Gis tri tra ve: Day du lieu vao 1 bang table queue

  -- Ngay tao:23/07/2008

  Procedure GET_IBPS_CONTENT IS
    cursor curMSG_OUT is
    
      SELECT IBPSMSG.MSG_ID,
             IBPSMSG.QUERY_ID,
             IBPSMSG.GW_TRANS_NUM,
             IBPSMSG.TAD,
             IBPSMSG.CONTENT,
             IBPSMSG.Department,
             IBPSMSG.Trans_Date,
             IBPSMSG.Transdate,
             IBPSMSG.Content_Tax,
             IBPSMSG.MSG_SRC,
             IBPSMSG.DESC_TAX
      
        From IBPS_Msg_Content IBPSMSG
       WHERE MSG_DIRECTION = 'SIBS-IBPS'
         and Status = '0'
         and RowNum <= 1000
         AND IBPSMSG.transdate = to_char(sysdate, 'YYYYMMDD')
         AND LTRIM(IBPSMSG.TAD, '0') in
             (Select LTRIM(TAD.SIBS_CODE, '0')
                FROM TAD
               Where TAD.Connection in (1, 5)
                 and status = 1)
       Order by IBPSMSG.TAD;
  
    tTypeDBlink ibps_dblink_out;
  
    v_MSG_OUT     curMSG_OUT%rowtype;
    v_tad         varchar2(5);
    vCONTENT      varchar2(4000);
    dTRANS_DATE   date;
    vGW_TRANS_NUM number(10);
    vDept         varchar2(3);
    v_non_sales   integer := 0;
    v_GWbankcode  varchar2(12);
    v_referent    varchar(100) := '';
    v_Amount      varchar(30);
  
    v_ContentTax   varchar2(3000);
    iErrTax        varchar2(20);
    v_rv_code      varchar2(20) := '';
    isLNH          number(1) := 0;
    lnhTr          varchar2(10) := '';
    LNH_TRANS_TYPE varchar2(100) := '';
    LNH_INTEREST   varchar2(100) := '';
    LNH_CYCLE      varchar2(100) := '';
    LNH_GTCG       varchar2(100) := '';
    vDESC_TAX      varchar2(800) := '';
    LNH_TRANS_Date varchar2(100) := '';
    vTaxAccCode     varchar2(2000);
    vTaxAccCode7111 varchar2(2000);
  BEGIN
    -- Kiem tra xem kenh TT hoac cong TAd co cho di DBLINK hay khong
    begin
      dbms_scheduler.purge_log;
    exception
      when others then
        iDBlink := 0;
    end;
    OPEN curMSG_OUT;
    LOOP
    
      isLNH    := 0;
      v_tad    := '';
      vCONTENT := '';
    
      vGW_TRANS_NUM := 0;
      vDept         := '';
      v_non_sales   := 0;
      v_GWbankcode  := '';
      v_referent    := '';
      v_Amount      := '';
      v_ContentTax  := '';
      iErrTax       := '';
      v_rv_code     := '';
    
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH curMSG_OUT
        INTO v_MSG_OUT;
    
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      EXIT WHEN curMSG_OUT %notfound;
      pQUERY_ID     := v_MSG_OUT.Query_Id;
      vCONTENT      := trim(v_MSG_OUT.Content);
      dTRANS_DATE   := v_MSG_OUT.Trans_Date;
      vDept         := TRIM(v_MSG_OUT.DEPARTMENT);
      vGW_TRANS_NUM := v_MSG_OUT.Gw_Trans_Num;
      v_tad         := v_MSG_OUT.Tad;
    
      lnhTr          := '';
      LNH_TRANS_TYPE := '';
      LNH_INTEREST   := '';
      LNH_CYCLE      := '';
      LNH_GTCG       := '';
    
      begin
      
        select trim(IBM.GW_BANK_CODE)
          into v_GWbankcode
          from IBPS_BANK_MAP IBM
         where ibm.sibs_bank_code = LPAD(v_tad, 5, '0')
           And rownum = 1;
      
        select TAD.CONNECTION
          into iDBlink
          from TAD
         where TAD.GW_BANK_CODE = v_GWbankcode
           And rownum = 1;
        /* (select IBM.GW_BANK_CODE
              from IBPS_BANK_MAP IBM
             where ibm.sibs_bank_code = LPAD(v_tad, 5, '0'))
        */
      
      Exception
        when others then
          m_Type_MSG_log.Log_Date      := Sysdate;
          m_Type_MSG_log.Query_Id      := 0;
          m_Type_MSG_log.Msg_Direction := m_Direction;
          m_Type_MSG_log.Status        := 0;
          m_Type_MSG_log.Descriptions  := 'ERROR WHEN GET CONNECTION IN table GWTYPE ' ||
                                          Sqlcode || ' -Error- ' || Sqlerrm;
          m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_INQUIRYOUT';
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
          REturn;
      end;
    
      if iDBlink = 1 or iDBlink = 5 then
        -- lay ten DBlink
        tTypeDBlink := GET_DBLINK_DATA(vCONTENT);
        -- day du lieu vao Queue
        -- Cap nhat lai dien vao bang IBPS_MSG_CONTENT
      
        begin
          v_Amount := to_char(to_number(tTypeDBlink.F27) / 100);
        exception
          when others then
            v_Amount := substr(tTypeDBlink.F27,
                               1,
                               length(tTypeDBlink.F27) - 2);
        end;
      
        Begin
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
        
          begin
            vDESC_TAX := v_MSG_OUT.Desc_Tax;
             if instr(vTaxAccCode7111, ';' || tTypeDBlink.F33 || ';') > 0 then
              if v_MSG_OUT.Msg_Src = '4' then
                v_referent      := 'IBPSVST000';
                tTypeDBlink.F33 := '7111';
                v_ContentTax    := ConvertMSGTax(v_MSG_OUT.Desc_Tax,
                                                 tTypeDBlink.f31,
                                                 substr(tTypeDBlink.f12,
                                                        1,
                                                        8),
                                                 v_MSG_OUT.Msg_Src,
                                                 iErrTax);
                --  tTypeDBlink.F34 := 'NSNN';
              
              else
                v_referent   := '';
                v_ContentTax := v_MSG_OUT.Content_Tax;
                vDESC_TAX    := 'TAXFROMBDS';
              
              end if;
            
            else
              -- ddien di Tren BDS
              v_referent := '';
            
            end if;
          exception
            when others then
              v_referent   := '';
              v_ContentTax := v_MSG_OUT.Content_Tax;
          end;
          /*end if;*/
        -- loai 3942
          if instr(vTaxAccCode, ';' || tTypeDBlink.f33 || ';') > 0 then
            v_ContentTax := '';
            v_referent   := '';
          
          end if;
        
          --lnhtr:=   in GET_IBPS_Field(pCOntent varchar2, FiledCode varchar2)
          -- them thong tin cho thi truong lien ngan hang
          if (v_MSG_OUT.Department = 'TR') then
            if instr(v_MSG_OUT.Content, '##099#') > 0 then
              /*666' || LNH_TRANS_TYPE || '#667' ||
              LNH_INTEREST || '#668' || SMO.LNH_CYCLE || '#669' ||
              SMO.LNH_GTCG*/
            
              LNH_TRANS_TYPE := GET_IBPS_Field(v_MSG_OUT.Content, '666');
              LNH_INTEREST   := GET_IBPS_Field(v_MSG_OUT.Content, '667');
            
              LNH_CYCLE    := GET_IBPS_Field(v_MSG_OUT.Content, '668');
              LNH_INTEREST := Lpad(trim(replace(to_char(LNH_INTEREST,
                                                        '999.99'),
                                                '.',
                                                ',')),
                                   5,
                                   '0');
            
              LNH_TRANS_Date := GET_IBPS_Field(v_MSG_OUT.Content, '670');
            
              LNH_GTCG        := GET_IBPS_Field(v_MSG_OUT.Content, '669');
              v_rv_code       := tTypeDBlink.F21;
              tTypeDBlink.F36 := 30; -- Ma loai nghiep vu v_opert1,
              tTypeDBlink.F37 := LNH_TRANS_TYPE;
            
              /*          
              yyyymmdd
              lai suat: 4
              ky han: 4
              loai tien:2
              loai giay to co gia*/
            
              v_referent := LNH_TRANS_Date ||
                            LPAD(replace(LNH_INTEREST, '.', ','), 5, '0') ||
                            LPAD(LNH_CYCLE, 4, '0') || '00' ||
                            LPAD(LNH_GTCG, 4, '0');
            
            end if;
          end if;
          -- het them thong tin thi truwowng lien ngan hang
        
          UPDATE IBPS_MSG_CONTENT
             SET STATUS      = 2,
                 content_tax = v_ContentTax,
                 DESC_TAX    = vDESC_TAX
           where MSG_ID = v_MSG_OUT.Msg_Id;
          commit;
        
          if subStr(trim(tTypeDBlink.F33), 1, 4) != '7111' then
            v_ContentTax := '';
          
          end if;
        
          insert into tbltransactionmsg_gtw
            (check_code,
             create_file_result_flag,
             file_name_result,
             trx_type,
             err_status,
             sd_time,
             response_code,
             serial_no,
             o_ci_code,
             r_ci_code,
             o_indirect_code,
             r_indirect_code,
             fee_ci_code,
             trx_date,
             balance_time,
             currency,
             amount,
             sd_name,
             sd_addr,
             sd_accnt,
             rv_name,
             rv_addr,
             rv_accnt,
             content,
             opert1,
             opert2,
             file_name,
             relation_no,
             sd_identify,
             rv_identify,
             authorized,
             fee_flag,
             reference,
             tax_code,
             sd_code,
             rv_code,
             ex_e_sign,
             msg_reason,
             orig_id,
             confirm_id,
             appr_id,
             create_time,
             appr_time,
             mac,
             type_flag,
             e_sign,
             optioncode,
             contentfromfile,
             err_msg,
             lineposition,
             fileprocessingtime,
             content_ex,
             gw_status,
             gw_direction,
             gw_org_account,
             gw_note,
             msg_id,
             query_id)
          values
            ('00', --v_check_code,
             '0', --v_create_file_result_flag,
             '', --v_file_name_result,
             tTypeDBlink.F3, --v_trx_type Gia tri thap , gia tri cao
             '', -- v_err_status,
             tTypeDBlink.F12, --v_sd_time,
             '', --v_response_code,
             '', -- v_serial_no,
             tTypeDBlink.F21, -- ma ngan hang gui dien v_o_ci_code,
             tTypeDBlink.F22, -- Ma ngan hang nhan dien v_r_ci_code,
             tTypeDBlink.F7, -- Ma ngan hang gui gian tiep v_o_indirect_code,
             tTypeDBlink.f19, -- Ma ngan hang nhan gian tiep
             '', -- Ma ngan hang chiu phi v_fee_ci_code,
             substr(tTypeDBlink.F12, 1, 8), -- Ngay giao dich v_trx_date,
             '', --v_balance_time,
             tTypeDBlink.F26, -- Loai tien v_currency,
             v_Amount, -- So tien v_amount,
             tTypeDBlink.F28, -- Ten nguwoi gui v_sd_name,
             tTypeDBlink.F29, -- Dia chi nguoi gui v_sd_addr,
             tTypeDBlink.F30, -- So tai khoan nguoi gui v_sd_accnt,
             tTypeDBlink.F31, -- Ten nguoi nhan v_rv_name,
             tTypeDBlink.F32, -- Dia chi nguoi nhan v_rv_addr,
             tTypeDBlink.f33, -- Tai khoan nguoi nhan v_rv_accnt,
             tTypeDBlink.F34, -- Noi dung thanh toan v_content,
             tTypeDBlink.F36, -- Ma loai nghiep vu v_opert1,
             tTypeDBlink.F37, -- Ma loai nghiep vu 2 v_opert2,
             '', --- Ten file dien v_file_name,
             LPAD(tTypeDBlink.F110, 8, '0'), -- So but toan v_relation_no,
             '', -- thong tin giay to kem theo cua nguoi gui v_sd_identify,
             '', -- thong tin giay to kem theo cua nguoi nhan  v_rv_identify,
             '0', -- thong tin xac d?nh chuyen no v_authorized,
             '', ---             v_fee_flag,
             
             v_referent,
             '', --v_tax_code,
             '', -- v_sd_code,
             v_rv_code, -- v_rv_code,
             '', -- v_ex_e_sign,
             '', -- v_msg_reason,
             '', -- v_orig_id,
             '', -- v_confirm_id,
             '', -- v_appr_id,
             tTypeDBlink.F12, --v_create_time,
             tTypeDBlink.F12, --v_appr_time,
             '', -- v_mac,
             '0', -- v_type_flag,
             '', --v_e_sign,
             '', --v_optioncode,
             '', --v_contentfromfile,
             '', --v_err_msg,
             '', --v_lineposition,
             '', --v_fileprocessingtime,
             v_ContentTax, --v_content_ex,
             '0', --v_gw_status,
             'O', -- v_gw_direction,
             '', -- v_gw_org_account,
             '',
             v_MSG_OUT.Msg_Id,
             v_MSG_OUT.Query_Id) -- v_gw_note);
          
          ;
          commit;
          --IBPS_CONTENT_UPDATE(v_MSG_OUT.Msg_Id, 1, sysdate);
        Exception
          when others then
            vCONTENT := sqlerrm;
            IBPS_CONTENT_UPDATE(v_MSG_OUT.Msg_Id, 0, null);
        end;
        v_non_sales := v_non_sales + 1;
      End if;
      commit;
    END LOOP;
    -- ?ong cursor 
    CLOSE curMSG_OUT;
  EXCEPTION
    when others then
    
      m_Type_MSG_log.Log_Date      := Sysdate;
      m_Type_MSG_log.Query_Id      := pQUERY_ID;
      m_Type_MSG_log.Msg_Direction := m_Direction;
      m_Type_MSG_log.Status        := 0;
      m_Type_MSG_log.Descriptions  := 'ERROR WHEN Query Message' || Sqlcode ||
                                      ' -Error- ' || Sqlerrm;
      m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_INQUIRYOUT';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
      IBPS_CONTENT_UPDATE(v_MSG_OUT.Msg_Id, -1, null);
    
  END GET_IBPS_CONTENT;

  --Send du lieu sang DBlink

  --Send du lieu sang DBlink

  Procedure SendMessage_DBLINKD IS
    vdblinkName  varchar2(50);
    vdGWbankName varchar2(12);
  
    vSqlStr      varchar2(1000);
    vSqlStrValue varchar2(4000);
  
    cursor curTad is
      Select TAD.GW_BANK_CODE, TAD.Dblink_Name, TAD.Sibs_Bank_Code
        from TAD
       where (TAD.Connection = 1 or TAD.Connection = 5)
         and TAD.Status = 1;
    v_curTad curTad%rowtype;
  
    cursor curtblImport IS
      select *
        from Tbltransactionmsg_Gtw imp
       where imp.o_ci_code = vdGWbankName
         and imp.gw_status = 0;
  
    v_curImport curtblImport%rowtype;
    i           integer;
  
  Begin
  
    OPEN curTad;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH curTad
        INTO v_curTad;
    
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      EXIT WHEN curTad %notfound;
      vdGWbankName := v_curTad.Gw_Bank_Code;
      vdblinkName  := v_curTad.Dblink_Name;
      i            := 0;
      if checkDBlinkTAD(vdblinkName) = true then
        OPEN curtblImport;
        LOOP
          BEGIN
            -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
            FETCH curtblImport
              INTO v_curImport;
            -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
            EXIT WHEN curtblImport %notfound;
            if vDBLINKName is not null then
            
              /*   vSqlStr      := 'INSERT INTO tbltransactionmsg_gtw@' ||
                                  vDBLINKName ||
                                  '(check_code, create_file_result_flag, file_name_result, trx_type, err_status, sd_time, response_code, serial_no, o_ci_code, r_ci_code, o_indirect_code, r_indirect_code, fee_ci_code, trx_date, balance_time, currency, amount, sd_name, sd_addr, sd_accnt, rv_name, rv_addr, rv_accnt, content, opert1, opert2, file_name, relation_no, sd_identify, rv_identify, authorized, fee_flag, reference, tax_code, sd_code, rv_code, ex_e_sign, msg_reason, orig_id, confirm_id, appr_id, create_time, appr_time, mac, type_flag, e_sign, optioncode, contentfromfile, err_msg, lineposition, fileprocessingtime, content_ex) VALUES (';
              */
              vSqlStr := 'INSERT INTO tbltransactionmsg_gtw@' ||
                         vDBLINKName ||
                         '(check_code, create_file_result_flag, file_name_result, trx_type, err_status, sd_time, response_code, serial_no, o_ci_code, r_ci_code, o_indirect_code, r_indirect_code, fee_ci_code, trx_date, balance_time, currency, amount, sd_name, sd_addr, sd_accnt, rv_name, rv_addr, rv_accnt, content, opert1, opert2, file_name, relation_no, sd_identify, rv_identify, authorized, fee_flag, reference, tax_code, sd_code, rv_code, ex_e_sign, msg_reason, orig_id, confirm_id, appr_id, create_time, appr_time, mac, type_flag, e_sign, optioncode, contentfromfile, err_msg, lineposition, fileprocessingtime, content_ex)';
            
              vSqlStrValue := ' select check_code, create_file_result_flag, file_name_result, trx_type, err_status, sd_time, response_code, serial_no, o_ci_code, r_ci_code, o_indirect_code, r_indirect_code, fee_ci_code, trx_date, balance_time, currency, amount, sd_name, sd_addr, sd_accnt, rv_name, rv_addr, rv_accnt, content, opert1, opert2, file_name, relation_no, sd_identify, rv_identify, authorized, fee_flag, reference, tax_code, sd_code, rv_code, ex_e_sign, msg_reason, orig_id, confirm_id, appr_id, create_time, appr_time, mac, type_flag, e_sign, optioncode, contentfromfile, err_msg, lineposition, fileprocessingtime, content_ex from tbltransactionmsg_gtw where MSG_ID=' ||
                              v_curImport.Msg_Id;
            
              if not ExecuteSQLNonReturn(vSqlStr || vSqlStrValue) then
              
                m_Type_MSG_log.Log_Date      := Sysdate;
                m_Type_MSG_log.Query_Id      := v_curImport.Msg_Id;
                m_Type_MSG_log.Msg_Direction := m_Direction;
                m_Type_MSG_log.Status        := 0;
                m_Type_MSG_log.Descriptions  := 'ERROR WHEN Insert DBLink IBPS Message' ||
                                                Sqlcode || ' -Error- ' ||
                                                Sqlerrm;
                m_Type_MSG_log.Job_Name      := 'IBPS_DBLINK_Q_CONVERTOUT';
                GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
              
                -- neu khi chay Dblink bi toi thi cap nhat lai trang thai dien =0 de 
                -- lan sau khi dblink lai chay tiep
              
                update tbltransactionmsg_gtw
                   set GW_status = 2
                 where MSG_ID = v_curImport.Msg_Id;
                IBPS_CONTENT_UPDATE_Query(v_curImport.Query_Id,
                                          -1,
                                          sysdate);
                commit;
                exit;
              else
                update tbltransactionmsg_gtw
                   set GW_status = 1
                 where MSG_ID = v_curImport.Msg_Id;
              
                IBPS_CONTENT_UPDATE_Query(v_curImport.Query_Id, 1, sysdate);
                commit;
              end if;
            
              m_Type_MSG_log.Log_Date      := Sysdate;
              m_Type_MSG_log.Query_Id      := v_curImport.Msg_Id;
              m_Type_MSG_log.Msg_Direction := m_Direction;
              m_Type_MSG_log.Status        := 1;
              m_Type_MSG_log.Descriptions  := 'Insert Message DBLink Succses';
              m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTOUT';
              GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
            end if;
            i := i + 1;
          
          EXCEPTION
            WHEN others THEN
            
              m_Type_MSG_log.Log_Date      := Sysdate;
              m_Type_MSG_log.Query_Id      := v_curImport.Msg_Id;
              m_Type_MSG_log.Msg_Direction := m_Direction;
              m_Type_MSG_log.Status        := 0;
              m_Type_MSG_log.Descriptions  := 'ERROR WHEN Enqueue Message' ||
                                              Sqlcode || ' -Error- ' ||
                                              Sqlerrm;
              m_Type_MSG_log.Job_Name      := 'IBPS_DBLINK_Q_CONVERTOUT';
              GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
              IBPS_CONTENT_UPDATE_Query(v_curImport.Msg_Id, -1, sysdate);
          END;
        
        end loop;
        close curtblImport;
      end if;
    
    end loop;
    close curTad;
  Exception
    when others then
      m_Type_MSG_log.Log_Date      := Sysdate;
      m_Type_MSG_log.Query_Id      := v_curImport.Msg_Id;
      m_Type_MSG_log.Msg_Direction := m_Direction;
      m_Type_MSG_log.Status        := 0;
      m_Type_MSG_log.Descriptions  := 'ERROR WHEN Enqueue Message' ||
                                      Sqlcode || ' -Error- ' || Sqlerrm;
      m_Type_MSG_log.Job_Name      := 'IBPS_DBLINK_Q_CONVERTOUT';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
      IBPS_CONTENT_UPDATE_Query(v_curImport.Msg_Id, -1, sysdate);
    
  End SendMessage_DBLINKD;

  -- Day du lieu vao queue

  FUNCTION GetDBLINKNAme(pGWBankcode varchar2) return varchar2 IS
    vReturn varchar2(50);
  BEGIN
    select TAD.DBLINK
      Into vReturn
      from TAD
     where TAD.GW_BANK_CODE = trim(pGWBankcode)
       And Rownum = 1;
    return vReturn;
  Exception
    When others then
      Return '';
  end GetDBLINKNAme;

  FUNCTION ExecuteSQLNonReturn(strSQL varchar2) RETURN boolean AS
    --err varchar2(1000);
  BEGIN
    execute immediate strSQL;
    COMMIT;
    return true;
  Exception
    when others then
      rollback;
    
      return false;
  END ExecuteSQLNonReturn;

  --
  FUNCTION GET_DBLINK_DATA(pCOntent varchar2) Return ibps_dblink_out IS
    IBPSDBLINKType ibps_dblink_out;
  BEGIN
  
    IBPSDBLINKType := ibps_dblink_out(pQUERY_ID,
                                      GET_IBPS_Field(pCOntent, '110'),
                                      GET_IBPS_Field(pCOntent, '003'),
                                      GET_IBPS_Field(pCOntent, '007'),
                                      GET_IBPS_Field(pCOntent, '012'),
                                      GET_IBPS_Field(pCOntent, '019'),
                                      GET_IBPS_Field(pCOntent, '021'),
                                      GET_IBPS_Field(pCOntent, '022'),
                                      GET_IBPS_Field(pCOntent, '026'),
                                      GET_IBPS_Field(pCOntent, '027'),
                                      GET_IBPS_Field(pCOntent, '028'),
                                      GET_IBPS_Field(pCOntent, '029'),
                                      GET_IBPS_Field(pCOntent, '030'),
                                      GET_IBPS_Field(pCOntent, '031'),
                                      GET_IBPS_Field(pCOntent, '032'),
                                      GET_IBPS_Field(pCOntent, '033'),
                                      GET_IBPS_Field(pCOntent, '034'),
                                      GET_IBPS_Field(pCOntent, '036'),
                                      GET_IBPS_Field(pCOntent, '037'),
                                      GET_IBPS_Field(pCOntent, '179'),
                                      GET_IBPS_Field(pCOntent, '25'),
                                      '0');
    return IBPSDBLINKType;
  END GET_DBLINK_DATA;

  FUNCTION GET_IBPS_Field(pCOntent varchar2, FiledCode varchar2)
    Return Varchar2 IS
    iposStart integer := 0;
    iposEnd   integer := 0;
    v_Value   varchar2(500);
    vTemp     varchar2(4000);
  
  BEGIN
  
    iposStart := instr(pCOntent, '#' || FiledCode);
    if iposStart = 0 then
      return '';
    end if;
    vTemp   := substr(pCOntent, iposStart + 4);
    iposEnd := instr(vTemp || '#', '#');
    v_Value := substr(vTemp, 1, iposEnd - 1);
  
    if FiledCode = '028' then
      v_Value := substr(v_Value, 1, 70);
    end if;
  
    if FiledCode = '029' then
      v_Value := substr(v_Value, 1, 70);
    end if;
  
    if FiledCode = '030' then
      v_Value := substr(v_Value, 1, 25);
    end if;
    if FiledCode = '031' then
      v_Value := substr(v_Value, 1, 70);
    end if;
    if FiledCode = '032' then
      v_Value := substr(v_Value, 1, 70);
    end if;
    if FiledCode = '033' then
      v_Value := substr(v_Value, 1, 25);
    end if;
  
    if FiledCode = '034' then
      v_Value := substr(v_Value, 1, 210);
    end if;
    return trim(v_Value);
  Exception
    when others then
      m_Type_MSG_log.Log_Date      := Sysdate;
      m_Type_MSG_log.Query_Id      := pQUERY_ID;
      m_Type_MSG_log.Msg_Direction := m_Direction;
      m_Type_MSG_log.Status        := 1;
      m_Type_MSG_log.Descriptions  := 'ERROR WHEN ' || FiledCode || v_Value ||
                                      Sqlcode || ' -Error- ' || Sqlerrm;
      m_Type_MSG_log.Job_Name      := 'IBPS_DBLINK_Q_CONVERTOUT';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
      Return '';
  END GET_IBPS_Field;

  PROCEDURE IBPS_CONTENT_UPDATE(pMSG_ID      number,
                                pstatus      number,
                                psendingtime date) IS
  BEGIN
    if psendingtime is null then
      UPDATE IBPS_MSG_CONTENT SET STATUS = pstatus where MSG_ID = pMSG_ID;
    else
      UPDATE IBPS_MSG_CONTENT
         SET STATUS = pstatus, IBPS_MSG_CONTENT.Sending_Time = psendingtime
       where MSG_ID = pMSG_ID;
    end if;
  
  EXCEPTION
    when others then
    
      rollback;
    
  END IBPS_CONTENT_UPDATE;

  PROCEDURE IBPS_CONTENT_UPDATE_Query(pMSG_ID      number,
                                      pstatus      number,
                                      psendingtime date) IS
  BEGIN
    if psendingtime is null then
      UPDATE IBPS_MSG_CONTENT
         SET STATUS = pstatus
       where QUERY_ID = pMSG_ID;
    else
      UPDATE IBPS_MSG_CONTENT
         SET STATUS = pstatus, IBPS_MSG_CONTENT.Sending_Time = psendingtime
       where QUERY_ID = pMSG_ID;
    end if;
  
  EXCEPTION
    when others then
    
      rollback;
    
  END IBPS_CONTENT_UPDATE_Query;

  Function CheckDBLINKTAD(pDblink varchar2) return boolean IS
  
    vsql varchar2(500);
  Begin
    vsql := 'select count(1) from dual@' || pDblink;
    if Gw_Pk_Lib.ExecuteSQLReturn(vsql) = '1' then
      return true;
    else
      return false;
    end if;
  Exception
    when others then
      return false;
  end CheckDBLINKTAD;

  --- Dro job dong theo nhom Group

  Procedure Drop_Job IS
  
    cursor curJobname is
      select GT.JOBNAME, GT.PERIOTY, GT.BRANCH_NAME from Group_Tad GT;
    v_curJob curJobname%Rowtype;
    vJobname varchar2(50);
  Begin
  
    OPEN curJobname;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH curJobname
        INTO v_curJob;
    
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      EXIT WHEN curJobname %notfound;
      vJobname := v_curJob.Jobname;
    
      DBMS_JOB.remove(job => vJobname);
    
    END loop;
  end;

  Procedure Create_Job IS
    istt            integer := 0;
    v_branch        varchar2(200);
    orderDblink     number(2);
    orderDblinkTemp number(2) := 0;
    vJobname        varchar2(50);
  
    cursor curTad IS
      select TAD.GW_BANK_CODE, TAD.Orderdblink
        from TAD
       where (TAD.Connection = 1 or TAD.Connection = 5)
         and status = 1
       order by TAD.Orderdblink;
  
    v_curTad curTad%rowtype;
  
    cursor curJobname is
      select GT.JOBNAME, GT.PERIOTY, GT.BRANCH_NAME from Group_Tad GT;
    v_curJob curJobname%Rowtype;
  Begin
  
    BEGIN
    
      Drop_Job;
      delete from Group_TAD;
      commit;
    
      Open curTad;
    
      loop
        FETCH curTad
          INTO v_curTad;
        -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
        EXIT WHEN curTad %notfound;
        orderDblink := v_curTad.Orderdblink;
      
        if nvl(orderDblinkTemp, 0) = 0 then
          orderDblinkTemp := orderDblink;
        end if;
        if orderDblinkTemp = orderDblink then
          if v_branch is null then
            v_branch :=  /*chr(39) ||*/
             v_curTad.Gw_Bank_Code /*|| chr(39)*/
              ;
          else
            v_branch := v_branch || ',' || /*chr(39) ||*/
                        v_curTad.Gw_Bank_Code /*||
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    chr(39)*/
             ;
          end if;
        
        else
          if v_branch is not null then
            istt := istt + 1;
            Insert into Group_TAD
              (Jobname, Perioty, Branch_Name)
            values
              (istt, istt, v_branch);
            commit;
          end if;
          v_branch := '';
        
          if v_branch is null then
            v_branch :=  /*chr(39) ||*/
             v_curTad.Gw_Bank_Code /*|| chr(39)*/
              ;
          else
            v_branch := v_branch || ',' || /*chr(39) ||*/
                        v_curTad.Gw_Bank_Code /*||
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    chr(39)*/
             ;
          end if;
        
          orderDblinkTemp := orderDblink;
        end if;
      
      end loop;
    
      if v_branch is not null then
        istt := istt + 1;
        Insert into Group_TAD
          (Jobname, Perioty, Branch_Name)
        values
          (istt, istt, v_branch);
        commit;
      end if;
    
      Close curTad;
    
      OPEN curJobname;
      LOOP
        -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
        FETCH curJobname
          INTO v_curJob;
      
        -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
        EXIT WHEN curJobname %notfound;
        vJobname := v_curJob.Jobname;
      
        DBMS_JOB.SUBMIT(vJobname,
                        'Gw_Pk_Ibps_Dblink.SendMessage_DBLINKD(' || Chr(39) ||
                        v_curJob.Branch_Name || Chr(39) || ');',
                        SYSDATE,
                        'SYSDATE + 1/1440'); -- 60 second
      
        Update Group_TAD
           set Group_TAD.Jobname = vJobname
         where Group_TAD.Branch_Name = v_curJob.Branch_Name;
        commit;
      
        /*  Dbms_Scheduler.create_job(job_name        => vJobname,
                                       job_type        => 'STORED_PROCEDURE',
                                       job_action      => 'GW_PK_IBPS_DBLINK.SendMessage_DBLINKD('|| v_curJob.Branch_Name||')' ,
                                       start_date      => systimestamp,
                                       enabled         => true,
                                       repeat_interval => 'FREQ=Secondly; Interval=2');
        */
      END loop;
    EXCEPTION
      when others then
        istt := 0;
    END;
  
  end Create_Job;

  function build_char(p_str in varchar2) return varchar2 is
    c varchar2(1) := chr(39);
  begin
    if p_str is null then
      return 'null';
    end if;
    return c || replace(replace(replace(p_str, c, c || c),
                                chr(10),
                                c || '||chr(10)||' || c),
                        '&',
                        c || '||chr(38)||' || c) || c;
  end;

  FUNCTION ConvertMSGTax(contentTax varchar2,
                         TCQT       varchar2,
                         transdate  varchar2,
                         msg_type   varchar2,
                         isErr      out number) return varchar2 IS
    vReturn   varchar2(4000);
    iposStart integer := 0;
    iposEnd   integer := 0;
    v_Value   varchar2(500);
    vTemp     varchar2(4000);
  
    dtlLTH       varchar2(3000) := ' ';
    dtlNDT       varchar2(3000) := ' ';
    dtlNDT_Value varchar2(3000) := ' ';
    v_return     varchar2(4000);
    l_xmltype    XMLTYPE;
    l_ctx        dbms_xmlgen.ctxhandle;
    MST          varchar2(100);
    CQT          varchar2(100) := ' ';
    TCQ          varchar2(100) := TCQT;
    LTH          varchar2(100) := ' ';
    NNT          varchar2(100) := transdate;
    SKH          varchar2(100) := ' ';
    SMA          varchar2(100) := ' ';
    SMH          varchar2(100) := ' ';
    STK          varchar2(100) := ' ';
    NTK          varchar2(100) := ' ';
    XNK          varchar2(100) := ' ';
    CQP          varchar2(100) := ' ';
    TKH          varchar2(100) := ' ';
  
    MCH      varchar(2000) := ' ';
    NDK      varchar(2000) := ' ';
    STN      varchar(2000) := ' ';
    NDG      varchar(3000) := ' ';
    j        number(3) := 0;
    j1       number(3) := 0;
    c        varchar2(1) := chr(39);
    v_Header varchar2(4000) := ' ';
  
    temContentTax varchar2(3000);
    ipos          number(10) := 0;
  
    itemp1  number(2);
    msgTemp varchar2(3000);
    msttt   varchar2(1000);
    icount  number(2) := 0;
  
    NTK1      varchar2(30) := '';
    NTK2      varchar2(30) := '';
    NTK3      varchar2(30) := '';
    tempValue varchar2(300) := '';
  
  BEGIN
    ipos := instr(contentTax, '>');
  
    if ipos = 0 then
      if (msg_type = 4) then
        temContentTax := contentTax;
      else
        temContentTax := replace(replace(replace(replace(replace(contentTax,
                                                                 '   ',
                                                                 ' '),
                                                         '  ',
                                                         ' '),
                                                 '  ',
                                                 ' '),
                                         '  ',
                                         ' '),
                                 ') (',
                                 ')(');
        ipos          := instr(Rtrim(trim(contentTax), ')('), ')', -1);
        if ipos > 0 then
          temContentTax := substr(contentTax, 1, ipos - 1);
        end if;
      end if;
    else
      temContentTax := substr(contentTax, 1, ipos - 1);
    end if;
  
    if (substr(trim(temContentTax), 1, 6) = '(NSNN!') then
      temContentTax := replace(temContentTax, '(NSNN!');
    end if;
  
    FOR i IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                FROM TABLE(SPLIT(temContentTax, '!'))) LOOP
    
      j := j + 1;
      if (j = 1) then
        MST := i.COLUMN_VALUE;
      end if;
    
      if (j = 2) then
        CQT := i.COLUMN_VALUE;
        BEGIN
          select trim(name_v)
            into TCQ
            from citad.TBLVST_CQT@CITAD_HSC
           where ma_cqthu = CQT
             and rownum = 1;
        
        exception
          when others then
            TCQ   := TCQT;
            isErr := 32;
            return '';
          
        end;
      end if;
    
      if (j = 3) then
        LTH := i.COLUMN_VALUE;
        if trim(LTH) is null then
          isErr := 33;
          return '';
        end if;
      end if;
    
      if (j = 4) then
        dtlLTH := i.COLUMN_VALUE;
        if (LTH = '03') then
          j1 := 0;
          FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                       FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
            tempValue := Rtrim(Ltrim(i1.COLUMN_VALUE, '('), ')');
            j1        := j1 + 1;
            if (j1 = 1) then
              SKH := tempValue;
            end if;
            if (j1 = 2) then
              SMA := tempValue;
            end if;
          END LOOP;
        end if;
      
        if (LTH = '04') then
          j1 := 0;
          SELECT count(COLUMN_VALUE)
            into icount
            FROM TABLE(SPLIT(dtlLTH, '/'));
          if icount = 5 then
            FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                         FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
              j1        := j1 + 1;
              tempValue := '';
              tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
              if (j1 = 1) then
                STK := tempValue;
              end if;
              if (j1 = 2) then
                NTK1 := tempValue;
              
              end if;
              if (j1 = 3) then
                NTK2 := tempValue;
              
              end if;
              if (j1 = 4) then
                NTK3 := tempValue;
              end if;
              if (j1 = 5) then
                XNK := tempValue;
                if (XNK = '') then
                  XNK := '999';
                end if;
              end if;
            END LOOP;
            NTK := replace(NTK3, '/') || replace(NTK2, '/') ||
                   replace(NTK1, '/');
          else
            FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                         FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
            
              j1        := j1 + 1;
              tempValue := '';
              tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
            
              if (j1 = 1) then
                STK := tempValue;
              end if;
              if (j1 = 2) then
                NTK := tempValue;
              
              end if;
              if (j1 = 3) then
                XNK := tempValue;
              
                if (XNK = '') and LTH = '04' then
                  XNK := '999';
                end if;
              end if;
            END LOOP;
          end if;
        end if;
      
        if (LTH = '07') then
          j1 := 0;
          FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                       FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
            j1 := j1 + 1;
            if (j1 = 1) then
              CQP := Rtrim(Ltrim(i1.COLUMN_VALUE, '('), ')');
            end if;
          
          END LOOP;
        end if;
        if (LTH != '07' and LTH != '03' and LTH != '04') then
          j1 := 0;
        
          dtlNDT := i.COLUMN_VALUE;
          j1     := 0;
          FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                       FROM TABLE(SPLIT(dtlNDT, '/'))) LOOP
            tempValue := '';
            tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
            j1        := j1 + 1;
          
            if (j1 = 2) then
              MCH := tempValue;
              if trim(MCH) is null then
                isErr := 34;
                return '';
              end if;
            end if;
            if (j1 = 3) then
              NDK := tempValue;
            end if;
            if (j1 = 4) then
              STN := tempValue;
            end if;
            if (j1 = 1) then
              NDG := tempValue;
            end if;
          
          END LOOP;
        
          vTemp        := c || MCH || c || '"MCH"' || ',' || c || NDK || c ||
                          '"NDK"' || ',' || c || STN || c || '"STN"' || ',' || c || NDG || c ||
                          '"NDG"';
          dtlNDT_Value := dtlNDT_Value || chr(10) || '<VSTD>' || chr(10) ||
                          GET_TAX_Field(vTemp) || chr(10) || '</VSTD>';
        end if;
      
      end if;
      if (j > 4) then
        dtlNDT := i.COLUMN_VALUE;
        j1     := 0;
        FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                     FROM TABLE(SPLIT(dtlNDT, '/'))) LOOP
          j1 := j1 + 1;
        
          tempValue := '';
          tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
        
          if (j1 = 2) then
            MCH := tempValue;
            if trim(MCH) is null then
              isErr := 34;
              return '';
            end if;
          end if;
          if (j1 = 3) then
            NDK := tempValue;
          end if;
          if (j1 = 4) then
            STN := tempValue;
          end if;
          if (j1 = 1) then
            NDG := tempValue;
          end if;
        
        END LOOP;
      
        vTemp        := c || MCH || c || '"MCH"' || ',' || c || NDK || c ||
                        '"NDK"' || ',' || c || STN || c || '"STN"' || ',' || c || NDG || c ||
                        '"NDG"';
        dtlNDT_Value := dtlNDT_Value || chr(10) || '<VSTD>' || chr(10) ||
                        GET_TAX_Field(vTemp) || chr(10) || '</VSTD>';
      end if;
    
    /* INSERT INTO tempo (col1) VALUES (i.COLUMN_VALUE);*/
    END LOOP;
  
    vTemp    := c || MST || c || '"MST"' || ',' || c || CQT || c || '"CQT"' || ',' || c || TCQ || c ||
                '"TCQ"' || ',' || c || LTH || c || '"LTH"' || ',' || c || NNT || c ||
                '"NNT"' || ',' || c || SKH || c || '"SKH"' || ',' || c || SMA || c ||
                '"SMA"' || ',' || c || STK || c || '"STK"' || ',' || c || NTK || c ||
                '"NTK"' || ',' || c || XNK || c || '"XNK"' || ',' || c || CQP || c ||
                '"CQP"' || ',' || c || TKH || c || '"TKH"';
    v_Header := GET_TAX_Field(vTemp);
    vReturn  := '<?xml version="1.0"?><VST>' || chr(10) || v_Header ||
                dtlNDT_Value || chr(10) || '</VST>';
  
    return replace(vReturn, '> <', '><');
  Exception
    When others then
      vTemp := sqlerrm;
      Return '';
  end ConvertMSGTax;

  FUNCTION GET_TAX_Field(pCOntent varchar2) Return Varchar2 IS
    iposStart integer := 0;
    iposEnd   integer := 0;
    v_Value   varchar2(500);
    vTemp     varchar2(4000);
    l_xmltype XMLTYPE;
    l_ctx     dbms_xmlgen.ctxhandle;
  BEGIN
    -- l_ctx := dbms_xmlgen.newcontext(pCOntent);
    l_ctx := dbms_xmlgen.newcontext('select ' || pCOntent || ' from dual');
  
    /* dbms_xmlgen.setrowsettag(l_ctx, 'VST');
    dbms_xmlgen.setrowtag(l_ctx, 'Dept');*/
  
    l_xmltype := dbms_xmlgen.getXmlType(l_ctx);
    dbms_xmlgen.closeContext(l_ctx);
    dbms_output.put_line('<?xml version="1.0"?>' || l_xmltype.getClobVal);
    return replace(replace(l_xmltype.getClobVal,
                           '<ROWSET>
 <ROW>
  '),
                   '
 </ROW>
</ROWSET>
');
  Exception
    when others then
    
      Return '';
  END GET_TAX_Field;

  FUNCTION ConvertMSGRemark(contentTax varchar2,
                            TCQT       varchar2,
                            transdate  varchar2,
                            msg_type   varchar2,
                            isErr      out number) return varchar2 IS
    vReturn   varchar2(4000);
    iposStart integer := 0;
    iposEnd   integer := 0;
    v_Value   varchar2(500);
    vTemp     varchar2(4000);
  
    dtlLTH       varchar2(3000) := ' ';
    dtlNDT       varchar2(3000) := ' ';
    dtlNDT_Value varchar2(3000) := ' ';
    v_return     varchar2(4000);
    l_xmltype    XMLTYPE;
    l_ctx        dbms_xmlgen.ctxhandle;
    MST          varchar2(100);
    CQT          varchar2(100) := ' ';
    TCQ          varchar2(100) := TCQT;
    LTH          varchar2(100) := ' ';
    NNT          varchar2(100) := transdate;
    SKH          varchar2(100) := ' ';
    SMA          varchar2(100) := ' ';
    SMH          varchar2(100) := ' ';
    STK          varchar2(100) := ' ';
    NTK          varchar2(100) := ' ';
    XNK          varchar2(100) := ' ';
    CQP          varchar2(100) := ' ';
    TKH          varchar2(100) := ' ';
  
    MCH      varchar(2000) := ' ';
    NDK      varchar(2000) := ' ';
    STN      varchar(2000) := ' ';
    NDG      varchar(3000) := ' ';
    j        number(3) := 0;
    j1       number(3) := 0;
    c        varchar2(1) := chr(39);
    v_Header varchar2(4000) := ' ';
  
    temContentTax varchar2(3000);
    ipos          number(10) := 0;
  
    itemp1  number(2);
    msgTemp varchar2(3000);
    msttt   varchar2(1000);
    icount  number(2) := 0;
  
    NTK1      varchar2(30) := '';
    NTK2      varchar2(30) := '';
    NTK3      varchar2(30) := '';
    tempValue varchar2(300) := '';
  
    vTempMST varchar2(2000) := '';
  
  BEGIN
    ipos := instr(contentTax, '>');
  
    if ipos = 0 then
      if (msg_type = 4) then
        temContentTax := contentTax;
      else
        temContentTax := replace(replace(replace(replace(replace(contentTax,
                                                                 '   ',
                                                                 ' '),
                                                         '  ',
                                                         ' '),
                                                 '  ',
                                                 ' '),
                                         '  ',
                                         ' '),
                                 ') (',
                                 ')(');
        ipos          := instr(Rtrim(trim(contentTax), ')('), ')', -1);
        if ipos > 0 then
          temContentTax := substr(contentTax, 1, ipos - 1);
        end if;
      end if;
    else
      temContentTax := substr(contentTax, 1, ipos - 1);
    end if;
  
    if (substr(trim(temContentTax), 1, 6) = '(NSNN!') then
      temContentTax := replace(temContentTax, '(NSNN!');
    end if;
  
    FOR i IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                FROM TABLE(SPLIT(temContentTax, '!'))) LOOP
    
      j := j + 1;
      if (j = 1) then
        MST := i.COLUMN_VALUE;
      end if;
    
      if (j = 2) then
        CQT := i.COLUMN_VALUE;
        BEGIN
          select trim(name_v)
            into TCQ
            from citad.TBLVST_CQT@CITAD_HSC_21
           where ma_cqthu = CQT
             and rownum = 1;
        
        exception
          when others then
            TCQ   := TCQT;
            isErr := 32;
            return '';
          
        end;
      end if;
    
      if (j = 3) then
        LTH := i.COLUMN_VALUE;
        if trim(LTH) is null then
          isErr := 33;
          return '';
        end if;
      end if;
    
      if (j = 4) then
        dtlLTH := i.COLUMN_VALUE;
        if (LTH = '03') then
          j1 := 0;
          FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                       FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
            tempValue := Rtrim(Ltrim(i1.COLUMN_VALUE, '('), ')');
            j1        := j1 + 1;
            if (j1 = 1) then
              SKH := tempValue;
            end if;
            if (j1 = 2) then
              SMA := tempValue;
            end if;
          END LOOP;
          vTempMST := '(SK:' || SKH || ',SM:' || SMA || ')';
        end if;
      
        if (LTH = '04') then
          j1 := 0;
          SELECT count(COLUMN_VALUE)
            into icount
            FROM TABLE(SPLIT(dtlLTH, '/'));
          if icount = 5 then
            FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                         FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
              j1        := j1 + 1;
              tempValue := '';
              tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
              if (j1 = 1) then
                STK := tempValue;
              end if;
              if (j1 = 2) then
                NTK1 := tempValue;
              
              end if;
              if (j1 = 3) then
                NTK2 := tempValue;
              
              end if;
              if (j1 = 4) then
                NTK3 := tempValue;
              end if;
              if (j1 = 5) then
                XNK := tempValue;
                if (XNK = '') then
                  XNK := '999';
                end if;
              end if;
            END LOOP;
            NTK := replace(NTK1, '/') || replace(NTK2, '/') ||
                   replace(NTK3, '/');
            /*replace(NTK3, '/') || replace(NTK2, '/') ||
            replace(NTK1, '/');*/
          else
            FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                         FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
            
              j1        := j1 + 1;
              tempValue := '';
              tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
            
              if (j1 = 1) then
                STK := tempValue;
              end if;
              if (j1 = 2) then
                NTK := tempValue;
              
              end if;
              if (j1 = 3) then
                XNK := tempValue;
              
                if (XNK = '') and LTH = '04' then
                  XNK := '999';
                end if;
              end if;
            END LOOP;
          end if;
        
          vTempMST := '(TK:' || STK || ',N:' || NTK || ',LH:' || XNK || ')';
        end if;
      
        if (LTH = '07') then
          j1 := 0;
          FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                       FROM TABLE(SPLIT(dtlLTH, '/'))) LOOP
            j1 := j1 + 1;
            if (j1 = 1) then
              CQP := Rtrim(Ltrim(i1.COLUMN_VALUE, '(CQP:'), ')');
            end if;
          
          END LOOP;
          vTempMST := '(' || CQP || ')';
        end if;
        if (LTH != '07' and LTH != '03' and LTH != '04') then
          j1 := 0;
        
          dtlNDT := i.COLUMN_VALUE;
          j1     := 0;
          FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                       FROM TABLE(SPLIT(dtlNDT, '/'))) LOOP
            tempValue := '';
            tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
            j1        := j1 + 1;
          
            if (j1 = 2) then
              MCH := tempValue;
              if trim(MCH) is null then
                isErr := 34;
                return '';
              end if;
            end if;
            if (j1 = 3) then
              NDK := tempValue;
            end if;
            if (j1 = 4) then
              STN := tempValue;
            end if;
            if (j1 = 1) then
              NDG := tempValue;
            end if;
          
          END LOOP;
        
          dtlNDT_Value := dtlNDT_Value || NDG || '( C' || MCH || ',TM' || NDK || '' ||
                          ',ST:' || STN || ');';
        
        end if;
      
      end if;
      if (j > 4) then
        dtlNDT := i.COLUMN_VALUE;
        j1     := 0;
        FOR i1 IN (SELECT trim(COLUMN_VALUE) COLUMN_VALUE
                     FROM TABLE(SPLIT(dtlNDT, '/'))) LOOP
          j1 := j1 + 1;
        
          tempValue := '';
          tempValue := RTRIM(Ltrim(i1.COLUMN_VALUE, '('), ')');
        
          if (j1 = 2) then
            MCH := tempValue;
            if trim(MCH) is null then
              isErr := 34;
              return '';
            end if;
          end if;
          if (j1 = 3) then
            NDK := tempValue;
          end if;
          if (j1 = 4) then
            STN := tempValue;
          end if;
          if (j1 = 1) then
            NDG := tempValue;
          end if;
        
        END LOOP;
      
        dtlNDT_Value := dtlNDT_Value || NDG || '( C' || MCH || ',TM' || NDK || '' ||
                        ',ST:' || STN || ');';
      end if;
    
    /* INSERT INTO tempo (col1) VALUES (i.COLUMN_VALUE);*/
    END LOOP;
  
    vReturn := 'MST:' || MST || vTempMST || '.' || dtlNDT_Value || TCQT || ',' ||
               to_char(sysdate, 'DD/MM/YYYY');
  
    return replace(vReturn, '> <', '><');
  Exception
    When others then
      vTemp := sqlerrm;
      Return contentTax;
  end ConvertMSGRemark;

end GW_PK_IBPS_DBLINK_CITAD_2_1;
/
