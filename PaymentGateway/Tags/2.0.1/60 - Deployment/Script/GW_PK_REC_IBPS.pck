create or replace package GW_PK_REC_IBPS is
  type RefCurType is ref cursor;

  Procedure GetIBPS_MSG_CONTENT(pDate in date, vReturn in out RefCurType);

  Procedure GetIBPS_MSG_REC(pDate       in date,
                            vRec_type   varchar2,
                            vDirection  varchar2,
                            vExp_type   varchar2,
                            vTAD        varchar2,
                            vDepartment varchar2,
                            vReturn     in out RefCurType);

  Procedure Rec_index(pDate    in date,
                      rec_type varchar2,
                      pTad     varchar2 default '011',
                      vIndex   in out varchar2);

  procedure InsertIBPS_MSG_REC_TAD(pdate   date,
                                   vTad    varchar2,
                                   vDBLINK varchar2);

  procedure InsertIBPS_MSG_REC_TAD_NOT_HO(pGW_TRANS_NUM     varchar2,
                                          pHLV              varchar2,
                                          pSENDER           varchar2,
                                          pDate             date,
                                          pRECEIVER         varchar2,
                                          pORG_BANK         varchar2,
                                          pRECEIVING_BRANCH varchar2,
                                          pCCY              varchar2,
                                          pAMOUNT           number,
                                          pDIRECTION        varchar2,
                                          pK1               varchar2,
                                          pRM_NUMBER        varchar2,
                                          pTad              varchar2);

  procedure InsertIBPS_MSG_REC_TEMP(pdate date,pPeriod number);

  Procedure InsertIBPS_MSG_CONTENT_TEMP(pDate date);

  Procedure InsertIBPS_MSG_CONTENT_TAD(pDate in date, pTAD varchar2);

  Procedure RECONCILE_SIBS(pDate date, pTellerID varchar2);

  Procedure RECONCILE_TR(pDate date, pTellerID varchar2);

  Procedure RECONCILE_IBPS(pDate date, pTAD varchar2, pTellerID varchar2);

  FUNCTION ExecuteSQLReturn(strSQL varchar2) return varchar2;

  FUNCTION ExecuteSQLNonReturn(strSQL varchar2) return boolean;

  Procedure SimpleIBPS_REC_ONE(vType varchar2);

end GW_PK_REC_IBPS;
/
create or replace package body GW_PK_REC_IBPS is

  Procedure GetIBPS_MSG_CONTENT(pDate in date, vReturn in out RefCurType) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    open vReturn for
      select gw.msg_direction, gw.department, gw.status, gw.tad, count(1)
        from ((select msg_direction, department, status, tad
                 from ibps_msg_content
                where Transdate = iNDATE) union all
              (select msg_direction, department, status, tad
                 from ibps_msg_all
                where Transdate = iNDATE) union all
              (select msg_direction, department, status, tad
                 from ibps_msg_all_his
                where Transdate = iNDATE)) gw
       group by gw.msg_direction, gw.department, gw.status, gw.tad
       order by gw.msg_direction, gw.department, gw.status, gw.tad;
  
  End GetIBPS_MSG_CONTENT;

  -- GetData : cua bang SWIFT_MSG_REC
  -- HoangLA
  Procedure GetIBPS_MSG_REC(pDate       in date,
                            vRec_type   varchar2,
                            vDirection  varchar2,
                            vExp_type   varchar2,
                            vTAD        varchar2,
                            vDepartment varchar2,
                            vReturn     in out RefCurType) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    open vReturn for
      select IMR.RM_NUMBER,
             IMR.REF_NUMBER,
             IMR.K1,
             IMR.AMOUNT,
             IMR.CCY,
             IMR.EXCEPTION_TYPE,
             IMR.REC_TYPE,
             IMR.Sender,
             IMR.Receiver,
             IMR.Tad,
             IMR.Status,
             IMR.MSG_DIRECTION,
             IMR.ORG_BANK,
             IMR.RECEIVING_BRANCH
        from IBPS_MSG_REC IMR
       where decode(vExp_type, 'ALL', '1', exception_type) =
             decode(vExp_type, 'ALL', '1', vExp_type) -- exp
         and decode(vTAD, 'ALL', '1', lpad(tad, 5, '0')) =
             decode(vTAD, 'ALL', '1', lpad(vTAD, 5, '0')) -- TAD
         and decode(vDepartment, 'ALL', '1', app_code) =
             decode(vDepartment, 'ALL', '1', vDepartment) -- pDepartment   
         and decode(vDirection, 'ALL', '1', msg_direction) =
             decode(vDirection, 'ALL', '1', vDirection) -- Direction
         and decode(vRec_type, 'ALL', '1', rec_type) =
             decode(vRec_type, 'ALL', '1', vRec_type) -- Rec_type
            
         and NDATE = iNDATE; -- pDate
  
  End GetIBPS_MSG_REC;

  -- Ham tra ve cau thong bao IBPS
  -- HoangLA
  -- Ten Ham : SIBS-BR
  Procedure Rec_index(pDate    in date,
                      rec_type varchar2,
                      pTad     varchar2 default '011',
                      vIndex   in out varchar2) Is
    iRecA   integer;
    iTotalA integer;
    iRecB   integer;
    iTotalB integer;
    vnDate  number(20);
  begin
    vnDate := to_number(to_char(pDate, 'YYYYMMDD'));
    vIndex := '';
    -- SIBS -> GW
    if rec_type = 'SIBS->BR' then
      -- iTotalA
      select count(1)
        into iTotalA
        from ibps_msg_rec_temp
       where msg_direction = 'O'
         and rec_type = 'SIBS-BR'
         and ndate = vnDate;
      -- iTotalB
      select count(1)
        into iTotalB
        from ibps_msg_content_temp
       where status = 1
         AND MSG_SRC in (1, 4, 5)
         and msg_direction = 'SIBS-IBPS'
         and department <> 'TR' -- NoTR
         and ndate = vnDate;
    
      -- iRecA
      select count(1)
        into iRecA
        from ibps_msg_rec
       where msg_direction = 'O'
         and rec_type = 'SIBS-BR'
         and exception_type = 'S'
         and ndate = vnDate;
      -- iRecB
      select count(1)
        into iRecB
        from ibps_msg_rec
       where msg_direction = 'O'
         and rec_type = 'SIBS-BR'
         and exception_type = 'GS'
         and ndate = vnDate;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
    -- SIBS<-BR
    if rec_type = 'SIBS<-BR' then
      -- iTotalA
      select count(1)
        into iTotalA
        from ibps_msg_rec_temp
       where msg_direction = 'I'
         and rec_type = 'SIBS-BR'
         and ndate = vnDate;
      -- iTotalB
      select count(1)
        into iTotalB
        from ibps_msg_content_temp
       where status > 0
         and msg_direction = 'IBPS-SIBS'
         and department <> 'TR' -- NoTR
         and ndate = vnDate;
      -- iRecA
      select count(1)
        into iRecA
        from ibps_msg_rec
       where msg_direction = 'I'
         and rec_type = 'SIBS-BR'
         and exception_type = 'S'
         and ndate = vnDate;
      -- iRecB
      select count(1)
        into iRecB
        from ibps_msg_rec
       where msg_direction = 'I'
         and rec_type = 'SIBS-BR'
         and exception_type = 'GS'
         and ndate = vnDate;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
    -- GW -> IBPS
    if rec_type = 'BR->IBPS' then
      -- iTotalA
      select count(1)
        into iTotalA
        from ibps_msg_content_tad
       where status = 1
         and (ltrim(TAD, '0') = ltrim(pTAD, '0'))
         and msg_direction = 'SIBS-IBPS'
         AND NDATE = vnDate;
      -- iTotalB
      select count(1)
        into iTotalB
        from ibps_msg_rec_tad
       where msg_direction = 'O'
         and (ltrim(TAD, '0') = ltrim(pTAD, '0'))
         and NDATE = vnDate;
      -- iRecA
      select count(1)
        into iRecA
        from ibps_msg_rec
       where msg_direction = 'O'
         and rec_type = 'IBPS-BR'
         and exception_type = 'GI'
         and (ltrim(TAD, '0') = ltrim(pTAD, '0'))
         and NDATE = vnDate;
      -- iRecB
      select count(1)
        into iRecB
        from ibps_msg_rec
       where msg_direction = 'O'
         and rec_type = 'IBPS-BR'
         and exception_type = 'I'
         and ltrim(TAD, '0') = ltrim(pTad, '0')
         and NDATE = vnDate;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
    -- GW <- IBPS
    if rec_type = 'BR<-IBPS' then
      -- iTotalA
      select count(1)
        into iTotalA
        from ibps_msg_content_tad
       where status = 1
         and ltrim(TAD, '0') = ltrim(pTad, '0')
            --and (ltrim(TAD, '0') = '11' or ltrim(TAD, '0') = '40')
         and msg_direction = 'IBPS-SIBS'
         and NDATE = vnDate;
      -- iTotalB
      select count(1)
        into iTotalB
        from ibps_msg_rec_tad
       where msg_direction = 'I'
         and ltrim(TAD, '0') = ltrim(pTad, '0')
            --and (ltrim(TAD, '0') = '11' or ltrim(TAD, '0') = '40')
         and NDATE = vnDate;
      -- iRecA
      select count(1)
        into iRecA
        from ibps_msg_rec
       where msg_direction = 'I'
            --and TAD='011'
         and ltrim(TAD, '0') = ltrim(pTad, '0')
         and rec_type = 'IBPS-BR'
         and exception_type = 'GI'
         and NDATE = vnDate;
      -- iRecB
      select count(1)
        into iRecB
        from ibps_msg_rec
       where msg_direction = 'I'
         and ltrim(TAD, '0') = ltrim(pTad, '0') --and TAD='011'
         and rec_type = 'IBPS-BR'
         and exception_type = 'I'
         and NDATE = vnDate;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
    -- TR->GW
    if rec_type = 'TR->BR' then
      -- iTotalA
      select count(1)
        into iTotalA
        from ibps_msg_rec_temp
       where msg_direction = 'O' -- Out
         and rec_type = 'TR-BR'
         and NDATE = vnDate;
      -- iTotalB
      select count(1)
        into iTotalB
        from ibps_msg_content_temp
       where status = 1
         and msg_direction = 'SIBS-IBPS'
         and department = 'TR' -- TR
         and NDATE = vnDate;
      -- iRecA
      select count(1)
        into iRecA
        from ibps_msg_rec
       where msg_direction = 'O'
         and rec_type = 'TR-BR'
         and exception_type = 'R'
         and NDATE = vnDate;
      -- iRecB
      select count(1)
        into iRecB
        from ibps_msg_rec
       where msg_direction = 'O'
         and rec_type = 'TR-BR'
         and exception_type = 'GR'
         and NDATE = vnDate;
    
      vIndex := iRecA || '/' || iTotalA || '-' || iRecB || '/' || iTotalB;
      return;
    end if;
  
  end Rec_index;

  -- Insert vao bang IBPS_MSG_CONTENT_TEMP
  --
  Procedure InsertIBPS_MSG_CONTENT_TEMP(pDate in date) Is
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    -- delete old msg if have
    delete from IBPS_MSG_CONTENT_TEMP where NDATE = iNDATE;
    -- Content
    insert into IBPS_MSG_CONTENT_TEMP
      (msg_id,
       department,
       rm_number,
       msg_direction,
       trans_date,
       rec_type,
       status,
       sender,
       receiver,
       k1,
       ccy,
       amount,
       tad,
       NDATE,
       QUERY_ID,
       MSG_SRC,
       F21,
       F22,
       GW_Transnum)
      (select msg_id,
              department,
              LTRIM(rm_number, '0'),
              msg_direction,
              pDate
              /*trans_date*/,
              'SIBS-BR',
              status,
              F07,
              f22,
              lpad(sibs_trans_num, 12, '0'),
              ccycd,
              amount,
              ltrim(tad, '0'),
              iNDATE,
              QUERY_ID,
              MSG_SRC,
              F21,
              F22,
              lpad(Gw_Trans_Num, 12, '0')
         from IBPS_MSG_CONTENT
        where status = 1
          and (To_number(To_char(Receiving_Time, 'YYYYMMDD')) = iNDATE or
              transdate = iNDATE));
    -- All
    insert into IBPS_MSG_CONTENT_TEMP
      (msg_id,
       department,
       rm_number,
       msg_direction,
       trans_date,
       rec_type,
       status,
       sender,
       receiver,
       k1,
       ccy,
       amount,
       tad,
       NDATE,
       QUERY_ID,
       MSG_SRC,
       F21,
       F22,
       gw_transnum)
      (select msg_id,
              department,
              LTRIM(rm_number, '0'),
              msg_direction,
              pdate
              /*trans_date*/,
              'SIBS-BR',
              status,
              F07,
              F22,
              lpad(sibs_trans_num, 12, '0'),
              ccycd,
              amount,
              ltrim(tad, '0'),
              iNDATE,
              QUERY_ID,
              MSG_SRC,
              F21,
              F22,
              lpad(Gw_Trans_Num, 12, '0')
         from IBPS_MSG_ALL
        where (To_number(To_char(Receiving_Time, 'YYYYMMDD')) = iNDATE or
              transdate = iNDATE)
          and status = 1) /* status = 1
                                                                                                                                  and transdate = iNDATE)*/
    ;
    -- All_his
    /* insert into IBPS_MSG_CONTENT_TEMP
      (msg_id,
       department,
       rm_number,
       msg_direction,
       trans_date,
       rec_type,
       status,
       sender,
       receiver,
       k1,
       ccy,
       amount,
       tad,
       NDATE,
       QUERY_ID,
       MSG_SRC,
       F21,
       F22,
       gw_transnum)
      (select msg_id,
              department,
              LTRIM(rm_number, '0'),
              msg_direction,
              pdate
              \* trans_date*\,
              'SIBS-BR',
              status,
              F07,
              F22,
              lpad(sibs_trans_num, 12, '0'),
              ccycd,
              amount,
              ltrim(tad, '0'),
              iNDATE,
              QUERY_ID,
              MSG_SRC,
              F21,
              F22,
              lpad(Gw_Trans_Num, 12, '0')
         from IBPS_MSG_ALL_HIS
        where \*status = 1
                                                                                                                                                                                  and transdate = iNDATE)*\
       \*      To_number(To_char(Receiving_Time, 'YYYYMMDD')) = iNDATE
       or*\
        transdate = iNDATE
    and status = 1);*/
    commit; -- Commit all;
  End;

  -- Insert from TBLHV + TBLLV + TBLTRANS
  --
  procedure InsertIBPS_MSG_REC_TAD(pdate   date,
                                   vTad    varchar2,
                                   vDBLINK varchar2) Is
    vSQL      varchar2(4000);
    iNDATE    number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
    vDBLINKHO varchar2(100);
    bOK       boolean:=true;
    err       varchar2(1000);
  
    -- Biet xac dinh chi nhanh HO
    isTadHO varchar2(2);
    -- Cursor
    Type RefCurType is ref cursor;
    curIBPS_REC_TAD RefCurType;
    -- Bien
    pGW_TRANS_NUM     varchar2(100);
    pHLV              varchar2(100);
    pSENDER           varchar2(100);
    pTRANS_DATE       date;
    pRECEIVER         varchar2(100);
    pORG_BANK         varchar2(100);
    pRECEIVING_BRANCH varchar2(100);
    pCCY              varchar2(100);
    pAMOUNT           number(30, 2);
    pDIRECTION        varchar2(100);
    pK1               varchar2(100);
    pRM_NUMBER        varchar2(100);
  begin
    -- Search DBLINK
    begin
      select tad.function
        into isTadHO
        from TAD
       where LPAD(tad.sibs_code, 5, '0') = LPAD(vTad, 5, '0')
         and rownum = 1;
    Exception
      when others then
        isTadHO := '0';
    end;
    if isTadHO = 2 then
      return;
    end if;
  
    select dblink
      into vDBLINKHO
      from tad
     where ltrim(tad.sibs_code, '0') = ltrim(vTad, '0')
       and rownum <= 1;
    -- delete old data if have
    Delete from IBPS_MSG_REC_TAD
     where rec_type = 'IBPS-BR'
       and ndate = iNDATE
       and Ltrim(TAD, '0') = Ltrim(vTad, '0');
    commit;
  
    ---   InsertIBPS_MSG_REC_TAD
    -- Excute Store
    vSQL := ' BEGIN gw_pk_rec_br.insertIBPS_MSG_REC_TAD@' || vDBLINKHO ||
            '(to_date(''' || iNDATE || ''',''YYYYMMDD''),' || vTAD ||
            '); END; ';
    bOK  := ExecuteSQLNonReturn(vSQL);
    if bOK = false then
      raise_application_error(-20001, 'Error when execute Insert REC_TAD');
    end if;
    -- Insert
    Begin
      open curIBPS_REC_TAD for 'SELECT GW_TRANS_NUM,HLV,SENDER,RECEIVER,CCY,AMOUNT,DIRECTION,LTRIM(RM_NUMBER,''0'') RM_NUMBER,TRANS_DATE,K1,ORG_BANK,RECEIVING_BRANCH FROM IBPS_MSG_REC_TAD@' || vDBLINKHO || ' WHERE REC_TYPE=''IBPS-BR'' AND NDATE=' || iNDATE || ' ';
      loop
        fetch curIBPS_REC_TAD
          into pGW_TRANS_NUM,
               pHLV,
               pSENDER,
               pRECEIVER,
               pCCY,
               pAMOUNT,
               pDIRECTION,
               pRM_NUMBER,
               pTRANS_DATE,
               pK1,
               pORG_BANK,
               pRECEIVING_BRANCH;
        exit when curIBPS_REC_TAD%notfound;
      
        Insert into IBPS_MSG_REC_TAD
          (GW_TRANS_NUM,
           HLV,
           SENDER,
           TRANS_DATE,
           RECEIVER,
           ORG_BANK,
           RECEIVING_BRANCH,
           CCY,
           AMOUNT,
           MSG_DIRECTION,
           TAD,
           K1,
           ACC_TYPE,
           APP_CODE,
           REC_TYPE,
           RM_NUMBER,
           NDATE)
        values
          (pGW_TRANS_NUM,
           pHLV,
           pSENDER,
           pTRANS_DATE,
           pRECEIVER,
           pORG_BANK,
           pRECEIVING_BRANCH,
           pCCY,
           pAMOUNT,
           pDIRECTION,
           vTAD,
           pK1,
           'R',
           'XX',
           'IBPS-BR',
           LTRIM(pRM_NUMBER, '0'),
           iNDATE);
        commit;
      end loop;
      close curIBPS_REC_TAD;
    exception
      when others then
        err := sqlcode || sqlerrm;
        raise_application_error(-20001, err);
    End;
  
    --insert into htest values(vSQL);commit;
  End InsertIBPS_MSG_REC_TAD;

  -- Day dien D/C voi GW
  --
  procedure InsertIBPS_MSG_REC_TAD_NOT_HO(pGW_TRANS_NUM     varchar2,
                                          pHLV              varchar2,
                                          pSENDER           varchar2,
                                          pDate             date,
                                          pRECEIVER         varchar2,
                                          pORG_BANK         varchar2,
                                          pRECEIVING_BRANCH varchar2,
                                          pCCY              varchar2,
                                          pAMOUNT           number,
                                          pDIRECTION        varchar2,
                                          pK1               varchar2,
                                          pRM_NUMBER        varchar2,
                                          pTad              varchar2) Is
    vSQL varchar2(4000);
  
    iNDATE number(20);
    vdate  date;
  
    vDBLINKHO varchar2(100);
    bOK       boolean;
    err       varchar2(1000);
    -- Cursor
    Type RefCurType is ref cursor;
    curIBPS_REC_TAD RefCurType;
    -- Bien
  
  begin
  
    begin
      iNDATE := to_number(substr(pk1, 1, 8));
      vdate  := to_date(iNDATE, 'YYYYMMDD');
      /*if pDate is null then
        vdate  := sysdate;
        iNDATE := to_number(to_char(sysdate, 'YYYYMMDD'));
      else
        iNDATE := to_number(to_char(pDate, 'YYYYMMDD'));
        vdate  := pDate;
        if to_number(to_char(pDate, 'YYYY')) < 1900 then
          vdate  := sysdate;
          iNDATE := to_number(to_char(sysdate, 'YYYYMMDD'));
        end if;
      end if;*/
    
    Exception
      when others then
        vdate  := sysdate;
        iNDATE := to_number(to_char(sysdate, 'YYYYMMDD'));
      
    end;
    -- Search DBLINK
  
    ---   InsertIBPS_MSG_REC_TAD
    -- Insert
    Begin
      /*      vdate  := sysdate;
            iNDATE := to_number(to_char(sysdate, 'YYYYMMDD'));
      */
    
      Insert into IBPS_MSG_REC_TAD
        (GW_TRANS_NUM,
         HLV,
         SENDER,
         TRANS_DATE,
         RECEIVER,
         ORG_BANK,
         RECEIVING_BRANCH,
         CCY,
         AMOUNT,
         MSG_DIRECTION,
         TAD,
         K1,
         ACC_TYPE,
         APP_CODE,
         REC_TYPE,
         RM_NUMBER,
         NDATE)
      values
        (pGW_TRANS_NUM,
         pHLV,
         pSENDER,
         vdate,
         pRECEIVER,
         pORG_BANK,
         pRECEIVING_BRANCH,
         pCCY,
         pAMOUNT,
         pDIRECTION,
         pTAD,
         pK1,
         'R',
         'XX',
         'IBPS-BR',
         LTRIM(pRM_NUMBER, '0'),
         iNDATE);
      commit;
      -- err :='';
    exception
      when others then
        err := sqlcode || sqlerrm;
        raise_application_error(-20001, err);
    End;
  
    --insert into htest values(vSQL);commit;
  End InsertIBPS_MSG_REC_TAD_NOT_HO;

  procedure InsertIBPS_MSG_REC_TEMP(pdate date,pPeriod number) Is
    vSQL   varchar2(4000);
    iNDATE number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
    vttt   varchar2(2000);
    vNDATE number(10);
  begin
    begin      
      -- lay ngay doi chieu gan nhat
      vNDATE:=to_number(to_char(pdate-pPeriod, 'YYYYMMDD'));       
      -- doi chieu qua jdbc
      if to_char(pDate, 'YYYYMMDD') = to_char(sysdate, 'YYYYMMDD') then
        delete from ibps_msg_rec_temp
         where rec_type = 'SIBS-BR'
           and ndate = iNDATE;
        commit;
        --      lay dien OL3
           vttt := reconile_readas400('select ''IBPSOUT '',
              T1.rmprdc,   
              T3.rdbr,     
              T1.rmamt,      
              T1.rmcurr,       
              T1.rmacno,       
              T1.rmdis6  
        from SVDATPV51.RMMAST T1,
              SVDATPV51.TLLOG T2, 
              SVDATPV51.RMDETL T3
        where T1.rmprdc = ''OL3''   
              and T1.rmcurr = ''VND''   
              and (T2.tlbf09 = ''120101001'' or T2.tlbf09 = ''280601002'')
              and T2.tlbf01 = T1.rmacno   
              and T3.rdacct = T2.tlbf01   
              and T3.rdtxid = ''CRRM''
              and T2.tltxok = ''Y''   
              and T2.tlbdel = ''''   
              and T2.tlbcor = ''N''
              and T2.tlbtcd = ''8163''',
                                          '10.2.1.1',
                                          'svoprdwh',
                                          'svoprdwh',
                                          'IBPS',
                                          'O',
                                          'OL3',
                                          to_char(pDate, 'YYYYMMDD'));
      
    /*    vttt := reconile_readas400('select ''IBPSOUT '',
       T1.rmprdc,   
       T3.rdbr,     
       T1.rmamt,      
       T1.rmcurr,       
       T1.rmacno,       
       T1.rmdis6  
 from STDATTRN.RMMAST T1,
       STDATTRN.TLLOG T2, 
       STDATTRN.RMDETL T3
 where T1.rmprdc = ''OL3''   
       and T1.rmcurr = ''VND''   
       and (T2.tlbf09 = ''120101001'' or T2.tlbf09 = ''280601002'')
       and T2.tlbf01 = T1.rmacno   
       and T3.rdacct = T2.tlbf01   
       and T3.rdtxid = ''CRRM''
       and T2.tltxok = ''Y''   
       and T2.tlbdel = ''''   
       and T2.tlbcor = ''N''
       and T2.tlbtcd = ''8163''',
                                   '10.0.1.1',
                                   'xemsolieut',
                                   'xemsolieut',
                                   'IBPS',
                                   'O',
                                   'OL3',
                                   to_char(pDate, 'YYYYMMDD'));*/
      
       
        /*  vttt := reconile_readas400('select ''IBPSOUT'',
              T1.rmprdc,
              T3.rdbr,
              T1.rmamt,
              T1.rmcurr,
              T1.rmacno,
              T1.rmdis6
         FROM SVDATPV51.RMMAST T1,
              SVDATPV51.TLLOG  T2,
              SVDATPV51.RMDETL T3 
        WHERE T2.TLBTCD = ''TP8277''
          and T1.rmprdc = ''OL2''
          and T3.RDRACT = ''280701018''
          and T2.tlbf01 = T1.rmacno
          and T3.rdacct = T2.tlbf01',
                                          '10.2.1.1',
                                          'svoprdwh',
                                          'svoprdwh',
                                          'IBPS',
                                          'O',
                                          'OL3',
                                          to_char(pDate, 'YYYYMMDD'));
             
               --      lay dien OL2 for IB
               vttt := reconile_readas400('
           select ''IBPSOUT'',
              T1.rmprdc,
              T3.rdbr,
              T1.rmamt,
              T1.rmcurr,
              T1.rmacno,
              T1.rmdis6
         FROM SVDATPV51.RMMAST T1,
              SVDATPV51.TLLOG  T2,
              SVDATPV51.RMDETL T3,
              SVDATPV51.CFMAST T4
        WHERE T2.TLBTCD = ''EB8277''
          and T1.rmprdc = ''OL2''
          and T3.RDRACT = ''280898009''
          and T2.TLBID=''EBANKING01''
          and T2.tlbf01 = T1.rmacno
          and T3.rdacct = T2.tlbf01
          and T1RMACIF = T4.CFCIFN',
                                          '10.2.1.1',
                                          'svoprdwh',
                                          'svoprdwh',
                                          'IBPS',
                                          'O',
                                          'OL3',
                                          to_char(pDate, 'YYYYMMDD'));*/
      end if;
      -- Xoa truoc du lieu
     delete from ibps_msg_rec_temp
         where rec_type = 'SIBS-BR'
         And running_seg_no='99999999'
           and ndate = iNDATE;
       --      lay dien OL2
       -- 
        insert into ibps_msg_rec_temp
          (gw_type,
           rm_number,
           acc_type,
           running_seg_no,
           ref_number,
           msg_type,
           seg_no,
           msg_no,
           ccy,
           amount,
           app_code,
           sender,
           org_bank,
           receiving_branch,
           receiver,
           value_date,
           journal_seg_no,
           msg_direction,
           from_system,
           to_system,
           trans_date,
           rec_type,
           K1,
           NDATE)
          (select 'IBPS',
                  ISO.RM_NUMBER,
                  'X',
                  '99999999',
                  null,
                  'XX',
                  '',
                  '',
                  ccycd,
                  amount,
                  'RM',
                  '',
                  'XX',
                  'XX',
                  '',
                  transdate,
                  '',
                  'O',
                  'RM',
                  'IBPS',
                  transdate,
                  'SIBS-BR',
                  ISO.RM_NUMBER,
                  iNDATE -- 12 so
             from IBPS_SIBS_OL2 ISO -- IBPS_TR
            where (status = 1 or status = -1)
              and ISO.Senddate = to_char(pdate, 'YYYYMMDD')
              and (ISO.RECEIVER = 'IBPS' or Trim(ISO.RECEIVER) is null));
      
      -- lay dien OL4. Co loai tru OL12, OL13
      vttt := reconile_readas400('select t1.RVACCT, t1.RVTAMT, t1.RVTCCY, t1.RVISBR, t1.RVBKRC, t1.RVAPPL,t1.RVSNT6 from SVDATPV51.RMABCS t1 join SVDATPV51.RMMAST t2 on t1.RVACCT=t2.RMACNO Where t1.RVSNT6=' ||
                                 LTRIM(to_char(pDate, 'DDMMYY'), '0') || ' and t2.rmprdc<>''OL12'' and t2.rmprdc<>''OL13''',
                                 '10.2.1.1',
                                 'svoprdwh',
                                 'svoprdwh',
                                 'IBPS',
                                 'O',
                                 'OL4',
                                 to_char(pDate, 'YYYYMMDD'));
    
  
      -- lay dien OL12,OL13
      vttt := reconile_readas400('select t1.RVACCT, t1.RVTAMT, t1.RVTCCY, t1.RVISBR, t1.RVBKRC, t1.RVAPPL,t1.RVSNT6 from SVDATPV51.RMABCS t1 join SVDATPV51.RMMAST t2 on t1.RVACCT=t2.RMACNO Where 
                                 INT(substr(RVSNT7,1,4) || substr(right(''0''||RVSNT6,6),3,2) ||Substr(right(''0''||RVSNT6,6),1,2))>='|| vNDATE ||
                                 ' AND INT(substr(RVSNT7,1,4) || substr(right(''0''||RVSNT6,6),3,2) ||Substr(right(''0''||RVSNT6,6),1,2))<'|| iNDATE || ' and (t2.rmprdc=''OL12'' or t2.rmprdc=''OL13'')',
                                 '10.2.1.1',
                                 'svoprdwh',
                                 'svoprdwh',
                                 'IBPS',
                                 'O',
                                 'OL4',
                                 to_char(pDate, 'YYYYMMDD'));     
      
      -- lay dien ve ibps
      vttt := reconile_readas400('select RMACNO,RMAMT,RMCURR,RMCBNK,RMIBR,RMSTAT,RMPRDC  from SVDATPV51.RMMAST Where rmsts6=' ||
                                 LTRIM(to_char(pDate, 'DDMMYY'), '0') ||
                                 ' and rmprdc=''IL6''',
                                 '10.2.1.1',
                                 'svoprdwh',
                                 'svoprdwh',
                                 'IBPS',
                                 'I',
                                 'IL6',
                                 to_char(pDate, 'YYYYMMDD'));
      ---    het doi chieu qua jdbc  
    
      vttt := reconile_huy('SELECT TLBAFM,TLBCUD, TLBTDT,TLBTCD ,TLBBRC,TLBF01, TLBCOR,TLTXOK, tlbdel ,TLBF04,TLBF03, TLBCUR,TLBCIF,TLBPRD,TLBRFN FROM SVDATPV51.TLLOG WHERE TLBCOR= ''N''  And TLTXOK =''Y'' and tlbdel <>'''' and (TLBAFM like ''%  OL4  %'' or TLBAFM like ''%  OL8  %''  or TLBAFM like ''%  OL12  %''  or TLBAFM like ''%  OL13  %''  or TLBAFM like ''%  OL14  %''  or TLBAFM like ''%  OL15  %'')',
                           '10.2.1.1',
                           'svoprdwh',
                           'svoprdwh');
      delete from ibps_msg_rec_temp
       where rec_type = 'SIBS-BR'
         and to_char(trans_date, 'DDMMYYYY') = To_char(pdate, 'DDMMYYYY')
         and rm_number in (select rm_number from Reconcile_Huy);
     
       -- Huy cac dien OL12 + OL13. Du lieu nam trong bang TLHIST      
      vttt := reconile_huy('SELECT TLBAFM,TLBCUD, TLBTDT,TLBTCD ,TLBBRC,TLBF01, TLBCOR,TLTXOK, tlbdel ,TLBF04,TLBF03, TLBCUR,TLBCIF,TLBPRD,TLBRFN FROM SVDATPV51.TLHIST WHERE TLBCOR= ''N''  And TLTXOK =''Y'' and tlbdel <>'''' and (TLBAFM like ''%  OL12  %''  or TLBAFM like ''%  OL13  %''  or TLBAFM like ''%  OL14  %''  or TLBAFM like ''%  OL15  %'') fetch first 500 row only',
                           '10.2.1.1',
                           'svoprdwh',
                           'svoprdwh');
      delete from ibps_msg_rec_temp
       where rec_type = 'SIBS-BR'
         and to_char(trans_date, 'DDMMYYYY') = To_char(pdate, 'DDMMYYYY')
         and rm_number in (select rm_number from Reconcile_Huy);   
     
    exception
      when others then
        vttt := '';
    end;
    -- delete old data if have
    delete from ibps_msg_rec_temp
     where rec_type = 'TR-BR'
       and to_char(trans_date, 'DDMMYYYY') = To_char(pdate, 'DDMMYYYY');
  
    insert into ibps_msg_rec_temp
      (gw_type,
       rm_number,
       acc_type,
       running_seg_no,
       ref_number,
       msg_type,
       seg_no,
       msg_no,
       ccy,
       amount,
       app_code,
       sender,
       org_bank,
       receiving_branch,
       receiver,
       value_date,
       journal_seg_no,
       msg_direction,
       from_system,
       to_system,
       trans_date,
       rec_type,
       K1,
       NDATE)
      (select 'IBPS',
              sibs_trans_num,
              'X',
              null,
              null,
              'XX',
              '',
              '',
              ccy,
              amount,
              'TR',
              sender,
              'XX',
              'XX',
              receiver,
              trans_date,
              '',
              'O',
              'TR',
              'IBPS',
              trans_date,
              'TR-BR',
              lpad(sibs_trans_num, 12, '0'),
              iNDATE -- 12 so
         from IBPS_TR -- IBPS_TR
        where status > 0
          and msg_direction = 'SIBS-IBPS'
          and to_char(trans_date, 'DDMMYYYY') = to_char(pdate, 'DDMMYYYY'));
    commit; -- commit all
  End InsertIBPS_MSG_REC_TEMP;

  -- Insert vao bang IBPS_MSG_CONTENT_TAD
  -- HoangLA
  Procedure InsertIBPS_MSG_CONTENT_TAD(pDate in date, pTAD varchar2) Is
    iNDATE number := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    -- Delete Old Result If Have
    delete from IBPS_MSG_CONTENT_TAD
     where Ltrim(TAD, '0') = Ltrim(pTad, '0')
       and NDATE = iNDATE; -- NDATE
    -- Insert: Content
    insert into IBPS_MSG_CONTENT_TAD
      (msg_id,
       department,
       trans_code,
       sender,
       receiver,
       ccy,
       amount,
       trans_date,
       msg_direction,
       status,
       tad,
       gw_trans_num,
       K1,
       NDATE,
       QUERY_ID,
       MSG_SRC,
       RM_NUMBER,
       F21,
       F22)
      select msg_id,
             department,
             trans_code,
             F07,
             F22,
             ccycd,
             amount,
             pDate
             /*trans_date*/,
             msg_direction,
             status,
             ltrim(tad, '0'),
             GW_PK_LIB.GET_IBPS_Field(CONTENT, '110') gw_trans_num,
             to_char(trans_date, 'YYYYMMDD') ||
             lpad(GW_PK_LIB.GET_IBPS_Field(CONTENT, '110'), 8, '0') K1,
             to_number(to_char(pDate, 'YYYYMMDD')),
             QUERY_ID,
             MSG_SRC,
             Ltrim(RM_NUMBER, '0'),
             F21,
             F22
        from ibps_msg_content
       where status > -2
         and ltrim(TAD, '0') = ltrim(pTAD, '0')
         and (To_number(To_char(Receiving_Time, 'YYYYMMDD')) = iNDATE
             
             or transdate = iNDATE)
      /*and transdate = iNdate*/
      ;
  
    insert into IBPS_MSG_CONTENT_TAD
      (msg_id,
       department,
       trans_code,
       sender,
       receiver,
       ccy,
       amount,
       trans_date,
       msg_direction,
       status,
       tad,
       gw_trans_num,
       K1,
       NDATE,
       QUERY_ID,
       MSG_SRC,
       RM_NUMBER,
       F21,
       F22)
      select msg_id,
             department,
             trans_code,
             F07,
             F22,
             ccycd,
             amount,
             pdate,
             msg_direction,
             status,
             ltrim(tad, '0'),
             GW_PK_LIB.GET_IBPS_Field(CONTENT, '110') gw_trans_num,
             to_char(trans_date, 'YYYYMMDD') ||
             lpad(GW_PK_LIB.GET_IBPS_Field(CONTENT, '110'), 8, '0') K1,
             to_number(to_char(pDate, 'YYYYMMDD')),
             QUERY_ID,
             MSG_SRC,
             Ltrim(RM_NUMBER, '0'),
             F21,
             F22
        from ibps_msg_all -- all
       where status > -2
         and ltrim(TAD, '0') = ltrim(pTAD, '0')
            -- and  (ltrim(TAD, '0') = ltrim('00011', '0') or ltrim(TAD, '0') = ltrim('00040', '0'))
            /* and transdate = iNdate*/
         and (To_number(To_char(Receiving_Time, 'YYYYMMDD')) = iNDATE or
             transdate = iNDATE);
  
    /* insert into IBPS_MSG_CONTENT_TAD
    (msg_id,
     department,
     trans_code,
     sender,
     receiver,
     ccy,
     amount,
     trans_date,
     msg_direction,
     status,
     tad,
     gw_trans_num,
     K1,
     NDATE,
     QUERY_ID,
     MSG_SRC,
     RM_NUMBER,
     F21,
     F22)
    select msg_id,
           department,
           trans_code,
           F07,
           F22,
           ccycd,
           amount,
           pdate,
           msg_direction,
           status,
           ltrim(tad, '0'),
           GW_PK_LIB.GET_IBPS_Field(CONTENT, '110') gw_trans_num,
           to_char(trans_date, 'YYYYMMDD') ||
           lpad(GW_PK_LIB.GET_IBPS_Field(CONTENT, '110'), 8, '0') K1,
           to_number(to_char(pDate, 'YYYYMMDD')),
           QUERY_ID,
           MSG_SRC,
           Ltrim(RM_NUMBER, '0'),
           F21,
           F22
      from ibps_msg_all_his -- all
     where status > -2
       and ltrim(TAD, '0') = ltrim(pTAD, '0')
          -- and  (ltrim(TAD, '0') = ltrim('00011', '0') or ltrim(TAD, '0') = ltrim('00040', '0'))
          \* and transdate = iNdate*\
       and ( \*To_number(To_char(Receiving_Time, 'YYYYMMDD')) = iNDATE
                                                                                                                                                                                                                                                                                                                           or*\
            transdate = iNDATE);*/
    commit; -- commit all
  End InsertIBPS_MSG_CONTENT_TAD;

  Procedure RECONCILE_SIBS(pDate date, pTellerID varchar2) Is
    pREC_TIME   date := sysdate;
    iNdate      number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
    vRM_NUMBER  varchar2(20);
    vDEPARTMENT varchar2(20);
    vCCYCD      varchar(5);
    nAmount     Number(19, 2);
  
    nMSG_ID_GW   number(20);
    nMSG_ID_HOST number(20);
  
    cursor Recone_SIBS_GW_Content is
      SELECT *
        FROM IBPS_MSG_CONTENT_TEMP A
       Where A.MSG_DIRECTION = 'SIBS-IBPS'
         AND A.NDATE = iNdate
         AND MSG_SRC in (1, 4, 5)
         And A.Status > -1
      /* And A.Status = 1*/
      ; -- Source
  
    cursor Recone_GW_SIBS_Content is
      SELECT *
        FROM IBPS_MSG_CONTENT_TEMP A
       Where A.MSG_DIRECTION = 'IBPS-SIBS'
         AND NDATE = iNdate
      /*and status = 1*/
      ;
    v_MSG_SISB_GW Recone_SIBS_GW_Content%rowtype;
    v_MSG_GW_SISB Recone_GW_SIBS_Content%rowtype;
    --  pREC_TIME     date := sysdate;
  
  Begin
    -- Delete Result Old If Have
    delete from ibps_msg_rec
     where rec_type = 'SIBS-BR'
       and NDATE = iNdate;
    -- insert msg no exp
    delete from IBPS_REC_one;
    commit;
    OPEN Recone_SIBS_GW_Content;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH Recone_SIBS_GW_Content
        INTO v_MSG_SISB_GW;
      EXIT WHEN Recone_SIBS_GW_Content %notfound;
      BEGIN
        vRM_NUMBER   := trim(v_MSG_SISB_GW.Rm_Number);
        vDEPARTMENT  := v_MSG_SISB_GW.Department;
        vCCYCD       := v_MSG_SISB_GW.Ccy;
        nAmount      := v_MSG_SISB_GW.Amount;
        nMSG_ID_GW   := v_MSG_SISB_GW.Msg_Id;
        nMSG_ID_HOST := 0;
        SELECT B.MSG_ID
          INTO nMSG_ID_HOST
          FROM IBPS_MSG_REC_TEMP B
         WHERE LTRIM(vRM_NUMBER, '0') = LTRIM(B.rm_number, '0')
           AND nAMOUNT = B.AMOUNT
           AND B.APP_CODE = 'RM' -- RM
           AND B.MSG_DIRECTION = 'O' -- Out
           AND B.REC_TYPE = 'SIBS-BR' --Date
           AND B.Ndate = iNDATE -- Date  
           AND MSG_ID NOT IN
               (select w.hs_msgid
                  from IBPS_REC_one w
                 where w.MSG_DIRECTION = 'SIBS-BR')
           And Rownum = 1;
      
        if (nMSG_ID_HOST > 0) then
          insert into IBPS_REC_ONE
            (GW_MSGID, MSG_DIRECTION, hs_msgid)
          Values
            (nMSG_ID_GW, 'SIBS-BR', nMSG_ID_HOST);
          commit;
        end if;
      
      Exception
        when others then
          nMSG_ID_HOST := 0;
      END;
    End loop;
    CLOSE Recone_SIBS_GW_Content;
  
    OPEN Recone_GW_SIBS_Content;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH Recone_GW_SIBS_Content
        INTO v_MSG_GW_SISB;
      EXIT WHEN Recone_GW_SIBS_Content %notfound;
      vRM_NUMBER   := v_MSG_GW_SISB.Rm_Number;
      vDEPARTMENT  := v_MSG_GW_SISB.Department;
      vCCYCD       := v_MSG_GW_SISB.Ccy;
      nAmount      := v_MSG_GW_SISB.Amount;
      nMSG_ID_HOST := v_MSG_GW_SISB.Msg_Id;
      Begin
        SELECT B.MSG_ID
          INTO nMSG_ID_GW
          FROM IBPS_MSG_REC_TEMP B
         WHERE LTRIM(vRM_NUMBER, '0') = LTRIM(B.rm_number, '0')
           AND nAmount = B.AMOUNT
           AND vDEPARTMENT = 'RM'
           AND B.APP_CODE = 'RM' -- RM
              
           AND B.MSG_DIRECTION = 'I' -- Out
           AND B.REC_TYPE = 'SIBS-BR' --Date
           AND B.NDATE = iNDATE
           AND MSG_ID NOT IN
               (select w.gw_msgid
                  from IBPS_REC_one w
                 where w.MSG_DIRECTION = 'BR-SIBS')
           And Rownum = 1;
      
        if (nMSG_ID_GW > 0) then
          insert into IBPS_REC_ONE
            (GW_MSGID, MSG_DIRECTION, hs_msgid)
          Values
            (nMSG_ID_GW, 'BR-SIBS', nMSG_ID_HOST);
          commit;
        end if;
      Exception
        when others then
          nMSG_ID_HOST := 0;
      end;
    End loop;
    CLOSE Recone_GW_SIBS_Content;
  
    -- SIBS -> GW: GS
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       APP_CODE,
       MSG_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       CCY,
       AMOUNT,
       SENDER,
       ORG_BANK,
       RECEIVER,
       RECEIVING_BRANCH,
       VALUE_DATE,
       EXCEPTION_TYPE,
       TAD,
       TRANS_DATE,
       MSG_DIRECTION,
       REC_TYPE,
       FROM_SYSTEM,
       TO_SYSTEM,
       STATUS,
       REC_TIME,
       TELLER_ID,
       NDATE,
       QUERY_ID,
       K1) -- User
    
      (SELECT 'IBPS',
              'RM',
              'XXXX',
              LTRIM(rm_number, '0'),
              'R',
              null,
              CCY,
              NVL(AMOUNT, 0),
              SENDER,
              F21,
              RECEIVER,
              F22,
              TRANS_DATE,
              'GS',
              TAD,
              TRANS_DATE,
              'O',
              'SIBS-BR',
              'SIBS',
              'IBPS',
              STATUS,
              pREC_TIME,
              pTellerID,
              iNDATE, -- User,
              QUERY_ID,
              gw_transnum
         FROM IBPS_MSG_CONTENT_TEMP --Content
        WHERE MSG_ID not in
              (SELECT GW_MSGID
                 FROM IBPS_REC_ONE
                where MSG_DIRECTION = 'SIBS-BR')
          AND status > -2
          AND MSG_DIRECTION = 'SIBS-IBPS' -- Out
          AND MSG_SRC in (1, 4, 5)
          AND DEPARTMENT = 'RM' -- RM
          AND NDATE = iNDATE -- Date
       
       /*AND MSG_SRC = 1*/
       );
  
    -- SIBS -> GW: S
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       MSG_TYPE,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       MSG_DIRECTION,
       STATUS,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID)
      (SELECT 'IBPS',
              LTRIM(rm_number, '0'),
              ACC_TYPE,
              ref_number,
              MSG_TYPE,
              CCY,
              AMOUNT,
              APP_CODE,
              SENDER,
              ORG_BANK,
              RECEIVING_BRANCH,
              RECEIVER,
              VALUE_DATE,
              MSG_DIRECTION,
              'S' -- Send
             ,
              'SIBS' FROM_SYSTEM,
              'IBPS' TO_SYSTEM,
              iNDATE,
              TRANS_DATE,
              'S',
              'SIBS-BR',
              pREC_TIME,
              pTellerID
         FROM IBPS_MSG_REC_TEMP -- F
        WHERE MSG_ID not in
              (SELECT HS_MSGID
                 FROM IBPS_REC_ONE
                where MSG_DIRECTION = 'SIBS-BR')
          AND Upper(MSG_DIRECTION) = 'O'
          AND Upper(APP_CODE) = 'RM'
          AND REC_TYPE = 'SIBS-BR'
          AND NDATE = iNDATE -- Date
       );
    commit;
    -- SIBS <- GW: GS
    -- insert msg no exp
    /*  delete from ibps_rec_one;
    insert into IBPS_REC_ONE
      (GW_MSGID, HS_MSGID)
      (SELECT A.MSG_ID, B.MSG_ID
         FROM IBPS_MSG_CONTENT_TEMP A, IBPS_MSG_REC_TEMP B
        WHERE A.NDATE || LTRIM(A.rm_number, '0') =
              B.NDATE || LTRIM(B.rm_number, '0')
          AND A.AMOUNT = B.AMOUNT
          AND A.DEPARTMENT = 'RM'
          AND B.APP_CODE = 'RM' -- RM
          AND A.MSG_DIRECTION = 'IBPS-SIBS' --Out
          AND B.MSG_DIRECTION = 'I' -- Out
          AND B.REC_TYPE = 'SIBS-BR' --Date
          AND A.status > 0
          AND A.NDATE = iNDATE -- Date                       
       UNION
       SELECT -1, -1 FROM DUAL);
    commit;
    SimpleIBPS_REC_ONE('SIBS');*/
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       APP_CODE,
       MSG_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       CCY,
       AMOUNT,
       SENDER,
       ORG_BANK,
       RECEIVER,
       RECEIVING_BRANCH,
       VALUE_DATE,
       EXCEPTION_TYPE,
       TAD,
       TRANS_DATE,
       MSG_DIRECTION,
       REC_TYPE,
       FROM_SYSTEM,
       TO_SYSTEM,
       STATUS,
       REC_TIME,
       TELLER_ID,
       k1,
       NDATE) -- User
    
      (SELECT 'IBPS',
              'RM',
              'XXXX',
              LTRIM(rm_number, '0'),
              'R',
              null,
              CCY,
              NVL(AMOUNT, 0) -- tien
             ,
              SENDER,
              F21,
              RECEIVER,
              F22 -- content
             ,
              TRANS_DATE,
              'GS',
              TAD -- type
             ,
              TRANS_DATE,
              'I',
              'SIBS-BR',
              'IBPS',
              'SIBS' -- navigator
             ,
              STATUS,
              pREC_TIME,
              pTellerID,
              k1,
              iNDATE -- User
         FROM IBPS_MSG_CONTENT_TEMP --Content
        WHERE MSG_ID not in
              (SELECT HS_MSGID
                 FROM IBPS_REC_ONE
                where MSG_DIRECTION = 'BR-SIBS')
          AND status > 0
          AND MSG_DIRECTION = 'IBPS-SIBS' -- In
          AND Upper(DEPARTMENT) = 'RM' -- RM
          AND NDATE = iNDATE -- Date
       );
  
    -- SIBS <- GW: S
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       MSG_TYPE,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       STATUS,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID)
      (SELECT 'IBPS',
              LTRIM(rm_number, '0'),
              ACC_TYPE,
              ref_number,
              MSG_TYPE,
              CCY,
              AMOUNT,
              APP_CODE,
              SENDER,
              ORG_BANK,
              RECEIVING_BRANCH,
              RECEIVER,
              VALUE_DATE,
              'I',
              'IBPS' FROM_SYSTEM,
              'SIBS' TO_SYSTEM,
              iNDATE,
              'R',
              TRANS_DATE,
              'S',
              'SIBS-BR',
              pREC_TIME,
              pTellerID
         FROM IBPS_MSG_REC_TEMP -- F
        WHERE MSG_ID not in
              (SELECT GW_MSGID
                 FROM IBPS_REC_ONE
                where MSG_DIRECTION = 'BR-SIBS')
          AND Upper(MSG_DIRECTION) = 'I'
          AND Upper(APP_CODE) = 'RM'
          AND REC_TYPE = 'SIBS-BR'
          AND NDATE = iNDATE -- Date
       );
    commit; -- commit all
  End RECONCILE_SIBS;

  Procedure RECONCILE_TR(pDate date, pTellerID varchar2) Is
    pREC_TIME date := sysdate;
    iNdate    number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  Begin
    -- Delete Result Old If Have
    delete from ibps_msg_rec
     where rec_type = 'TR-BR'
       and NDATE = iNDATE;
    -- TR-> GW
    -- insert msg no exp
    delete from ibps_rec_one;
    commit;
    insert into IBPS_REC_ONE
      (GW_MSGID, HS_MSGID)
      (SELECT A.MSG_ID, B.MSG_ID
         FROM IBPS_MSG_CONTENT_TEMP A, IBPS_MSG_REC_TEMP B
        WHERE LTRIM(trim(A.K1), '0') = LTRIM(trim(B.K1), '0') -- K1
          AND Upper(A.DEPARTMENT) = 'TR'
          AND Upper(B.APP_CODE) = 'TR' -- RM
          AND Upper(A.MSG_DIRECTION) = 'SIBS-IBPS' --Out
          AND Upper(B.MSG_DIRECTION) = 'O'
          AND B.REC_TYPE = 'TR-BR'
          AND A.NDATE = iNDATE -- Date
          AND B.NDATE = iNDATE -- Date
          and a.status = 1
       UNION
       SELECT -1, -1 FROM DUAL);
    commit;
    SimpleIBPS_REC_ONE('SIBS');
    -- GR
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       APP_CODE,
       MSG_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       CCY,
       AMOUNT,
       SENDER,
       ORG_BANK,
       RECEIVER,
       RECEIVING_BRANCH,
       VALUE_DATE,
       EXCEPTION_TYPE,
       TRANS_DATE,
       MSG_DIRECTION,
       REC_TYPE,
       FROM_SYSTEM,
       TO_SYSTEM,
       STATUS,
       NDATE,
       REC_TIME,
       TELLER_ID,
       QUERY_ID) -- User
    
      (SELECT 'IBPS',
              'RM',
              'XXXX',
              LTRIM(rm_number, '0'),
              'R',
              null,
              CCY,
              NVL(AMOUNT, 0) -- tien
             ,
              SENDER,
              F21,
              RECEIVER,
              F22 -- content
             ,
              TRANS_DATE,
              'GR' -- type
             ,
              TRANS_DATE,
              'O',
              'TR-BR',
              'SIBS',
              'IBPS' -- navigator
             ,
              STATUS,
              iNDATE,
              pREC_TIME,
              pTellerID, -- User
              QUERY_ID
         FROM IBPS_MSG_CONTENT_TEMP --Content
        WHERE MSG_ID not in (SELECT GW_MSGID FROM IBPS_REC_ONE)
          AND status > -2
          AND MSG_DIRECTION = 'SIBS-IBPS' -- Out
          AND Upper(DEPARTMENT) = 'TR' -- RM
          AND NDATE = iNDATE);
  
    -- TR -> GW (R)
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       MSG_TYPE,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       NDATE,
       STATUS,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID)
    
      (SELECT 'IBPS',
              LTRIM(rm_number, '0'),
              'R',
              K1,
              'XX',
              CCY,
              NVL(AMOUNT, 0),
              'RM',
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              'O',
              'SIBS',
              'IBPS',
              iNDATE,
              'S',
              TRANS_DATE,
              'R',
              'TR-BR',
              pREC_TIME,
              pTellerID
         FROM IBPS_MSG_REC_TEMP -- IBPS_TR
        WHERE MSG_ID not in (SELECT HS_MSGID FROM IBPS_REC_ONE)
             --AND status>0
          AND MSG_DIRECTION = 'O' -- Out
          AND rec_type = 'TR-BR'
          AND NDATE = iNDATE);
    commit; -- commit all
  End RECONCILE_TR;

  Procedure RECONCILE_IBPS(pDate date, pTAD varchar2, pTellerID varchar2) Is
    pREC_TIME date := sysdate;
    iNdate    number(10) := to_number(to_char(pDate, 'YYYYMMDD'));
  
    vRM_NUMBER  varchar2(20);
    vDEPARTMENT varchar2(20);
    vCCYCD      varchar(5);
    nAmount     Number(19, 2);
  
    nMSG_ID_GW   number(20);
    nMSG_ID_HOST number(20);
  
    cursor Recone_SIBS_GW_Content is
      SELECT *
        FROM IBPS_MSG_CONTENT_TAD A
       Where A.MSG_DIRECTION = 'SIBS-IBPS'
         AND A.NDATE = iNdate
         AND status = 1
            --AND (ltrim(trim(A.TAD), '0') = ltrim(trim('00011'), '0') or ltrim(trim(A.TAD), '0') = ltrim(trim('00040'), '0')); -- Source
         AND ltrim(trim(A.TAD), '0') = ltrim(trim(pTad), '0');
    cursor Recone_GW_SIBS_Content is
      SELECT *
        FROM IBPS_MSG_CONTENT_TAD A
       Where A.MSG_DIRECTION = 'IBPS-SIBS'
         AND NDATE = iNdate
         AND ltrim(trim(A.TAD), '0') = ltrim(trim(pTad), '0')
         and status = 1
      --Them theo yeu cau 10062010
      /* And status = 1*/
      ;
    v_MSG_SISB_GW Recone_SIBS_GW_Content%rowtype;
    v_MSG_GW_SISB Recone_GW_SIBS_Content%rowtype;
  
  Begin
    -- Delete Result Old If Have
    delete from ibps_msg_rec
     where rec_type = 'IBPS-BR'
       and NDATE = iNDATE
       and Ltrim(tad, '0') = Ltrim(pTAD, '0');
    -- insert msg no exp
    delete from ibps_rec_one;
    commit;
    OPEN Recone_SIBS_GW_Content;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH Recone_SIBS_GW_Content
        INTO v_MSG_SISB_GW;
      EXIT WHEN Recone_SIBS_GW_Content %notfound;
      BEGIN
        vRM_NUMBER   := v_MSG_SISB_GW.K1;
        vDEPARTMENT  := v_MSG_SISB_GW.Department;
        vCCYCD       := v_MSG_SISB_GW.Ccy;
        nAmount      := v_MSG_SISB_GW.Amount;
        nMSG_ID_GW   := v_MSG_SISB_GW.Msg_Id;
        nMSG_ID_HOST := 0;
        SELECT B.MSG_ID
          INTO nMSG_ID_HOST
          FROM IBPS_MSG_REC_TAD B
         WHERE LTRIM(trim(vRM_NUMBER), '0') = LTRIM(trim(B.K1), '0') -- RM
           AND Upper(B.MSG_DIRECTION) = 'O'
           AND B.REC_TYPE = 'IBPS-BR'
           AND B.NDATE = iNDATE -- Date
           And B.Amount = nAmount
              
           AND MSG_ID NOT IN
               (select w.hs_msgid
                  from IBPS_REC_one w
                 where w.MSG_DIRECTION = 'BR-IBPS')
           And Rownum = 1;
      
        if (nMSG_ID_HOST > 0) then
          insert into IBPS_REC_ONE
            (GW_MSGID, MSG_DIRECTION, hs_msgid)
          Values
            (nMSG_ID_GW, 'BR-IBPS', nMSG_ID_HOST);
          commit;
        end if;
      
      Exception
        when others then
          nMSG_ID_HOST := 0;
      END;
    End loop;
    CLOSE Recone_SIBS_GW_Content;
  
    OPEN Recone_GW_SIBS_Content;
    LOOP
      -- L?y t?ng dong d? li?u c?a cursor d? x? ly 
      FETCH Recone_GW_SIBS_Content
        INTO v_MSG_GW_SISB;
      EXIT WHEN Recone_GW_SIBS_Content %notfound;
      vRM_NUMBER   := v_MSG_GW_SISB.k1;
      vDEPARTMENT  := v_MSG_GW_SISB.Department;
      vCCYCD       := v_MSG_GW_SISB.Ccy;
      nAmount      := v_MSG_GW_SISB.Amount;
      nMSG_ID_HOST := v_MSG_GW_SISB.Msg_Id;
    
      Begin
        SELECT B.MSG_ID
          INTO nMSG_ID_GW
          FROM IBPS_MSG_REC_TAD B
         WHERE LTRIM(trim(vRM_NUMBER), '0') = LTRIM(trim(B.K1), '0') -- RM
           AND Upper(B.MSG_DIRECTION) = 'I'
           AND B.REC_TYPE = 'IBPS-BR'
           AND ltrim(trim(B.TAD), '0') = ltrim(trim(pTad), '0')
              --AND (ltrim(trim(b.TAD), '0') = ltrim(trim('00011'), '0') or ltrim(trim(b.TAD), '0') = ltrim(trim('00040'), '0'))
           AND B.NDATE = iNDATE -- Date
              
           AND MSG_ID NOT IN
               (select w.gw_msgid
                  from IBPS_REC_one w
                 where w.MSG_DIRECTION = 'IBPS-BR')
           And Rownum = 1;
      
        if (nMSG_ID_GW > 0) then
          insert into IBPS_REC_ONE
            (GW_MSGID, MSG_DIRECTION, hs_msgid)
          Values
            (nMSG_ID_GW, 'IBPS-BR', nMSG_ID_HOST);
          commit;
        end if;
      Exception
        when others then
          nMSG_ID_HOST := 0;
      end;
    End loop;
    CLOSE Recone_GW_SIBS_Content;
  
    -- GW -> IBPS GI
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       APP_CODE,
       TAD,
       VALUE_DATE,
       rm_number,
       ACC_TYPE,
       ref_number,
       CCY,
       AMOUNT,
       MSG_TYPE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       EXCEPTION_TYPE,
       TRANS_DATE,
       REC_TYPE,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       STATUS,
       NDATE,
       K1,
       FW_ID,
       REC_TIME,
       TELLER_ID,
       QUERY_ID) -- User
    
      (SELECT 'IBPS',
              trim(DEPARTMENT),
              pTad,
              TRANS_DATE,
              LTRIM(rm_number, '0'),
              'R',
              GW_TRANS_NUM,
              CCY,
              NVL(AMOUNT, 0),
              'XXXX',
              SENDER,
              F21,
              F22,
              RECEIVER,
              'GI',
              TRANS_DATE,
              'IBPS-BR',
              'O',
              'SIBS',
              'IBPS',
              STATUS,
              iNDATE,
              K1,
              MSG_ID,
              pREC_TIME,
              pTellerID,
              QUERY_ID -- User
         FROM IBPS_MSG_CONTENT_TAD --Content
        WHERE MSG_ID not in
              (SELECT GW_MSGID
                 FROM IBPS_REC_ONE g
                where g.msg_direction = 'BR-IBPS')
          AND status = 1
          AND MSG_DIRECTION = 'SIBS-IBPS' -- Out
          AND ltrim(trim(TAD), '0') = ltrim(trim(pTad), '0')
             --     AND Upper(DEPARTMENT) = 'RM' -- RM
             -- AND (ltrim(trim(TAD), '0') = ltrim(trim('00011'), '0') or ltrim(trim(TAD), '0') = ltrim(trim('00040'), '0'))
          AND NDATE = iNDATE);
  
    -- GW-> IBPS (I)
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       MSG_TYPE,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       MSG_DIRECTION,
       STATUS,
       NDATE,
       FROM_SYSTEM,
       TO_SYSTEM,
       TAD,
       FW_ID,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       K1)
    
      (SELECT 'IBPS',
              '',
              'R',
              '',
              'XX',
              CCY,
              NVL(AMOUNT, 0),
              APP_CODE,
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              'O',
              'R',
              iNDATE,
              'SIBS',
              'IBPS',
              pTad,
              MSG_ID,
              TRANS_DATE,
              'I',
              'IBPS-BR',
              pREC_TIME,
              pTellerID,
              K1
         FROM IBPS_MSG_REC_TAD -- REC_TAD
        WHERE MSG_ID not in
              (SELECT HS_MSGID
                 FROM IBPS_REC_ONE
                WHERE msg_direction = 'BR-IBPS')
          AND MSG_DIRECTION = 'O' -- Out
          AND ltrim(trim(TAD), '0') = ltrim(trim(pTad), '0')
             --  AND (ltrim(trim(TAD), '0') = ltrim(trim('00011'), '0') or ltrim(trim(TAD), '0') = ltrim(trim('00040'), '0'))
          AND NDATE = iNDATE);
  
    -- GW<-IBPS GI
    /*   delete from ibps_rec_one;
    insert into IBPS_REC_ONE
      (GW_MSGID, TAD_MSGID)
      (SELECT A.MSG_ID, B.MSG_ID
         FROM IBPS_MSG_CONTENT_TAD A, IBPS_MSG_REC_TAD B
        WHERE LTRIM(trim(A.K1), '0') = LTRIM(trim(B.K1), '0') -- RM
             --AND   Upper(A.DEPARTMENT)='RM'
             --AND   Upper(B.APP_CODE)='RM'
          AND Upper(A.MSG_DIRECTION) = 'IBPS-SIBS' -- In
          AND Upper(B.MSG_DIRECTION) = 'I'
          AND B.REC_TYPE = 'IBPS-BR'
          AND ltrim(trim(A.TAD), '0') = ltrim(trim(pTad), '0')
          AND ltrim(trim(B.TAD), '0') = ltrim(trim(pTad), '0')
          AND A.NDATE = iNDATE -- Date
          AND B.NDATE = iNDATE -- Date
       UNION
       SELECT -1, -1 FROM DUAL);
    commit;
    SimpleIBPS_REC_ONE('IBPS');*/
  
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       APP_CODE,
       TAD,
       VALUE_DATE,
       rm_number,
       ACC_TYPE,
       ref_number,
       CCY,
       AMOUNT,
       MSG_TYPE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       EXCEPTION_TYPE,
       TRANS_DATE,
       REC_TYPE,
       MSG_DIRECTION,
       FROM_SYSTEM,
       TO_SYSTEM,
       STATUS,
       NDATE,
       K1,
       FW_ID,
       REC_TIME,
       TELLER_ID,
       QUERY_ID) -- User
    
      (SELECT 'IBPS',
              DEPARTMENT,
              pTad,
              TRANS_DATE,
              LTRIM(rm_number, '0'),
              'R',
              GW_TRANS_NUM,
              CCY,
              NVL(AMOUNT, 0),
              'XXXX',
              SENDER,
              SENDER,
              SENDER,
              RECEIVER,
              'GI',
              TRANS_DATE,
              'IBPS-BR',
              'I',
              'IBPS',
              'SIBS',
              STATUS,
              iNDATE,
              K1,
              MSG_ID,
              pREC_TIME,
              pTellerID,
              QUERY_ID -- User
         FROM IBPS_MSG_CONTENT_TAD -- Content
        WHERE MSG_ID not in
              (SELECT HS_MSGID
                 FROM IBPS_REC_ONE
                where msg_direction = 'IBPS-BR')
          AND status > -1
          AND MSG_DIRECTION = 'IBPS-SIBS' -- In
             --  AND (ltrim(trim(TAD), '0') = ltrim(trim('00011'), '0') or ltrim(trim(TAD), '0') = ltrim(trim('00040'), '0'))
          AND ltrim(trim(TAD), '0') = ltrim(trim(pTad), '0')
          AND NDATE = iNDATE);
  
    -- GW<-IBPS I
    INSERT INTO IBPS_MSG_REC
      (GW_TYPE,
       rm_number,
       ACC_TYPE,
       ref_number,
       MSG_TYPE,
       CCY,
       AMOUNT,
       APP_CODE,
       SENDER,
       ORG_BANK,
       RECEIVING_BRANCH,
       RECEIVER,
       VALUE_DATE,
       MSG_DIRECTION,
       STATUS,
       NDATE,
       FROM_SYSTEM,
       TO_SYSTEM,
       TAD,
       TRANS_DATE,
       EXCEPTION_TYPE,
       REC_TYPE,
       REC_TIME,
       TELLER_ID,
       K1)
    
      (SELECT 'IBPS',
              '',
              'R',
              '',
              'XX',
              CCY,
              NVL(AMOUNT, 0),
              'XX',
              SENDER,
              SENDER,
              RECEIVER,
              RECEIVER,
              TRANS_DATE,
              'I',
              'S',
              iNDATE -- send
             ,
              'IBPS',
              'SIBS',
              pTad,
              TRANS_DATE,
              'I',
              'IBPS-BR',
              pREC_TIME,
              pTellerID,
              K1
         FROM IBPS_MSG_REC_TAD --Content
        WHERE MSG_ID not in
              (SELECT GW_MSGID
                 FROM IBPS_REC_ONE
                where msg_direction = 'IBPS-BR')
             --AND status>-2
          AND MSG_DIRECTION = 'I' -- In
             -- AND (ltrim(trim(TAD), '0') = ltrim(trim('00011'), '0') or ltrim(trim(TAD), '0') = ltrim(trim('00040'), '0'))
          AND ltrim(trim(TAD), '0') = ltrim(trim(pTad), '0')
             --AND Upper(DEPARTMENT)='RM' -- RM
          AND NDATE = iNDATE);
    commit; -- Commit all;
  End RECONCILE_IBPS;

  function ExecuteSQLNonReturn(strSQL varchar2) return boolean as
    err varchar2(1000);
  begin
    execute immediate strSQL;
    commit;
    return true;
  Exception
    when others then
      err := 'Sai loi' || sqlcode || sqlerrm;
      return false;
  end ExecuteSQLNonReturn;

  function ExecuteSQLReturn(strSQL varchar2) return varchar2 AS
    m_vReturn varchar2(1000);
  begin
    execute immediate strSQL
      into m_vReturn;
    commit;
    return m_vReturn;
  Exception
    when others then
      return null;
  end ExecuteSQLReturn;

  Procedure SimpleIBPS_REC_ONE(vType varchar2) Is
    -- Cursor 1
    cursor curIBPS_C1 is
      select distinct GW_MSGID from IBPS_REC_ONE;
    rowIBPS_C1 curIBPS_C1%Rowtype;
    -- Cursor 2
    cursor curIBPS_C2 is
      select distinct HS_MSGID from IBPS_REC_ONE;
    rowIBPS_C2 curIBPS_C2%Rowtype;
    -- Cursor 3
    cursor curIBPS_C3 is
      select distinct TAD_MSGID from IBPS_REC_ONE;
    rowIBPS_C3 curIBPS_C3%Rowtype;
  
    -- Cac bien ho tro Simple
    minRowID  varchar2(100);
    thisMSGID number(30);
  
  Begin
    -- Simple C1
    open curIBPS_C1;
    LOOP
      Fetch curIBPS_C1
        into rowIBPS_C1;
      Exit When curIBPS_C1%notfound;
      thisMSGID := rowIBPS_C1.Gw_Msgid;
      -- minRowID
      Begin
        select min(rowid)
          into minRowID
          from IBPS_REC_ONE
         where GW_MSGID = thisMSGID;
      Exception
        when others then
          minRowID := '0';
      End;
      -- delete duplicate value
      if minRowID <> '0' then
        delete from IBPS_REC_ONE
         where gw_msgid = thisMSGID
           and rowid <> minRowID;
      end if;
    END LOOP;
    close curIBPS_C1;
    commit;
    if vType = 'SIBS' then
      -- Simple C2
      open curIBPS_C2;
      LOOP
        Fetch curIBPS_C2
          into rowIBPS_C2;
        Exit When curIBPS_C2%notfound;
        thisMSGID := rowIBPS_C2.HS_Msgid;
        -- minRowID
        Begin
          select min(rowid)
            into minRowID
            from IBPS_REC_ONE
           where hs_msgid = thisMSGID;
        Exception
          when others then
            minRowID := '0';
        End;
        -- delete duplicate value
        if minRowID <> '0' then
          delete from IBPS_REC_ONE
           where hs_msgid = thisMSGID
             and rowid <> minRowID;
        end if;
      END LOOP;
      close curIBPS_C2;
    End If;
    commit;
    if vType = 'IBPS' then
      -- Simple C3
      open curIBPS_C3;
      LOOP
        Fetch curIBPS_C3
          into rowIBPS_C3;
        Exit When curIBPS_C3%notfound;
        thisMSGID := rowIBPS_C3.TAD_Msgid;
        -- minRowID
        Begin
          select min(rowid)
            into minRowID
            from IBPS_REC_ONE
           where tad_msgid = thisMSGID;
        Exception
          when others then
            minRowID := '0';
        End;
        -- delete duplicate value
        if minRowID <> '0' then
          delete from IBPS_REC_ONE
           where tad_msgid = thisMSGID
             and rowid <> minRowID;
        end if;
      END LOOP;
      close curIBPS_C3;
    End If;
    commit;
  
  End SimpleIBPS_REC_ONE;

end GW_PK_REC_IBPS;
/
