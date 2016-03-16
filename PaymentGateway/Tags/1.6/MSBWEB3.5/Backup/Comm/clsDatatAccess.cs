using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.OracleClient;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using BIDVWEB.Comm.DA;


namespace BIDVWEB.Comm
{
    public class clsDatatAccess : OracleHelper         
    {
        //Khai bao bien dung cho lop DataAccess
        public string strError = "";        
        private System.Data.OracleClient.OracleConnection oraConn;
        private System.Data.OracleClient.OracleCommand goraCommand;        
        private clsConnection objConn = new clsConnection();

        //Dispose class
        public void Dispose()
        {
            goraCommand.Dispose();
            if (oraConn.State == ConnectionState.Open)
            {
                oraConn.Close();
                oraConn.Dispose();
            }            
        }

        //Ham khoi tao
        public clsDatatAccess()
        {            
            strError = "";
        }
        

        //Ham fill du lieu vao dropdownlist//////////////////////////
        // Muc dich:    Ham fill du lieu vao dropdownlist
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strSql: chuoi menh de where
        //              strTableName: Ten bang
        //              ddlName: ten dropdownlist can chen du lieu
        //              strDisplay: truong du lieu hien thi
        //              strValue: truong du lieu gan gia tri
        //              strOrder: truong order by
        // Dau ra:      Mot dropdownlist da fill du lieu        
        ///////////////////////////////////////////////////////////// 
        public DropDownList FillDataToDropDownList(string strTableName, string strWhere, 
            DropDownList ddlName, string strDisplay, string strValue,
            string strOrder, bool bAll)
        {            
            DataSet dsData = new DataSet();
            string strSql = "";
            
            try
            {
                dsData.DataSetName = "something";
                //Lay du lieu vao dataset
                strSql = "SELECT * FROM " + strTableName;
                if (!string.IsNullOrEmpty(strWhere))
                    strSql = strSql + " WHERE " + strWhere;
                if (!string.IsNullOrEmpty(strOrder))
                    strSql = strSql + " ORDER BY " + strOrder;                                    
                dsData = dsGetDataSourceByStr(strSql, strTableName);
                if (dsData == null)
                {
                    dsData.Dispose();
                    return null;                
                }
                ddlName.Items.Clear();
                //Neu co bAll = True, cho truong hop chon tat ca hien thi
                if (bAll == true)
                {
                    ListItem lst0 = new ListItem();
                    lst0.Text = "ALL";
                    lst0.Value = "ALL";
                    ddlName.Items.Add(lst0);
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr  = dsData.Tables[0].Rows[i];
                        ListItem lst = new ListItem();
                        lst.Text = dr[strDisplay].ToString();
                        lst.Value = dr[strValue].ToString();
                        ddlName.Items.Add(lst);
                    }
                }
                else
                {
                    ddlName.DataSource = dsData.Tables[0];
                    ddlName.DataTextField = strDisplay;
                    ddlName.DataValueField = strValue;
                    ddlName.DataBind();
                }
                dsData.Dispose();
                return ddlName;                
            }
            catch (Exception ex)
            {                
                strError = ex.Message;
                return null;                
            }                    
        }


        //Ham fill du lieu vao dropdownlist//////////////////////////
        // Muc dich:    Ham fill du lieu vao dropdownlist
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strSql: chuoi menh de where
        //              strTableName: Ten bang
        //              ddlName: ten dropdownlist can chen du lieu
        //              strDisplay: truong du lieu hien thi
        //              strValue: truong du lieu gan gia tri
        //              strOrder: truong order by
        // Dau ra:      Mot dropdownlist da fill du lieu        
        ///////////////////////////////////////////////////////////// 
        public DropDownList FillDataToDropDownList1(string strTableName, string strWhere,
            DropDownList ddlName, string strDisplay, string strValue,
            string strOrder, int iType)
        {
            DataSet dsData = new DataSet();
            string strSql = "";

            try
            {
                dsData.DataSetName = "something";
                //Lay du lieu vao dataset
                strSql = "SELECT * FROM " + strTableName;
                if (!string.IsNullOrEmpty(strWhere))
                    strSql = strSql + " WHERE " + strWhere;
                if (!string.IsNullOrEmpty(strOrder))
                    strSql = strSql + " ORDER BY " + strOrder;
                dsData = dsGetDataSourceByStr(strSql, strTableName);
                if (dsData == null)
                {
                    dsData.Dispose();
                    return null;
                }
                ddlName.Items.Clear();
                //Neu co bAll = True, cho truong hop chon tat ca hien thi
                if (iType == 1)
                {
                    ddlName.DataSource = dsData.Tables[0];
                    ddlName.DataTextField = strDisplay;
                    ddlName.DataValueField = strValue;
                    ddlName.DataBind();                    
                }
                else if (iType == 2)
                {
                    ListItem lst0 = new ListItem();
                    lst0.Text = "ALL";
                    lst0.Value = "ALL";
                    ddlName.Items.Add(lst0);
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dsData.Tables[0].Rows[i];
                        ListItem lst = new ListItem();
                        lst.Text = dr[strDisplay].ToString();
                        lst.Value = dr[strValue].ToString();
                        ddlName.Items.Add(lst);
                    }
                }
                else if (iType == 3)
                {
                    ListItem lst0 = new ListItem();
                    lst0.Text = "ALL";
                    lst0.Value = "ALL";
                    ddlName.Items.Add(lst0);
                    ListItem lst1 = new ListItem();
                    lst1.Text = "";
                    lst1.Value = "NULL";
                    ddlName.Items.Add(lst1);
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dsData.Tables[0].Rows[i];
                        ListItem lst = new ListItem();
                        lst.Text = dr[strDisplay].ToString();
                        lst.Value = dr[strValue].ToString();
                        ddlName.Items.Add(lst);
                    }

                }
                dsData.Dispose();
                return ddlName;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }        


        //Khoi tao Transaction///////////////////////////////////
        //Muc dich:         Ham BeginTransaction
        //Ngay tao:         05/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          /
        //Dau ra:           /Khoi tao Transaction
        /////////////////////////////////////////////////////////
        public bool bBeginTrans()
        {            
            try
            {
                goraCommand.Transaction = oraConn.BeginTransaction();                
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Ham thuc hien commit Transaction///////////////////////
        //Muc dich:         Ham CommitTransaction
        //Ngay tao:         05/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          /
        //Dau ra:           /Commit Transaction thanh cong
        /////////////////////////////////////////////////////////
        public bool bCommitTrans()
        {
            try
            {
                goraCommand.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Ham RollbackTransaction////////////////////////////////
        //Muc dich:         Ham RollbackTransaction
        //Ngay tao:         05/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          /
        //Dau ra:           /RollBack thanh cong
        /////////////////////////////////////////////////////////
        public bool bRollbackTrans()
        {
            try
            {
                goraCommand.Transaction.Rollback();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Lay du lieu theo strSQL////////////////////////////////
        //Muc dich:         Lay du lieu theo strSQL
        //Ngay tao:         05/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          strSQL: Cau lenh SQL
        //                  strTableName: Ten bang
        //Dau ra:           Dataset
        /////////////////////////////////////////////////////////        
        public DataSet dsGetDataSourceByStr(string strSQL , string strTableName)
        {
            DataSet dsData = new DataSet();            
            strError = "";
            try
            {
                //Tao connection
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return null;
                dsData = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                //Huy connection
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                if (dsData != null)                
                    return dsData;                
                else                
                    return null;                
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    dsData.Dispose();
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return null;
            }
        }

                
        //Cho phep thuc hien cau lenh SQL////////////////////////
        //Muc dich:         Cho phep thuc hien cau lenh SQL
        //Ngay tao:         05/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          strSQL: Cau lenh SQL
        //Dau ra:           True: Thanh cong, false: khong thanh cong
        /////////////////////////////////////////////////////////         
        public bool ExecuteSQL(String strSql)
        {
            int i;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return false;

                i = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                if (i <= 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return false;
            }                       
        }


        /////////////////////////////////////////////////////////
        //Muc dich:         Ham thuc hien cau lenh sql
        //Ngay tao:         05/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          strSQL: phu thuoc iTypeStore
        //          neu iTypeStore = True:  Ten store procedure
        //          neu iTypeStore = False: Chuoi cau lenh sql
        //                  paras: Mang cac parameter
        //Dau ra:
        /////////////////////////////////////////////////////////
        public bool ExecuteSQL(string strSQL, bool iTypeStore, params OracleParameter[] paras)
        {
            int iBool;

            try
            {                
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return false;
                //goraCommand = new OracleCommand(strSQL, oraConn);
                //goraCommand.CommandType = CommandType.StoredProcedure;
                //foreach (OracleParameter para in paras)
                //{
                //    goraCommand.Parameters.Add(para);
                //}
                //goraCommand.ExecuteNonQuery();
                //goraCommand.Dispose();
                if (iTypeStore==true)
                    iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, 
                        CommandType.StoredProcedure, strSQL, paras);
                else
                    iBool=clsDataAcessComm.ExecuteNonQuery(oraConn, 
                        CommandType.Text, strSQL, paras);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                if (iBool <= 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                //goraCommand.Dispose();
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return false;
            }
        }

        /////////////////////////////////////////////////////////
        //Muc dich:         Thuc hien cap nhat mot truong cho bang
        //Ngay tao:         06/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          pv_sTableName: Ten bang
        //                  pv_sColName: Ten cot
        //                  pv_sValue: Gia tri
        //                  pv_bIsNum: Kieu so
        //                  pv_sCond: Dieu kien cap nhat du lieu
        //Dau ra:           True: thanh cong, false: khong thanh cong
        /////////////////////////////////////////////////////////
        public bool gs_UpdateBatch(string pv_sTableName, string pv_sColName, string pv_sValue, bool pv_bIsNum, string pv_sCond)
        {
            string sv_sSQL;
            goraCommand = new OracleCommand();
            try
            {
                if (pv_bIsNum == false)
                {
                    sv_sSQL = "Update " + pv_sTableName + " Set " + pv_sColName + " = " + pv_sValue;
                }
                else
                {
                    sv_sSQL = "Update " + pv_sTableName + " Set " + pv_sColName + " = '" + pv_sValue + "'";
                }                

                if (pv_sCond != "")
                {
                    sv_sSQL = sv_sSQL + " where " + pv_sCond;
                }
                goraCommand.CommandText = sv_sSQL;
                goraCommand.ExecuteNonQuery();
                goraCommand.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                goraCommand.Dispose();
                return false;
            }
        }



        //Lay du lieu theo SP tra ra qua cursor//////////////////
        //Muc dich:         Lay du lieu theo SP tra ra qua cursor
        //Ngay tao:         07/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          strSql: Ten package.ten thu tuc
        //                  sArrName: Danh sach cac tham so
        //                  sArrValue: Danh sach gia tri
        //                  sArrDataType: Danh sach kieu
        //                  dsview: Dataset tra ve
        //Dau ra:           Dataset
        /////////////////////////////////////////////////////////  
        public void RunStorePro(string strSql, string sArrName, string sArrValue, 
            string sArrDataType, out DataSet dsview)
        {
            int iSum;
            DataSet ds = new DataSet();
            OracleCommand OraCmd = new OracleCommand();
            OracleDataAdapter oraDataAdapter;
            dsview = null;
            try
            {
                strError = "";
                oraConn = objConn.Connect();                                
                //Mang luu cac tham so dau vao cho SP
                string[] arrayName;
                //Mang luu cac tham tri can truyen vao SP
                string[] arrayValue;
                //Mang luu cac kieu du lieu cua tham so
                string[] arrayDataType;
                //Ky tu ket noi cac phan tu o 2 mang tren
                char[] splitter = { '|' };

                //Tao cac phan tu mang
                arrayName = sArrName.Split(splitter);
                arrayValue = sArrValue.Split(splitter);
                arrayDataType = sArrDataType.Split(splitter);
                iSum = arrayName.Length;
                OracleParameter[] oraParas = new OracleParameter[iSum];
                                
                //Gan cac tham so cho SP
                for (int i = 0; i <= arrayName.Length - 1; i++)
                {
                    //Goi OracleDbType
                    OracleType oraDbtype = new OracleType();
                    oraDbtype = GetOraDbType(arrayDataType[i]);
                    if (i < arrayName.Length - 1)
                    {
                        oraParas[i] = new OracleParameter(arrayName[i], oraDbtype);
                        oraParas[i].Direction = ParameterDirection.Input;
                        if (oraDbtype == OracleType.DateTime)
                        {
                            oraParas[i].Value = Convert.ToDateTime(arrayValue[i]);
                        }
                        else
                        {
                            oraParas[i].Value = arrayValue[i];
                        }                        
                    }
                    else
                    {
                        oraParas[i] = new OracleParameter(arrayName[i], oraDbtype);
                        oraParas[i].Direction = ParameterDirection.Output;
                        oraParas[i].Value = arrayValue[i];
                    }
                }
                // create and setup command to call stored procedure                 
                OraCmd.CommandText = strSql;
                OraCmd.CommandType = CommandType.StoredProcedure;
                OraCmd.Connection = oraConn;
                OraCmd.ExecuteNonQuery();                
                //Gan du lieu cho dataset
                oraDataAdapter = new OracleDataAdapter(OraCmd);
                oraDataAdapter.Fill(ds);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dsview = ds;
                }
                if (ds == null) { strError = "Không có dữ liệu"; }
                if (ds.Tables[0].Rows.Count == 0) { strError = "Không có dữ liệu!"; }
                //Huy cac doi tuong conn, cmd, DataAdapter                
                oraDataAdapter.Dispose();
                OraCmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();

            }
            catch (Exception ex)
            {
                //Huy cac doi tuong conn, cmd, DataAdapter                
                OraCmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
                strError = ex.Message;
            }
        }
        

        //Ham lay kieu cua OracleDbType//////////////////////////
        //Muc dich:         Ham lay kieu cua OracleDbType
        //Ngay tao:         06/2008
        //Nguoi tao:        Huypq7
        //Dau vao:          strDataType: Kieu truyen chp tham so
        //Dau ra:           Kieu OracleDbType
        ///////////////////////////////////////////////////////// 
        private OracleType GetOraDbType(string strDataType)
        {
            OracleType oraDbType = new OracleType();
            switch (strDataType.ToUpper())
            {
                case "INT16":
                    oraDbType = OracleType.Int16;
                    break;
                case "DATE":
                    oraDbType = OracleType.DateTime;
                    break;
                case "CURSOR":
                    oraDbType = OracleType.Cursor;
                    break;
                case "VARCHAR2":
                    oraDbType = OracleType.VarChar;
                    break;
                case "CHAR":
                    oraDbType = OracleType.Char;
                    break;
                case "DECIMAL":
                    oraDbType = OracleType.Double;
                    break;
                case "LONG":
                    oraDbType = OracleType.Int32;
                    break;
                case "NVARCHAR2":
                    oraDbType = OracleType.NVarChar;
                    break;
                case "DOUBLE":
                    oraDbType = OracleType.Double;
                    break;
                case "NUMBER":
                    oraDbType = OracleType.Number;
                    break;                
            }
            return oraDbType;
        }
    }
}
