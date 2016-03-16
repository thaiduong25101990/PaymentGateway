-- Create table
create table IBPS_TAD_MAP
(
  bank_code    VARCHAR2(3) not null,
  tad_code     VARCHAR2(5),
  updatetime   DATE,
  note         NVARCHAR2(200),
  bank_name    NVARCHAR2(200),
  gw_bank_code VARCHAR2(8)
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table IBPS_TAD_MAP
  is 'Minhhb Tao dung cho viec map cong';
-- Add comments to the columns 
comment on column IBPS_TAD_MAP.bank_code
  is 'Ma Ngan hang huong';
comment on column IBPS_TAD_MAP.tad_code
  is 'Ma cong Citad';
comment on column IBPS_TAD_MAP.updatetime
  is 'Ngay cap nhat';
comment on column IBPS_TAD_MAP.note
  is 'Nguoi cap nhat + Thong tin chu y';
comment on column IBPS_TAD_MAP.bank_name
  is 'Ten ngan hang huong';
comment on column IBPS_TAD_MAP.gw_bank_code
  is 'Ma tuong ung F21';
-- Create/Recreate primary, unique and foreign key constraints 
alter table IBPS_TAD_MAP
  add constraint PR_IBPS_TAD_MAP primary key (BANK_CODE)
  using index 
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
