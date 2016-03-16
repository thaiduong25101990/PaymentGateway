create or replace package GW_PK_IBPS_DBLINK_IN_CITAD21 is

  -- Author  : ADMINISTRATOR
  -- Created : 7/23/2008 10:45:29 AM
  -- Purpose : DN cac ham xu ly dien di cua kenh TT IBPS DBLINK
  Procedure GET_IBPS_TBLEXPORT;
  Function GET_SIBSTRANNUM Return varchar2;
  Function CheckDBLINKTAD(pDblink varchar2) return boolean;
  Procedure GET_IBPS_CONTENTDB;
  --Function GET_SIBSTRANNUM Return varchar2;
  --Function CheckDBLINKTAD(pDblink varchar2) return boolean;
  Procedure Create_Job;
  Procedure Drop_Job;
  Function ConvertXML(xmlTax varchar2) return varchar2;
  Function ConvertXMLView(xmlTax varchar2) return varchar2;
  Function ConvertXML_3492(xmlTax varchar2) return varchar2;

end GW_PK_IBPS_DBLINK_IN_CITAD21;
/
create or replace package body GW_PK_IBPS_DBLINK_IN_CITAD21 is

  -- Khai bao tham so
  m_Direction    varchar2(10) := 'SIBS-IBPS';
  m_Type_MSG_log IBPS_MSG_LOG%rowtype;
  iDBlink        integer := 1;
  vDBlinkname    varchar2(50);

  -- Nguoi tao          :QuanLD
  -- Muc dich           :day du lieu vao queue IBPS_Q_ConvertIN
  -- Ten ham            :GET_IBPS_CONTENT()
  -- Tham so            :pFILE_NAME      varchar2,
  --                    :pMSG_TYPE       varchar2,
  --                    :pSIBS_TRANS_NUM varchar2,
  --                    :pIBPS_CONTENT   varchar2,
  --                    :pSYSTEMDATE     DATE
  --                    
  -- Gis tri tra ve: Day du lieu vao 1 bang table queue

  Procedure GET_IBPS_TBLEXPORT IS
    cursor curMSG_OUT is
      SELECT TAD.TAD_NAME,
             TAD.GW_BANK_CODE,
             TAD.SIBS_BANK_CODE,
             TAD.DBLINK_NAME,
             TAD.Connection
        From TAD
       WHERE TAD.Connection in (1, 4)
         and TAD.Status = 1;
  
    v_MSG_OUT curMSG_OUT%rowtype;
    TYPE cursor_ref IS REF CURSOR;
    c1              cursor_ref;
    v_sqlInsert     varchar2(4000);
    vSQLdym         Varchar2(4000);
    trx_type        Varchar2(40);
    serial_no       Varchar2(40);
    o_ci_code       Varchar2(40);
    r_ci_code       Varchar2(40);
    o_indirect_code Varchar2(40);
    r_indirect_code Varchar2(40);
    trx_date        Varchar2(40);
    currency        Varchar2(40);
    amount          Varchar2(40);
    relation_no     Varchar2(40);
    reference       Varchar2(40);
    type_flag       Varchar2(40);
  
  BEGIN
    -- Kiem tra xem kenh TT hoac cong TAd co cho di DBLINK hay khong
  
    OPEN curMSG_OUT;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH curMSG_OUT
        INTO v_MSG_OUT;
    
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u 
      EXIT WHEN curMSG_OUT %notfound;
      --- Lay vi tri bat dau phan detail
    
      vDBlinkname := v_MSG_OUT.Dblink_Name;
      BEGIN
        if CheckDBLINKTAD(vDBlinkname) then
        
          vSQLdym := 'select 
                      trx_type,
                      serial_no,
                      o_ci_code,
                      r_ci_code,
                      o_indirect_code,
                      r_indirect_code,
                      trx_date,
                      currency,
                      amount,
                      relation_no,
                      reference,
                      type_flag                     
        from tbltransactionmsg_gtw@' || vDBlinkname || ' ex
             where ex.check_code = ''05''
            and ex.response_code =''4000''
            and ex.type_flag = 0
            and rownum < 1000
            and trx_date=' || chr(39) ||
                     to_char(sysdate,'yyyyMMdd') || chr(39) || '
             ' || ' and r_ci_code=' || chr(39) ||
                     Trim(v_MSG_OUT.Gw_Bank_Code) || chr(39) || '';
        
          OPEN c1 FOR vSQLdym;
          Loop
            Begin
              FETCH c1
                INTO trx_type,
                     serial_no,
                     o_ci_code,
                     r_ci_code,
                     o_indirect_code,
                     r_indirect_code,
                     trx_date,
                     currency,
                     amount,
                     relation_no,
                     reference,
                     type_flag;
              EXIT WHEN c1 %notfound;
            
              -- nSIBSNUM := GET_SIBSTRANNUM;
              v_sqlInsert := ' insert into tbltransactionmsg_gtw
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
               content_ex)
               
         select check_code,
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
               content_ex from tbltransactionmsg_gtw@' ||
                             vDBlinkname || ' where trx_type=' || chr(39) ||
                             trx_type || chr(39) ||
                             ' and 
                     serial_no =' || chr(39) ||
                             serial_no || chr(39) ||
                             ' and
                     o_ci_code =' || chr(39) ||
                             o_ci_code || chr(39) ||
                             ' and
                     r_ci_code  =' || chr(39) ||
                             r_ci_code || chr(39) ||
                             ' and
                     o_indirect_code   =' ||
                             chr(39) || o_indirect_code || chr(39) ||
                             ' and
                     r_indirect_code   =' ||
                             chr(39) || r_indirect_code || chr(39) ||
                             ' and
                     trx_date =' || chr(39) ||
                             trx_date || chr(39) ||
                             ' and
                     currency =' || chr(39) ||
                             currency || chr(39) ||
                             ' and 
                     relation_no =' || chr(39) ||
                             relation_no || chr(39) ||
                       
                             ' and
                     type_flag  =' || chr(39) ||
                             type_flag || chr(39);
            
            /*  insert into test (content) values (v_sqlInsert);
              commit;*/
            
              if GW_PK_IBPS_DBLINK.ExecuteSQLNonReturn(v_sqlInsert) = false then
                m_Type_MSG_log.Log_Date      := Sysdate;
                m_Type_MSG_log.Query_Id      := 0;
                m_Type_MSG_log.Msg_Direction := m_Direction;
                m_Type_MSG_log.Status        := 0;
                m_Type_MSG_log.Descriptions  := 'ERROR WHEN update table INTERFACE IBPS table: tblExport  ' ||
                                                Sqlcode || ' -Error- ' ||
                                                Sqlerrm || ' F110:' ||
                                                relation_no || ' F022' ||
                                                r_ci_code;
                m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
                GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
              
                COMMIT;
              end if;
              v_sqlInsert := ' update tbltransactionmsg_gtw@' ||
                             vDBlinkname ||
                             ' set check_code=''06'' where trx_type=' ||
                             chr(39) || trx_type || chr(39) ||
                             ' and 
                     serial_no =' || chr(39) ||
                             serial_no || chr(39) ||
                             ' and
                     o_ci_code =' || chr(39) ||
                             o_ci_code || chr(39) ||
                             ' and
                     r_ci_code  =' || chr(39) ||
                             r_ci_code || chr(39) ||
                             ' and
                     o_indirect_code   =' ||
                             chr(39) || o_indirect_code || chr(39) ||
                             ' and
                     r_indirect_code   =' ||
                             chr(39) || r_indirect_code || chr(39) ||
                             ' and
                     trx_date =' || chr(39) ||
                             trx_date || chr(39) ||
                             ' and
                     currency =' || chr(39) ||
                             currency || chr(39) ||
                             ' and
                    
                     relation_no =' || chr(39) ||
                             relation_no || chr(39) ||
                             ' and
                     type_flag  =' || chr(39) ||
                             type_flag || chr(39);
            
              if GW_PK_IBPS_DBLINK.ExecuteSQLNonReturn(v_sqlInsert) = false then
                m_Type_MSG_log.Log_Date      := Sysdate;
                m_Type_MSG_log.Query_Id      := 0;
                m_Type_MSG_log.Msg_Direction := m_Direction;
                m_Type_MSG_log.Status        := 0;
                m_Type_MSG_log.Descriptions  := 'ERROR WHEN update table INTERFACE IBPS table: tblExport  ' ||
                                                Sqlcode || ' -Error- ' ||
                                                Sqlerrm || ' F110:' ||
                                                relation_no || ' F022' ||
                                                r_ci_code;
                m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
                GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
              
                COMMIT;
              end if;
              commit;
            
            Exception
              when others then
                vSQLdym                      := '';
                m_Type_MSG_log.Log_Date      := Sysdate;
                m_Type_MSG_log.Query_Id      := 0;
                m_Type_MSG_log.Msg_Direction := m_Direction;
                m_Type_MSG_log.Status        := 0;
              
            end;
          
          End loop;
        end if;
      Exception
        when others then
          m_Type_MSG_log.Log_Date      := Sysdate;
          m_Type_MSG_log.Query_Id      := 0;
          m_Type_MSG_log.Msg_Direction := m_Direction;
          m_Type_MSG_log.Status        := 0;
          m_Type_MSG_log.Descriptions  := 'ERROR WHEN  Get from tblExport IBPS ' ||
                                          Sqlcode || ' -Error- ' || Sqlerrm;
          m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
        
          COMMIT;
        
      end;
    
      COMMIT;
    END LOOP;
    -- Dong cursor 
  
    CLOSE curMSG_OUT;
  EXCEPTION
    when others then
    
      m_Type_MSG_log.Log_Date := Sysdate;
    
      m_Type_MSG_log.Msg_Direction := m_Direction;
      m_Type_MSG_log.Status        := 0;
      m_Type_MSG_log.Descriptions  := 'ERROR WHEN Query Message' || Sqlcode ||
                                      ' -Error- ' || Sqlerrm;
      m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
    
  END GET_IBPS_TBLEXPORT;

  Function GET_SIBSTRANNUM Return varchar2 IS
    icount   integer := 0;
    nSIBSNum integer := 0;
    RefNum   varchar2(20);
  Begin
    SELECT Value
      Into RefNum
      FROM SYSVAR
     WHERE VARNAME = 'IBPSSeqNum'
       and GWTYPE = 'IBPS'
       and NOTE = to_char(Sysdate, 'YYYYMMDD');
    nSIBSNum := to_number(RefNum);
  
    /* if nSIBSNum < 50001 then
      nSIBSNum := 50001;
    else
      if MOD(nSIBSNum, 2) = 0 then
        nSIBSNum := nSIBSNum + 1;
      end if;
      nSIBSNum := nSIBSNum + 2;
    end if;*/
  
    nSIBSNum := nSIBSNum + 1;
    Update Sysvar
       set Value = nSIBSNum
     where VARNAME = 'IBPSSeqNum'
       and GWTYPE = 'IBPS';
    return nSIBSNum;
  Exception
    when others then
      select count(*)
        into icount
        FROM SYSVAR
       WHERE VARNAME = 'IBPSSeqNum'
         and GWTYPE = 'IBPS';
      if (icount > 0) then
        Update Sysvar
           set Value = nSIBSNum, note = to_char(sysdate, 'YYYYMMDD')
         where VARNAME = 'IBPSSeqNum'
           and GWTYPE = 'IBPS';
      else
        INSERT INTO Sysvar
          (Value, VARNAME, GWTYPE, Note)
        Values
          (nSIBSNum, 'IBPSSeqNum', 'IBPS', to_char(sysdate, 'YYYYMMDD'));
      end if;
      Return 50001;
  end GET_SIBSTRANNUM;

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

  -- Ham lay du lieu dien ve theo cac chi nhanh

  Procedure GET_IBPS_CONTENTDB IS
    cursor curMSG_OUT is
      SELECT TAD.TAD_NAME,
             TAD.GW_BANK_CODE,
             TAD.SIBS_BANK_CODE,
             TAD.DBLINK,
             TAD.Connection
      
        From TAD
       WHERE TAD.Connection in (1, 4)
         and TAD.Status = 1;
  
    tTypeDBlink ibps_type_convertin;
    v_MSG_OUT   curMSG_OUT%rowtype;
    TYPE cursor_ref IS REF CURSOR;
    cursor c1 is
      select *
        from tbltransactionmsg_gtw gw
       where gw.gw_status = 1
         and gw.gw_direction = 'I'
         and gw.r_ci_code = v_MSG_OUT.Gw_Bank_Code;
  
    v_Mc1        c1%rowtype;
    v_SQLEXC     Varchar2(4000);
    vSQLdym      Varchar2(4000);
    rows_fetched NUMBER;
    f12          varchar2(20);
    nSIBSNUM     Integer;
    remark_tax   varchar2(2000) := '';
    content_tax varchar2(4000):='';
  
  BEGIN
    -- Kiem tra xem kenh TT hoac cong TAd co cho di DBLINK hay khong
  
    OPEN curMSG_OUT;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly
      FETCH curMSG_OUT
        INTO v_MSG_OUT;
    
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u
      EXIT WHEN curMSG_OUT %notfound;
      --- Lay vi tri bat dau phan detail
    
      vDBlinkname := v_MSG_OUT.Dblink;
      BEGIN
        if CheckDBLINKTAD(vDBlinkname) then
        
          OPEN c1;
          Loop
            Begin
              FETCH c1
                INTO v_Mc1;
              EXIT WHEN c1 %notfound;
            
              remark_tax := ConvertXML(v_Mc1.Content_Ex);
              if (v_Mc1.Sd_Time is null) then
                f12 := to_char(sysdate, 'yyyyMMddHH24Miss');
              else
                f12 := RPAD(v_Mc1.Sd_Time, 14, '0');
              end if;
              nSIBSNUM := GET_SIBSTRANNUM;
            
              v_SQLEXC := '#110' || v_Mc1.Relation_No || '#003' ||
                          v_Mc1.Trx_Type || '#007' || v_Mc1.o_Indirect_Code ||
                          '#012' || f12 || '#019' || v_Mc1.r_Indirect_Code ||
                          '#021' || v_Mc1.o_Ci_Code || '#022' ||
                          v_Mc1.r_Ci_Code || '#026' || v_Mc1.Currency ||
                          '#027' || v_Mc1.Amount || '00' || '#028' ||
                          v_Mc1.Sd_Name || '#029' || v_Mc1.Sd_Addr ||
                          '#030' || v_Mc1.Sd_Accnt || '#031' ||
                          v_Mc1.Rv_Name || '#032' || v_Mc1.Rv_Addr ||
                          '#033' || v_Mc1.Rv_Accnt || '#034' ||
                          remark_tax || ')' || v_Mc1.Content ||
                          '#036' || v_Mc1.Opert1 || '#037' || v_Mc1.Opert2 ||
                          '#011' || v_Mc1.Serial_No || '#179' || '' || '#';
                          
              v_SQLEXC := convertfont(v_SQLEXC);
              begin
              content_tax:=convertfont(v_Mc1.Content_Ex);
              exception when others then
                 content_tax:=' ';
              end;
              GW_PK_IBPS_Q_CONVERTIN.IBPS_EN_CONVERTIN('DBLINK',
                                                       'IBPS-SIBS',
                                                       nSIBSNUM,
                                                       v_SQLEXC,
                                                       content_tax,
                                                       sysdate);
            
              -- Cap nhat trang thai dien cho bang giao dien
              update tbltransactionmsg_gtw
                 set gw_status = 2
               where gw_id = v_Mc1.Gw_Id;
            
            Exception
              when others then
                vSQLdym                      := '';
                m_Type_MSG_log.Log_Date      := Sysdate;
                m_Type_MSG_log.Query_Id      := 0;
                m_Type_MSG_log.Msg_Direction := m_Direction;
                m_Type_MSG_log.Status        := 0;
                m_Type_MSG_log.Descriptions  := 'ERROR WHEN update table INTERFACE IBPS table: tblExport  ' ||
                                                Sqlcode || ' -Error- ' ||
                                                Sqlerrm || ' F110:' ||
                                                v_Mc1.Relation_No ||
                                                ' F022:' || v_Mc1.r_Ci_Code;
                m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
                GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
            end;
          
          End loop;
        end if;
      Exception
        when others then
          m_Type_MSG_log.Log_Date      := Sysdate;
          m_Type_MSG_log.Query_Id      := 0;
          m_Type_MSG_log.Msg_Direction := m_Direction;
          m_Type_MSG_log.Status        := 0;
          m_Type_MSG_log.Descriptions  := 'ERROR WHEN  Get from tblExport IBPS ' ||
                                          Sqlcode || ' -Error- ' || Sqlerrm;
          m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
          GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
        
          COMMIT;
        
      end;
    
      COMMIT;
    END LOOP;
    -- Dong cursor
  
    CLOSE curMSG_OUT;
  EXCEPTION
    when others then
    
      m_Type_MSG_log.Log_Date := Sysdate;
    
      m_Type_MSG_log.Msg_Direction := m_Direction;
      m_Type_MSG_log.Status        := 0;
      m_Type_MSG_log.Descriptions  := 'ERROR WHEN Query Message' || Sqlcode ||
                                      ' -Error- ' || Sqlerrm;
      m_Type_MSG_log.Job_Name      := 'IBPS_JOB_DBLINK_CONVERTIN';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
    
  END GET_IBPS_CONTENTDB;

  --Ham tao Job cho die In

  Procedure Create_Job IS
    istt            integer := 0;
    v_branch        varchar2(200);
    orderDblink     number(2);
    orderDblinkTemp number(2) := 0;
    vJobname        varchar2(50);
  
    cursor curTad IS
      select TAD.GW_BANK_CODE, TAD.Orderdblink
        from TAD
       where TAD.Connection = 1
         and status = 1
       order by TAD.Orderdblink;
  
    v_curTad curTad%rowtype;
  
    cursor curJobname is
      select GT.JOBNAME, GT.PERIOTY, GT.BRANCH_NAME from Group_TAD_IN GT;
    v_curJob curJobname%Rowtype;
  Begin
  
    BEGIN
    
      delete from Group_TAD_IN;
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
            v_branch := v_curTad.Gw_Bank_Code;
          else
            v_branch := v_branch || ',' || v_curTad.Gw_Bank_Code;
          end if;
        
        else
          if v_branch is not null then
            istt := istt + 1;
            Insert into Group_TAD_IN
              (Jobname, Perioty, Branch_Name)
            values
              (istt, istt, v_branch);
            commit;
          end if;
          v_branch := '';
        
          if v_branch is null then
            v_branch := v_curTad.Gw_Bank_Code;
          else
            v_branch := v_branch || ',' || v_curTad.Gw_Bank_Code;
          end if;
        
          orderDblinkTemp := orderDblink;
        end if;
      
      end loop;
    
      if v_branch is not null then
        istt := istt + 1;
        Insert into Group_TAD_IN
          (Jobname, Perioty, Branch_Name)
        values
          (istt, istt, v_branch);
        commit;
      end if;
    
      Close curTad;
    
      OPEN curJobname;
      LOOP
        FETCH curJobname
          INTO v_curJob;
      
        EXIT WHEN curJobname %notfound;
        vJobname := v_curJob.Jobname;
      
        DBMS_JOB.SUBMIT(vJobname,
                        'gw_pk_ibps_dblink_in.GET_IBPS_CONTENTDB(' ||
                        Chr(39) || v_curJob.Branch_Name || Chr(39) || ');',
                        SYSDATE,
                        'SYSDATE + 1/2880'); -- 60 second
      
        Update Group_TAD_IN
           set Group_TAD_IN.Jobname = vJobname
         where Group_TAD_IN.Branch_Name = v_curJob.Branch_Name;
        commit;
      
      END loop;
    END;
  
  end Create_Job;

  Procedure Drop_Job IS
  
    cursor curJobname is
      select GT.JOBNAME, GT.PERIOTY, GT.BRANCH_NAME from Group_Tad_IN GT;
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
    close curJobname;
  end;
  /*
    Convert noi dung XML cua dien thue
    '<?xml version="1.0"?>
  <VST>
    <MST>0000000017</MST>
    <CQT>1000001</CQT>
    <TCQ>Chi c?c thu? huy?n Châu thành</TCQ>
    <LTH>03</LTH>
    <NNT>20140320</NNT>
    <SKH>S123456789K123456789</SKH>
    <SMA>S123456789M123456789</SMA>
    <STK>
    </STK>
    <NTK>
    </NTK>
    <XNK>
    </XNK>
    <CQP></CQP>
    <TKH></TKH>
    <VSTD>
      <MCH>001</MCH>
      <NDK>0012</NDK>
      <STN>100000000</STN>
      <NDG>N?i dung chi ti?t 1</NDG>
    </VSTD>
    <VSTD>
      <MCH>002</MCH>
      <NDK>0801</NDK>
      <STN>200000000</STN>
      <NDG>
      </NDG>
    </VSTD>
    <VSTD>
      <MCH>003</MCH>
      <NDK>0802</NDK>
      <STN>300000000</STN>
      <NDG>N?i dung 3</NDG>
    </VSTD>
  </VST>
     '
    
    */
  Function ConvertXML(xmlTax varchar2) return varchar2 IS
    vReturn varchar2(4000) := '';
    iCount  number(2) := 0;
    mst     varchar2(200) := '';
    cqt     varchar2(200) := '';
    TCQ     varchar2(200) := '';
    LTH     varchar2(200) := '';
    NNT     varchar2(200) := '';
    SKH     varchar2(200) := '';
    SMA     varchar2(200) := '';
    STK     varchar2(200) := '';
    NTK     varchar2(200) := '';
    XNK     varchar2(200) := '';
    CQP     varchar2(200) := '';
    TKH     varchar2(200) := '';
    mch     varchar2(200) := '';
    NDK     varchar2(200) := '';
    STN     varchar2(200) := '';
    NDG     varchar2(200) := '';
  
    cursor curXmlTax IS
      select xmlorders.mst,
             xmlorders.cqt,
             xmlorders.TCQ,
             xmlorders.LTH,
             xmlorders.NNT,
             xmlorders.SKH,
             xmlorders.SMA,
             xmlorders.STK,
             xmlorders.NTK,
             xmlorders.XNK,
             xmlorders.CQP,
             xmlorders.TKH,
             xmllines.mch,
             xmllines.NDK,
             xmllines.STN,
             xmllines.NDG
        from xmltable('/VST' passing xmltype(xmlTax) columns MST
                      varchar2(200) path 'MST',
                      CQT varchar2(200) path 'CQT',
                      TCQ varchar2(200) path 'TCQ',
                      LTH varchar2(200) path 'LTH',
                      NNT varchar2(200) path 'NNT',
                      SKH varchar2(200) path 'SKH',
                      SMA varchar2(200) path 'SMA',
                      STK varchar2(200) path 'STK',
                      NTK varchar2(200) path 'NTK',
                      XNK varchar2(200) path 'XNK',
                      CQP varchar2(200) path 'CQP',
                      TKH varchar2(200) path 'TKH',
                      VSTD xmltype path 'VSTD') xmlorders,
             xmltable('/VSTD' passing xmlorders.VSTD columns MCH
                      varchar2(200) path 'MCH',
                      NDK varchar2(200) path 'NDK',
                      STN varchar2(200) path 'STN',
                      NDG varchar2(200) path 'NDG') xmllines;
  
    v_curXmlTax curXmlTax%rowtype;
  Begin
  
    if (trim(xmlTax) is null) then
      return '';
    end if;
  
    OPEN curXmlTax;
    LOOP
      iCount := iCount + 1;
     
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly
      FETCH curXmlTax
        INTO v_curXmlTax;
    
     EXIT WHEN curXmlTax %notfound;
     
      mst := v_curXmlTax.mst;
      cqt := v_curXmlTax.cqt;
      TCQ := v_curXmlTax.TCQ;
      LTH := v_curXmlTax.LTH;
      NNT := v_curXmlTax.NNT;
      SKH := v_curXmlTax.SKH;
      SMA := v_curXmlTax.SMA;
      STK := v_curXmlTax.STK;
      NTK := v_curXmlTax.NTK;
      XNK := v_curXmlTax.XNK;
      CQP := v_curXmlTax.CQP;
      TKH := v_curXmlTax.TKH;
      mch := v_curXmlTax.mch;
      NDK := v_curXmlTax.NDK;
      STN := v_curXmlTax.STN;
      NDG := v_curXmlTax.NDG;
      if iCount = 1 then
        vReturn := mst || '!' || LTH;
      
        if LTH = '03' then
          vReturn := vReturn || '!(' || SKH || '/' || SMA || ')';
        end if;
      
        if LTH = '04' then
          vReturn := vReturn || '!(' || STK || '/' || NTK || '/' || XNK || ')';
        end if;
      
        if LTH = '07' then
          vReturn := vReturn || '!(' || CQP || ')';
        end if;
         vReturn := vReturn || '!(' || NDG || '/' || mch || '/' || '/' || NDK || '/' || '/' || STN || '/' || ')';
      else
        vReturn := vReturn || '!(' || NDG || '/' || mch || '/' || '/' || NDK || '/' || '/' || STN || '/' || ')';
      end if;
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u
    
    END loop;
    close curXmlTax;
  
    return replace(vReturn,'//','/');
  end ConvertXML;

  Function ConvertXMLView(xmlTax varchar2) return varchar2 IS
    vReturn varchar2(4000) := '';
    iCount  number(2) := 0;
    mst     varchar2(200) := '';
    cqt     varchar2(200) := '';
    TCQ     varchar2(200) := '';
    LTH     varchar2(200) := '';
    NNT     varchar2(200) := '';
    SKH     varchar2(200) := '';
    SMA     varchar2(200) := '';
    STK     varchar2(200) := '';
    NTK     varchar2(200) := '';
    XNK     varchar2(200) := '';
    CQP     varchar2(200) := '';
    TKH     varchar2(200) := '';
    mch     varchar2(200) := '';
    NDK     varchar2(200) := '';
    STN     varchar2(200) := '';
    NDG     varchar2(200) := '';
  
    cursor curXmlTax IS
      select xmlorders.mst,
             xmlorders.cqt,
             xmlorders.TCQ,
             xmlorders.LTH,
             xmlorders.NNT,
             xmlorders.SKH,
             xmlorders.SMA,
             xmlorders.STK,
             xmlorders.NTK,
             xmlorders.XNK,
             xmlorders.CQP,
             xmlorders.TKH,
             xmllines.mch,
             xmllines.NDK,
             xmllines.STN,
             xmllines.NDG
        from xmltable('/VST' passing xmltype(xmlTax) columns MST
                      varchar2(200) path 'MST',
                      CQT varchar2(200) path 'CQT',
                      TCQ varchar2(200) path 'TCQ',
                      LTH varchar2(200) path 'LTH',
                      NNT varchar2(200) path 'NNT',
                      SKH varchar2(200) path 'SKH',
                      SMA varchar2(200) path 'SMA',
                      STK varchar2(200) path 'STK',
                      NTK varchar2(200) path 'NTK',
                      XNK varchar2(200) path 'XNK',
                      CQP varchar2(200) path 'CQP',
                      TKH varchar2(200) path 'TKH',
                      VSTD xmltype path 'VSTD') xmlorders,
             xmltable('/VSTD' passing xmlorders.VSTD columns MCH
                      varchar2(200) path 'MCH',
                      NDK varchar2(200) path 'NDK',
                      STN varchar2(200) path 'STN',
                      NDG varchar2(200) path 'NDG') xmllines;
  
    v_curXmlTax curXmlTax%rowtype;
  Begin
    if (trim(xmlTax) is null) then
      return '';
    end if;
    OPEN curXmlTax;
    LOOP
      iCount := iCount + 1;
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly
      FETCH curXmlTax
        INTO v_curXmlTax;
      mst := v_curXmlTax.mst;
      cqt := v_curXmlTax.cqt;
      TCQ := v_curXmlTax.TCQ;
      LTH := v_curXmlTax.LTH;
      NNT := v_curXmlTax.NNT;
      SKH := v_curXmlTax.SKH;
      SMA := v_curXmlTax.SMA;
      STK := v_curXmlTax.STK;
      NTK := v_curXmlTax.NTK;
      XNK := v_curXmlTax.XNK;
      CQP := v_curXmlTax.CQP;
      TKH := v_curXmlTax.TKH;
      mch := v_curXmlTax.mch;
      NDK := v_curXmlTax.NDK;
      STN := v_curXmlTax.STN;
      NDG := v_curXmlTax.NDG;
    
      EXIT WHEN curXmlTax %notfound;
      if iCount = 1 then
        vReturn := ' Ma so thue: ' || mst || chr(13) || chr(10) ||
                   ' Ma CQT: ' || cqt || '; Ten CQT: ' || TCQ || chr(13) ||
                   chr(10) || ' Loai thue: ' || LTH || ': ';
        if LTH = '03' then
          vReturn := vReturn || ': So khung:' || SKH || '; So may:' || SMA || ';' ||
                     chr(13) || chr(10);
        end if;
      
        if LTH = '04' then
          vReturn := vReturn || ':STK: ' || STK || '; NTK: ' || NTK ||
                     '; XNK:' || XNK || ';' || chr(13) || chr(10);
        end if;
      
        if LTH = '07' then
          vReturn := vReturn || ':CQP: ' || CQP || ';';
        end if;
        vReturn := vReturn || chr(13) || chr(10) || NDG || '; Chuong: ' || mch ||
                   '; NDK: ' || NDK || '; STN: ' || STN || ';' || chr(13) ||
                   chr(10);
      else
        vReturn := vReturn || NDG || '; Chuong: ' || mch || '; NDK: ' || NDK ||
                   '; STN: ' || STN || ';' || chr(13) || chr(10);
      end if;
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u
    
    END loop;
    close curXmlTax;
  
    return vReturn;
  end ConvertXMLView;

  Function ConvertXML_3492(xmlTax varchar2) return varchar2 IS
    vReturn varchar2(4000) := '';
    iCount  number(2) := 0;
    mst     varchar2(200) := '';
    cqt     varchar2(200) := '';
    TCQ     varchar2(200) := '';
    LTH     varchar2(200) := '';
    NNT     varchar2(200) := '';
    SKH     varchar2(200) := '';
    SMA     varchar2(200) := '';
    STK     varchar2(200) := '';
    NTK     varchar2(200) := '';
    XNK     varchar2(200) := '';
    CQP     varchar2(200) := '';
    TKH     varchar2(200) := '';
    mch     varchar2(200) := '';
    NDK     varchar2(200) := '';
    STN     varchar2(200) := '';
    NDG     varchar2(200) := '';
  
    cursor curXmlTax IS
      select xmlorders.mst,
             xmlorders.cqt,
             xmlorders.TCQ,
             xmlorders.LTH,
             xmlorders.NNT,
             xmlorders.SKH,
             xmlorders.SMA,
             xmlorders.STK,
             xmlorders.NTK,
             xmlorders.XNK,
             xmlorders.CQP,
             xmlorders.TKH,
             xmllines.mch,
             xmllines.NDK,
             xmllines.STN,
             xmllines.NDG
        from xmltable('/VST' passing xmltype(xmlTax) columns MST
                      varchar2(200) path 'MST',
                      CQT varchar2(200) path 'CQT',
                      TCQ varchar2(200) path 'TCQ',
                      LTH varchar2(200) path 'LTH',
                      NNT varchar2(200) path 'NNT',
                      SKH varchar2(200) path 'SKH',
                      SMA varchar2(200) path 'SMA',
                      STK varchar2(200) path 'STK',
                      NTK varchar2(200) path 'NTK',
                      XNK varchar2(200) path 'XNK',
                      CQP varchar2(200) path 'CQP',
                      TKH varchar2(200) path 'TKH',
                      VSTD xmltype path 'VSTD') xmlorders,
             xmltable('/VSTD' passing xmlorders.VSTD columns MCH
                      varchar2(200) path 'MCH',
                      NDK varchar2(200) path 'NDK',
                      STN varchar2(200) path 'STN',
                      NDG varchar2(200) path 'NDG') xmllines;
  
    v_curXmlTax curXmlTax%rowtype;
  Begin
  
    if (trim(xmlTax) is null) then
      return '';
    end if;
  
    OPEN curXmlTax;
    LOOP
      iCount := iCount + 1;

      -- L?y t?ng dong d? li?u c?a cursor d? x? ly
      FETCH curXmlTax
        INTO v_curXmlTax;
        
      EXIT WHEN curXmlTax %notfound;
      mst := v_curXmlTax.mst;
      cqt := v_curXmlTax.cqt;
      TCQ := v_curXmlTax.TCQ;
      LTH := v_curXmlTax.LTH;
      NNT := v_curXmlTax.NNT;
      SKH := v_curXmlTax.SKH;
      SMA := v_curXmlTax.SMA;
      STK := v_curXmlTax.STK;
      NTK := v_curXmlTax.NTK;
      XNK := v_curXmlTax.XNK;
      CQP := v_curXmlTax.CQP;
      TKH := v_curXmlTax.TKH;
      mch := v_curXmlTax.mch;
      NDK := v_curXmlTax.NDK;
      STN := v_curXmlTax.STN;
      NDG := v_curXmlTax.NDG;
      if iCount = 1 then
        vReturn := 'MST:' || mst || ' '  ;
      
        if LTH = '03' then
          vReturn := vReturn || '(' || SKH || ',' || SMA || ')';
        end if;
      
        if LTH = '04' then
          vReturn := vReturn || '(' || STK || ',' || NTK || ',' || XNK || ')';
        end if;
      
        if LTH = '07' then
          vReturn := vReturn || '(' || CQP || ')';
        end if;
      else
        vReturn := vReturn || '(' || NDG || ',' || mch || ',' || ',' || NDK || ',' || ',' || STN || ',' || ')' || TCQ;
      end if;
      -- Thoat kh?i l?nh l?p n?u d? duy?t h?t t?t c? d? li?u
    
    END loop;
    close curXmlTax;
  
    return vReturn;
  end ConvertXML_3492;

end GW_PK_IBPS_DBLINK_IN_CITAD21;
/
