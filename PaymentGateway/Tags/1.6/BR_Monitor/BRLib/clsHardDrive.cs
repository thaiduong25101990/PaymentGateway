using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace BR.BRLib
{
    public class clsHardDrive
    {
        private string model = null;
        private string type = null;
        private string serialNo = null;
        private string strHDD_serial;
        private clsFile objFile = new clsFile();
        private UserEncrypt objEncrypt = new UserEncrypt();

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Model
        // Ngay tao:     
        // Nguoi tao:    
        ///////////////////////////////////////////////////////////
        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Kieu HDD
        // Ngay tao:    
        // Nguoi tao:    
        ///////////////////////////////////////////////////////////
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       So serial HDD
        // Ngay tao:    
        // Nguoi tao:   
        ///////////////////////////////////////////////////////////
        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        //////////////////////////////////////////////////////////-/
        // Mo ta:       Hien thi thong bao
        // Tham so:     
        // Tra ve:      Show sucessfull
        // Ngay tao:    12/2008
        // Nguoi tao:         
        // Ngay sua:    01/2009
        // Nguoi sua:     
        ///////////////////////////////////////////////////////////
        public string GetHDD_Serial()
        {
            ArrayList hdCollection = new ArrayList();
            

            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                clsHardDrive hd = new clsHardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();

                hdCollection.Add(hd);
            }

            searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                clsHardDrive hd = (clsHardDrive)hdCollection[i];

                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] == null)
                    hd.SerialNo = "None";
                else
                    hd.SerialNo = wmi_HD["SerialNumber"].ToString();

                ++i;
            }
            foreach (clsHardDrive hd in hdCollection)
            {
                strHDD_serial = hd.SerialNo;
            }
            return strHDD_serial;
        }


        //////////////////////////////////////////////////////////-/
        // Mo ta:       Ham kiem tra thong tin cai dat
        // Tham so:     NA
        // Tra ve:      True: sucessfull
        // Ngay tao:    12/2008
        // Nguoi tao:   
        // Ngay sua:    01/2009
        // Nguoi sua:     
        ///////////////////////////////////////////////////////////
        public bool check_setup()
        {
            bool check = true;
            try
            {
                List<FileInfo> list = new List<FileInfo>();
                list.Clear();
                //kiem tra xem co file ton tai chua
                list = objFile.ScanFile(Common.FilePath, Common.File_key);
                //khong ton tai thu muc do
                if (list.Count == 0)
                    check = false;                
                else if (list.Count != 0)
                {
                    //ma hoa so serial 
                    string strHDD = objEncrypt.Encrypt(GetHDD_Serial(), objEncrypt.sKeySetup);
                    string NOIDUNG = objFile.OpenFile(Common.FilePath + "\\" + Common.File_key);
                    if (strHDD == NOIDUNG)                    
                        check = true;                    
                    else
                    {
                        check = false;
                        objFile.MoveFile(Common.File_key, Common.FilePath);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                check = false;
            }
            return check;
        }



    }
}
