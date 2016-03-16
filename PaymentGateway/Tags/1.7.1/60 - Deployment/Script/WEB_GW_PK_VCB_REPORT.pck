CREATE OR REPLACE PACKAGE GW_PK_VCB_REPORT IS
  TYPE m_tblField_type IS TABLE OF Varchar2(2000) INDEX BY Varchar2(20);
  /*---------------------------------------------------------------------
  Ten ham: GETTYPE
  Muc dich: So sanh ngay lay bao cao voi ngay he thong hien tai
  Mo ta:
  Tham so: pTransDate: Ngay thuc hien giao dich
  Tra ve: Kieu sau so sanh: 1: trong ngay, 2: trong thang, 3: trong nam
  Nguoi tao: huypq
  Ngay tao: 06/2008
  Nguoi sua:
  Ngay sua:
  Version: 1.0
  ---------------------------------------------------------------------*/
  FUNCTION GETTYPE(pTransDate Date) RETURN NUMBER;

  PROCEDURE VCB_PRINT(pid             in number,
                      RefCurVCB_PRINT IN OUT PKG_CR.RefCurType);
  PROCEDURE VCB_BM02(pTRAN_DATE  IN DATE,
                     pBranch     IN VARCHAR2,
                     pCCY        IN VARCHAR2,
                     RefCurVCB02 OUT PKG_CR.RefCurType);
  PROCEDURE VCB_BM04(pTRAN_DATE   IN DATE,
                     pSession     IN OUT NUMBER,
                     pCCY         IN VARCHAR2,
                     pPrintStatus IN OUT NUMBER,
                     RefCurVCB04  OUT PKG_CR.RefCurType);
  FUNCTION GET_FIELD(pCONTENT VARCHAR2, pFieldName VARCHAR2) RETURN VARCHAR2;

  PROCEDURE VCB_BM06(pTRAN_DATE   IN DATE,
                     pSession     IN OUT NUMBER,
                     pCCY         IN VARCHAR2,
                     pPrintStatus IN OUT NUMBER,
                     pISCHARGE    IN VARCHAR2,
                     RefCurVCB04  OUT PKG_CR.RefCurType);
  FUNCTION VCB_GET_FIELD(pCONTENT   VARCHAR2,
                         pFieldName VARCHAR2,
                         m_MSG_TYPE nvarchar2) RETURN VARCHAR2;                     
  FUNCTION VCB_GET_SWIFT_Field(pCOntent   clob,
                               pFiledCode varchar2,
                               pRownum    number,
                               pPartnum   number,
                               m_MSG_TYPE varchar2) Return Varchar2;                          
  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                pclobContent clob,
                                m_MSG_TYPE   varchar2) return m_tblField_type;

  FUNCTION SWIFT_RM_GETFIELD_IN(pSWiftFile   varchar2,
                                pclobContent clob,
                                m_MSG_TYPE   varchar2,
                                prownum      integer) return m_tblField_type;

  FUNCTION SWIFT_RM_GETFIELD_IN(pFieldName varchar2, pCONTENT clob)
    return varchar2;
  
  FUNCTION GetFieldValue(pSWFieldTag  varchar2,
                         piRowNum     integer,
                         piPartNum    Integer,
                         pFilecontent m_tblField_type) return Varchar2;
  PROCEDURE VCB_PRINT_MSG(pMSG_ID    in   NUMBER,
                          pMSG_TYPE  in   varchar2,
                          pMSGDirection in varchar2,
                          pCurContent  in out PKG_CR.RefCurType);
  FUNCTION GET_BRANCH_NAME(pSIBSBANKCODE varchar2) return varchar2; 
                           
  PROCEDURE VCB_03(pdate        IN date,
                   pCCYCD       in varchar2 default 'VND',
                   pBranchA     in varchar2,
                   pBranchB     in varchar2,
                   RefCurVCB_03 IN OUT PKG_CR.RefCurType);               
  FUNCTION GET_BRANCH_ID_NAME(pSIBSBANKCODE varchar2) return varchar2;                   
END; -- Package spec
/
CREATE OR REPLACE PACKAGE BODY GW_PK_VCB_REPORT IS



  /*
  Ten ham: GETTYPE
  Muc dich: So sanh ngay lay bao cao voi ngay he thong hien tai
  Mo ta:
  Tham so: pTransDate: Ngay thuc hien giao dich
  Tra ve: Kieu sau so sanh: 1: trong ngay, 2: trong thang, 3: trong nam
  Nguoi tao: huypq
  Ngay tao: 06/2008
  Nguoi sua:
  Ngay sua:
  Version: 1.0
  */
  FUNCTION GETTYPE(pTransDate DATE) RETURN NUMBER IS
    pType    NUMBER;
    pSysDate DATE;
  BEGIN
    pType := 0;
    --Lay ngay he thong
    SELECT SYSDATE INTO pSysDate FROM DUAL;
    --pSysDate:=to_date('28-JUN-2008','DD/MM/YYYY');
  
    --Kiem tra neu la ngay hien tai: pType = 1
    IF to_char(pTransDate, 'DD/MM/YYYY') = to_char(pSysDate, 'DD/MM/YYYY') THEN
      pType := 1;
      --Ngay trong thang
    ELSIF substr(to_char(pTransDate, 'DD/MM/YYYY'), 4, 2) =
          substr(to_char(pSysDate, 'DD/MM/YYYY'), 4, 2) THEN
      pType := 2;
      --Ngay trong nam
    ELSE
      pType := 3;
    END IF;
    --Gia tri tra ve
    Return pType;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN pType;
  END GETTYPE;

  PROCEDURE VCB_PRINT(pid             in number,
                      RefCurVCB_PRINT IN OUT PKG_CR.RefCurType) IS
    vMsg_type varchar2(10) := '';    
    pQUERY_ID      NUMBER(20);
    pTRANS_DATE    DATE;
    pFILE_NAME     NVARCHAR2(400);
    pFIELD_CODE    Varchar2(30) := '';
    pMSG_TYPE      varchar2(10);
    pCONTENT       Clob;
    pMSG_DIRECTION varchar2(20);
    vMSG_DIREC     varchar2(1);    
    
  BEGIN
  Begin
      Select QUERY_ID,
             TRANS_DATE,
             FILE_NAME,
             MSG_TYPE,
             CONTENT,
             MSG_DIRECTION
        into pQUERY_ID,
             pTRANS_DATE,
             pFILE_NAME,
             pMSG_TYPE,
             pCONTENT,
             pMSG_DIRECTION
        from VCB_MSG_CONTENT
       where MSG_ID = pid and rownum=1;
    EXCEPTION
      when OTHERS THEN      
        Begin
          Select QUERY_ID,
                 TRANS_DATE,
                 FILE_NAME,
                 MSG_TYPE,
                 CONTENT,
                 MSG_DIRECTION
            into pQUERY_ID,
                 pTRANS_DATE,
                 pFILE_NAME,
                 pMSG_TYPE,
                 pCONTENT,
                 pMSG_DIRECTION
            from VCB_MSG_ALL
           where MSG_ID = pid  and rownum=1;
        EXCEPTION
          when OTHERS THEN
            Begin
              Select QUERY_ID,
                     TRANS_DATE,
                     FILE_NAME,
                     MSG_TYPE,
                     CONTENT,
                     MSG_DIRECTION
                into pQUERY_ID,
                     pTRANS_DATE,
                     pFILE_NAME,
                     pMSG_TYPE,
                     pCONTENT,
                     pMSG_DIRECTION
                from VCB_MSG_ALL_HIS
               where MSG_ID = pid  and rownum=1;
            EXCEPTION
              when OTHERS THEN
                vMsg_type := '';
            END;
        END;
    end;
    if pMSG_DIRECTION = 'VCB-SIBS' then
      vMSG_DIREC := '0';
    else
      vMSG_DIREC := '1';
    end if;
    GW_PK_SWIFT_REPORT.SWIFT_CONVERT_MSG(pMSG_TYPE, pCONTENT, vMSG_DIREC);
  
    --Xoa bang temp detail
    delete from vcb_temp;
    insert into vcb_temp
      (MSG_ID,
       FIELD_NAME,
       FIELD_VALUE,
       ROW_NUM,
       PART_ROW,
       FILE_NAME,
       TRANS_DATE)
      select pid,
             FIELD_NAME,
             FIELD_VALUE,
             ROW_NUM,
             PART_ROW,
             pFILE_NAME,
             pTRANS_DATE
        from SWIFT_FIELD_TEMP;       
       
    open RefCurVCB_PRINT for
      select K.QUERY_ID,
             K.msg_type,
             K.branch_a as branch_a,
             K.branch_b as branch_b,
             K.field_name,
             K.field_value,
             K.Trans_date,
             V.E_FIELD_NAME,
             Y.E_MSG_NAME,
             M.branch_sender,
             N.branch_receive,
             B.CONTENT AS STATUS,
             K.ROW_NUM,
             K.msg_direction
        from (select X.*, T.FIELD_NAME, T.field_value, T.ROW_NUM
                from (select *
                        from (select query_id,
                                     MSG_ID,
                                     msg_type,
                                     branch_a,
                                     branch_b,
                                     status,
                                     Trans_date,
                                     msg_direction
                                from vcb_msg_content
                               where MSG_ID = pid
                              union
                              select query_id,
                                     MSG_ID,                              
                                     msg_type,
                                     branch_a,
                                     branch_b,
                                     status,
                                     Trans_date,
                                     msg_direction
                                from vcb_msg_all
                               where MSG_ID = pid
                              union
                              select query_id,
                                     MSG_ID,
                                     msg_type,
                                     branch_a,
                                     branch_b,
                                     status,
                                     Trans_date,
                                     msg_direction
                                from vcb_msg_all_his
                               where MSG_ID = pid)
                       where rownum = 1) X
                left join (select u.MSG_ID,
                                 u.FIELD_NAME,
                                 u.field_value,
                                 u.ROW_NUM --,V.E_FIELD_NAME,V.msg_type
                            from (select max(MSG_ID) MSG_ID,
                                         FIELD_NAME,
                                         max(decode(PART_ROW,
                                                    0,
                                                    FIELD_VALUE,
                                                    '')) || ' ' ||
                                         max(decode(PART_ROW,
                                                    1,
                                                    FIELD_VALUE,
                                                    '')) || ' ' ||
                                         max(decode(PART_ROW,
                                                    2,
                                                    FIELD_VALUE,
                                                    '')) field_value,
                                         max(ROW_NUM) ROW_NUM
                                    from vcb_temp
                                   where MSG_ID = pid
                                     and FIELD_NAME not in
                                         ('32A', '33B', '71F')
                                   group by FIELD_NAME, ROW_NUM
                                  union
                                  select max(MSG_ID) MSG_ID,
                                         max(FIELD_NAME) FIELD_NAME,
                                         max(decode(row_num,
                                                    1,
                                                    FIELD_VALUE,
                                                    '')) || ' ' ||
                                         max(decode(row_num,
                                                    2,
                                                    FIELD_VALUE,
                                                    '')) || ' ' ||
                                         max(decode(row_num,
                                                    3,
                                                    FIELD_VALUE,
                                                    '')) field_value,
                                         max(ROW_NUM) ROW_NUM
                                    from vcb_temp
                                   where MSG_ID = pid
                                     and FIELD_NAME in ('32A', '33B', '71F')
                                   group by FIELD_NAME
                                  --order by FIELD_NAME asc
                                  ) U
                           order by u.FIELD_NAME, u.ROW_NUM) T
                  on t.MSG_ID = X.MSG_ID
              
              ) K
        left join (select FIELD_NAME, E_FIELD_NAME, msg_type from msgfield) V
          on upper(trim(K.FIELD_NAME)) = upper(trim(V.FIELD_NAME))
         and substr(trim(K.msg_type), 3, 5) = trim(V.msg_type)
        left join msgtype Y
          on substr(trim(K.msg_type), 3, 5) = trim(Y.msg_type)
        left join (SELECT lpad(SIBS_BANK_CODE, 5, '0') as SIBS_BANK_CODE,
                          BRAN_NAME branch_sender
                     FROM branch) M
          ON TRIM(K.branch_a) = TRIM(M.sibs_bank_code)
        left join (SELECT lpad(SIBS_BANK_CODE, 5, '0') as SIBS_BANK_CODE,
                          BRAN_NAME branch_receive
                     FROM branch) N
          ON TRIM(K.branch_b) = TRIM(N.SIBS_BANK_CODE)
        LEFT JOIN (SELECT CONTENT, CDVAL
                     FROM allcode
                    WHERE gwtype = 'SYSTEM'
                      and cdname = 'STATUS') B
          ON trim(K.status) = B.cdval
       order by K.QUERY_ID, K.field_name, K.ROW_NUM asc;
  END VCB_PRINT;
  
  
  PROCEDURE VCB_BM02(pTRAN_DATE  IN DATE,
                     pBranch     IN VARCHAR2,
                     pCCY        IN VARCHAR2,
                     RefCurVCB02 OUT PKG_CR.RefCurType) IS
    pDate Varchar2(8);
  BEGIN
    --Convert pTRAN_DATE to Varchar2
    pDate := to_char(pTRAN_DATE, 'YYYYMMDD');
    --Xoa bang tam
    DELETE FROM VCB_TEMP02;
    --INSERT DU LIEU BANG VCB_MSG_CONTENT VAO BANG VCB_TEMP02
    INSERT INTO VCB_TEMP02
      (MSG_ID,
       QUERY_ID,
       MSG_TYPE,
       BRANCH_A,
       BRANCH_B,
       AMOUNT,
       CCYCD,
       STATUS,
       RM_NUMBER,
       FIELD20,
       FIELD21,
       TRANS_NO,
       FOREIGN_BANK,
       FOREIGN_BANK_NAME)
      SELECT A.MSG_ID,
             A.QUERY_ID,
             A.MSG_TYPE,
             GW_PK_LIB.SWIFT_RM_GETFIELD_IN('57D',A.CONTENT) AS BRANCH_A,
            /* (SELECT B.FIELD_VALUE
                FROM VCB_MSGDTL B
               WHERE UPPER(B.FIELD_NAME) = '57D'
                 AND A.QUERY_ID = B.QUERY_ID
                 AND ROWNUM = 1) AS BRANCH_A,*/
             A.BRANCH_B,
             A.AMOUNT,
             A.CCYCD,
             (SELECT CONTENT
                FROM ALLCODE B
               WHERE B.CDVAL = to_char(A.STATUS)
                 AND GWTYPE = 'SYSTEM'
                 AND CDNAME = 'STATUS') AS STATUS,
             LTRIM(A.RM_NUMBER, '0000'),
             A.FIELD20,
             A.FIELD21,
             A.TRANS_NO,
             FOREIGN_BANK,
             FOREIGN_BANK_NAME
        FROM VCB_MSG_CONTENT A
      --       WHERE TO_DATE(A.TRANS_DATE, 'DD/MM/YYYY') = TO_DATE(pTRAN_DATE, 'DD/MM/YYYY')
       WHERE A.TRANSDATE = pDate
         AND LPAD(TRIM(A.BRANCH_A), 5, '0') LIKE
             '%' || LPAD(TRIM(pBranch), 5, '0') || '%'
         AND UPPER(TRIM(A.CCYCD)) LIKE
             '%' || DECODE(pCCY, 'ALL', '', UPPER(TRIM(pCCY))) || '%'
         AND A.MSG_DIRECTION = 'SIBS-VCB';
    --INSERT DU LIEU BANG VCB_MSG_ALL VAO BANG VCB_TEMP02
    INSERT INTO VCB_TEMP02
      (MSG_ID,
       QUERY_ID,
       MSG_TYPE,
       BRANCH_A,
       BRANCH_B,
       AMOUNT,
       CCYCD,
       STATUS,
       RM_NUMBER,
       FIELD20,
       FIELD21,
       TRANS_NO,
       FOREIGN_BANK,
       FOREIGN_BANK_NAME)
      SELECT A.MSG_ID,
             A.QUERY_ID,
             A.MSG_TYPE,
             GW_PK_LIB.SWIFT_RM_GETFIELD_IN('57D',A.CONTENT) AS BRANCH_A,
             /*(SELECT B.FIELD_VALUE
                FROM VCB_MSGDTL_ALL B
               WHERE UPPER(B.FIELD_NAME) = '57D'
                 AND A.QUERY_ID = B.QUERY_ID
                 AND ROWNUM = 1) AS BRANCH_A,*/
             A.BRANCH_B,
             A.AMOUNT,
             A.CCYCD,
             (SELECT CONTENT
                FROM ALLCODE B
               WHERE B.CDVAL = to_char(A.STATUS)
                 AND GWTYPE = 'SYSTEM'
                 AND CDNAME = 'STATUS') AS STATUS,
             LTRIM(A.RM_NUMBER, '0000'),
             A.FIELD20,
             A.FIELD21,
             A.TRANS_NO,
             FOREIGN_BANK,
             FOREIGN_BANK_NAME
        FROM VCB_MSG_ALL A
      --       WHERE TO_DATE(A.TRANS_DATE, 'DD/MM/YYYY') = TO_DATE(pTRAN_DATE, 'DD/MM/YYYY')
       WHERE A.TRANSDATE = pDate
         AND LPAD(TRIM(A.BRANCH_A), 5, '0') LIKE
             '%' || LPAD(TRIM(pBranch), 5, '0') || '%'
         AND UPPER(TRIM(A.CCYCD)) LIKE
             '%' || DECODE(pCCY, 'ALL', '', UPPER(TRIM(pCCY))) || '%'
         AND A.MSG_DIRECTION = 'SIBS-VCB';
    --INSERT DU LIEU BANG VCB_MSG_ALL_HIS VAO BANG VCB_TEMP02
    INSERT INTO VCB_TEMP02
      (MSG_ID,
       QUERY_ID,
       MSG_TYPE,
       BRANCH_A,
       BRANCH_B,
       AMOUNT,
       CCYCD,
       STATUS,
       RM_NUMBER,
       FIELD20,
       FIELD21,
       TRANS_NO,
       FOREIGN_BANK,
       FOREIGN_BANK_NAME)
      SELECT A.MSG_ID,
             A.QUERY_ID,
             A.MSG_TYPE,
             GW_PK_LIB.SWIFT_RM_GETFIELD_IN('57D',A.CONTENT) AS BRANCH_A,
            /* (SELECT B.FIELD_VALUE
                FROM VCB_MSGDTL_ALL_HIS B
               WHERE UPPER(B.FIELD_NAME) = '57D'
                 AND A.QUERY_ID = B.QUERY_ID
                 AND ROWNUM = 1) AS BRANCH_A,*/
             A.BRANCH_B,
             A.AMOUNT,
             A.CCYCD,
             (SELECT CONTENT
                FROM ALLCODE B
               WHERE B.CDVAL = to_char(A.STATUS)
                 AND GWTYPE = 'SYSTEM'
                 AND CDNAME = 'STATUS') AS STATUS,
             LTRIM(A.RM_NUMBER, '0000'),
             A.FIELD20,
             A.FIELD21,
             A.TRANS_NO,
             FOREIGN_BANK,
             FOREIGN_BANK_NAME
        FROM VCB_MSG_ALL_HIS A
      --       WHERE TO_DATE(A.TRANS_DATE, 'DD/MM/YYYY') = TO_DATE(pTRAN_DATE, 'DD/MM/YYYY')
       WHERE A.TRANSDATE = pDate
         AND LPAD(TRIM(A.BRANCH_A), 5, '0') LIKE
             '%' || LPAD(TRIM(pBranch), 5, '0') || '%'
         AND UPPER(TRIM(A.CCYCD)) LIKE
             '%' || DECODE(pCCY, 'ALL', '', UPPER(TRIM(pCCY))) || '%'
         AND A.MSG_DIRECTION = 'SIBS-VCB';
    -- Du lieu lay ra
    OPEN RefCurVCB02 FOR
      SELECT MSG_ID,
             QUERY_ID,
             MSG_TYPE,
             MSG_DIRECTION,
             DEPARTMENT,
             BRANCH_A,
             BRANCH_B,
             FIELD20,
             FIELD21,
             AMOUNT,
             CCYCD,
             STATUS,
             DEPARTMENT,
             FOREIGN_BANK,
             FOREIGN_BANK_NAME,
             TRANS_NO,
             RM_NUMBER,
             PRIORITY,
             VALUE_DATE,
             TRAN_DATE,
             RECEIVING_TIME,
             SENDING_TIME
        FROM VCB_TEMP02
       ORDER BY CCYCD ASC, RM_NUMBER ASC, FIELD20 ASC;
  
  END;

  --Bang ke dien di theo phien
  PROCEDURE VCB_BM04(pTRAN_DATE   IN DATE,
                     pSession     IN OUT NUMBER,
                     pCCY         IN VARCHAR2,
                     pPrintStatus IN OUT NUMBER,
                     RefCurVCB04  OUT PKG_CR.RefCurType) IS
    pTYPE  NUMBER;
    pSess  NUMBER;
    iCount NUMBER;
    pDATE  VARCHAR2(8);
    pNDATE NUMBER(10);
    STT    NUMBER;
    pORDER NUMBER;
  BEGIN
    --Convert pTRAN_DATE to Varchar2
    pDATE := to_char(pTRAN_DATE, 'YYYYMMDD');
    --Convert pDATE to NUMBER
    pNDATE := TO_NUMBER(pDATE);
    --Gan pSession =0 neu pSession co gia tri null
    IF pSession is NULL THEN
      pSession := 0;
    END IF;
    --Gan pPrintStatus
    if pPrintStatus is not null and pPrintStatus = 2 then
      pPrintStatus := 2;
    else
      pPrintStatus := 1;
    end if;
    STT    := 0;
    pORDER := 0;
    --Lay pType sau khi so sanh ngay giao dich voi ngay he thong
    pTYPE := GETTYPE(pTRAN_DATE);
    --Xoa bang tam
    DELETE FROM vcb_temp04;
    --Ngay hien tai
    if pTYPE = 1 then
      --Lay phien max cua ngay bao cao
      SELECT MAX(SE)
        INTO pSess
        FROM VCB_BM04_SE
       WHERE NDATE = pNDATE
         AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
      --Neu chua co phien, gan = 0
      IF pSess is NULL THEN
        pSess := 0;
      END IF;
      --Tu dong tang phien len 1 khi NSD khong nhap phien hay nhap phien
      --bang 0 va ngay bao cao bang ngay he thong(ngay DB)
      pSess := pSess + 1;
      --Xet phien dua vao =0
      IF pSession = 0 THEN
        --LAY DU LIEU TU BANG IBPS_MSG_CONTENT
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 GET_FIELD(A.Content, '71F') || GET_FIELD(A.Content, '71G') as F71
            FROM VCB_MSG_CONTENT A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'SIBS-VCB'
             AND A.STATUS = 1
             AND A.MSG_ID NOT IN
                 (SELECT DISTINCT MSG_ID
                    FROM VCB_BM04_SE
                   WHERE NDATE = pNDATE
                     AND TRIM(Ccycd) LIKE
                         '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%');
        --Stt
        SELECT MAX(priority)
          INTO STT
          FROM VCB_BM04_SE
         WHERE NDATE = pNDATE;
        IF STT IS NULL THEN
          STT := 0;
        END IF;
        --Day du lieu ra bang phien
        INSERT INTO VCB_BM04_SE
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           SE,
           NDATE,
           F32,
           F71)
          SELECT A.msg_id,
                 A.query_id,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 (STT + ROWNUM) AS priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 A.F50,
                 A.F57,
                 A.F59,
                 A.F7072,
                 pSess,
                 to_number(to_char(A.trans_date, 'YYYYMMDD')) AS NDATE,
                 A.F32,
                 A.F71
            FROM (SELECT * FROM VCB_TEMP04 ORDER BY sending_time ASC) A
           WHERE A.msg_id NOT IN
                 (SELECT msg_id FROM VCB_BM04_SE WHERE NDATE = pNDATE);
        --commit;
        --Dem so ban ghi phien hien tai trong bang temp
        iCount := 0;
        SELECT COUNT(1)
          INTO iCount
          FROM VCB_TEMP04
         WHERE To_CHAR(TRANS_DATE, 'YYYYMMDD') = pDATE
           AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        --Neu iCount = 0 thi lay du lieu trong bang phien
        IF iCount = 0 THEN
          --Gan lai phien
          pSession := pSess - 1;
          --pPrintStatus:=2;
          pORDER := 1;
          insert into VCB_TEMP04
            (msg_id,
             query_id,
             msg_type,
             msg_direction,
             branch_a,
             branch_b,
             field20,
             field21,
             amount,
             ccycd,
             status,
             department,
             foreign_bank,
             foreign_bank_name,
             trans_no,
             rm_number,
             priority,
             value_date,
             trans_date,
             receiving_time,
             sending_time,
             F50,
             F57,
             F59,
             F7072,
             F32,
             F71)
            SELECT A.MSG_ID,
                   A.QUERY_ID,
                   A.msg_type,
                   A.msg_direction,
                   A.branch_a,
                   A.branch_b,
                   A.field20,
                   A.field21,
                   A.amount,
                   A.ccycd,
                   A.status,
                   A.department,
                   A.foreign_bank,
                   A.foreign_bank_name,
                   A.trans_no,
                   A.rm_number,
                   A.priority,
                   A.value_date,
                   A.trans_date,
                   A.receiving_time,
                   A.sending_time,
                   A.F50,
                   A.F57,
                   A.F59,
                   A.F7072,
                   A.F32,
                   A.F71
              FROM VCB_BM04_SE A
             WHERE NDATE = pNDATE
               AND SE = pSession
               AND TRIM(Ccycd) LIKE
                   '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        else
          pSession     := pSess;
          pPrintStatus := 2;
        END IF;
        --LAY DU LIEU BANG PHIEN DA CO CUA NGAY HIEN TAI
        --KHI PHIEN > 0
      ELSE
        IF pSession = 1000 THEN
          --Gan lai phien =0
          pSession := 0;
          --LAY DU LIEU TU BANG IBPS_MSG_CONTENT
          insert into VCB_TEMP04
            (msg_id,
             query_id,
             msg_type,
             msg_direction,
             branch_a,
             branch_b,
             field20,
             field21,
             amount,
             ccycd,
             status,
             department,
             foreign_bank,
             foreign_bank_name,
             trans_no,
             rm_number,
             priority,
             value_date,
             trans_date,
             receiving_time,
             sending_time,
             F50,
             F57,
             F59,
             F7072,
             F32,
             F71)
            SELECT A.MSG_ID,
                   A.QUERY_ID,
                   A.msg_type,
                   A.msg_direction,
                   A.branch_a,
                   A.branch_b,
                   A.field20,
                   A.field21,
                   A.amount,
                   A.ccycd,
                   A.status,
                   A.department,
                   A.foreign_bank,
                   A.foreign_bank_name,
                   A.trans_no,
                   A.rm_number,
                   A.priority,
                   A.value_date,
                   A.trans_date,
                   A.receiving_time,
                   A.sending_time,
                   decode(MSG_TYPE,
                          'MT202',
                          GET_FIELD(A.Content, '52A') ||
                          GET_FIELD(A.Content, '52D'),
                          GET_FIELD(A.Content, '50A') ||
                          GET_FIELD(A.Content, '50K')) as F50,
                   GET_FIELD(A.Content, '57A') ||
                   GET_FIELD(A.Content, '57B') ||
                   GET_FIELD(A.Content, '57C') ||
                   GET_FIELD(A.Content, '57D') as F57,
                   decode(MSG_TYPE,
                          'MT202',
                          GET_FIELD(A.Content, '58A') ||
                          GET_FIELD(A.Content, '58D'),
                          GET_FIELD(A.Content, '59') ||
                          GET_FIELD(A.Content, '59A')) as F59,
                   GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                   GET_FIELD(A.Content, '32') ||
                   GET_FIELD(A.Content, '32A') as F32,
                   GET_FIELD(A.Content, '71') ||
                   GET_FIELD(A.Content, '71A') ||
                   GET_FIELD(A.Content, '71F') ||
                   GET_FIELD(A.Content, '71G') as F71
              FROM VCB_MSG_CONTENT A
             WHERE A.TRANSDATE = pDATE
               AND TRIM(A.Ccycd) LIKE
                   '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
               AND A.MSG_DIRECTION = 'SIBS-VCB'
               AND A.STATUS = 1;
        ELSE
          pORDER := 1;
          insert into VCB_TEMP04
            (msg_id,
             query_id,
             msg_type,
             msg_direction,
             branch_a,
             branch_b,
             field20,
             field21,
             amount,
             ccycd,
             status,
             department,
             foreign_bank,
             foreign_bank_name,
             trans_no,
             rm_number,
             priority,
             value_date,
             trans_date,
             receiving_time,
             sending_time,
             F50,
             F57,
             F59,
             F7072,
             F32,
             F71)
            SELECT msg_id,
                   query_id,
                   msg_type,
                   msg_direction,
                   branch_a,
                   branch_b,
                   field20,
                   field21,
                   amount,
                   ccycd,
                   status,
                   department,
                   foreign_bank,
                   foreign_bank_name,
                   trans_no,
                   rm_number,
                   priority,
                   value_date,
                   trans_date,
                   receiving_time,
                   sending_time,
                   F50,
                   F57,
                   F59,
                   F7072,
                   F32,
                   F71
              FROM VCB_BM04_SE
             WHERE NDATE = pNDATE
               AND SE = pSession
               AND TRIM(Ccycd) LIKE
                   '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        END IF;
      END IF;
    end if;
    --Ngay bao cao khac ngay he thong
    if pTYPE = 2 then
      IF pSession = 0 THEN
        --Lay phien max cua ngay bao cao
        SELECT MAX(SE)
          INTO pSess
          FROM VCB_BM04_SE
         WHERE TO_CHAR(TRANS_DATE, 'YYYYMMDD') =
               TO_CHAR(pTRAN_DATE, 'YYYYMMDD')
           AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        --Neu chua co phien, gan = 0
        IF pSess is NULL THEN
          pSess := 0;
        END IF;
        pSession := pSess;
      END IF;
      IF pSession = 1000 THEN
        pSession := 0;
      END IF;
      --Kiem tra phien =0
      IF pSession = 0 THEN
        --Du lieu trong ngay
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 GET_FIELD(A.Content, '71F') || GET_FIELD(A.Content, '71G') as F71
            FROM VCB_MSG_CONTENT A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'SIBS-VCB'
             AND A.STATUS = 1;
        --Du lieu trong thang
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 GET_FIELD(A.Content, '71F') || GET_FIELD(A.Content, '71G') as F71
            FROM VCB_MSG_ALL A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'SIBS-VCB'
             AND A.STATUS = 1;
      ELSE
        --THEO PHIEN
        pORDER := 1;
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT msg_id,
                 query_id,
                 msg_type,
                 msg_direction,
                 branch_a,
                 branch_b,
                 field20,
                 field21,
                 amount,
                 ccycd,
                 status,
                 department,
                 foreign_bank,
                 foreign_bank_name,
                 trans_no,
                 rm_number,
                 priority,
                 value_date,
                 trans_date,
                 receiving_time,
                 sending_time,
                 F50,
                 F57,
                 F59,
                 F7072,
                 F32,
                 F71
            FROM VCB_BM04_SE
           WHERE NDATE = pNDATE
             AND SE = pSession
             AND TRIM(Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
      END IF;
    end if;
    --Ngay bao cao khac thang he thong
    if pTYPE = 3 then
      IF pSession = 0 THEN
        --Lay phien max cua ngay bao cao
        SELECT MAX(SE)
          INTO pSess
          FROM VCB_BM04_SE
         WHERE TO_CHAR(TRANS_DATE, 'YYYYMMDD') =
               TO_CHAR(pTRAN_DATE, 'YYYYMMDD')
           AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        --Neu chua co phien, gan = 0
        IF pSess is NULL THEN
          pSess := 0;
        END IF;
        pSession := pSess;
      END IF;
      IF pSession = 1000 THEN
        pSession := 0;
      END IF;
      IF pSession = 0 THEN
        --Du lieu trong ngay
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 GET_FIELD(A.Content, '71F') || GET_FIELD(A.Content, '71G') as F71
            FROM VCB_MSG_CONTENT A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'SIBS-VCB'
             AND A.STATUS = 1;
        --Du lieu trong thang (bang IBPS_MSG_ALL)
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 GET_FIELD(A.Content, '71F') || GET_FIELD(A.Content, '71G') as F71
            FROM VCB_MSG_ALL A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'SIBS-VCB'
             AND A.STATUS = 1;
        --DU LIEU BANG IBPS_MSG_ALL_HIS
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 GET_FIELD(A.Content, '71F') || GET_FIELD(A.Content, '71G') as F71
            FROM VCB_MSG_ALL_HIS A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'SIBS-VCB'
             AND A.STATUS = 1;
      ELSE
        --THEO PHIEN
        pORDER := 1;
        insert into VCB_TEMP04
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT msg_id,
                 query_id,
                 msg_type,
                 msg_direction,
                 branch_a,
                 branch_b,
                 field20,
                 field21,
                 amount,
                 ccycd,
                 status,
                 department,
                 foreign_bank,
                 foreign_bank_name,
                 trans_no,
                 rm_number,
                 priority,
                 value_date,
                 trans_date,
                 receiving_time,
                 sending_time,
                 F50,
                 F57,
                 F59,
                 F7072,
                 F32,
                 F71
            FROM VCB_BM04_SE
           WHERE NDATE = pNDATE
             AND SE = pSession
             AND TRIM(Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
      END IF;
    end if;
    -- Du lieu lay ra
    IF pORDER = 0 THEN
      open RefCurVCB04 for
        SELECT msg_id,
               query_id,
               msg_type,
               msg_direction,
               branch_a,
               branch_b,
               field20,
               field21,
               amount,
               ccycd,
               status,
               department,
               foreign_bank,
               foreign_bank_name,
               trans_no,
               ltrim(rm_number, '0') as rm_number,
               ROWNUM as priority,
               value_date,
               trans_date,
               receiving_time,
               sending_time,
               F50,
               F57,
               F59,
               F7072,
               to_char(amount) || ' ' || ccycd as F32,
               rtrim(F71, '-') as F71
          FROM (SELECT * FROM VCB_TEMP04 ORDER BY sending_time)
         ORDER BY priority, sending_time, rm_number;
    ELSE
      open RefCurVCB04 for
        SELECT msg_id,
               query_id,
               msg_type,
               msg_direction,
               branch_a,
               branch_b,
               field20,
               field21,
               amount,
               ccycd,
               status,
               department,
               foreign_bank,
               foreign_bank_name,
               trans_no,
               ltrim(rm_number, '0') as rm_number,
               priority,
               value_date,
               trans_date,
               receiving_time,
               sending_time,
               F50,
               F57,
               F59,
               F7072,
               to_char(amount) || ' ' || ccycd as F32,
               rtrim(F71, '-') as F71
          FROM VCB_TEMP04
         ORDER BY priority, sending_time, rm_number;
    END IF;
    --rollback;
  END;

  FUNCTION GET_FIELD(pCONTENT VARCHAR2, pFieldName VARCHAR2) RETURN VARCHAR2 IS
    iposStart       NUMBER(4);
    vstrTemp        VARCHAR2(400);
    v_returnContent VARCHAR2(400);
  BEGIN
    iposStart := dbms_lob.instr(pCONTENT,
                                ':' || UPPER(LTRIM(Trim(pFieldName), 'F')) || ':');
    if iposStart > 0 then
      vstrTemp := dbms_lob.substr(pCONTENT, 400, iposStart + 4);
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
      if vstrTemp is not null then
        for i in 1 .. Length(vstrTemp) loop
          if substr(vstrTemp, i, 1) = ':' And
             (substr(vstrTemp, i + 3, 1) = ':' or
              substr(vstrTemp, i + 4, 1) = ':') AND
             (substr(vstrTemp, i - 1, 1) = chr(10) or
              substr(vstrTemp, i - 1, 1) = chr(13)) or
             substr(vstrTemp, i, 1) = '}' then
            v_returnContent := substr(vstrTemp, 1, i - 1);
            exit;
          end if;
        end loop;
      end if;
      if v_returnContent is null then
        v_returnContent := vstrTemp;
      end if;
    end if;
    RETURN v_returnContent;
  END;

  --Bang ke dien di theo phien
  PROCEDURE VCB_BM06(pTRAN_DATE   IN DATE,
                     pSession     IN OUT NUMBER,
                     pCCY         IN VARCHAR2,
                     pPrintStatus IN OUT NUMBER,
                     pISCHARGE    IN VARCHAR2,
                     RefCurVCB04  OUT PKG_CR.RefCurType) IS
    pTYPE  NUMBER;
    pSess  NUMBER;
    iCount NUMBER;
    pDATE  VARCHAR2(8);
    pNDATE NUMBER(10);
    STT    NUMBER;
    pORDER NUMBER;
  BEGIN
    --Convert pTRAN_DATE to Varchar2
    pDATE := to_char(pTRAN_DATE, 'YYYYMMDD');
    --Convert pDATE to NUMBER
    pNDATE := TO_NUMBER(pDATE);
    --Gan pSession =0 neu pSession co gia tri null
    IF pSession is NULL THEN
      pSession := 0;
    END IF;
    --Gan pPrintStatus
    if pPrintStatus is not null and pPrintStatus = 2 then
      pPrintStatus := 2;
    else
      pPrintStatus := 1;
    end if;
    STT    := 0;
    pORDER := 0;
    --Lay pType sau khi so sanh ngay giao dich voi ngay he thong
    pTYPE := GETTYPE(pTRAN_DATE);
    --Xoa bang tam
    --DELETE FROM VCB_TEMP06;
    --Ngay hien tai
    if pTYPE = 1 then
      --Lay phien max cua ngay bao cao
      SELECT MAX(SE)
        INTO pSess
        FROM VCB_BM06_SE
       WHERE NDATE = pNDATE
         AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
      --Neu chua co phien, gan = 0
      IF pSess is NULL THEN
        pSess := 0;
      END IF;
      --Tu dong tang phien len 1 khi NSD khong nhap phien hay nhap phien
      --bang 0 va ngay bao cao bang ngay he thong(ngay DB)
      pSess := pSess + 1;
      --Xet phien dua vao =0
      IF pSession = 0 THEN
        --LAY DU LIEU TU BANG IBPS_MSG_CONTENT
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                 SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
            FROM VCB_MSG_CONTENT A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'VCB-SIBS'
             --QUYND ?
             --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                 --decode(pISCHARGE, 'A', '1', pISCHARGE)
             -------
             AND A.STATUS = 1
             AND A.MSG_ID NOT IN
                 (SELECT DISTINCT MSG_ID
                    FROM VCB_BM06_SE
                   WHERE NDATE = pNDATE
                     AND TRIM(Ccycd) LIKE
                         '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%');
        --Stt
        SELECT MAX(priority)
          INTO STT
          FROM VCB_BM06_SE
         WHERE NDATE = pNDATE;
        IF STT IS NULL THEN
          STT := 0;
        END IF;
        --Day du lieu ra bang phien
        INSERT INTO VCB_BM06_SE
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           SE,
           NDATE,
           F32,
           F71)
          SELECT A.msg_id,
                 A.query_id,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 (STT + ROWNUM) AS priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 A.F50,
                 A.F57,
                 A.F59,
                 A.F7072,
                 pSess,
                 to_number(to_char(A.trans_date, 'YYYYMMDD')) AS NDATE,
                 A.F32,
                 A.F71
            FROM (SELECT * FROM VCB_TEMP06 ORDER BY sending_time ASC) A
           WHERE A.msg_id NOT IN
                 (SELECT msg_id FROM VCB_BM06_SE WHERE NDATE = pNDATE);
        --commit;
        --Dem so ban ghi phien hien tai trong bang temp
        iCount := 0;
        SELECT COUNT(1)
          INTO iCount
          FROM VCB_TEMP06
         WHERE To_CHAR(TRANS_DATE, 'YYYYMMDD') = pDATE
           AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        --Neu iCount = 0 thi lay du lieu trong bang phien
        IF iCount = 0 THEN
          --Gan lai phien
          pSession := pSess - 1;
          --pPrintStatus:=2;
          pORDER := 1;
          insert into VCB_TEMP06
            (msg_id,
             query_id,
             msg_type,
             msg_direction,
             branch_a,
             branch_b,
             field20,
             field21,
             amount,
             ccycd,
             status,
             department,
             foreign_bank,
             foreign_bank_name,
             trans_no,
             rm_number,
             priority,
             value_date,
             trans_date,
             receiving_time,
             sending_time,
             F50,
             F57,
             F59,
             F7072,
             F32,
             F71)
            SELECT A.MSG_ID,
                   A.QUERY_ID,
                   A.msg_type,
                   A.msg_direction,
                   A.branch_a,
                   A.branch_b,
                   A.field20,
                   A.field21,
                   A.amount,
                   A.ccycd,
                   A.status,
                   A.department,
                   A.foreign_bank,
                   A.foreign_bank_name,
                   A.trans_no,
                   A.rm_number,
                   A.priority,
                   A.value_date,
                   A.trans_date,
                   A.receiving_time,
                   A.sending_time,
                   A.F50,
                   A.F57,
                   A.F59,
                   A.F7072,
                   A.F32,
                   A.F71
              FROM VCB_BM06_SE A
             WHERE NDATE = pNDATE
               AND SE = pSession
               AND TRIM(Ccycd) LIKE
                   '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        else
          pSession     := pSess;
          pPrintStatus := 2;
        END IF;
        --LAY DU LIEU BANG PHIEN DA CO CUA NGAY HIEN TAI
        --KHI PHIEN > 0
      ELSE
        IF pSession = 1000 THEN
          --Gan lai phien =0
          pSession := 0;
          --LAY DU LIEU TU BANG IBPS_MSG_CONTENT
          insert into VCB_TEMP06
            (msg_id,
             query_id,
             msg_type,
             msg_direction,
             branch_a,
             branch_b,
             field20,
             field21,
             amount,
             ccycd,
             status,
             department,
             foreign_bank,
             foreign_bank_name,
             trans_no,
             rm_number,
             priority,
             value_date,
             trans_date,
             receiving_time,
             sending_time,
             F50,
             F57,
             F59,
             F7072,
             F32,
             F71)
            SELECT A.MSG_ID,
                   A.QUERY_ID,
                   A.msg_type,
                   A.msg_direction,
                   A.branch_a,
                   A.branch_b,
                   A.field20,
                   A.field21,
                   A.amount,
                   A.ccycd,
                   A.status,
                   A.department,
                   A.foreign_bank,
                   A.foreign_bank_name,
                   A.trans_no,
                   A.rm_number,
                   A.priority,
                   A.value_date,
                   A.trans_date,
                   A.receiving_time,
                   A.sending_time,
                   decode(MSG_TYPE,
                          'MT202',
                          GET_FIELD(A.Content, '52A') ||
                          GET_FIELD(A.Content, '52D'),
                          GET_FIELD(A.Content, '50A') ||
                          GET_FIELD(A.Content, '50K')) as F50,
                   GET_FIELD(A.Content, '57A') ||
                   GET_FIELD(A.Content, '57B') ||
                   GET_FIELD(A.Content, '57C') ||
                   GET_FIELD(A.Content, '57D') as F57,
                   decode(MSG_TYPE,
                          'MT202',
                          GET_FIELD(A.Content, '58A') ||
                          GET_FIELD(A.Content, '58D'),
                          GET_FIELD(A.Content, '59') ||
                          GET_FIELD(A.Content, '59A')) as F59,
                   GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                   GET_FIELD(A.Content, '32') ||
                   GET_FIELD(A.Content, '32A') as F32,
                   GET_FIELD(A.Content, '71') ||
                   GET_FIELD(A.Content, '71A') ||
                   SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                   SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
              FROM VCB_MSG_CONTENT A
             WHERE A.TRANSDATE = pDATE
               AND TRIM(A.Ccycd) LIKE
                   '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
               AND A.MSG_DIRECTION = 'SIBS-VCB'
               --QUYND ?
               --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                   --decode(pISCHARGE, 'A', '1', pISCHARGE)
               --------
               AND A.STATUS = 1;
        ELSE
          pORDER := 1;
          insert into VCB_TEMP06
            (msg_id,
             query_id,
             msg_type,
             msg_direction,
             branch_a,
             branch_b,
             field20,
             field21,
             amount,
             ccycd,
             status,
             department,
             foreign_bank,
             foreign_bank_name,
             trans_no,
             rm_number,
             priority,
             value_date,
             trans_date,
             receiving_time,
             sending_time,
             F50,
             F57,
             F59,
             F7072,
             F32,
             F71)
            SELECT msg_id,
                   query_id,
                   msg_type,
                   msg_direction,
                   branch_a,
                   branch_b,
                   field20,
                   field21,
                   amount,
                   ccycd,
                   status,
                   department,
                   foreign_bank,
                   foreign_bank_name,
                   trans_no,
                   rm_number,
                   priority,
                   value_date,
                   trans_date,
                   receiving_time,
                   sending_time,
                   F50,
                   F57,
                   F59,
                   F7072,
                   F32,
                   F71
              FROM VCB_BM06_SE
             WHERE NDATE = pNDATE
               AND SE = pSession
               AND TRIM(Ccycd) LIKE
                   '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        END IF;
      END IF;
    end if;
    --Ngay bao cao khac ngay he thong
    if pTYPE = 2 then
      IF pSession = 0 THEN
        --Lay phien max cua ngay bao cao
        SELECT MAX(SE)
          INTO pSess
          FROM VCB_BM06_SE
         WHERE TO_CHAR(TRANS_DATE, 'YYYYMMDD') =
               TO_CHAR(pTRAN_DATE, 'YYYYMMDD')
           AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        --Neu chua co phien, gan = 0
        IF pSess is NULL THEN
          pSess := 0;
        END IF;
        pSession := pSess;
      END IF;
      IF pSession = 1000 THEN
        pSession := 0;
      END IF;
      --Kiem tra phien =0
      IF pSession = 0 THEN
        --Du lieu trong ngay
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                 SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
            FROM VCB_MSG_CONTENT A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'VCB-SIBS'
             --QUYND ?
             --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                 --decode(pISCHARGE, 'A', '1', pISCHARGE)
             ----
             AND A.STATUS = 1;
        --Du lieu trong thang
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                 SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
            FROM VCB_MSG_ALL A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'VCB-SIBS'
             --QUYND ?
             --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                 --decode(pISCHARGE, 'A', '1', pISCHARGE)
                 -----
             AND A.STATUS = 1;
      ELSE
        --THEO PHIEN
        pORDER := 1;
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT msg_id,
                 query_id,
                 msg_type,
                 msg_direction,
                 branch_a,
                 branch_b,
                 field20,
                 field21,
                 amount,
                 ccycd,
                 status,
                 department,
                 foreign_bank,
                 foreign_bank_name,
                 trans_no,
                 rm_number,
                 priority,
                 value_date,
                 trans_date,
                 receiving_time,
                 sending_time,
                 F50,
                 F57,
                 F59,
                 F7072,
                 F32,
                 F71
            FROM VCB_BM06_SE
           WHERE NDATE = pNDATE
             AND SE = pSession
             AND TRIM(Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
      END IF;
    end if;
    --Ngay bao cao khac thang he thong
    if pTYPE = 3 then
      IF pSession = 0 THEN
        --Lay phien max cua ngay bao cao
        SELECT MAX(SE)
          INTO pSess
          FROM VCB_BM06_SE
         WHERE TO_CHAR(TRANS_DATE, 'YYYYMMDD') =
               TO_CHAR(pTRAN_DATE, 'YYYYMMDD')
           AND TRIM(Ccycd) LIKE '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
        --Neu chua co phien, gan = 0
        IF pSess is NULL THEN
          pSess := 0;
        END IF;
        pSession := pSess;
      END IF;
      IF pSession = 1000 THEN
        pSession := 0;
      END IF;
      IF pSession = 0 THEN
        --Du lieu trong ngay
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                 SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
            FROM VCB_MSG_CONTENT A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'VCB-SIBS'
             --QUYND ?
             --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                 --decode(pISCHARGE, 'A', '1', pISCHARGE)
                 ------
             AND A.STATUS = 1;
        --Du lieu trong thang (bang IBPS_MSG_ALL)
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                 SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
            FROM VCB_MSG_ALL A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'VCB-SIBS'
             --QUYND ?
             --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                 --decode(pISCHARGE, 'A', '1', pISCHARGE)
                 ----
             AND A.STATUS = 1;
        --DU LIEU BANG IBPS_MSG_ALL_HIS
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT A.MSG_ID,
                 A.QUERY_ID,
                 A.msg_type,
                 A.msg_direction,
                 A.branch_a,
                 A.branch_b,
                 A.field20,
                 A.field21,
                 A.amount,
                 A.ccycd,
                 A.status,
                 A.department,
                 A.foreign_bank,
                 A.foreign_bank_name,
                 A.trans_no,
                 A.rm_number,
                 A.priority,
                 A.value_date,
                 A.trans_date,
                 A.receiving_time,
                 A.sending_time,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '52A') ||
                        GET_FIELD(A.Content, '52D'),
                        GET_FIELD(A.Content, '50A') ||
                        GET_FIELD(A.Content, '50K')) as F50,
                 GET_FIELD(A.Content, '57A') || GET_FIELD(A.Content, '57B') ||
                 GET_FIELD(A.Content, '57C') || GET_FIELD(A.Content, '57D') as F57,
                 decode(MSG_TYPE,
                        'MT202',
                        GET_FIELD(A.Content, '58A') ||
                        GET_FIELD(A.Content, '58D'),
                        GET_FIELD(A.Content, '59') ||
                        GET_FIELD(A.Content, '59A')) as F59,
                 GET_FIELD(A.Content, '70') || GET_FIELD(A.Content, '72') as F7072,
                 GET_FIELD(A.Content, '32') || GET_FIELD(A.Content, '32A') as F32,
                 GET_FIELD(A.Content, '71') || GET_FIELD(A.Content, '71A') ||
                 SUBSTR(GET_FIELD(A.Content, '71F'), 1, 3) ||
                 SUBSTR(GET_FIELD(A.Content, '71G'), 1, 3) as F71
            FROM VCB_MSG_ALL_HIS A
           WHERE A.TRANSDATE = pDATE
             AND TRIM(A.Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%'
             AND A.MSG_DIRECTION = 'VCB-SIBS'
             --QUYND ?
             --AND decode(pISCHARGE, 'A', '1', A.ISCHARGE) =
                 --decode(pISCHARGE, 'A', '1', pISCHARGE)
                 -----
             AND A.STATUS = 1;
      ELSE
        --THEO PHIEN
        pORDER := 1;
        insert into VCB_TEMP06
          (msg_id,
           query_id,
           msg_type,
           msg_direction,
           branch_a,
           branch_b,
           field20,
           field21,
           amount,
           ccycd,
           status,
           department,
           foreign_bank,
           foreign_bank_name,
           trans_no,
           rm_number,
           priority,
           value_date,
           trans_date,
           receiving_time,
           sending_time,
           F50,
           F57,
           F59,
           F7072,
           F32,
           F71)
          SELECT msg_id,
                 query_id,
                 msg_type,
                 msg_direction,
                 branch_a,
                 branch_b,
                 field20,
                 field21,
                 amount,
                 ccycd,
                 status,
                 department,
                 foreign_bank,
                 foreign_bank_name,
                 trans_no,
                 rm_number,
                 priority,
                 value_date,
                 trans_date,
                 receiving_time,
                 sending_time,
                 F50,
                 F57,
                 F59,
                 F7072,
                 F32,
                 F71
            FROM VCB_BM06_SE
           WHERE NDATE = pNDATE
             AND SE = pSession
             AND TRIM(Ccycd) LIKE
                 '%' || DECODE(pCCY, 'ALL', '', pCCY) || '%';
      END IF;
    end if;
    -- Du lieu lay ra
    IF pORDER = 0 THEN
      open RefCurVCB04 for
        SELECT msg_id,
               query_id,
               msg_type,
               msg_direction,
               branch_a,
               branch_b,
               field20,
               field21,
               amount,
               ccycd,
               status,
               department,
               foreign_bank,
               foreign_bank_name,
               trans_no,
               ltrim(rm_number, '0') as rm_number,
               ROWNUM as priority,
               value_date,
               trans_date,
               receiving_time,
               sending_time,
               F50,
               F57,
               F59,
               F7072,
               to_char(amount) || ' ' || ccycd as F32,
               rtrim(F71, '-') as F71
          FROM (SELECT * FROM VCB_TEMP06 ORDER BY sending_time)
         ORDER BY priority, sending_time, rm_number;
    ELSE
      open RefCurVCB04 for
        SELECT msg_id,
               query_id,
               msg_type,
               msg_direction,
               branch_a,
               branch_b,
               field20,
               field21,
               amount,
               ccycd,
               status,
               department,
               foreign_bank,
               foreign_bank_name,
               trans_no,
               ltrim(rm_number, '0') as rm_number,
               priority,
               value_date,
               trans_date,
               receiving_time,
               sending_time,
               F50,
               F57,
               F59,
               F7072,
               to_char(amount) || ' ' || ccycd as F32,
               rtrim(F71, '-') as F71
          FROM VCB_TEMP06
         ORDER BY priority, sending_time, rm_number;
    END IF;
    --rollback;
  END;
  ------------------------------
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
  
  PROCEDURE VCB_PRINT_MSG(pMSG_ID   in    NUMBER,
                          pMSG_TYPE  in   varchar2,
                          pMSGDirection in varchar2,
                          --RefCurVCB_03 IN OUT PKG_CR.RefCurType
                          pCurContent in   out PKG_CR.RefCurType) is
  
    vContent varchar2(4000) := '';
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
        if icount <> 0 then
          vTable := 'VCB_Msg_All_His';
          select content,
                 Query_ID,
                 substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                 C.BRANCH_A AS SENDER,
                 C.BRANCH_B AS RECEIVER,
                 GET_BRANCH_NAME(C.Branch_a) AS sendername,
                 GET_BRANCH_NAME(C.Branch_b) AS receivername,
                 C.Receiving_Time,
                 C.SENDING_TIME,
                 S.NAME AS STATUS,
                 PRINT_STS
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
                 print_status
            from VCB_MSG_ALL_HIS C
            JOIN STATUS S ON C.STATUS = S.STATUS
           where msg_id = pMSG_ID;
        end if;
      else
        vTable := 'VCB_MSG_All';
        select content,
               Query_ID,
               substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
               C.BRANCH_A AS SENDER,
               C.BRANCH_B AS RECEIVER,
               GET_BRANCH_NAME(C.Branch_a) AS sendername,
               GET_BRANCH_NAME(C.Branch_b) AS receivername,
               C.Receiving_Time,
               C.SENDING_TIME,
               S.NAME AS STATUS,
               PRINT_STS
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
               print_status
          from VCB_MSG_ALL C
          JOIN STATUS S ON C.STATUS = S.STATUS
         where msg_id = pMSG_ID;
      end if;
    else
      vTable := 'VCB_MSG_CONTENT';
      select content,
             Query_ID,
             substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
             C.BRANCH_A AS SENDER,
             C.BRANCH_B AS RECEIVER,
             GET_BRANCH_NAME(C.Branch_a) AS sendername,
             GET_BRANCH_NAME(C.Branch_b) AS receivername,
             C.Receiving_Time,
             C.SENDING_TIME,
             S.NAME AS STATUS,
             PRINT_STS
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
             print_status
        from VCB_MSG_CONTENT C
        JOIN STATUS S ON C.STATUS = S.STATUS
       where msg_id = pMSG_ID;
    
    end if;
  
    --if not vContent is NULL then
      OPen pCurContent for      
        select to_date(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                            '32A',
                                                            1,
                                                            1,
                                                            pMSG_ID),
                       'YYMMDD') AS NGAY_32A,
               rm_number AS RM_NUMBER,
               pMSG_ID AS SENDER,
               receiver AS RECEIVER,
               sendername AS SENDERNAME,
               receivername AS RECEIVERNAME,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '20',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS SOLENH,
               receiving_time AS receiving_time ,
               sending_time AS sending_time,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '50K',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS NGUOIRALENH,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '59',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS NGUOIHUONG,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '59',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS TK_HUONG,
               --Minhhb comment out                                     
               /*GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '59',
                                                    3,
                                                    1,
                                                    pMSG_ID) AS DIACHI,*/
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '57E',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS DIACHI,                                                    
               --                                                                          
             replace(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '32A',
                                                    3,
                                                    1,
                                                    pMSG_ID),',','.') AS AMOUNT,
               status AS Status,
               print_status AS print_status,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '70',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS CONTENT,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '52D',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS TK_GHINO,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '52D',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS TEN_TK,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '32A',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS CCY,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '33B',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS ORG_AMOUNT,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '71A',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS LOAIPHI,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '71F',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS TIENPHI,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '57D',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS NH_HUONG,                                                                                                                 
                '1' AS GRPID                                                    
         from dual
         union
          select to_date(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                            '32A',
                                                            1,
                                                            1,
                                                            pMSG_ID),
                       'YYMMDD') AS NGAY_32A,
               rm_number AS RM_NUMBER,
               pMSG_ID AS SENDER,
               receiver AS RECEIVER,
               sendername AS SENDERNAME,
               receivername AS RECEIVERNAME,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '20',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS SOLENH,
               receiving_time AS receiving_time ,
               sending_time AS sending_time,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '50K',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS NGUOIRALENH,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '59',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS NGUOIHUONG,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '59',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS TK_HUONG,
               /*GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '59',
                                                    3,
                                                    1,
                                                    pMSG_ID) AS DIACHI,*/                                                    
                GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '57E',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS DIACHI, 
                                                                                     
               replace(GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '32A',
                                                    3,
                                                    1,
                                                    pMSG_ID),',','.') AS AMOUNT,
               status AS Status,
               print_status AS print_status,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '70',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS CONTENT,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '52D',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS TK_GHINO,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '52D',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS TEN_TK,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '32A',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS CCY,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '33B',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS ORG_AMOUNT,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '71A',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS LOAIPHI,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '71F',
                                                    2,
                                                    1,
                                                    pMSG_ID) AS TIENPHI,
               GW_PK_VCB_report.VCB_GET_SWIFT_Field(vContent,
                                                    '57D',
                                                    1,
                                                    1,
                                                    pMSG_ID) AS NH_HUONG   ,                                                                                                              
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

  PROCEDURE VCB_03(pdate        IN date,
                   pCCYCD       in varchar2 default 'VND',
                   pBranchA     in varchar2,
                   pBranchB     in varchar2,
                   RefCurVCB_03 IN OUT PKG_CR.RefCurType) IS
    vCCYCD   varchar2(3);
    vBranchA varchar2(12);
    vBranchB varchar2(12);
  BEGIN
    if upper(trim(pCCYCD)) = 'ALL' or pCCYCD is null 
      or upper(trim(pCCYCD)) = '' then
      vCCYCD := '%';
    else
      vCCYCD := trim(pCCYCD);
    end if;
    if upper(trim(pBranchA)) = 'ALL' or pBranchA is null
     or upper(trim(pBranchA)) = '' then
      vBranchA := '%';
    else
      vBranchA := trim(pBranchA);
    end if;
    if upper(trim(pBranchB)) = 'ALL' or pBranchA is null 
      or upper(trim(pBranchB)) = '' then
      vBranchB := '%';
    else
      vBranchB := trim(pBranchB);
    end if;

    open RefCurVCB_03 for
      SELECT A.*
        FROM (select a.*,b.content
                from (SELECT A.msg_id,                             
                            A.RM_NUMBER,
                            CASE A.MSG_DIRECTION WHEN 'I' THEN CASE trim(C.CONTENT) WHEN null THEN
                                                                                            CASE trim(CA.CONTENT) WHEN null THEN GW_PK_VCB_REPORT.VCB_GET_Field(CH.CONTENT,'72',CH.Msg_Type)
                                                                                                            ELSE GW_PK_VCB_REPORT.VCB_GET_Field(CA.CONTENT,'72',CA.Msg_Type)     
                                                                                            END
                                                                               ELSE GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT,'72',C.MSG_TYPE)                    
                                                                END
                              ELSE -- DIEN DI
                              A.ref_number
                              END AS FIELD20 , 
                            --A.ref_number field20,
                             A.sender,
                             A.receiver,
                             A.amount,
                             A.gw_type sibs_ttsp,
                             A.trans_date,
                             A.ccy,
                             A.exception_type,
                             A.msg_direction,
                             a.status                            
                        FROM VCB_MSG_REC A
                        LEFT JOIN VCB_MSG_CONTENT C ON C.QUERY_ID=A.Query_Id                                                                                                                                   
                        LEFT JOIN VCB_MSG_ALL CA ON CA.QUERY_ID=A.Query_Id
                        LEFT JOIN VCB_MSG_ALL_HIS CH ON CH.QUERY_ID=A.Query_Id
                       WHERE trim(a.ccy) like vCCYCD
                         AND to_char(A.trans_date, 'DD/MM/YYYY') =
                             TO_char(pdate, 'DD/MM/YYYY')
                         AND A.sender like vBranchA
                         AND A.receiver like vBranchB
                         and A.exception_type in ('GV', 'GS')
                         ) A
                LEFT JOIN (SELECT cast(NAME as nvarchar2(100)) as content, STATUS FROM STATUS) B 
                ON trim(A.STATUS) = trim(B.STATUS)
                
              union
              select a.*, b.content
                from (SELECT A.msg_id,
                             A.RM_NUMBER,
                             CASE A.MSG_DIRECTION WHEN 'I' THEN CASE trim(C.CONTENT) WHEN null THEN
                                                                                            CASE trim(CA.CONTENT) WHEN null THEN GW_PK_VCB_REPORT.VCB_GET_Field(CH.CONTENT,'72',CH.Msg_Type)
                                                                                                            ELSE GW_PK_VCB_REPORT.VCB_GET_Field(CA.CONTENT,'72',CA.Msg_Type)     
                                                                                            END
                                                                               ELSE GW_PK_VCB_REPORT.VCB_GET_Field(C.CONTENT,'72',C.MSG_TYPE)                    
                                                                END
                              ELSE -- DIEN DI
                              A.ref_number
                              END AS FIELD20 ,
                             --A.ref_number field20,
                             A.sender,
                             A.receiver,
                             A.amount,
                             A.gw_type sibs_ttsp,
                             A.trans_date,
                             A.ccy,
                             A.exception_type,
                             A.msg_direction,
                             a.status
                        FROM VCB_MSG_REC A
                        LEFT JOIN VCB_MSG_CONTENT C ON C.QUERY_ID=A.Query_Id                                                                                                                                   
                          LEFT JOIN VCB_MSG_ALL CA ON CA.QUERY_ID=A.Query_Id
                          LEFT JOIN VCB_MSG_ALL_HIS CH ON CH.QUERY_ID=A.Query_Id
                       WHERE trim(a.ccy) like vCCYCD
                         AND A.NDATE = TO_char(pdate, 'YYYYMMDD')
                         AND A.sender like vBranchA
                         AND A.receiver like vBranchB
                         and a.exception_type not in ('GV', 'GS')) A
                left join (select CONTENT, cdval
                             from allcode
                            where gwtype = 'SYSTEM'
                              and cdname = 'MSGSTS') B on a.status = b.cdval) A
       ORDER BY A.msg_direction desc;
  END VCB_03;  
  
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
