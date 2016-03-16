-- Create table
create table IBPS_RECEIPTBANKMAP
(
  sibs     VARCHAR2(20),
  sibsname VARCHAR2(200),
  f22      VARCHAR2(20),
  f22name  VARCHAR2(200),
  f19      VARCHAR2(20),
  f19name  VARCHAR2(200),
  status   NUMBER(2)
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 256K
    minextents 1
    maxextents unlimited
  );