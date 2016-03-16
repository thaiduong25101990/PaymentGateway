create or replace procedure INSERT_IBPS_OL2(pRM_NUMBER    VARCHAR2,
                                            pBRANCHCREATE VARCHAR2,
                                            pREMARK       VARCHAR2,
                                            pCONTENT      VARCHAR2,
                                            pCCYCD        VARCHAR2,
                                            pAMOUNT       VARCHAR2,
                                            pRMBENA       VARCHAR2,
                                            pTELLERID     VARCHAR2,
                                            pRMPB40       VARCHAR2,
                                            pSENDERACC    VARCHAR2,
                                            pATTRIBUTE1   VARCHAR2,
                                            pATTRIBUTE2   VARCHAR2,
                                            pATTRIBUTE3   VARCHAR2,
                                            pATTRIBUTE4   VARCHAR2,
                                            pATTRIBUTE5   VARCHAR2,
                                            pATTRIBUTE6   VARCHAR2,
                                            pATTRIBUTE7   VARCHAR2) is
  pragma autonomous_transaction;
  icount int;
begin

  Insert into IBPS_SIBS_OL2
    (RM_NUMBER,
     BRANCHCREATE,
     REMARK,
     CONTENT,
     CCYCD,
     AMOUNT,
     RMBENA,
     TELLERID,
     RMPB40,
     SENDERACC,
     ATTRIBUTE1,
     ATTRIBUTE2,
     ATTRIBUTE3,
     ATTRIBUTE4,
     ATTRIBUTE5,
     ATTRIBUTE6,
     ATTRIBUTE7,
     transdate,
     status)
  values
    (pRM_NUMBER,
     pBRANCHCREATE,
     pREMARK,
     pCONTENT,
     pCCYCD,
     pAMOUNT,
     pRMBENA,
     pTELLERID,
     pRMPB40,
     pSENDERACC,
     pATTRIBUTE1,
     pATTRIBUTE2,
     pATTRIBUTE3,
     pATTRIBUTE4,
     pATTRIBUTE5,
     pATTRIBUTE6,
     pATTRIBUTE7,
     sysdate,
     0);

  IF (pATTRIBUTE5 = 'TAX') then
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_CHECKPOINT';
    if icount > 0 then
      Update sysvar
         set value = pATTRIBUTE2
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_CHECKPOINT';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (3333,
         'IBPS',
         'IBPS_OL2_CHECKPOINT',
         pATTRIBUTE2,
         'String',
         'Check point khi lay dien Nop thue',
         'Check point khi lay dien Nop thue');
    
    end if;
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_CHECKPOINT1';
    if icount > 0 then
      Update sysvar
         set value = pRM_NUMBER
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_CHECKPOINT1';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (3333,
         'IBPS',
         'IBPS_OL2_CHECKPOINT1',
         pRM_NUMBER,
         'String',
         'Check point khi lay dien Nop thue',
         'Check point khi lay dien Nop thue');
    
    end if;
  elsif (pATTRIBUTE5 = 'IB') then
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_CHECKPOINT_IB';
    if icount > 0 then
      Update sysvar
         set value = pATTRIBUTE2
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_CHECKPOINT_IB';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (111112,
         'IBPS',
         'IBPS_OL2_CHECKPOINT_IB',
         pATTRIBUTE2,
         'String',
         'Check point khi lay dien Nop thue',
         'Check point khi lay dien Nop thue');
    
    end if;
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_CHECKPOINT_IB1';
    if icount > 0 then
      Update sysvar
         set value = pRM_NUMBER
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_CHECKPOINT_IB1';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (21231,
         'IBPS',
         'IBPS_OL2_CHECKPOINT_IB1',
         pRM_NUMBER,
         'String',
         'Check point khi lay dien Nop thue',
         'Check point khi lay dien Nop thue');
    
    end if;
  elsif (pATTRIBUTE5 = 'TAXONLINE') then ---- TAXONLINE
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_TAXOL_CHECKPOINT';
    if icount > 0 then
      Update sysvar
         set value = pATTRIBUTE2
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_TAXOL_CHECKPOINT';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (111112,
         'IBPS',
         'IBPS_OL2_TAXOL_CHECKPOINT',
         pATTRIBUTE2,
         'String',
         'Check point khi lay dien Nop thue DIEN TU ONLINE',
         'Check point khi lay dien Nop thue  DIEN TU ONLINE');
    
    end if;
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_TAXOL_CHECKPOINT1';
    if icount > 0 then
      Update sysvar
         set value = pRM_NUMBER
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_TAXOL_CHECKPOINT1';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (21231,
         'IBPS',
         'IBPS_OL2_TAXOL_CHECKPOINT',
         pRM_NUMBER,
         'String',
         'Check point khi lay dien Nop thue DIEN TU ONLINE',
         'Check point khi lay dien Nop thue DIEN TU ONLINE');
    
    end if;
  else
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_CHECKPOINT_SEC';
    if icount > 0 then
      Update sysvar
         set value = pATTRIBUTE2
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_CHECKPOINT_SEC';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (111113,
         'IBPS',
         'IBPS_OL2_CHECKPOINT_SEC',
         pATTRIBUTE2,
         'String',
         'Check point khi lay dien Chung khoan',
         'Check point khi lay dien Chung khoan');
    
    end if;
    select count(1)
      into icount
      from sysvar
     where Gwtype = 'IBPS'
       and varname = 'IBPS_OL2_CHECKPOINT_SEC1';
    if icount > 0 then
      Update sysvar
         set value = pRM_NUMBER
       where Gwtype = 'IBPS'
         and varname = 'IBPS_OL2_CHECKPOINT_SEC1';
    else
      insert into sysvar
        (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
      values
        (21233,
         'IBPS',
         'IBPS_OL2_CHECKPOINT_SEC1',
         pRM_NUMBER,
         'String',
         'Check point khi lay dien Chung khoan',
         'Check point khi lay dien Chung khoan');
    
    end if;
  end if;
  commit;
end INSERT_IBPS_OL2;
/
