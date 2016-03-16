/****************************************************************
 * File: IBPSImport.cs
 * ----------------------------
 * Noi dung: 
 * - Xay dung cac ham xu ly cho Service IBPS_Import_In
 * ----------------------------
 * Nguoi tao: TRUNGNV
 * FPT Infomation System - Banking Software Solution Center
 ****************************************************/
//=================================================================

#region Using Area
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Data.OracleClient;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Reflection;

#endregion

namespace BRIBPSImport
{
    public partial class IBPSImport : ServiceBase
    {
        public static bool isStop = false;
        public static bool isEncrypt = false;
        public static string EncryptPath = "";
        public static string DecryptPath = "";
        public static int timeSleep = 200; // Thoi gian tre cho service

        #region Cac bien va Hang so
        private const string key = "Security2008"; //Encrypt Key 
        private const string MSG_TYPE = "IBPS-SIBS"; // Chieu dien va kenh thanh toan
        private const string BACKUP_FOLDER = "Backup"; // Thu muc Backup trong thu muc TAD ngay hien tai
        private const string ERROR_FOLDER = "ExpError"; // Thu muc chua file Error  trong thu muc TAD ngay hien tai

        private const string LOG_FOLDER = @"C:\ServiceLog"; // Thu muc ghi file log - Mac dinh la @"C:\ServiceLog"
        private const string LOG_FILENAME = "BRIBPSImport_Service.log"; // Ten file log cua Service

        // Cac gia tri cua Log Level
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;

        // Ten cua Service
        public static string SERVICE_NAME="";
        // Duong dan file config - cau hinh Database
        private string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";
                
        private static string dateNow; // lay ngay thang theo dang yyyyMMdd
        private static string timeNow; // Lay full time theo dang 208/04/15 10:30:25 PM
        private static string dateFolder;  // lay ten Folder theo ngay gio hien tai
        private static string dateFile; // Tim ten file theo dinh dang ngay thang

        #endregion

        public IBPSImport()
        {
            InitializeComponent();
            OnService();
        }

        #region  Cac ham OnStart va OnStop
        protected override void OnStart(string[] args)
        {
            try
            {
                SERVICE_NAME = this.ServiceName;
                //WriteLogDB(LOG_Info, this.ServiceName + " On Start");
                Thread th = new Thread(new ThreadStart(OnService));
                th.Start();

            }
            catch //(Exception ex)
            {
                //WriteLogDB(LOG_Error, this.ServiceName + " Error Try-Catch on Process: " + ex.Message,1);
                base.OnStart(args);
            }
        }

        protected override void OnStop()
        {
            //WriteLogDB(LOG_Info, this.ServiceName + " On Stop...");
            isStop = true;
        }

        private void OnService()
        {
            SERVICE_NAME = this.ServiceName;
            while (!isStop)
            {
                // Goi ham truoc khi xu li
                BeforeProcess();
                    
                //ServiceProcess SrvProcess = new ServiceProcess();
               // WriteLogDB(LOG_Info, this.ServiceName + " Start new process");
                StartProcess();
                // Dat thoi gian tre
                Thread.Sleep(timeSleep);
            }
        }


        #endregion
        

        /*------------------------------------------------------
         * GATEWAY CODE HERE
         * ----------------------------------------------------*/

        // Ham kiem tra de lay cac tham so ve ma hoa ENCRYPT
        private void BeforeProcess()
        {
            try
            {
                DataTable datGWType = new DataTable();


                // cap nhat moi 03/12/2008  lay thoi gian delay cua service tu bang GWService_port.
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRIBPSImport')";
                datGWType = new DataTable();
                datGWType = ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();
                //Het cap nhat


                //// Lay cac thong tin ve ENCRYPT
                //strCmd = "SELECT ENCRYPT, ENCRYPTFUNCTION,DECRYPTFUNCTION, FILETIME From GWTYPE WHERE GWTYPE = 'IBPS'";

                //datGWType = new DataTable();
                //datGWType = ExcuteDataTable(strCmd);
                //if (datGWType.Rows.Count > 0)
                //{
                //    //timeSleep = Convert.ToInt32(datGWType.Rows[0]["FILETIME"]);
                //    if (datGWType.Rows[0]["ENCRYPT"] + "" == "1")
                //    {
                //        isEncrypt = true;
                //    }
                //    else
                //        return;

                //    EncryptPath = datGWType.Rows[0]["ENCRYPTFUNCTION"].ToString() + "";
                //    DecryptPath = datGWType.Rows[0]["DECRYPTFUNCTION"].ToString() + "";  //dtReader["DECRYPTFUNCTION"].ToString();
                //}
                //datGWType.Clear();
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, " Khong the lay thong tin ve Encrypt trong GWTYPE " + ex.Message,1);
                return;
            }
        }
        
        /*---------------------------------------------------------------
        * Method           : StartProcess() 
        * Muc dich         : Ham goi bat dau phuong thuc xu li Service
        * Tham so          : Khong co tham so
        * Tra ve           : void
        * Ngay tao         : 05/04/2008
        * Nguoi tao        : TrungNV3
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : TrungNV3
        *--------------------------------------------------------------*/
        #region Ham StartProcess()
        public void StartProcess()
        {
            try
            {
                if (!isStop)
                {
                    // Lay danh sach cac thu muc can xu li
                    List<string> FolderList = getFolderList();

                    // Neu danh sach Folder la Ro^ng~ hoac Null
                    if (FolderList == null || FolderList.Count == 0)
                        return;  // Thoat khoi viec xu li khi danh sach thu muc rong

                    // Neu co danh sach Thu muc thi bat dau xu li
                    else
                    {
                        // Khoi tao Cac luong de xu li doi voi tung Folder
                        int n_Folder = FolderList.Count;
                        //Thread[] theThreads = new Thread[n_Folder];
                        for (int i = 0; i < n_Folder; i++)
                        {
                            try
                            {
                                // theThreads[i] = new Thread(aFolder.Process);
                                //  theThreads[i].Start();
                                // new FileProcess().WriteLogDB(LOG_FOLDER, "Service.log", "To Process Folder: " + FolderList[i]);
                                
                                // Goi ham xu li tung thu muc
                                ProcessFolder(FolderList[i]);
                                
                            }
                            catch //(Exception ex)
                            {
                                // Neu xay ra loi voi thu muc nao do, thuc hien ghi log
                                //WriteLogDB(LOG_Error, "Khong the xu li thu muc: " + FolderList[i] + " -- " + ex.Message,1);                               
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    MESSAGE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        
        /*---------------------------------------------------------------
        * Method           : ProcessFolder() 
        * Muc dich         : Xu li cac file trong mot Folder
        * Tham so          : Khong co tham so
        * Tra ve           : void
        * Ngay tao         : 05/04/2008
        * Nguoi tao        : TrungNV3
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : TrungNV3
        *--------------------------------------------------------------*/

        #region Ham xu li cac file trong mot Folder : void Process()
        public void ProcessFolder(string folderPath)
        {
            Thread.Sleep(10); // Moi thu muc xu li se nghi 10 mili giay
            try
            {
                if (!isStop)
                {
                    DirectoryInfo dir = new DirectoryInfo(folderPath);
                    List<FileInfo> list = new List<FileInfo>();
                    //WriteLogDB(LOG_Info, "Start search file " + dir.Name);

                    // Bat dau quet File trong thu muc folderPath
                    list = ScanFile(folderPath);

                    // Kiem tra neu so file bang khong
                    if (list == null || list.Count == 0)
                    {  
                        // Neu khong co file nao thi return de sang thu muc khac
                        return;                        
                    }
                    
                    // Neu co file thi bat dau xu li lan luot tung file
                    for (int i = 0; i < list.Count; i++)
                    {
                        //WriteLogDB(LOG_Info, "Start Read file: " + list[i].FullName,1); 
                        
                        FileInfo file = list[i];
                        try
                        {
                            // Gan gia tri truong SIBS_TRANS_NUM bang so RefNum
                            int SIBS_TRANS_NUM = getRefNum();

                            if (SIBS_TRANS_NUM == 0)
                            {
                                // Ghi log neu khong lay duoc RefNum
                                WriteLogDB(LOG_Error, "Can not read from SYSVAR",1);
                                return; // Ket thuc qua trinh xu li file vi khong lay duoc so Ref 
                            }

                            // Neu lay duoc so Ref thi tiep tuc
                            string FileName = file.Name;

                            // Mo noi dung file Text cua dien IBPS_In
                            string MsgContent = OpenFile(file.FullName);
                            //if (isEncrypt==true) 
                            //{
                            //    MsgContent = MakeDecrypt(MsgContent);
                            //}
                            

                            // Convert dien tu Tieng Viet TCVN3 sang Tieng Viet khong dau
                            MsgContent = ConvertVietnamese(MsgContent);
                            
                            // Neu noi dung dien bi rong hoac file bi loi
                            if (string.IsNullOrEmpty(MsgContent))
                            {
                                // Chuyen file vao thu muc loi - Error   
                               WriteLogDB(LOG_Error, "File is empty: " + file.FullName,1); 

                                // File loi - Dua vao thu muc Error theo ngay thang hien tai
                                MoveFile(file, dir.FullName + "\\" + ERROR_FOLDER + "\\" + dateFolderName);
                                
                                                              
                                // Thoat khoi vong lap de xu li file khac
                                continue;
                            }
                            
                            // Neu noi dung dien hop thi thi tiep tuc
                            else
                            {
                                //WriteLogDB(LOG_Info, "File " + FileName + " co noi dung: " + MsgContent);
                                // Tao Parameter va Chuan hoa String Query tranh loi Injection SQL

                                OracleParameter[] parameters = new OracleParameter[]{
                                                    new OracleParameter("pFILE_NAME",FileName),
                                                    new OracleParameter("pMSG_TYPE",MSG_TYPE),
                                                    new OracleParameter("pSIBS_TRANS_NUM",SIBS_TRANS_NUM.ToString()),
                                                    new OracleParameter("pIBPS_CONTENT",MsgContent),
                                                    new OracleParameter("pSYSTEMDATE",OracleType.DateTime)
                                                    }; // Tao moi sanh sach cac Parameters
                                parameters[4].Value = DateTime.Now;
                                ExecuteNonQuery("GW_PK_IBPS_Q_CONVERTIN.IBPS_EN_CONVERTIN", parameters);
                                file.Refresh();
                                // Xu li file thanh cong va Backup file vao thu muc
                                BackupFile(file);
                            }
                        }
                        catch //(Exception ex)
                        {
                            // Thuc hien viec ghi Log khi file bi loi
                            //WriteLogDB(LOG_Error, "Can not read file: " + file.Name + ex.Message,1);
                            continue;        // Neu bi loi thoat khoi vong lap de xu li file khac        
                        }

                    }

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
               // WriteLogDB(LOG_Error, "Read Folder failed: " + folderPath + ex.Message,1);
                throw ex;
            }
            //Thread.CurrentThread.Abort();
        }
        #endregion

       
        // Ham public hien Giai Ma Hoa tu thu vien DLL
        /*---------------------------------------------------------------
        * Method           : MakeDecrypt() 
        * Muc dich         : Tao ra so Ref cho dien IBPS ConvertIn
        * Tra ve           : Mot so Ref kieu int (theo ngay)
        * Ngay tao         : 08/04/2008
        * Nguoi tao        : QuanLD
        *--------------------------------------------------------------*/
        //private string MakeDecrypt(string input)
        //{
        //    string result = "";
        //    Type theType = null;
        //    object theClass = null;
        //    try
        //    {
        //        Assembly a =
        //             Assembly.LoadFrom("SecurityEncrypt.dll");
        //        theClass = a.CreateInstance(DecryptPath);
        //        theType = a.GetType(DecryptPath);
        //        object[] arguments = new object[1] { input };
        //        object retVal =
        //           theType.InvokeMember("Decrypt",
        //           BindingFlags.Default |
        //           BindingFlags.InvokeMethod,
        //           null,
        //           theClass,
        //           arguments);
        //        result = retVal.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Decrypt failed: {0}", ex.Message);
        //        result = null;
        //    }
        //    if (result == null)
        //        return input; // Neu ham ma hoa bi loi thi khong ma hoa
        //    return result;
        //}


        /*---------------------------------------------------------------
         * Method           : getRefNum() 
         * Muc dich         : Tao ra so Ref cho dien IBPS ConvertIn
         * Tra ve           : Mot so Ref kieu int (theo ngay)
         * Ngay tao         : 08/04/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham tao so Ref_Num
        public int getRefNum()
        {
            // Goi ham de lay ngay thang nam theo dinh dang: yyyyMMdd (vi du ngay 08/04/2008: 20080408)
            // Lay mot string theo ngay thang
            string strToday = dateFolderName;

            // String Truy van Oracle de lay ra so ref hien tai ung voi ngay Today
            string strCmd = "SELECT Value AS RefNum FROM SYSVAR WHERE VARNAME='IBPSSeqNum' and GWTYPE='IBPS' and NOTE='" + strToday + "'";

            // Khoi tao so Ref bang 0
            int RefNum = 0;
            try
            {
                DataTable dattbl = new DataTable();
                dattbl = ExcuteDataTable(strCmd);
                //OracleDataReader dataReader = Select_ReturnReader(strCmd);
                if (dattbl.Rows.Count > 0)
                {
                    RefNum = Convert.ToInt32(dattbl.Rows[0]["RefNum"].ToString());
                }
                dattbl.Clear();
                RefNum = RefNum + 1;
                // Update so Ref vao trong bang Constant
                ExecuteNonQuery("UPDATE SYSVAR SET Value='" + RefNum.ToString() + "', NOTE='" + strToday + "' WHERE VARNAME='IBPSSeqNum'");
            }
            catch
            {
                RefNum = 0;
            }
            return RefNum;
        }
        #endregion


        /********************************************************************
         * Method            : string Convert(string strInput)
         * Muc dich          : Chuyen mot String tu Tieng Viet TCVN3 sang Tieng Viet khong dau
         * Tham so           : string strInput
         * Tra ve            : Mot string da duoc loai bo dau Tieng Viet
         * Ngay tao          : 07/04/2008
         * Nguoi tao         : TrungNV3
         * Ngay cap nhat     : 07/12/2008
         * Nguoi cap nhat    : QUANLD
         ********************************************************************/
        #region string ConvertVietnamese(string strInput): Chuyen tieng Viet co dau TCVN3 sang Khong dau
        public string ConvertVietnamese(string strInput)
        {
            StringBuilder strOut = new StringBuilder();
            char cTemp;
            if (string.IsNullOrEmpty(strInput))
                return string.Empty; // Neu input la rong thi return string.Empty

            try
            {
                for (int i = 0; i < strInput.Length; i++)
                {
                    cTemp = strInput[i];
                    if (strInput[i] > 30 && strInput[i] < 127)
                    {
                        strOut.Append(strInput[i]);
                    }
                        
                    //***********************************************************************
                    else if (strInput[i] == 'Í' || strInput[i] == 'Ì' || strInput[i] == 'Ị' //Unicode
                          || strInput[i] == 'Ĩ' || strInput[i] == 'Ỉ')                      //Unicode
                    {
                        strOut.Append('I');
                    }
                    else if (strInput[i] == 'í' || strInput[i] == 'ì' || strInput[i] == 'ị' //Unicode
                          || strInput[i] == 'ĩ' || strInput[i] == 'ỉ' //Unicode
                          || strInput[i] == '×' || strInput[i] == 'Ø' || strInput[i] == 'Þ'  //TCVN
                          || strInput[i] == 'Ü')//TCVN
                    {
                        strOut.Append('i');
                    }
                    else if (strInput[i] == 'Ô' || strInput[i] == 'Ố' || strInput[i] == 'Ồ' //Unicode
                          || strInput[i] == 'Ộ' || strInput[i] == 'Ỗ' || strInput[i] == 'Ổ')//Unicode
                    {
                        strOut.Append('O');
                    }
                    else if (strInput[i] == 'ô' || strInput[i] == 'ố' || strInput[i] == 'ồ' //Unicode
                          || strInput[i] == 'ộ' || strInput[i] == 'ỗ' || strInput[i] == 'ổ')//Unicode
                    {
                        strOut.Append('o');
                    }
                    else if (strInput[i] == 'Ơ' || strInput[i] == 'Ớ' || strInput[i] == 'Ờ' //Unicode
                          || strInput[i] == 'Ợ' || strInput[i] == 'Ỡ' || strInput[i] == 'Ở'//Unicode
                          || strInput[i] == '¤' || strInput[i] == '¥')
                    {
                        strOut.Append('O');
                    }
                    else if (strInput[i] == 'ơ' || strInput[i] == 'ớ' || strInput[i] == 'ờ' //Unicode
                          || strInput[i] == 'ợ' || strInput[i] == 'ỡ' || strInput[i] == 'ở')//Unicode
                    {
                        strOut.Append('o');
                    }
                    else if (strInput[i] == 'Â' || strInput[i] == 'Ầ' || strInput[i] == 'Ấ' //Unicode
                          || strInput[i] == 'Ậ' || strInput[i] == 'Ẫ' || strInput[i] == 'Ẩ'//Unicode
                          || strInput[i] == '¡' || strInput[i] == '¢')//TCVN
                    {
                        strOut.Append('A');
                    }
                    else if (strInput[i] == 'â' || strInput[i] == 'ầ' || strInput[i] == 'ấ' //Unicode
                          || strInput[i] == 'ậ' || strInput[i] == 'ẫ' || strInput[i] == 'ẩ' //Unicode
                          || strInput[i] == 'µ' || strInput[i] == '¶'//TCVN
                          || strInput[i] == '¹' || strInput[i] == '¾'//TCVN
                          || strInput[i] == 'Ç' || strInput[i] == 'Ë' || strInput[i] == '©' //TCVN
                          || strInput[i] == '»' || strInput[i] == '¼' || strInput[i] == 'Æ' //TCVN
                          || strInput[i] == '½' || strInput[i] == '¨')
                    {
                        strOut.Append('a');
                    }
                    else if (strInput[i] == 'Ă' || strInput[i] == 'Ằ' || strInput[i] == 'Ắ' //Unicode
                          || strInput[i] == 'Ặ' || strInput[i] == 'Ẵ' || strInput[i] == 'Ẳ')//Unicode
                    {
                        strOut.Append('A');
                    }
                    else if (strInput[i] == 'ă' || strInput[i] == 'ằ' || strInput[i] == 'ắ' //Unicode
                          || strInput[i] == 'ặ' || strInput[i] == 'ẵ' || strInput[i] == 'ẳ')//Unicode
                    {
                        strOut.Append('a');
                    }
                    else if (strInput[i] == 'Á' || strInput[i] == 'À' || strInput[i] == 'Ạ' //Unicode
                          || strInput[i] == 'Ã' || strInput[i] == 'Ả')//Unicode
                    {
                        strOut.Append('A');
                    }
                    else if (strInput[i] == 'á' || strInput[i] == 'à' || strInput[i] == 'ạ' //Unicode
                          || strInput[i] == 'ã' || strInput[i] == 'ả')//Unicode
                    {
                        strOut.Append('a');
                    }
                    else if (strInput[i] == 'Ú' || strInput[i] == 'Ù' || strInput[i] == 'Ụ' //Unicode
                          || strInput[i] == 'Ũ' || strInput[i] == 'Ủ'//TCVN
                          || strInput[i] == '¦')//TCVN
                    {
                        strOut.Append('U');
                    }
                    else if (strInput[i] == 'ú' || strInput[i] == 'ù' || strInput[i] == 'ụ' //Unicode
                          || strInput[i] == 'ũ' || strInput[i] == 'ủ'//Unicode
                          || strInput[i] == 'ï' || strInput[i] == 'ñ' || strInput[i] == 'ø' //TCVN
                          || strInput[i] == 'ö' || strInput[i] == '÷' || strInput[i] == '­')//TCVN
                    {
                        strOut.Append('u');
                    }
                    else if (strInput[i] == 'Ư' || strInput[i] == 'Ừ' || strInput[i] == 'Ứ'//Unicode
                          || strInput[i] == 'Ự' || strInput[i] == 'Ữ' || strInput[i] == 'Ử')//Unicode
                    {
                        strOut.Append('U');
                    }
                    else if (strInput[i] == 'ư' || strInput[i] == 'ừ' || strInput[i] == 'ứ'//Unicode
                          || strInput[i] == 'ự' || strInput[i] == 'ữ' || strInput[i] == 'ử')//Unicode
                    {
                        strOut.Append('u');
                    }
                    else if (strInput[i] == 'Ó' || strInput[i] == 'Ò' || strInput[i] == 'Ọ' //Unicode
                          || strInput[i] == 'Õ' || strInput[i] == 'Ỏ')//Unicode
                    {
                        strOut.Append('O');
                    }
                    else if (strInput[i] == 'ó' || strInput[i] == 'ò' || strInput[i] == 'ọ' //Unicode
                          || strInput[i] == 'õ' || strInput[i] == 'ỏ' || strInput[i] == '¬'//Unicode
                          || strInput[i] == 'ß' || strInput[i] == 'ä' || strInput[i] == 'æ' //TCVN
                          || strInput[i] == 'ç' || strInput[i] == 'ë'//TCVN
                          || strInput[i] == 'å' || strInput[i] == '«' || strInput[i] == 'î') //TCVN
                    {
                        strOut.Append('o');
                    }
                    else if (strInput[i] == 'É' || strInput[i] == 'È' || strInput[i] == 'Ẹ' //Unicode
                          || strInput[i] == 'Ẽ' || strInput[i] == 'Ẻ'//Unicode
                          || strInput[i] == '£')
                    {
                        strOut.Append('E');
                    }
                    else if (strInput[i] == 'é' || strInput[i] == 'è' || strInput[i] == 'ẹ' //Unicode
                          || strInput[i] == 'ẽ' || strInput[i] == 'ẻ'//Unicode
                          || strInput[i] == 'Î' || strInput[i] == 'Ñ' || strInput[i] == 'Ï'
                          || strInput[i] == 'Ö' || strInput[i] == 'ª')
                    {
                        strOut.Append('e');
                    }
                    else if (strInput[i] == 'Ê' || strInput[i] == 'Ề' || strInput[i] == 'Ế' //Unicode
                          || strInput[i] == 'Ễ' || strInput[i] == 'Ể' || strInput[i] == 'Ệ')//Unicode
                    {
                        strOut.Append('E');
                    }
                    else if (strInput[i] == 'ê' || strInput[i] == 'ề' || strInput[i] == 'ế' //Unicode
                          || strInput[i] == 'ễ' || strInput[i] == 'ể' || strInput[i] == 'ệ')//Unicode
                    {
                        strOut.Append('e');
                    }
                    else if (strInput[i] == 'Y' || strInput[i] == 'Ỳ' || strInput[i] == 'Ý' //Unicode
                          || strInput[i] == 'Ỹ' || strInput[i] == 'Ỷ' || strInput[i] == 'Ỵ')//Unicode
                    {
                        strOut.Append('Y');
                    }
                    else if (strInput[i] == 'y' || strInput[i] == 'ỳ' || strInput[i] == 'ý' //Unicode
                          || strInput[i] == 'ỹ' || strInput[i] == 'ỷ' || strInput[i] == 'ỵ'//Unicode
                          || strInput[i] == 'û' || strInput[i] == 'þ' || strInput[i] == 'ü')
                    {
                        strOut.Append('y');
                    }
                    else if (strInput[i] == 'Đ' //Unicode
                          || strInput[i] == '§')
                    {
                        strOut.Append('D');
                    }
                    else if (strInput[i] == 'đ')//Unicode
                    {
                        strOut.Append('d');
                    }
                    else if (strInput[i] == '\r' || strInput[i] == '\n' || strInput[i] == '\0'
                          || strInput[i] == '\b' || strInput[i] == '\t')
                    {
                        strOut.Append(strInput[i]);
                    }
                    //***********************************************************************
                    else
                    {
                        strOut.Append(strInput[i]);
                    }
                }
            }
            catch
            {
                return String.Empty;
            }
            return strOut.ToString();


        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    Lay DATE - TIME - FolderName, File theo ngay gio hien tai         
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/


        // Lay cac gia tri; Ten Folder, ten File, Ngay-Thang theo thoi gian
        #region Cac gia tri theo thoi gian Date - Time hien tai
        /*----------------------------
         * Lay ten thu muc theo ngay gio hien tai dang NamThangNgay: 20080415
         */
        public static string dateFolderName
        {
            get
            {
                DateTime datetime = DateTime.Now;
                dateFolder = datetime.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
                return dateFolder;
            }
        }

        /*----------------------------
         * Lay dang ten File theo ngay gio hien tai dang: ******dd.MMy
         */
        public static string dateFileFormat
        {
            get
            {
                string strFile;
                DateTime datetime = DateTime.Now;
                strFile = DateTime.Now.Year.ToString().Substring(3);
                dateFile = datetime.ToString("******dd.MM" + strFile, CultureInfo.CreateSpecificCulture("en-US"));
                return dateFile;
            }
        }

        /*----------------------------
         * Lay ngay hien tai theo format: YYYY/MM/dd (2008/04/15)
         */
        public static string DateNow
        {
            get
            {
                DateTime datetime = DateTime.Now;
                dateNow = datetime.ToString("yyyy/MM/dd", CultureInfo.CreateSpecificCulture("en-US"));
                return dateNow;
            }
        }
        /*----------------------------
         * Lay thoi gian ngay gio hien tai theo format: yyyy/MM/dd hh:mm:ss tt(2008/04/15 10:20:25 PM)
         */
        public static string TimeNow
        {
            get
            {
                DateTime datetime = DateTime.Now;
                timeNow = datetime.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CreateSpecificCulture("en-US"));
                return timeNow;
            }
        }
        #endregion


        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    FILE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*---------------------------------------------------------------
         * Method           : getFolderList() 
         * Muc dich         : Lay danh sach Folder tu truong EXPORT_FOLDER cua bang TAD
         * Tham so          : Khong co tham so
         * Tra ve           : List<string> - Danh sach String
         * Ngay tao         : 05/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 09/04/2008
         * Nguoi cap nhat   : QuanLD
         *--------------------------------------------------------------*/
        #region Ham lay danh sach cac Folder IBPSConvertIn tu bang CSDL TAD
        private List<string> getFolderList()
        {
            List<string> FolderList = new List<string>();
            try
            {
                // Lay tat ca cac Folder cua cac chi nhanh co su dung DBLINK

                string strCmd = "SELECT EXPORT_FOLDER From TAD where (TAD.Connection=3 or TAD.Connection=5) and TAD.status=1";
                DataTable datTbl = new DataTable();
                datTbl = ExcuteDataTable(strCmd);
                if (datTbl.Rows.Count > 0)
                {
                    for (int i = 0; i < datTbl.Rows.Count; i++)
                    {
                        string str_temp = datTbl.Rows[i]["EXPORT_FOLDER"].ToString();
                        if (!String.IsNullOrEmpty(str_temp))
                            FolderList.Add(str_temp);
                    }
                }
                else
                {
                    FolderList = null;
                    WriteLogDB(LOG_Error, "Danh sach thu muc EXPORT_FOLDER trong bang TAD bi rong",1);
                    return null;
                }


                //OracleDataReader dtReader = Select_ReturnReader(strCmd);
                //if (dtReader != null && dtReader.HasRows)
                //{
                //    while (dtReader.Read())
                //    {
                //        string str_temp = dtReader["EXPORT_FOLDER"].ToString();
                //        if (!String.IsNullOrEmpty(str_temp))
                //            FolderList.Add(str_temp);
                //    }
                //    dtReader.Close();
                //    dtReader.Dispose();
                //}
                //else
                //{                    
                //    FolderList = null;
                //    WriteLogDB(LOG_Error, "Danh sach thu muc EXPORT_FOLDER trong bang TAD bi rong");
                //    return null;
                //}
            }
            catch (Exception ex)
            {
                //
                WriteLogDB(LOG_Error, "Khong the lay duoc danh sach thu muc EXPORT_FOLDER trong bang TAD" + ex.Message,1);
                //throw ex;
                return null;
            }
            return FolderList;
        }
        #endregion



        /*---------------------------------------------------------------
         * Method           : scanFile(string DirPath) 
         * Muc dich         : Lay mot danh sach file trong thu muc theo dinh dang
         * Tham so          : + DirPath: duong dan cua thu muc
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Doc mot danh sach file tu Folder - ScanFile(string DirPath)
        public List<FileInfo> ScanFile(string DirPath)
        {
            List<FileInfo> list = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(DirPath + "\\" + dateFolderName));
            try
            {
                // Scan file tu thu muc, voi cac file co dinh dang lay 
                // tu dateFileFormat trong lop Constant
                FileInfo[] files = dir.GetFiles(dateFileFormat);
                list.AddRange(files);
            }
            catch
            {
                list = null;
                //new DatabaseProcess().WriteLog(DateTime.Now, Constant.SERVICE_NAME, "Co loi khi scan file trong thu muc " + dir.FullName, Constant.LEVEL_ERROR_SCAN_FILE);
            }
            return list;
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : MoveFile(FileInfo file, string destPath) 
         * Muc dich         : Di chuyen mot file den thu muc khac
         * Tham so          : 
         *                      + FileInfofile (la file can di chuyen)
         *                      + destPath: duong dan cua thu muc chua file chuyen
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region MoveFile(FileInfo file, string destPath)
        public void MoveFile(FileInfo file, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(destPath);
            if (!dir.Exists)  // Neu thu muc chua ton tai thi Create
                dir.Create();
            CheckFileExists(file,destPath, file.Name);
            file.MoveTo(destPath + "\\" + file.Name); // Goi ham di chuyen file den thu muc can chuyen toi
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : CheckFileExists(string destPath) 
         * Muc dich         : Kiem tra xem File co ton tai khong neu co thi Rename di
         * Tham so          : 
         *                      + FileInfofile (la file can di chuyen)
         *                      + destPath: duong dan cua thu muc chua file chuyen
         * Tra ve           : void
         * Ngay tao         : 11/08/2008
         * Nguoi tao        : QuanLD
         * Ngay cap nhat    : 11/08/2008
         * Nguoi cap nhat   : Quanld
         *--------------------------------------------------------------*/
        #region CheckFileExists( string destPath)
        public void CheckFileExists(FileInfo file, string destPath, string strFilename)
        {
            bool isExists=false;
            string strName;
            //FileInfo file = new FileInfo(destPath + "\\" + strFilename);
            strName = strFilename;
            int increate=0;
            int iLen=0;
            iLen=strName.Length;
            while (!isExists)
            {
                if (!File.Exists(destPath + "\\" + strName))
                {
                    isExists = true;
                    file.MoveTo(destPath + "\\" + strName);
                    break;
                }
                else
                { 
                    increate=increate+1;
                    strName = strFilename + "." + increate.ToString();
                }
            }

           

        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : BackupFile 
         * Muc dich         : Backup file sang mot thuc muc Backup
         * Tham so          : FileInfo file: file can Backup
         * Tra ve           : Tra ve kieu void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham Backup mot file - BackupFile(FileInfo file)
        public void BackupFile(FileInfo file)
        {
            try
            {
                // Khoi tao thu muc Backup
                // Constant.BACKUP_FOLDER: la ten thu muc Backup duoc dinh nghia trong lop Const
                string backupFolder = file.Directory.Parent.FullName + "\\" + BACKUP_FOLDER + "\\" + dateFolderName;
                DirectoryInfo DirBackup = new DirectoryInfo(backupFolder);

                if (!DirBackup.Exists)   // Neu thu muc chua ton tai thi Create
                    DirBackup.Create();

                CheckFileExists(file,DirBackup.FullName, file.Name);
                // Goi ham Move file den thu muc Backup
                file.MoveTo(DirBackup.FullName + "\\" + file.Name);
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, "Backup failed" + ex.Message + file.FullName,1);
            }
        }
        #endregion



        /*---------------------------------------------------------------
         * Method           : OpenFile(string pathFie) 
         * Muc dich         : Open mot file Text
         * Tham so          : string pathFie - Duong dan cua file
         * Tra ve           : Tra ve noi dung cua File kieu String
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham Open file Text : OpenFile(string pathFie)
        public string OpenFile(string pathFie)
        {
            string result = String.Empty;
            try
            {
                StreamReader sr = new StreamReader(pathFie, Encoding.Default);
                // Doc tu dau den het file, tra lai kieu String
              
                result = sr.ReadToEnd();
                sr.Close();  // Dong lai stream reader file              

                // Mot cach doc file khac, co the khong dung den
                //FileStream fs = File.OpenRead(pathFie);
                //byte[] data = new byte[fs.Length];
                //fs.Read(data, 0, data.Length);
                //result = Encoding.Default.GetString(data, 0, data.Length);
                //fs.Close();                
            }
            catch
            {
                // Neu viec doc file bi loi thi tra ve mot xau rong StringEmpty
                result = String.Empty;
            }
            return result;
        }
        #endregion



        /*---------------------------------------------------------------
         * Method: WriteLogFile 
         * Muc dich: Viet mot noi dung String vao File
         * Tham so:
         *  - int log_Level
         *  - string DirPath: Thu muc chua file log
         *  - string FileName: Ten file Log
         *  - string content: Noi dung ghi log
         *  Tra ve: True neu write thanh cong, tra ve False neu bi loi
         * Ngay tao: 02/04/2008
         * Nguoi tao: TrungNV3
         *--------------------------------------------------------------*/
        #region Ham GHI LOG ra mot file log
        public bool WriteLogFile(int log_Level, string strContent)
        {
            string DirPath = LOG_FOLDER;
            string FileName = LOG_FILENAME;
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    using (StreamWriter sw = File.AppendText(FullName))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + strContent);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(FullName))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + strContent);
                        sw.Close();

                    }
                }
               // WriteLogDB(log_Level, strContent);
                return true;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }
        #endregion

        /*---------------------------------------------------------------
         * Method           : WriteLogDB(...) 
         * Muc dich         : Thuc hien ghi Service Log vao bang MSG_LOG trong Database Oracle 
         * Tham so          : 
         *                      + string Content: Noi dung Log             
         *                      + int ErrorLevel; Muc do quy uoc cho Log
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nha     : 05/05/2008
         * Nguoi cap nhat   : QuanLD
         *--------------------------------------------------------------*/
        #region void  WriteLogDB(int LogLevel, string Content)
        public void WriteLogDB(int LogLevel, string Content, long lQueryID)
        {
            try
            {
                // Mo ket noi database            
                OracleTransaction oraTransaction;
                OracleConnection oraConnection = OpenConnect();
                oraTransaction = oraConnection.BeginTransaction();
                try
                {                    
                    OracleParameter[] parameters = new OracleParameter[]
                {   new OracleParameter("pLogDate",OracleType.DateTime),
                    new OracleParameter("pQUERY_ID", OracleType.Number,20),
                    new OracleParameter("pSERVICE",OracleType.VarChar,50),
                    new OracleParameter("pDESCRIPTIONS", OracleType.VarChar,2000 ),
                    new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,20),
                    new OracleParameter("pSTATUS",OracleType.Number,2)};

                    parameters[0].Value = DateTime.Now;
                    parameters[1].Value = lQueryID;
                    parameters[2].Value = SERVICE_NAME;
                    parameters[3].Value = Content;
                    parameters[4].Value = "IBPS-SIBS";
                    parameters[5].Value = LogLevel;
                    string strSQL = "insert into IBPS_SVR_LOG (Log_Date,QUERY_ID,STATUS,DESCRIPTIONS,SERVICE,MSG_DIRECTION)"
                                  + " values (:pLogDate,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS,:pSERVICE,:pMSG_DIRECTION)";



                    OracleCommand oraCommand = new OracleCommand(strSQL, oraConnection, oraTransaction);
                    oraCommand.Parameters.AddRange(parameters);
                    oraCommand.CommandType = CommandType.Text;
                    oraCommand.ExecuteNonQuery();
                    oraTransaction.Commit();

                    // Huy ket noi sau khi hoan thanh
                    oraConnection.Dispose();
                    oraCommand.Dispose();

                }
                catch// (Exception ex)
                {
                    // Neu qua trinh Insert bi loi thi RollBack
                    oraTransaction.Rollback();
                }
            }
            catch { }
        }
        #endregion


        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    DATABASE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

         /*---------------------------------------------------------------
         * Method           : void ExecuteNonQuery(...) 
         * Muc dich         : Thuc hien lenh thuc thi Database Oracle
         *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
         * Tham so          : + stirng cmdText: Ten cua StoreProcedure             
         *                    + parameters: Mot danh sach OracleParameter
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method ExecuteNonQuery (string StoreName, OracleParameter[] parameters)
        public void ExecuteNonQuery(string StoreName, OracleParameter[] parameters)
        {
            OracleConnection oraConnection = OpenConnect();

            OracleTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            OracleCommand oraCommand = new OracleCommand(StoreName, oraConnection, oraTransaction);
            oraCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                oraCommand.Parameters.AddRange(parameters);
            try
            {
                oraCommand.ExecuteNonQuery();
                oraTransaction.Commit();
                oraConnection.Dispose();
                oraCommand.Dispose();
                oraTransaction.Dispose();
            }
            catch (Exception ex)
            {
                oraTransaction.Rollback(); // neu xay ra loi thi RollBack
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
            }

        }
        #endregion



        /*---------------------------------------------------------------
         * Method           : void ExecuteNonQuery(...) 
         * Muc dich         : Thuc hien lenh thuc thi Database Oracle 
         *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
         * Tham so          : string cmdText - cau lenh PL/SQL                
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        #region void ExecuteNonQuery(string cmdText)
        public void ExecuteNonQuery(string cmdText)
        {
            // Mo ket noi moi
            OracleConnection oraConnection = OpenConnect();

            // Mo Transction de kiem soat qua trinh thuc hien 
            OracleTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection, oraTransaction);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                oraCommand.ExecuteNonQuery();
                oraTransaction.Commit();

                // Dong ket noi khi hoan thanh
                oraConnection.Dispose();
                oraCommand.Dispose();
                oraTransaction.Dispose();
            }
            catch (Exception ex)
            {
                oraTransaction.Rollback();
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
            }

        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : SelectQueryStore(...) 
         * Muc dich         : Thuc hien truy van Select thi Database Oracle 
         * Tham so          : cmdText kieu string - cau lenh truy van SELECT
         * Tra ve           : DataTable
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 06/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region DataTable SelectQueryCmd(string cmdText)
        public DataTable ExcuteDataTable(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable dtb = new DataTable();
            OracleDataAdapter da;

            // Khoi tao ket noi moi
            OracleConnection oraConnection = OpenConnect();

            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                da = new OracleDataAdapter(oraCommand);
                da.Fill(dtb);

                // Huy ket noi khi Select hoan thanh
                oraConnection.Dispose();
                oraCommand.Dispose();
                da.Dispose();
            }
            catch (Exception ex)
            {
                dtb = null;
                throw new Exception("Database Process have error when call SelectQueryCmd: " + ex.Message);
            }
            return dtb;
        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    KET NOI DATABASE       
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*---------------------------------------------------------------
         * Method           : OpenConnect() 
         * Muc dich         : Tao mot ket noi Connection toi Oracle DB
         * Tham so          : Khong co tham so
         * Tra ve           : Mot OracleConnection da duoc Open
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method OpenConnect() - Code
        public OracleConnection OpenConnect()
        {
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = getConnString();
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                int count = 0;
                // Cho so lan connect toi da la 3 lan
                while (count < 3)
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)

                        return connection;
                    count++;
                }
                
            }
            catch //(Exception ex)
            {
                WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
            }
            return null;
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : getConnString() 
         * Muc dich         : Lay chuoi ket noi Database tu file cau hinh
         * Tham so          : Khong co tham so
         * Tra ve           : Mot string ket noi database
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 04/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method getConnString() - Code
        public string getConnString()
        {
            string strConnect = "";
            try
            {
                string strDatabase = getKeyConfig("DatabaseName");
                // Giai ma Username

                string strUser = getKeyConfig("Username");
                // Giai ma Password
                string strPassword = Decrypt(getKeyConfig("Password"));
                strConnect = "Data Source=" + strDatabase + "; User Id=" + strUser + " ; Password=" + strPassword + " ; Integrated Security=No;";

            }
            catch (Exception ex)
            {
                throw new Exception("Khong the doc va giai ma duoc ConnectionString tu file cau hinh." + ex.Message);
            }
            return strConnect;
        }
        #endregion


        // ham giai ma        
        /********************************************************************
         * Method: public string Decrypt(string toDecrypt)
         * Muc dich: Giai Ma hoa ma mot Strig
         * Tham so: string toDecrypt
         * Tra ve: Mot string da duoc giai ma
         * Ngay tao: 20/03/2008
         * Nguoi tao: TrungNV3
         * Ngay cap nhat: 31/03/2008
         * Nguoi cap nhat: TrungNV3
        ********************************************************************/
        #region string Decrypt(string toDecrypt): Giai ma mot string
        public string Decrypt(string toDecrypt)
        {
            string key = "FPTSecurity2008"; //Encrypt Key 
            string result = "";
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                result = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                throw new Exception("Can't Decode from Input");
                //MsgBox.Show("Can't decode User and Password in Config File", "Message error");
            }
            return result;
        }

        #endregion


        /*---------------------------------------------------------------
         * Method           : getKeyConfig(string key) 
         * Muc dich         : Doc mot value key tu the Tag trong file cau hinh
         * Tham so          : string key
         * Tra ve           : Mot string la Value cua key 
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 04/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method getKeyConfig(string key) - Code
        public string getKeyConfig(string key)
        {
            string result = "";
            // load config document 
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(strConfigFilePath);
            }
            catch (System.IO.FileNotFoundException ex)
            {

                throw new Exception("Khong tim thay File cau hinh cua he thong Service: " + ex.Message);
            }
            // retrieve ServicesConfig node
            XmlNode node = doc.SelectSingleNode("//DatabaseConfig");
            if (node == null)
            {
                throw new InvalidOperationException("Khong tim thay tag DatabaseConfig trong file cau hinh.");
            }
            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem == null)
                {
                    // add value for key
                    throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");
                }
                else
                {
                    // key was not found so create the 'add' element
                    // and set it's key/value attributes
                    result = elem.Attributes.GetNamedItem("value").Value;
                }
            }
            catch
            {
                

            }
            return result;
        }
        #endregion

    
        // End class
    }
}
