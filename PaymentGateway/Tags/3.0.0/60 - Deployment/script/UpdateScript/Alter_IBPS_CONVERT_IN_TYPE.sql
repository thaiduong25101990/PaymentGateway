

begin
  DBMS_AQADM.stop_queue( 'IBPS_Q_CONVERT_IN');
  DBMS_AQADM.drop_queue(queue_name  => 'IBPS_Q_CONVERT_IN',
                        auto_commit => true);

  DBMS_AQADM.DROP_QUEUE_TABLE('IBPS_TB_Q_CONVERT_IN', true, true);
END;
/

drop TYPE IBPS_TYPE_CONVERTIN;

CREATE OR REPLACE TYPE "IBPS_TYPE_CONVERTIN"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              AS OBJECT
(
  FILE_NAME      varchar2(50),
  MSG_TYPE       varchar2(10),
  SIBS_TRANS_NUM varchar2(5),
  IBPS_CONTENT   varchar2(4000),
  CONTENT_TAX   varchar2(4000),
  SYSTEMDATE     DATE
);


-- Create table
begin
  sys.dbms_aqadm.create_queue_table(
    queue_table => 'IBPS_TB_Q_CONVERT_IN',
    queue_payload_type => 'IBPS_TYPE_CONVERTIN',
    sort_list => 'ENQ_TIME',
    compatible => '10.0.0',
    primary_instance => 0,
    secondary_instance => 0,
    storage_clause => 'tablespace USERS pctfree 10 initrans 1 maxtrans 255 storage ( initial 3M minextents 1 maxextents unlimited )');
end;
/

CREATE OR REPLACE TYPE "IBPS_DBLINK_OUT"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   AS OBJECT
--
-- To modify this template, edit file TYPSPEC.TXT in TEMPLATE
-- directory of SQL Navigator
--
-- Purpose: Briefly explain the functionality of the type
--
-- MODIFICATION HISTORY
-- Person      Date    Comments
-- ---------   ------  ------------------------------------------
--
--

(
QUERY_ID number(20),
   F110   VARCHAR2(20),
  F3     VARCHAR2(6),
  F7     VARCHAR2(8),
  F12    VARCHAR2(14),
  F19    VARCHAR2(8),
  F21    VARCHAR2(8),
  F22    VARCHAR2(8),
  F26    VARCHAR2(3),
  F27    VARCHAR2(22),
  F28    VARCHAR2(70),
  F29    VARCHAR2(70),
  F30    VARCHAR2(25),
  F31    VARCHAR2(70),
  F32    VARCHAR2(70),
  F33    VARCHAR2(25),
  F34    VARCHAR2(210),
  F36    VARCHAR2(2),
  F37    VARCHAR2(3),
  F179 VARCHAR2(210),
  F25 VARCHAR2(210),
  F175 VARCHAR2(210))
/


begin
  sys.dbms_aqadm.create_queue(
    queue_name => 'IBPS_Q_CONVERT_IN',
    queue_table => 'IBPS_TB_Q_CONVERT_IN',
    queue_type => sys.dbms_aqadm.normal_queue,
    max_retries => 5,
    retry_delay => 0,
    retention_time => 0);
end;
/
begin
  DBMS_AQADM.start_queue( 'IBPS_Q_CONVERT_IN');
  end;
  /
