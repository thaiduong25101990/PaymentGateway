using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using BR.BRLib;
using BR.DataAccess;
using Microsoft.VisualBasic;

namespace BR.BRBusinessObject
{
    public class GetData
    {                       

        ///////////////////////////////////////////////////////////-/
        //Muc dich: Ham kiem tra Keyword co trong Message?
        //          Kiem tra xau Message co dung chuan SQL        
        //Ngay tao: 01/08/2008
        //Tham so:  strKeyword: xau Keyword
        //          strMessage: xau Messafe
        //Dau ra:   True: thanh cong, false: ko thanh cong
        /////////////////////////////////////////////////////////////
        public bool CheckKeyword(string strKeyword, string strMessage,
            out string strError, out int iCheck)
        {
            string strSql = "";
            string strSelect = "";
            string strDrop = "";
            DataSet ds = new DataSet();
            char[] splitter = { ';' };
            string[] arrKeyword;
            int iID = 0;

            strError = "";
            iCheck = 0;
            try
            {
                //Tao cac phan tu mang
                strKeyword = strKeyword.ToUpper();
                arrKeyword = strKeyword.Split(splitter);
                strMessage = strMessage.ToUpper();

                for (int i = 0; i <= arrKeyword.Length - 1; i++)
                {
                    if (strMessage.IndexOf(arrKeyword[i].ToString().Trim()) < 0)
                    {
                        strError = "Keyword not in Message";
                        return false;
                    }
                }
                //Tao bang temp
                strSql = strSql + " CREATE GLOBAL TEMPORARY TABLE SWIFT_BRANCH_TEMP (";
                for (int i = 0; i <= arrKeyword.Length - 1; i++)
                {
                    if (i < arrKeyword.Length - 1)
                    {
                        strSql = strSql + arrKeyword[i].ToString().Trim() + " VARCHAR2(200),";
                    }
                    else
                    {
                        strSql = strSql + arrKeyword[i].ToString().Trim() + " VARCHAR2(200)";
                    }
                }
                strSql = strSql + ") ON COMMIT PRESERVE ROWS ";
                //Select bang temp voi menh de where = criteria message           
                if (strMessage.Trim().Substring(0, 6).ToUpper() == "SELECT")
                {
                    strSelect = strMessage;
                }
                else
                {
                    strSelect = " SELECT * FROM SWIFT_BRANCH_TEMP WHERE " + strMessage;
                }
                strDrop = " DROP TABLE SWIFT_BRANCH_TEMP ";
                //iID = RunSPCheckMessage(strSql, strSelect, strDrop, out bCheck);
                iID = RunSPCheckMessage(strSql, strSelect, strDrop, out iCheck);
                if (iID < 0)
                {
                    strError = "Criteria message is invalid";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////-/
        //Muc dich: Ham run SP check xau sql        
        //Ngay tao: 01/08/2008
        //Tham so:  strCreate: xau tao bang temp
        //          strSelect: xau select
        //          strDrop: xau drop bang temp
        //          iCheck: gia tri tra ra
        //Dau ra:   True: thanh cong, false: ko thanh cong
        /////////////////////////////////////////////////////////////
        public int RunSPCheckMessage(string strCreate, string strSelect,
            string strDrop, out int iCheck)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            int iResult = 0;
            iCheck = 0;

            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pCreateTable",OracleType.VarChar,500),
                                                new OracleParameter("pSelectTable",OracleType.VarChar,500) ,
                                                new OracleParameter("pDropTable",OracleType.VarChar,500)                                            
                                                ,new  OracleParameter("pOut",OracleType.Int16,2)
                                            };
                oraParas[0].Value = strCreate;
                oraParas[1].Value = strSelect;
                oraParas[2].Value = strDrop;
                oraParas[3].Value = 0;
                oraParas[3].Direction = ParameterDirection.Output;
                iCheck = 0;

                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    iCheck = 0;
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_CHECK_PARA.CHECK_SWIFT_PARA", oraParas);
                    iCheck = Convert.ToInt16(oraParas[3].Value);
                }
                oraConn.Close();
                oraConn.Dispose();
                return iResult;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                oraConn.Close();
                oraConn.Dispose();
                return -1;
            }
        }


        public bool FillDataComboBox_Branch(ComboBox cboCom)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            try
            {
                DataSet dsData = new DataSet();
                string sSQL;
                sSQL = "SELECT '' as Bankcode FROM DUAL UNION ALL SELECT B.SIBS_BANK_CODE as Bankcode FROM BRANCH B UNION ALL SELECT 'TC'  as Bankcode FROM DUAL UNION ALL SELECT 'LT' as Bankcode   FROM DUAL UNION ALL SELECT 'Other'  as Bankcode   FROM DUAL UNION ALL SELECT 'TMPRBR'  as Bankcode   FROM DUAL UNION ALL SELECT 'RECEIVER_BRAN'  as Bankcode FROM DUAL ";
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to DataBase", 1, MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    dsData = DataAcess.ExecuteDataset(oraConn, CommandType.Text, sSQL, null);
                    if (dsData.Tables[0].Rows.Count == 0)
                    {
                        Common.ShowError("Table Data is empty", 1, MessageBoxButtons.OK);
                        return false;
                    }
                    cboCom.DataSource = dsData.Tables[0];
                    cboCom.ValueMember = "Bankcode";
                    cboCom.DisplayMember = "Bankcode";

                   
                }
                oraConn.Close();
                oraConn.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
       


        /////////////////////////////////////////////////////////////-/
        // Muc dich:    Ham nay nhu ham getDataCombo1 tren
        //              Chi cho them truong order by
        // Tham so:     cboCom: Ten combobox
        //              sColDisplay: Cot du lieu hien thi
        //              sColValue: Cot du lieu lay gia tri cua combobox
        //              sTable: Ten bang
        //              sWhere: Menh de where
        //              sColOrder: Cot order
        //              bASC:   True: Order theo ASC
        //                      False: Order theo DESC
        //              bALL:   True: Them chon ALL cho combobox
        //                      False: combobox ko chon ALL
        //              sDisplayALL: Khi combobox co chon ALL, nhap gia tri
        //                           hien thi cho lua chon ALL    
        // Tra ra:      True: successfull
        //              False: not successfull
        // Nguoi tao:   Huypq7
        // Ngay tao:    02/08/2008
        ///////////////////////////////////////////////////////////////
        public bool FillDataComboBox(ComboBox cboCom, string sColDisplay,
            string sColValue, string sTable, string sWhere, string sColOrder,
            bool bASC, bool bALL, string sDisplayALL)
        {

            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            try
            {
                DataSet dsData = new DataSet();
                string sSQL;

                if (cboCom == null)
                {
                    Common.ShowError("Combobox is null or empty", 1, MessageBoxButtons.OK);
                    return false;
                }
                if (sColDisplay == null || sColDisplay == "")
                {
                    Common.ShowError("Column display is null or empty", 1, MessageBoxButtons.OK);
                    return false;
                }
                if (sColValue == null || sColValue == "")
                {
                    Common.ShowError("Column value is null or empty", 1, MessageBoxButtons.OK);
                    return false;
                }
                if (sTable == null || sTable == "")
                {
                    Common.ShowError("TableName is null or empty", 1, MessageBoxButtons.OK);
                    return false;
                }
                //
                if (bALL == true)
                {
                    if (sDisplayALL == null)
                    {
                        Common.ShowError("Column display 'ALL' is null or empty", 1, MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(sColOrder))
                    if (sColDisplay.Trim() == sColValue.Trim())
                        sSQL = "SELECT DISTINCT " + sColDisplay + "," + sColValue +
                            " as " + sColValue + "0" +
                            "," + sColOrder + " as " + sColOrder + "1 FROM " + sTable;
                    else
                        sSQL = "SELECT DISTINCT " + sColDisplay + "," + sColValue +
                            "," + sColOrder + " as " + sColOrder + "1 FROM " + sTable;
                else
                    if (sColDisplay.Trim() == sColValue.Trim())
                        sSQL = "SELECT DISTINCT " + sColDisplay + "," + sColValue +
                            " as " + sColValue + "0" + " FROM " + sTable;
                    else
                        sSQL = "SELECT DISTINCT " + sColDisplay + "," + sColValue +
                            " FROM " + sTable;
                // sWhere is not null or empty
                if (!string.IsNullOrEmpty(sWhere))
                    sSQL = sSQL + " WHERE " + sWhere;
                ////Them chon all
                if (bALL == true)
                {
                    if (!string.IsNullOrEmpty(sColOrder))
                        if (sColDisplay.Trim() == sColValue.Trim())
                            sSQL = " SELECT '" + sDisplayALL + "' as " + sColDisplay +
                            ",'ALL' as " + sColValue + "0" +
                            ",'-1' as " + sColOrder +
                            "1 FROM DUAL UNION ALL " + sSQL;
                        else
                            sSQL = " SELECT '" + sDisplayALL + "' as " + sColDisplay +
                            ",'ALL' as " + sColValue + ",'-1' as " + sColOrder +
                            "1 FROM DUAL UNION ALL " + sSQL;
                    else
                        if (sColDisplay.Trim() == sColValue.Trim())
                            sSQL = " SELECT '" + sDisplayALL + "' as " + sColDisplay +
                            ",'ALL' as " + sColValue + "0" +
                            " FROM DUAL UNION ALL " + sSQL;
                        else
                            sSQL = " SELECT '" + sDisplayALL + "' as " + sColDisplay +
                            ",'ALL' as " + sColValue + " FROM DUAL UNION ALL " + sSQL;

                }

                // sColOrder is not null or empty
                if (!string.IsNullOrEmpty(sColOrder))
                {
                    //Order by ASC                    
                    if (bASC == true)
                        sSQL = sSQL + " ORDER BY " + sColOrder + "1 ASC";
                    //Order by DESC
                    else
                        sSQL = sSQL + " ORDER BY " + sColOrder + "1 DESC";
                }

                //Connec DB
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to DataBase", 1, MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    dsData = DataAcess.ExecuteDataset(oraConn, CommandType.Text, sSQL, null);
                    if (dsData.Tables[0].Rows.Count == 0)
                    {
                        Common.ShowError("Table Data is empty", 1, MessageBoxButtons.OK);
                        return false;
                    }
                    cboCom.DataSource = dsData.Tables[0];
                    if (sColDisplay.Trim() == sColValue.Trim())
                        cboCom.ValueMember = sColValue + "0";
                    else
                        cboCom.ValueMember = sColValue;
                    cboCom.DisplayMember = sColDisplay;

                    if (bALL == true)
                        cboCom.SelectedValue = "ALL";
                }
                oraConn.Close();
                oraConn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                oraConn.Close();
                oraConn.Dispose();
                return false;
            }
        }

        public static bool CheckExistBranch(string strBranchID)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            DataSet ds = new DataSet();

            bool isBool = false;
            string QueryString = "SELECT * from BRANCH where lpad(SIBS_BANK_CODE,5,'0')=" +
                "lpad('" + strBranchID + "',5,'0')";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to DataBase!", 1, MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, QueryString, null);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        isBool = true;
                    }
                }
                oraConn.Close();
                oraConn.Dispose();
                return isBool;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                oraConn.Close();
                oraConn.Dispose();
                return false;
            }
        }

        public static void getDataListBox(ListBox listBox, string strSelectCol, string strTable, 
            string strColWhere1, string strValueCol1, string strColWhere2, string strValueCol2)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            DataSet ds = new DataSet();
            string QueryString = "";
                       

            try
            {
                if (string.IsNullOrEmpty(strValueCol2))
                {
                    QueryString = "SELECT " + strSelectCol + " FROM  " + strTable + " where " +
                    strColWhere1 + "= '" + strValueCol1 + "'" + " AND " + strColWhere2 + " IS NULL";
                }
                else
                {
                    QueryString = "SELECT " + strSelectCol + " FROM  " + strTable + " where " +
                    strColWhere1 + "= '" + strValueCol1 + "'" + " AND " + strColWhere2 + "= '" + strValueCol2 + "'";
                }

                oraConn = conn.Connect();
                if (oraConn == null)
                {                    
                    Common.ShowError("Can not connect to dataBase", 1, MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, QueryString, null);                    
                    int i = 0;
                    listBox.Items.Add("");
                    while (i < ds.Tables[0].Rows.Count)
                    {
                        string strItem = ds.Tables[0].Rows[i][strSelectCol].ToString();
                        listBox.Items.Add(strItem);
                        i = i + 1;
                    }
                }
                oraConn.Close();
                oraConn.Dispose();
            }
            catch (Exception ex)
            {
                Common.ShowError("Can not connect to dataBase Table: " + strTable + " " + ex, 
                    1, MessageBoxButtons.OK);                
                oraConn.Close();
                oraConn.Dispose();
                return;                
            }
        }
        
        public static void LoadData(string tableName, string strSql, DataGridView dgvSearch)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to dataBase", 1, MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                    dt = ds.Tables[tableName];
                    dgvSearch.DataSource = dt;
                }
                oraConn.Close();
                oraConn.Dispose();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                oraConn.Close();
                oraConn.Dispose();
                return;
            }
        }
        
        public static bool IDIsExisting(bool isUpdate, string strTable, string strColumn, string strColumnValue, string strOldValue)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            DataSet ds = new DataSet();
            string strSQL; //= "Select TAD_NAME  from TAD WHERE TAD_NAME ='" + ID + "'";


            try
            {
                if (isUpdate == true)
                    strSQL = "Select " + strColumn + " from " + strTable + " Where trim(" +
                        strColumn + ") ='" + strColumnValue + "' and trim(" +
                        strColumn + ") <> '" + strOldValue + "'";
                else
                    strSQL = "Select " + strColumn + " from " + strTable + " Where trim(" +
                        strColumn + ") ='" + strColumnValue + "'";

                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to DataBase", 1, MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //Common.ShowError("ID already existed!", 1, MessageBoxButtons.OK);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                oraConn.Close();
                oraConn.Dispose();
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool ID2IsExisting(bool isUpdate, string strTable,
            string ID1, string ID2, string strColumnValue1, string strColumnValue2,
            string strOldValue1, string strOldValue2)
        {
            string strSQL;
            Boolean bReturn = false;
            DataSet ds = new DataSet();
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();

            try
            {
                oraConn = conn.Connect();
                if (isUpdate == true)
                    strSQL = "Select " + ID1 + "," + ID2 + " from " + strTable +
                        " Where trim(" + ID1 + ")='" + strColumnValue1 +
                        "' and trim(" + ID1 + ") <>'" + strOldValue1 + "' and trim(" +
                        ID2 + ")='" + strColumnValue2 + "' and trim(" + ID2 + ") <>'" +
                        strOldValue2 + "'";
                else
                    strSQL = "Select " + ID1 + "," + ID2 + " from " + strTable +
                        " Where trim(" + ID1 + ")='" + strColumnValue1 +
                        "' and trim(" + ID2 + ") ='" + strColumnValue2 + "'";
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to DataBase", 1, MessageBoxButtons.OK);
                    return bReturn;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Common.ShowError("ID already existed!", 1, MessageBoxButtons.OK);
                        bReturn = true;
                    }
                }
                oraConn.Close();
                oraConn.Dispose();
                return bReturn;
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                oraConn.Close();
                oraConn.Dispose();
                return false;
            }
        }        

        public static bool IDCCYIsExisting(bool isUpdate, string strColumnCCYCD,
            string strColumnSHORTCD)
        {
            DataSet ds = new DataSet();
            string strSQL;
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            Boolean bReturn = false;

            try
            {

                oraConn = conn.Connect();
                if (isUpdate == true)
                    strSQL = "Select t.ccycd,t.SHORTCD from CURRENCYCODE t" +
                        " Where upper(trim(t.ccycd)) ='" + strColumnCCYCD +
                        "' or upper(trim(t.SHORTCD))='" + strColumnSHORTCD + "'";
                else
                    ///////////////////////////////////////////////-/
                    //Muc dich: sua cau sql
                    //Ngay sua: 03/08/2008                    
                    strSQL = "Select t.ccycd,t.SHORTCD from CURRENCYCODE t" +
                        " Where upper(trim(t.SHORTCD)) ='" + strColumnCCYCD +
                        "' or upper(trim(t.ccycd))='" + strColumnSHORTCD + "'";
                    /////////////////////////////////////////////////
                if (oraConn == null)
                {
                    Common.ShowError("Can not connect to DataBase!", 1, MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //Common.ShowError("ID already existed!", 1, MessageBoxButtons.OK);
                        bReturn = true;
                    }
                }
                oraConn.Close();
                oraConn.Dispose();
                return bReturn;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
                return false;
            }
        }                        
               
        public static string Getwarning(string strTitle)
        {
            OracleConnection oraConn = new OracleConnection();
            Connect_Process conn = new Connect_Process();
            
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pchannel",OracleType.VarChar,500),
                                                new OracleParameter("strwarning",OracleType.VarChar,500)                                                
                                            };
                oraParas[0].Value = strTitle;
                oraParas[1].Value = "";
                oraParas[0].Direction = ParameterDirection.Input;
                oraParas[1].Direction = ParameterDirection.Output;
                oraConn = conn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                else
                {
                    DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, 
                        "gw_pk_warning.selectwarning", oraParas);
                    return oraParas[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
            }
            return null;
        }

        /*************************
         * Cac ham lay du lieu phuc vu cho AWS(Auto Warning System)
         * HoangLA
         * 17-08-08
         *************************/        
        #region AWS
        public static int GetTimerWarning()
        {
            try
            {
                int Interval = Convert.ToInt32(ConfigSettings.ReadSetting("TimerWarning"));
                return Interval;
            }
            catch 
            { 
            }
            return 600000;        
        }
        #endregion
        /*************************
         * Cac ham lay du lieu phuc vu cho MRS(Messages Reconcile System)
         * HoangLA
         * 17-08-08
         ************************/

        #region MRS
        public static string GetSYSVAR(string strVarName)
        {
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();
                string strSQL = "select value from sysvar where varname='" + strVarName + "' and rownum=1";
                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return "";
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0]["value"].ToString().Trim();
                        
                    }
                    else
                    {
                        return "";
                    }
                }
               // oraConn.Close();
                //oraConn.Dispose();
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Cannot get REC_PATH ", Common.sCaption);
                return "";
            }
        }
                
        /**************************************************
         * Ham lay gia tri tham so trong bang RecParameter
         **************************************************/
        public static string GetRecParameter(string pName,string pGWTYPE,string pTYPE)
        {
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();
                string strSQL = "select content from rec_parameters where pname='" + pName + "' and type='"+pTYPE+"' and gwtype='"+pGWTYPE+"' and rownum=1";
                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return "";
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0]["content"].ToString().Trim();
                        
                    }
                    else
                    {
                        return "";
                    }
                }
                //oraConn.Close();
                //oraConn.Dispose();
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Cannot get REC_PARAMETERS ", Common.sCaption);
                return "";
            }       
        }

        /**************************************************
         * Ham dinh dang format cua xau
         **************************************************/
        public static int CheckFormat(string pStringSource,string vFormat, string[] pParams)
        {
            // Build Params
            
            // Build SQL
            string strSQL = "select 1 from dual where '" + pStringSource + "' like " + vFormat + " ";
            if (pParams != null && pParams.Length > 0)
            {
                for (int i = 0; i < vFormat.Length; i++) { strSQL.Replace("p" + i.ToString(),pParams[i]); }
            }
            
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();
                
                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return -1;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return 1;
                       
                    }                   
                    return -1;
                    
                }
               
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Cannot get Check Format " + vFormat, Common.sCaption);
                return -1;
            }
        }

        /**************************************************
         * Ham dem so luong ban ghi trong bang
         **************************************************/
        public static int GetCount(string vSubSQL)
        {
            // Build Params

            // Build SQL
            string strSQL = "select count(1) " + vSubSQL;
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();

                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return -1;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                    
                    return (int)Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    //MessageBox.Show("cannot execute cmd : " + strSQL);
                }
                //oraConn.Close();
                //oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("cannot execute cmd : " + strSQL +ex.Message);
                return -1;
            }
        }

        /**************************************************
         * Ham Select ra mot du lieu tuy y
         **************************************************/
        public static DataSet GetSelect(string vSubSQL)
        {
            // Build SQL
            string strSQL = "select " + vSubSQL;
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();

                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return null;
                }
                else
                {
                    ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                    
                    return ds;
                    //MessageBox.Show("cannot execute cmd : " + strSQL);
                }
                //oraConn.Close();
                //oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("cannot execute cmd : " + strSQL + ex.Message);
                return null;
            }
        }

        /**************************************************
         * Ham Delete dien 
         **************************************************/
        public static int ExcuteNonQuery(string strSQL)
        {
            // Build SQL            
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();

                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return -1;
                }
                else
                {
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                                        
                }
                //oraConn.Close();
                //oraConn.Dispose();
            }
            catch //(Exception ex)
            {
                MessageBox.Show("cannot execute cmd : " + strSQL);
                return -1;
            }
        }

        /**************************************************
         * Ham ExcuteStore 
         **************************************************/
        public static int ExcuteStore(string strSPName,OracleParameter[] param )
        {
            // Build SQL            
            DataSet ds = new DataSet();
            try
            {
                OracleConnection oraConn = new OracleConnection();
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();

                if (oraConn == null)
                {
                    MessageBox.Show("Can not connect to DataBase");
                    return -1;
                }
                else
                {
                    return DataAcess.ExecuteNonQuery(oraConn,CommandType.StoredProcedure ,strSPName, param);
                    //ds = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                                        
                }
                //oraConn.Close();
               // oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }
        }

        /**************************************************
         * Ham ExcuteSelect
         **************************************************/
        public static DataTable ExcuteSelect(string strSPName, OracleParameter[] param)
        {                       
            DataSet ds = new DataSet();
            OracleConnection oraConn = new OracleConnection();
            try
            {
                
                Connect_Process conn = new Connect_Process();
                oraConn = conn.Connect();

                if (oraConn == null)
                {
                    //MessageBox.Show("Can not connect to DataBase");
                    return null;
                }
                else
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = oraConn;
                    cmd.CommandText = strSPName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (OracleParameter p in param)
                    {
                    if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds == null) { return null; }
                    return ds.Tables[0];
                    //da.Dispose();
                    //cmd.Dispose();                
                }
                //oraConn.Close();
                //oraConn.Dispose();
            }
            catch (Exception ex)
            {
                if (oraConn == null) { return null; }
                if (oraConn.State != ConnectionState.Closed)
                { 
                    oraConn.Close();
                    oraConn.Dispose();
                    return null;
                }
            }
            return null;
        }

        #endregion
    }

    
}
