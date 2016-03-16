create or replace package GW_PK_IBPS_UPDATE_ACC is

  -- Author  : ADMINISTRATOR
  -- Created : 12/2/2008 5:15:42 PM
  -- Purpose : Cap nhat trang truong 31 va truong 33 voi cac dien co trang thai la 99
  -- Public type declarations
  Procedure UPDATE_BEN_ACCOUNT;

end GW_PK_IBPS_UPDATE_ACC;
/
CREATE OR REPLACE PACKAGE BODY GW_PK_IBPS_UPDATE_ACC is
  vSIBS_HOST     varchar2(20) := '10.2.1.1';
  vSIBS_USER     varchar2(20) := 'SVOPRDWH';
  vSIBS_PASS     varchar2(20) := 'SVOPRDWH';
  m_Type_MSG_log IBPS_MSG_LOG%rowtype;
  pQUERY_ID      number(20);
  strResult      varchar2(4000) := '';

  Procedure UPDATE_BEN_ACCOUNT IS
  
    vCONTENT    varchar2(4000);
    dTRANS_DATE date;
    vTransdate  varchar2(8);
    vSqlExcute  varchar2(1000);
  
    /*vSIBS_SQL   varchar2(4000) := 'Select  a.rmacno, 
          b.tlbafm       
     FROM STDATTRN.RMMAST A,
          STDATTRN.TLLOG  B,
          STDATTRN.RMDETL C,
          STDATTRN.CFMAST d
    WHERE
      b.tlbf01 = a.rmacno
      and c.rdacct = b.tlbf01
      and a.RMACIF = d.CFCIFN
      and a.rmacno=';*/
  
    vSIBS_SQL  varchar2(4000) := 'Select A.rmacno, A.RMBENA, coalesce(CFC.CFNA1, CFT.CTNAME) AS  REC_NAME
  FROM  SVDATPV51.RMMAST A
  LEFT  join  SVDATPV51.CFTNAM CFT ON  A.RMACFT=''T''  AND   A.RMACIF = CFT.CTTCIF
  LEFT  join  SVDATPV51.CFMAST CFC ON  A.RMACFT=''C''  AND   A.RMACIF = CFC.CFCIFN
  where  A.rmacno=';
  
    cursor curMSG_OUT is
      SELECT IBPSMSG.MSG_ID,
             IBPSMSG.QUERY_ID,
             IBPSMSG.GW_TRANS_NUM,
             IBPSMSG.TAD,
             IBPSMSG.CONTENT,
             IBPSMSG.Department,
             IBPSMSG.Trans_Date,
             IBPSMSG.Transdate,
             IBPSMSG.Rm_Number
        From IBPS_Msg_Content IBPSMSG
       WHERE MSG_DIRECTION = 'SIBS-IBPS'
         and Status = '99'
         AND IBPSMSG.transdate = to_char(sysdate, 'YYYYMMDD');
  
    v_MSG_OUT curMSG_OUT%rowtype;
  
  BEGIN
  
    OPEN curMSG_OUT;
    LOOP
      -- Lay tung dong du lieu cua cursor de xu ly
      FETCH curMSG_OUT
        INTO v_MSG_OUT;
    
      -- Thoat khoi lenh lap neu da duyet het tat ca du lieu
      EXIT WHEN curMSG_OUT %notfound;
    
      pQUERY_ID   := v_MSG_OUT.Query_Id;
      vCONTENT    := trim(v_MSG_OUT.Content);
      dTRANS_DATE := v_MSG_OUT.Trans_Date;
      vTransdate  := v_MSG_OUT.Transdate;
      vSIBS_SQL   := vSIBS_SQL || v_MSG_OUT.Rm_Number;
    
      strResult := Update_BEN_ACC(vSIBS_SQL,
                                  vSIBS_HOST,
                                  vSIBS_USER,
                                  vSIBS_PASS,v_MSG_OUT.Rm_Number);
    
    END LOOP;
  
    -- ?ong cursor
    CLOSE curMSG_OUT;
  EXCEPTION
    when others then
      m_Type_MSG_log.Log_Date     := Sysdate;
      m_Type_MSG_log.Query_Id     := pQUERY_ID;
      m_Type_MSG_log.Status       := 0;
      m_Type_MSG_log.Descriptions := 'ERROR WHEN Query Message' || Sqlcode ||
                                     ' -Error- ' || Sqlerrm;
      m_Type_MSG_log.Job_Name     := 'IBPS_JOB_UPDATE_TRANSREF';
      GW_PK_IBPS_PROCESS.IBPS_Msg_Trace(m_Type_MSG_log);
    
  END UPDATE_BEN_ACCOUNT;

end GW_PK_IBPS_UPDATE_ACC;
/
