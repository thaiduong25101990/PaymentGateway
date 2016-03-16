using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRIBPS.Technical
{
    public class clsThread
    {
        public static List<string> listTAD = new List<string>();// Danh sach TAD extract
        public static DateTime pDate = DateTime.Now;// Ngày Extract dữ liệu
        /*
         * Update ngay 04062010 
         * iOption=1 Extract Database Link
         * iOption=2 Extract xuat file sql
         * 
         */
        public static void ExtractData(string strPath, int  iOption )
        {
            int i =0;
            string strExportText = "";

            string strSPName = "gw_pk_reconcile.extractdataforbranch";
            for (int TADCnt = 0; TADCnt < listTAD.Count; TADCnt++)
            {
                try
                {
                    if (iOption == 1)
                    {
                        OracleParameter[] param = {   new OracleParameter("pDate",OracleType.DateTime,20),
                                              new OracleParameter("pTAD",OracleType.VarChar,20) ,
                                              new OracleParameter("pOK",OracleType.VarChar,1000)};
                        param[2].Direction = ParameterDirection.InputOutput;
                        param[0].Value = pDate.Date;
                        param[1].Value = listTAD[TADCnt].ToString();
                        param[2].Value = " ";
                        int iResult = GetData.ExcuteStore(strSPName, param);
                        if (iResult > 0) { MessageBox.Show(param[2].Value.ToString(), Common.sCaption); }
                    }
                    else if (iOption == 2)
                    {
                        DataTable _dt = new DataTable();
                        OracleParameter[] param = {   new OracleParameter("pDate",OracleType.DateTime,20),
                                              new OracleParameter("pTAD",OracleType.VarChar,20) ,
                                              new OracleParameter("pOK",OracleType.VarChar,1000),
                                                  new OracleParameter("pCursor", OracleType.Cursor)};
                        param[2].Direction = ParameterDirection.InputOutput;
                        param[3].Direction = ParameterDirection.Output;
                        param[0].Value = pDate.Date;
                        param[1].Value = listTAD[TADCnt].ToString();
                        param[2].Value = " ";
                        _dt = new DataTable();
                       _dt = GetData.ExcuteSelect(strSPName, param);
                       strExportText = "Connect gwcitad/gwcncitad@gwcitad" + "\r\n" + "Delete from ibps_msg_content_tad where ndate= " + pDate.Date.Year.ToString() + pDate.Date.Month.ToString().PadLeft(2, '0') + pDate.Date.Day.ToString().PadLeft(2, '0') + ";";
                       for (i = 0; i < _dt.Rows.Count; i++)
                       {
                           strExportText = strExportText + "\r\n" + _dt.Rows[i]["content"].ToString();
                       }
                       strExportText = strExportText + "\r\n" + "Commit;";
                       clsImport.ExportFile(strPath, "Citad" + listTAD[TADCnt].ToString() + pDate.Date.Year.ToString() + pDate.Date.Month.ToString().PadLeft(2, '0') + pDate.Date.Day.ToString().PadLeft(2, '0') + ".bat", "sqlplus /nolog @" +strPath + @"\Citad" + listTAD[TADCnt].ToString() + pDate.Date.Year.ToString() + pDate.Date.Month.ToString().PadLeft(2, '0') + pDate.Date.Day.ToString().PadLeft(2, '0') + ".txt");
                       clsImport.ExportFile(strPath, "Citad"+listTAD[TADCnt].ToString() + pDate.Date.Year.ToString() + pDate.Date.Month.ToString().PadLeft(2, '0') + pDate.Date.Day.ToString().PadLeft(2, '0') + ".txt", strExportText);
                       
                    }
                }
                catch (Exception ex ){ MessageBox.Show(ex.Message, Common.sCaption); }
               
            }
            MessageBox.Show("Extract success", Common.sCaption);
            // Hủy thread
            //Thread.CurrentThread.Abort();
        }

        public static void ExtractData()
        {
            string strSPName = "gw_pk_reconcile.extractdataforbranch";
            for (int TADCnt = 0; TADCnt < listTAD.Count; TADCnt++)
            {
                try
                {
                    
                        OracleParameter[] param = {   new OracleParameter("pDate",OracleType.DateTime,20),
                                              new OracleParameter("pTAD",OracleType.VarChar,20) ,
                                              new OracleParameter("pOK",OracleType.VarChar,1000)};
                        param[2].Direction = ParameterDirection.InputOutput;
                        param[0].Value = pDate.Date;
                        param[1].Value = listTAD[TADCnt].ToString();
                        param[2].Value = " ";
                        int iResult = GetData.ExcuteStore(strSPName, param);
                        if (iResult > 0) { MessageBox.Show(param[2].Value.ToString(), Common.sCaption); }
                 
                }
                catch { }
            }
            // Hủy thread
            Thread.CurrentThread.Abort();
        }
    }
}
