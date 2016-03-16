CREATE OR REPLACE FUNCTION Update_BEN_ACC(strSQL    varchar2,
                                    host      varchar2,
                                    user      varchar2,
                                    pwd       varchar2,
                                    rmNumber varchar2) return varchar2 AS
  LANGUAGE JAVA NAME 'UpdateBenefitAcc.UpdateBenefitAcc(java.lang.String,
                                    java.lang.String,
                                    java.lang.String,
                                    java.lang.String,
                                    java.lang.String) return java.lang.String ';
/
