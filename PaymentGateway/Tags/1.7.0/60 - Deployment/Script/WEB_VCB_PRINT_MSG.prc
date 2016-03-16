CREATE OR REPLACE PROCEDURE VCB_PRINT_MSG(pMSG_ID       in NUMBER,
                          pMSG_TYPE     in varchar2,
                          pMSGDirection in varchar2,
                          pBranch       in varchar2,
                          pUser         in varchar2,
                          --RefCurVCB_03 IN OUT PKG_CR.RefCurType
                          pCurContent in out PKG_CR.RefCurType) is
  
    vContent    varchar2(4000) := '';
    vBranchName varchar(150);
    vUser       varchar2(300);
    ---vContent       clob;
    --vsql         varchar2(3000);
    icount         integer;
    vdetail_temp   varchar2(300);
    vField_name    varchar2(20);
    vTable         varchar2(50);
    v_fieldValue   varchar2(500);
    nQuery_ID      number(20);
    rm_number      varchar2(100);
    sender         varchar2(100);
    receiver       varchar2(100);
    sendername     varchar2(500);
    receivername   varchar2(500);
    receiving_time date;
    sending_time   date;
    print_status   int;
    status         varchar2(50);
    temp_date      date;
    vCCYNAME       nvarchar2(2000);
    vSibs_tellerID  varchar2(50);
    vMsgDirection  varchar2(10);
  BEgin
    select count(1)
      into icount
      from VCB_MSG_CONTENT IMC
     where IMC.MSG_ID = pMSG_ID;
    if icount = 0 then
      select count(1)
        into icount
        from VCB_Msg_All IMC
       where IMC.MSG_ID = pMSG_ID;
      if icount = 0 then
        select count(1)
          into icount
          from VCB_Msg_All_His IMC
         where IMC.MSG_ID = pMSG_ID;
        vUser := pUser;
        if icount <> 0 then
          vTable := 'VCB_Msg_All_His';
          select BR.Bran_Name
            into vBranchName
            From Branch BR
           where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
             and rownum = 1;
          if vBranchName is null then
            vBranchName := '';
          end if;
          select content,
                 Query_ID,
                 to_number(C.RM_NUMBER) AS RM_NUMBER,
                 C.BRANCH_A AS SENDER,
                 C.BRANCH_B AS RECEIVER,
                 GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,11,'X'),
                                           'BFTVVNVXXXX',
                                           '011',
                                           C.Branch_a)) AS sendername,
                 GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,11,'X'),
                                           'BFTVVNVXXXX',
                                           '011',
                                           C.Branch_b)) AS receivername,
                 C.Receiving_Time,
                 C.SENDING_TIME,
                 S.NAME AS STATUS,
                 PRINT_STS,
                 CY.V_READ,
                 C.Sibs_Tellerid,
                 C.Msg_Direction
            into vContent,
                 nQuery_ID,
                 rm_number,
                 sender,
                 receiver,
                 sendername,
                 receivername,
                 receiving_time,
                 sending_time,
                 status,
                 print_status,
                 vCCYNAME,
                 vSibs_tellerID,
                 vMsgDirection
            from VCB_MSG_ALL_HIS C
            LEFT JOIN STATUS S ON C.STATUS = S.STATUS
            LEFT JOIn CCYTOVIE CY ON trim(C.CCYCD) = trim(CY.CCYCODE)
           where msg_id = pMSG_ID;
        end if;
      else
        vTable := 'VCB_MSG_All';
        select BR.Bran_Name
          into vBranchName
          From Branch BR
         where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
           and rownum = 1;
        if vBranchName is null then
          vBranchName := '';
        end if;
        vUser := pUser;
        select content,
               Query_ID,
               to_number(C.RM_NUMBER) AS RM_NUMBER,
               C.BRANCH_A AS SENDER,
               C.BRANCH_B AS RECEIVER,
               GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,11,'X'),
                                           'BFTVVNVXXXX',
                                           '011',
                                           C.Branch_a)) AS sendername,
                GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,11,'X'),
                                           'BFTVVNVXXXX',
                                           '011',
                                           C.Branch_b)) AS receivername,
               C.Receiving_Time,
               C.SENDING_TIME,
               S.NAME AS STATUS,
               PRINT_STS,
               CY.V_READ,
               C.Sibs_Tellerid,
               C.Msg_Direction
          into vContent,
               nQuery_ID,
               rm_number,
               sender,
               receiver,
               sendername,
               receivername,
               receiving_time,
               sending_time,
               status,
               print_status,
               vCCYNAME,
               vSibs_tellerID,
               vMsgDirection
          from VCB_MSG_ALL C
          LEFT JOIN STATUS S ON C.STATUS = S.STATUS
          LEFT JOIn CCYTOVIE CY ON trim(C.CCYCD) = trim(CY.CCYCODE)
         where msg_id = pMSG_ID;
      end if;
    else
      vTable := 'VCB_MSG_CONTENT';
      select BR.Bran_Name
        into vBranchName
        From Branch BR
       where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
         and rownum = 1;
      if vBranchName is null then
        vBranchName := '';
      end if;
      vUser := pUser;
      select content,
             Query_ID,
             to_number(C.RM_NUMBER) AS RM_NUMBER,
             C.BRANCH_A AS SENDER,
             C.BRANCH_B AS RECEIVER,
            GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,11,'X'),
                                           'BFTVVNVXXXX',
                                           '011',
                                           C.Branch_a)) AS sendername,
             GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,11,'X'),
                                           'BFTVVNVXXXX',
                                           '011',
                                           C.Branch_b)) AS receivername,
             C.Receiving_Time,
             C.SENDING_TIME,
             S.NAME AS STATUS,
             PRINT_STS,
             CY.V_READ,
             C.Sibs_Tellerid,
             C.Msg_Direction
        into vContent,
             nQuery_ID,
             rm_number,
             sender,
             receiver,
             sendername,
             receivername,
             receiving_time,
             sending_time,
             status,
             print_status,
             vCCYNAME,
             vSibs_tellerID,
             vMsgDirection             
        from VCB_MSG_CONTENT C
        LEFT JOIN STATUS S ON C.STATUS = S.STATUS
        LEFT JOIn CCYTOVIE CY ON trim(C.CCYCD) = trim(CY.CCYCODE)
       where msg_id = pMSG_ID;
    
    end if;
  
    --if not vContent is NULL then
    OPen pCurContent for
      select to_date(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                          '32A',
                                                          1,
                                                          1,
                                                          pMSG_TYPE),
                     'YYMMDD') AS NGAY_32A,
             rm_number AS RM_NUMBER,
             pMSG_ID  AS SENDER,  -- sua  cho web
             receiver AS RECEIVER,
             sendername AS SENDERNAME,
             receivername AS RECEIVERNAME,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '20', pMSG_TYPE) AS SOLENH,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '72', pMSG_TYPE) AS REF_NO,
             receiving_time AS receiving_time,
             sending_time AS sending_time,
             -- using VCB_GET_Field to get all in a field
            /* replace(GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '50K',
                                                    pMSG_TYPE),
                     chr(13) || chr(10),
                     ';') AS NGUOIRALENH,*/
              decode(vMsgDirection,'VCB-SIBS',       
                      GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '50K',
                                                    pMSG_TYPE)
                    , GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  1,
                                                  1,
                                                  pMSG_TYPE)
                     ) AS NGUOIRALENH,
                GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  2,
                                                  1,
                                                  pMSG_TYPE)
                   AS TKNGUOIRALENH,                                     
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  3,
                                                  1,
                                                  pMSG_TYPE)
                   AS DCNGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS NGUOIHUONG,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS TK_HUONG,
             /*GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  3,
                                                  1,
                                                  pMSG_TYPE) AS DIACHI,*/
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '57E',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS DIACHI, 
                                                                                      
             replace(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                          '32A',
                                                          3,
                                                          1,
                                                          pMSG_TYPE),
                     ',',
                     '.') AS AMOUNT,
             status AS Status,
             print_status AS print_status,
             replace(
             GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                  '70',                                                  
                                                  pMSG_TYPE),chr(10)||chr(13),' ') AS CONTENT,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '52D',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS TK_GHINO,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '52D',
                                                  2,
                                                  1,
                                                  pMSG_ID) AS TEN_TK,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '32A',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS CCY,
            /* GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '33B',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS ORG_AMOUNT,*/
           trim(to_char(to_number(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                              '33B',
                                                              2,
                                                              1,
                                                              pMsg_type)),
               '9,999,999,999,999,999.99'))    || ' ' ||
                 (GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '33B',
                                                  1,
                                                  1,
                                                  pMsg_type))   
                                                   AS ORG_AMOUNT,                                               
                                                  
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71A',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS LOAIPHI,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71F',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS TIENPHI,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71F',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS DVTIENPHI,
             
             GW_PK_VCB_report.VCB_GET_Field(vContent, '57D', pMSG_TYPE) AS NH_HUONG,
             vBranchName AS BRNAME,
             vUser AS Tellername,
             vCCYNAME AS CCYNAME,
             '' AS TellerID,
             vSibs_tellerID AS SIBS_TELLERID,
             '1' AS GRPID
        from dual
      union
      select to_date(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                          '32A',
                                                          1,
                                                          1,
                                                          pMSG_TYPE),
                     'YYMMDD') AS NGAY_32A,
             rm_number AS RM_NUMBER,
               pMSG_ID  AS SENDER,
             receiver AS RECEIVER,
             sendername AS SENDERNAME,
             receivername AS RECEIVERNAME,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '20', pMSG_TYPE) AS SOLENH,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '72', pMSG_TYPE) AS REF_NO,
             receiving_time AS receiving_time,
             sending_time AS sending_time,
             /* replace(GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '50K',
                                                    pMSG_TYPE),
                     chr(13) || chr(10),
                     ';') AS NGUOIRALENH,*/
                     decode(vMsgDirection,'VCB-SIBS',       
                      GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '50K',
                                                    pMSG_TYPE)
                    , GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  1,
                                                  1,
                                                  pMSG_TYPE)
                     ) AS NGUOIRALENH,
                GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  2,
                                                  1,
                                                  pMSG_TYPE)
                   AS TKNGUOIRALENH,                                     
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  3,
                                                  1,
                                                  pMSG_TYPE)
                   AS DCNGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS NGUOIHUONG,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS TK_HUONG,
             /*GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  3,
                                                  1,
                                                  pMSG_TYPE) AS DIACHI,*/
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '57E',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS DIACHI,                                     

             replace(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                          '32A',
                                                          3,
                                                          1,
                                                          pMSG_TYPE),
                     ',',
                     '.') AS AMOUNT,
             status AS Status,
             print_status AS print_status,
             replace(
             GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                  '70',                                                  
                                                  pMSG_TYPE),chr(10)||chr(13),' ') AS CONTENT,
            GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '52D',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS TK_GHINO,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '52D',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS TEN_TK,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '32A',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS CCY,
             /*GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '33B',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS ORG_AMOUNT,*/
             trim(to_char(to_number(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                              '33B',
                                                              2,
                                                              1,
                                                              pMsg_type)),
               '9,999,999,999,999,999.99'))    || ' ' ||
                 (GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '33B',
                                                  1,
                                                  1,
                                                  pMsg_type))   
                                                   AS ORG_AMOUNT,                                                  
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71A',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS LOAIPHI,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71F',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS TIENPHI,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71F',
                                                  1,
                                                  1,
                                                  pMSG_TYPE) AS DVTIENPHI,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '57D', pMSG_TYPE) AS NH_HUONG,
             vBranchName AS BRNAME,
             vUser AS Tellername,
             vCCYNAME AS CCYNAME,
             '' AS TellerID,
             vSibs_tellerID AS SIBS_TELLERID,
             '2' AS GRPID
        from dual;
  
    /*else
       OPen pCurContent for
         select temp_date AS NGAY_32A,
                '' AS rm_number,
                '' AS sender,
                '' AS receiver,
                '' AS sendername,
                '' AS receivername,
                '' AS SOLENH,
                receiving_time,
                sending_time,
                '' AS NGUOIRALENH,
                '' AS NGUOIHUONG,
                '' AS TK_HUONG,
                '' AS DIACHI,
                '' AS AMOUNT,
                '' AS status,
                0 AS print_status,
                '' AS CONTENT,
                '' AS TK_GHINO,
                '' AS TEN_TK,
                '' AS CCY,
                '' AS ORG_AMOUNT,
                '' AS LOAIPHI,
                '' AS TIENPHI
           from VCB_MSG_CONTENT;
    
     end if;
    */
  end VCB_PRINT_MSG;
/
