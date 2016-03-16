create or replace procedure STO_UPDATE_BEN_ACC(pRM_NUMBER VARCHAR2,
                                               pAcc   VARCHAR2,pName varchar2) is
  pragma autonomous_transaction;

  vContent varchar2(1000);
  v031     varchar2(200);
  v033     varchar2(30);
begin
  /*Insert into Test (Text, Content) values ('123', '321');
  commit;*/
  /*v031 := '#031' || trim(Substr(pCONTENT, 558, 40)) || ' ' ||
          trim(Substr(pCONTENT, 972, 30));
  v033 := '#033' || trim(Substr(pCONTENT, 538, 20));*/
  
  v031:='#031'|| pName;
  v033:='#033'||pAcc;
  select t.content
    into vContent
    from Ibps_Msg_Content t
   where t.rm_number = pRM_NUMBER;
  vContent := replace(vContent, '#033.', v033);
  vContent := replace(vContent, '#031.', v031);
  update Ibps_Msg_Content t
     set t.content = vContent, t.status=0
   where t.rm_number = pRM_NUMBER and t.status=99;
  commit;

end STO_UPDATE_BEN_ACC;
/
