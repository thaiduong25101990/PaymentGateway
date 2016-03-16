using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRFTPService.Technique
{
    // Nhom thu muc GW
    public class GWFLDs
    {
        public string Normal = "";
        public string BackUp = "";
        public string Error  = "";
        public GWFLDs()
        {            
        }
    }
    // Nhom thu muc FTP FOLDERS
    public class FTPFOLDERS
    {
        public string Host = "";
        public string Username = "";
        public string Password = "";

        public string Normal = "";
        public string BackUp = "";
        public string Error = "";
        public FTPFOLDERS()
        {            
        }
    }
}
