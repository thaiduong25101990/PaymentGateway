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
namespace  BR.BRBusinessObject
{
	public class MENUDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();	
		public MENUDP()
		{
		}
		public static MENUDP Instance()
		{
			return new MENUDP();
		}
		
		public int AddMENU(MENUInfo objTable)
		{
            string strSql = "Insert into MENU (MENUID,PARENTID,CAPTION,ASSEMBLY,ASSEMBLYFILE,METHOD,ENABLE,CTRL,ALT,KEY,GWTYPE,OPTIONDATA,TOOLTIPTEXT,ORDERMENU) values (:pMENUID,:pPARENTID,:pCAPTION,:pASSEMBLY,:pASSEMBLYFILE,:pMETHOD,:pENABLE,:pCTRL,:pALT,:pKEY,:pGWTYPE,:pOPTIONDATA,:pTOOLTIPTEXT,:pORDERMENU)";
            OracleParameter[] oraParam = {new OracleParameter("pMENUID", OracleType.NVarChar, 4),
                                         new OracleParameter("pPARENTID",OracleType.NVarChar,4),
                                         new OracleParameter("pCAPTION",OracleType.NVarChar,30),
                                         new OracleParameter("pASSEMBLY",OracleType.NVarChar,100),
                                         new OracleParameter("pASSEMBLYFILE",OracleType.NVarChar,100),
                                         new OracleParameter("pMETHOD",OracleType.NVarChar,30),
                                         new OracleParameter("pENABLE",OracleType.Number,1),
                                         new OracleParameter("pCTRL",OracleType.Number,1),
                                         new OracleParameter("pALT",OracleType.Number,1),
                                         new OracleParameter("pKEY",OracleType.NVarChar,5),
                                         new OracleParameter("pGWTYPE",OracleType.NVarChar,10),
                                         new OracleParameter("pOPTIONDATA",OracleType.NVarChar,10),
                                         new OracleParameter("pTOOLTIPTEXT",OracleType.NVarChar,100),
                                         new OracleParameter("pORDERMENU", OracleType.Number,3)};

            oraParam[0].Value = objTable.MENUID;
            oraParam[1].Value = objTable.PARENTID;
            oraParam[2].Value = objTable.CAPTION;
            oraParam[3].Value = objTable.Assembly;
            oraParam[4].Value = objTable.ASSEMBLYFILE;
            oraParam[5].Value = objTable.METHOD;
            oraParam[6].Value = objTable.ENABLE;
            oraParam[7].Value = objTable.CTRL;
            oraParam[8].Value = objTable.ALT;
            oraParam[9].Value = objTable.KEY;
            oraParam[10].Value = objTable.GWTYPE;
            oraParam[11].Value = objTable.OPTIONDATA;
            oraParam[12].Value = objTable.TOOLTIPTEXT;
            oraParam[13].Value = objTable.ORDERMENU;

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
		
		public int UpdateMENU(MENUInfo objTable)
		{
            string strSql = "Update MENU  set MENUID=:pMENUID,PARENTID=:pPARENTID,";
            strSql = strSql + " CAPTION=:pCAPTION,ASSEMBLY=:pASSEMBLY,";
            strSql = strSql + " ASSEMBLYFILE=:pASSEMBLYFILE,METHOD=:pMETHOD,";
            strSql = strSql + " ENABLE=:pENABLE,CTRL=:pCTRL,";
            strSql = strSql + " ALT=:pALT,KEY=:pKEY,";
            strSql = strSql + " GWTYPE=:pGWTYPE,OPTIONDATA=:pOPTIONDATA,";
            strSql = strSql + " TOOLTIPTEXT=:pTOOLTIPTEXT,ORDERMENU=:pORDERMENU";
            strSql = strSql + " where MENUID=:pMENUID";

            OracleParameter[] oraParam = {new OracleParameter("pMENUID", OracleType.NVarChar, 4),
                                         new OracleParameter("pPARENTID",OracleType.NVarChar,4),
                                         new OracleParameter("pCAPTION",OracleType.NVarChar,30),
                                         new OracleParameter("pASSEMBLY",OracleType.NVarChar,100),
                                         new OracleParameter("pASSEMBLYFILE",OracleType.NVarChar,100),
                                         new OracleParameter("pMETHOD",OracleType.NVarChar,30),
                                         new OracleParameter("pENABLE",OracleType.Number,1),
                                         new OracleParameter("pCTRL",OracleType.Number,1),
                                         new OracleParameter("pALT",OracleType.Number,1),
                                         new OracleParameter("pKEY",OracleType.NVarChar,5),
                                         new OracleParameter("pGWTYPE",OracleType.NVarChar,10),
                                         new OracleParameter("pOPTIONDATA",OracleType.NVarChar,10),
                                         new OracleParameter("pTOOLTIPTEXT",OracleType.NVarChar,100),
                                         new OracleParameter("pORDERMENU", OracleType.Number,3)};
            oraParam[0].Value = objTable.MENUID;
            oraParam[1].Value = objTable.PARENTID;
            oraParam[2].Value = objTable.CAPTION;
            oraParam[3].Value = objTable.Assembly;
            oraParam[4].Value = objTable.ASSEMBLYFILE;
            oraParam[5].Value = objTable.METHOD;
            oraParam[6].Value = objTable.ENABLE;
            oraParam[7].Value = objTable.CTRL;
            oraParam[8].Value = objTable.ALT;
            oraParam[9].Value = objTable.KEY;
            oraParam[10].Value = objTable.GWTYPE;
            oraParam[11].Value = objTable.OPTIONDATA;
            oraParam[12].Value = objTable.TOOLTIPTEXT;
            oraParam[13].Value = objTable.ORDERMENU;
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

        public int DeleteMENU(string  iID)
        {
            string strSql = "Delete from MENU where MENUID=:pMENUID";
            OracleParameter[] oraParam = { new OracleParameter("pMENUID", OracleType.Int32, 5) };
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

        public DataSet GetMENU(string strUserID,string strGwtype)
        {
            DataSet datDs = new DataSet();
          
            string strSql = "select mnu.menuid, mnu.parentid, mnu.caption, mnu.assembly, mnu.assemblyfile, mnu.method, ";
            strSql = strSql + "mnu.enable, mnu.ctrl, mnu.alt, mnu.key, mnu.gwtype, mnu.optiondata, mnu.tooltiptext, mnu.ordermenu from Menu mnu ";
            strSql = strSql + " Where mnu.enable = 1 AND (MNU.GWTYPE = :pGWTYPE or MNU.GWTYPE='GWSYSTEM' ) And (";
            strSql = strSql + "MNU.MENUID in (Select MENUID  from Group_Menu GM Where GM.GROUPID in ";
            strSql = strSql + " (select GU.GROUPID from user_groups GU where GU.USERID = :pUserID))  or  Trim(MNU.MENUID) = '1000' or Trim(MNU.MENUID) = '6000' or ";
            strSql = strSql + "  Trim(MNU.MENUID) = '1001' or Trim(MNU.MENUID) = '5000' or  Trim(MNU.MENUID) = '5001' or Trim(MNU.MENUID) = '1003') ";
            strSql = strSql + "  and (Trim(MNU.MENUID) not in ('7901', '7902', '7903'))";
            DataTable datTblMenu = new DataTable("Menu");
            OracleParameter[] oraParam = { new OracleParameter("pUserID", OracleType.VarChar, 20),
                                           new OracleParameter("pGWTYPE", OracleType.VarChar , 10)};
            oraParam[0].Value = strUserID;
            oraParam[1].Value = strGwtype;            
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;                
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, oraParam);   
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetMENU1(string strUserID)//QuyND update 29/07/2008//MNU.GWTYPE = :pGWTYPE or AND (MNU.GWTYPE='GWSYSTEM' or  MNU.GWTYPE='GWTYPE')
        {
            DataSet datDs = new DataSet();         
            string strSql = "select mnu.menuid, mnu.parentid, mnu.caption, mnu.assembly, mnu.assemblyfile, mnu.method, ";
            strSql = strSql + "mnu.enable, mnu.ctrl, mnu.alt, mnu.key, mnu.gwtype, mnu.optiondata, mnu.tooltiptext, mnu.ordermenu from Menu mnu ";
            strSql = strSql + " Where mnu.enable = 1  And (";
            strSql = strSql + "MNU.MENUID in (Select MENUID  from Group_Menu GM Where GM.GROUPID in ";
            strSql = strSql + " (select GU.GROUPID from user_groups GU where GU.USERID = :pUserID)) or Trim(MNU.MENUID) = '1000' or Trim(MNU.MENUID) = '6000' or Trim(MNU.MENUID) = '1001' or Trim(MNU.MENUID) = '5000' or";
            strSql = strSql + "  Trim(MNU.MENUID) = '5001' or Trim(MNU.MENUID) = '1003') and (Trim(MNU.MENUID) not in ('7901','7902','7903'))";


            DataTable datTblMenu = new DataTable("Menu");
            OracleParameter[] oraParam = { new OracleParameter("pUserID", OracleType.VarChar, 20)                                          
                                         };
            oraParam[0].Value = strUserID;
           
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetMENU1()
        {
            DataSet datDs = new DataSet();
            string strSql = "select mnu.MENUID,mnu.PARENTID,mnu.CAPTION,mnu.ASSEMBLY,mnu.ASSEMBLYFILE,mnu.METHOD,mnu.ENABLE,mnu.CTRL,mnu.ALT,mnu.KEY,mnu.GWTYPE,mnu.OPTIONDATA,mnu.TOOLTIPTEXT,mnu.ORDERMENU   from MENU mnu";
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
        public DataSet GetMenuid( string pMenuid)
        {
            DataSet datDs = new DataSet();
            string strSql = "select GWTYPE from Menu m where Trim(m.menuid)='" + pMenuid.Trim() + "'";
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
        public DataSet GetMenuid_Gwtype(string pCaption)
        {
            DataSet datDs = new DataSet();
            string strSql = "select distinct(m.menuid) from Menu m where Trim(m.caption)='" + pCaption.Trim() + "'";
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
        public DataSet GetMenuid_Gwtype_TYPE(string pCaption,string strGWTYPE)
        {
            DataSet datDs = new DataSet();
            string strSql = "select distinct(m.menuid) from Menu m where Trim(m.caption)='" + pCaption.Trim() + "' and (trim(m.gwtype)='" + strGWTYPE + "' or Trim(m.gwtype)='GWSYSTEM' )";
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
        public DataSet GetMenuid_Gwtype_TYPE1(string pCaption, string strGWTYPE,string strMenuID)
        {
            DataSet datDs = new DataSet();
            string strSql = "select distinct(m.menuid) from Menu m where Trim(m.caption)='" + pCaption.Trim() + "' and (trim(m.gwtype)='" + strGWTYPE + "' or Trim(m.gwtype)='GWSYSTEM' ) and trim(m.menuid)='" + strMenuID + "'";
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
        public DataSet GetMenu_MenuID(string pMENUID)
        {
            DataSet datDs = new DataSet();
            string strSql = "select M.Assembly from Menu M where M.Menuid='" + pMENUID + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                datDs = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                oraConn.Close();
                oraConn.Dispose();
                return datDs;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable Menu_get(string pParentid)
        {
            DataTable _dt = new DataTable();
            string strSql = "select ID,menuid,parentid,caption,ASSEMBLY  from menu where Upper(trim(parentid)) ='" + pParentid + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable select_menuid()
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                DataTable _dt = new DataTable();
                string strSql = "select menuid from  menu order by menuid desc";
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int Check_delete(string pMenuid)
        {
            try
            {
                int iResult = 0;
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                DataTable _dt = new DataTable();
                string strSql = "select PARENTID,CAPTION,ASSEMBLY,ASSEMBLYFILE,METHOD,ENABLE,CTRL,ALT,KEY,GWTYPE,OPTIONDATA,TOOLTIPTEXT,ORDERMENU from  menu where trim(PARENTID)= '" + pMenuid + "'";
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                if (_dt.Rows.Count == 0) { return iResult = -1; } //parent id khong dung
                _dt.Clear();
                return iResult;
            }
            catch (Exception ex)
            {
                throw (ex);                
            }
        }

        public int Check_input(string pParentid)
        {
            try
            {
                int iResult = 0;
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                DataTable _dt = new DataTable();
                string strSql = "select PARENTID,CAPTION,ASSEMBLY,ASSEMBLYFILE,METHOD,ENABLE,CTRL,ALT,KEY,GWTYPE,OPTIONDATA,TOOLTIPTEXT,ORDERMENU from  menu where trim(MENUID)= '" + pParentid + "'";
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                if (_dt.Rows.Count == 0) { return iResult = -1; } //parent id khong dung
                _dt.Clear();
                return iResult;
            }
            catch (Exception ex)
            {
                throw (ex);               
            }
        }

        public int Check_inputs(string pMenuid)
        {
            try
            {
                int iResult = 0;
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                DataTable _dt = new DataTable();
                string strSql = "select PARENTID,CAPTION,ASSEMBLY,ASSEMBLYFILE,METHOD,ENABLE,CTRL,ALT,KEY,GWTYPE,OPTIONDATA,TOOLTIPTEXT,ORDERMENU from  menu where trim(MENUID)= '" + pMenuid + "'";
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                if (_dt.Rows.Count != 0) { return iResult = -1; }
                _dt.Clear();
                return iResult;
            }
            catch (Exception ex)
            {
                throw (ex);               
            }
        }
        public int Check_inputst(string pAssembly)
        {
            try
            {
                int iResult = 0;
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                DataTable _dt = new DataTable();
                string strSql = "select PARENTID,CAPTION,ASSEMBLY,ASSEMBLYFILE,METHOD,ENABLE,CTRL,ALT,KEY,GWTYPE,OPTIONDATA,TOOLTIPTEXT,ORDERMENU from  menu where trim(MENUID)= '" + pAssembly + "'";
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                if (_dt.Rows.Count != 0) { return iResult = -1; }
                _dt.Clear();
                return iResult;
            }
            catch (Exception ex)
            {
                throw (ex);               
            }
        }

        public DataTable Menu_data(string pMenuid)
        {
            DataTable _dt = new DataTable();
            string strSql = "select ID,PARENTID,CAPTION,ASSEMBLY,ASSEMBLYFILE,METHOD,ENABLE,CTRL,ALT,KEY,GWTYPE,OPTIONDATA,TOOLTIPTEXT,ORDERMENU from  menu where trim(MENUID)= '" + pMenuid + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
	}
	
}
