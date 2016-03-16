-- Add/modify columns 
alter table IBPS_MSG_CONTENT add content_tax varchar2(3000);
alter table IBPS_MSG_CONTENT add desc_tax VARCHAR2(3000);
-- Add/modify columns 
alter table IBPS_MSG_ALL add content_tax varchar2(3000);
alter table IBPS_MSG_ALL add desc_tax VARCHAR2(3000);

-- Add/modify columns 
alter table IBPS_MSG_ALL_HIS add content_tax varchar2(3000);
alter table IBPS_MSG_ALL_HIS add desc_tax VARCHAR2(3000);


 alter table IBPS_TR add is_lnh         VARCHAR2(2);
  alter table IBPS_TR add lnh_transdate  DATE;
  alter table IBPS_TR add lnh_trans_type VARCHAR2(20);
 alter table IBPS_TR add lnh_interest   NUMBER(20,4);
  alter table IBPS_TR add lnh_cycle      VARCHAR2(20);
 alter table IBPS_TR add lnh_currency   VARCHAR2(20);
 alter table IBPS_TR add lnh_gtcg       VARCHAR2(20);

 alter table ATD add DBLINK_NAME       VARCHAR2(20);
