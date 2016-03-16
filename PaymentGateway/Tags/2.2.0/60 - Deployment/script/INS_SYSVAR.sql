prompt Importing table sysvar...
set feedback off
set define off
insert into sysvar (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
values (2236, 'IBPS', 'IBPS_OL2_CHECKPOINT_SEC', '2013207155756', 'STRING ', '20111007', null);

insert into sysvar (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
values (2237, 'IBPS', 'IBPS_OL2_CHECKPOINT_SEC1', '110214050983915', 'STRING ', '20111007', null);

insert into sysvar (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
values (2256, 'IBPS', 'IBPS_SEC_CUTOFF_TIME', '163000', 'int', 'IBPS_SEC_TIME', 'Gio cut off time cua SEC');

prompt Done.
