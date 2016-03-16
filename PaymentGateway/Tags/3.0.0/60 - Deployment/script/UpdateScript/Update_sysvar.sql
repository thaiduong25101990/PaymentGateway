
insert into sysvar (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
values ('2296', 'IBPS', 'IBPS_ACC_TAX_7111', ';711AAAAA;', 'String', 'Ma cac TK di Thue', 'Ma cac TK di Thue');

insert into sysvar (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)
values ('2276', 'IBPS', 'IBPS_ACC_TAX_3942', ';3942***;', 'String', 'Ma cac TK di Thue', 'Ma cac TK di Thue');

insert into error_code (ERROR_CODE, NAME, GWTYPE, DESCRIPTION)
values ('32', 'Tax Code Error', 'SYSTEM', 'Ma CQT khong co tren Citad');

update tad set dblink_name='CITAD_HSC_21' where tad='TAD011';
update tad set dblink_name='CITAD_HCM_21' where tad='TAD040';

insert into Allcode (ID, CDVAL, CDNAME, CONTENT, GWTYPE, DESCRIPTION, LSTORD, DIRECTION)
values ('2050', '8', 'MSG_SRC', 'FROM BDS-TAX', 'IBPS', null, '8', null);


commit;
