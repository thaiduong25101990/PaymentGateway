using System;
using System.Data;
using System.Data.OleDb;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Text;
using BIDVWEB.Comm;

namespace BIDVWEB.Comm
{       
        
    public class clsRunStore
    {
        //Khai bao bien dung cho lop DataAccess
        private clsConnection objConn = new clsConnection();
        private clsCommon objCommon = new clsCommon();
        private Oracle.DataAccess.Client.OracleConnection Conn;        
        private OracleCommand cmd;        
        private OracleDataAdapter oraDataAdapter = new OracleDataAdapter();        
        public string strError = "";
        public int iErr = 0;


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
            string sArrDataType, out DataSet dsview, out string strArrValueReturn)
        {            
            cmd=new OracleCommand();            
            dsview = null;
            strArrValueReturn = "";
            int iSession = -1;

            try
            {   
                strError = "";
                Conn = objConn.ConnectOra();                                
                
                //Mang luu cac tham so dau vao cho SP
                string[] arrayName;
                //Mang luu cac tham tri can truyen vao SP
                string[] arrayValue;
                //Mang luu cac kieu du lieu cua tham so
                string[] arrayDataType;
                //Mang luu cac kieu du lieu cua tham so
                string[] arraySession;
                //Ky tu ket noi cac phan tu o 2 mang tren
                char[] splitter = { '|' };                

                //Tao cac phan tu mang
                arrayName = sArrName.Split(splitter);
                arrayValue = sArrValue.Split(splitter);
                arrayDataType = sArrDataType.Split(splitter);
                arraySession = sArrValue.Split(splitter);
                
                //Khai bao mang luu gia tri tra ve
                string[] arrValueReturn = new string[arrayName.Length];
                
                // create and setup command to call stored procedure 
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Conn;

                //Gan cac tham so cho SP
                for (int i = 0; i <= arrayName.Length - 1; i++)
                {
                    arraySession[i] = "0";
                    //Gan lai mang gia tri
                    arrValueReturn[i] = arrayValue[i];
                    //Goi OracleDbType
                    Oracle.DataAccess.Client.OracleDbType oraDbtype =
                        new Oracle.DataAccess.Client.OracleDbType();
                    oraDbtype = GetOraDbType(arrayDataType[i]);
                    //arrayDataType[i]
                    if (i < arrayName.Length - 1)
                    {   
                        Oracle.DataAccess.Client.OracleParameter prm =
                            new Oracle.DataAccess.Client.OracleParameter(arrayName[i],
                            oraDbtype);
                        //Lay ra vi tri bien session, gan thuoc tinh out
                        if (arrayName[i].ToUpper() == "pSession".ToUpper() ||
                            arrayName[i].ToUpper() == "pPrintStatus".ToUpper())
                        {
                            prm.Direction = ParameterDirection.InputOutput;   
                            iSession = i;
                            arraySession[i] = "1";
                        }
                        else
                        {
                            prm.Direction = ParameterDirection.Input;                            
                        }
                        //Kieu du lieu date
                        if (arrayDataType[i].ToUpper() == "Date".ToUpper())
                        {                            
                            if (arrayValue[i] != null && arrayValue[i] != "")                            
                                //prm.Value = Convert.ToDateTime(arrayValue[i]);
                                //Xu ly lai truong hop ngay thang 08/03/2011
							    //prm.Value = Convert.ToDateTime(objCommon.g_Formatdate(arrayValue[i], true));
                                prm.Value = DateTime.ParseExact(arrayValue[i], "MM/dd/yyyy", null); //Convert.ToDateTime(arrayValue[i],"MM/dd/yyyy");
                            else                            
                                prm.Value = null;                            
                        }                        
                        else
                        {
                            if (arrayName[i].ToUpper() == "pSession".ToUpper())
                            {
                                if (arrayValue[i] != null && arrayValue[i].Trim() != "")
                                    if (arrayValue[i].ToString().Trim()=="0")
                                        prm.Value = "1000";
                                    else
                                        prm.Value = arrayValue[i];
                                else
                                    prm.Value = "0";
                            }
                            else
                            {
                                if (arrayValue[i] != null && arrayValue[i].Trim() != "")
                                    prm.Value = arrayValue[i];
                                else
                                    prm.Value = null;
                            }
                        }                        
                        cmd.Parameters.Add(prm);
                    }
                    else
                    {
                        Oracle.DataAccess.Client.OracleParameter prm =
                            new Oracle.DataAccess.Client.OracleParameter(arrayName[i],
                        oraDbtype, DBNull.Value, ParameterDirection.Output);
                        prm.Value = arrayValue[i];
                        cmd.Parameters.Add(prm);
                    }                                        
                }
                //Gan du lieu cho dataset
                //cmd.ExecuteNonQuery();
                oraDataAdapter = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();                
                oraDataAdapter.Fill(ds,"AAAA");
                if (ds == null) { strError = "Không có dữ liệu"; }
                else if (ds.Tables[0].Rows.Count == 0) 
                {
                    dsview = ds;
                    strError = "Không có dữ liệu!"; 
                }
                else if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dsview = ds;
                }                
                //Lay session
                
                //////if (iSession != -1 && !string.IsNullOrEmpty(cmd.Parameters[iSession].Value.ToString()))
                //////{
                //////    arrValueReturn[iSession] = cmd.Parameters[iSession].Value.ToString();                    
                //////    //Lay mang gia tri tra ve
                //////    for (int i = 0; i < arrValueReturn.Length; i++)
                //////    {                      
                //////        if (string.IsNullOrEmpty(strArrValueReturn))
                //////            strArrValueReturn = strArrValueReturn + arrValueReturn[i];
                //////        else
                //////            strArrValueReturn = strArrValueReturn + "|" + arrValueReturn[i];                      
                //////    }                    
                //////}
                //////else                
                //////    strArrValueReturn = sArrValue;                   
                
                //Lay mang gia tri tra ve
                for (int i = 0; i < arrValueReturn.Length; i++)
                {
                    if (arraySession[i].ToString()=="1")
                    {
                        arrValueReturn[i] = cmd.Parameters[i].Value.ToString();
                    }
                    if (string.IsNullOrEmpty(strArrValueReturn))
                        strArrValueReturn = strArrValueReturn + arrValueReturn[i];
                    else
                        strArrValueReturn = strArrValueReturn + "|" + arrValueReturn[i];
                }               
                
             
                //Huy cac doi tuong conn, cmd, DataAdapter                
                oraDataAdapter.Dispose();
                cmd.Dispose();                
                Conn.Close();
                Conn.Dispose();
                
            }
            catch (Exception ex)
            {
                //Huy cac doi tuong conn, cmd, DataAdapter
                oraDataAdapter.Dispose();
                cmd.Dispose();                
                Conn.Close();
                Conn.Dispose();
                iErr = -1;
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
        private Oracle.DataAccess.Client.OracleDbType GetOraDbType(string strDataType)
        {
            OracleDbType oraDbType = new OracleDbType(); 
            switch (strDataType)
            {
                case "OracleDbType.Int16":
                    oraDbType= OracleDbType.Int16;
                    break;
                case "OracleDbType.Date":
                    oraDbType = OracleDbType.Date;
                    break;
                case "OracleDbType.RefCursor":
                    oraDbType = OracleDbType.RefCursor;
                    break;
                case "OracleDbType.Varchar2":
                    oraDbType = OracleDbType.Varchar2;
                    break;
                case "OracleDbType.Char":
                    oraDbType = OracleDbType.Char;
                    break;
                case "OracleDbType.Decimal":
                    oraDbType = OracleDbType.Decimal;
                    break;
                case "OracleDbType.Long":
                    oraDbType = OracleDbType.Long;
                    break;
                case "OracleDbType.NVarchar2":
                    oraDbType = OracleDbType.NVarchar2;
                    break;
                case "OracleDbType.Double":
                    oraDbType = OracleDbType.Double;
                    break;
                case "NUMBER":
                    oraDbType = OracleDbType.Double;
                    break;
                case "DATE":
                    oraDbType = OracleDbType.Date;
                    break;
                case "CHAR":
                    oraDbType = OracleDbType.Char;
                    break;
                case "VARCHAR2":
                    oraDbType = OracleDbType.Varchar2;
                    break;
                case "CURSOR":
                    oraDbType = OracleDbType.RefCursor;
                    break;
            }
            return oraDbType;
        }
        
    }
}

