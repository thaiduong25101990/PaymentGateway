-- Create table
create table GW_WEEKEND_WORKING
(
  id           NUMBER(10) not null,
  working_date DATE,
  des          VARCHAR2(200),
  type         NUMBER(2) default 0
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
-- Create/Recreate primary, unique and foreign key constraints 
alter table GW_WEEKEND_WORKING
  add constraint PK_GW_WEEKEND_WORKING primary key (ID)
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
