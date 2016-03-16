CREATE OR REPLACE PACKAGE GW_PK_VCB_REPORT IS
  TYPE m_tblField_type IS TABLE OF Varchar2(2000) INDEX BY Varchar2(20);

  PROCEDURE VCB_PRINT_MSG(pMSG_ID       in NUMBER,
                          pMSG_TYPE     in varchar2,
                          pMSGDirection in varchar2,
                          pBranch       in varchar2,
                          pUser         in varchar2,
                          pCurContent   in out PKG_CR.RefCurType);
  PROCEDURE VCB_PRINT_MSG_LIST(pMSG_ID in NUMBER,
                               pBranch in varchar2,
                               pUser   in varchar2,
                               --RefCurVCB_03 IN OUT PKG_CR.RefCurType
                               pcurBM_VCB_MSG in out PKG_CR.RefCurType);
  PROCEDURE VCB_03(pdate        IN date,
                   pCCYCD       in varchar2 default 'VND',
                   pBranchA     in varchar2,
                   pBranchB     in varchar2,
                   RefCurVCB_03 IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_03_1(pdate          IN date,
                     pCCYCD         in varchar2 default 'VND',
                     pBranchA       in varchar2,
                     pBranchB       in varchar2,
                     RefCurVCB_03_1 IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_04(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_04 IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_04_CN(pDate        in date,
                      pBRANCHA     in VARCHAR2,
                      pBRANCHB     in VARCHAR2,
                      pCCYCD       in VARCHAR2,
                      pStatus      in VARCHAR2,
                      RefCurVCB_04 IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_04_1(pDate          in date,
                     pBRANCHA       in VARCHAR2,
                     pBRANCHB       in VARCHAR2,
                     pCCYCD         in VARCHAR2,
                     pStatus        in VARCHAR2,
                     RefCurVCB_04_1 IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_05(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_05 IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_05_1(pDate          in date,
                     pBRANCHA       in VARCHAR2,
                     pBRANCHB       in VARCHAR2,
                     pCCYCD         in VARCHAR2,
                     pStatus        in VARCHAR2,
                     RefCurVCB_05_1 IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_06(pFrDate   date,
                   ptoDate   date,
                   msg_type  in varchar,
                   RefCurVCB IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_07(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_07 IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_08(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_08 IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_09(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_09 IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_print_all(RefCurVCB_PRINT_ALL IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_PRINT_RECONCILE(RefCurvcb_print_reconcile IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_print(RefCurVCB_PRINT IN OUT PKG_CR.RefCurType);
  PROCEDURE vcb_print_1(RefCurVCB_PRINT IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_FEE_CAL(pFromDate  in date,
                        pToDate    in date,
                        pBranch    in varchar,
                        pFeeType   in number,
                        pCCYCD     in varchar,
                        RefCurBK02 IN OUT PKG_CR.RefCurType);
  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                pclobContent clob,
                                m_MSG_TYPE   varchar2) return m_tblField_type;
  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                pclobContent clob,
                                m_MSG_TYPE   varchar2,
                                prownum      integer) return m_tblField_type;
  FUNCTION SWIFT_RM_GETFIELD_IN(pFieldName varchar2, pCONTENT clob)
    return varchar2;
  /*  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
  pclobContent clob,
  pFile_name   varchar2) return m_tblField_type;*/
  FUNCTION VCB_GET_FIELD(pCONTENT   VARCHAR2,
                         pFieldName VARCHAR2,
                         m_MSG_TYPE nvarchar2) RETURN VARCHAR2;
  FUNCTION VCB_GET_SWIFT_Field(pCOntent   clob,
                               pFiledCode varchar2,
                               pRownum    number,
                               pPartnum   number,
                               m_MSG_TYPE varchar2) Return Varchar2;
  FUNCTION GetFieldValue(pSWFieldTag  varchar2,
                         piRowNum     integer,
                         piPartNum    Integer,
                         pFilecontent m_tblField_type) return Varchar2;
  FUNCTION GET_BRANCH_NAME(pSIBSBANKCODE varchar2) return varchar2;
  FUNCTION GET_BRANCH_ID_NAME(pSIBSBANKCODE varchar2) return varchar2;
END; -- Package spec
/
CREATE OR REPLACE PACKAGE BODY GW_PK_VCB_REPORT IS
  /* Ði?n in VCB
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE VCB_PRINT_MSG(pMSG_ID       in NUMBER,
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
    vSibs_tellerID varchar2(50);
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
                 GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,
                                                                 11,
                                                                 'X'),
                                                            'BFTVVNVXXXX',
                                                            '011',
                                                            C.Branch_a)) AS sendername,
                 GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,
                                                                 11,
                                                                 'X'),
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
            LEFT JOIN STATUS S
              ON C.STATUS = S.STATUS
            LEFT JOIn CCYTOVIE CY
              ON trim(C.CCYCD) = trim(CY.CCYCODE)
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
               GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,
                                                               11,
                                                               'X'),
                                                          'BFTVVNVXXXX',
                                                          '011',
                                                          C.Branch_a)) AS sendername,
               GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,
                                                               11,
                                                               'X'),
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
          LEFT JOIN STATUS S
            ON C.STATUS = S.STATUS
          LEFT JOIn CCYTOVIE CY
            ON trim(C.CCYCD) = trim(CY.CCYCODE)
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
             GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,
                                                             11,
                                                             'X'),
                                                        'BFTVVNVXXXX',
                                                        '011',
                                                        C.Branch_a)) AS sendername,
             GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,
                                                             11,
                                                             'X'),
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
        LEFT JOIN STATUS S
          ON C.STATUS = S.STATUS
        LEFT JOIn CCYTOVIE CY
          ON trim(C.CCYCD) = trim(CY.CCYCODE)
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
             pMSG_ID AS SENDER, -- sua  cho web
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
             decode(vMsgDirection,
                    'VCB-SIBS',
                    GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                   '50K',
                                                   pMSG_TYPE),
                    GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                         '50K',
                                                         1,
                                                         1,
                                                         pMSG_TYPE)) AS NGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS TKNGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  3,
                                                  1,
                                                  pMSG_TYPE) AS DCNGUOIRALENH,
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
             --Minhhb comment out                                     
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
             replace(GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '70',
                                                    pMSG_TYPE),
                     chr(10) || chr(13),
                     ' ') AS CONTENT,
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
                          '9,999,999,999,999,999.99')) || ' ' ||
             (GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                   '33B',
                                                   1,
                                                   1,
                                                   pMsg_type)) AS ORG_AMOUNT,
             
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
             pMSG_ID AS SENDER,
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
             decode(vMsgDirection,
                    'VCB-SIBS',
                    GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                   '50K',
                                                   pMSG_TYPE),
                    GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                         '50K',
                                                         1,
                                                         1,
                                                         pMSG_TYPE)) AS NGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  2,
                                                  1,
                                                  pMSG_TYPE) AS TKNGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  3,
                                                  1,
                                                  pMSG_TYPE) AS DCNGUOIRALENH,
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
             -- Minhhb comment out                                     
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
             replace(GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '70',
                                                    pMSG_TYPE),
                     chr(10) || chr(13),
                     ' ') AS CONTENT,
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
                          '9,999,999,999,999,999.99')) || ' ' ||
             (GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                   '33B',
                                                   1,
                                                   1,
                                                   pMsg_type)) AS ORG_AMOUNT,
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
  -------------------------------------------
  /* Ði?n in VCB
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE VCB_PRINT_MSG_LIST(pMSG_ID in NUMBER,
                               pBranch in varchar2,
                               pUser   in varchar2,
                               --RefCurVCB_03 IN OUT PKG_CR.RefCurType
                               pcurBM_VCB_MSG in out PKG_CR.RefCurType) is
  
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
    vSibs_tellerID varchar2(50);
    vMsgDirection  varchar2(10);
    vMsg_type      varchar2(20);
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
                 GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,
                                                                 11,
                                                                 'X'),
                                                            'BFTVVNVXXXX',
                                                            '011',
                                                            C.Branch_a)) AS sendername,
                 GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,
                                                                 11,
                                                                 'X'),
                                                            'BFTVVNVXXXX',
                                                            '011',
                                                            C.Branch_b)) AS receivername,
                 C.Receiving_Time,
                 C.SENDING_TIME,
                 S.NAME AS STATUS,
                 PRINT_STS,
                 CY.V_READ,
                 C.Sibs_Tellerid,
                 C.Msg_Direction,
                 C.Msg_Type
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
                 vMsgDirection,
                 vMsg_type
            from VCB_MSG_ALL_HIS C
            LEFT JOIN STATUS S
              ON C.STATUS = S.STATUS
            LEFT JOIn CCYTOVIE CY
              ON trim(C.CCYCD) = trim(CY.CCYCODE)
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
               GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,
                                                               11,
                                                               'X'),
                                                          'BFTVVNVXXXX',
                                                          '011',
                                                          C.Branch_a)) AS sendername,
               GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,
                                                               11,
                                                               'X'),
                                                          'BFTVVNVXXXX',
                                                          '011',
                                                          C.Branch_b)) AS receivername,
               C.Receiving_Time,
               C.SENDING_TIME,
               S.NAME AS STATUS,
               PRINT_STS,
               CY.V_READ,
               C.Sibs_Tellerid,
               C.Msg_Direction,
               C.Msg_Type
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
               vMsgDirection,
               vMsg_type
          from VCB_MSG_ALL C
          LEFT JOIN STATUS S
            ON C.STATUS = S.STATUS
          LEFT JOIn CCYTOVIE CY
            ON trim(C.CCYCD) = trim(CY.CCYCODE)
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
             GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_a,
                                                             11,
                                                             'X'),
                                                        'BFTVVNVXXXX',
                                                        '011',
                                                        C.Branch_a)) AS sendername,
             GW_PK_VCB_REPORT.GET_BRANCH_ID_NAME(decode(rpad(C.Branch_b,
                                                             11,
                                                             'X'),
                                                        'BFTVVNVXXXX',
                                                        '011',
                                                        C.Branch_b)) AS receivername,
             C.Receiving_Time,
             C.SENDING_TIME,
             S.NAME AS STATUS,
             PRINT_STS,
             CY.V_READ,
             C.Sibs_Tellerid,
             C.Msg_Direction,
             C.Msg_Type
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
             vMsgDirection,
             vMsg_type
        from VCB_MSG_CONTENT C
        LEFT JOIN STATUS S
          ON C.STATUS = S.STATUS
        LEFT JOIn CCYTOVIE CY
          ON trim(C.CCYCD) = trim(CY.CCYCODE)
       where msg_id = pMSG_ID;
    
    end if;
  
    --if not vContent is NULL then
    OPen pcurBM_VCB_MSG for
      select to_date(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                          '32A',
                                                          1,
                                                          1,
                                                          vMsg_type),
                     'YYMMDD') AS NGAY_32A,
             rm_number AS RM_NUMBER,
             pMSG_ID AS SENDER, -- sua  cho web
             receiver AS RECEIVER,
             sendername AS SENDERNAME,
             receivername AS RECEIVERNAME,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '20', vMsg_type) AS SOLENH,
             GW_PK_VCB_report.VCB_GET_Field(vContent, '72', vMsg_type) AS REF_NO,
             receiving_time AS receiving_time,
             sending_time AS sending_time,
             -- using VCB_GET_Field to get all in a field
             /* replace(GW_PK_VCB_report.VCB_GET_Field(vContent,
                                            '50K',
                                            pMSG_TYPE),
             chr(13) || chr(10),
             ';') AS NGUOIRALENH,*/
             decode(vMsgDirection,
                    'VCB-SIBS',
                    GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                   '50K',
                                                   vMsg_type),
                    GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                         '50K',
                                                         1,
                                                         1,
                                                         vMsg_type)) AS NGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  2,
                                                  1,
                                                  vMsg_type) AS TKNGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '50K',
                                                  3,
                                                  1,
                                                  vMsg_type) AS DCNGUOIRALENH,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  2,
                                                  1,
                                                  vMsg_type) AS NGUOIHUONG,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  1,
                                                  1,
                                                  vMsg_type) AS TK_HUONG,
             /*GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '59',
                                                  3,
                                                  1,
                                                  vMsg_type) AS DIACHI,*/
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '57E',
                                                  1,
                                                  1,
                                                  vMsg_type) AS DIACHI,                                     
             replace(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                          '32A',
                                                          3,
                                                          1,
                                                          vMsg_type),
                     ',',
                     '.') AS AMOUNT,
             status AS Status,
             print_status AS print_status,
             replace(GW_PK_VCB_report.VCB_GET_Field(vContent,
                                                    '70',
                                                    vMsg_type),
                     chr(10) || chr(13),
                     ' ') AS CONTENT,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '52D',
                                                  1,
                                                  1,
                                                  vMsg_type) AS TK_GHINO,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '52D',
                                                  2,
                                                  1,
                                                  pMSG_ID) AS TEN_TK,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '32A',
                                                  2,
                                                  1,
                                                  vMsg_type) AS CCY,
             
             trim(to_char(to_number(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                                         '33B',
                                                                         2,
                                                                         1,
                                                                         vMsg_type)),
                          '9,999,999,999,999,999.99')) || ' ' ||
             (GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                   '33B',
                                                   1,
                                                   1,
                                                   vMsg_type)) AS ORG_AMOUNT,
             /*GW_PK_VCB_report.VCB_GET_Field(vContent,'33B',                                                  
             pMSG_TYPE) AS ORG_AMOUNT,*/
             
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71A',
                                                  1,
                                                  1,
                                                  vMsg_type) AS LOAIPHI,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71F',
                                                  2,
                                                  1,
                                                  vMsg_type) AS TIENPHI,
             GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                  '71F',
                                                  1,
                                                  1,
                                                  vMsg_type) AS DVTIENPHI,
             
             GW_PK_VCB_report.VCB_GET_Field(vContent, '57D', vMsg_type) AS NH_HUONG,
             vBranchName AS BRNAME,
             vUser AS Tellername,
             vCCYNAME AS CCYNAME,
             '' AS TellerID,
             vSibs_tellerID AS SIBS_TELLERID,
             pMSG_ID AS GRPID
      
        from dual;
  
  end VCB_PRINT_MSG_LIST;
  -------------------------------------------

  /* Báo cáo d?i chi?u VCB
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE VCB_03(pdate        IN date,
                   pCCYCD       in varchar2 default 'VND',
                   pBranchA     in varchar2,
                   pBranchB     in varchar2,
                   RefCurVCB_03 IN OUT PKG_CR.RefCurType) IS
    vCCYCD   varchar2(3);
    vBranchA varchar2(12);
    vBranchB varchar2(12);
  BEGIN
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pBranchA)) = 'ALL' or trim(pBranchA) is null then
      vBranchA := '%';
    else
      vBranchA := substr(trim(pBranchA), -3);
    
    end if;
    if upper(trim(pBranchB)) = 'ALL' or trim(pBranchB) is null then
      vBranchB := '%';
    else
      vBranchB := substr(trim(pBranchB), -3);
    end if;
  
    open RefCurVCB_03 for
      SELECT AR.*
        FROM (select R.*, b.content AS STATUSNAME
                from (SELECT A.msg_id,
                             NVL(A.RM_NUMBER, '0') as RM_NUMBER,
                             /*CASE A.MSG_DIRECTION
                               WHEN 'I' THEN
                                CASE trim(C.CONTENT)
                               WHEN null THEN
                                CASE trim(CA.CONTENT)
                               WHEN null THEN
                                VCB_GET_Field(CH.CONTENT, '72', CH.Msg_Type)
                               ELSE
                                VCB_GET_Field(CA.CONTENT, '72', CA.Msg_Type)
                             END ELSE VCB_GET_Field(C.CONTENT, '72', C.MSG_TYPE) END ELSE*/ -- DIEN DI
                             A.Trans_No AS Ref_no, -- truong 72
                             A.ref_number AS Field20, -- so lenh
                             A.sender,
                             A.receiver,
                             A.amount,
                             A.gw_type sibs_ttsp,
                             A.trans_date,
                             A.ccy,
                             A.exception_type,
                             A.msg_direction,
                             decode(trim(C.Content),
                                    NULL,
                                    decode(CA.Content,
                                           NULL,
                                           CH.Status,
                                           CA.Status),
                                    C.Status) AS STATUS
                      /*                           
                      CASE trim(C.CONTENT)
                                   WHEN null THEN
                                         CASE trim(CA.CONTENT)
                                               WHEN null THEN
                                                     CH.Status
                                         ELSE
                                                 CA.Status
                                         END 
                                   ELSE  C.Status 
                      END AS STATUS*/
                        FROM VCB_MSG_REC A
                        LEFT JOIN VCB_MSG_CONTENT C
                          ON C.QUERY_ID = A.Query_Id
                        LEFT JOIN VCB_MSG_ALL CA
                          ON CA.QUERY_ID = A.Query_Id
                        LEFT JOIN VCB_MSG_ALL_HIS CH
                          ON CH.QUERY_ID = A.Query_Id
                       WHERE nvl(a.ccy, '%') like vCCYCD
                         AND to_char(A.trans_date, 'DDMMYYYY') =
                             TO_char(pdate, 'DDMMYYYY')
                         AND nvl(substr(A.sender, -3), '%') like vBranchA
                         AND nvl(substr(A.receiver, -3), '%') like vBranchB
                      
                      ) R
                LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
                  ON trim(R.STATUS) = trim(B.STATUS)
              
              ) AR
      --uncomment de chuyen sang bao cao doi chieu ko co dien lap GV-V
       WHERE AR.msg_id not in
             (select R.Msg_Id
                from (select A.msg_id,
                             (select B.Amount
                                from vcb_msg_rec B
                               where B.Ref_Number = A.Ref_Number
                                 AND B.Exception_Type = 'V'
                                 and rownum = 1) AS VCB_AMOUNT
                        from vcb_msg_rec A
                       where A.Exception_Type = 'GV'
                         AND to_char(A.Trans_Date, 'DDMMYYYY') =
                             TO_char(pdate, 'DDMMYYYY')) R
               where trim(R.vcb_amount) is not null
              union
              select R.Msg_Id
                from (select A.msg_id,
                             (select B.Amount
                                from vcb_msg_rec B
                               where B.Ref_Number = A.Ref_Number
                                 AND B.Exception_Type = 'GV'
                                 and rownum = 1) AS VCB_AMOUNT
                        from vcb_msg_rec A
                       where A.Exception_Type = 'V'
                         AND to_char(A.Trans_Date, 'DDMMYYYY') =
                             TO_char(pdate, 'DDMMYYYY')) R
               where trim(R.vcb_amount) is not null)
       ORDER BY AR.field20, AR.msg_direction, AR.RM_NUMBER;
  END VCB_03;
  -----------------------
  /* Báo cáo d?i chi?u BM01b/HD.01/QD.DV.001
     Creator: Bùi H?ng Phuong 
     Date: 14/09/2010 
  */
  PROCEDURE VCB_03_1(pdate          IN date,
                     pCCYCD         in varchar2 default 'VND',
                     pBranchA       in varchar2,
                     pBranchB       in varchar2,
                     RefCurVCB_03_1 IN OUT PKG_CR.RefCurType) IS
    vCCYCD   varchar2(3);
    vBranchA varchar2(12);
    vBranchB varchar2(12);
  BEGIN
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pBranchA)) = 'ALL' or trim(pBranchA) is null then
      vBranchA := '%';
    else
      vBranchA := substr(trim(pBranchA), -3);
    
    end if;
    if upper(trim(pBranchB)) = 'ALL' or trim(pBranchB) is null then
      vBranchB := '%';
    else
      vBranchB := substr(trim(pBranchB), -3);
    end if;
  
    open RefCurVCB_03_1 for
      select Msg_Id,
             Trans_Date,
             NVL(RM_NUMBER, '0') as RM_NUMBER,
             Field20, -- so lenh
             Ref_no, -- truong 72
             Exception_type,
             Msg_Direction,
             Sender,
             Receiver,
             CCY,
             Amount,
             VCB_AMOUNT,
             (VCB_AMOUNT - Amount) AS DIF_AMT,
             '' AS EXTFIELD01,
             '' AS EXTFIELD02,
             '' AS EXTFIELD03
        from (select A.Msg_Id,
                     A.RM_NUMBER,
                     A.Ref_Number AS Field20, -- so lenh,
                     A.Trans_Date,
                     A.Trans_No AS Ref_no, -- truong 72,
                     A. CCY,
                     A.Amount,
                     A.Sender,
                     A.Receiver,
                     A.Msg_Direction,
                     A.Exception_Type,
                     (select B.Amount
                        from vcb_msg_rec B
                       where B.Ref_Number = A.Ref_Number
                         AND B.Exception_Type = 'V'
                         and rownum = 1) AS VCB_AMOUNT
                from vcb_msg_rec A
               where A.Exception_Type = 'GV'
                 AND to_char(A.Trans_Date, 'DDMMYYYY') =
                     to_char(pDate, 'DDMMYYYY')
                 AND nvl(A.Ccy, '%') LIKE vCCYCD
                 AND A.sender like vBranchA
                 AND A.receiver like vBranchB) R
       where trim(R.vcb_amount) is not null
       ORDER BY R.field20, R.msg_direction, R.RM_NUMBER;
  
  END VCB_03_1;

  ----------------------------------------------
  /* IBPS_VCB02/BC : Danh sách di?n di VCB
    Creator: Bùi H?ng Phuong 
  */
  PROCEDURE VCB_04(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_04 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(100);
    vBranchB VARCHAR2(100);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(lpad(pBRANCHA, 5, '0'));
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(pBRANCHB);
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_04 for
    --select * from VCB_04_TEMP order by QUERY_ID,RM_NUMBER asc;
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             substr(BRANCH_A, 3) as BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER,
             'aasa' AAA
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_04;
  -----------------------
  /*IBPS_VCB03/BC : Danh sach dien di VCB cua chi nhanh
  */
  PROCEDURE VCB_04_CN(pDate        in date,
                      pBRANCHA     in VARCHAR2,
                      pBRANCHB     in VARCHAR2,
                      pCCYCD       in VARCHAR2,
                      pStatus      in VARCHAR2,
                      RefCurVCB_04 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(100);
    vBranchB VARCHAR2(100);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(lpad(pBRANCHA, 5, 0));
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(pBRANCHB);
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_04 for
    --select * from VCB_04_TEMP order by QUERY_ID,RM_NUMBER asc;
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             substr(BRANCH_A, 3) as BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER,
             'aasa' AAA
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_04_CN;
  ----------------------
  /* Danh sách di?n di VCB
     Creator: Bùi H?ng Phuong 
  */
  PROCEDURE vcb_04_1(pDate          in date,
                     pBRANCHA       in VARCHAR2,
                     pBRANCHB       in VARCHAR2,
                     pCCYCD         in VARCHAR2,
                     pStatus        in VARCHAR2,
                     RefCurVCB_04_1 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(100);
    vBranchB VARCHAR2(100);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(lpad(pBRANCHA, 5, 0));
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(pBRANCHB);
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       VCB_GET_SWIFT_Field(C.CONTENT, '59', 2, 1, C.MSG_TYPE) AS RECEIVER
                  FROM VCB_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_04_1 for
    --select * from VCB_04_TEMP order by QUERY_ID,RM_NUMBER asc;
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             substr(BRANCH_A, 3) as BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_04_1;
  -----------------------
  /* B?ng kê di?n d?n
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE VCB_05(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_05 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(12);
    vBranchB VARCHAR2(12);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(pBRANCHA);
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(lpad(pBRANCHB, 5, 0));
    end if;
  
    if to_char(pDate, 'DD/MM/YYYY') = to_char(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         RECEIVERACCT)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.Trans_No,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.RECEIVERACCT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,                       
                       C.Trans_No,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVERACCT
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(pDate, 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(vOndate, 'DD/MM/YYYY'), 4, 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         RECEIVERACCT)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.Trans_No,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.RECEIVERACCT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.Trans_No,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVERACCT
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT *
                       FROM ALLCODE
                      WHERE trim(GWTYPE) = 'SYSTEM'
                        AND UPPER(TRIM(cdname)) = 'STATUS') B
            ON trim(A.STATUS) = trim(B.CDVAL)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         RECEIVERACCT)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.Trans_No,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.RECEIVERACCT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.Trans_No,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVERACCT
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         RECEIVERACCT)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.Trans_No,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.RECEIVERACCT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.Trans_No,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVERACCT
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         RECEIVERACCT)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.Trans_No,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.RECEIVERACCT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.Trans_No,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVERACCT
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         RECEIVERACCT)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.Trans_No,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.RECEIVERACCT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.Trans_No,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       VCB_GET_FIELD(C.CONTENT, '50K', C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVERACCT
                  FROM VCB_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src not in ('2', '3')
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_05 for
    --select * from VCB_04_TEMP order by QUERY_ID,RM_NUMBER asc;
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             substr(BRANCH_B, -3) as BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER,
             RECEIVERACCT
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_05;
  ----------------------
  PROCEDURE vcb_05_1(pDate          in date,
                     pBRANCHA       in VARCHAR2,
                     pBRANCHB       in VARCHAR2,
                     pCCYCD         in VARCHAR2,
                     pStatus        in VARCHAR2,
                     RefCurVCB_05_1 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(12);
    vBranchB VARCHAR2(12);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(pBRANCHA);
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(lpad(pBRANCHB, 5, 0));
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src <> '1'
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src <> '1'
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src <> '1'
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src <> '1'
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src <> '1'
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE
                  FROM VCB_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'VCB-SIBS'
                   AND c.branch_a LIKE vBranchA
                   AND c.branch_b like vBranchB
                   AND c.msg_src <> '1'
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_05_1 for
    --select * from VCB_04_TEMP order by QUERY_ID,RM_NUMBER asc;
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             substr(BRANCH_B, -3) as BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             BRANCH_A,
             CCYCD,
             TRANS_DATE
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_05_1;
  ----------------------
  PROCEDURE VCB_06(pFrDate   date,
                   ptoDate   date,
                   msg_type  in varchar,
                   RefCurVCB IN OUT PKG_CR.RefCurType) IS
  
    Refnum  varchar(10);
    Ccycd   varchar(10);
    Amount  varchar(10);
    BenBank varchar(10);
    BenAcc  varchar(10);
    BenName varchar(10);
    sqlstr  varchar(1000);
  
    /******************************************************************************
       NAME:       VCB_06
       PURPOSE:    Bao cao cac dien Excel den VCB
       Creater: Hienntm
    
       REVISIONS:
       Ver        Date        Author           Description
       ---------  ----------  ---------------  ------------------------------------
       1.0        15/08/2008          1. Created this procedure.
    
    ******************************************************************************/
  BEGIN
  
    select a.xlscol
      into Refnum
      from msg_xls a
     where gwtype = 'VCB'
       and msg_type = 'EX103'
       and swift_field_name = '20';
  
    select a.xlscol
      into ccycd
      from msg_xls a
     where gwtype = 'VCB'
       and msg_type = 'EX103'
       and swift_field_name = '32A'
       and row_num = 2;
  
    select a.xlscol
      into amount
      from msg_xls a
     where gwtype = 'VCB'
       and msg_type = 'EX103'
       and swift_field_name = '32A'
       and row_num = 3;
  
    select a.xlscol
      into BenBank
      from msg_xls a
     where gwtype = 'VCB'
       and msg_type = 'EX103'
       and swift_field_name = '57D'
       and row_num = 1;
  
    select a.xlscol
      into BenAcc
      from msg_xls a
     where gwtype = 'VCB'
       and msg_type = 'EX103'
       and swift_field_name = '59'
       and row_num = 1;
  
    select a.xlscol
      into BenName
      from msg_xls a
     where gwtype = 'VCB'
       and msg_type = 'EX103'
       and swift_field_name = '59'
       and row_num = 2;
  
    sqlstr := 'insert into xls_rpt select ' || Refnum || ' as Refnum,' ||
              ccycd || ' as ccycd,' || amount || ' as amount,' || BenBank ||
              ' as BenBank,' || BenAcc || ' as BenAcc,' || BenName ||
              ' as BenName,' ||
              ' FILE_NAME, TRANS_DATE, STATUS, GWTYPE, MSG_DIRECTION, decode(TYPE, 2,''MT103'',1,''SAL'') as Msg_type,TELLER_ID, OFFICER_ID ' ||
              ' from excel where gwtype=''VCB'' and to_char(trans_date,''YYYYMMDD'')>=''' ||
              to_char(pFrDate, 'YYYYMMDD') ||
              ''' and  to_char(trans_date,''YYYYMMDD'')<=''' ||
              to_char(pToDate, 'YYYYMMDD') || '''';
    -- 
    Delete from xls_rpt;
    execute immediate sqlstr;
    --   if SQL%rowcount > 0 then
    open RefCurVCB for
      select * from xls_rpt;
    --refnum,ccycd,to_number(amount) amount,benbank,benacc,benname,file_name,trans_date,status,gwtype,msg_direction,msg_type,teller_id,officer_id
    --   end if;
  
  END VCB_06;
  ----------------------
  /* B?ng kê di?n EBANK di VCBMoney trong ngày
     Creator: Bùi H?ng Phuong
     Date: 27/10/2011 
  */
  PROCEDURE VCB_07(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_07 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(100);
    vBranchB VARCHAR2(100);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(lpad(pBRANCHA, 5, '0'));
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(pBRANCHB);
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_07 for
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             substr(BRANCH_A, 3) as BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER,
             BANK_SEND,
             BANK_RECEIVE
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_07;

  ----------------------
  /* B?ng kê di?n EBANK di VCBMoney trong ngày cua ngay giao dich
     Creator: Bùi H?ng Phuong
     Date: 15/11/2011 
  */
  PROCEDURE VCB_08(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_08 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(100);
    vBranchB VARCHAR2(100);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(lpad(pBRANCHA, 5, '0'));
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(pBRANCHB);
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL_HIS C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_08 for
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             substr(BRANCH_A, 3) as BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER,
             BANK_SEND,
             BANK_RECEIVE
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_08;
  --------------------------------------------------------------------
  /* B?ng kê di?n EBANK di VCBMoney trong ngày cua ngay truoc
     Creator: Bùi H?ng Phuong
     Date: 15/11/2011 
  */
  PROCEDURE VCB_09(pDate        in date,
                   pBRANCHA     in VARCHAR2,
                   pBRANCHB     in VARCHAR2,
                   pCCYCD       in VARCHAR2,
                   pStatus      in VARCHAR2,
                   RefCurVCB_09 IN OUT PKG_CR.RefCurType) IS
  
    vCCYCD   VARCHAR2(3);
    vOndate  date;
    vStatus  VARCHAR2(50);
    vBranchA VARCHAR2(100);
    vBranchB VARCHAR2(100);
  BEGIN
    --select sysdate into vOndate from dual;
    --select to_char(sysdate,'dd/mm/yyyy') from dual;
    select sysdate into vOndate from dual;
    if upper(trim(pCCYCD)) = 'ALL' or trim(pCCYCD) is null then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    if upper(trim(pBRANCHA)) = 'ALL' or trim(pBRANCHA) is null then
      vBranchA := '%';
    else
      vBranchA := trim(lpad(pBRANCHA, 5, '0'));
    end if;
    if upper(trim(pBRANCHB)) = 'ALL' or trim(pBRANCHB) is null then
      vBranchB := '%';
    else
      vBranchB := trim(pBRANCHB);
    end if;
  
    if to_date(pDate, 'DD/MM/YYYY') = to_date(vOndate, 'DD/MM/YYYY') then
      --open RefCurBM_IBPS02 for
      delete from VCB_04_TEMP;
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') <>
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    elsif substr(to_char(to_date(pDate, 'DD/MM/YYYY'), 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(to_date(vOndate, 'DD/MM/YYYY'), 'DD/MM/YYYY'),
                 4,
                 2) then
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') <>
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') <>
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    else
      delete from VCB_04_TEMP;
      --lay du lieu ngay
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_CONTENT C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') <>
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong thang
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') <>
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --lay du lieu trong nam
      insert into VCB_04_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         FIELD20,
         BRANCH_B,
         AMOUNT,
         MSG_TYPE,
         STATUS,
         BRANCH_A,
         CCYCD,
         TRANS_DATE,
         SENDER,
         RECEIVER,
         BANK_SEND,
         BANK_RECEIVE)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.RM_NUMBER,
               A.FIELD20,
               A.BRANCH_B,
               A.AMOUNT,
               A.MSG_TYPE,
               B.CONTENT AS STATUS,
               A.BRANCH_A || '-' || BR.BRAN_NAME AS BRANCH_A,
               A.CCYCD,
               A.TRANS_DATE,
               A.SENDER,
               A.RECEIVER,
               A.BANK_SEND,
               A.BANK_RECEIVE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       --C.RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.FIELD20,
                       BRANCH_B,
                       C.AMOUNT,
                       C.MSG_TYPE,
                       C.STATUS,
                       C.BRANCH_A,
                       C.CCYCD,
                       C.TRANS_DATE,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '50K',
                                                            1,
                                                            1,
                                                            C.MSG_TYPE) AS SENDER,
                       GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(C.CONTENT,
                                                            '59',
                                                            2,
                                                            1,
                                                            C.MSG_TYPE) AS RECEIVER,
                       '' AS BANK_SEND,
                       GW_PK_VCB_report.VCB_GET_Field(C.Content,
                                                      '57D',
                                                      MSG_TYPE) AS BANK_RECEIVE
                  FROM VCB_MSG_ALL_HIS C
                 inner join IBPS_SIBS_ol2 O
                    on to_number(C.Rm_Number) = O.RM_NUMBER
                   and nvl(C.RM_number, ' ') <> ' '
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND to_char(O.TRANSDATE, 'YYYYMMDD') <>
                       to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-VCB'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND lpad(c.branch_a, 5, '0') LIKE vBranchA
                   AND lpad(c.branch_b, 5, '0') like vBranchB
                   AND C.ccycd LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.BRANCH_A, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    
    end if;
    open RefCurVCB_09 for
      select MSG_ID,
             QUERY_ID,
             RM_NUMBER,
             FIELD20,
             BRANCH_B,
             AMOUNT,
             MSG_TYPE,
             STATUS,
             substr(BRANCH_A, 3) as BRANCH_A,
             CCYCD,
             TRANS_DATE,
             SENDER,
             RECEIVER,
             BANK_SEND,
             BANK_RECEIVE
        from VCB_04_TEMP
       order by QUERY_ID, RM_NUMBER asc;
  END VCB_09;
  --------------------------------------------------------------------
  PROCEDURE vcb_print_all(RefCurVCB_PRINT_ALL IN OUT PKG_CR.RefCurType) IS
  BEGIN
    open RefCurVCB_PRINT_ALL for
      select * from VCB_PRINT_TEMP where 1 < 0;
  END VCB_PRINT_ALL;
  ----------------------
  PROCEDURE VCB_PRINT_RECONCILE(RefCurvcb_print_reconcile IN OUT PKG_CR.RefCurType) IS
  BEGIN
    open RefCurvcb_print_reconcile for
      SELECT MSG_ID,
             REC_TYPE,
             '' as STATUS,
             '' as MODULE,
             '' as ORG_ref_number,
             '' as REF_NUMBER,
             SENDER,
             RECEIVER,
             AMOUNT,
             '' as CCY,
             TRANS_DATE,
             MSG_TYPE
        FROM VCB_MSG_REC
       where 1 < 0;
  END vcb_print_reconcile;
  -----------------------
  PROCEDURE vcb_print(RefCurVCB_PRINT IN OUT PKG_CR.RefCurType) IS
  BEGIN
  
    open RefCurVCB_PRINT for
      select '' as msg_type,
             '' as branch_a,
             '' as branch_b,
             '' as field_name,
             '' as field_value,
             '' as E_FIELD_NAME,
             '' as E_MSG_NAME,
             '' as branch_sender,
             '' as branch_receive,
             '' AS STATUS,
             '' as ROW_NUM,
             '' as msg_direction
        from vcb_msg_content
       where 1 < 0;
  END VCB_PRINT;
  ----------------------
  PROCEDURE vcb_print_1(RefCurVCB_PRINT IN OUT PKG_CR.RefCurType) IS
  BEGIN
  
    open RefCurVCB_PRINT for
      select '' as msg_type,
             '' as branch_a,
             '' as branch_b,
             '' as field_name,
             '' as field_value,
             '' as E_FIELD_NAME,
             '' as E_MSG_NAME,
             '' as branch_sender,
             '' as branch_receive,
             '' AS STATUS,
             '' as ROW_NUM,
             '' as msg_direction
        from vcb_msg_content
       where 1 < 0;
  END VCB_PRINT_1;

  --------------------------
  PROCEDURE VCB_FEE_CAL(pFromDate  in date,
                        pToDate    in date,
                        pBranch    in varchar,
                        pFeeType   in number,
                        pCCYCD     in varchar,
                        RefCurBK02 IN OUT PKG_CR.RefCurType) IS
    vFIXEDFEE NUMBER(19, 2);
    vRATE     NUMBER(8, 4);
    vMINFEE   NUMBER(19, 2);
    vMAXFEE   NUMBER(19, 2);
    strSql    varchar2(4000);
    vFR_DATE  NUMBER(10);
    vTO_DATE  NUMBER(10);
  BEGIN
    vFR_DATE := TO_NUMBER(TO_CHAR(pFromDate, 'YYYYMMDD'));
    vTO_DATE := TO_NUMBER(TO_CHAR(pToDate, 'YYYYMMDD'));
    --LAY CAC THAM SO TINH PHI
    SELECT FIXEDFEE, RATE, MINFEE, MAXFEE
      INTO vFIXEDFEE, vRATE, vMINFEE, vMAXFEE
      FROM VCB_FEE
     WHERE ROWNUM = 1;
    --XOA DU LIEU BANG TEMP
    DELETE FROM VCB_FEE_CAL_TEMP;
    ----------------------------------
    --INSERT DU LIEU VAO BANG TEMP
  
    --LAY CAC CHI NHANH
    IF pBranch = 'ALL' THEN
      --LAY LOAI TIEN = ALL
      IF pCCYCD = 'ALL' THEN
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_CONTENT
           WHERE TRANSDATE >= vFR_DATE
             AND TRANSDATE <= vTO_DATE
             AND STATUS = 1
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND STATUS = 1
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL_HIS
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND STATUS = 1
             AND MSG_DIRECTION = 'SIBS-VCB';
        --LAY LOAI TIEN = VND
      ELSIF pCCYCD = 'VND' THEN
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_CONTENT
           WHERE TRANSDATE >= vFR_DATE
             AND TRANSDATE <= vTO_DATE
             AND STATUS = 1
             AND CCYCD = 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND STATUS = 1
             AND CCYCD = 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL_HIS
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND STATUS = 1
             AND CCYCD = 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        --LAY LOAI TIEN <> VND
      ELSE
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_CONTENT
           WHERE TRANSDATE >= vFR_DATE
             AND TRANSDATE <= vTO_DATE
             AND STATUS = 1
             AND CCYCD <> 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND STATUS = 1
             AND CCYCD <> 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL_HIS
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND STATUS = 1
             AND CCYCD <> 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
      END IF;
      --LAY THEO TUNG CHI NHANH
    ELSE
      IF pCCYCD = 'ALL' THEN
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_CONTENT
           WHERE TRANSDATE >= vFR_DATE
             AND TRANSDATE <= vTO_DATE
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL_HIS
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND MSG_DIRECTION = 'SIBS-VCB';
        --LOAI TIEN = VND
      ELSIF pCCYCD = 'VND' THEN
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_CONTENT
           WHERE TRANSDATE >= vFR_DATE
             AND TRANSDATE <= vTO_DATE
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND CCYCD = 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND CCYCD = 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL_HIS
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND CCYCD = 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        --LOAI TIEN <> VND
      ELSE
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_CONTENT
           WHERE TRANSDATE >= vFR_DATE
             AND TRANSDATE <= vTO_DATE
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND CCYCD <> 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND CCYCD <> 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
        INSERT INTO VCB_FEE_CAL_TEMP
          (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)
          SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH_A
            FROM VCB_MSG_ALL_HIS
           WHERE TO_CHAR(TRANSDATE) >= TO_CHAR(pFromDate, 'YYYYMMDD')
             AND TO_CHAR(TRANSDATE) <= TO_CHAR(pToDate, 'YYYYMMDD')
             AND BRANCH_A = LPAD(pBranch, 5, '0')
             AND STATUS = 1
             AND CCYCD <> 'VND'
             AND MSG_DIRECTION = 'SIBS-VCB';
      END IF;
    END IF;
    --TINH PHI VOI LOAI PHI CO DINH
    IF pFeeType = 1 THEN
      strSql := 'SELECT SUM(T.AMOUNT)AS FEE,COUNT(T.MSG_ID) AS NUM, 
      K.BRAN_NAME, BRANCH, CCYCD
      FROM
      (SELECT MSG_ID,' || vFIXEDFEE || ' as AMOUNT,BRANCH,' ||
                pCCYCD || ' as CCYCD 
                FROM VCB_FEE_CAL_TEMP 
      )T INNER JOIN BRANCH K ON 
      LPAD(T.BRANCH,5,''0'') = LPAD(K.SIBS_BANK_CODE,5,''0'')
      GROUP BY K.BRAN_NAME, T.BRANCH, T.CCYCD 
      ORDER BY BRAN_NAME';
      --TINH PHI THEO TY LE
    ELSE
      strSql := 'SELECT SUM(T.AMOUNT)AS FEE,COUNT(T.MSG_ID) AS NUM, 
      K.BRAN_NAME, BRANCH, CCYCD
      FROM
      (SELECT MSG_ID,(CASE WHEN ' || vRATE || '*AMOUNT>=' ||
                vMAXFEE || ' THEN ' || vMAXFEE || ' WHEN ' || vRATE ||
                '*AMOUNT<= ' || vMINFEE || ' THEN ' || vMINFEE || ' ELSE ' ||
                vRATE || '*AMOUNT END)as AMOUNT,BRANCH,,
                DECODE(CCYCD,''VND'',''VND'',''USD'') AS CCYCD 
                FROM VCB_FEE_CAL_TEMP 
      )T INNER JOIN BRANCH K ON 
      LPAD(T.BRANCH,5,''0'') = LPAD(K.SIBS_BANK_CODE,5,''0'')      
      GROUP BY K.BRAN_NAME, T.BRANCH, T.CCYCD 
      ORDER BY BRAN_NAME';
    END IF;
  
    OPEN RefCurBK02 FOR strSql;
  
  END VCB_FEE_CAL;
  ---------------------
  /*
   FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                  pclobContent clob,
                                  pFile_name   varchar2) return m_tblField_type IS
      v_ReturnContent      m_tblField_type;
      v_ReturnContent_temp m_tblField_type;
      -- iPosStart       integer := 0;
      iPosEnd Integer := 0;
      --iPosStart Integer := 0;
      ilen        Integer := 0;
      iMSGLen     Integer := 0;
      v_Compare   varchar2(6);
      v_check_Acc varchar2(50);
      --v_FieldNext     varchar2(6);
      isOptionalTag boolean := false;
      k             integer := 0;
      --l               integer := 0;
      --  iRow_pos       Integer := 0;
      v_Fieldcontent varchar2(4000);
      v_char_1       varchar2(2);
      v_char         varchar2(2);
      v_char1        varchar2(2);
      v_char2        varchar2(2);
      v_char3        varchar2(2);
      v_char4        varchar2(200);
      l              integer := 0;
       m_MSG_TYPE varchar2(10);
    m_TELEX_NO Varchar2(20);
      nstartReplace number(5);
      pTypeSwiftLog        Swift_Msg_Log%Rowtype;
      m_Type_SWIFT_Content SWIFT_MSG_CONTENT%Rowtype;
      m_vSibsCreatedate         Varchar2(10);
      m_Query_ID                number(19);
      m_MSG_Direction           varchar2(10) := 'SWIFT-SIBS';
      incorrect EXCEPTION;
      --v_char5        varchar2(2);
      vtest varchar2(2000);
      --vtest1 varchar2(2000);
      -- vtest2 varchar2(20);
    BEGIN
      for i in 0 .. 40 loop
        v_ReturnContent(i) := '';
      end loop;
      k       := 0;
      iMSGLen := Length(pclobContent);
      ilen    := Length(trim(pSWiftFile));
    
      -- lay ra vi tri dau tien cua doan chua du lieu Thuoc swift file name (VD field 20)
      nstartReplace := Dbms_Lob.instr(pclobContent, ':' || pSWiftFile || ':');
    
      if (pSWiftFile = '20') then
        k := 0;
      end if;
    
      --if iPosStart > 0 Then
      for m in nstartReplace - 6 .. imsglen loop
        if Dbms_Lob.substr(pclobContent, 1, m) = ':' and
           Dbms_Lob.substr(pclobContent, 1, m + 1 + ilen) = ':' then
        
          v_Compare := Dbms_Lob.substr(pclobContent, 2, m + 1); -- substr(pSWiftFile, 1, 2);
          if (ilen = 3) And substr(pSWiftFile, 3, 1) = 'a' then
            v_Compare := v_Compare || 'a';
          else
            v_Compare := v_Compare || Dbms_Lob.substr(pclobContent, 1, m + 3);
          end if;
          If (substr(v_Compare, 1, ilen) = substr(pSWiftFile, 1, ilen)) then
            if (ilen = 3) And substr(pSWiftFile, 3, 1) = 'a' then
              v_ReturnContent(0) := Dbms_Lob.substr(pclobContent, 1, m + 3);
            end if;
          
            if (ilen = 3 AND substr(pSWiftFile, 3, 1) = 'a') then
              v_ReturnContent(0) := dbms_lob.substr(pclobContent, 1, m + 3);
            End if;
          
            k := 1;
          
            for i in m + ilen + 2 .. iMSGLen Loop
            
              v_char_1 := dbms_lob.substr(pclobContent, 1, i - 1);
              v_char   := dbms_lob.substr(pclobContent, 1, i);
            
              v_char1 := dbms_lob.substr(pclobContent, 1, i + 1);
            
              v_char2 := dbms_lob.substr(pclobContent, 1, i + 2);
            
              v_char3 := dbms_lob.substr(pclobContent, 1, i + 3);
              v_char4 := dbms_lob.substr(pclobContent, 1, i + 4);
              vtest   := v_char_1 || v_char || v_char1 || v_char2 || v_char3 ||
                         v_char4;
            
              if ((v_char = '-' AND v_char1 = '}') Or
                 ((v_char_1 = chr(10) or (v_char_1 = chr(13))) And
                 v_char = ':' And (v_char3 = ':' or v_char4 = ':'))) Then
              
                Exit;
              END IF;
            
              iPosEnd := i;
              v_char  := dbms_lob.substr(pclobContent, 1, i);
              IF v_char = chr(10) Then
                --Kiem tra 
                if (isOptionalTag AND (k = 1) And
                   dbms_lob.substr(pclobContent, 1, i) <> '/') then
                  -- Cai nay lam gi day
                  v_ReturnContent(2) := v_ReturnContent(1);
                  v_ReturnContent(1) := ' ';
                
                  k := 3;
                  l := i + 1;
                else
                  if (k > 10) then
                    k := k;
                  end if;
                  k := k + 1;
                  l := i + 1;
                End if;
                -- lay noi dung cua truong dien        
              
              else
              
                IF dbms_lob.substr(pclobContent, 1, i) <> chr(13) Then
                  --lay vi tri dau tien  cua doan du lieu tiep theo
                
                  --iRow_pos := i + 1;
                
                  v_Fieldcontent := dbms_lob.substr(pclobContent, 1, i);
                  v_ReturnContent(k) := v_ReturnContent(k) || v_Fieldcontent;
                  v_Fieldcontent := v_ReturnContent(k);
                
                end if;
              end if;
            
            End loop;
          
            Exit;
          End if;
        end if;
      
      end loop;
    
      if (substr(pSWiftFile, 1, 2) = '21') then
        m_Type_SWIFT_Content.Field21 := v_ReturnContent(1);
      End if;
    
      if (substr(pSWiftFile, 1, 3) = '23E') then
      
        v_ReturnContent(2) := substr(v_ReturnContent(2), 1, 5) ||
                              substr(v_ReturnContent(1), 5);
        v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 4);
      End if;
    
      if pSWiftFile = '32A' Then
        --vtest := v_ReturnContent(1);
        -- Tach truong 32A ra thanh 3 phan ngay, loai tien, so tien
        v_ReturnContent(2) := substr(v_ReturnContent(1), 7, 3);
        v_ReturnContent(3) := substr(v_ReturnContent(1), 10);
        v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 6);
        -- lay loai tien
        m_Type_SWIFT_Content.Ccycd := v_ReturnContent(2);
        -- chuyen gia tri tien tu chuoi sang so
        v_ReturnContent(3) := Replace(v_ReturnContent(3), ',', '.');
      
        begin
          -- da tung nhan 100
          m_Type_SWIFT_Content.Amount := To_number(v_ReturnContent(3));
          v_ReturnContent(3) := to_char(To_number(v_ReturnContent(3)) * 100);
        exception
          when others then
            m_Type_SWIFT_Content.Amount := 0;
        End;
        -- lay ngay create date
        -- Remember that SWIFT date format is YYMMDD, while SIBS is DDMMYY
        begin
        
          if trim(v_ReturnContent(1)) is not null then
            v_Fieldcontent                  := to_char(to_date(v_ReturnContent(1),
                                                               'YYMMDD'),
                                                       'YYMMDD');
            m_Type_SWIFT_Content.Value_Date := to_date(v_Fieldcontent,
                                                       'YYMMDD');
            m_vSibsCreatedate               := to_char(m_Type_SWIFT_Content.Value_Date,
                                                       'DDMMYY');
          else
            m_Type_SWIFT_Content.Value_Date := null;
            m_vSibsCreatedate               := null;
          end if;
        
        exception
          when others then
            m_Type_SWIFT_Content.Value_Date := null;
        End;
      
      End if;
    
      if (pSWiftFile = '32B') then
      
        v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
        v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
      
        -- lay loai tien
        m_Type_SWIFT_Content.Ccycd := v_ReturnContent(1);
        -- chuyen gia tri tien tu chuoi sang so
        v_ReturnContent(2) := Replace(v_ReturnContent(2), ',', '.');
        begin
          m_Type_SWIFT_Content.Amount := To_number(v_ReturnContent(2));
          v_ReturnContent(2) := to_char(m_Type_SWIFT_Content.Amount * 100);
        exception
          when others then
            m_Type_SWIFT_Content.Amount := 0;
            v_ReturnContent(2) := '0';
        End;
      END IF;
    
      if (pSWiftFile = '33B') then
      
        begin
          v_ReturnContent(2) := Replace(substr(v_ReturnContent(1), 4),
                                        ',',
                                        '.');
          v_ReturnContent(2) := to_char(to_number(v_ReturnContent(2)) * 100);
        
        exception
          when others then
            v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
        end;
      
        v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
      
      End if;
    
      if (pSWiftFile = '71F') then
      
        v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
        v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
      
      End if;
    
      if (pSWiftFile = '71G') then
      
        v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
        v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
      
      End if;
      --if m_Type_SWIFT_Content.Department = 'RM' then
      v_ReturnContent_temp := v_ReturnContent;
      if m_MSG_TYPE = 'MT103' then
        if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '53' or
           substr(pSWiftFile, 1, 2) = '50' or substr(pSWiftFile, 1, 2) = '54' or
           substr(pSWiftFile, 1, 2) = '55' or substr(pSWiftFile, 1, 2) = '56' or
           substr(pSWiftFile, 1, 2) = '57' or substr(pSWiftFile, 1, 2) = '59' then
          v_check_Acc := v_ReturnContent(1);
          if substr(v_check_Acc, 1, 1) <> '/' then
            for i in 2 .. 10 loop
              v_check_Acc := v_ReturnContent(i);
            
              v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
            end loop;
            v_ReturnContent(1) := '';
          end if;
        
        end if;
      
      end if;
    
      if m_MSG_TYPE = 'MT191' then
        if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '57' then
          v_check_Acc := v_ReturnContent(1);
          if substr(v_check_Acc, 1, 1) <> '/' then
            for i in 2 .. 10 loop
              v_check_Acc := v_ReturnContent(i);
              v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
            end loop;
            v_ReturnContent(1) := '';
          end if;
        
        end if;
      
      end if;
    
      if m_MSG_TYPE = 'MT202' then
        if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '53' or
           substr(pSWiftFile, 1, 2) = '54' or substr(pSWiftFile, 1, 2) = '58' or
           substr(pSWiftFile, 1, 2) = '56' or substr(pSWiftFile, 1, 2) = '57' then
          v_check_Acc := v_ReturnContent(1);
          if substr(v_check_Acc, 1, 1) <> '/' then
            for i in 2 .. 10 loop
              v_check_Acc := v_ReturnContent(i);
              v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
            end loop;
            v_ReturnContent(1) := '';
          end if;
        
        end if;
      
      end if;
    
      if m_MSG_TYPE = 'MT900' then
        if substr(pSWiftFile, 1, 2) = '52'
        
         then
          v_check_Acc := v_ReturnContent(1);
          if substr(v_check_Acc, 1, 1) <> '/' then
            for i in 2 .. 10 loop
              v_check_Acc := v_ReturnContent(i);
              v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
            end loop;
            v_ReturnContent(1) := '';
          end if;
        
        end if;
      
      end if;
      if m_MSG_TYPE = 'MT910' then
        if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '50' or
           substr(pSWiftFile, 1, 2) = '56'
        
         then
          v_check_Acc := v_ReturnContent(1);
          if substr(v_check_Acc, 1, 1) <> '/' then
            for i in 2 .. 10 loop
              v_check_Acc := v_ReturnContent(i);
              v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
            end loop;
            v_ReturnContent(1) := '';
          end if;
        
        end if;
      
      end if;
      -- end if;
      --
      Return v_ReturnContent;
    Exception
      when OTHERS THEN
      
        pTypeSwiftLog.Log_Date      := sysdate;
        pTypeSwiftLog.Query_Id      := m_Query_ID;
        pTypeSwiftLog.Status        := 0;
        pTypeSwiftLog.Descriptions  := 'SWIFT_RM_GETFIELD_IN: Start Dequeue Message In from file: ' ||
                                       pFILE_NAME || Sqlcode || Sqlerrm;
        pTypeSwiftLog.Job_Name      := 'SWIFT_JOB_CONVERTIN';
        pTypeSwiftLog.Msg_Direction := m_MSG_Direction;
        GW_PK_SWIFT_PROCESS.SWIFT_Msg_Trace(pTypeSwiftLog);
        -- Tra ve gia tri loi ra ham cha
        m_Type_SWIFT_Content.Err_Code := -1;
        RAISE incorrect;
      
        return v_ReturnContent;
    END SWIFT_RM_GETFIELD_IN;
  
  */
  -----
  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                pclobContent clob,
                                m_MSG_TYPE   varchar2) return m_tblField_type IS
    v_ReturnContent      m_tblField_type;
    v_ReturnContent_temp m_tblField_type;
    -- iPosStart       integer := 0;
    iPosEnd Integer := 0;
    --iPosStart Integer := 0;
    ilen        Integer := 0;
    iMSGLen     Integer := 0;
    v_Compare   varchar2(6);
    v_check_Acc varchar2(50);
    --v_FieldNext     varchar2(6);
    isOptionalTag  boolean := false;
    k              integer := 0;
    nstartReplace  integer := 0;
    v_Fieldcontent varchar2(4000);
    v_char_1       varchar2(2);
    v_char         varchar2(2);
    v_char1        varchar2(2);
    v_char2        varchar2(2);
    v_char3        varchar2(2);
    v_char4        varchar2(200);
    l              integer := 0;
    --v_char5        varchar2(2);
    vtest varchar2(2000);
    --vtest1 varchar2(2000);
    -- vtest2 varchar2(20);
  BEGIN
    for i in 0 .. 20 loop
      v_ReturnContent(i) := '';
    end loop;
    k       := 0;
    iMSGLen := Length(pclobContent);
    ilen    := Length(trim(pSWiftFile));
  
    -- lay ra vi tri dau tien cua doan chua du lieu Thuoc swift file name (VD field 20)
    nstartReplace := Dbms_Lob.instr(pclobContent, ':' || pSWiftFile || ':');
  
    if (pSWiftFile = '20') then
      k := 0;
    end if;
  
    --if iPosStart > 0 Then
    for m in nstartReplace - 6 .. imsglen loop
      if Dbms_Lob.substr(pclobContent, 1, m) = ':' and
         Dbms_Lob.substr(pclobContent, 1, m + 1 + ilen) = ':' then
      
        v_Compare := Dbms_Lob.substr(pclobContent, 2, m + 1); -- substr(pSWiftFile, 1, 2);
        if (ilen = 3) And substr(pSWiftFile, 3, 1) = 'a' then
          v_Compare := v_Compare || 'a';
        else
          v_Compare := v_Compare || Dbms_Lob.substr(pclobContent, 1, m + 3);
        end if;
        If (substr(v_Compare, 1, ilen) = substr(pSWiftFile, 1, ilen)) then
          if (ilen = 3) And substr(pSWiftFile, 3, 1) = 'a' then
            v_ReturnContent(0) := ''; --Dbms_Lob.substr(pclobContent, 1, m + 3);
          end if;
        
          if (ilen = 3 AND substr(pSWiftFile, 3, 1) = 'a') then
            v_ReturnContent(0) := ''; --dbms_lob.substr(pclobContent, 1, m + 3);
          End if;
        
          k := 1;
        
          for i in m + ilen + 2 .. iMSGLen Loop
          
            v_char_1 := dbms_lob.substr(pclobContent, 1, i - 1);
            v_char   := dbms_lob.substr(pclobContent, 1, i);
          
            v_char1 := dbms_lob.substr(pclobContent, 1, i + 1);
          
            v_char2 := dbms_lob.substr(pclobContent, 1, i + 2);
          
            v_char3 := dbms_lob.substr(pclobContent, 1, i + 3);
            v_char4 := dbms_lob.substr(pclobContent, 1, i + 4);
            vtest   := v_char_1 || v_char || v_char1 || v_char2 || v_char3 ||
                       v_char4;
          
            if ((v_char = '-' AND v_char1 = '}') Or
               ((v_char_1 = chr(10) or (v_char_1 = chr(13))) And
               v_char = ':' And (v_char3 = ':' or v_char4 = ':'))) Then
            
              Exit;
            END IF;
          
            iPosEnd := i;
            v_char  := dbms_lob.substr(pclobContent, 1, i);
            IF v_char = chr(10) Then
              --Kiem tra 
              if (isOptionalTag AND (k = 1) And
                 dbms_lob.substr(pclobContent, 1, i) <> '/') then
                -- Cai nay lam gi day
                v_ReturnContent(2) := v_ReturnContent(1);
                v_ReturnContent(1) := ' ';
              
                k := 3;
                l := i + 1;
              else
                if (k > 10) then
                  k := k;
                end if;
                k := k + 1;
                l := i + 1;
              End if;
              -- lay noi dung cua truong dien        
            
            else
            
              IF dbms_lob.substr(pclobContent, 1, i) <> chr(13) Then
                --lay vi tri dau tien  cua doan du lieu tiep theo
              
                --iRow_pos := i + 1;
              
                v_Fieldcontent := dbms_lob.substr(pclobContent, 1, i);
                v_ReturnContent(k) := v_ReturnContent(k) || v_Fieldcontent;
                v_Fieldcontent := v_ReturnContent(k);
              
              end if;
            end if;
          
          End loop;
        
          Exit;
        End if;
      end if;
    
    end loop;
  
    if (substr(pSWiftFile, 1, 3) = '23E') then
    
      v_ReturnContent(2) := substr(v_ReturnContent(2), 1, 5) ||
                            substr(v_ReturnContent(1), 5);
      v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 4);
    End if;
  
    if pSWiftFile = '32A' Then
      --vtest := v_ReturnContent(1);
      -- Tach truong 32A ra thanh 3 phan ngay, loai tien, so tien
      v_ReturnContent(2) := substr(v_ReturnContent(1), 7, 3);
      v_ReturnContent(3) := substr(v_ReturnContent(1), 10);
      v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 6);
      -- lay loai tien
      -- lay ngay create date
      -- Remember that SWIFT date format is YYMMDD, while SIBS is DDMMYY
      begin
      
        if trim(v_ReturnContent(1)) is not null then
          v_Fieldcontent := to_char(to_date(v_ReturnContent(1), 'YYMMDD'),
                                    'YYMMDD');
        
        else
          v_Fieldcontent := null;
        end if;
      
      exception
        when others then
          v_Fieldcontent := null;
      End;
    
    End if;
  
    if (pSWiftFile = '32B') then
    
      v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
      v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
    END IF;
    if (pSWiftFile = '33B') then
      v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
      v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
    End if;
  
    if (pSWiftFile = '71F') then
      v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
      v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
    End if;
    if (pSWiftFile = '71G') then
      v_ReturnContent(2) := substr(v_ReturnContent(1), 4);
      v_ReturnContent(1) := substr(v_ReturnContent(1), 1, 3);
    End if;
  
    --if m_Type_SWIFT_Content.Department = 'RM' then
    v_ReturnContent_temp := v_ReturnContent;
    ----------------------
    /*Phuong sua: 26052010
      Reason : chat field khong chuan    
      Edit  : Comment all   
    */
    /*if m_MSG_TYPE = 'MT103' then
      if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '53' or
         substr(pSWiftFile, 1, 2) = '50' or substr(pSWiftFile, 1, 2) = '54' or
         substr(pSWiftFile, 1, 2) = '55' or substr(pSWiftFile, 1, 2) = '56' or
         substr(pSWiftFile, 1, 2) = '57' or substr(pSWiftFile, 1, 2) = '59' then
        v_check_Acc := v_ReturnContent(1);
      if substr(v_check_Acc, 1, 1) <> '/' then
          for i in 2 .. 10 loop
            v_check_Acc := v_ReturnContent(i);
          
            v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
          end loop;
          v_ReturnContent(1) := '';
        end if;
      end if;
    end if;
    
    if m_MSG_TYPE = 'MT191' then
      if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '57' then
        v_check_Acc := v_ReturnContent(1);
        if substr(v_check_Acc, 1, 1) <> '/' then
          for i in 2 .. 10 loop
            v_check_Acc := v_ReturnContent(i);
            v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
          end loop;
          v_ReturnContent(1) := '';
        end if;
      
      end if;
    
    end if;
    
    if m_MSG_TYPE = 'MT202' then
      if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '53' or
         substr(pSWiftFile, 1, 2) = '54' or substr(pSWiftFile, 1, 2) = '58' or
         substr(pSWiftFile, 1, 2) = '56' or substr(pSWiftFile, 1, 2) = '57' then
        v_check_Acc := v_ReturnContent(1);
        if substr(v_check_Acc, 1, 1) <> '/' then
          for i in 2 .. 10 loop
            v_check_Acc := v_ReturnContent(i);
            v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
          end loop;
          v_ReturnContent(1) := '';
        end if;
      
      end if;
    
    end if;
    
    if m_MSG_TYPE = 'MT900' then
      if substr(pSWiftFile, 1, 2) = '52'
      
       then
        v_check_Acc := v_ReturnContent(1);
        if substr(v_check_Acc, 1, 1) <> '/' then
          for i in 2 .. 10 loop
            v_check_Acc := v_ReturnContent(i);
            v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
          end loop;
          v_ReturnContent(1) := '';
        end if;
      
      end if;
    
    end if;
    if m_MSG_TYPE = 'MT910' then
      if substr(pSWiftFile, 1, 2) = '52' or substr(pSWiftFile, 1, 2) = '50' or
         substr(pSWiftFile, 1, 2) = '56'
      
       then
        v_check_Acc := v_ReturnContent(1);
        if substr(v_check_Acc, 1, 1) <> '/' then
          for i in 2 .. 10 loop
            v_check_Acc := v_ReturnContent(i);
            v_ReturnContent(i) := v_ReturnContent_temp(i - 1);
          end loop;
          v_ReturnContent(1) := '';
        end if;
      
      end if;
    
    end if;
    */
    ------------------------------
    -- end if;
    --
    Return v_ReturnContent;
  Exception
    when OTHERS THEN
    
      return v_ReturnContent;
  END SWIFT_RM_GETFIELD_IN;

  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: Lay noi dung cua cac truong trong dien
  Co tinh den cac truong hop cua cac truong dien dac biet phai xu lys rieng
  
    Ten ham:  SWIFT_RM_GETFIELD_IN()
  Tham so:  pSWiftFile varchar2, pclobContent clob
  Mo ta: -  
     
  Ngay khoi tao:  13/06/2008
  Nguoi sua:    
  Ngay sua:   
  Mo ta     
  
  **********************************************************************/

  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                pclobContent clob,
                                m_MSG_TYPE   varchar2,
                                prownum      integer) return m_tblField_type IS
    v_ReturnContent      m_tblField_type;
    v_ReturnContent_temp m_tblField_type;
    -- iPosStart       integer := 0;
    iPosEnd Integer := 0;
    --iPosStart Integer := 0;
    ilen        Integer := 0;
    iMSGLen     Integer := 0;
    v_Compare   varchar2(6);
    v_check_Acc varchar2(50);
    --v_FieldNext     varchar2(6);
    isOptionalTag  boolean := false;
    k              integer := 0;
    nstartReplace  integer := 0;
    v_Fieldcontent varchar2(4000);
    v_char_1       varchar2(2);
    v_char         varchar2(2);
    v_char1        varchar2(2);
    v_char2        varchar2(2);
    v_char3        varchar2(2);
    v_char4        varchar2(200);
    l              integer := 0;
    --v_char5        varchar2(2);
    vtest varchar2(2000);
    --vtest1 varchar2(2000);
    -- vtest2 varchar2(20);
  BEGIN
    for i in 0 .. 30 loop
      v_ReturnContent(i) := '';
    end loop;
    k       := 0;
    iMSGLen := Length(pclobContent);
    ilen    := Length(trim(pSWiftFile));
    -- lay ra vi tri dau tien cua doan chua du lieu Thuoc swift file name (VD field 20)
    nstartReplace := Dbms_Lob.instr(pclobContent, ':' || pSWiftFile || ':');
  
    --if iPosStart > 0 Then
    for m in nstartReplace - 6 .. imsglen loop
      if Dbms_Lob.substr(pclobContent, 1, m) = ':' and
         Dbms_Lob.substr(pclobContent, 1, m + 1 + ilen) = ':' then
      
        v_Compare := Dbms_Lob.substr(pclobContent, 2, m + 1); -- substr(pSWiftFile, 1, 2);
        if (ilen = 3) And substr(pSWiftFile, 3, 1) = 'a' then
          v_Compare := v_Compare || 'a';
        else
          v_Compare := v_Compare || Dbms_Lob.substr(pclobContent, 1, m + 3);
        end if;
        If (substr(v_Compare, 1, ilen) = substr(pSWiftFile, 1, ilen)) then
          if (ilen = 3) And substr(pSWiftFile, 3, 1) = 'a' then
            v_ReturnContent(0) := Dbms_Lob.substr(pclobContent, 1, m + 3);
          end if;
        
          if (ilen = 3 AND substr(pSWiftFile, 3, 1) = 'a') then
            v_ReturnContent(0) := dbms_lob.substr(pclobContent, 1, m + 3);
          End if;
        
          k := 1;
        
          for i in m + ilen + 2 .. iMSGLen Loop
          
            v_char_1 := dbms_lob.substr(pclobContent, 1, i - 1);
            v_char   := dbms_lob.substr(pclobContent, 1, i);
          
            v_char1 := dbms_lob.substr(pclobContent, 1, i + 1);
          
            v_char2 := dbms_lob.substr(pclobContent, 1, i + 2);
          
            v_char3 := dbms_lob.substr(pclobContent, 1, i + 3);
            v_char4 := dbms_lob.substr(pclobContent, 1, i + 4);
          
            if ((v_char = '-' AND v_char1 = '}') Or
               ((v_char_1 = chr(10) or (v_char_1 = chr(13))) And
               v_char = ':' And (v_char3 = ':' or v_char4 = ':'))) Then
            
              Exit;
            END IF;
          
            iPosEnd := i;
            v_char  := dbms_lob.substr(pclobContent, 1, i);
            IF v_char = chr(10) Then
              --Kiem tra 
              if (isOptionalTag AND (k = 1) And
                 dbms_lob.substr(pclobContent, 1, i) <> '/') then
                -- Cai nay lam gi day
                v_ReturnContent(2) := v_ReturnContent(1);
                v_ReturnContent(1) := ' ';
              
                k := 3;
                l := i + 1;
              else
                if (k > 10) then
                  k := k;
                end if;
                k := k + 1;
                l := i + 1;
              End if;
              -- lay noi dung cua truong dien        
            
            else
            
              IF dbms_lob.substr(pclobContent, 1, i) <> chr(13) Then
                --lay vi tri dau tien  cua doan du lieu tiep theo
              
                --iRow_pos := i + 1;
              
                v_Fieldcontent := dbms_lob.substr(pclobContent, 1, i);
                v_ReturnContent(k) := v_ReturnContent(k) || v_Fieldcontent;
                v_Fieldcontent := v_ReturnContent(k);
              
              end if;
            end if;
          
          End loop;
        
          Exit;
        End if;
      end if;
    
    end loop;
  
    if pSWiftFile = '32A' Then
      v_ReturnContent(1) := Replace(v_ReturnContent(1), ',', '.');
    End if;
  
    if (pSWiftFile = '32B' or pSWiftFile = '33B' or pSWiftFile = '71F' or
       pSWiftFile = '71G') then
      v_ReturnContent(1) := Replace(v_ReturnContent(1), ',', '.');
    END IF;
    Return v_ReturnContent;
  Exception
    when OTHERS THEN
    
      return v_ReturnContent;
  END SWIFT_RM_GETFIELD_IN;

  ----------------------------------------
  -- Lay noi dung ca field dien
  ----------------------------------------

  FUNCTION SWIFT_RM_GETFIELD_IN(pFieldName varchar2, pCONTENT clob)
    return varchar2 IS
    v_returnContent varchar2(1000);
    vstrTemp        varchar2(1500);
    iposStart       integer;
  Begin
    -- lay toan bo row
  
    iposStart := dbms_lob.instr(pCONTENT,
                                ':' || UPPER(LTRIM(Trim(pFieldName), 'F')) || ':');
  
    if iposStart > 0 then
    
      vstrTemp := dbms_lob.substr(pCONTENT, 1000, iposStart + 4);
      if (substr(vstrTemp, 1, 1) = '/') or
         (substr(vstrTemp, 1, 1) = ':' and substr(vstrTemp, 2, 1) <> '/') then
        vstrTemp := substr(vstrTemp, 2, length(vstrTemp) - 1);
      elsif (substr(vstrTemp, 1, 1) = ':') and
            (substr(vstrTemp, 2, 1) = '/') then
        vstrTemp := substr(vstrTemp, 3, length(vstrTemp) - 2);
      elsif (substr(vstrTemp, 2, 1) = ':') and
            (substr(vstrTemp, 3, 1) = '/') then
        vstrTemp := substr(vstrTemp, 4, length(vstrTemp) - 3);
      end if;
      for i in 1 .. Length(vstrTemp) loop
        if substr(vstrTemp, i, 1) = ':' And
           (substr(vstrTemp, i + 4, 1) = ':' or
            substr(vstrTemp, i + 3, 1) = ':') AND
           (substr(vstrTemp, i - 1, 1) = chr(10) or
            substr(vstrTemp, i - 1, 1) = chr(13)) or
           substr(vstrTemp, i, 1) = '}' then
          v_returnContent := substr(vstrTemp, 1, i - 1);
          exit;
        end if;
      
      end loop;
    
    end if;
    Return v_returnContent;
  end SWIFT_RM_GETFIELD_IN;

  FUNCTION VCB_GET_FIELD(pCONTENT   VARCHAR2,
                         pFieldName VARCHAR2,
                         m_MSG_TYPE nvarchar2) RETURN VARCHAR2 IS
  
  BEGIN
    RETURN GW_PK_VCB_REPORT.VCB_GET_SWIFT_Field(pCONTENT,
                                                pFieldName,
                                                0,
                                                0,
                                                m_MSG_TYPE);
  
  END VCB_GET_FIELD;
  ---------------------
  FUNCTION VCB_GET_SWIFT_Field(pCOntent   clob,
                               pFiledCode varchar2,
                               pRownum    number,
                               pPartnum   number,
                               m_MSG_TYPE varchar2) Return Varchar2 IS
    v_Value varchar2(2000);
  
    v_ReturnContent m_tblField_type;
  
  BEGIN
  
    -- Lay noi dung dien theo tung rownum/partnumb
    if pRownum > 0 and pPartnum > 0 then
      v_ReturnContent := SWIFT_RM_GETFIELD_IN(pFiledCode,
                                              pCOntent,
                                              m_MSG_TYPE);
      v_Value         := GetFieldValue(pFiledCode,
                                       pRownum,
                                       pPartnum,
                                       v_ReturnContent);
    else
      -- lay ca 1 rownum cua field
      if pRownum > 0 and pPartnum = 0 then
        v_ReturnContent := SWIFT_RM_GETFIELD_IN(pFiledCode,
                                                pCOntent,
                                                m_MSG_TYPE);
        v_Value         := GetFieldValue(pFiledCode,
                                         pRownum,
                                         0,
                                         v_ReturnContent);
      
      else
        --lay ca noi dung 1 field
        v_Value := SWIFT_RM_GETFIELD_IN(pFiledCode, pCOntent);
      end if;
    
    end if;
    v_Value := Ltrim(v_Value, chr(13));
    v_Value := Ltrim(v_Value, chr(10));
    v_Value := Rtrim(v_Value, chr(13));
  
    if substr(pFiledCode, 3, 1) = 'a' then
      v_Value := '';
    end if;
    return Rtrim(Rtrim(v_Value, chr(13)), chr(10));
  Exception
    when others then
      Return '';
  END VCB_GET_SWIFT_Field;
  FUNCTION GetFieldValue(pSWFieldTag  varchar2,
                         piRowNum     integer,
                         piPartNum    Integer,
                         pFilecontent m_tblField_type) return Varchar2 IS
  
    v_Value varchar2(4000);
    ipos    integer := 0;
    ipos1   integer := 0;
    vtest   varchar2(4000);
    --Test1   varchar2(200) := '';
  BEGIN
    if piRowNum > pFilecontent.LAST then
      return ' ';
    end if;
    v_Value := '';
  
    if (not pFilecontent(0) is null) Then
    
      if (substr(pFilecontent(0), 1, 1) = substr(pSWFieldTag, 3, 1) or
         piRowNum = 0) Then
        v_Value := pFilecontent(piRowNum);
      else
        v_Value := '';
      End if;
    else
      v_Value := pFilecontent(piRowNum);
      if (substr(pSWFieldTag, 1, 2) = '72') Then
        -- neu dong chi co 1 phan partnum=1
        if (piPartNum = 1) Then
          if (substr(pFilecontent(piRowNum), 1, 1) = '/' AND
             substr(pFilecontent(piRowNum), 2, 1) <> '/') Then
            ipos1 := instr(substr(pFilecontent(piRowNum), 2, 9), '/');
            if ipos1 > 0 then
              v_Value := substr(pFilecontent(piRowNum), 2, ipos1 - 1);
            end if;
          
          else
          
            v_Value := '';
          End if;
        else
          -- Neu dong co 2 phan PartNum==2
          -- kiem tra xem co ky tu phan biet tung phan cua dien hay khong
          if (substr(pFilecontent(piRowNum), 1, 1) = '/' AND
             substr(pFilecontent(piRowNum), 2, 1) <> '/') then
            ipos := instr(substr(pFilecontent(piRowNum), 3), '/');
          
            --ipos    := instr(substr(pFilecontent(piRowNum), j + 1), '/');
            v_Value := substr(pFilecontent(piRowNum), ipos + 1);
            ipos    := instr(v_Value, '/');
            v_Value := substr(v_Value, ipos + 1);
          else
            v_Value := pFilecontent(piRowNum);
          
          end if;
        End if;
      end if;
    End if;
    if (substr(pSWFieldTag, 1, 2) = '72') Then
      if substr(v_Value, 1, 1) <> '/' then
        v_Value := '/' || v_Value;
      end if;
    else
      v_Value := LTrim(v_Value, '/');
    end if;
  
    v_Value := LTrim(v_Value, '/');
  
    if substr(pSWFieldTag, 3, 1) = 'a' then
      v_Value := '';
    end if;
    Return Replace(Replace(v_Value, chr(13)), chr(10));
  Exception
    when OTHERS THEN
      v_Value := '';
      return v_Value;
  END;
  ---------------------------------------------
  FUNCTION GET_BRANCH_NAME(pSIBSBANKCODE varchar2) return varchar2 IS
    vReturn varchar2(200);
  BEGIN
    SELECT bran_name
      into vReturn
      FROM BRANCH
     where LPAD(sibs_bank_code, 5, '0') = LPAD(pSIBSBANKCODE, 5, '0')
       and rownum = 1;
    Return vReturn;
  Exception
    when others then
      Return ' ';
  END GET_BRANCH_NAME;
  ---------------------------------------------

  FUNCTION GET_BRANCH_ID_NAME(pSIBSBANKCODE varchar2) return varchar2 IS
    vReturn varchar2(200);
  BEGIN
    SELECT bran_name
      into vReturn
      FROM BRANCH
     where LPAD(sibs_bank_code, 5, '0') = LPAD(pSIBSBANKCODE, 5, '0')
       and rownum = 1;
    /* IF vReturn ='' or vReturn is null then
       SELECT  M.BANK_NAME
         into vReturn
         FROM Swift_Bank_Map M
         where LPAD(M.Sibs_Bank_Code, 5, '0') = LPAD(pSIBSBANKCODE, 5, '0')
         and rownum = 1;
    else
       vReturn:=  substr(pSIBSBANKCODE,-3) || ' - ' || vReturn ;
    end if;    */
    vReturn := substr(pSIBSBANKCODE, -3) || ' - ' || vReturn;
    Return vReturn;
  Exception
    when others then
      Return ' ';
  END GET_BRANCH_ID_NAME;
END;
/
