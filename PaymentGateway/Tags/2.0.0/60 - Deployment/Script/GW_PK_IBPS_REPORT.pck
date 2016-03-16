CREATE OR REPLACE PACKAGE GW_PK_IBPS_REPORT IS

  /*PROCEDURE bm_ibps01(RefCurBM_IBPS01 IN OUT PKG_CR.RefCurType);*/
  PROCEDURE IBPS_PRINT_MSG(pBranch         in varchar2,
                           pMSG_ID         in number,
                           pUser           in varchar2,
                           pcurBM_IBPS_MSG IN OUT PKG_CR.RefCurType);
  PROCEDURE IBPS_PRINT_MSG_LIST(pBranch         in varchar2,
                                pMSG_ID         in number,
                                pUser           in varchar2,
                                pcurBM_IBPS_MSG IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS02(pDate           in date,
                      pCitad          in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS02 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS02_CN(pDate              in date,
                         pCitad             in varchar2,
                         pCcycd             in varchar2 default 'VND',
                         pStatus            in varchar2,
                         RefCurBM_IBPS02_CN IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS03_1(pDate             in date,
                        pCitad            in varchar2,
                        pCcycd            in varchar2 default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS03_1 IN OUT PKG_CR.RefCurType);

  PROCEDURE BM_IBPS03_2(pDate             IN DATE,
                        pCITAD            IN VARCHAR2,
                        pCCYCD            IN VARCHAR2 Default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS03_2 IN OUT PKG_CR.RefCurType);

  PROCEDURE BM_IBPS02_A(pDate             in date,
                        pCitad            in varchar2,
                        pCcycd            in varchar2 default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS02_A IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS03(pdate           IN date,
                      pCcycd          in varchar2 default 'VND',
                      pCitad          in varchar2,
                      RefCurBM_IBPS03 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS04(pDate           in date,
                      pCitadOld       in varchar2,
                      pCitadNew       in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pSTATUS         IN VARCHAR2,
                      RefCurBM_IBPS04 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS05(pDATE           IN DATE,
                      pCitad          IN VARCHAR2,
                      pCcycd          IN VARCHAR2,
                      RefCurBM_IBPS05 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS06(pDate           in date,
                      pCitad          in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS06 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS07(pDATE           IN DATE,
                      pCitad          IN VARCHAR2,
                      pBranch         IN VARCHAR2,
                      pCcycd          IN VARCHAR2,
                      RefCurBM_IBPS07 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS08(pfrdate         in date,
                      ptodate         in date,
                      pCcycd          in varchar2 default 'VND',
                      RefCurBM_IBPS08 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS09(pDate           IN DATE,
                      pCITAD          IN VARCHAR2,
                      pCCYCD          IN VARCHAR2 Default 'VND',
                      RefCurBM_IBPS09 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS09_CN(pDate              IN DATE,
                         pCITAD             IN VARCHAR2,
                         pCCYCD             IN VARCHAR2 Default 'VND',
                         RefCurBM_IBPS09_CN IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS10(pdate           IN date,
                      pCcycd          in varchar2 default 'VND',
                      pCitad          in varchar2,
                      RefCurBM_IBPS10 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS11(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pTrans_code     in varchar2,
                      RefCurBM_IBPS11 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS13(pdate           IN date,
                      pCcycd          in varchar2 default 'VND',
                      pCitad          in varchar2,
                      RefCurBM_IBPS13 IN OUT PKG_CR.RefCurType);

  PROCEDURE BM_IBPS17(pDate           in date,
                      pCitadOld       in varchar2,
                      pCitadNew       in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      RefCurBM_IBPS17 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS18(pDate           in date,
                      pCitad          in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS18 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS18_2(pDate           in date,
                      pCitad          in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS18_2 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS19(pDate           in date,
                      pBranchCreate   in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS19 IN OUT PKG_CR.RefCurType);
                      
PROCEDURE BM_IBPS19_2(  pFromDate         in date,
                        pToDate           in date,
                        pBranchCreate     in varchar2,
                        pCcycd            in varchar2 default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS19_2 IN OUT PKG_CR.RefCurType) ;
  PROCEDURE BM_IBPS20(pDate           in date,
                      pBranchCreate   in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS20 IN OUT PKG_CR.RefCurType);
  PROCEDURE BM_IBPS21(pDate           in date,
                      pBranchCreate   in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS21 IN OUT PKG_CR.RefCurType);
                      
  PROCEDURE BM_IBPS22(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pFromRelationNum in varchar2,
                      pToRelationNum in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS22 IN OUT PKG_CR.RefCurType);
  PROCEDURE IBPS01(pfrdate      in date,
                   ptodate      in date,
                   RefCurIBPS01 IN OUT PKG_CR.RefCurType);
  PROCEDURE ibps_print_all(RefCurIBPS_PRINT_ALL IN OUT PKG_CR.RefCurType);
  PROCEDURE IBPS_PRINT_RECONCILE(RefCuribps_print_reconcile IN OUT PKG_CR.RefCurType);
  PROCEDURE tad_print(RefCurTAD_PRINT IN OUT PKG_CR.RefCurType);

  PROCEDURE IBPS01_M(pMSG_ID number, pcurIBPSDTL out PKG_CR.RefCurType);

  PROCEDURE BK02(pFromDate   in date,
                 pToDate     in date,
                 pBranchType in number,
                 pFeeType    in number,
                 pBranch     in varchar2,
                 pBranch8    in varchar2,
                 RefCurBK02  IN OUT PKG_CR.RefCurType);

  PROCEDURE IBPS02(pfrdate      in date,
                   ptodate      in date,
                   RefCurIBPS02 IN OUT PKG_CR.RefCurType);
  FUNCTION IBPS_GET_Field(pCOntent varchar2, FiledCode varchar2)
    Return Varchar2;
  PROCEDURE SPLIT_SENDER(org_String   in varchar2,
                         split_string in varchar2,
                         srcBranch    out varchar2,
                         sender       out varchar2);
	PROCEDURE BM_IBPS23(pFromDate         in date,
                      pToDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS23 IN OUT PKG_CR.RefCurType);											 
												 
END; -- Package spec

CREATE OR REPLACE PACKAGE BODY GW_PK_IBPS_REPORT IS

  /*PROCEDURE bm_ibps01(RefCurBM_IBPS01 IN OUT PKG_CR.RefCurType)
  IS
  BEGIN
  open RefCurBM_IBPS01 for
  select '' as query_id,gw_trans_num,'' as rm_number,trans_date,'' as trans_code ,'' as sender,'' as receiver,'' as bank_sender
    ,'' as bank_receiver, amount,'' as ccycd,'' as msg_direction,'' as sendname,'' as sendaddress,'' as sendacc,'' as banksend,'' as receiname,'' as receiaddress,'' as receiacc,'' as bankrecei,'' as content,
      '' as buttoan,'' as trandate,'' as banksend_name,'' as bankreceive_name,'' as status from
  ibps_msg_content where 1<0;
  
  END BM_IBPS01;
  */
  ---------------------------------------
  /* Dien IBPS
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE IBPS_PRINT_MSG(pBranch         in varchar2,
                           pMSG_ID         in number,
                           pUser           in varchar2,
                           pcurBM_IBPS_MSG IN OUT PKG_CR.RefCurType) IS
  
    icount integer;
    vUser  varchar(30);
    --v_sql     varchar2(4000);
    -- pTblTable varchar2(100);  
    vContent      varchar2(4000) := '';
    vBranchName   varchar2(50) := '';
    Msg_ID        number(20);
    rm_number     varchar2(20);
    TransCode     varchar2(20);
    MSG_DIRECTION varchar2(10);
    Source_Branch varchar2(20);
    --Source_BranchName  varchar2(150);
    Receive_Branch varchar2(50);
    --Receive_BranchName varchar2(150);
    Trans_Date     date;
    Receiving_Time date;
    Sending_Time   date;
    Pre_Tad        varchar2(50);
    --Pre_TadName        varchar2(150);
    Print_Sts    number(1);
    Gw_Trans_Num number(6);
    Sender       varchar2(30);
    --SenderName         varchar2(150);
    Receiver varchar2(30);
    --ReceiverName       varchar2(150);
    AtBankSend      varchar2(30);
    AtBankSendName  varchar2(130);
    AtBankRecei     varchar2(30);
    AtBankReceiName varchar2(130);
    STATUS          varchar2(20);
    Amount          number(19, 2);
    Descriptions    varchar2(512);
    Tellerid        varchar2(30);
    TellerName      varchar(150);
    vCCYName        varchar2(100);
    vCCYCD          varchar2(5);
    vSibs_TellerID  varchar2(30);
    vF07            varchar2(30);
    vF19            varchar2(30);
    vF21            varchar2(30);
    vF22            varchar2(30);
    vProductType    varchar2(5);
  BEGIN
  
    select count(1)
      into icount
      from IBPS_MSG_CONTENT IMC
     where IMC.MSG_ID = pMSG_ID;
  
    if icount = 0 then
      select count(1)
        into icount
        from Ibps_Msg_All IMC
       where IMC.MSG_ID = pMSG_ID;
      if icount = 0 then
        select count(1)
          into icount
          from Ibps_Msg_All_His IMC
         where IMC.MSG_ID = pMSG_ID;
        if icount <> 0 then
          -- pTblTable := 'Ibps_Msg_All_His';
          select BR.Bran_Name
            into vBranchName
            From Branch BR
           where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
             and rownum = 1;
          if vBranchName is null then
            vBranchName := '';
          end if;
          vUser := pUser;
          select C.content,
                 C.Msg_id,
                 C.Rm_Number,
                 C.Trans_Code,
                 C.Msg_Direction,
                 C.Source_Branch,
                 C.tad,
                 C.Trans_Date,
                 C.Receiving_Time,
                 C.Sending_Time,
                 C.Pre_Tad,
                 C.Print_Sts,
                 C.Gw_Trans_Num,
                 C.F21               AS SENDER,
                 C.F22               AS RECEIVER,
                 C.F07, -- at send bank
                 C.F22, -- at recei bank
                 S.name, --status
                 C.Amount,
                 C.Trans_Description,
                 vUser,
                 C.Ccycd,
                 C.Sibs_Tellerid,
                 C.F07,
                 C.F19,
                 C.F21,
                 C.F22,
                 C.Product_Type
          --                 C.Tellerid,
            into vContent,
                 Msg_ID,
                 rm_number,
                 TransCode,
                 MSG_DIRECTION,
                 Source_Branch,
                 Receive_Branch,
                 Trans_Date,
                 Receiving_Time,
                 Sending_Time,
                 Pre_Tad,
                 Print_Sts,
                 Gw_Trans_Num,
                 Sender,
                 Receiver,
                 AtBankSend,
                 AtBankRecei,
                 STATUS,
                 Amount,
                 Descriptions,
                 Tellerid,
                 vCCYCD,
                 vSibs_TellerID,
                 vF07,
                 vF19,
                 vF21,
                 vF22,
                 vProductType
            from Ibps_Msg_All_His C
            LEFT JOIN STATUS S
              ON C.STATUS = S.STATUS
           where C.msg_id = pMSG_ID;
        
        end if;
      else
        -- pTblTable := 'Ibps_Msg_All';
        select BR.Bran_Name
          into vBranchName
          From Branch BR
         where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
           and rownum = 1;
        if vBranchName is null then
          vBranchName := '';
        end if;
        vUser := pUser;
        select C.content,
               C.Msg_id,
               C.Rm_Number,
               C.Trans_Code,
               C.Msg_Direction,
               C.Source_Branch,
               C.tad,
               C.Trans_Date,
               C.Receiving_Time,
               C.Sending_Time,
               C.Pre_Tad,
               C.Print_Sts,
               C.Gw_Trans_Num,
               C.F21               AS SENDER,
               C.F22               AS RECEIVER,
               C.F07, -- at send bank
               C.F19, -- at recei bank
               S.name, --status
               C.Amount,
               C.Trans_Description,
               vUser,
               C.Ccycd,
               C.Sibs_Tellerid,
               C.F07,
               C.F19,
               C.F21,
               C.F22,
               C.Product_Type
          into vContent,
               Msg_ID,
               rm_number,
               TransCode,
               MSG_DIRECTION,
               Source_Branch,
               Receive_Branch,
               Trans_Date,
               Receiving_Time,
               Sending_Time,
               Pre_Tad,
               Print_Sts,
               Gw_Trans_Num,
               Sender,
               Receiver,
               AtBankSend,
               AtBankRecei,
               STATUS,
               Amount,
               Descriptions,
               Tellerid,
               vCCYCD,
               vSibs_TellerID,
               vF07,
               vF19,
               vF21,
               vF22,
               vProductType
          from Ibps_Msg_All C
          LEFT JOIN STATUS S
            ON C.STATUS = S.STATUS
         where C.msg_id = pMSG_ID;
      
      end if;
    else
      --pTblTable := 'IBPS_MSG_CONTENT';   
      select BR.Bran_Name
        into vBranchName
        From Branch BR
       where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
         and rownum = 1;
      if vBranchName is null then
        vBranchName := '';
      end if;
      vUser := pUser;
      select C.content,
             C.Msg_id,
             C.Rm_Number,
             C.Trans_Code,
             C.Msg_Direction,
             C.Source_Branch,
             C.tad,
             C.Trans_Date,
             C.Receiving_Time,
             C.Sending_Time,
             C.Pre_Tad,
             C.Print_Sts,
             C.Gw_Trans_Num,
             /* C.F07 AS SENDER,
             C.F19 AS RECEIVER,
             C.F21, -- at send bank
             C.F22, -- at recei bank*/
             C.F21               AS SENDER,
             C.F22               AS RECEIVER,
             C.F07, -- at send bank
             C.F19, -- at recei bank
             S.name, --status
             C.Amount,
             C.Trans_Description,
             vUser,
             C.Ccycd,
             C.Sibs_Tellerid,
             C.F07,
             C.F19,
             C.F21,
             C.F22,
             C.Product_Type
        into vContent,
             Msg_ID,
             rm_number,
             TransCode,
             MSG_DIRECTION,
             Source_Branch,
             Receive_Branch,
             Trans_Date,
             Receiving_Time,
             Sending_Time,
             Pre_Tad,
             Print_Sts,
             Gw_Trans_Num,
             Sender,
             Receiver,
             AtBankSend,
             AtBankRecei,
             STATUS,
             Amount,
             Descriptions,
             Tellerid,
             vCCYCD,
             vSibs_TellerID,
             vF07,
             vF19,
             vF21,
             vF22,
             vProductType
        from IBPS_MSG_CONTENT C
        LEFT JOIN STATUS S
          ON C.STATUS = S.STATUS
       where C.msg_id = pMSG_ID;
    
    end if;
  
    Open pcurBM_IBPS_MSG for
      select vBranchName as branchname,
             Msg_ID AS MSG_ID,
             to_number(rm_number) AS RM_NUMBER,
             MSG_DIRECTION AS MSG_DIRECTION,
             decode(trim(vProductType),
                    'OL3',
                    Receive_Branch,
                    Source_Branch) AS SRCBRANCH,
             decode(trim(vProductType),
                    'OL3',
                    NVL((select substr(Br1.Sibs_Bank_Code, -3) || '-' ||
                               BR1.Tad_Name
                          From TAD BR1
                         where lpad(BR1.Sibs_Code, 5, '0') =
                               lpad(Receive_Branch, 5, '0')
                           and Rownum = 1),
                        ''),
                    NVL((select substr(Br.Sibs_Bank_Code, -3) || '-' ||
                               BR.Bank_Name
                          From IBPS_BANK_MAP BR
                         where Br.Gw_Bank_Code = vF21
                           AND lpad(Br.Sibs_Bank_Code, 5, '0') =
                               lpad(Source_Branch, 5, '0')
                           and Rownum = 1),
                        '')) AS SRCBRANCHNAME,
             /* NVL((select substr(Br.Sibs_Bank_Code,-3) || '-' || BR.Bank_Name
               From IBPS_BANK_MAP BR
              where Br.Gw_Bank_Code = decode(trim(vProductType),'OL3',vF21, vF07 )
              AND lpad(Br.Sibs_Bank_Code,5,'0')=lpad(decode(trim(vProductTYpe),'OL3',Receive_Branch, Source_Branch),5,'0')
              ),
             '') AS SRCBRANCHNAME,*/
             
             Receive_Branch AS RECEIBRANCH,
             NVL((select (Br.Gw_Bank_Code) || '-' || BR.TAD_NAME
                   From TAD BR
                  where Br.Gw_Bank_Code = vF21
                    and rownum = 1
                 --AND lpad(Br.Sibs_Bank_Code,5,'0')=lpad(Receive_Branch,5,'0')                  
                 ),
                 '') AS RECEIBRANCHNAME,
             
             Trans_Date     AS TRANSDATE,
             Receiving_Time AS Receiving_Time,
             Sending_Time   AS SEnding_Time,
             
             Pre_Tad AS PreTAD,
             NVL((select (Br.Gw_Bank_Code) || '-' || BR.TAD_NAME
                   From TAD BR
                  where rownum = 1
                    and to_number(Br.Sibs_Bank_Code) = to_number(Pre_Tad)),
                 '') AS PreTADName,
             
             Print_Sts AS PRINT_STATUS,
             Gw_Trans_Num AS BUTTOAN,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '011') as SOGD,
             Sender AS Sender,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = Sender
                 and rownum = 1) AS BankSenderName,
             
             Receiver AS Recerver,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = Receiver
                 and rownum = 1) AS BankReceiName,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '028') as sendname,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '029') as sendaddress,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '030') as sendacc,
             
             AtBankSend AS AtBankSend,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = AtBankSend
                 and rownum = 1) AS AtBankSendName,
             
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '031') as receiname,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '032') as receiaddress,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '033') as receiacc,
             
             AtBankRecei AS AtBankRecei,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = AtBankRecei
                 and rownum = 1) AS AtBankReceiName,
             trim(STATUS) AS STATUS,
             Amount AS AMOUNT,
             vCCYCD as CCY,
             
             --GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '034') as NOIDUNG,
             Descriptions as NOIDUNG,
             Descriptions AS Descriptions,
             vSibs_TellerID AS TellerID,
             vUser AS TellerName,
             TransCode AS TRANS_CODE,
             (Select CV.V_READ
                from ccytovie CV
               where CV.CCYCODE LIKE vCCYCD
                 and rownum = 1) AS CCYNAME,
             '1' AS GRPID
        from dual
      union
      select vBranchName AS BRANCHNAME,
             Msg_ID AS MSG_ID,
             to_number(rm_number) AS RM_NUMBER,
             MSG_DIRECTION AS MSG_DIRECTION,
             decode(trim(vProductTYpe),
                    'OL3',
                    Receive_Branch,
                    Source_Branch) AS SRCBRANCH,
             
             decode(trim(vProductType),
                    'OL3',
                    NVL((select substr(Br1.Sibs_Bank_Code, -3) || '-' ||
                               BR1.Tad_Name
                          From TAD BR1
                         where lpad(BR1.Sibs_Code, 5, '0') =
                               lpad(Receive_Branch, 5, '0')
                           and Rownum = 1),
                        ''),
                    NVL((select substr(Br.Sibs_Bank_Code, -3) || '-' ||
                               BR.Bank_Name
                          From IBPS_BANK_MAP BR
                         where Br.Gw_Bank_Code = vF21
                           AND lpad(Br.Sibs_Bank_Code, 5, '0') =
                               lpad(Source_Branch, 5, '0')
                           and Rownum = 1),
                        '')) AS SRCBRANCHNAME,
             
             /*NVL((select substr(Br.Sibs_Bank_Code,-3) || '-' || BR.Bank_Name
               From IBPS_BANK_MAP BR
              where Br.Gw_Bank_Code = decode(trim(vProductType),'OL3',vF21, vF07 )
              AND lpad(Br.Sibs_Bank_Code,5,'0')=lpad(decode(trim(vProductTYpe),'OL3',Receive_Branch, Source_Branch),5,'0')
              ),
             '')*/
             
             Receive_Branch AS RECEIBRANCH,
             NVL((select (Br.Gw_Bank_Code) || '-' || BR.TAD_NAME
                   From TAD BR
                  where Br.Gw_Bank_Code = vF21
                    and rownum = 1
                 --AND lpad(Br.Sibs_Bank_Code,5,'0')=lpad(Receive_Branch,5,'0')                  
                 ),
                 '') AS RECEIBRANCHNAME,
             
             Trans_Date     AS TRANSDATE,
             Receiving_Time AS Receiving_Time,
             Sending_Time   AS SEnding_Time,
             
             Pre_Tad AS PreTAD,
             (select Br.Sibs_Bank_Code || '-' || BR.Bran_Name
                From Branch BR
               where rownum = 1
                 and to_number(Br.Sibs_Bank_Code) = to_number(Pre_Tad)) AS PreTADName,
             
             Print_Sts AS PRINT_STATUS,
             Gw_Trans_Num AS BUTTOAN,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '011') as SOGD,
             
             Sender AS Sender,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = Sender
                 and rownum = 1) AS BankSenderName,
             
             Receiver AS Recerver,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = Receiver
                 and rownum = 1) AS BankReceiName,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '028') as sendname,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '029') as sendaddress,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '030') as sendacc,
             
             AtBankSend AS AtBankSend,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = AtBankSend
                 and rownum = 1) AS AtBankSendName,
             
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '031') as receiname,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '032') as receiaddress,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '033') as receiacc,
             
             AtBankRecei AS AtBankRecei,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = AtBankRecei
                 and rownum = 1) AS AtBankReceiName,
             trim(STATUS) AS STATUS,
             Amount AS AMOUNT,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '026') as CCY,
             
             --GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '034') as NOIDUNG,
             Descriptions as NOIDUNG,
             Descriptions AS Descriptions,
             vSibs_TellerID AS TellerID,
             vUser AS TellerName,
             TransCode AS TRANS_CODE,
             (Select CV.V_READ
                from ccytovie CV
               where CV.CCYCODE LIKE vCCYCD
                 and rownum = 1) AS CCYNAME,
             '2' AS GRPID
        from dual;
  
    /*   v_sql := 'select A.Msg_ID, A.Query_ID, A.Rm_Number, ''' || vBranchName ||
                ''' AS BRANCHNAME,A.MSG_DIRECTION,
          A.Source_Branch ,
          (select Br.Sibs_Bank_Code || '' - '' || BR.Bran_Name From Branch BR where to_number(Br.Sibs_Bank_Code)= A.Source_Branch) AS SRCBranchName,
    A.tad AS Receive_Branch,
     (select Br.Sibs_Bank_Code || '' - '' || BR.Bran_Name From Branch BR where to_number(Br.Sibs_Bank_Code)=to_number( A.tad)) AS RECBranchName, 
    A.Trans_Date,
    A.Receiving_Time,
    A.Sending_Time, 
    A.Pre_Tad,
    '''' PreTADName,
    A.Print_Sts,
    A.Gw_Trans_Num AS SOBUTTOAN,
    A.Trans_Code,
    A.F07 AS Sender,
    (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F07
                       and rownum = 1) bank_sender,
    A.F19 AS Receiver,                   
                   (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F19
                       and rownum = 1) bank_receiver,
    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''028'') as sendname, 
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''029'') as sendaddress,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''030'') as sendacc,
                   A.F21 AtBankSend,
                   (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F21
                       and rownum = 1) AS AtBankSendName,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''031'') as receiname,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''032'') as receiaddress,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''033'') as receiacc,  
                   A.F22 AS AtBankRecei,
                   (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F22
                       and rownum = 1) AtBankReceiName,
                   (select S.name
                      from status S
                     where S.status = A.status
                       and rownum = 1) STATUS,
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''026'') as CCY,
                    A.Amount,
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''034'') as NOIDUNG,
                   A.Trans_description as Descriptions, 
                   A.Tellerid,
                   ''noname'' AS TellerName  
                     from ' || pTblTable || ' A
                    where trim(A.MSG_ID) = trim(' || pMSG_ID || ')
                      and rownum = 1';
     
       Open pcurBMIBPSMSG for v_sql;
     */
  END IBPS_PRINT_MSG;
  -------------------------
  /* Dien IBPS 
     Creator: Bùi H?ng Phuong  
  */
  PROCEDURE IBPS_PRINT_MSG_LIST(pBranch         in varchar2,
                                pMSG_ID         in number,
                                pUser           in varchar2,
                                pcurBM_IBPS_MSG IN OUT PKG_CR.RefCurType) IS
  
    icount integer;
    vUser  varchar(30);
    --v_sql     varchar2(4000);
    -- pTblTable varchar2(100);  
    vContent      varchar2(4000) := '';
    vBranchName   varchar2(50) := '';
    Msg_ID        number(20);
    rm_number     varchar2(20);
    TransCode     varchar2(20);
    MSG_DIRECTION varchar2(10);
    Source_Branch varchar2(20);
    --Source_BranchName  varchar2(150);
    Receive_Branch varchar2(50);
    --Receive_BranchName varchar2(150);
    Trans_Date     date;
    Receiving_Time date;
    Sending_Time   date;
    Pre_Tad        varchar2(50);
    --Pre_TadName        varchar2(150);
    Print_Sts    number(1);
    Gw_Trans_Num number(6);
    Sender       varchar2(30);
    --SenderName         varchar2(150);
    Receiver varchar2(30);
    --ReceiverName       varchar2(150);
    AtBankSend      varchar2(30);
    AtBankSendName  varchar2(130);
    AtBankRecei     varchar2(30);
    AtBankReceiName varchar2(130);
    STATUS          varchar2(20);
    Amount          number(19, 2);
    Descriptions    varchar2(512);
    Tellerid        varchar2(30);
    TellerName      varchar(150);
    vCCYName        varchar2(100);
    vCCYCD          varchar2(5);
    vSibs_TellerID  varchar2(30);
    vF07            varchar2(30);
    vF19            varchar2(30);
    vF21            varchar2(30);
    vF22            varchar2(30);
    vProductType    varchar2(5);
  BEGIN
  
    select count(1)
      into icount
      from IBPS_MSG_CONTENT IMC
     where IMC.MSG_ID = pMSG_ID;
  
    if icount = 0 then
      select count(1)
        into icount
        from Ibps_Msg_All IMC
       where IMC.MSG_ID = pMSG_ID;
      if icount = 0 then
        select count(1)
          into icount
          from Ibps_Msg_All_His IMC
         where IMC.MSG_ID = pMSG_ID;
        if icount <> 0 then
          -- pTblTable := 'Ibps_Msg_All_His';
          select BR.Bran_Name
            into vBranchName
            From Branch BR
           where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
             and rownum = 1;
          if vBranchName is null then
            vBranchName := '';
          end if;
          vUser := pUser;
          select C.content,
                 C.Msg_id,
                 C.Rm_Number,
                 C.Trans_Code,
                 C.Msg_Direction,
                 C.Source_Branch,
                 C.tad,
                 C.Trans_Date,
                 C.Receiving_Time,
                 C.Sending_Time,
                 C.Pre_Tad,
                 C.Print_Sts,
                 C.Gw_Trans_Num,
                 C.F21               AS SENDER,
                 C.F22               AS RECEIVER,
                 C.F07, -- at send bank
                 C.F19, -- at recei bank
                 S.name, --status
                 C.Amount,
                 C.Trans_Description,
                 vUser,
                 C.Ccycd,
                 C.Sibs_Tellerid,
                 C.F07,
                 C.F19,
                 C.F21,
                 C.F22,
                 C.Product_Type
          --                 C.Tellerid,
            into vContent,
                 Msg_ID,
                 rm_number,
                 TransCode,
                 MSG_DIRECTION,
                 Source_Branch,
                 Receive_Branch,
                 Trans_Date,
                 Receiving_Time,
                 Sending_Time,
                 Pre_Tad,
                 Print_Sts,
                 Gw_Trans_Num,
                 Sender,
                 Receiver,
                 AtBankSend,
                 AtBankRecei,
                 STATUS,
                 Amount,
                 Descriptions,
                 Tellerid,
                 vCCYCD,
                 vSibs_TellerID,
                 vF07,
                 vF19,
                 vF21,
                 vF22,
                 vProductType
            from Ibps_Msg_All_His C
            LEFT JOIN STATUS S
              ON C.STATUS = S.STATUS
           where C.msg_id = pMSG_ID;
        
        end if;
      else
        -- pTblTable := 'Ibps_Msg_All';
        select BR.Bran_Name
          into vBranchName
          From Branch BR
         where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
           and rownum = 1;
        if vBranchName is null then
          vBranchName := '';
        end if;
        vUser := pUser;
        select C.content,
               C.Msg_id,
               C.Rm_Number,
               C.Trans_Code,
               C.Msg_Direction,
               C.Source_Branch,
               C.tad,
               C.Trans_Date,
               C.Receiving_Time,
               C.Sending_Time,
               C.Pre_Tad,
               C.Print_Sts,
               C.Gw_Trans_Num,
               C.F21               AS SENDER,
               C.F22               AS RECEIVER,
               C.F07, -- at send bank
               C.F19, -- at recei bank
               S.name, --status
               C.Amount,
               C.Trans_Description,
               vUser,
               C.Ccycd,
               C.Sibs_Tellerid,
               C.F07,
               C.F19,
               C.F21,
               C.F22,
               C.Product_Type
          into vContent,
               Msg_ID,
               rm_number,
               TransCode,
               MSG_DIRECTION,
               Source_Branch,
               Receive_Branch,
               Trans_Date,
               Receiving_Time,
               Sending_Time,
               Pre_Tad,
               Print_Sts,
               Gw_Trans_Num,
               Sender,
               Receiver,
               AtBankSend,
               AtBankRecei,
               STATUS,
               Amount,
               Descriptions,
               Tellerid,
               vCCYCD,
               vSibs_TellerID,
               vF07,
               vF19,
               vF21,
               vF22,
               vProductType
          from Ibps_Msg_All C
          LEFT JOIN STATUS S
            ON C.STATUS = S.STATUS
         where C.msg_id = pMSG_ID;
      
      end if;
    else
      --pTblTable := 'IBPS_MSG_CONTENT';   
      select BR.Bran_Name
        into vBranchName
        From Branch BR
       where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)
         and rownum = 1;
      if vBranchName is null then
        vBranchName := '';
      end if;
      vUser := pUser;
      select C.content,
             C.Msg_id,
             C.Rm_Number,
             C.Trans_Code,
             C.Msg_Direction,
             C.Source_Branch,
             C.tad,
             C.Trans_Date,
             C.Receiving_Time,
             C.Sending_Time,
             C.Pre_Tad,
             C.Print_Sts,
             C.Gw_Trans_Num,
             /* C.F07 AS SENDER,
             C.F19 AS RECEIVER,
             C.F21, -- at send bank
             C.F22, -- at recei bank*/
             C.F21               AS SENDER,
             C.F22               AS RECEIVER,
             C.F07, -- at send bank
             C.F19, -- at recei bank
             S.name, --status
             C.Amount,
             C.Trans_Description,
             vUser,
             C.Ccycd,
             C.Sibs_Tellerid,
             C.F07,
             C.F19,
             C.F21,
             C.F22,
             C.Product_Type
        into vContent,
             Msg_ID,
             rm_number,
             TransCode,
             MSG_DIRECTION,
             Source_Branch,
             Receive_Branch,
             Trans_Date,
             Receiving_Time,
             Sending_Time,
             Pre_Tad,
             Print_Sts,
             Gw_Trans_Num,
             Sender,
             Receiver,
             AtBankSend,
             AtBankRecei,
             STATUS,
             Amount,
             Descriptions,
             Tellerid,
             vCCYCD,
             vSibs_TellerID,
             vF07,
             vF19,
             vF21,
             vF22,
             vProductType
        from IBPS_MSG_CONTENT C
        LEFT JOIN STATUS S
          ON C.STATUS = S.STATUS
       where C.msg_id = pMSG_ID;
    
    end if;
  
    Open pcurBM_IBPS_MSG for
      select vBranchName as branchname,
             Msg_ID AS MSG_ID,
             to_number(rm_number) AS RM_NUMBER,
             MSG_DIRECTION AS MSG_DIRECTION,
             decode(trim(vProductType),
                    'OL3',
                    Receive_Branch,
                    Source_Branch) AS SRCBRANCH,
             NVL((select substr(Br.Sibs_Bank_Code, -3) || '-' ||
                        BR.Bank_Name
                   From IBPS_BANK_MAP BR
                  where Br.Gw_Bank_Code =
                        decode(trim(vProductType), 'OL3', vF21, vF07)
                    AND lpad(Br.Sibs_Bank_Code, 5, '0') =
                        lpad(decode(trim(vProductTYpe),
                                    'OL3',
                                    Receive_Branch,
                                    Source_Branch),
                             5,
                             '0')),
                 '') AS SRCBRANCHNAME,
             
             Receive_Branch AS RECEIBRANCH,
             NVL((select (Br.Gw_Bank_Code) || '-' || BR.TAD_NAME
                   From TAD BR
                  where Br.Gw_Bank_Code = vF21
                 --AND lpad(Br.Sibs_Bank_Code,5,'0')=lpad(Receive_Branch,5,'0')                  
                 ),
                 '') AS RECEIBRANCHNAME,
             
             Trans_Date     AS TRANSDATE,
             Receiving_Time AS Receiving_Time,
             Sending_Time   AS SEnding_Time,
             
             Pre_Tad AS PreTAD,
             NVL((select (Br.Gw_Bank_Code) || '-' || BR.TAD_NAME
                   From TAD BR
                  where to_number(Br.Sibs_Bank_Code) = to_number(Pre_Tad)),
                 '') AS PreTADName,
             
             Print_Sts AS PRINT_STATUS,
             Gw_Trans_Num AS BUTTOAN,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '011') as SOGD,
             Sender AS Sender,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = Sender
                 and rownum = 1) AS BankSenderName,
             
             Receiver AS Recerver,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = Receiver
                 and rownum = 1) AS BankReceiName,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '028') as sendname,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '029') as sendaddress,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '030') as sendacc,
             
             AtBankSend AS AtBankSend,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = AtBankSend
                 and rownum = 1) AS AtBankSendName,
             
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '031') as receiname,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '032') as receiaddress,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '033') as receiacc,
             
             AtBankRecei AS AtBankRecei,
             (select B.bank_name
                from IBPS_BANK_MAP B
               where B.gw_bank_code = AtBankRecei
                 and rownum = 1) AS AtBankReceiName,
             trim(STATUS) AS STATUS,
             Amount AS AMOUNT,
             vCCYCD as CCY,
             
             --GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, '034') as NOIDUNG,
             Descriptions as NOIDUNG,
             Descriptions AS Descriptions,
             vSibs_TellerID AS TellerID,
             vUser AS TellerName,
             TransCode AS TRANS_CODE,
             (Select CV.V_READ
                from ccytovie CV
               where CV.CCYCODE LIKE vCCYCD
                 and rownum = 1) AS CCYNAME,
             MSG_ID AS GRPID
      
        from dual;
  
    /*   v_sql := 'select A.Msg_ID, A.Query_ID, A.Rm_Number, ''' || vBranchName ||
                ''' AS BRANCHNAME,A.MSG_DIRECTION,
          A.Source_Branch ,
          (select Br.Sibs_Bank_Code || '' - '' || BR.Bran_Name From Branch BR where to_number(Br.Sibs_Bank_Code)= A.Source_Branch) AS SRCBranchName,
    A.tad AS Receive_Branch,
     (select Br.Sibs_Bank_Code || '' - '' || BR.Bran_Name From Branch BR where to_number(Br.Sibs_Bank_Code)=to_number( A.tad)) AS RECBranchName, 
    A.Trans_Date,
    A.Receiving_Time,
    A.Sending_Time, 
    A.Pre_Tad,
    '''' PreTADName,
    A.Print_Sts,
    A.Gw_Trans_Num AS SOBUTTOAN,
    A.Trans_Code,
    A.F07 AS Sender,
    (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F07
                       and rownum = 1) bank_sender,
    A.F19 AS Receiver,                   
                   (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F19
                       and rownum = 1) bank_receiver,
    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''028'') as sendname, 
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''029'') as sendaddress,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''030'') as sendacc,
                   A.F21 AtBankSend,
                   (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F21
                       and rownum = 1) AS AtBankSendName,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''031'') as receiname,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''032'') as receiaddress,
                   GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''033'') as receiacc,  
                   A.F22 AS AtBankRecei,
                   (select B.bank_name
                      from IBPS_BANK_MAP B
                     where B.gw_bank_code = A.F22
                       and rownum = 1) AtBankReceiName,
                   (select S.name
                      from status S
                     where S.status = A.status
                       and rownum = 1) STATUS,
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''026'') as CCY,
                    A.Amount,
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, ''034'') as NOIDUNG,
                   A.Trans_description as Descriptions, 
                   A.Tellerid,
                   ''noname'' AS TellerName  
                     from ' || pTblTable || ' A
                    where trim(A.MSG_ID) = trim(' || pMSG_ID || ')
                      and rownum = 1';
     
       Open pcurBMIBPSMSG for v_sql;
     */
  END IBPS_PRINT_MSG_LIST;
  -------------------------
  /* BM_IBPS02 : IBPS_BM_IBPS01 Bang ke dien di trong ngay
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS02(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS02 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vStatus     varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS02 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS02;
  -------------------------------------  
  /* BM_IBPS02_CN : IBPS_BM_IBPS01CN Bang ke dien di cua chi nhanh
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS02_CN(pDate              in date,
                         pCitad             in varchar2, --sender
                         pCcycd             in varchar2 default 'VND',
                         pStatus            in varchar2,
                         RefCurBM_IBPS02_CN IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vSender     varchar2(50); --sender 8 so
    vCcycd      varchar2(3);
    vStatus     varchar2(50);
    vSrcBranch  varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vSender     := '%';
      vSrcBranch  := '%';
      vBranchname := 'ALL';
    else
      GW_PK_IBPS_REPORT.SPLIT_SENDER(trim(pCitad),
                                     '-',
                                     vSrcBranch,
                                     vSender);
      vSrcBranch := lpad(vSrcBranch, 5, '0');
      if vSrcBranch = '00011' then
        select substr(T.Sibs_Bank_Code, -3) || '-' || T.Tad_Name
          into vBranchName
          from tad T
         where T.Gw_Bank_Code = vSender;
      else
        select substr(M.Sibs_Bank_Code, -3) || '-' || M.Bank_Name
          into vBranchName
          from ibps_bank_map M
         where M.Sibs_Bank_Code <> -1
           and lpad(M.Sibs_Bank_Code, 5, '0') = (vSrcBranch)
           and M.Gw_Bank_Code = vSender;
      end if;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               --decode(pCitad,'ALL','ALL', substr(BSEND.SIBS_BANK_CODE,-3) || ' - ' || BSEND.BANK_NAME) AS BRAN_NAME,               
               vBranchName,
               A.Tellerid,
               BR.Gw_Bank_Code || ' - ' || BR.TAD_NAME AS TADNAME, -- tad name
               BR1.SIBS_BANK_CODE || ' - ' || BR1.BRAN_NAME AS PRETADNAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                      /*AND C.F07 LIKE vSender                   
                      AND lpad(C.Source_Branch,5,'0') LIKE vSrcBranch*/
                      
                   AND ((nvl(C.Product_Type, '%') = 'OL3' AND
                       (lpad(C.Tad, 5, '0') like vSrcBranch OR
                       (C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) OR
                       (nvl(C.Product_Type, '%') <> 'OL3' AND
                       C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)  
        --     AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,'0'))   
          LEFT JOIN TAD BR
            ON BR.Gw_Bank_Code = A.F21
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
        
         WHERE B.CONTENT LIKE vStatus;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               --decode(pCitad,'ALL','ALL', substr(BSEND.SIBS_BANK_CODE,-3) || ' - ' || BSEND.BANK_NAME) AS BRAN_NAME,               
               vBranchName,
               A.Tellerid,
               BR.GW_BANK_CODE || ' - ' || BR.Tad_Name AS TADNAME, -- tad name
               BR1.SIBS_BANK_CODE || ' - ' || BR1.BRAN_NAME AS PRETADNAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                      /*AND C.F07 LIKE vSender                   
                      AND lpad(C.Source_Branch,5,'0') LIKE vSrcBranch*/
                      
                   AND ((nvl(C.Product_Type, '%') = 'OL3' AND
                       (lpad(C.Tad, 5, '0') like vSrcBranch OR
                       (C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) OR
                       (nvl(C.Product_Type, '%') <> 'OL3' AND
                       C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)  
        --     AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,'0'))   
          LEFT JOIN TAD BR
            ON BR.Gw_Bank_Code = A.F21
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
         WHERE B.CONTENT LIKE vStatus
         order by A.taD, A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               --decode(pCitad,'ALL','ALL', substr(BSEND.SIBS_BANK_CODE,-3) || ' - ' || BSEND.BANK_NAME) AS BRAN_NAME,               
               vBranchName,
               A.Tellerid,
               BR.GW_BANK_CODE || ' - ' || BR.Tad_Name AS TADNAME, -- tad name
               BR1.SIBS_BANK_CODE || ' - ' || BR1.BRAN_NAME AS PRETADNAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                      /*AND C.F07 LIKE vSender                   
                      AND lpad(C.Source_Branch,5,'0') LIKE vSrcBranch*/
                      
                   AND ((nvl(C.Product_Type, '%') = 'OL3' AND
                       (lpad(C.Tad, 5, '0') like vSrcBranch OR
                       (C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) OR
                       (nvl(C.Product_Type, '%') <> 'OL3' AND
                       C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)  
        --     AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,'0'))   
          LEFT JOIN TAD BR
            ON BR.Gw_Bank_Code = A.F21
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               --decode(pCitad,'ALL','ALL', substr(BSEND.SIBS_BANK_CODE,-3) || ' - ' || BSEND.BANK_NAME) AS BRAN_NAME,               
               vBranchName,
               A.Tellerid,
               BR.Gw_Bank_Code || ' - ' || BR.Tad_Name AS TADNAME, -- tad name
               BR1.SIBS_BANK_CODE || ' - ' || BR1.BRAN_NAME AS PRETADNAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                      /*AND C.F07 LIKE vSender                   
                      AND lpad(C.Source_Branch,5,'0') LIKE vSrcBranch*/
                      
                   AND ((nvl(C.Product_Type, '%') = 'OL3' AND
                       (lpad(C.Tad, 5, '0') like vSrcBranch OR
                       (C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) OR
                       (nvl(C.Product_Type, '%') <> 'OL3' AND
                       C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)  
        --     AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,'0'))   
          LEFT JOIN TAD BR
            ON BR.Gw_Bank_Code = A.F21
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               --decode(pCitad,'ALL','ALL', substr(BSEND.SIBS_BANK_CODE,-3) || ' - ' || BSEND.BANK_NAME) AS BRAN_NAME,               
               vBranchName,
               A.Tellerid,
               BR.Gw_Bank_Code || ' - ' || BR.Tad_Name AS TADNAME, -- tad name
               BR1.SIBS_BANK_CODE || ' - ' || BR1.BRAN_NAME AS PRETADNAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                      /*AND C.F07 LIKE vSender                   
                      AND lpad(C.Source_Branch,5,'0') LIKE vSrcBranch*/
                      
                   AND ((nvl(C.Product_Type, '%') = 'OL3' AND
                       (lpad(C.Tad, 5, '0') like vSrcBranch OR
                       (C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) OR
                       (nvl(C.Product_Type, '%') <> 'OL3' AND
                       C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)  
        --     AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,'0'))   
          LEFT JOIN TAD BR
            ON BR.Gw_Bank_Code = A.F21
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               --decode(pCitad,'ALL','ALL', substr(BSEND.SIBS_BANK_CODE,-3) || ' - ' || BSEND.BANK_NAME) AS BRAN_NAME,               
               vBranchName,
               A.Tellerid,
               BR.Gw_Bank_Code || ' - ' || BR.Tad_Name AS TADNAME, -- tad name
               BR1.SIBS_BANK_CODE || ' - ' || BR1.BRAN_NAME AS PRETADNAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                      /*AND C.F07 LIKE vSender                   
                      AND lpad(C.Source_Branch,5,'0') LIKE vSrcBranch*/
                      
                   AND ((nvl(C.Product_Type, '%') = 'OL3' AND
                       (lpad(C.Tad, 5, '0') like vSrcBranch OR
                       (C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) OR
                       (nvl(C.Product_Type, '%') <> 'OL3' AND
                       C.F07 LIKE vSender AND
                       lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)  
        --     AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,'0'))   
          LEFT JOIN TAD BR
            ON BR.Gw_Bank_Code = A.F21
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS02_CN for
      select *
        from BM_IBPS02_TEMP
       order by CCYCD, TAD, GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS02_CN;
  --------------------------
  /* BM_IBPS03_1 : IBPS_BM03.1 Bang ke dien di cua chi nhanh dau moi*/
  --Phuongbh: not use from 14/07/2010--
  PROCEDURE BM_IBPS03_1(pDate             in date,
                        pCitad            in varchar2, -- citad ma 8 so
                        pCcycd            in varchar2 default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS03_1 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitad  varchar2(50);
    vCcycd  varchar2(3);
    vStatus varchar2(50);
    vOndate date;
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad := '%';
    else
      vCitad := trim(pCitad);
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         BRNAME,
         Sendername,
         Receivername)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F07 LIKE C.F21
                   AND C.F21 like vCitad
                   AND to_number(C.Source_Branch) =
                       decode(C.F07, '79302001', 11, to_number(C.Tad))
                   AND to_number(C.Tad) =
                       decode(C.F07,
                              '79302001',
                              40,
                              to_number(C.Source_Branch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.tad, A.Trans_Code, RECEIVER asc;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         BRNAME,
         Sendername,
         Receivername)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F07 LIKE C.F21
                   AND C.F21 like vCitad
                   AND to_number(C.Source_Branch) =
                       decode(C.F07, '79302001', 11, to_number(C.Tad))
                   AND to_number(C.Tad) =
                       decode(C.F07,
                              '79302001',
                              40,
                              to_number(C.Source_Branch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SOURCE_BRANCH
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.taD, A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         BRNAME,
         Sendername,
         Receivername)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F07 LIKE C.F21
                   AND C.F21 like vCitad
                   AND to_number(C.Source_Branch) =
                       decode(C.F07, '79302001', 11, to_number(C.Tad))
                   AND to_number(C.Tad) =
                       decode(C.F07,
                              '79302001',
                              40,
                              to_number(C.Source_Branch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
        
         order by A.tad, A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         BRNAME,
         Sendername,
         Receivername)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F07 LIKE C.F21
                   AND C.F21 like vCitad
                   AND to_number(C.Source_Branch) =
                       decode(C.F07, '79302001', 11, to_number(C.Tad))
                   AND to_number(C.Tad) =
                       decode(C.F07,
                              '79302001',
                              40,
                              to_number(C.Source_Branch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.tad, A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         BRNAME,
         Sendername,
         Receivername)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F07 LIKE C.F21
                   AND C.F21 like vCitad
                   AND to_number(C.Source_Branch) =
                       decode(C.F07, '79302001', 11, to_number(C.Tad))
                   AND to_number(C.Tad) =
                       decode(C.F07,
                              '79302001',
                              40,
                              to_number(C.Source_Branch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.tad, A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         BRNAME,
         Sendername,
         Receivername)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               upper(trim(A.CCYCD)),
               A.Trans_Code,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.F21
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F07 LIKE C.F21
                   AND C.F21 like vCitad
                   AND to_number(C.Source_Branch) =
                       decode(C.F07, '79302001', 11, to_number(C.Tad))
                   AND to_number(C.Tad) =
                       decode(C.F07,
                              '79302001',
                              40,
                              to_number(C.Source_Branch))) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.tad, A.Trans_Code, RECEIVER asc;
    end if;
    open RefCurBM_IBPS03_1 for
      select *
        from BM_IBPS02_TEMP
       order by CCYCD, TAD, GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS03_1;
  --------------------------

  /* BM_IBPS03_2 : Bang ke dien import file Excel 
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS03_2(pDate             IN DATE,
                        pCITAD            IN VARCHAR2, -- citad ma 8 so
                        pCCYCD            IN VARCHAR2 Default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS03_2 IN OUT PKG_CR.RefCurType) IS
  
    vCitad      VARCHAR2(10);
    vCcycd      VARCHAR2(10);
    vStatus     VARCHAR2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      DELETE FROM BM_IBPS02_TEMP;
      --Du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, -- tad name
         EXTFIELD03, -- ten nguoi huong
         EXTFIELD04) -- Msg-src
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BCNDM.SIBS_BANK_CODE, -3) || ' - ' || BCNDM.BANK_NAME AS BRAN_NAME,
               A.NGUOIHUONG,
               A.Msg_Src
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOIHUONG,
                       C.Msg_Src,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND C.Msg_Src IN ('2', '3')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         where B.content like vStatus
         order by A.Trans_Code, RECEIVER asc;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- ten nguoi huong
         EXTFIELD04) -- Msg-src
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TellerID,
               substr(BCNDM.SIBS_BANK_CODE, -3) || ' - ' || BCNDM.BANK_NAME AS BRAN_NAME,
               A.NGUOIHUONG,
               A.Msg_Src
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOIHUONG,
                       C.Msg_Src,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND C.Msg_Src IN ('2', '3')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         where B.content like vStatus
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- ten nguoi huong
         EXTFIELD04) -- Msg-src
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TellerID,
               substr(BCNDM.SIBS_BANK_CODE, -3) || ' - ' || BCNDM.BANK_NAME AS BRAN_NAME,
               A.NGUOIHUONG,
               A.Msg_Src
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOIHUONG,
                       C.Msg_Src,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND C.Msg_Src IN ('2', '3')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         where B.content like vStatus
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- ten nguoi huong
         EXTFIELD04) -- Msg-src
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BCNDM.SIBS_BANK_CODE, -3) || ' - ' || BCNDM.BANK_NAME AS BRAN_NAME,
               A.NGUOIHUONG,
               A.Msg_Src
        
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOIHUONG,
                       C.Msg_Src,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND C.Msg_Src IN ('2', '3')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         where B.content like vStatus
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- ten nguoi huong
         EXTFIELD04) -- Msg-src
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BCNDM.SIBS_BANK_CODE, -3) || ' - ' || BCNDM.BANK_NAME AS BRAN_NAME,
               A.NGUOIHUONG,
               A.Msg_Src
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOIHUONG,
                       C.Msg_Src,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND C.Msg_Src IN ('2', '3')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         where B.content like vStatus
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- ten nguoi huong
         EXTFIELD04) -- Msg-src
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BCNDM.SIBS_BANK_CODE, -3) || ' - ' || BCNDM.BANK_NAME AS BRAN_NAME,
               A.NGUOIHUONG,
               A.Msg_Src
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOIHUONG,
                       C.Msg_Src,
                       C.F21
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND C.Msg_Src IN ('2', '3')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BCNDM
            on BCNDM.GW_BANK_CODE = A.SENDER
           AND BCNDM.Sibs_Bank_Code =
               decode(BCNDM.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         where B.content like vStatus
         order by A.Trans_Code, RECEIVER asc;
    end if;
  
    open RefCurBM_IBPS03_2 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS03_2;
  ----------------------------------

  PROCEDURE BM_IBPS02_A(pDate             in date,
                        pCitad            in varchar2,
                        pCcycd            in varchar2 default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS02_A IN OUT PKG_CR.RefCurType) IS
    --vDate   date;
    vCitad  varchar2(50);
    vCcycd  varchar2(3);
    vStatus varchar2(50);
    --vDirec  varchar2(10);
    vOndate date;
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad := '%';
    else
      vCitad := trim(lpad(pCitad, 5, 0));
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       RM_NUMBER,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       C.F07           AS SENDER,
                       C.F22           as RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TAD like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --order by tad,RM_NUMBER asc;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 AS SENDER,
                       C.F22 as RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TAD like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 AS SENDER,
                       C.F22 as RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TAD like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 AS SENDER,
                       C.F22 as RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TAD like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 AS SENDER,
                       C.F22 as RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TAD like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       --substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 AS SENDER,
                       C.F22 as RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TAD like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS02_A for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER;
  END BM_IBPS02_A;
  ------------------------------
  PROCEDURE BM_IBPS03(pdate           IN date,
                      pCcycd          in varchar2 default 'VND',
                      pCitad          in varchar2,
                      RefCurBM_IBPS03 IN OUT PKG_CR.RefCurType) IS
    vCitad varchar2(50);
    vCcycd varchar2(3);
  BEGIN
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad := '%';
    else
      vCitad := trim(pCitad);
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    open RefCurBM_IBPS03 for
      SELECT A.*
        FROM (select a.*, b.CONTENT
                from (SELECT A.msg_id,
                             substr(A.rm_number, 5) field20,
                             A.sender,
                             A.receiver,
                             A.amount,
                             A.ccy,
                             A.trans_date,
                             A.exception_type,
                             a.msg_direction,
                             a.status,
                             a.tad
                        FROM IBPS_MSG_REC A
                      /*     WHERE decode(trim(a.ccy),null,'1','','1',UPPER(TRIM(a.ccy))) LIKE
                                  decode(trim(a.ccy),null,'%%','','%%','%' || DECODE(pCcycd, 'ALL', '', UPPER(TRIM(pCcycd))) || '%')
                          AND decode(trim(a.tad),null,'1','','1',UPPER(TRIM(a.tad))) LIKE
                                  decode(trim(a.tad),null,'%%','','%%','%' || DECODE(pCitad, 'ALL', '', UPPER(TRIM(pCitad))) || '%')
                      --AND a.tad like vCitad*/
                      --Sua 22/10/2008
                       where decode(trim(a.ccy),
                                    null,
                                    '1',
                                    '',
                                    '1',
                                    UPPER(TRIM(a.ccy))) LIKE
                             DECODE(pCcycd, 'ALL', '%', UPPER(TRIM(pCcycd)))
                         and decode(trim(a.tad),
                                    null,
                                    '1',
                                    '',
                                    '1',
                                    UPPER(TRIM(a.tad))) LIKE
                             DECODE(pCitad, 'ALL', '%', UPPER(TRIM(pCitad)))
                            --end
                         AND A.NDATE = to_char(pdate, 'YYYYMMDD')
                         and a.exception_type in ('GI', 'GS')) A
              /*left join
              (
                 select CONTENT,cdval from allcode where gwtype='SYSTEM'and cdname='STATUS'
              )B on a.status=b.cdval*/
                LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
                  ON trim(A.STATUS) = trim(B.STATUS)
              union
              select a.*, b.CONTENT
                from (SELECT A.msg_id,
                             substr(A.rm_number, 5) field20,
                             A.sender,
                             A.receiver,
                             A.amount,
                             A.ccy,
                             A.trans_date,
                             A.exception_type,
                             a.msg_direction,
                             a.status,
                             a.tad
                        FROM IBPS_MSG_REC A
                      /*WHERE decode(trim(a.ccy),null,'1','','1',UPPER(TRIM(a.ccy))) LIKE
                                  decode(trim(a.ccy),null,'%%','','%%','%' || DECODE(pCcycd, 'ALL', '', UPPER(TRIM(pCcycd))) || '%')
                          AND decode(trim(a.tad),null,'1','','1',UPPER(TRIM(a.tad))) LIKE
                                  decode(trim(a.tad),null,'%%','','%%','%' || DECODE(pCitad, 'ALL', '', UPPER(TRIM(pCitad))) || '%')
                      --AND a.tad like vCitad*/
                      --Sua 22/10/2008
                       where decode(trim(a.ccy),
                                    null,
                                    '1',
                                    '',
                                    '1',
                                    UPPER(TRIM(a.ccy))) LIKE
                             DECODE(pCcycd, 'ALL', '%', UPPER(TRIM(pCcycd)))
                         and decode(trim(a.tad),
                                    null,
                                    '1',
                                    '',
                                    '1',
                                    UPPER(TRIM(a.tad))) LIKE
                             DECODE(pCitad, 'ALL', '%', UPPER(TRIM(pCitad)))
                            --end
                         AND A.NDATE = to_number(to_char(pdate, 'YYYYMMDD'))
                         and a.exception_type not in ('GI', 'GS')) A
                left join (select CONTENT, cdval
                             from allcode
                            where gwtype = 'SYSTEM'
                              and cdname = 'MSGSTS') B
                  on a.status = b.cdval
              
              ) A
       order by msg_direction desc;
  END BM_IBPS03;
  -------------------------
  /*BM_IBPS04 : Bang ke dien chuyen cong TAD
    Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS04(pDate           in date,
                      pCitadOld       in varchar2, -- ma 8 so
                      pCitadNew       in varchar2, -- ma 8 so
                      pCcycd          in varchar2 default 'VND',
                      pSTATUS         IN VARCHAR2,
                      RefCurBM_IBPS04 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitadOld varchar2(50); --ma 3 so
    vCitadNew varchar2(50); -- ma 8 so
    vCcycd    varchar2(3);
    vOndate   date;
    vStatus   varchar2(20);
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pCitadOld)) = 'ALL' or trim(pCitadOld) IS NULL then
      vCitadOld := '%';
    else
      select nvl(T.Sibs_Code, '')
        into vCitadOld
        from tad T
       where T.Gw_Bank_Code LIKE trim(pCitadOld)
         AND rownum = 1;
    end if;
    if upper(trim(pCitadNew)) = 'ALL' or trim(pCitadNew) IS NULL then
      vCitadNew := '%';
    else
      vCitadNew := trim(pCitadNew);
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) IS NULL then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) = '' then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitadOld,
                      '%',
                      'ALL',
                      BR1.GW_BANK_CODE || ' - ' || BR1.Tad_Name),
               A.Tellerid,
               T.GW_BANK_CODE || ' - ' || T.Bank_Name, -- tad name
               substr(BR1.Sibs_Code, -3) || ' - ' || BR1.TAD_NAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 where trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                   AND lpad(C.PRE_TAD, 5, '0') like vCitadOld
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitadNew
                   AND length(C.GW_TRANS_NUM) = 6
                   AND substr(C.Gw_Trans_Num, 1, 1) = '6'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TRANSDATE = To_char(pDATE, 'YYYYMMDD')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN TAD BR1
            ON BR1.Sibs_Code = lpad(A.Pre_Tad, 5, '0')
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitadOld,
                      '%',
                      'ALL',
                      BR1.GW_BANK_CODE || ' - ' || BR1.Tad_Name),
               A.Tellerid,
               T.GW_BANK_CODE || ' - ' || T.Bank_Name, -- tad name
               substr(BR1.Sibs_Code, -3) || ' - ' || BR1.TAD_NAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 where trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.PRE_TAD IS NOT NULL
                   AND lpad(C.PRE_TAD, 5, '0') like vCitadOld
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitadNew
                   AND length(C.GW_TRANS_NUM) = 6
                   AND substr(C.Gw_Trans_Num, 1, 1) = '6'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TRANSDATE = To_char(pDATE, 'YYYYMMDD')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN TAD BR1
            ON BR1.Sibs_Code = lpad(A.Pre_Tad, 5, '0')
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitadOld,
                      '%',
                      'ALL',
                      BR1.GW_BANK_CODE || ' - ' || BR1.Tad_Name),
               A.Tellerid,
               T.GW_BANK_CODE || ' - ' || T.Bank_Name, -- tad name
               substr(BR1.Sibs_Code, -3) || ' - ' || BR1.TAD_NAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 where trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.PRE_TAD IS NOT NULL
                   AND lpad(C.PRE_TAD, 5, '0') like vCitadOld
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitadNew
                   AND length(C.GW_TRANS_NUM) = 6
                   AND substr(C.Gw_Trans_Num, 1, 1) = '6'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TRANSDATE = To_char(pDATE, 'YYYYMMDD')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN TAD BR1
            ON BR1.Sibs_Code = lpad(A.Pre_Tad, 5, '0')
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitadOld,
                      '%',
                      'ALL',
                      BR1.GW_BANK_CODE || ' - ' || BR1.Tad_Name),
               A.TelleriD,
               T.GW_BANK_CODE || ' - ' || T.Bank_Name, -- tad name
               substr(BR1.Sibs_Code, -3) || ' - ' || BR1.TAD_NAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 where trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.PRE_TAD IS NOT NULL
                   AND lpad(C.PRE_TAD, 5, '0') like vCitadOld
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitadNew
                   AND length(C.GW_TRANS_NUM) = 6
                   AND substr(C.Gw_Trans_Num, 1, 1) = '6'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TRANSDATE = To_char(pDATE, 'YYYYMMDD')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN TAD BR1
            ON BR1.Sibs_Code = lpad(A.Pre_Tad, 5, '0')
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitadOld,
                      '%',
                      'ALL',
                      BR1.GW_BANK_CODE || ' - ' || BR1.Tad_Name),
               A.TellerID,
               T.GW_BANK_CODE || ' - ' || T.Bank_Name, -- tad name
               substr(BR1.Sibs_Code, -3) || ' - ' || BR1.TAD_NAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 where trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.PRE_TAD IS NOT NULL
                   AND lpad(C.PRE_TAD, 5, '0') like vCitadOld
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitadNew
                   AND length(C.GW_TRANS_NUM) = 6
                   AND substr(C.Gw_Trans_Num, 1, 1) = '6'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TRANSDATE = To_char(pDATE, 'YYYYMMDD')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN TAD BR1
            ON BR1.Sibs_Code = lpad(A.Pre_Tad, 5, '0')
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitadOld,
                      '%',
                      'ALL',
                      BR1.GW_BANK_CODE || ' - ' || BR1.Tad_Name),
               A.TellerID,
               T.GW_BANK_CODE || ' - ' || T.Bank_Name, -- tad name
               substr(BR1.Sibs_Code, -3) || ' - ' || BR1.TAD_NAME -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21
                  FROM IBPS_MSG_ALL_HIS C
                 where trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.PRE_TAD IS NOT NULL
                   AND lpad(C.PRE_TAD, 5, '0') like vCitadOld
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitadNew
                   AND length(C.GW_TRANS_NUM) = 6
                   AND substr(C.Gw_Trans_Num, 1, 1) = '6'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.TRANSDATE = To_char(pDATE, 'YYYYMMDD')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN TAD BR1
            ON BR1.Sibs_Code = lpad(A.Pre_Tad, 5, '0')
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
    end if;
    -----
    open RefCurBM_IBPS04 for
      select * from bm_ibps02_temp order by GW_TRANS_NUM;
  END BM_IBPS04;

  -------------------------
  /*  Bang ke chuyen doi ma loai giao dich 
      Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS05(pDATE           IN DATE,
                      pCitad          IN VARCHAR2, -- citad ma 8 so
                      pCcycd          IN VARCHAR2,
                      RefCurBM_IBPS05 IN OUT PKG_CR.RefCurType) IS
  
    vCitad  VARCHAR2(10);
    vCcycd  VARCHAR2(10);
    vOndate date;
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad := '%';
    else
      vCitad := trim(pCitad);
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      DELETE FROM BM_IBPS02_TEMP;
      --Du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, -- pre tad name
         EXTFIELD02 --tad name         
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BR1.Bran_Name, -- pre tad name
               BSEND.GW_BANK_CODE || ' - ' || BSEND.BANK_NAME --chi nhanh nhan dien F07               
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = '7') A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, -- pre tad name
         EXTFIELD02 --tad name 
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BR1.Bran_Name, -- pre tad name
               BSEND.GW_BANK_CODE || ' - ' || BSEND.BANK_NAME --chi nhanh nhan dien F07
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = '7') A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, -- pre tad name
         EXTFIELD02 --tad name 
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BR1.Bran_Name, -- pre tad name
               BSEND.GW_BANK_CODE || ' - ' || BSEND.BANK_NAME --chi nhanh nhan dien F07
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = '7') A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);
      --order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, -- pre tad name
         EXTFIELD02 --tad name 
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BR1.Bran_Name, -- pre tad name
               BSEND.GW_BANK_CODE || ' - ' || BSEND.BANK_NAME --chi nhanh nhan dien F07
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = '7') A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);
      --order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, -- pre tad name
         EXTFIELD02 --tad name 
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BR1.Bran_Name, -- pre tad name
               BSEND.GW_BANK_CODE || ' - ' || BSEND.BANK_NAME --chi nhanh nhan dien F07
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = '7') A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);
      -- order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, -- pre tad name
         EXTFIELD02 --tad name 
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               decode(vCitad,
                      '%',
                      'ALL',
                      T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               BR1.Bran_Name, -- pre tad name
               BSEND.GW_BANK_CODE || ' - ' || BSEND.BANK_NAME --chi nhanh nhan dien F07
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.F21
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND C.ccycd LIKE vCcycd
                   AND C.F21 like vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = '7') A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);
    end if;
  
    open RefCurBM_IBPS05 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  
  END BM_IBPS05;
  ------------------------- 
  /* BM_IBPS06 : Bang ke dien den trong ngay
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS06(pDate           in date,
                      pCitad          in varchar2, -- ma 8 so
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS06 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vStatus     varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F21 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F22
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F22 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F22
           AND T.Sibs_Bank_Code <> -1
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F21 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F22
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F22 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F22
           AND T.Sibs_Bank_Code <> -1
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               -- decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F21 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F22
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F22 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F22
           AND T.Sibs_Bank_Code <> -1
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               -- decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TelleriD
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F21 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F22
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F22 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F22
           AND T.Sibs_Bank_Code <> -1
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TellerID
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F21 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F22
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F22 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F22
           AND T.Sibs_Bank_Code <> -1
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TellerID
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F21 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F22
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'IBPS-SIBS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F22 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F22
           AND T.Sibs_Bank_Code <> -1
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE B.CONTENT LIKE vStatus
         order by A.Trans_Code, RECEIVER asc;
    end if;
    open RefCurBM_IBPS06 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS06;

  ----------------------------
  /*  Bang ke dien di ho chi nhanh khac 
      Creator: Bùi H?ng Phuong  
  */
  PROCEDURE BM_IBPS07(pDATE           IN DATE,
                      pCitad          IN VARCHAR2, -- citad ma 8 so
                      pBranch         IN VARCHAR2, -- ma 8 so
                      pCcycd          IN VARCHAR2,
                      RefCurBM_IBPS07 IN OUT PKG_CR.RefCurType) IS
  
    vCitad VARCHAR2(10);
    --vBranch VARCHAR2(10);
    vCcycd      VARCHAR2(10);
    vOndate     date;
    vSender     varchar2(50); --sender 8 so   
    vSrcBranch  varchar2(50);
    vBranchName varchar2(200);
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pBranch)) = 'ALL' or trim(pBranch) is null then
      vSender    := '%';
      vSrcBranch := '%';
    else
      GW_PK_IBPS_REPORT.SPLIT_SENDER(trim(pBranch),
                                     '-',
                                     vSrcBranch,
                                     vSender);
      vSrcBranch := lpad(vSrcBranch, 5, '0');
    
    end if;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    /* if upper(trim(pBranch)) = 'ALL'  or trim(pBranch) is null  then
      vBranch := '%';
    else
      select nvl(T.Sibs_Code,'') into vBranch 
    from tad T where T.Gw_Bank_Code LIKE
       trim(pBranch) AND rownum=1;
       if vBranch<>'' then vBranch:=lpad(vBranch,5,'0'); end if;
    end if;*/
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      DELETE FROM BM_IBPS02_TEMP;
      --Du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BSEND.SIBS_BANK_CODE, -3) || '-' || BSEND.Bank_Name AS SRCBRANCHNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                      --AND lpad(C.PRE_TAD,5,'0') like vBranch                   
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitad
                   AND ((nvl(C.Product_Type, '%') LIKE 'OL3') OR
                       (nvl(C.Product_Type, '%') NOT LIKE 'OL3' AND
                       (C.F07 <> C.F21 OR
                       (C.F07 = C.F21 AND lpad(C.Source_Branch, 5, '0') <>
                       lpad(C.Tad, 5, '0')))) OR
                       (length(C.GW_TRANS_NUM) = 6 AND
                       substr(C.Gw_Trans_Num, 1, 1) = '6'))
                   AND nvl(C.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on BCNDM.SIBS_BANK_CODE = substr(A.SOURCE_BRANCH, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TellerID,
               substr(BSEND.SIBS_BANK_CODE, -3) || '-' || BSEND.Bank_Name AS SRCBRANCHNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                      --AND lpad(C.PRE_TAD,5,'0') like vBranch                   
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitad
                   AND ((nvl(C.Product_Type, '%') LIKE 'OL3') OR
                       (nvl(C.Product_Type, '%') NOT LIKE 'OL3' AND
                       (C.F07 <> C.F21 OR
                       (C.F07 = C.F21 AND lpad(C.Source_Branch, 5, '0') <>
                       lpad(C.Tad, 5, '0')))) OR
                       (length(C.GW_TRANS_NUM) = 6 AND
                       substr(C.Gw_Trans_Num, 1, 1) = '6'))
                   AND nvl(C.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on BCNDM.SIBS_BANK_CODE = substr(A.SOURCE_BRANCH, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.TellerID,
               substr(BSEND.SIBS_BANK_CODE, -3) || '-' || BSEND.Bank_Name AS SRCBRANCHNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                      --AND lpad(C.PRE_TAD,5,'0') like vBranch                   
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitad
                   AND ((nvl(C.Product_Type, '%') LIKE 'OL3') OR
                       (nvl(C.Product_Type, '%') NOT LIKE 'OL3' AND
                       (C.F07 <> C.F21 OR
                       (C.F07 = C.F21 AND lpad(C.Source_Branch, 5, '0') <>
                       lpad(C.Tad, 5, '0')))) OR
                       (length(C.GW_TRANS_NUM) = 6 AND
                       substr(C.Gw_Trans_Num, 1, 1) = '6'))
                   AND nvl(C.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on BCNDM.SIBS_BANK_CODE = substr(A.SOURCE_BRANCH, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BSEND.SIBS_BANK_CODE, -3) || '-' || BSEND.Bank_Name AS SRCBRANCHNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                      --AND lpad(C.PRE_TAD,5,'0') like vBranch                   
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitad
                   AND ((nvl(C.Product_Type, '%') LIKE 'OL3') OR
                       (nvl(C.Product_Type, '%') NOT LIKE 'OL3' AND
                       (C.F07 <> C.F21 OR
                       (C.F07 = C.F21 AND lpad(C.Source_Branch, 5, '0') <>
                       lpad(C.Tad, 5, '0')))) OR
                       (length(C.GW_TRANS_NUM) = 6 AND
                       substr(C.Gw_Trans_Num, 1, 1) = '6'))
                   AND nvl(C.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on BCNDM.SIBS_BANK_CODE = substr(A.SOURCE_BRANCH, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BSEND.SIBS_BANK_CODE, -3) || '-' || BSEND.Bank_Name AS SRCBRANCHNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                      --AND lpad(C.PRE_TAD,5,'0') like vBranch                   
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitad
                   AND ((nvl(C.Product_Type, '%') LIKE 'OL3') OR
                       (nvl(C.Product_Type, '%') NOT LIKE 'OL3' AND
                       (C.F07 <> C.F21 OR
                       (C.F07 = C.F21 AND lpad(C.Source_Branch, 5, '0') <>
                       lpad(C.Tad, 5, '0')))) OR
                       (length(C.GW_TRANS_NUM) = 6 AND
                       substr(C.Gw_Trans_Num, 1, 1) = '6'))
                   AND nvl(C.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on BCNDM.SIBS_BANK_CODE = substr(A.SOURCE_BRANCH, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(vCitad,'%','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),
               vBranchName,
               A.Tellerid,
               substr(BSEND.SIBS_BANK_CODE, -3) || '-' || BSEND.Bank_Name AS SRCBRANCHNAME
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                      --AND C.PRE_TAD IS NOT NULL
                      --AND lpad(C.PRE_TAD,5,'0') like vBranch                   
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch
                      --and C.tad like vCitadNew
                   AND C.F21 LIKE vCitad
                   AND ((nvl(C.Product_Type, '%') LIKE 'OL3') OR
                       (nvl(C.Product_Type, '%') NOT LIKE 'OL3' AND
                       (C.F07 <> C.F21 OR
                       (C.F07 = C.F21 AND lpad(C.Source_Branch, 5, '0') <>
                       lpad(C.Tad, 5, '0')))) OR
                       (length(C.GW_TRANS_NUM) = 6 AND
                       substr(C.Gw_Trans_Num, 1, 1) = '6'))
                   AND nvl(C.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on BCNDM.SIBS_BANK_CODE = substr(A.SOURCE_BRANCH, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         order by A.Trans_Code, RECEIVER asc;
    end if;
  
    open RefCurBM_IBPS07 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  
  END BM_IBPS07;

  --------------
  /* BM_IBPS08 : Han muc no rong
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS08(pfrdate         in date,
                      ptodate         in date,
                      pCcycd          in varchar2 default 'VND',
                      RefCurBM_IBPS08 IN OUT PKG_CR.RefCurType) IS
    NumOfDate  NUMBER;
    LVMsgIN    NUMBER(38, 2);
    LVMsgOUT   NUMBER(38, 2);
    LVCountIN  NUMBER(38);
    LVCountOUT NUMBER(38);
    vCcycd     varchar2(5);
  BEGIN
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) IS NULL then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    --insert into test_clob values (TO_DATE(frdate,'DD/MM/YYYY') || '-' || TO_DATE(todate,'DD/MM/YYYY'));
    -- commit;  
    ---Tong gia tri dien di gia tri thap    
    SELECT SUM(AMOUNT), COUNT(msg_id)
      into LVMsgOUT, LVCountOUT
      FROM (SELECT amount, msg_id
              FROM ibps_msg_content
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
               AND TRIM(Ccycd) LIKE TRIM(vCcycd)
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
               AND TRIM(Ccycd) LIKE TRIM(vCcycd)
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL_HIS
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
               AND TRIM(Ccycd) LIKE TRIM(vCcycd));
  
    ---Tong gia tri dien den gia tri thap
    SELECT SUM(amount), COUNT(MSG_ID)
      into LVMsgIN, LVCountIN
      FROM (SELECT amount, msg_id
              FROM ibps_msg_content
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
               AND TRIM(Ccycd) LIKE TRIM(vCcycd)
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
               AND TRIM(Ccycd) LIKE TRIM(vCcycd)
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL_HIS
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
               AND TRIM(Ccycd) LIKE TRIM(vCcycd));
  
    --NumOfDate := ptodate - pfrdate + 1;
    --NumOfDate := MONTHS_BETWEEN(ptodate, pfrdate) + 1;
    NumOfDate := ROUND(MONTHS_BETWEEN(ptodate, pfrdate) + 0.4999) * 30;
    open RefCurBM_IBPS08 for
      select nvl(LVCountOUT, 0) AS LVCountOUT,
             nvl(LVCountIN, 0) AS LVCountIN,
             nvl(LVMsgOUT, 0) AS LVMsgOUT,
             nvl(LVMsgIN, 0) AS LVMsgIN,
             NumOfDate DAYS,
             ((nvl(LVMsgOUT, 0) - nvl(LVMsgIN, 0)) / NumOfDate) as Net
        from dual;
  
  END BM_IBPS08;
  ----------------------------
  /* BM_IBPS09 : Bang ke gui lai dien ngay truoc 
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS09(pDate           IN DATE,
                      pCITAD          IN VARCHAR2, --citad ma 8 so
                      pCCYCD          IN VARCHAR2 Default 'VND',
                      RefCurBM_IBPS09 IN OUT PKG_CR.RefCurType) IS
  
    vCitad      VARCHAR2(10);
    vCcycd      VARCHAR2(10);
    vBranchName varchar2(200);
    vOndate     date;
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      DELETE FROM BM_IBPS02_TEMP;
      --Du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.Tellerid,
               BCNDM.SIBS_BANK_CODE || '-' || BCNDM.BRAN_NAME AS SRCNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 LIKE vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on lpad(BCNDM.SIBS_BANK_CODE, 5, '0') =
               lpad(A.SOURCE_BRANCH, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.TellerID,
               BCNDM.SIBS_BANK_CODE || '-' || BCNDM.BRAN_NAME AS SRCNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 LIKE vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on lpad(BCNDM.SIBS_BANK_CODE, 5, '0') =
               lpad(A.SOURCE_BRANCH, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.TellerID,
               BCNDM.SIBS_BANK_CODE || '-' || BCNDM.BRAN_NAME AS SRCNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 LIKE vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on lpad(BCNDM.SIBS_BANK_CODE, 5, '0') =
               lpad(A.SOURCE_BRANCH, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.Tellerid,
               BCNDM.SIBS_BANK_CODE || '-' || BCNDM.BRAN_NAME AS SRCNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 LIKE vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on lpad(BCNDM.SIBS_BANK_CODE, 5, '0') =
               lpad(A.SOURCE_BRANCH, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.Tellerid,
               BCNDM.SIBS_BANK_CODE || '-' || BCNDM.BRAN_NAME AS SRCNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 LIKE vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on lpad(BCNDM.SIBS_BANK_CODE, 5, '0') =
               lpad(A.SOURCE_BRANCH, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.Tellerid,
               BCNDM.SIBS_BANK_CODE || '-' || BCNDM.BRAN_NAME AS SRCNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 LIKE vCitad
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BCNDM
            on lpad(BCNDM.SIBS_BANK_CODE, 5, '0') =
               lpad(A.SOURCE_BRANCH, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
    end if;
  
    open RefCurBM_IBPS09 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS09;
  ----------------------------------
  /* BM_IBPS09CN : Bang ke gui lai dien ngay truoc CN
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS09_CN(pDate              IN DATE,
                         pCITAD             IN VARCHAR2, --sender f07  + tad
                         pCCYCD             IN VARCHAR2 Default 'VND',
                         RefCurBM_IBPS09_CN IN OUT PKG_CR.RefCurType) IS
  
    vSender     varchar2(50); --sender 8 so                     
    vCitad      VARCHAR2(10);
    vCcycd      VARCHAR2(10);
    vSrcBranch  varchar2(50);
    vBranchName varchar2(150);
    vOndate     date;
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vSender     := '%';
      vSrcBranch  := '%';
      vBranchName := 'ALL';
    else
      GW_PK_IBPS_REPORT.SPLIT_SENDER(trim(pCitad),
                                     '-',
                                     vSrcBranch,
                                     vSender);
      vSrcBranch := lpad(vSrcBranch, 5, '0');
      SELECT M.Bank_Name
        into vBranchName
        FROM Ibps_Bank_Map M
       where lpad(M.Sibs_Bank_Code, 5, '0') = vSrcBranch
         and M.Gw_Bank_Code = vSender
         and rownum = 1;
    
    end if;
    /*
    if upper(trim(pCitad)) = 'ALL'  or trim(pCitad) is null then
      vCitad := '%';
    else
      vCitad := trim(pCitad);
    end if;*/
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      DELETE FROM BM_IBPS02_TEMP;
      --Du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BrNAme,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.Tellerid,
               BCNDM.Gw_Bank_Code || '-' || BCNDM.Tad_Name AS TADNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND C.ccycd LIKE vCcycd
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')
                      --AND C.f07 like vCitad
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =
        --                          substr(A.TAD, 3, 3)
          LEFT JOIN TAD BCNDM
            ON lpad(BCNDM.Sibs_Code, 5, '0') = lpad(A.TAD, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName,
               A.TellerID,
               BCNDM.Gw_Bank_Code || '-' || BCNDM.Tad_Name AS TADNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')
                      --AND C.f07 like vCitad
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =
        --                          substr(A.TAD, 3, 3)
          LEFT JOIN TAD BCNDM
            ON lpad(BCNDM.Sibs_Code, 5, '0') = lpad(A.TAD, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName AS BRNAME,
               A.TellerID,
               BCNDM.Gw_Bank_Code || '-' || BCNDM.Tad_Name AS TADNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')
                      --AND C.f07 like vCitad
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =
        --                          substr(A.TAD, 3, 3)
          LEFT JOIN TAD BCNDM
            ON lpad(BCNDM.Sibs_Code, 5, '0') = lpad(A.TAD, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName AS BRNAME,
               A.Tellerid,
               BCNDM.Gw_Bank_Code || '-' || BCNDM.Tad_Name AS TADNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')
                      --AND C.f07 like vCitad
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =
        --                          substr(A.TAD, 3, 3)
          LEFT JOIN TAD BCNDM
            ON lpad(BCNDM.Sibs_Code, 5, '0') = lpad(A.TAD, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --order by tad,RM_NUMBER asc;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName AS BRNAME,
               A.Tellerid,
               BCNDM.Gw_Bank_Code || '-' || BCNDM.Tad_Name AS TADNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')
                      --AND C.f07 like vCitad
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =
        --                          substr(A.TAD, 3, 3)
          LEFT JOIN TAD BCNDM
            ON lpad(BCNDM.Sibs_Code, 5, '0') = lpad(A.TAD, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               vBranchName AS BRNAME,
               A.Tellerid,
               BCNDM.Gw_Bank_Code || '-' || BCNDM.Tad_Name AS TADNAME,
               SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6
                   AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN ('8')
                      --AND C.f07 like vCitad
                   AND C.F07 LIKE vSender
                   AND lpad(C.Source_Branch, 5, '0') LIKE vSrcBranch) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =
        --                          substr(A.TAD, 3, 3)
          LEFT JOIN TAD BCNDM
            ON lpad(BCNDM.Sibs_Code, 5, '0') = lpad(A.TAD, 5, '0')
         order by A.Trans_Code, RECEIVER asc;
    end if;
  
    open RefCurBM_IBPS09_CN for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS09_CN;
  ----------------------------------
  /* BM_IBPS10 : Bang ke ket qua doi chieu dien IBPS
     Creator: Bùi H?ng Phuong
  */
  PROCEDURE BM_IBPS10(pdate           IN date,
                      pCcycd          in varchar2 default 'VND',
                      pCitad          in varchar2,
                      RefCurBM_IBPS10 IN OUT PKG_CR.RefCurType) IS
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vBranchName varchar2(200);
  BEGIN
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      -- lay ma gia 3 so cong tad de so sanh dieu kien    
      select lpad(T.Sibs_Code, 5, '0')
        into vCitad
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
      --vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || ' - ' || T.TAD_NAME
        into vBranchName
        from TAD T
       WHERE T.GW_BANK_CODE = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
  
    open RefCurBM_IBPS10 for
      SELECT A.msg_id,
             A.rm_number,
             A.sender,
             BSEND.BANK_NAME  AS SENDERNAME,
             A.receiver,
             BRECV.BANK_NAME  AS RECEINAME,
             A.amount,
             A.ccy,
             A.trans_date,
             A.exception_type,
             A.msg_direction,
             B.Content        AS Status,
             --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, CH.F21,CA.F21),C.F21 ) AS TAD,
             decode(trim(A.Tad), NULL, ' ', to_char(to_number(A.tad))) AS TAD,
             --substr(A.tad, -3) AS TAD,
             T.GW_BANK_CODE || ' - ' || T.TAD_NAME AS TADNAME,
             GW_PK_IBPS_REPORT.IBPS_GET_Field(decode(C.Query_Id,
                                                     NULL,
                                                     decode(CA.Query_Id,
                                                            NULL,
                                                            CH.Content,
                                                            CA.Content),
                                                     C.Content),
                                              '011') AS SOGD,
             --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, CH.Gw_Trans_Num,CA.Gw_Trans_Num),C.GW_TRANS_NUM) AS GW_TRANS_NUM,
             to_number(substr(A.K1, -8)) AS GW_TRANS_NUM,
             decode(C.Query_Id,
                    NULL,
                    decode(CA.Query_Id, NULL, CH.Trans_Code, CA.Trans_Code),
                    C.Trans_Code) AS TRANS_CODE,
             decode(C.Query_Id,
                    NULL,
                    decode(CA.Query_Id,
                           NULL,
                           CH.SOURCE_BRANCH,
                           CA.SOURCE_BRANCH),
                    C.SOURCE_BRANCH) AS SOURCE_BRANCH,
             vBranchName AS SRC_BRANCHNAME,
             decode(C.Query_Id,
                    NULL,
                    decode(CA.Query_Id, NULL, CH.MSG_SRC, CA.MSG_SRC),
                    C.MSG_SRC) AS MSG_SRC
        FROM IBPS_MSG_REC A
        LEFT JOIN IBPS_MSG_CONTENT C
          ON A.Query_Id = C.Query_Id
        LEFT JOIN IBPS_MSG_ALL CA
          ON CA.Query_Id = A.Query_Id
        LEFT JOIN IBPS_MSG_ALL_HIS CH
          ON A.Query_Id = CH.Query_Id
        LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
          ON trim(A.STATUS) = trim(B.STATUS)
        LEFT JOIN IBPS_BANK_MAP BSEND
          on BSEND.gw_bank_code = A.SENDER
         AND BSEND.Sibs_Bank_Code =
             decode(BSEND.Sibs_Bank_Code,
                    -1,
                    -1,
                    lpad(C.Source_Branch, 5, '0'))
        LEFT JOIN IBPS_BANK_MAP BRECV
          on BRECV.gw_bank_code = A.RECEIVER
         AND BRECV.Sibs_Bank_Code =
             decode(BRECV.Sibs_Bank_Code,
                    -1,
                    -1,
                    lpad(C.Source_Branch, 5, '0'))
        LEFT JOIN TAD T
          ON lpad(A.TAD, 5, '0') = lpad(T.Sibs_Code, 5, '0')
       where A.Ndate = to_char(pDate, 'YYYYMMDD')
            --to_char(A.Trans_Date,'YYYYMMDD')=to_char(pDate,'YYYYMMDD')
         AND decode(trim(A.tad), NULL, '%', lpad(A.Tad, 5, '0')) like
             vCitad
            --AND decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, decode(CH.F21,NULL,'%',CH.F21),CA.F21),C.F21 ) LIKE '%'
         AND nvl(A.CCY, '%') LIKE vCcycd
       ORDER BY GW_TRANS_NUM;
    /*LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21 
    and T.GW_BANK_CODE NOT in
     (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )*/
  END BM_IBPS10;
  -------------------------
  /*  BM_IBPS11: Bang ket qua thanh toan
      Creator: Bui Hong Phuong
      Date:21/12/2010      
  */
  PROCEDURE BM_IBPS11(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pTrans_code     in varchar2,
                      RefCurBM_IBPS11 IN OUT PKG_CR.RefCurType) IS
    vCitad      varchar2(50);
    vBranchName varchar2(200);
    vCcycd      varchar2(3);
    vTrans_code varchar2(50);
  begin
  
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select M.BANK_NAME
        into vBranchName
        from ibps_bank_map M
       where M.Gw_Bank_Code = vCitad
         and rownum = 1;
    end if;
    -- uncomment if IBPS channel does not use only VND
    /* if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then  
       vCcycd := '%';  
    else
      vCcycd := trim(pCcycd);
    end if;*/
  
    -- comment if IBPS channel does not use only VND
    -- begin
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := 'VND';
    else
      vCcycd := trim(pCcycd);
    end if;
    -- end
    if upper(trim(pTrans_code)) = 'ALL' or trim(pTrans_code) is null then
      vTrans_code := '%';
    else
      vTrans_code := trim(pTrans_code);
    end if;
  
    delete from BM_IBPS02_TEMP;
    insert into BM_IBPS02_TEMP
      (MSG_ID,
       QUERY_ID,
       SENDER,
       AMOUNT,
       TRANS_DATE,
       SOURCE_BRANCH,
       TAD,
       PRE_TAD,
       CCYCD,
       Tran_Code,
       Sendername,
       Msg_Direction,
       BRNAME)
      SELECT A.MSG_ID,
             A.QUERY_ID,
             A.TADGATE,
             --               A.RECEIVER,
             A.AMOUNT,
             A.TRANS_DATE,
             A.SOURCE_BRANCH,
             A.TAD,
             A.PRE_TAD,
             A.CCYCD,
             A.Trans_Code,
             TF21.Bank_Name AS TADGATENAME, --F21 Name                   
             A.MSG_DIRECTION,
             vBranchName
        FROM (SELECT C.MSG_ID,
                     C.QUERY_ID,
                     decode(C.msg_direction, 'SIBS-IBPS', C.F21, C.F22) AS TADGATE,
                     --                       C.F22 AS RECEIVER,
                     C.AMOUNT,
                     C.TRANS_DATE,
                     C.SOURCE_BRANCH,
                     C.TAD,
                     C.PRE_TAD,
                     C.CCYCD,
                     C.Trans_Code,
                     C.MSG_DIRECTION
                FROM IBPS_MSG_CONTENT C
               WHERE C.TRANSDATE = to_char(pdate, 'YYYYMMDD')
                 AND nvl(C.ccycd, '%') LIKE vCcycd
                 AND (C.F21 like vCitad OR C.F22 like vCitad)
                 AND C.Trans_Code like vTrans_code
                 AND C.status IN (1, 0, -1)) A
        LEFT JOIN IBPS_BANK_MAP TF21
          ON TF21.Gw_Bank_Code = A.TADGATE
         AND TF21.SIBS_BANK_CODE <> -1
         AND lpad(A.TAD, 5, '0') = lpad(TF21.SIBS_BANK_CODE, 5, '0');
  
    insert into BM_IBPS02_TEMP
      (MSG_ID,
       QUERY_ID,
       SENDER,
       AMOUNT,
       TRANS_DATE,
       SOURCE_BRANCH,
       TAD,
       PRE_TAD,
       CCYCD,
       Tran_Code,
       Sendername,
       Msg_Direction,
       BRNAME)
      SELECT A.MSG_ID,
             A.QUERY_ID,
             A.TADGATE,
             --               A.RECEIVER,
             A.AMOUNT,
             A.TRANS_DATE,
             A.SOURCE_BRANCH,
             A.TAD,
             A.PRE_TAD,
             A.CCYCD,
             A.Trans_Code,
             TF21.Bank_Name AS TADGATENAME, --F21 Name               
             A.MSG_DIRECTION,
             vBranchName
        FROM (SELECT C.MSG_ID,
                     C.QUERY_ID,
                     decode(C.msg_direction, 'SIBS-IBPS', C.F21, C.F22) AS TADGATE,
                     --                       C.F22 AS RECEIVER,
                     C.AMOUNT,
                     C.TRANS_DATE,
                     C.SOURCE_BRANCH,
                     C.TAD,
                     C.PRE_TAD,
                     C.CCYCD,
                     C.Trans_Code,
                     C.MSG_DIRECTION
                FROM IBPS_MSG_ALL C
               WHERE C.TRANSDATE = to_char(pdate, 'YYYYMMDD')
                 AND nvl(C.ccycd, '%') LIKE vCcycd
                 AND (C.F21 like vCitad OR C.F22 like vCitad)
                 AND C.Trans_Code like vTrans_code
                 AND C.status IN (1, 0, -1)) A
        LEFT JOIN IBPS_BANK_MAP TF21
          ON TF21.Gw_Bank_Code = A.TADGATE
         AND TF21.SIBS_BANK_CODE <> -1
         AND lpad(A.TAD, 5, '0') = lpad(TF21.SIBS_BANK_CODE, 5, '0');
  
    insert into BM_IBPS02_TEMP
      (MSG_ID,
       QUERY_ID,
       SENDER,
       AMOUNT,
       TRANS_DATE,
       SOURCE_BRANCH,
       TAD,
       PRE_TAD,
       CCYCD,
       Tran_Code,
       Sendername,
       Msg_Direction,
       BRNAME)
      SELECT A.MSG_ID,
             A.QUERY_ID,
             A.TADGATE,
             --               A.RECEIVER,
             A.AMOUNT,
             A.TRANS_DATE,
             A.SOURCE_BRANCH,
             A.TAD,
             A.PRE_TAD,
             A.CCYCD,
             A.Trans_Code,
             TF21.Bank_Name AS TADGATENAME, --F21 Name               
             A.MSG_DIRECTION,
             vBranchName
        FROM (SELECT C.MSG_ID,
                     C.QUERY_ID,
                     decode(C.msg_direction, 'SIBS-IBPS', C.F21, C.F22) AS TADGATE,
                     --                       C.F22 AS RECEIVER,
                     C.AMOUNT,
                     C.TRANS_DATE,
                     C.SOURCE_BRANCH,
                     C.TAD,
                     C.PRE_TAD,
                     C.CCYCD,
                     C.Trans_Code,
                     C.MSG_DIRECTION
                FROM IBPS_MSG_ALL_HIS C
               WHERE C.TRANSDATE = to_char(pdate, 'YYYYMMDD')
                 AND nvl(C.ccycd, '%') LIKE vCcycd
                 AND (C.F21 like vCitad OR C.F22 like vCitad)
                 AND C.Trans_Code like vTrans_code
                 AND C.status IN (1, 0, -1)) A
        LEFT JOIN IBPS_BANK_MAP TF21
          ON TF21.Gw_Bank_Code = A.TADGATE
         AND TF21.SIBS_BANK_CODE <> -1
         AND lpad(A.TAD, 5, '0') = lpad(TF21.SIBS_BANK_CODE, 5, '0');
  
    delete from BM_IBPS11_Temp;
    insert into BM_IBPS11_Temp
      (TAD,
       TADNAME,
       I_MSGCOUNT,
       I_TOTAL,
       O_MSGCOUNT,
       O_TOTAL,
       TRANSCODE,
       BRNAME)
      SELECT A.SENDER,
             A.Sendername,
             COUNT(I_COUNT),
             SUM(I_SUM),
             COUNT(O_COUNT),
             SUM(O_SUM),
             pTrans_code,
             vBranchName
        FROM (SELECT R.SENDER,
                     R.Sendername,
                     decode(R.Msg_Direction, 'IBPS-SIBS', R.MSG_ID, null) AS I_COUNT,
                     decode(R.Msg_Direction, 'IBPS-SIBS', R.Amount, 0) AS I_SUM,
                     decode(R.Msg_Direction, 'SIBS-IBPS', R.MSG_ID, null) AS O_COUNT,
                     decode(R.Msg_Direction, 'SIBS-IBPS', R.Amount, 0) AS O_SUM
                FROM BM_IBPS02_TEMP R) A
       GROUP BY A.SENDER, A.Sendername;
  
    open RefCurBM_IBPS11 for
      select * from BM_IBPS11_TEMP;
  
  end BM_IBPS11;

  -------------------------------------------------------
  PROCEDURE BM_IBPS13(pdate           IN date,
                      pCcycd          in varchar2 default 'VND',
                      pCitad          in varchar2,
                      RefCurBM_IBPS13 IN OUT PKG_CR.RefCurType) IS
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vBranchName varchar2(200);
  BEGIN
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      -- lay ma gia 3 so cong tad de so sanh dieu kien    
      select lpad(T.Sibs_Code, 5, '0')
        into vCitad
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
      --vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || ' - ' || T.TAD_NAME
        into vBranchName
        from TAD T
       WHERE T.GW_BANK_CODE = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
  
    open RefCurBM_IBPS13 for
      select *
        from (SELECT A.msg_id,
                     A.rm_number,
                     A.sender,
                     BSEND.BANK_NAME  AS SENDERNAME,
                     A.receiver,
                     BRECV.BANK_NAME  AS RECEINAME,
                     A.amount,
                     A.ccy,
                     A.trans_date,
                     A.exception_type,
                     A.msg_direction,
                     B.Content        AS Status,
                     --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, CH.F21,CA.F21),C.F21 ) AS TAD,
                     decode(trim(A.Tad),
                            NULL,
                            ' ',
                            to_char(to_number(A.tad))) AS TAD,
                     --substr(A.tad, -3) AS TAD,
                     T.GW_BANK_CODE || ' - ' || T.TAD_NAME AS TADNAME,
                     GW_PK_IBPS_REPORT.IBPS_GET_Field(decode(C.Query_Id,
                                                             NULL,
                                                             decode(CA.Query_Id,
                                                                    NULL,
                                                                    CH.Content,
                                                                    CA.Content),
                                                             C.Content),
                                                      '011') AS SOGD,
                     --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, CH.Gw_Trans_Num,CA.Gw_Trans_Num),C.GW_TRANS_NUM) AS GW_TRANS_NUM,
                     to_number(substr(A.K1, -8)) AS GW_TRANS_NUM,
                     decode(C.Query_Id,
                            NULL,
                            decode(CA.Query_Id,
                                   NULL,
                                   CH.Trans_Code,
                                   CA.Trans_Code),
                            C.Trans_Code) AS TRANS_CODE,
                     decode(C.Query_Id,
                            NULL,
                            decode(CA.Query_Id,
                                   NULL,
                                   CH.SOURCE_BRANCH,
                                   CA.SOURCE_BRANCH),
                            C.SOURCE_BRANCH) AS SOURCE_BRANCH,
                     vBranchName AS SRC_BRANCHNAME,
                     decode(C.Query_Id,
                            NULL,
                            decode(CA.Query_Id, NULL, CH.MSG_SRC, CA.MSG_SRC),
                            C.MSG_SRC) AS MSG_SRC
                FROM IBPS_MSG_REC A
                LEFT JOIN IBPS_MSG_CONTENT C
                  ON A.Query_Id = C.Query_Id
                LEFT JOIN IBPS_MSG_ALL CA
                  ON CA.Query_Id = A.Query_Id
                LEFT JOIN IBPS_MSG_ALL_HIS CH
                  ON A.Query_Id = CH.Query_Id
                LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
                  ON trim(A.STATUS) = trim(B.STATUS)
                LEFT JOIN IBPS_BANK_MAP BSEND
                  on BSEND.gw_bank_code = A.SENDER
                 AND BSEND.Sibs_Bank_Code =
                     decode(BSEND.Sibs_Bank_Code,
                            -1,
                            -1,
                            lpad(C.Source_Branch, 5, '0'))
                LEFT JOIN IBPS_BANK_MAP BRECV
                  on BRECV.gw_bank_code = A.RECEIVER
                 AND BRECV.Sibs_Bank_Code =
                     decode(BRECV.Sibs_Bank_Code,
                            -1,
                            -1,
                            lpad(C.Source_Branch, 5, '0'))
                LEFT JOIN TAD T
                  ON lpad(A.TAD, 5, '0') = lpad(T.Sibs_Code, 5, '0')
               where A.Ndate = to_char(pDate, 'YYYYMMDD')
                    --to_char(A.Trans_Date,'YYYYMMDD')=to_char(pDate,'YYYYMMDD')
                 AND decode(trim(A.tad), NULL, '%', lpad(A.Tad, 5, '0')) like
                     vCitad
                    --AND decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, decode(CH.F21,NULL,'%',CH.F21),CA.F21),C.F21 ) LIKE '%'
                 AND nvl(A.CCY, '%') LIKE vCcycd) R
       where R.GW_TRANS_NUM like '6%'
         and length(R.GW_TRANS_NUM) = 5;
    /*LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21 
    and T.GW_BANK_CODE NOT in
     (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )*/
  END BM_IBPS13;

  -------------------------------------------------------------------

  PROCEDURE BM_IBPS17(pDate           in date,
                      pCitadOld       in varchar2,
                      pCitadNew       in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      RefCurBM_IBPS17 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitadOld varchar2(50);
    vCitadNew varchar2(50);
    vCcycd    varchar2(3);
    vOndate   date;
  
  BEGIN
    select sysdate into vOndate from dual;
    if upper(trim(pCitadOld)) = 'ALL' then
      vCitadOld := '%';
    else
      vCitadOld := trim(lpad(pCitadOld, 5, 0));
    end if;
    if upper(trim(pCitadNew)) = 'ALL' then
      vCitadNew := '%';
    else
      vCitadNew := trim(lpad(pCitadNew, 5, 0));
    end if;
    if upper(trim(pCcycd)) = 'ALL' then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if to_char(pDate, 'DD/MM/YYYY') = to_char(vOndate, 'DD/MM/YYYY') then
      --    open RefCurBM_IBPS17 for
      delete from bm_ibps17_temp;
      insert into bm_ibps17_temp
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         TRANS_CODE,
         RECEIVER,
         sender,
         TRANS_DATE,
         SOURCE_BRANCH,
         tad,
         pre_tad,
         rm_number,
         amount,
         ccycd,
         pretran_code,
         pretrans_num)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               GW_PK_LIB.GET_IBPS_Field(A.content, '011') AS GW_TRANS_NUM,
               A.TRANS_CODE,
               A.F22 AS RECEIVER,
               A.F07 as SENDER,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.tad,
               A.pre_tad,
               a.rm_number,
               a.amount,
               a.ccycd,
               a.pretran_code,
               a.pretrans_num
          FROM IBPS_MSG_CONTENT A
         where trim(A.MSG_DIRECTION) = 'SIBS-IBPS'
           AND A.PRE_TAD IS NOT NULL
           and A.PRE_TAD like vCitadOld
           and a.tad like vCitadNew
           and a.ccycd like vCcycd
           AND A.TRANSDATE = To_char(pDATE, 'YYYYMMDD');
    elsif substr(pDate, 4, 2) = substr(vOndate, 4, 2) then
      --   open RefCurBM_IBPS17 for
      delete from bm_ibps17_temp;
      --Lay du lieu trong ngay
      insert into bm_ibps17_temp
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         TRANS_CODE,
         RECEIVER,
         sender,
         TRANS_DATE,
         SOURCE_BRANCH,
         tad,
         pre_tad,
         rm_number,
         amount,
         ccycd,
         pretran_code,
         pretrans_num)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               GW_PK_LIB.GET_IBPS_Field(A.content, '011') AS GW_TRANS_NUM,
               A.TRANS_CODE,
               A.F22 AS RECEIVER,
               A.F07 as SENDER,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.tad,
               A.pre_tad,
               a.rm_number,
               a.amount,
               a.ccycd,
               a.pretran_code,
               a.pretrans_num
          FROM IBPS_MSG_CONTENT A
         where trim(A.MSG_DIRECTION) = 'SIBS-IBPS'
           AND A.PRE_TAD IS NOT NULL
           and A.PRE_TAD like vCitadOld
           and a.tad like vCitadNew
           and a.ccycd like vCcycd
           AND A.TRANSDATE = To_char(pDATE, 'YYYYMMDD');
      --Lay du lieu trong thang
      insert into bm_ibps17_temp
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         TRANS_CODE,
         RECEIVER,
         sender,
         TRANS_DATE,
         SOURCE_BRANCH,
         tad,
         pre_tad,
         rm_number,
         amount,
         ccycd,
         pretran_code,
         pretrans_num)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               GW_PK_LIB.GET_IBPS_Field(A.content, '011') AS GW_TRANS_NUM,
               A.TRANS_CODE,
               A.F22 AS RECEIVER,
               A.F07 as SENDER,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.tad,
               A.pre_tad,
               a.rm_number,
               a.amount,
               a.ccycd,
               a.pretran_code,
               a.pretrans_num
          FROM IBPS_MSG_ALL A
         where trim(A.MSG_DIRECTION) = 'SIBS-IBPS'
           AND A.PRE_TAD IS NOT NULL
           and A.PRE_TAD like vCitadOld
           and a.tad like vCitadNew
           and a.ccycd like vCcycd
           AND A.TRANSDATE = To_char(pDATE, 'YYYYMMDD');
    else
      --   c
      delete from bm_ibps17_temp;
      --Lay du lieu trong ngay
      insert into bm_ibps17_temp
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         TRANS_CODE,
         RECEIVER,
         sender,
         TRANS_DATE,
         SOURCE_BRANCH,
         tad,
         pre_tad,
         rm_number,
         amount,
         ccycd,
         pretran_code,
         pretrans_num)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               GW_PK_LIB.GET_IBPS_Field(A.content, '011') AS GW_TRANS_NUM,
               A.TRANS_CODE,
               A.F22 AS RECEIVER,
               A.F07 as SENDER,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.tad,
               A.pre_tad,
               a.rm_number,
               a.amount,
               a.ccycd,
               a.pretran_code,
               a.pretrans_num
          FROM IBPS_MSG_CONTENT A
         where trim(A.MSG_DIRECTION) = 'SIBS-IBPS'
           AND A.PRE_TAD IS NOT NULL
           and A.PRE_TAD like vCitadOld
           and a.tad like vCitadNew
           and a.ccycd like vCcycd
           AND A.TRANSDATE = To_char(pDATE, 'YYYYMMDD');
      --Lay du lieu trong thang
      insert into bm_ibps17_temp
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         TRANS_CODE,
         RECEIVER,
         sender,
         TRANS_DATE,
         SOURCE_BRANCH,
         tad,
         pre_tad,
         rm_number,
         amount,
         ccycd,
         pretran_code,
         pretrans_num)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               GW_PK_LIB.GET_IBPS_Field(A.content, '011') AS GW_TRANS_NUM,
               A.TRANS_CODE,
               A.F22 AS RECEIVER,
               A.f07 as SENDER,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.tad,
               A.pre_tad,
               a.rm_number,
               a.amount,
               a.ccycd,
               a.pretran_code,
               a.pretrans_num
          FROM IBPS_MSG_ALL A
         where trim(A.MSG_DIRECTION) = 'SIBS-IBPS'
           AND A.PRE_TAD IS NOT NULL
           and A.PRE_TAD like vCitadOld
           and a.tad like vCitadNew
           and a.ccycd like vCcycd
           AND A.TRANSDATE = To_char(pDATE, 'YYYYMMDD');
      --Lay du lieu trong nam
      insert into bm_ibps17_temp
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         TRANS_CODE,
         RECEIVER,
         sender,
         TRANS_DATE,
         SOURCE_BRANCH,
         tad,
         pre_tad,
         rm_number,
         amount,
         ccycd,
         pretran_code,
         pretrans_num)
        SELECT A.MSG_ID,
               A.QUERY_ID,
               GW_PK_LIB.GET_IBPS_Field(A.content, '011') AS GW_TRANS_NUM,
               A.TRANS_CODE,
               A.F22 AS RECEIVER,
               A.f07 as SENDER,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.tad,
               A.pre_tad,
               a.rm_number,
               a.amount,
               a.ccycd,
               a.pretran_code,
               a.pretrans_num
          FROM IBPS_MSG_ALL_HIS A
         where trim(A.MSG_DIRECTION) = 'SIBS-IBPS'
           AND A.PRE_TAD IS NOT NULL
           and A.PRE_TAD like vCitadOld
           and a.tad like vCitadNew
           and a.ccycd like vCcycd
           AND A.TRANSDATE = To_char(pDATE, 'YYYYMMDD');
    end if;
    open RefCurBM_IBPS17 for
      select * from bm_ibps17_temp;
  END BM_IBPS17;

  -------------------------
  /* BM_IBPS18 : Bang ke dien EBANK di CITAD trong ngay
     Creator: Bùi H?ng Phuong
     Date: 27/10/2011
  */

  PROCEDURE BM_IBPS18(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS18 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vStatus     varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS18 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS18;

 -------------------------
  /* BM_IBPS18 : Bang ke dien EBANK di CITAD trong ngay
     Creator: Bùi H?ng Phuong
     Date: 27/10/2011
  */

  PROCEDURE BM_IBPS18_2(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS18_2 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vStatus     varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD04 -- is resent
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
                decode(length(A.GW_TRANS_NUM), 6, 
                decode(substr(A.GW_TRANS_NUM, 1, 2), '85', 'Y','N'),'N') AS IS_RESENT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD04 -- is resent
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
                           decode(length(A.GW_TRANS_NUM), 6, 
                decode(substr(A.GW_TRANS_NUM, 1, 2), '85', 'Y','N'),'N') AS IS_RESENT
                         FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD04 -- is resent
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
                               decode(length(A.GW_TRANS_NUM), 6, 
                decode(substr(A.GW_TRANS_NUM, 1, 2), '85', 'Y','N'),'N') AS IS_RESENT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD04 -- is resent
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
                               decode(length(A.GW_TRANS_NUM), 6, 
                decode(substr(A.GW_TRANS_NUM, 1, 2), '85', 'Y','N'),'N') AS IS_RESENT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD04 -- is resent
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
                               decode(length(A.GW_TRANS_NUM), 6, 
                decode(substr(A.GW_TRANS_NUM, 1, 2), '85', 'Y','N'),'N') AS IS_RESENT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD04 -- is resent
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
                decode(length(A.GW_TRANS_NUM), 6, 
                decode(substr(A.GW_TRANS_NUM, 1, 2), '85', 'Y','N'),'N') AS IS_RESENT
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS18_2 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS18_2;
 
  -----------------------
  /* BM_IBPS19 : IBPS_BM_IBPS01 Bang ke dien EBANK di sau gio cut-off-time
     Creator: Bùi H?ng Phuong
     Date: 27/10/2011
  */
  PROCEDURE BM_IBPS19(pDate           in date,
                      pBranchCreate   in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS19 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vBranchCreate varchar2(50);
    vCcycd        varchar2(3);
    vStatus       varchar2(50);
    vOndate       date;
    vBranchName   varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pBranchCreate)) = 'ALL' or trim(pBranchCreate) is null then
      vBranchCreate := '%';
      vBranchName   := 'ALL';
    else
      vBranchCreate := trim(pBranchCreate);
      select substr(T.SIBS_BANK_CODE, -3) || '-' || T.BRAN_NAME
        into vBranchName
        from branch T
       where T.SIBS_BANK_CODE = pBranchCreate
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         AMOUNT,
         TRANS_DATE,
         STATUS,
         CCYCD,
         BRNAME, -- branch create name       
         EXTFIELD04 --branch create id
         )
        select MSGID,
               MSGID,
               O.RM_NUMBER,
               O.AMOUNT,
               O.TRANSDATE,
               '',
               O.CCYCD,
               vBranchName,
               O.Branchcreate
          from ibps_sibs_ol2 O
         where vStatus = '%'
           and to_char(O.Transdate, 'YYYYMMDD') =
               to_char(pDate, 'YYYYMMDD')
           and to_char(O.Transdate, 'YYYYMMDD') <>
               nvl(trim(O.Senddate), ' ')
           and (nvl(trim(O.Senddate), ' ') = ' ')
           and O.Status in (0, 1, -1)
           and lpad(O.Branchcreate, 3, '0') like vBranchCreate
           and O.Attribute5 = 'IB'
           AND nvl(O.ccycd, '%') LIKE vCcycd;
    
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
    
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         AMOUNT,
         TRANS_DATE,
         STATUS,
         CCYCD,
         BRNAME, -- branch create name       
         EXTFIELD04 --branch create id
         )
        select MSGID,
               MSGID,
               O.RM_NUMBER,
               O.AMOUNT,
               O.TRANSDATE,
               '',
               O.CCYCD,
               vBranchName,
               O.Branchcreate
          from ibps_sibs_ol2 O
         where vStatus = '%'
           and to_char(O.Transdate, 'YYYYMMDD') =
               to_char(pDate, 'YYYYMMDD')
           and to_char(O.Transdate, 'YYYYMMDD') <>
               nvl(trim(O.Senddate), ' ')
           and (nvl(trim(O.Senddate), ' ') = ' ')
           and O.Status in (0, 1, -1)
           and lpad(O.Branchcreate, 3, '0') like vBranchCreate
           and O.Attribute5 = 'IB'
           AND nvl(O.ccycd, '%') LIKE vCcycd;
    
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         RM_NUMBER,
         AMOUNT,
         TRANS_DATE,
         STATUS,
         CCYCD,
         BRNAME, -- branch create name       
         EXTFIELD04 --branch create id
         )
        select MSGID,
               MSGID,
               O.RM_NUMBER,
               O.AMOUNT,
               O.TRANSDATE,
               '',
               O.CCYCD,
               vBranchName,
               O.Branchcreate
          from ibps_sibs_ol2 O
         where vStatus = '%'
           and to_char(O.Transdate, 'YYYYMMDD') =
               to_char(pDate, 'YYYYMMDD')
           and to_char(O.Transdate, 'YYYYMMDD') <>
               nvl(trim(O.Senddate), ' ')
           and (nvl(trim(O.Senddate), ' ') = ' ')
           and O.Status in (0, 1, -1)
           and lpad(O.Branchcreate, 3, '0') like vBranchCreate
           and O.Attribute5 = 'IB'
           AND nvl(O.ccycd, '%') LIKE vCcycd;
    
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL_HIS C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    end if;
    open RefCurBM_IBPS19 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS19;

  -----------------------
  /* BM_IBPS19_2 : IBPS_BM_IBPS01 Bang ke dien EBANK di sau gio cut-off-time 
     Add: Transdate between [from date] and [to date]
     Creator: Bùi H?ng Phuong
     Date: 15/11/2011
  */
  PROCEDURE BM_IBPS19_2(pFromDate         in date,
                        pToDate           in date,
                        pBranchCreate     in varchar2,
                        pCcycd            in varchar2 default 'VND',
                        pStatus           in varchar2,
                        RefCurBM_IBPS19_2 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vBranchCreate varchar2(50);
    vCcycd        varchar2(3);
    vStatus       varchar2(50);
    vOndate       date;
    vBranchName   varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pBranchCreate)) = 'ALL' or trim(pBranchCreate) is null then
      vBranchCreate := '%';
      vBranchName   := 'ALL';
    else
      vBranchCreate :=lpad( trim(pBranchCreate),3,'0');
      select substr(T.SIBS_BANK_CODE, -3) || '-' || T.BRAN_NAME
        into vBranchName
        from branch T
       where T.SIBS_BANK_CODE = pBranchCreate
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
    delete from BM_IBPS02_TEMP;
    -- lay dien chua send
    if (vStatus='%') then
    insert into BM_IBPS02_TEMP
      (MSG_ID,
       QUERY_ID,
       RM_NUMBER,
       AMOUNT,
       TRANS_DATE,
       STATUS,
       CCYCD,
       BRNAME, -- branch create name       
       EXTFIELD04 --branch create id
       )
      select MSGID,
             MSGID,
             O.RM_NUMBER,
             O.AMOUNT,
             O.TRANSDATE,
             '',
             O.CCYCD,
             vBranchName,
             O.Branchcreate
        from ibps_sibs_ol2 O
       where  to_char(O.Transdate,'YYYYMMDD')>=to_char(pFromDate,'YYYYMMDD')
                   and to_char(O.Transdate,'YYYYMMDD')<=to_char(pToDate,'YYYYMMDD')
         and (nvl(trim(O.Senddate), ' ') = ' ')
         and O.Status in (0, 1, -1)
         and lpad(O.Branchcreate, 3, '0') like vBranchCreate
         and O.Attribute5 = 'IB'
         AND nvl(O.ccycd, '%') LIKE vCcycd;
    end if;
    -- lay dien da send       
   /* if to_char(vOndate,'YYYYMMDD')>=to_char(pFromDate,'YYYYMMDD')
                   and to_char(vOndate,'YYYYMMDD')<=to_char(pToDate,'YYYYMMDD')
                    then*/
      -- neu du lieu co ngay hien tai
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04, --branch create id
         EXTFIELD05
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate,
               A.NGUOI_HUONG
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id and C.Msg_Src=5
                 WHERE to_char(O.Transdate,'YYYYMMDD')>=to_char(pFromDate,'YYYYMMDD')
                   and to_char(O.Transdate,'YYYYMMDD')<=to_char(pToDate,'YYYYMMDD')                   
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
   -- end if;
   /* if (to_char(pFromDate, 'YYYYMMDD') < (to_char(vOndate, 'YYYYMMDD'))) then*/
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04, --branch create id
         EXTFIELD05
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate,
               A.NGUOI_HUONG
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id and C.Msg_Src=5
                 WHERE to_char(O.Transdate,'YYYYMMDD')>=to_char(pFromDate,'YYYYMMDD')
                   and to_char(O.Transdate,'YYYYMMDD')<=to_char(pToDate,'YYYYMMDD')
                    and to_char(O.Transdate, 'YYYYMMDD') <>
                    nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
   -- end if;
    if (to_char(pFromDate, 'YYYYMM') < (to_char(vOndate, 'YYYYMM'))) then
      -- lay du lieu nam   
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL_HIS C
                    ON O.Msgid = C.Query_Id and C.Msg_Src=5
                 WHERE to_char(O.Transdate,'YYYYMMDD')>=to_char(pFromDate,'YYYYMMDD')
                   and to_char(O.Transdate,'YYYYMMDD')<=to_char(pToDate,'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    end if;
    open RefCurBM_IBPS19_2 for
      select * from BM_IBPS02_TEMP order by to_char(TRANS_DATE,'YYYYMMDD'),
       GW_TRANS_NUM asc;
  END BM_IBPS19_2;
  ------------------------------------
  /* BM_IBPS20 : Bang ke dien EBANK di CITAD trong ngay cua ngay giao dich
     Creator: Bùi H?ng Phuong
     Date: 14/11/2011
  */

  PROCEDURE BM_IBPS20(pDate           in date,
                      pBranchCreate   in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS20 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vBranchCreate varchar2(50);
    vCcycd        varchar2(3);
    vStatus       varchar2(50);
    vOndate       date;
    vBranchName   varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pBranchCreate)) = 'ALL' or trim(pBranchCreate) is null then
      vBranchCreate := '%';
      vBranchName   := 'ALL';
    else
      vBranchCreate := trim(pBranchCreate);
      select substr(T.SIBS_BANK_CODE, -3) || '-' || T.BRAN_NAME
        into vBranchName
        from branch T
       where T.SIBS_BANK_CODE = pBranchCreate
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL_HIS C
                    ON O.Msgid = C.Query_Id
                 WHERE to_char(O.Transdate, 'YYYYMMDD') =
                       to_char(pDate, 'YYYYMMDD')
                   and O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    end if;
    open RefCurBM_IBPS20 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS20;

  ---------------------------------------------------  
  /* BM_IBPS21 : Bang ke dien EBANK di CITAD trong ngay cua ngay hom truoc
     Creator: Bui Hong Phuong
     Date: 14/11/2011
  */

  PROCEDURE BM_IBPS21(pDate           in date,
                      pBranchCreate   in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS21 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vBranchCreate varchar2(50);
    vCcycd        varchar2(3);
    vStatus       varchar2(50);
    vOndate       date;
    vBranchName   varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pBranchCreate)) = 'ALL' or trim(pBranchCreate) is null then
      vBranchCreate := '%';
      vBranchName   := 'ALL';
    else
      vBranchCreate := trim(pBranchCreate);
      select substr(T.SIBS_BANK_CODE, -3) || '-' || T.BRAN_NAME
        into vBranchName
        from branch T
       where T.SIBS_BANK_CODE = pBranchCreate
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
    
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
    
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id
                 WHERE O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
    
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03 -- pre tad name
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name -- pre tad name
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_CONTENT C
                    ON O.Msgid = C.Query_Id
                 WHERE O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL C
                    ON O.Msgid = C.Query_Id
                 WHERE O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME, -- branch create name
         EXTFIELD01,
         EXTFIELD02,
         EXTFIELD03, -- pre tad name
         EXTFIELD04 --branch create id
         )
        SELECT A.MSGID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.Branchcreate
          FROM (SELECT O.MSGID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(O.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       O.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       O.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       O.tellerid,
                       C.F21,
                       C.F19,
                       lpad(O.Branchcreate, 3, '0') AS BranchCreate
                  FROM IBPS_SIBS_OL2 O
                 inner join IBPS_MSG_ALL_HIS C
                    ON O.Msgid = C.Query_Id
                 WHERE O.Senddate = to_char(pDate, 'YYYYMMDD')
                   and to_char(O.Transdate, 'YYYYMMDD') <>
                       nvl(trim(O.Senddate), ' ')
                   and (nvl(trim(O.Senddate), ' ') <> ' ')
                   and O.Status in (0, 1, -1)
                   and lpad(O.Branchcreate, 3, '0') like vBranchCreate
                   and O.Attribute5 = 'IB'
                   AND nvl(O.ccycd, '%') LIKE vCcycd) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
         WHERE nvl(trim(B.CONTENT), '%') LIKE vStatus;
    end if;
    open RefCurBM_IBPS21 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS21;


-------------------------
  /* BM_IBPS22 : Bang ke chi tiet dien EBANK di CITAD 
     Creator: Bui Hong Phuong
     Date: 06/06/2012
  */

  PROCEDURE BM_IBPS22(pDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pFromRelationNum in varchar2,
                      pToRelationNum in varchar2,
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS22 IN OUT PKG_CR.RefCurType) IS
    --vDate  date;
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vFromRelationNum number(8);
    vToRelationNum number(8);
    vStatus     varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if (trim(pFromRelationNum) is not null) then
      vFromRelationNum:= to_number(pFromRelationNum);      
    end if;
     if (trim(pToRelationNum) is not null) then
      vToRelationNum:= to_number(pToRelationNum);         
    end if;
    
    if to_char(pDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS02 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, --TellerId
         EXTFIELD02, --CUST_SENDER
         EXTFIELD03, -- ACCOUNT_SENDER
         EXTFIELD04, -- CUST_RECEIVER
         EXTFIELD05, --ACCOUNT_RECEIVER
         EXTFIELD06 -- REMARK
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,              
               CUST_SENDER,
               ACCOUNT_SENDER,
               CUST_RECEIVER,
               ACCOUNT_RECEIVER,
               REMARK
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '028') AS CUST_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '030') AS ACCOUNT_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS CUST_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '033') AS ACCOUNT_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '034') AS REMARK,                       
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    elsif substr(to_char(pDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, --TellerId
         EXTFIELD02, --CUST_SENDER
         EXTFIELD03, -- ACCOUNT_SENDER
         EXTFIELD04, -- CUST_RECEIVER
         EXTFIELD05, --ACCOUNT_RECEIVER
         EXTFIELD06 -- REMARK
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
              CUST_SENDER,
               ACCOUNT_SENDER,
               CUST_RECEIVER,
               ACCOUNT_RECEIVER,
               REMARK
                         FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                         GW_PK_LIB.GET_IBPS_Field(C.content, '028') AS CUST_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '030') AS ACCOUNT_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS CUST_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '033') AS ACCOUNT_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '034') AS REMARK,   
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, --TellerId
         EXTFIELD02, --CUST_SENDER
         EXTFIELD03, -- ACCOUNT_SENDER
         EXTFIELD04, -- CUST_RECEIVER
         EXTFIELD05, --ACCOUNT_RECEIVER
         EXTFIELD06 -- REMARK
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               CUST_SENDER,
               ACCOUNT_SENDER,
               CUST_RECEIVER,
               ACCOUNT_RECEIVER,
               REMARK
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                         GW_PK_LIB.GET_IBPS_Field(C.content, '028') AS CUST_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '030') AS ACCOUNT_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS CUST_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '033') AS ACCOUNT_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '034') AS REMARK,   
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, --TellerId
         EXTFIELD02, --CUST_SENDER
         EXTFIELD03, -- ACCOUNT_SENDER
         EXTFIELD04, -- CUST_RECEIVER
         EXTFIELD05, --ACCOUNT_RECEIVER
         EXTFIELD06 -- REMARK
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
                CUST_SENDER,
               ACCOUNT_SENDER,
               CUST_RECEIVER,
               ACCOUNT_RECEIVER,
               REMARK
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                         GW_PK_LIB.GET_IBPS_Field(C.content, '028') AS CUST_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '030') AS ACCOUNT_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS CUST_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '033') AS ACCOUNT_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '034') AS REMARK,   
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, --TellerId
         EXTFIELD02, --CUST_SENDER
         EXTFIELD03, -- ACCOUNT_SENDER
         EXTFIELD04, -- CUST_RECEIVER
         EXTFIELD05, --ACCOUNT_RECEIVER
         EXTFIELD06 -- REMARK
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
              CUST_SENDER,
               ACCOUNT_SENDER,
               CUST_RECEIVER,
               ACCOUNT_RECEIVER,
               REMARK
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                         GW_PK_LIB.GET_IBPS_Field(C.content, '028') AS CUST_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '030') AS ACCOUNT_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS CUST_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '033') AS ACCOUNT_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '034') AS REMARK,   
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01, --TellerId
         EXTFIELD02, --CUST_SENDER
         EXTFIELD03, -- ACCOUNT_SENDER
         EXTFIELD04, -- CUST_RECEIVER
         EXTFIELD05, --ACCOUNT_RECEIVER
         EXTFIELD06 -- REMARK
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               CUST_SENDER,
               ACCOUNT_SENDER,
               CUST_RECEIVER,
               ACCOUNT_RECEIVER,
               REMARK
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                         GW_PK_LIB.GET_IBPS_Field(C.content, '028') AS CUST_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '030') AS ACCOUNT_SENDER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS CUST_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '033') AS ACCOUNT_RECEIVER,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '034') AS REMARK,   
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE = to_char(pDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND C.msg_src = 5
                   and C.product_type = 'OL2'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS22 for
      select * from BM_IBPS02_TEMP 
      where (trim(pFromRelationNum) is null OR (trim(pFromRelationNum) is not null  and (GW_TRANS_NUM) >= vFromRelationNum))
       and (trim(pToRelationNum) is null OR (trim(pToRelationNum) is not null and (GW_TRANS_NUM)<=vToRelationNum))
      order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS22;
 
  ---------------------------------------------------

  PROCEDURE IBPS01(pfrdate      in date,
                   ptodate      in date,
                   RefCurIBPS01 IN OUT PKG_CR.RefCurType) IS
    NumOfDate NUMBER;
    LVMsgIN   NUMBER(38);
    LVMsgOUT  NUMBER(38);
  BEGIN
  
    --insert into test_clob values (TO_DATE(frdate,'DD/MM/YYYY') || '-' || TO_DATE(todate,'DD/MM/YYYY'));
    commit;
  
    ---Tong gia tri dien di gia tri thap
    /* Formatted on 2008/08/10 20:58 (Formatter Plus v4.8.6) */
    SELECT SUM(AMOUNT)
      into LVMsgOUT
      FROM (SELECT amount, msg_id
              FROM ibps_msg_content
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL_HIS
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD'));
  
    ---Tong gia tri dien den gia tri thap
    SELECT SUM(amount)
      into LVMsgIN
      FROM (SELECT amount, msg_id
              FROM ibps_msg_content
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL_HIS
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD'));
  
    NumOfDate := ptodate - pfrdate + 1;
  
    open RefCurIBPS01 for
      select to_char(nvl(LVMsgOUT, 0)) OV,
             to_char(nvl(LVMsgIN, 0)) IV,
             NumOfDate DAYS,
             to_char((LVMsgOUT - nvl(LVMsgIN, 0)) / NumOfDate) as Net
        from dual;
    --    select * from IBPS_MSG_CONTENT;
  END IBPS01;

  -------------------------
  PROCEDURE ibps_print_all(RefCurIBPS_PRINT_ALL IN OUT PKG_CR.RefCurType) IS
  BEGIN
    open RefCurIBPS_PRINT_ALL for
      select * from IBPS_PRINT_TEMP where 1 < 0;
  END IBPS_PRINT_ALL;
  -------------------------

  PROCEDURE IBPS_PRINT_RECONCILE(RefCuribps_print_reconcile IN OUT PKG_CR.RefCurType) IS
  BEGIN
    open RefCuribps_print_reconcile for
      SELECT MSG_ID,
             MSG_DIRECTION,
             REC_TYPE,
             '' as STATUS,
             APP_CODE,
             '' as DP,
             '' as RM_NUMBER,
             '' as REF_NUMBER,
             SENDER,
             RECEIVER,
             AMOUNT,
             CCY,
             TRANS_DATE,
             TAD
        FROM IBPS_MSG_REC
       where 1 < 0;
  END ibps_print_reconcile;
  -------------------------

  PROCEDURE tad_print(RefCurTAD_PRINT IN OUT PKG_CR.RefCurType) IS
  BEGIN
    open RefCurTAD_PRINT for
      select * from TAD_PRINT_LIST where 1 < 0;
  END TAD_PRINT;
  -------------------------

  /**********************************************************************
  Nguoi tao:  QuanLD
  Muc dich: View báo cao in chi tiet dien IBPS
  Ten ham:  GET_IBPS_DTL_VIEWREPORT()
  Tham so:  pMSG_ID number, 
            pcurIBPSDTL out RefCurType
  Mo ta: -  
     Dua vao MSG_ID cua dien lay ra noi dung dien sau do chat ra thanh cac fiedl
  Ngay khoi tao:  13/12/2008
  Nguoi sua:    
  Ngay sua:   
  Mo ta     
  
  **********************************************************************/

  PROCEDURE IBPS01_M(pMSG_ID number, pcurIBPSDTL out PKG_CR.RefCurType) IS
  
    icount    integer;
    v_sql     varchar2(4000);
    pTblTable varchar2(100);
  
  BEGIN
  
    select count(1)
      into icount
      from IBPS_MSG_CONTENT IMC
     where IMC.MSG_ID = pMSG_ID;
    if icount = 0 then
      select count(1)
        into icount
        from Ibps_Msg_All IMC
       where IMC.MSG_ID = pMSG_ID;
      if icount = 0 then
        select count(1)
          into icount
          from Ibps_Msg_All_His IMC
         where IMC.MSG_ID = pMSG_ID;
        if icount <> 0 then
          pTblTable := 'Ibps_Msg_All_His';
        end if;
      else
        pTblTable := 'Ibps_Msg_All';
      end if;
    else
    
      pTblTable := 'IBPS_MSG_CONTENT';
    
    end if;
  
    v_sql := 'select distinct X.query_id,
            X.gw_trans_num,
            decode(X.msg_direction,''SIBS-IBPS'',substr(X.rm_number, 5),
             X.rm_number) rm_number, 
             X.trans_date, 
             X.trans_code, 
             X.sender, 
             X.receiver,
             (select a.bank_name
                   from IBPS_BANK_MAP a
                  where a.gw_bank_code = X.F07
                    and rownum = 1) bank_sender,
                (select a.bank_name
                   from IBPS_BANK_MAP a
                  where a.gw_bank_code = X.F19
                    and rownum = 1) bank_receiver,
                X.amount,
                X.ccycd,
                X.msg_direction,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''028'') as sendname, --028
                GW_PK_LIB.GET_IBPS_Field(X.content, ''029'') as sendaddress,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''030'') as sendacc,
                X.F21 banksend,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''031'') as receiname,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''032'') as receiaddress,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''033'') as receiacc,
                X.F19 BankRecei,
                X.Trans_description as content,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''011'') as buttoan,
                GW_PK_LIB.GET_IBPS_Field(X.content, ''012'') as trandate,
                (select a.bank_name
                   from IBPS_BANK_MAP a
                  where a.gw_bank_code = X.F21
                    and rownum = 1) banksend_name,
                (select a.bank_name
                   from IBPS_BANK_MAP a
                  where a.gw_bank_code = X.F22
                    and rownum = 1) bankreceive_name,
                (select status.name
                   from status
                  where status = X.status
                    and rownum = 1) status
  from (select *
          from (select distinct a.query_id,
                                a.gw_trans_num,
                                a.rm_number,
                                a.trans_date,
                                a.trans_code,
                                (select IBPS_BANK_MAP.GW_BANK_CODE
                                   from IBPS_BANK_MAP
                                  where Trim(IBPS_BANK_MAP.SIBS_BANK_CODE) =
                                        trim(a.TAD) and rownum = 1 ) as SENDER,
                                a.F22 receiver,
                                a.amount,
                                a.ccycd,
                                a.msg_direction,
                                a.status,
                                a.Content,
                                a.f21,
                                a.f22,
                                a.f07,
                                a.f19,
                                a.Trans_description
                  from ' || pTblTable || ' a
                 where trim(a.MSG_ID) = trim(' || pMSG_ID || ')
                   and rownum = 1)
         where rownum = 1) X';
  
    Open pcurIBPSDTL for v_sql;
  
  END IBPS01_M;
  ----------------------

  PROCEDURE BK02(pFromDate   in date,
                 pToDate     in date,
                 pBranchType in number,
                 pFeeType    in number,
                 pBranch     in varchar2,
                 pBranch8    in varchar2,
                 RefCurBK02  IN OUT PKG_CR.RefCurType) IS
    vHV_hardFee    NUMBER;
    vHV_hardFee_2  NUMBER;
    vLV_hardFee    NUMBER;
    vLV_hardFee_2  NUMBER;
    vHV_FeeRate    NUMBER(20, 4);
    vHV_FeeRate_2  NUMBER(20, 4);
    vLV_FeeRate    NUMBER(20, 4);
    vLV_FeeRate_2  NUMBER(20, 4);
    vHV_MinFee     NUMBER;
    vHV_MinFee_2   NUMBER;
    vLV_MinFee     NUMBER;
    vLV_MinFee_2   NUMBER;
    vHV_MaxFee     NUMBER;
    vHV_MaxFee_2   NUMBER;
    vLV_MaxFee     NUMBER;
    vLV_MaxFee_2   NUMBER;
    vHV_TimeFee    date;
    vHV_TimeFee_2  date;
    vLV_TimeFee    date;
    vLV_TimeFee_2  date;
    vStrwhere_HV   varchar(500);
    vStrwhere_HV_2 varchar(500);
    vStrwhere_LV   varchar(500);
    vStrwhere_LV_2 varchar(500);
    strSql         varchar2(10000);
    vHV_TypeFee    varchar(200);
    vHV_TypeFee_2  varchar(200);
    vLV_TypeFee    varchar(200);
    vLV_TypeFee_2  varchar(200);
    vCount_HV      NUMBER;
    vCount_LV      NUMBER;
    vCCYCD_HV      char(3);
    vCCYCD_HV_2    char(3);
    vCCYCD_LV      char(3);
    vCCYCD_LV_2    char(3);
  
    vDCV_hardFee NUMBER;
    vDCV_FeeRate NUMBER(20, 4);
    vDCV_MinFee  NUMBER;
    vDCV_MaxFee  NUMBER;
    vCCYCD_DCV   char(3);
    VSumHV       NUMBER;
    VSumLV       NUMBER;
    VSumDCV      NUMBER;
  
    --vHV_TypeFee_2 varchar(200);
    --vLV_TypeFee_2 varchar(200);
    ----------End-----------
  BEGIN
    select count(*) into vCount_HV from IBPS_Fee where Trans_Type = '1';
    select count(*) into vCount_LV from IBPS_Fee where Trans_Type = '2';
    --Tham so tinh phi gia tri cao
    if (vCount_HV = 1) then
      select FIXED_FEE,
             RATE_Fee,
             Min_Fee,
             Max_Fee,
             FeeDisc_Time,
             FeeDisc_Type,
             CCYCD
        into vHV_hardFee,
             vHV_FeeRate,
             vHV_MinFee,
             vHV_MaxFee,
             vHV_TimeFee,
             vHV_TypeFee,
             vCCYCD_HV
        from IBPS_Fee
       where Trans_Type = '1'
         and rownum = 1;
    else
      select FIXED_FEE,
             RATE_Fee,
             Min_Fee,
             Max_Fee,
             FeeDisc_Time,
             FeeDisc_Type,
             CCYCD
        into vHV_hardFee,
             vHV_FeeRate,
             vHV_MinFee,
             vHV_MaxFee,
             vHV_TimeFee,
             vHV_TypeFee,
             vCCYCD_HV
        from IBPS_Fee
       where Trans_Type = '1'
         and FeeDisc_type = '1'
         and rownum = 1;
      select FIXED_FEE,
             RATE_Fee,
             Min_Fee,
             Max_Fee,
             FeeDisc_Time,
             FeeDisc_Type,
             CCYCD
        into vHV_hardFee_2,
             vHV_FeeRate_2,
             vHV_MinFee_2,
             vHV_MaxFee_2,
             vHV_TimeFee_2,
             vHV_TypeFee_2,
             vCCYCD_HV_2
        from IBPS_Fee
       where Trans_Type = '1'
         and FeeDisc_type = '2'
         and rownum = 1;
    end if;
    --Tham so tinh phi gia tri thap
    if (vCount_LV = 1) then
      select FIXED_FEE,
             RATE_Fee,
             Min_Fee,
             Max_Fee,
             FeeDisc_Time,
             FeeDisc_Type,
             CCYCD
        into vLV_hardFee,
             vLV_FeeRate,
             vLV_MinFee,
             vLV_MaxFee,
             vLV_TimeFee,
             vLV_TypeFee,
             vCCYCD_LV
        from IBPS_Fee
       where Trans_Type = '2'
         and rownum = 1;
    else
      select FIXED_FEE,
             RATE_Fee,
             Min_Fee,
             Max_Fee,
             FeeDisc_Time,
             FeeDisc_Type,
             CCYCD
        into vLV_hardFee,
             vLV_FeeRate,
             vLV_MinFee,
             vLV_MaxFee,
             vLV_TimeFee,
             vLV_TypeFee,
             vCCYCD_LV
        from IBPS_Fee
       where Trans_Type = '2'
         and FeeDisc_type = '1'
         and rownum = 1;
      select FIXED_FEE,
             RATE_Fee,
             Min_Fee,
             Max_Fee,
             FeeDisc_Time,
             FeeDisc_Type,
             CCYCD
        into vLV_hardFee_2,
             vLV_FeeRate_2,
             vLV_MinFee_2,
             vLV_MaxFee_2,
             vLV_TimeFee_2,
             vLV_TypeFee_2,
             vCCYCD_LV_2
        from IBPS_Fee
       where Trans_Type = '2'
         and FeeDisc_type = '2'
         and rownum = 1;
    end if;
    --Tham so tinh phi dieu chuyen von
    select FIXED_FEE, RATE_Fee, Min_Fee, Max_Fee, CCYCD
      into vDCV_hardFee, vDCV_FeeRate, vDCV_MinFee, vDCV_MaxFee, vCCYCD_DCV
      from IBPS_Fee
     where Trans_Type = '3'
       and rownum = 1;
  
    if (vHV_TypeFee = '1') then
      vStrwhere_HV   := '<';
      vStrwhere_HV_2 := '>=';
    end if;
  
    if (vLV_TypeFee = '1') then
      vStrwhere_LV   := '<';
      vStrwhere_LV_2 := '>=';
    end if;
    --XOA BANG TEMP
    delete from ibps_cal_Fee_temp;
    --CHI NHANH TAO DIEN
    IF pBranchType = 1 THEN
      --TAT CA CAC CHI NHANH
      IF pBranch = 'ALL' THEN
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F07,
                 SOURCE_BRANCH,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_content
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F07,
                 SOURCE_BRANCH,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F07,
                 SOURCE_BRANCH,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all_his
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY THEO TUNG CHI NHANH
      ELSE
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F07,
                 SOURCE_BRANCH,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_content
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND F07 = pBranch8
             AND LPAD(SOURCE_BRANCH, 5, '0') = LPAD(pBranch, 5, '0')
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F07,
                 SOURCE_BRANCH,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND F07 = pBranch8
             AND LPAD(SOURCE_BRANCH, 5, '0') = LPAD(pBranch, 5, '0')
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F07,
                 SOURCE_BRANCH,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all_his
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND F07 = pBranch8
             AND LPAD(SOURCE_BRANCH, 5, '0') = LPAD(pBranch, 5, '0')
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
      END IF;
      --CHI NHAN GUI DIEN
    ELSE
      --TAT CA CAC CHI NHANH
      IF pBranch = 'ALL' THEN
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F21,
                 TAD,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_content
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F21,
                 TAD,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F21,
                 TAD,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all_his
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY THEO TUNG CHI NHANH
      ELSE
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F21,
                 TAD,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_content
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)             
             AND F21 = pBranch8
             AND LPAD(TAD, 5, '0') = LPAD(pBranch, 5, '0')
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F21,
                 TAD,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND F21 = pBranch8
             AND LPAD(TAD, 5, '0') = LPAD(pBranch, 5, '0')
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp
          (MSG_ID,
           TRANS_CODE,
           BRANCH8,
           SOURCE_BRANCH,
           AMOUNT,
           TRANS_DATE,
           CONTENT,
           CCYCD)
          select MSG_ID,
                 TRANS_CODE,
                 F21,
                 TAD,
                 NVL(AMOUNT, 0) AS AMOUNT,
                 SENDING_TIME,
                 CONTENT,
                 CCYCD
            from ibps_msg_all_his
           where transdate >= to_char(pFromDate, 'YYYYMMDD')
             and transdate <= to_char(pToDate, 'YYYYMMDD')
                --and CCYCD LIKE DECODE(pCCYCD, 'ALL', '%', pCCYCD)
             AND F21 = pBranch8
             AND LPAD(TAD, 5, '0') = LPAD(pBranch, 5, '0')
             AND status = 1
             AND DEPARTMENT <> 'TR'
             and MSG_DIRECTION = 'SIBS-IBPS';
      END IF;
    END IF;
    --TINH PHI
    if (vHV_TypeFee = '0' and vLV_TypeFee = '0') then
      strSql := ' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
        X.GW_BANK_CODE AS SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV,
        ' || pBranchType ||
                ' AS BranchType 
        from 
        (select DISTINCT SIBS_BANK_CODE GW_BANK_CODE,BANK_NAME AS BRAN_NAME 
        from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = ''302'')X
        LEFT join
        (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
        from
        (
            select MSG_ID,TRANS_CODE,decode(' || vLV_hardFee || ',0,
            (case when ' || vLV_FeeRate || '*AMOUNT>=' ||
                vLV_MaxFee || ' then ' || vLV_MaxFee || ' when ' ||
                vLV_FeeRate || '*AMOUNT<= ' || vLV_MinFee || ' then ' ||
                vLV_MinFee || ' else ' || vLV_FeeRate || '*AMOUNT end),' ||
                vLV_hardFee || ')as AMOUNT,SOURCE_BRANCH 
            from IBPS_CAL_Fee_TEMP
        )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
        )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
        LEFT join
        (
        select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
        from
        (
            select MSG_ID,TRANS_CODE,decode(' || vHV_hardFee || ',0,
            (case when ' || vHV_FeeRate || '*AMOUNT>=' ||
                vHV_MaxFee || ' then ' || vHV_MaxFee || ' when ' ||
                vHV_FeeRate || '*AMOUNT<= ' || vHV_MinFee || ' then ' ||
                vHV_MinFee || ' else ' || vHV_FeeRate || '*AMOUNT end),' ||
                vHV_hardFee || ')as AMOUNT,SOURCE_BRANCH 
            FROM IBPS_CAL_Fee_TEMP
        )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
        )V
        on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
        LEFT join
        (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
        from
        (
            select MSG_ID,decode(' || vDCV_hardFee ||
                ',0,(case when ' || vDCV_FeeRate || '*AMOUNT>=' ||
                vDCV_MaxFee || ' then ' || vDCV_MaxFee || ' when ' ||
                vDCV_FeeRate || '*AMOUNT<= ' || vDCV_MinFee || ' then ' ||
                vDCV_MinFee || ' else ' || vDCV_FeeRate || '*AMOUNT end),' ||
                vDCV_hardFee ||
                ')as AMOUNT,SOURCE_BRANCH 
            FROM IBPS_CAL_Fee_TEMP where 
            substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''019''),3,3) = 
            substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''022''),3,3)
            and 
            substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''019''),3,3)=
            ''101''
        )T group by T.SOURCE_BRANCH )Y on 
        TRIM(X.GW_BANK_CODE)=TRIM(Y.SOURCE_BRANCH)';
    elsif (vHV_TypeFee = '0' and vLV_TypeFee <> '0') then
      strSql := ' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
      X.GW_BANK_CODE AS SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV,
        ' || pBranchType ||
                ' AS BranchType 
      from
      (select DISTINCT SIBS_BANK_CODE GW_BANK_CODE,BANK_NAME AS BRAN_NAME 
        from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = ''302'')X
      LEFT join
      (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode(' || vLV_hardFee ||
                ',0,(case when ' || vLV_FeeRate || '*AMOUNT>=' ||
                vLV_MaxFee || ' then ' || vLV_MaxFee || ' when ' ||
                vLV_FeeRate || '*AMOUNT<= ' || vLV_MinFee || ' then ' ||
                vLV_MinFee || ' else ' || vLV_FeeRate || '*AMOUNT end),' ||
                vLV_hardFee ||
                ')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_LV || to_char(vLV_TimeFee, 'HH24MISS') || '
          union
          select MSG_ID,TRANS_CODE,decode(' || vLV_hardFee_2 ||
                ',0,(case when ' || vLV_FeeRate_2 || '*AMOUNT>=' ||
                vLV_MaxFee_2 || ' then ' || vLV_MaxFee_2 || ' when ' ||
                vLV_FeeRate_2 || '*AMOUNT<= ' || vLV_MinFee_2 || ' then ' ||
                vLV_MinFee_2 || ' else ' || vLV_FeeRate_2 ||
                '*AMOUNT end),' || vLV_hardFee_2 ||
                ')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_LV_2 || to_char(vLV_TimeFee_2, 'HH24MISS') || '
      )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
      )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
      LEFT join
      (
      select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode(' || vHV_hardFee ||
                ',0,(case when ' || vHV_FeeRate || '*AMOUNT>=' ||
                vHV_MaxFee || ' then ' || vHV_MaxFee || ' 
          when ' || vHV_FeeRate || '*AMOUNT<= ' ||
                vHV_MinFee || ' then ' || vHV_MinFee || ' else ' ||
                vHV_FeeRate || '*AMOUNT end),' || vHV_hardFee ||
                ')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
      )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
      )V
      on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
      LEFT join
      (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
      from
      (
          select MSG_ID,decode(' || vDCV_hardFee ||
                ',0,(case when ' || vDCV_FeeRate || '*AMOUNT>=' ||
                vDCV_MaxFee || ' then ' || vDCV_MaxFee || ' when ' ||
                vDCV_FeeRate || '*AMOUNT<= ' || vDCV_MinFee || ' then ' ||
                vDCV_MinFee || ' else ' || vDCV_FeeRate || '*AMOUNT end),' ||
                vDCV_hardFee ||
                ')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''022''),3,3)
          and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''019''),3,3)=''101''
      )T group by T.SOURCE_BRANCH )Y on 
      TRIM(X.GW_BANK_CODE) = TRIM(Y.SOURCE_BRANCH)';
    elsif (vHV_TypeFee <> '0' and vLV_TypeFee = '0') then
      strSql := ' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
      X.GW_BANK_CODE AS SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV,
        ' || pBranchType ||
                ' AS BranchType 
      from
      (select DISTINCT SIBS_BANK_CODE GW_BANK_CODE,BANK_NAME AS BRAN_NAME 
        from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = ''302'')X
      LEFT join
      (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode(' || vLV_hardFee ||
                ',0,(case when ' || vLV_FeeRate || '*AMOUNT>=' ||
                vLV_MaxFee || ' then ' || vLV_MaxFee || ' when ' ||
                vLV_FeeRate || '*AMOUNT<= ' || vLV_MinFee || ' then ' ||
                vLV_MinFee || ' else ' || vLV_FeeRate || '*AMOUNT end),' ||
                vLV_hardFee || ')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
      )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
      )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
      LEFT join
      (
      select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode(' || vHV_hardFee ||
                ',0,(case when ' || vHV_FeeRate || '*AMOUNT>=' ||
                vHV_MaxFee || ' then ' || vHV_MaxFee || ' when ' ||
                vHV_FeeRate || '*AMOUNT<= ' || vHV_MinFee || ' then ' ||
                vHV_MinFee || ' else ' || vHV_FeeRate || '*AMOUNT end),' ||
                vHV_hardFee ||
                ')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_HV || to_char(vHV_TimeFee, 'HH24MISS') || '
          union
          select MSG_ID,TRANS_CODE,decode(' || vHV_hardFee_2 ||
                ',0,(case when ' || vHV_FeeRate_2 || '*AMOUNT>=' ||
                vHV_MaxFee_2 || ' then ' || vHV_MaxFee_2 || ' when ' ||
                vHV_FeeRate_2 || '*AMOUNT<= ' || vHV_MinFee_2 || ' then ' ||
                vHV_MinFee_2 || ' else ' || vHV_FeeRate_2 ||
                '*AMOUNT end),' || vHV_hardFee_2 ||
                ')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_HV_2 || to_char(vHV_TimeFee_2, 'HH24MISS') || '
      )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
      )V
      on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
      LEFT join
      (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
      from
      (
          select MSG_ID,decode(' || vDCV_hardFee ||
                ',0,(case when ' || vDCV_FeeRate || '*AMOUNT>=' ||
                vDCV_MaxFee || ' then ' || vDCV_MaxFee || ' when ' ||
                vDCV_FeeRate || '*AMOUNT<= ' || vDCV_MinFee || ' then ' ||
                vDCV_MinFee || ' else ' || vDCV_FeeRate || '*AMOUNT end),' ||
                vDCV_hardFee ||
                ')as 
          AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''022''),3,3)
          and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3)=''101''
      )T group by T.SOURCE_BRANCH )Y 
      on TRIM(X.GW_BANK_CODE)= TRIM(Y.SOURCE_BRANCH)';
    else
      strSql := ' SELECT A.SIBS_BANK_CODE,
      A.BRANCH8,A.SumLV,A.NumLV,A.SumHV,A.NumHV,A.SumDCV,
      A.NumDCV,A.BRAN_NAME,' || pBranchType ||
                ' AS BranchType  
      FROM
      (select NVL(U.SumLV,0) SumLV,NVL(U.NumLV,0) NumLV,
      NVL(V.SumHV,0) SumHV,NVL(V.NumHV,0) NumHV,X.BRAN_NAME,
      X.SIBS_BANK_CODE AS SIBS_BANK_CODE,NVL(Y.SumDCV,0) SumDCV,
      NVL(Y.NumDCV,0) NuMDCV,
      (CASE WHEN U.BRANCH8 IS NOT NULL THEN U.BRANCH8 
           WHEN V.BRANCH8 IS NOT NULL THEN V.BRANCH8
           WHEN Y.BRANCH8 IS NOT NULL THEN U.BRANCH8
           ELSE X.GW_BANK_CODE END) AS BRANCH8   
      from
      (select DISTINCT SIBS_BANK_CODE, GW_BANK_CODE,BANK_NAME AS BRAN_NAME 
        from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = ''302'')X
      LEFT JOIN      
      (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH, BRANCH8 
      from
      (
          select MSG_ID,TRANS_CODE,decode(' || vLV_hardFee || ',0,
          (case when ' || vLV_FeeRate || '*AMOUNT/100>=' ||
                vLV_MaxFee || ' then ' || vLV_MaxFee || ' when ' ||
                vLV_FeeRate || '*AMOUNT/100<= ' || vLV_MinFee || ' then ' ||
                vLV_MinFee || ' else ' || vLV_FeeRate ||
                '*AMOUNT/100 end),' || vLV_hardFee ||
                ') as AMOUNT,SOURCE_BRANCH, BRANCH8 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_LV || to_char(vLV_TimeFee, 'HH24MISS') || '
          union
          select MSG_ID,TRANS_CODE,decode(' || vLV_hardFee_2 || ',0,
          (case when ' || vLV_FeeRate_2 || '*AMOUNT/100>=' ||
                vLV_MaxFee_2 || ' then ' || vLV_MaxFee_2 || ' when ' ||
                vLV_FeeRate_2 || '*AMOUNT/100<= ' || vLV_MinFee_2 ||
                ' then ' || vLV_MinFee_2 || ' else ' || vLV_FeeRate_2 ||
                '*AMOUNT/100 end),' || vLV_hardFee_2 ||
                ') as AMOUNT,SOURCE_BRANCH, BRANCH8 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_LV_2 || to_char(vLV_TimeFee_2, 'HH24MISS') || '
      )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH, T.BRANCH8 
      )U 
      ON (LPAD(X.SIBS_BANK_CODE,5,''0'')=LPAD(U.SOURCE_BRANCH,5,''0''))
      LEFT join
      (
      select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH, BRANCH8 
      from
      (
          select MSG_ID,TRANS_CODE,decode(' || vHV_hardFee ||
                ',0,(case when ' || vHV_FeeRate || '*AMOUNT/100>=' ||
                vHV_MaxFee || ' then ' || vHV_MaxFee || ' when ' ||
                vHV_FeeRate || '*AMOUNT/100<= ' || vHV_MinFee || ' then ' ||
                vHV_MinFee || ' else ' || vHV_FeeRate ||
                '*AMOUNT/100 end),' || vHV_hardFee ||
                ') as AMOUNT,SOURCE_BRANCH, BRANCH8 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_HV || to_char(vHV_TimeFee, 'HH24MISS') || '
          union
          select MSG_ID,TRANS_CODE,decode(' || vHV_hardFee_2 || ',0,
          (case when ' || vHV_FeeRate_2 || '*AMOUNT/100>=' ||
                vHV_MaxFee_2 || ' 
          then ' || vHV_MaxFee_2 || ' when ' ||
                vHV_FeeRate_2 || '*AMOUNT/100<= ' || vHV_MinFee_2 ||
                ' then ' || vHV_MinFee_2 || ' else ' || vHV_FeeRate_2 ||
                '*AMOUNT/100 end),' || vHV_hardFee_2 ||
                ') as AMOUNT,SOURCE_BRANCH, BRANCH8 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') ' ||
                vStrwhere_HV_2 || to_char(vHV_TimeFee_2, 'HH24MISS') || '
      )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH, T.BRANCH8 
      )V
      ON (LPAD(X.SIBS_BANK_CODE,5,''0'')=LPAD(V.SOURCE_BRANCH,5,''0'')) 
      LEFT join
      (select Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV,SOURCE_BRANCH,BRANCH8 
      from
      (
         select MSG_ID,decode(' || vDCV_hardFee ||
                ',0,(case when ' || vDCV_FeeRate || '*AMOUNT/100>=' ||
                vDCV_MaxFee || ' then ' || vDCV_MaxFee || ' when ' ||
                vDCV_FeeRate || '*AMOUNT/100<= ' || vDCV_MinFee || ' then ' ||
                vDCV_MinFee || ' else ' || vDCV_FeeRate ||
                '*AMOUNT/100 end),' || vDCV_hardFee ||
                ')as AMOUNT,SOURCE_BRANCH,BRANCH8 
         from IBPS_CAL_Fee_TEMP
         where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
         ''019''),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
         ''022''),3,3)
         and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
         ''019''),3,3)=''101''
      )T group by T.SOURCE_BRANCH,T.BRANCH8)Y 
      ON (LPAD(X.SIBS_BANK_CODE,5,''0'')=LPAD(Y.SOURCE_BRANCH,5,''0''))
      ) A
      WHERE NVL(A.NUMHV,0)+ NVL(A.NUMLV,0) + NVL(A.NUMDCV,0) > 0 
      ORDER BY A.SIBS_BANK_CODE, A.BRANCH8 ';
    end if;
    open RefCurBK02 for strSql;
  END BK02;

  PROCEDURE IBPS02(pfrdate      in date,
                   ptodate      in date,
                   RefCurIBPS02 IN OUT PKG_CR.RefCurType) IS
    NumOfDate NUMBER;
    LVMsgIN   NUMBER(38);
    LVMsgOUT  NUMBER(38);
  BEGIN
  
    --insert into test_clob values (TO_DATE(frdate,'DD/MM/YYYY') || '-' || TO_DATE(todate,'DD/MM/YYYY'));
    commit;
  
    ---Tong gia tri dien di gia tri thap
    /* Formatted on 2008/08/10 20:58 (Formatter Plus v4.8.6) */
    SELECT SUM(AMOUNT)
      into LVMsgOUT
      FROM (SELECT amount, msg_id
              FROM ibps_msg_content
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL_HIS
             WHERE msg_direction = 'SIBS-IBPS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD'));
  
    ---Tong gia tri dien den gia tri thap
    SELECT SUM(amount)
      into LVMsgIN
      FROM (SELECT amount, msg_id
              FROM ibps_msg_content
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD')
            UNION ALL
            SELECT amount, msg_id
              FROM ibps_msg_ALL_HIS
             WHERE msg_direction = 'IBPS-SIBS'
               AND trans_code = '101001'
               AND status = 1
               and transdate >= to_char(pfrdate, 'YYYYMMDD')
               AND transdate <= to_char(ptodate, 'YYYYMMDD'));
  
    NumOfDate := ptodate - pfrdate + 1;
  
    open RefCurIBPS02 for
      select to_char(nvl(LVMsgOUT, 0)) OV,
             to_char(nvl(LVMsgIN, 0)) IV,
             NumOfDate DAYS,
             to_char((LVMsgOUT - nvl(LVMsgIN, 0)) / NumOfDate) as Net
        from dual;
    --    select * from IBPS_MSG_CONTENT;
  END IBPS02;
  FUNCTION IBPS_GET_Field(pCOntent varchar2, FiledCode varchar2)
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
  
    return trim(v_Value);
  Exception
    when others then
      Return '';
  END IBPS_GET_Field;
  /*PROCEDURE BK021(pFromDate in date,
                 pToDate in date,
                 pCCYCD in varchar2,
                 pBranchType in number,
                 pBranch in varchar2,
                 RefCurBK02 IN OUT PKG_CR.RefCurType)
  IS
    vHV_hardFee            NUMBER;
    vLV_hardFee            NUMBER;
    vDCV_hardFee           NUMBER;
    vHV_FeeRate            NUMBER(20,4);
    vLV_FeeRate            NUMBER(20,4);
    vDCV_FeeRate           NUMBER(20,4);
    vHV_MinFee             NUMBER;
    vLV_MinFee             NUMBER;
    vDCV_MinFee            NUMBER;
    vHV_MaxFee             NUMBER;
    vLV_MaxFee             NUMBER;
    vDCV_MaxFee            NUMBER;
    vHV_TimeFee            date;
    vLV_TimeFee            date;
    --vDCV_TimeFee           date;
    vHV_TypeFee            varchar(200);
    vLV_TypeFee            varchar(200);
    vStrwhere_HV            varchar(500);
    vStrwhere_LV            varchar(500);
    strSql                  varchar2(4000);
    ---------------Add more------------
    vStrwhere_HV_2          varchar(500);
    vStrwhere_LV_2          varchar(500);
  
    vCount_HV               NUMBER;
    vCount_LV               NUMBER;
  
    vHV_hardFee_2          NUMBER;
    vLV_hardFee_2          NUMBER;
    vHV_FeeRate_2          NUMBER(20,4);
    vLV_FeeRate_2          NUMBER(20,4);
    vHV_MinFee_2           NUMBER;
    vLV_MinFee_2           NUMBER;
    vHV_MaxFee_2           NUMBER;
    vLV_MaxFee_2           NUMBER;
    vHV_TimeFee_2          date;
    vLV_TimeFee_2          date;    
    --vHV_TypeFee_2 varchar(200);
    --vLV_TypeFee_2 varchar(200);
    ----------End-----------
  BEGIN    
    ----Add more------
    select count(*) into vCount_HV from IBPS_Fee where TransType='HV';
    select count(*) into vCount_LV from IBPS_Fee where TransType='LV';
    if (vCount_HV=1) then
    select hardFee,PERCENTFee,MinFee,MaxFee,Feetime,Feetype into vHV_hardFee,vHV_FeeRate,vHV_MinFee,vHV_MaxFee,vHV_TimeFee,vHV_TypeFee from IBPS_Fee where TransType='HV' and rownum=1;
    else
    select hardFee,PERCENTFee,MinFee,MaxFee,Feetime,Feetype into vHV_hardFee,vHV_FeeRate,vHV_MinFee,vHV_MaxFee,vHV_TimeFee,vHV_TypeFee from IBPS_Fee where TransType='HV' and Feetype=1 and rownum=1;
    select hardFee,PERCENTFee,MinFee,MaxFee,Feetime into vHV_hardFee_2,vHV_FeeRate_2,vHV_MinFee_2,vHV_MaxFee_2,vHV_TimeFee_2 from IBPS_Fee where TransType='HV' and Feetype=2 and rownum=1;
    end if;
    if(vCount_LV=1)then
    select hardFee,PERCENTFee,MinFee,MaxFee,Feetime,Feetype into vLV_hardFee,vLV_FeeRate,vLV_MinFee,vLV_MaxFee,vLV_TimeFee,vLV_TypeFee from IBPS_Fee where TransType='LV'and rownum=1;
    else
    select hardFee,PERCENTFee,MinFee,MaxFee,Feetime,Feetype into vLV_hardFee,vLV_FeeRate,vLV_MinFee,vLV_MaxFee,vLV_TimeFee,vLV_TypeFee from IBPS_Fee where TransType='LV'and Feetype=1 and rownum=1;
    select hardFee,PERCENTFee,MinFee,MaxFee,Feetime into vLV_hardFee_2,vLV_FeeRate_2,vLV_MinFee_2,vLV_MaxFee_2,vLV_TimeFee_2 from IBPS_Fee where TransType='LV'and Feetype=2 and rownum=1;
    end if;
    ----End-------
  
    --select hardFee,PERCENTFee,MinFee,MaxFee,Feetime,Feetype into vHV_hardFee,vHV_FeeRate,vHV_MinFee,vHV_MaxFee,vHV_TimeFee,vHV_TypeFee from IBPS_Fee where TransType='HV';
    --select hardFee,PERCENTFee,MinFee,MaxFee,Feetime,Feetype into vLV_hardFee,vLV_FeeRate,vLV_MinFee,vLV_MaxFee,vLV_TimeFee,vLV_TypeFee from IBPS_Fee where TransType='LV';
    select hardFee,PERCENTFee,MinFee,MaxFee into vDCV_hardFee,vDCV_FeeRate,vDCV_MinFee,vDCV_MaxFee from IBPS_Fee where TransType='DCV' and rownum=1;
  
    if (vHV_TypeFee='1')then
    vStrwhere_HV:='<';
    vStrwhere_HV_2:='>=';
    end if;
  
    if(vLV_TypeFee='1')then
    vStrwhere_LV:='<';
    vStrwhere_LV_2:='>=';
    end if;
    --XOA BANG TEMP
    delete from ibps_cal_Fee_temp;
    --CHI NHANH TAO DIEN
    IF pBranchType=1 THEN
      --TAT CA CAC CHI NHANH
      IF pBranch ='ALL' THEN
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F07,AMOUNT,TRANS_DATE,
        CONTENT 
        from ibps_msg_content 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F07,AMOUNT,TRANS_DATE,CONTENT 
        from ibps_msg_all 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F07,AMOUNT,TRANS_DATE,
        CONTENT from 
        ibps_msg_all_his 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
      --LAY THEO TUNG CHI NHANH
      ELSE
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F07,AMOUNT,TRANS_DATE,
        CONTENT 
        from ibps_msg_content 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        F07 = pBranch AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F07,AMOUNT,TRANS_DATE,CONTENT 
        from ibps_msg_all 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        F07 = pBranch AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F07,AMOUNT,TRANS_DATE,
        CONTENT from 
        ibps_msg_all_his 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        F07 = pBranch AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';      
      END IF;
    --CHI NHAN GUI DIEN
    ELSE
      --TAT CA CAC CHI NHANH
      IF pBranch ='ALL' THEN
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F21,AMOUNT,TRANS_DATE,
        CONTENT 
        from ibps_msg_content 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F21,AMOUNT,TRANS_DATE,CONTENT 
        from ibps_msg_all 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F21,AMOUNT,TRANS_DATE,
        CONTENT from 
        ibps_msg_all_his 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';    
      --LAY THEO TUNG CHI NHANH
      ELSE
        --LAY DU LIEU TU BANG IBPS_MSG_CONTNET
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F21,AMOUNT,TRANS_DATE,
        CONTENT 
        from ibps_msg_content 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        F21 = pBranch AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F21,AMOUNT,TRANS_DATE,CONTENT 
        from ibps_msg_all 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND 
        F21 = pBranch AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';
        --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS
        insert into ibps_cal_Fee_temp(MSG_ID,TRANS_CODE,SOURCE_BRANCH,
        AMOUNT,TRANS_DATE,CONTENT)
        select MSG_ID,TRANS_CODE,F21,AMOUNT,TRANS_DATE,
        CONTENT from 
        ibps_msg_all_his 
        where transdate>=to_char(pFromDate,'YYYYMMDD') and 
        transdate<=to_char(pToDate,'YYYYMMDD') and 
        CCYCD LIKE DECODE(pCCYCD,'ALL','%',pCCYCD) AND         
        F21 = pBranch AND 
        status=1 and 
        MSG_DIRECTION='SIBS-IBPS';      
      END IF;  
    END IF;
    --TINH PHI
    if (vHV_TypeFee='0' and vLV_TypeFee='0') then
        strSql:=' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
        X.GW_BANK_CODE,Y.SumDCV,Y.NumDCV
        from
        (select GW_BANK_CODE,BANK_NAME AS BRAN_NAME from IBPS_BANK_MAP where 
        substr(gw_bank_code,3,3) = ''302'')X
        LEFT join
        (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
        from
        (
            select MSG_ID,TRANS_CODE,decode('||vLV_hardFee||',0,
            (case when '||vLV_FeeRate||'*AMOUNT>='||vLV_MaxFee||
            ' then '||vLV_MaxFee||' when '||vLV_FeeRate||'*AMOUNT<= 
            '||vLV_MinFee||' then '||vLV_MinFee||' else '||vLV_FeeRate||
            '*AMOUNT end),'||vLV_hardFee||')as AMOUNT,SOURCE_BRANCH 
            from IBPS_CAL_Fee_TEMP
        )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
        )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
        LEFT join
        (
        select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
        from
        (
            select MSG_ID,TRANS_CODE,decode('||vHV_hardFee||',0,
            (case when '||vHV_FeeRate||'*AMOUNT>='||vHV_MaxFee||
            ' then '||vHV_MaxFee||' when '||vHV_FeeRate||'*AMOUNT<= '
            ||vHV_MinFee||' then '||vHV_MinFee||' else '||vHV_FeeRate||
            '*AMOUNT end),'||vHV_hardFee||')as AMOUNT,SOURCE_BRANCH from 
            IBPS_CAL_Fee_TEMP
        )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
        )V
        on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
        RIGHT join
        (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
        from
        (
            select MSG_ID,decode('||vDCV_hardFee||',0,(case when '
            ||vDCV_FeeRate||'*AMOUNT>='||vDCV_MaxFee||' then '
            ||vDCV_MaxFee||' when '||vDCV_FeeRate||'*AMOUNT<= '
            ||vDCV_MinFee||' then '||vDCV_MinFee||' else '||vDCV_FeeRate||
            '*AMOUNT end),'||vDCV_hardFee||')as AMOUNT,SOURCE_BRANCH from 
            IBPS_CAL_Fee_TEMP where 
            substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''019''),3,3) = 
            substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''022''),3,3)
            and 
            substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,''019''),3,3)=
            ''101''
        )T group by T.SOURCE_BRANCH )Y on 
        TRIM(X.GW_BANK_CODE)=TRIM(Y.SOURCE_BRANCH)';
    elsif (vHV_TypeFee='0' and vLV_TypeFee<>'0') then
      strSql:=' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
      X.GW_BANK_CODE,Y.SumDCV,Y.NumDCV
      from
      (select GW_BANK_CODE,BANK_NAME AS BRAN_NAME from IBPS_BANK_MAP where 
      substr(gw_bank_code,3,3) = ''302'')X
      LEFT join
      (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode('||vLV_hardFee||',0,(case when '
          ||vLV_FeeRate||'*AMOUNT>='||vLV_MaxFee||' then '||vLV_MaxFee||
          ' when '||vLV_FeeRate||'*AMOUNT<= '||vLV_MinFee||' then '
          ||vLV_MinFee||' else '||vLV_FeeRate||'*AMOUNT end),'
          ||vLV_hardFee||')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_LV
          ||to_char(vLV_TimeFee,'HH24MISS')||'
          union
          select MSG_ID,TRANS_CODE,decode('||vLV_hardFee_2||
          ',0,(case when '||vLV_FeeRate_2||'*AMOUNT>='||vLV_MaxFee_2||
          ' then '||vLV_MaxFee_2||' when '||vLV_FeeRate_2||'*AMOUNT<= '
          ||vLV_MinFee_2||' then '||vLV_MinFee_2||' else '||vLV_FeeRate_2
          ||'*AMOUNT end),'||vLV_hardFee_2||')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_LV_2||
          to_char(vLV_TimeFee_2,'HH24MISS')||'
      )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
      )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
      LEFT join
      (
      select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode('||vHV_hardFee||',0,(case when '
          ||vHV_FeeRate||'*AMOUNT>='||vHV_MaxFee||' then '||vHV_MaxFee||' 
          when '||vHV_FeeRate||'*AMOUNT<= '||vHV_MinFee||' then '||
          vHV_MinFee||' else '||vHV_FeeRate||'*AMOUNT end),'||vHV_hardFee
          ||')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
      )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
      )V
      on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
      RIGHT join
      (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
      from
      (
          select MSG_ID,decode('||vDCV_hardFee||',0,(case when '||vDCV_FeeRate
          ||'*AMOUNT>='||vDCV_MaxFee||' then '||vDCV_MaxFee||' when '
          ||vDCV_FeeRate||'*AMOUNT<= '||vDCV_MinFee||' then '||vDCV_MinFee
          ||' else '||vDCV_FeeRate||'*AMOUNT end),'||vDCV_hardFee||')as 
          AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''022''),3,3)
          and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3)=''101''
      )T group by T.SOURCE_BRANCH )Y on 
      TRIM(X.GW_BANK_CODE) = TRIM(Y.SOURCE_BRANCH)';
    elsif (vHV_TypeFee<>'0' and vLV_TypeFee='0') then
      strSql:=' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
      X.GW_BANK_CODE,Y.SumDCV,Y.NumDCV
      from
      (select GW_BANK_CODE,BANK_NAME AS BRAN_NAME from IBPS_BANK_MAP where 
      substr(gw_bank_code,3,3) = ''302'')X
      LEFT join
      (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode('||vLV_hardFee||',0,(case
           when '||vLV_FeeRate||'*AMOUNT>='||vLV_MaxFee||' then '||
           vLV_MaxFee||' when '||vLV_FeeRate||'*AMOUNT<= '||vLV_MinFee
           ||' then '||vLV_MinFee||' else '||vLV_FeeRate||'*AMOUNT end),'
           ||vLV_hardFee||')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
      )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
      )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
      LEFT join
      (
      select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode('||vHV_hardFee||',0,(case when '
          ||vHV_FeeRate||'*AMOUNT>='||vHV_MaxFee||' then '||vHV_MaxFee||
          ' when '||vHV_FeeRate||'*AMOUNT<= '||vHV_MinFee||' then '
          ||vHV_MinFee||' else '||vHV_FeeRate||'*AMOUNT end),'||vHV_hardFee
          ||')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_HV||
          to_char(vHV_TimeFee,'HH24MISS')||'
          union
          select MSG_ID,TRANS_CODE,decode('||vHV_hardFee_2||',0,(case when '
          ||vHV_FeeRate_2||'*AMOUNT>='||vHV_MaxFee_2||' then '||
          vHV_MaxFee_2||' when '||vHV_FeeRate_2||'*AMOUNT<= '||
          vHV_MinFee_2||' then '||vHV_MinFee_2||' else '||vHV_FeeRate_2
          ||'*AMOUNT end),'||vHV_hardFee_2||')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_HV_2||
          to_char(vHV_TimeFee_2,'HH24MISS')||'
      )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
      )V
      on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
      RIGHT join
      (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
      from
      (
          select MSG_ID,decode('||vDCV_hardFee||',0,(case when '||vDCV_FeeRate
          ||'*AMOUNT>='||vDCV_MaxFee||' then '||vDCV_MaxFee||' when '||
          vDCV_FeeRate||'*AMOUNT<= '||vDCV_MinFee||' then '||vDCV_MinFee
          ||' else '||vDCV_FeeRate||'*AMOUNT end),'||vDCV_hardFee||')as 
          AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''022''),3,3)
          and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
          ''019''),3,3)=''101''
      )T group by T.SOURCE_BRANCH )Y 
      on TRIM(X.GW_BANK_CODE)= TRIM(Y.SOURCE_BRANCH)';
    else
      strSql:=' select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,
      X.GW_BANK_CODE,Y.SumDCV,Y.NumDCV
      from
      (select GW_BANK_CODE,BANK_NAME AS BRAN_NAME from IBPS_BANK_MAP where 
      substr(gw_bank_code,3,3) = ''302'')X
      LEFT join
      (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode('||vLV_hardFee||',0,
          (case when '||vLV_FeeRate||'*AMOUNT>='||vLV_MaxFee||' then '
          ||vLV_MaxFee||' when '||vLV_FeeRate||'*AMOUNT<= '||
          vLV_MinFee||' then '||vLV_MinFee||' else '||vLV_FeeRate
          ||'*AMOUNT end),'||vLV_hardFee||')as AMOUNT,SOURCE_BRANCH 
          from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_LV||
          to_char(vLV_TimeFee,'HH24MISS')||'
          union
          select MSG_ID,TRANS_CODE,decode('||vLV_hardFee_2||',0,
          (case when '||vLV_FeeRate_2||'*AMOUNT>='||vLV_MaxFee_2||
          ' then '||vLV_MaxFee_2||' when '||vLV_FeeRate_2||'*AMOUNT<= '
          ||vLV_MinFee_2||' then '||vLV_MinFee_2||' else '||
          vLV_FeeRate_2||'*AMOUNT end),'||vLV_hardFee_2||')as AMOUNT,
          SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_LV_2||
          to_char(vLV_TimeFee_2,'HH24MISS')||'
      )T where T.TRANS_CODE=''101001'' group by T.SOURCE_BRANCH
      )U on TRIM(X.GW_BANK_CODE)=TRIM(U.SOURCE_BRANCH)
      LEFT join
      (
      select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH
      from
      (
          select MSG_ID,TRANS_CODE,decode('||vHV_hardFee||',0,(case when '
          ||vHV_FeeRate||'*AMOUNT>='||vHV_MaxFee||' then '||vHV_MaxFee||
          ' when '||vHV_FeeRate||'*AMOUNT<= '||vHV_MinFee||' then '||
          vHV_MinFee||' else '||vHV_FeeRate||'*AMOUNT end),'||vHV_hardFee||
          ')as AMOUNT,SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_HV||
          to_char(vHV_TimeFee,'HH24MISS')||'
          union
          select MSG_ID,TRANS_CODE,decode('||vHV_hardFee_2||',0,
          (case when '||vHV_FeeRate_2||'*AMOUNT>='||vHV_MaxFee_2||' 
          then '||vHV_MaxFee_2||' when '||vHV_FeeRate_2||'*AMOUNT<= '||
          vHV_MinFee_2||' then '||vHV_MinFee_2||' else '||
          vHV_FeeRate_2||'*AMOUNT end),'||vHV_hardFee_2||')as AMOUNT,
          SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
          where to_char(TRANS_DATE,''HH24MISS'') '||vStrwhere_HV_2||
          to_char(vHV_TimeFee_2,'HH24MISS')||'
      )T where T.TRANS_CODE=''201001'' group by T.SOURCE_BRANCH
      )V
      on TRIM(X.GW_BANK_CODE)=TRIM(V.SOURCE_BRANCH)
      RIGHT join
      (select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV
      from
      (
         select MSG_ID,decode('||vDCV_hardFee||',0,(case when '||vDCV_FeeRate
         ||'*AMOUNT>='||vDCV_MaxFee||' then '||vDCV_MaxFee||' when '||
         vDCV_FeeRate||'*AMOUNT<= '||vDCV_MinFee||' then '||vDCV_MinFee
         ||' else '||vDCV_FeeRate||'*AMOUNT end),'||vDCV_hardFee||')as AMOUNT,
         SOURCE_BRANCH from IBPS_CAL_Fee_TEMP
         where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
         ''019''),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
         ''022''),3,3)
         and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,
         ''019''),3,3)=''101''
      )T group by T.SOURCE_BRANCH )Y 
      on TRIM(X.GW_BANK_CODE)= TRIM(Y.SOURCE_BRANCH)';
    end if;
    open RefCurBK02 for strSql;
  
  END BK021;*/

  /*
       Create by Bui Hong Phuong
       Split sender input to 3-digit (srcBranch) and 8-digit (sender)    
  */
  PROCEDURE SPLIT_SENDER(org_String   in varchar2,
                         split_string in varchar2,
                         srcBranch    out varchar2,
                         sender       out varchar2) IS
  
    iPosSplit int;
  BEGIN
    srcBranch := '';
    sender    := '';
    iPosSplit := instr(org_String, split_string);
    if iPosSplit > 0 then
      srcBranch := substr(org_String, 1, iPosSplit - length(split_string));
      sender    := substr(org_String,
                          iPosSplit + length(split_string),
                          length(org_String) - iPosSplit +
                          length(split_string));
    end if;
  END SPLIT_SENDER;

  /* 
     Report BM_IBPS23
  */
  PROCEDURE BM_IBPS23(pFromDate         in date,
                      pToDate           in date,
                      pCitad          in varchar2, --tad 8 so
                      pCcycd          in varchar2 default 'VND',
                      pStatus         in varchar2,
                      RefCurBM_IBPS23 IN OUT PKG_CR.RefCurType) IS
        --vDate  date;
    vCitad      varchar2(50);
    vCcycd      varchar2(3);
    vStatus     varchar2(50);
    vOndate     date;
    vBranchName varchar2(200);
  BEGIN
  
    select sysdate into vOndate from dual;
    if upper(trim(pCitad)) = 'ALL' or trim(pCitad) is null then
      vCitad      := '%';
      vBranchName := 'ALL';
    else
      vCitad := trim(pCitad);
      select substr(T.Sibs_Code, -3) || '-' || T.Tad_Name
        into vBranchName
        from tad T
       where T.Gw_Bank_Code = pCitad
         and rownum = 1;
    end if;
    if upper(trim(pCcycd)) = 'ALL' or trim(pCcycd) is null then
      vCcycd := '%';
    else
      vCcycd := trim(pCcycd);
    end if;
    if upper(trim(pStatus)) = 'ALL' or trim(pStatus) is null then
      vStatus := '%';
    else
      vStatus := trim(pStatus);
    end if;
  
    if to_char(pFromDate, 'DDMMYYYY') = to_char(vOndate, 'DDMMYYYY') then
      --open RefCurBM_IBPS23 for
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         TRAN_CODE,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name ,-- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS BUTTOAN,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '011') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.tellerid,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE >= to_char(pFromDate, 'YYYYMMDD') and C.TRANSDATE <= to_char(pToDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   --minh them
                   AND (C.PRODUCT_TYPE='OL12' OR C.PRODUCT_TYPE='OL13') 
                   AND C.F21 like vCitad ) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    elsif substr(to_char(pFromDate, 'DDMMYYYY'), 3, 6) =
          substr(to_char(vOndate, 'DDMMYYYY'), 3, 6) then
      delete from BM_IBPS02_TEMP;
      --Lay du lieu trong ngay
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.TellerID,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE >= to_char(pFromDate, 'YYYYMMDD') and C.TRANSDATE <= to_char(pToDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   --minh them
                   AND (C.PRODUCT_TYPE='OL12' OR C.PRODUCT_TYPE='OL13')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE >= to_char(pFromDate, 'YYYYMMDD') and C.TRANSDATE <= to_char(pToDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   --minh them
                   AND (C.PRODUCT_TYPE='OL12' OR C.PRODUCT_TYPE='OL13')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    
    else
      delete from BM_IBPS02_TEMP;
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_MSG_CONTENT C
                 WHERE C.TRANSDATE >= to_char(pFromDate, 'YYYYMMDD') and C.TRANSDATE <= to_char(pToDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   --minh them
                   AND (C.PRODUCT_TYPE='OL12' OR C.PRODUCT_TYPE='OL13')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong thang
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_MSG_ALL C
                 WHERE C.TRANSDATE >= to_char(pFromDate, 'YYYYMMDD') and C.TRANSDATE <= to_char(pToDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   --minh them
                   AND (C.PRODUCT_TYPE='OL12' OR C.PRODUCT_TYPE='OL13')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
      --Lay du lieu trong nam
      insert into BM_IBPS02_TEMP
        (MSG_ID,
         QUERY_ID,
         GW_TRANS_NUM,
         SOGD,
         RM_NUMBER,
         SENDER,
         RECEIVER,
         AMOUNT,
         TRANS_DATE,
         SOURCE_BRANCH,
         TAD,
         PRE_TAD,
         STATUS,
         CCYCD,
         Tran_Code,
         Sendername,
         Receivername,
         Msg_Direction,
         BRNAME,
         EXTFIELD01,
         EXTFIELD02, --tad name
         EXTFIELD03, -- pre tad name
         EXTFIELD05
         )
        SELECT A.MSG_ID,
               A.QUERY_ID,
               A.GW_TRANS_NUM,
               A.SOGD,
               A.RM_NUMBER,
               A.SENDER,
               A.RECEIVER,
               A.AMOUNT,
               A.TRANS_DATE,
               A.SOURCE_BRANCH,
               A.TAD,
               A.PRE_TAD,
               B.CONTENT       AS STATUS,
               A.CCYCD,
               A.Trans_Code,
               BSEND.BANK_NAME AS SENDERNAME,
               BRECV.Bank_Name AS RECEIVERNAME,
               A.MSG_DIRECTION,
               --decode(trim(pCitad),'ALL','ALL', T.GW_BANK_CODE || ' - ' || T.BANK_NAME),            
               vBranchName,
               A.Tellerid,
               BR.Bran_Name, -- tad name
               BR1.Bran_Name, -- pre tad name
               A.NGUOI_HUONG
          FROM (SELECT C.MSG_ID,
                       C.QUERY_ID,
                       C.GW_TRANS_NUM,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '110') AS SOGD,
                       to_number(C.RM_NUMBER) AS RM_NUMBER,
                       C.F07 as SENDER,
                       C.F22 AS RECEIVER,
                       C.AMOUNT,
                       C.TRANS_DATE,
                       C.SOURCE_BRANCH,
                       C.TAD,
                       C.PRE_TAD,
                       C.STATUS,
                       C.CCYCD,
                       C.Trans_Code,
                       C.MSG_DIRECTION,
                       C.Tellerid,
                       C.F21,
                       C.F19,
                       GW_PK_LIB.GET_IBPS_Field(C.content, '031') AS NGUOI_HUONG
                  FROM IBPS_MSG_ALL_HIS C
                 WHERE C.TRANSDATE >= to_char(pFromDate, 'YYYYMMDD') and C.TRANSDATE <= to_char(pToDate, 'YYYYMMDD')
                   AND trim(C.MSG_DIRECTION) = 'SIBS-IBPS'
                   AND nvl(C.ccycd, '%') LIKE vCcycd
                   AND C.F21 like vCitad
                   --minh them
                   AND (C.PRODUCT_TYPE='OL12' OR C.PRODUCT_TYPE='OL13')) A
          LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B
            ON trim(A.STATUS) = trim(B.STATUS)
          LEFT JOIN IBPS_BANK_MAP BSEND
            on BSEND.gw_bank_code = A.SENDER
           AND BSEND.Sibs_Bank_Code =
               decode(BSEND.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
          LEFT JOIN IBPS_BANK_MAP BRECV
            on BRECV.gw_bank_code = A.RECEIVER
           AND BRECV.Sibs_Bank_Code =
               decode(BRECV.Sibs_Bank_Code,
                      -1,
                      -1,
                      lpad(A.Source_Branch, 5, '0'))
        
          LEFT JOIN BRANCH BR
            ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)
          LEFT JOIN BRANCH BR1
            ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)
          LEFT JOIN IBPS_BANK_MAP T
            ON T.GW_BANK_CODE = A.F21
           and T.GW_BANK_CODE NOT in
               (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)
        
         WHERE B.CONTENT LIKE vStatus;
    end if;
    open RefCurBM_IBPS23 for
      select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;
  END BM_IBPS23;

END;

