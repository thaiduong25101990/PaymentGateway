using System;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Text;
using System.Windows.Forms;
using BR.DataAccess;

namespace BR.BRBusinessObject
{
    public class clsRunStore
    {        
        System.Data.OracleClient.OracleConnection oraConn = new System.Data.OracleClient.OracleConnection();
        Connect_Process connect = new Connect_Process();
        public string sError = "";
         

        ///////////////////////////////////////////////////////////
        // Mo ta:       Ham convert kieu du lieu datetime        
        // Tham so:     sDate: Gia tri kieu chuoi truyen vao
        //              bDDmmyy:True: "DD/MM/YYYY" => "MM/DD/YYYY"
        //                      False: "MM/DD/YYYY" => "DD/MM/YYYY"
        // Tra ve:      Successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        public string g_Formatdate(string sDate, bool bDDmmyy)
        {
            string str = "";
            string sDay = "";
            string sMonth = "";
            string sYear = "";
            string[] arrayDate = new string[2];
            char[] splitter = { '/' };

            if (sDate == "")
            {
                return "";
            }
            arrayDate = sDate.Split(splitter);
            //"DD/MM/YYYY" => "MM/DD/YYYY"
            if (bDDmmyy == true)
            {
                sDay = arrayDate[0];
                sMonth = arrayDate[1];
                sYear = arrayDate[2];
                str = sMonth.PadLeft(2, '0') + "/" + sDay.PadLeft(2, '0') + "/" + sYear;
            }
            //"MM/DD/YYYY" => "DD/MM/YYYY"
            else
            {
                sDay = arrayDate[0];
                sMonth = arrayDate[1];                
                sYear = arrayDate[2];
                str = sDay.PadLeft(2, '0') + "/" + sMonth.PadLeft(2, '0') + "/" + sYear;
            }
            return str;
        }


        ///////////////////////////////////////////////////////////
        // Mo ta:       Chay store procedure cua bao cao
        // Tham so:     sSQL: Ten store procedure
        //              (gom ca ten package)
        //              sArrName: Ten cac tham so
        //              sArrValue: Gia tri cac tham so
        //              sArrDataType: Kieu du lieu cac tham so
        //              dsview: Dataset duoc tra ve
        // Tra ve:      Successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        public void RunStoreProcedure(string sSQL, string sArrName, string sArrValue, string sArrDataType, out DataSet dsview)
        {
            try
            {                
                sError = "";                                          
                //Mang luu cac tham so dau vao cho SP
                string[] arrayName;
                //Mang luu cac tham tri can truyen vao SP
                string[] arrayValue;
                //Mang luu cac kieu du lieu cua tham so
                string[] arrayDataType;
                //Ky tu ket noi cac phan tu o 2 mang tren
                char[] splitter = { '|' };
                dsview = null;

                //Tao cac phan tu mang
                arrayName = sArrName.Split(splitter);
                arrayValue = sArrValue.Split(splitter);
                arrayDataType = sArrDataType.Split(splitter);

                //Khoi tao connection
                oraConn = connect.Connect();
                if (oraConn == null)                
                    return;               

                //
                System.Data.OracleClient.OracleParameter[] Oraparam = new System.Data.OracleClient.OracleParameter[arrayName.Length];                               

                //Gan cac tham so cho SP
                for (int i = 0; i <= arrayName.Length - 1; i++)
                {
                    if (i < arrayName.Length - 1)
                        arrayName[i] = "p" + arrayName[i].Substring(2, arrayName[i].Length - 2);
                    System.Data.OracleClient.OracleParameter prm =
                        new System.Data.OracleClient.OracleParameter(arrayName[i], GetOracleType(arrayDataType[i]));

                    if (arrayDataType[i] == "DateTimeParameter")
                    {
                        string strdate = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern.ToString().Trim();
                        if (strdate.ToUpper() == "M/D/YYYY" || strdate.ToUpper() == "MM/DD/YYYY" || strdate.ToUpper() == "MM/DD/YY" || strdate.ToUpper() == "M/D/YY")
                        {
                            arrayValue[i] = g_Formatdate(arrayValue[i], true);
                            prm.Value = Convert.ToDateTime(arrayValue[i]);
                        }
                        else
                        {
                            arrayValue[i] = g_Formatdate(arrayValue[i], false);
                            prm.Value = Convert.ToDateTime(arrayValue[i]);
                        }
                    }
                    else                    
                        prm.Value = arrayValue[i];                    
                    if (arrayDataType[i] == "Cursor" || arrayDataType[i] == "OracleDbType.RefCursor")                    
                        prm.Direction = ParameterDirection.Output;                                       

                    Oraparam[i] = prm;                   

                    #region "Test"  
                    ////System.Data.OracleClient.OracleCommand cmd = new System.Data.OracleClient.OracleCommand();
                    ////cmd.CommandText = sSQL;
                    ////cmd.CommandType = CommandType.StoredProcedure;
                    ////cmd.Connection = connect.Connect(); 
                    //////////Goi OracleDbType
                    ////System.Data.OracleClient.OracleType oraDbtype =
                    ////    new System.Data.OracleClient.OracleType();
                    ////oraDbtype = GetOracleType(arrayDataType[i]);
                    //////                  
                    ////if (i < arrayName.Length - 1)
                    ////    arrayName[i] = "p" + arrayName[i].Substring(2, arrayName[i].Length - 2);                    
                    ////System.Data.OracleClient.OracleParameter prm =
                    ////    new System.Data.OracleClient.OracleParameter(arrayName[i], oraDbtype);
                    ////if (arrayDataType[i] == "Cursor" || arrayDataType[i] == "OracleDbType.RefCursor")
                    ////    prm.Direction = ParameterDirection.Output;
                    ////else
                    ////    prm.Direction = ParameterDirection.Input;
                    ////if (arrayDataType[i] == "DateTimeParameter")
                    ////{
                    ////    string strdate = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern.ToString().Trim();
                    ////    if (strdate.ToUpper() == "M/D/YYYY" || strdate.ToUpper() == "MM/DD/YYYY" || strdate.ToUpper() == "MM/DD/YY" || strdate.ToUpper() == "M/D/YY")
                    ////    {
                    ////        arrayValue[i] = g_Formatdate(arrayValue[i], true);
                    ////        prm.Value = Convert.ToDateTime(arrayValue[i]);
                    ////    }
                    ////    else
                    ////    {
                    ////        arrayValue[i] = g_Formatdate(arrayValue[i], false);
                    ////        prm.Value = Convert.ToDateTime(arrayValue[i]);
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    prm.Value = arrayValue[i];
                    ////}
                    ////cmd.Parameters.Add(prm);
                    ////System.Data.OracleClient.OracleDataAdapter oraDataAdapter = new System.Data.OracleClient.OracleDataAdapter(cmd);
                    ////oraDataAdapter.Fill(ds);                    
                    #endregion
                }
                dsview = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, sSQL, Oraparam);
                if (dsview == null) { sError = "Dataset is null!"; }
                oraConn.Close();
                oraConn.Dispose();
            }
            catch (Exception ex)
            {
                oraConn.Close();
                oraConn.Dispose();
                dsview = null;
                sError = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }


        ///////////////////////////////////////////////////////////
        // Mo ta:       Lay kieu du lieu chuan cua tham so trong
        //              store procedure
        // Tham so:     sDataType: Kieu setup
        // Tra ve:      Successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private System.Data.OracleClient.OracleType GetOracleType(string sDataType)
        {
            System.Data.OracleClient.OracleType oraDbType = new System.Data.OracleClient.OracleType();
            switch (sDataType)
            {
                case "OracleType.DateTime":
                    oraDbType = System.Data.OracleClient.OracleType.DateTime;
                    break;
                case "OracleType.Cursor":
                    oraDbType = System.Data.OracleClient.OracleType.Cursor;
                    break;
                case "OracleType.Char":
                    oraDbType = System.Data.OracleClient.OracleType.Char;
                    break;
                case "OracleType.VarChar":
                    oraDbType = System.Data.OracleClient.OracleType.VarChar;
                    break;
                case "OracleType.NChar":
                    oraDbType = System.Data.OracleClient.OracleType.NChar;
                    break;
                case "OracleType.NVarChar":
                    oraDbType = System.Data.OracleClient.OracleType.NVarChar;
                    break;
                case "OracleType.Int16":
                    oraDbType = System.Data.OracleClient.OracleType.Int16;
                    break;
                case "OracleType.Int32":
                    oraDbType = System.Data.OracleClient.OracleType.Int32;
                    break;
                case "OracleType.Float":
                    oraDbType = System.Data.OracleClient.OracleType.Float;
                    break;
                case "OracleType.Double":                    
                    oraDbType = System.Data.OracleClient.OracleType.Double;
                    break;                
                case "NumberParameter":
                    oraDbType = System.Data.OracleClient.OracleType.Number;
                    break;
                case "DateTimeParameter":
                    oraDbType = System.Data.OracleClient.OracleType.DateTime;
                    break;
                case "StringParameter":
                    oraDbType = System.Data.OracleClient.OracleType.VarChar;
                    break;
                case "Cursor":
                    oraDbType = System.Data.OracleClient.OracleType.Cursor;
                    break;
                case "OracleDbType.RefCursor":
                    oraDbType = System.Data.OracleClient.OracleType.Cursor;
                    break;
            }
            return oraDbType;
        }
    }
    
}
