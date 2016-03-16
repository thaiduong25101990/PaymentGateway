create or replace and compile java source named updateben as
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.*;
import oracle.jdbc.* ;
public class UpdateBenefitAcc
{
 
    private String strSQLTest = "SELECT TLBAFM,TLBCUD, TLBTDT,TLBTCD ,TLBBRC,TLBF01, TLBCOR,TLTXOK, tlbdel ,TLBF04,TLBF03, TLBCUR,TLBCIF,TLBPRD,TLBRFN FROM SVDATPV51.TLLOG WHERE TLBCOR= 'N'  And TLTXOK ='Y' and tlbdel <>'' and (TLBAFM like '%  OL4  %' or TLBAFM like '%  OL8  %' )";// FETCH FIRST 10 ROWS ONLY";
       
    
       public static boolean Update_IBPS_MSG__CONTENT_BEN_ACC( String sRM_NUMBER,                                                   
                                                   String sAcc,String sName) throws Exception
            {
                try{
                    Connection oraconn =   DriverManager.getConnection("jdbc:default:connection:");
                    // Step-2: identify the stored procedure
                    String proc3StoredProcedure = "{ call STO_UPDATE_BEN_ACC(?, ?,?) }";
                    // Step-3: prepare the callable statement
                    CallableStatement cs = oraconn.prepareCall(proc3StoredProcedure);
                    // Step-4: set input parameters ...
                    // first input argument
                    
                     cs.setString(1, sRM_NUMBER);  
                     cs.setString(2, sAcc); 
                     cs.setString(3, sName);                       
                   
                    // Step-5: register output parameters ...

                    // Step-6: execute the stored procedures: proc3
                    cs.execute();
                    cs.close();
                    oraconn.close();
                   return true;
                }
             catch(Exception e){
             throw new Exception("Loi" +  e.toString());
        }
   }
	 
 public static String UpdateBenefitAcc(String strSQL,
            String host,
            String user,
            String pwd,
            String rmNumber) {
            Connection con = null;
            Statement stmt = null;
            ResultSet rs = null;
            String strReturn = "";
            String sRM_NUMBER;                        
            String sAcc; 
            String sName;  
        try {
        
        
            Class.forName("com.ibm.as400.access.AS400JDBCDriver");
        
            con = DriverManager.getConnection("jdbc:as400://" + host, user, pwd);
            stmt = con.createStatement();
            rs = stmt.executeQuery(strSQL);
            while (rs.next()) { 
						      sRM_NUMBER =rmNumber;//rs.getString("RMACNO").trim();    
                  sAcc=rs.getString("RMBENA").trim(); 
                  sName=rs.getString("REC_NAME").trim();                  
                  Update_IBPS_MSG__CONTENT_BEN_ACC( sRM_NUMBER, sAcc,sName);
            }
            rs.close();
            stmt.close();
            con.close();
        } catch (Exception e) {
            System.out.println("Err ResultSet : " + e.toString());
            strReturn = " failse  " + e.toString()+host+user+pwd;

        }

        return strReturn;
    }



}
/
