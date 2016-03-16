
insert into RPTPARAMETER (RPTNAME, CRLNAME, CRLTYPE, CRLLENGH, CAPTION, SQL, LSTORD, ID, OPTALL)
values ('BM_IBPS23', 'combobox', 'currency', 3, 'Currency', 'select ccycd as COLUMNNAME from currencycode', 4, 962, 1);

insert into RPTPARAMETER (RPTNAME, CRLNAME, CRLTYPE, CRLLENGH, CAPTION, SQL, LSTORD, ID, OPTALL)
values ('BM_IBPS23', 'combobox', 'varchar2', 10, 'Citad Gate', 'select GW_BANK_CODE AS COLUMNNAME  from 
TAD', 3, 963, 1);

insert into RPTPARAMETER (RPTNAME, CRLNAME, CRLTYPE, CRLLENGH, CAPTION, SQL, LSTORD, ID, OPTALL)
values ('BM_IBPS23', 'combobox', 'varchar2', 10, 'Status', 'SELECT NAME AS COLUMNNAME FROM STATUS', 5, 964, 1);

insert into RPTPARAMETER (RPTNAME, CRLNAME, CRLTYPE, CRLLENGH, CAPTION, SQL, LSTORD, ID, OPTALL)
values ('BM_IBPS23', 'picker', 'date', 10, 'From Date', null, 1, 961, 0);

insert into RPTPARAMETER (RPTNAME, CRLNAME, CRLTYPE, CRLLENGH, CAPTION, SQL, LSTORD, ID, OPTALL)
values ('BM_IBPS23', 'picker', 'date', 10, 'To Date', null, 2, 981, 0);


