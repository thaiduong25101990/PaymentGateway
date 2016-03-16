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
using System.Data.OracleClient;
//using BR.BRLib;
using BR.DataAccess;

//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class TADDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public TADDP()
        {
        }
        public static TADDP Instance()
        {
            return new TADDP();
        }

        public int AddTAD(TADInfo objTable)
        {
            string strSql = "Insert into TAD(TAD,TAD_NAME,GW_BANK_CODE,EXPORT_FOLDER,IMPORT_FOLDER,FTPPATH,FTPUSER,FTPPASS,AREA,";
            strSql = strSql + "CONNECTION,TIME,AMOUNT,CCYCD,STATUS,SIBS_BANK_CODE,SET_LOW_VALUE,FUNCTION,DBLINK,SBV_TADID) ";
                   strSql = strSql + "Values(:pTAD,:pTAD_NAME,:pGW_BANK_CODE,:pEXPORT_FOLDER,:pIMPORT_FOLDER,:pFTPPATH,:pFTPUSER,";
                   strSql = strSql + ":pFTPPASS,:pAREA,:pCONNECTION,:pTIME,:pAMOUNT,:pCCYCD,:pSTATUS,:pSIBS_BANK_CODE,:pSET_LOW_VALUE,";
                   strSql = strSql + ":pFUNCTION,:pDBLINK,:pSBV_TADID)";       
            OracleParameter[] oraParam ={new OracleParameter("pTAD",OracleType.VarChar,50),
                                           new OracleParameter("pTAD_NAME",OracleType.VarChar,50),
                                           new OracleParameter("pGW_BANK_CODE",OracleType.VarChar,12),
                                           new OracleParameter("pEXPORT_FOLDER",OracleType.VarChar,250),
                                           new OracleParameter("pIMPORT_FOLDER",OracleType.VarChar,250),
                                           new OracleParameter("pFTPPATH",OracleType.NVarChar,30),
                                           new OracleParameter("pFTPUSER",OracleType.NVarChar,30),
                                           new OracleParameter("pFTPPASS",OracleType.NVarChar,30),
                                           new OracleParameter("pAREA",OracleType.VarChar,5),
                                           new OracleParameter("pCONNECTION",OracleType.Number,1),
                                           new OracleParameter("pTIME",OracleType.NVarChar,10),
                                           new OracleParameter("pAMOUNT",OracleType.Number,18),
                                           new OracleParameter("pCCYCD",OracleType.Char,3),
                                           new OracleParameter("pSTATUS",OracleType.Number,1),
                                           new OracleParameter("pSIBS_BANK_CODE",OracleType.NVarChar,5),
                                           new OracleParameter("pSET_LOW_VALUE",OracleType.Number,1),
                                           new OracleParameter("pFUNCTION",OracleType.Char,1),
                                           new OracleParameter("pDBLINK",OracleType.NVarChar ,50),
                                           new OracleParameter("pSBV_TADID",OracleType.VarChar,50)
                                        };
            oraParam[0].Value = objTable.TAD;
            oraParam[1].Value = objTable.TAD_NAME;
            oraParam[2].Value = objTable.GW_BANK_CODE;
            oraParam[3].Value = objTable.EXPORT_FOLDER;
            oraParam[4].Value = objTable.IMPORT_FOLDER;
            oraParam[5].Value = objTable.FTPPATH;
            oraParam[6].Value = objTable.FTPUSER;
            oraParam[7].Value = objTable.FPTPASS;
            oraParam[8].Value = objTable.AREA;
            oraParam[9].Value = objTable.CONNECTION;            
            oraParam[10].Value = objTable.TIME;
            oraParam[11].Value = objTable.AMOUNT;
            oraParam[12].Value = objTable.CCYCD;
            oraParam[13].Value = objTable.STATUS;
            oraParam[14].Value = objTable.SIBS_BANK_CODE;
            oraParam[15].Value = objTable.SET_LOW_VALUE;
            oraParam[16].Value = objTable.FUNCTION ;
            oraParam[17].Value = objTable.DBLINK;
            oraParam[18].Value = objTable.SBV_TADID;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
                Create_job();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int UpdateTAD(TADInfo objTable)
        {
            string strSql = "Update TAD set TAD=:pTAD,TAD_NAME=:pTAD_NAME,GW_BANK_CODE=:pGW_BANK_CODE,EXPORT_FOLDER=:pEXPORT_FOLDER,";
                   strSql = strSql + " IMPORT_FOLDER=:pIMPORT_FOLDER,FTPPATH=:pFTPPATH,FTPUSER=:pFTPUSER,FTPPASS=:pFTPPASS,AREA=:pAREA,";
                   strSql = strSql + " CONNECTION=:pCONNECTION,TIME=:pTIME,AMOUNT=:pAMOUNT,CCYCD=:pCCYCD,STATUS=:pSTATUS,";
                   strSql = strSql + " SIBS_BANK_CODE=:pSIBS_BANK_CODE,SET_LOW_VALUE=:pSET_LOW_VALUE,";
                   strSql = strSql + " FUNCTION=:pFUNCTION,DBLINK=:pDBLINK ,SBV_TADID=:pSBV_TADID";
                   strSql = strSql + " where TADID=:pTADID";
            OracleParameter[] oraParam ={  new OracleParameter("pTAD",OracleType.VarChar,50),
                                           new OracleParameter("pTAD_NAME",OracleType.VarChar,50),
                                           new OracleParameter("pGW_BANK_CODE",OracleType.VarChar,12),
                                           new OracleParameter("pEXPORT_FOLDER",OracleType.VarChar,250),
                                           new OracleParameter("pIMPORT_FOLDER",OracleType.VarChar,250),
                                           new OracleParameter("pFTPPATH",OracleType.NVarChar,30),
                                           new OracleParameter("pFTPUSER",OracleType.NVarChar,30),
                                           new OracleParameter("pFTPPASS",OracleType.NVarChar,30),
                                           new OracleParameter("pAREA",OracleType.VarChar,5),
                                           new OracleParameter("pCONNECTION",OracleType.Number,1),
                                           new OracleParameter("pTIME",OracleType.NVarChar,10),
                                           new OracleParameter("pAMOUNT",OracleType.Number,18),
                                           new OracleParameter("pCCYCD",OracleType.Char,3),
                                           new OracleParameter("pSTATUS",OracleType.Number,1),
                                           new OracleParameter("pSIBS_BANK_CODE",OracleType.NVarChar,5),
                                           new OracleParameter("pSET_LOW_VALUE",OracleType.Number,1),
                                           new OracleParameter("pFUNCTION",OracleType.Char,1),
                                           new OracleParameter("pDBLINK",OracleType.NVarChar ,50),
                                           new OracleParameter("pTADID",OracleType.VarChar,50),
                                           new OracleParameter("pSBV_TADID",OracleType.VarChar,50)

                                        };
            oraParam[0].Value = objTable.TAD;
            oraParam[1].Value = objTable.TAD_NAME;
            oraParam[2].Value = objTable.GW_BANK_CODE;
            oraParam[3].Value = objTable.EXPORT_FOLDER;
            oraParam[4].Value = objTable.IMPORT_FOLDER;
            oraParam[5].Value = objTable.FTPPATH;
            oraParam[6].Value = objTable.FTPUSER;
            oraParam[7].Value = objTable.FPTPASS;
            oraParam[8].Value = objTable.AREA;
            oraParam[9].Value = objTable.CONNECTION;
            oraParam[10].Value = objTable.TIME;
            oraParam[11].Value = objTable.AMOUNT;
            oraParam[12].Value = objTable.CCYCD;
            oraParam[13].Value = objTable.STATUS;
            oraParam[14].Value = objTable.SIBS_BANK_CODE;
            oraParam[15].Value = objTable.SET_LOW_VALUE;
            oraParam[16].Value = objTable.FUNCTION;
            oraParam[17].Value = objTable.DBLINK;
            oraParam[18].Value = objTable.TADID ;
            oraParam[19].Value = objTable.SBV_TADID;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
                Create_job();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Create_job()
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return 0;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "gw_pk_ibps_dblink.Create_Job", null);
            }
            catch
            {
                return -1;
            }
        }
        public DataSet GetTAD1()
        {
            //Select TAD,SIBS_BANK_CODE from TAD   
            string strSql = "Select TAD,SIBS_BANK_CODE from TAD";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetTAD2()
        {
            //Select TAD,SIBS_BANK_CODE from TAD   
            string strSql = "Select  replace(T.TAD,'TAD') as TAD,SIBS_BANK_CODE from TAD T order by TAD  ASC";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataSet GetTAD()
        {
            
            string strSql = "Select td.TADID,td.TAD,td.TAD_NAME,td.GW_BANK_CODE,td.EXPORT_FOLDER,td.IMPORT_FOLDER,";
                   strSql = strSql + "td.FTPPATH,td.FTPUSER,td.FTPPASS,td.AREA,td.TIME,td.AMOUNT,td.STATUS,td.SIBS_BANK_CODE,";
                   strSql = strSql + "td.SET_LOW_VALUE,bankname.bank_name,branchname.BRAN_NAME from TAD td,"; 
                   strSql = strSql + "(select ibm.sibs_bank_code,ibm.gw_bank_code,ibm.bank_name from ibps_bank_map ibm) bankname,";
                   strSql = strSql + "(select br.SIBS_BANK_CODE,br.BRAN_NAME from branch br) branchname ";
                   strSql = strSql + " where td.sibs_bank_code = bankname.sibs_bank_code(+)";
                   strSql = strSql + "and Substr(td.sibs_bank_code,3)= branchname.sibs_bank_code(+)";
                   strSql = strSql + "order by td.TAD_NAME";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable Search_Sbv(string pSBV_TADID, string pGWBankCode, string strWhere)
        {

            //string strSql = "select T.TAD_NAME from TAD T where trim(SBV_TADID)='" + pSBV_TADID + "'";
            string strSql = "select T.TAD_NAME from TAD T where trim(SBV_TADID)='" + pSBV_TADID.Trim() + "' and trim(Area)='" + pGWBankCode.Substring(0, 2) + "' " + strWhere;
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
        }

        public DataSet GetTADInfo()//Quynd Update 30/08/2008(thay Status va Erea bang cach map den bang allcode de lay ra ten: khong hien thi dang so nhu cu nua)
        {            
            //string strSql = "Select td.TADID,td.TAD,td.TAD_NAME,td.GW_BANK_CODE,td.EXPORT_FOLDER,td.IMPORT_FOLDER,";
            //strSql = strSql + "  td.FTPPATH,td.FTPUSER,td.ftppass,(select a.fullname from area a where trim(a.prov_code)= trim(td.area))as Area";
            //strSql = strSql + " ,td.TIME,td.AMOUNT,";
            //strSql = strSql + " (select a.content from Allcode a where trim(a.cdname)='CITADSTS' and trim(a.cdval)= trim(td.STATUS)) as STATUS";
            //strSql = strSql + " ,substr(td.sibs_bank_code,-3) as sibs_bank_code,td.SET_LOW_VALUE,bankname.bank_name,bankname.bank_name as BRAN_NAME,td.CCYCD,(select a.content from Allcode a where trim(a.cdname) = 'Connection' and trim(a.cdval) = trim(td.connection)) as Connection,(select a.content from Allcode a where trim(a.cdname) = 'FUNCTION' and a.gwtype='IBPS' and trim(a.cdval) = trim(td.Function)) as FUNCTION,td.DBLink,td.SBV_TADID from TAD td,";
            //strSql = strSql + " (select ibm.sibs_bank_code,ibm.gw_bank_code,ibm.bank_name from ibps_bank_map ibm) bankname ";
            //strSql = strSql + "  where trim(td.gw_bank_code) ";
            //strSql = strSql + " = bankname.gw_bank_code(+) order by td.TAD_NAME";


            string strSql = "Select td.TADID,td.TAD, td.TAD_NAME,td.GW_BANK_CODE, td.EXPORT_FOLDER, td.IMPORT_FOLDER,td.FTPPATH,  td.FTPUSER, td.ftppass, (select a.fullname ";
            strSql = strSql + "   from area a  where trim(a.prov_code) = trim(td.area)) as Area,td.TIME,  td.AMOUNT,(select a.content  from Allcode a where trim(a.cdname) = 'CITADSTS' ";
            strSql = strSql + "  and trim(a.cdval) = trim(td.STATUS)) as STATUS, substr(td.sibs_bank_code, -3) as sibs_bank_code,td.SET_LOW_VALUE, (select  ibm.bank_name ";
            strSql = strSql + "  from ibps_bank_map ibm where  ibm.gw_bank_code=GW_BANK_CODE and rownum=1) bank_name, (select  ibm.bank_name   from ibps_bank_map ibm where  ibm.gw_bank_code=GW_BANK_CODE and rownum=1) as BRAN_NAME,";

            strSql = strSql + "        td.CCYCD,   (select a.content    from Allcode a  where trim(a.cdname) = 'Connection'    and trim(a.cdval) = trim(td.connection)) as Connection, (select a.content ";
            strSql = strSql + "           from Allcode a  where trim(a.cdname) = 'FUNCTION' and a.gwtype = 'IBPS' and trim(a.cdval) = trim(td.Function)) as FUNCTION, td.DBLink, td.SBV_TADID from TAD td order by td.TAD_NAME ";
         



            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public int DeleteTAD(int iID)
        {
            string strSql = "Delete from TAD where TADID=:pTADID";
            OracleParameter[] oraParam = { new OracleParameter("pTADID", OracleType.Number, 5) };
            oraParam[0].Value = iID;
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
        }


        public DataSet GetTADBranch(string SIBSBankCode)
        {
            string strSql = "select distinct bra.sibs_bank_code,bra.bran_name from tad ta, branch bra where trim(bra.sibs_bank_code) ='" + SIBSBankCode ;
                   strSql= strSql + "' order by bra.bran_name";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        public int AddIBPSHVLV(TADInfo objTable)
        {
            string strSql = "Insert into TAD(TAD_NAME,TIME,AMOUNT) values(:pTAD_NAME,:pTIME_VALUE,:pAMOUNT_VALUE)";
            OracleParameter[] oraParam ={new OracleParameter("pTAD_NAME",OracleType.VarChar,20),
                                           new OracleParameter("pTIME_VALUE",OracleType.DateTime),
                                           new OracleParameter("pAMOUNT_VALUE",OracleType.Number,10)};
            oraParam[0].Value = objTable.TAD_NAME;
            oraParam[1].Value = objTable.TIME;
            oraParam[2].Value = objTable.AMOUNT;
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
        }

        public int UpdateIBPSHVLV(TADInfo objTable)
        {
            string strSql = "Update TAD set TAD_NAME=:pTAD_NAME,TIME_VALUE =:pTIME_VALUE,AMOUNT_VALUE =:pAMOUNT_VALUE where TAD_ID=:pTAD_ID";
            OracleParameter[] oraParam ={ new OracleParameter("pTAD_ID",OracleType.Number,5),
                                            new OracleParameter("pTAD_NAME",OracleType.VarChar,20),
                                            new OracleParameter("pTIME_VALUE",OracleType.DateTime),
                                            new OracleParameter("pAMOUNT_VALUE",OracleType.Number,10)};
            oraParam[0].Value = objTable.TADID;
            oraParam[1].Value = objTable.TAD_NAME;
            oraParam[2].Value = objTable.TIME;
            oraParam[3].Value = objTable.AMOUNT;

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
        }

        public int DeleteIBPSHVLV(int iID)
        {
            string strSql = "Delete from TAD where TAD_ID=:pTAD_ID";
            OracleParameter[] oraParam = { new OracleParameter("pTAD_ID", OracleType.Number, 5) };
            oraParam[0].Value = iID;
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
        }

        public DataSet GetIBPSHVLV()
        {
            string strSql = "Select tad.TADID, tad.TAD_NAME,tad.TIME,tad.AMOUNT from TAD tad";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable GetTADName()
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select imc.query_id,imc.tad,td.tad_name tadcanlay,imc.pre_tad,tapre.tad_name from ibps_msg_content imc, Tad td,";
                   strSQL = strSQL + " Tad tapre where Ltrim(imc.tad, '0') = Ltrim(td.sibs_bank_code, '0')";
                   strSQL = strSQL+ " And Ltrim(imc.pre_tad,'0')= Ltrim(tapre.sibs_bank_code,'0') order by td.tad_name";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable Get_Tad()
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select trim(T.TAD) as TAD,T.Time from TAD T Order by T.TAD ASC";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTadFW()
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select distinct(T.TAD) from TAD T where T.Status in(select a.cdval from Allcode a";
                       strSQL = strSQL + " where Trim(a.cdname)='CITADSTS' and Trim(a.content)='ACTIVE')";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTAD_ERROR(string strSTATUS)//QUYND
        {
            //
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select Ltrim(Trim(T.TAD),'TAD') as TAD from TAD T where Trim(T.STATUS) in(select A.CDVAL from ALLCODE A";
                       strSQL = strSQL +  "  where Trim(A.CDNAME)='CITADSTS' and Trim(A.CONTENT)='" + strSTATUS + "')  order by TAD ASC";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTAD_SIBS_BANK_CODE(string strTAD)//QUYND
        {
            //
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select T.SIBS_CODE as SIBS_BANK_CODE,T.GW_BANK_CODE from TAD T where trim(T.TAD)='" + strTAD + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        //Muc dich: Ham load du lieu khi chon cboTADView trong frmTADInfo
        //Nguoi tao: Nguyen Thi Thu Ha (HaNTT10@fpt.com.vn)
        //Ngay tao: 14.07.2008
        //BICHNN sua lai ngay 13/08/2008
        public DataSet GetTAD_View(string strArea)
        {


            string strSql = "Select td.TADID,td.TAD,td.TAD_NAME,td.GW_BANK_CODE,td.EXPORT_FOLDER,td.IMPORT_FOLDER,td.FTPPATH,td.FTPUSER,td.ftppass, ";
            strSql = strSql + "(select a.fullname from area a where trim(a.prov_code)= trim(td.area))as Area ,td.TIME,td.AMOUNT, ";
            strSql = strSql + "(select a.content from Allcode a where trim(a.cdname)='CITADSTS' and trim(a.cdval)= trim(td.STATUS)) as STATUS ,";
            strSql = strSql + " substr(td.sibs_bank_code,-3) as sibs_bank_code,td.SET_LOW_VALUE,bankname.bank_name,branchname.BRAN_NAME,td.CCYCD,td.connection,";
            strSql = strSql + "(select a.content from Allcode a where trim(a.cdname) = 'FUNCTION' and trim(a.gwtype)='IBPS' and trim(a.cdval) = trim(td.Function)) as FUNCTION,td.DBLink,SBV_TADID from TAD td, ";
            strSql = strSql + "(select ibm.sibs_bank_code,ibm.gw_bank_code,ibm.bank_name from ibps_bank_map ibm) bankname,  ";
            strSql = strSql + "(select br.SIBS_BANK_CODE,br.BRAN_NAME from branch br) branchname";
            strSql = strSql + " where trim(td.sibs_bank_code)  = bankname.sibs_bank_code(+) and Substr(trim(td.sibs_bank_code),3)=branchname.sibs_bank_code(+)";
            strSql = strSql + " and trim(td.area) = '" + strArea + "'" + " order by td.TAD_NAME";

            //string strSql = "Select td.TADID,td.TAD,td.TAD_NAME,td.GW_BANK_CODE,td.EXPORT_FOLDER,td.IMPORT_FOLDER,td.FTPPATH,td.FTPUSER,td.ftppass, ";
            //strSql = strSql + "(select a.fullname from area a where trim(a.prov_code)= trim(td.area))as Area ,td.TIME,td.AMOUNT, ";
            //strSql = strSql + "(select a.content from Allcode a where trim(a.cdname)='CITADSTS' and trim(a.cdval)= trim(td.STATUS)) as STATUS ,";
            //strSql = strSql + " ltrim(td.SIBS_BANK_CODE,'0') as SIBS_BANK_CODE,td.SET_LOW_VALUE,bankname.bank_name,branchname.BRAN_NAME,td.CCYCD,(select a.content from Allcode a where trim(a.cdname) = 'Connection' and trim(a.cdval) = trim(td.connection)) as Connection,";
            //strSql = strSql + "td.Function,td.DBLink from TAD td, ";
            //strSql = strSql + "(select ibm.sibs_bank_code,ibm.gw_bank_code,ibm.bank_name from ibps_bank_map ibm) bankname,  ";
            //strSql = strSql + "(select br.SIBS_BANK_CODE,br.BRAN_NAME from branch br) branchname";
            //strSql = strSql + " where td.sibs_bank_code  = bankname.sibs_bank_code(+) and Substr(td.sibs_bank_code,3)=branchname.sibs_bank_code(+)";
            //strSql = strSql + " and td.area = '" + strArea + "'" + " order by td.TAD_NAME";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //DungNT add, phuc vu phan Reconcile
        public DataSet GetTAD_DBLink_Name()
        {
            string strSql = "Select TAD,DBLINK, GW_BANK_CODE,SIBS_BANK_CODE from TAD where status in(select A.CDVAL from ALLCODE A";
                   strSql = strSql + " where Trim(A.CDNAME)='CITADSTS' and Trim(A.CONTENT)= 'ACTIVE') order by TAD_NAME";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //BICHNN add 10/08/2008
        public string GetROUTER_TAD()
        {
            string strSql = "select value from sysvar  where trim(VarName)='ROUTER_TAD'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return "" ;
                return Convert.ToString( DataAcess.ExecuteScalar(oraConn, CommandType.Text, strSql, null));
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable GetTAD_HOST(string strUserID)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                //string strSQL = "select t.value from sysvar t where trim(t.varname)='MAINBRANCODE'";
                string strSQL = "select u.branch from users u where u.userid='" + strUserID + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTAD_TAD(string pGW_BANK_CODE)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select *  from TAD T where Trim(T.GW_BANK_CODE)='" + pGW_BANK_CODE + "' ";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetTAD_CHECK(string pSIBS_BANK_CODE)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select *  from TAD T where Trim(T.SIBS_BANK_CODE)='" + pSIBS_BANK_CODE + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTAD_CheckHeadOffice(string pTAD)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select *  from TAD T where trim(t.function)='1' and trim(t.tad) <> '" + pTAD + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable GET_HO_TAD()
        {
            try
            {
                DataTable _dt = new DataTable();

                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select * from tad t where t.function in (1,3,2) and status=1";
                _dt= DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTAD_CheckMainBranch(string pGW_BANK_CODE)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = "select *  from TAD T where substr(trim(t.gw_bank_code),1,2)='" + pGW_BANK_CODE.Substring(0, 2) + "' and (trim(t.function)='2' or trim(t.function)='3') and trim(t.gw_bank_code) <> '" + pGW_BANK_CODE + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
    }


}
