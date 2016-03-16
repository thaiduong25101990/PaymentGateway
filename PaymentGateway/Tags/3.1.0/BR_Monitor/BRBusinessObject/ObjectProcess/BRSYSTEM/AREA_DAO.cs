using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using BR.DataAccess;
using System.Data.OracleClient;
namespace BR.BRBusinessObject
{
   public  class AREADP
    {
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();

        public static AREADP Instance()
        {
            return new AREADP();
        }

        public DataSet GetAREA(string strCondition)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "Select Area.ID, Area.PROV_CODE, Area.SHORTNAME, Area.FULLNAME,Area.CITAD_MEMBER,";
            strSQL = strSQL + " (select a.content from Allcode a where upper(trim(a.cdname))='AREA' and trim(a.cdval)= trim(Area.CITAD_MEMBER)) as CITAD_MEMBER  from Area ";            
            if (strCondition.Trim() != "")
            {
                strSQL = strSQL + " where " + strCondition + " order by Area.PROV_CODE";
            }
            else
            {
                strSQL = strSQL + " order by Area.PROV_CODE";
            }

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch //(Exception ex)
            {
                oraConn.Dispose();
                return null;                
            }
            finally
            {
                oraConn.Dispose();
            }
        }

       //Rem for later use
        //public DataSet GetAREA(AREAInfo objAREAInfo)
        //{
        //    OracleParameter[] oraParam = { 
        //                                 {new OracleParameter("pPROV_CODE", OracleType.Number , 5),
        //                                 new OracleParameter("pSHORTNAME", OracleType.NVarChar, 5),                                         
        //                                 new OracleParameter("pFULLNAME", OracleType.NVarChar, 100),
        //                                 new OracleParameter("pCITAD_MEMBER", OracleType.Char , 1)};
        //                                };
           
        //    oraParam[0].Value = objAREAInfo.PROV_CODE;
        //    oraParam[1].Value = objAREAInfo.SHORTNAME;
        //    oraParam[2].Value = objAREAInfo.FULLNAME;
        //    oraParam[3].Value = objAREAInfo.CITAD_MEMBER;
           
        //    oraConn = conn.Connect();
        //    if (oraConn == null)
        //    {
        //        return null;
        //    }

        //    string strSQL = "Select trim(to_char(Area.PROV_CODE,'00')) , Area.SHORTNAME, Area.FULLNAME, Area.CITAD_MEMBER, Allcode.CONTENT  from Area, Allcode ";
        //    strSQL = strSQL + " Where Area.CITAD_MEMBER=Allcode.CDVAL and upper(Allcode.CDNAME)='AREA' ";
        //    strSQL = " and PROV_CODE like :pPROV_CODE and SHORTNAME like :pSHORTNAME and FULLNAME like :pFULLNAME and CITAD_MEMBER like :pCITAD_MEMBER ";
     
        //    try
        //    {
        //        return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

        //    }
        //    catch (Exception ex)
        //    {
        //        oraConn.Dispose();
        //        return null;
        //    }
        //    finally
        //    {
        //        oraConn.Dispose();
        //    }
        //}

        public int InsertAREA(AREAInfo objAREAInfo)
        { 
             string strSql = "Insert into AREA  (PROV_CODE, SHORTNAME, FULLNAME, CITAD_MEMBER) values (:pPROV_CODE,:pSHORTNAME,:pFULLNAME,:pCITAD_MEMBER)";
            OracleParameter[] oraParam = {new OracleParameter("pPROV_CODE", OracleType.VarChar , 6),
                                         new OracleParameter("pSHORTNAME", OracleType.VarChar, 5),                                         
                                         new OracleParameter("pFULLNAME", OracleType.NVarChar, 50),
                                         new OracleParameter("pCITAD_MEMBER", OracleType.Char , 1)};
            oraParam[0].Value = objAREAInfo.PROV_CODE;
            oraParam[1].Value = objAREAInfo.SHORTNAME;
            oraParam[2].Value = objAREAInfo.FULLNAME;
            oraParam[3].Value = objAREAInfo.CITAD_MEMBER;
            
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
               
            }
            finally
            {
                oraConn.Dispose();
            }
        }

       public int UpdateAREA(AREAInfo objAREAInfo)
        {
            string strSql = "Update AREA  set SHORTNAME=:pSHORTNAME, FULLNAME=:pFULLNAME, CITAD_MEMBER=:pCITAD_MEMBER,PROV_CODE=:pPROV_CODE where ID=:pID ";
            OracleParameter[] oraParam = { new OracleParameter("pID", OracleType.Number),
                                         new OracleParameter("pSHORTNAME", OracleType.VarChar, 5),
                                         new OracleParameter("pFULLNAME", OracleType.NVarChar, 50),
                                         new OracleParameter("pCITAD_MEMBER", OracleType.Char , 1),
                                         new OracleParameter("pPROV_CODE", OracleType.VarChar , 6)//,
                                        };

            oraParam[0].Value = objAREAInfo.ID;
            oraParam[1].Value = objAREAInfo.SHORTNAME;
            oraParam[2].Value = objAREAInfo.FULLNAME;
            oraParam[3].Value = objAREAInfo.CITAD_MEMBER;
            oraParam[4].Value = objAREAInfo.PROV_CODE;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                oraConn.Dispose();
            }
        }
       public int DeleteAREA(int iID)
       {
           string strSql = "Delete from AREA where ID=" + iID;
          
           try
           {
               oraConn = conn.Connect();
               if (oraConn == null)
                   return -1;

               return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null );
           }
           catch (Exception ex)
           {
               throw (ex);
               
           }
           finally
           {
               oraConn.Dispose();
           }
       }
       public DataTable Search(string pPROV_CODE)
       {
           string strSql = "select PROV_CODE,SHORTNAME,FULLNAME, CITAD_MEMBER,ID from Area a where Trim(a.FULLNAME)='" + pPROV_CODE + "'";
           try
           {
               oraConn = conn.Connect();
               if (oraConn == null)
                   return null;

               return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
           }
           catch (Exception ex)
           {
               throw (ex);

           }
           finally
           {
               oraConn.Dispose();
           }
       }
       /// <summary>
       /// BichNN add 12/08/2008
       /// Get data from table AREA
       /// </summary>
       /// <returns> DataSet </returns>
       public DataSet GetAREA()
       {
           oraConn = conn.Connect();
           if (oraConn == null)
           {
               return null;
           }

           string strSQL = "Select Area.PROV_CODE PROV_CODE, Area.SHORTNAME SHORTNAME, Area.FULLNAME FULLNAME,";
                  strSQL = strSQL + "Area.CITAD_MEMBER CITAD_MEMBER, Allcode.CONTENT  CONTENT from Area, Allcode ";
                  strSQL = strSQL + " Where Area.CITAD_MEMBER=Allcode.CDVAL and upper(Allcode.CDNAME)='AREA' and citad_member='1'";
           
           try
           {
               return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

           }
           catch //(Exception ex)
           {
               oraConn.Dispose();
               return null;
           }
           finally
           {
               oraConn.Dispose();
           }
       }

       public DataSet GetAREA_GWBankCode(string strGWBankCode)
       {
           oraConn = conn.Connect();
           if (oraConn == null)
           {
               return null;
           }

           string strSQL = "Select Area.PROV_CODE PROV_CODE, Area.SHORTNAME SHORTNAME, Area.FULLNAME FULLNAME,";
           strSQL = strSQL + "Area.CITAD_MEMBER CITAD_MEMBER, Allcode.CONTENT  CONTENT from Area, Allcode ";
            strSQL = strSQL + " Where Area.CITAD_MEMBER=Allcode.CDVAL and upper(Allcode.CDNAME)='AREA' and citad_member='1' and trim(Area.PROV_CODE)='" + strGWBankCode.Substring(0,2)+ "'";

           try
           {
               return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

           }
           catch //(Exception ex)
           {
               oraConn.Dispose();
               return null;
           }
           finally
           {
               oraConn.Dispose();
           }
       }
    }
}
