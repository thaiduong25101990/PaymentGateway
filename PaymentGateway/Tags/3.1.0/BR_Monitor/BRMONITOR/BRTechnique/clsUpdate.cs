using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using BR.BRLib;


namespace BR.BRMONITOR.BRTechnique
{
    public class Update
    {
                
        public static void CheckConnectServer()
        {
            string vServerName = "ServerName";
            string vCheckDir   = "CheckDir";
            string vUserName   = "UserName";
            string vPassword   = "Password";
            string vMessage    = "Message";
            string vIsUpdate   = "IsUpdate";

            try
            {
                DataTable dt = GetData.GetSelect(" * from brupdate").Tables[0];
                vServerName = dt.Select("Name = 'ServerName'")  [0]["Value"].ToString();
                vCheckDir   = dt.Select("Name = 'CheckDir'")    [0]["Value"].ToString();
                vUserName   = dt.Select("Name = 'UserName'")    [0]["Value"].ToString();
                vPassword   = dt.Select("Name = 'Password'")    [0]["Value"].ToString();
                vMessage    = dt.Select("Name = 'Message'")     [0]["Value"].ToString();
                vIsUpdate   = dt.Select("Name = 'IsUpdate'")    [0]["Value"].ToString();
            }
            catch(Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);                
                return;
            }
            if (vIsUpdate != "1") { return; };
            try
            {
                DirectoryInfo di = new DirectoryInfo(vCheckDir);
                di.GetDirectories();
            }
            catch
            {
                try
                {
                    MessageBox.Show(vMessage, Common.sCaption);
                    ProcessStartInfo si = new ProcessStartInfo();
                    si.FileName = @"\\" + vServerName;
                    Process.Start(si);                    
                }
                catch(Exception ex1)
                {
                    Common.ShowError(ex1.Message, 2, MessageBoxButtons.OK);
                }
                
            }

        }

    }
}
