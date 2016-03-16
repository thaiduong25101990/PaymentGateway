using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using System.Reflection;
using BR.BRBusinessObject;
using BR.BRSWIFT;
using BR.BRSYSTEM;
using System.Net;
using BR.BRIQS;
using System.Security.Cryptography;
using System.IO;
using BR.BRMONITOR;


namespace BR.BRMONITOR
{
    public partial class frmMain : Form
    {
        #region khai bao bien va cac ham
        private clsLog objLog = new clsLog();
        private MENUController objcontrolmenu = new MENUController();
        private GROUP_MENUController ObjctrolGroup_menu = new GROUP_MENUController();
        private USERSController objCtrlUser = new USERSController();
        private SYSVARController objCtrlSysvar = new SYSVARController();
        private clsHardDrive objHD = new clsHardDrive();
        private UserEncrypt objEncrypt = new UserEncrypt(); 
        
        private int iOK;
        private int iTick;
        private static int iGwtype = 0;        
        //private bool firstStart = true;
        private bool NeedConfirm = true;
        private static bool bSelect = false;
        private string strTime;
        private static string strItem;
        private string strMenuid = "";
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }          
        
        //lay thoi gian cua he thong trong bang Excel
        private void Get_Ontime()
        {
            try
            {
                string strVarname = "LockTime";
                DataTable datSysvar = new DataTable();
                datSysvar = objCtrlSysvar.Get_Ontime(strVarname);
                if (datSysvar.Rows.Count == 0)                
                    Common.Ontime = "5";               
                else
                {
                    Common.Ontime = datSysvar.Rows[0]["VALUE"].ToString();
                    strTime = datSysvar.Rows[0]["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }

        }      
            
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");                
                iOK = 0;
                //goi ham kiem tra khi bat dau dang nhap
                //neu tra ra gia tri la true cho tiep tuc chuong trinh
                #region Tam thoi commen doan check key
                if (objHD.check_setup())
                    iOK = 1;
                //hien thi form de dang ky key_code
                else
                {
                    //frmKeyCode frmkeycode = new frmKeyCode();
                    //frmkeycode.ShowDialog();
                    //if (Common.iCode_key == 1)
                    //    iOK = 1;
                    //else
                    //    iOK = 0;
                }
                //Them doan nay vao
                iOK = 1;
                #endregion
                //neu thoa ma thi tiep tuc
                if (iOK == 1)
                {
                    #region Commen tam thoi
                    BRMONITOR.BRTechnique.Update.CheckConnectServer();
                    #endregion
                    //chay thi mo comment dong nay ra
                    string sHostName = Dns.GetHostName();
                    /*Moi them*/
                    System.Diagnostics.Process[] dg = System.Diagnostics.Process.GetProcesses(sHostName);
                    if (System.Diagnostics.Process.GetProcessesByName("BRMONITOR", sHostName).Length > 1)
                    {
                        Common.iClose_Exit = 1;
                        this.Close();
                    }  
                    /*Het*/


                    System.Net.IPAddress[] a = System.Net.Dns.GetHostAddresses(sHostName);
                    Common.IpLocal = Convert.ToString(a[0]);                    

                    selectdata();
                    GetdataTaskbar();
                    Get_Ontime();
                    this.Text = "Bridge Monitor";
                    Common.bTimer = 0;
                    this.timer1.Enabled = true;
                    this.timer1.Interval = 2000;
                    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);                    
                }
                else                
                    this.Close();                
                
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void GetdataTaskbar()
        {
            try
            {
                DataSet datUU = new DataSet();
                datUU = objCtrlUser.GetUSERSUPDATEPASS(Common.Userid);
                string strUserID = datUU.Tables[0].Rows[0]["USERID"].ToString();
                string str11 = strUserID.Substring(0, 3);
                string str22 = strUserID.Substring(3);
                if (strUserID.ToUpper()=="ADMIN")
                    toolStripStatusLabel4.Text = "User ID: " + strUserID;
                else
                    toolStripStatusLabel4.Text = "User ID: " + str11 + "-" + str22;

                toolStripStatusLabel5.Text = " : " + datUU.Tables[0].Rows[0]["USERNAME"].ToString() + ".";
               // toolStripStatusLabel6.Text = " Channel : " + Common.strGWTYPE;
                toolStripStatusLabel4.ForeColor = Color.Blue;
                toolStripStatusLabel5.ForeColor = Color.Blue;
                toolStripStatusLabel6.ForeColor = Color.Blue;

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void selectdata()
        {
            try
            {
                if (iGwtype == 0)
                {
                    if (Start())
                    {
                        //bat dau dang nhap thi no nhay vao day
                        string strUserID = Common.Userid;
                        //lay thong tin kenh thanh toan ma User do chon vao
                       // string strGwtype = Common.strGWTYPE;
                        string strUserid = Common.Userid;
                        this.ForeColor = Color.Red;                        
                        
                        DataSet m_datDS = new DataSet("Menu");
                        m_datDS = objcontrolmenu.GetMENU1(strUserid);
                        //cho to het ma hinh
                        this.WindowState = FormWindowState.Maximized;                                                
                        AddMenuItem(mnuMain, m_datDS, "0000");
                    }
                    else this.Close();
                    //firstStart = false;
                }
                else if (iGwtype == 1)
                {
                    //khi chon lai kenh thah toan thi nhay vao day                    
                    string strUserID = Common.Userid;                    
                    mnuMain.Items.Clear();
                    //lay thong tin kenh thanh toan ma User do chon vao
                    string strUserid = Common.Userid;

                    //this.Text = strGwtype + "  Monitor";
                    this.ForeColor = Color.Red;
                    
                    DataSet m_datDS1 = new DataSet("Menu");
                    m_datDS1 = objcontrolmenu.GetMENU1(strUserid);                    
                    //cho to het ma hinh
                    this.WindowState = FormWindowState.Maximized;                    
                    AddMenuItem(mnuMain, m_datDS1, "0000");
                 }
                //khi Log chuong trinh
                //khi goi for logchuong trinh
                else if (iGwtype == 2)
                {
                    // van vao user do
                    if (Common.iLogin == 10)
                    {
                    }
                    //log vao user khac
                    else if (Common.iLogin == 11)
                    {

                        //neu user do duoc vao mot kenh thanh toan
                        if (Common.CountGWtype == 1 || Common.CountGWtype == 0)
                        {
                            mnuMain.Items.Clear();
                            //lấy thông tin kênh thanh toán mà User đó chọn vào
                            this.ForeColor = Color.Red;
                            string strUserid = Common.Userid;
                            DataSet m_datDS1 = new DataSet("Menu");
                            m_datDS1 = objcontrolmenu.GetMENU1(strUserid);
                            //cho to het ma hinh
                            this.WindowState = FormWindowState.Maximized;                            
                            AddMenuItem(mnuMain, m_datDS1, "0000");
                            
                        }
                        if (Common.CountGWtype == 2)
                        {
                            string strUserID = Common.Userid;
                            mnuMain.Items.Clear();                            
                            //lấy thông tin kênh thanh toán mà User đó chọn vào
                            this.ForeColor = Color.Red;
                            string strUserid = Common.Userid;
                            Common.iSelectType = 5;
                            //frmUserL.ShowDialog();
                            if (Common.iCloseT == 5)                           
                                this.Close();                            
                           
                            DataSet m_datDS1 = new DataSet("Menu");
                            m_datDS1 = objcontrolmenu.GetMENU1(strUserid);
                            //cho to het ma hinh
                            this.WindowState = FormWindowState.Maximized;                            
                            AddMenuItem(mnuMain, m_datDS1, "0000");
                            toolStripStatusLabel3.Text = " Channel : " + Common.strGWTYPE;
                            
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }     
        }
        
        private bool Start()
        {

            bool result = false;
            try
            {
                this.Hide();
                frmWelcome frmwelcome = new frmWelcome();                
                frmwelcome.Show();
                frmLogin frmlogin = new frmLogin();
                frmlogin.ShowDialog();
                if (frmlogin.DialogResult == DialogResult.OK)
                {
                    //neu user do thuoc nhom chi duoc phep vao mot kenh thanh toan
                    if (Common.CountGWtype == 1)                    
                        result = true;                    
                    if (Common.CountGWtype == 0)                    
                        result = true;                    
                    if (Common.CountGWtype > 1)
                    {
                        result = true;
                      
                    }
                    this.Show();
                }
                if (frmlogin.DialogResult == DialogResult.Cancel)
                {
                    result = false;
                    bSelect = true;
                }
                frmwelcome.Close();

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            return result;
        }
                
        public void OnTimerEvent(object source, EventArgs e)
        {
            //Call form log in sau khoang thgian n (phut),
            //n = LockTime trong sysvar
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DynamicMenuItem item = (DynamicMenuItem)sender;
                string strMenu = item.MenuID;
                strMenuid = item.MenuID;
                Common.gGWTYPE = item.GWType;
                ExecMethod(item);
                DataSet datMenu = new DataSet();
                datMenu = objcontrolmenu.GetMenu_MenuID(strMenu);
                string strAssembly = datMenu.Tables[0].Rows[0]["ASSEMBLY"].ToString();
                //cat chuoi
                String[] M = strAssembly.Split(new String[] { "." }, StringSplitOptions.None);
                int r = M.Count<String>();
                if (r == 3)
                {
                    string strDM = M[2];
                    if (strDM.Trim() == "frmSelectGWType")
                    {
                        iGwtype = 1;
                        selectdata();
                    }                    
                }
                GetdataTaskbar();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void GetMenuid()
        {
            try
            {
                DataSet datMenus = new DataSet();
                datMenus = objcontrolmenu.GetMenuid(strMenuid);
                Common.strMenuid = strMenuid;
                //Common.strGWTYPE = datMenus.Tables[0].Rows[0]["GWTYPE"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        // *****************************************************
        // Muc dich:   Thuc hien excute Tren Menu
        // Dau vao:     item
        // DynamicMenuItem: Lop dinhj nghia menu dong, 
        // Tra ve:  
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //******************************************************
        private void ExecMethod(DynamicMenuItem item)
        {
            try
            {

                if (item.AssemblyName == "THIS")                
                    ExecMDIMethod(item);                
                else if ((item.AssemblyName != string.Empty) && 
                    ((item.AssemblyMethodName == "ShowDialog") || 
                    (item.AssemblyMethodName == "Show")))
                {
                    Assembly assembly = Assembly.LoadFrom(Application.StartupPath + "\\" + 
                        item.AssemblyFile);
                    Type type = assembly.GetType(item.AssemblyName);
                    string strMNU = item.MenuID;
                    strItem = Convert.ToString(item);
                    if (type.BaseType.Name == "frmBasedata")
                    {
                        GetMenuid();
                        //gionh thuong
                        if (strMNU == "2084")                        
                            Common.strSTATUS_IN = "Swift";
                        //Import tham so
                        else if (strMNU == "2085")                        
                            Common.strSTATUS_IN = "Paramester";                        
                        if (strMNU == "7895")//7895                        
                            Common.strSTATUS_IN = "Resend";                        
                        else if (strMNU == "7896")                        
                            Common.strSTATUS_IN = "Manual";                        
                        else if (strMNU == "2084")                        
                            BR.BRLib.Common.strExcel = strMNU;                        
                        else if (strMNU == "2027")                       
                            Common.strTTSP_Resend = "Resend";                        
                        else if (strMNU == "2022")                        
                            Common.strTTSP_Resend = "Views";                       
                        else                        
                            BR.BRLib.Common.strExcel = "Quynd";                  
                        
                        object instance = (frmBasedata)Activator.CreateInstance(type);
                        DynamicMenuInvokeInfo dynInfo = new DynamicMenuInvokeInfo();                        
                        dynInfo.OprionData = item.OptionData;
                        //lay Ten cua menu de lay ra menuid
                        strItem = Convert.ToString(item);
                        GetMenuid();
                        type.InvokeMember(item.AssemblyMethodName, 
                            System.Reflection.BindingFlags.InvokeMethod, 
                            null, instance, new object[] { item.Text, dynInfo });

                    }
                    else
                    {
                        Form instance = (Form)Activator.CreateInstance(type);
                        DynamicMenuInvokeInfo dynInfo = new DynamicMenuInvokeInfo();
                        strItem = Convert.ToString(item);
                        string Name = Convert.ToString(item.AssemblyName);
                        GetMenuid();
                        //Cat chuoi
                        String[] K = Name.Split(new String[] { "." }, StringSplitOptions.None);
                        int r = K.Count<String>();
                        if (K[2].Trim() == "frmLogin")
                        {
                            Common.strUserID1 = Common.Userid;
                            Common.iLogout = 5;
                        }
                        if (Common.strMenuid == "7895")//7895                        
                            Common.strSTATUS_IN = "Resend";
                      
                        else if (Common.strMenuid == "7896")                        
                            Common.strSTATUS_IN = "Manual";                        
                        else if (strMNU == "2027")                        
                            Common.strTTSP_Resend = "Resend";                        
                        else if (strMNU == "2022")                        
                            Common.strTTSP_Resend = "Views";                       
                        if (item.AssemblyMethodName == "ShowDialog")
                        {
                            instance.ShowDialog();
                            if (Common.iClose == 5)                            
                                this.Close();                            
                        }
                        if (r == 3)
                        {
                            string strDM = K[2];
                            if (strDM.Trim() == "frmLogin")
                            {
                                iGwtype = 2;
                                selectdata();
                            }
                        }
                        else                        
                            instance.Show();                       

                    }                    
                }
                else if (item.AssemblyName != string.Empty)
                {
                    if (item.AssemblyMethodName == "")                                        
                        Common.ShowError("Under Construction", 1, MessageBoxButtons.OK);                    
                    else
                    {
                        Assembly assembly = Assembly.LoadFrom(Application.StartupPath + 
                                            "\\" + item.AssemblyFile);

                        Type type = assembly.GetType(item.AssemblyName);
                        object instance = Activator.CreateInstance(type);
                        type.InvokeMember(item.AssemblyMethodName, 
                            System.Reflection.BindingFlags.InvokeMethod, null, instance, null);
                    }
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void ExecMDIMethod(DynamicMenuItem item)
        {
            try
            {
                if (item.AssemblyMethodName == "EXIT")                
                    this.Close();                
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        //form main close
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (bSelect == false)
                {
                    if (Common.iClose_Exit == 1)
                    {
                        e.Cancel = false;
                        objCtrlUser.UPDATE_LOGSTS(Common.Userid, "O");
                    }
                    else
                    {
                        if (Common.iClose == 5 || Common.iCloseT == 5)
                        {
                        }
                        else
                        {
                            if (NeedConfirm == true)
                                e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit", Common.sCaption);
                            //thoat khoi chuong trinh
                            if (e.Cancel == false)
                                objCtrlUser.UPDATE_LOGSTS(Common.Userid, "O");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        
        
        private void timer1_Tick(object sender, EventArgs e)
        {            
            try
            {
                if (Common.bTimer == 0)                
                    Timer_Log();                
                else if (Common.bTimer == 1)
                {
                    iTick = 0;
                    Common.Ontimer1 = 0;
                    Common.bTimer = 0;
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void Timer_Log()
        {
            try
            {
                iTick = iTick + 1;
                Common.Ontimer1 = iTick;                
                Log_Timer();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void Log_Timer()
        {
            try
            {
                if (Convert.ToInt32(strTime.Trim()) != 0)               
                {
                    //thoi gian bat dau bang thoi gian Log may thi Log chuong trinh
                    if (Common.Ontimer1 >= (Convert.ToInt32(strTime.Trim()) * 30))
                     {
                        Common.bTimer = 1;
                        this.timer1.Stop();
                        //-----------goi gorm Login----------------
                        Common.strUserID1 = Common.Userid;
                        Common.iLogout = 5;
                        iGwtype = 2;
                        //---------------------------------------------                        
                        frmMain frmm = new frmMain();
                        frmm.WindowState= FormWindowState.Minimized;
                        //Form1 frmLog = new Form1();
                        //ghi thoi gian bat dau look chuong trinh
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "";
                        int iLoglevel = 1;
                        string strWorked = "Login";
                        string strTable = "USERS";
                        string strOld_value = "";
                        string strNew_value = "";
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, 
                            iLoglevel, strWorked, strTable, strOld_value, strNew_value);                        
                        //frmLog.ShowDialog();
                        if (Common.iClose_Exit == 1)                        
                            this.Close();                                            
                        this.timer1.Start();
                    }
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }
               
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void mnuMain_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void mnuMain_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }       
        
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            Common.bTimer = 1;
        }        
        
        private void OnReconcile(object sender, EventArgs e)
        {
            BR.BRIBPS.frmIBPSRecManagement fRec = new BR.BRIBPS.frmIBPSRecManagement();
            fRec.Show();
        }         
             
        private void btnShowReportPara_Click(object sender, EventArgs e)
        {   
            frmReportPara fdf = new frmReportPara();
            fdf.ShowDialog();            
        }
        
        private void AddMenuItem(MenuStrip mnu, DataSet datDS, string Level)
        {
            try
            {
                DataView dv = new DataView(datDS.Tables[0], "parentid = " + Level.ToString(), 
                    "ordermenu", DataViewRowState.CurrentRows);
                foreach (DataRowView row in dv)
                {
                    DynamicMenuItem menuItem = new DynamicMenuItem(row["CAPTION"].ToString(),
                        null, new EventHandler(this.MenuItem_Click));
                    menuItem.AssemblyName = row["ASSEMBLY"].ToString();

                    menuItem.MenuID = row["MENUID"].ToString();
                    if (row["ASSEMBLY"] != null)
                        menuItem.AssemblyName = row["ASSEMBLY"].ToString();
                    if (row["ASSEMBLYFILE"] != null)
                        menuItem.AssemblyFile = row["ASSEMBLYFILE"].ToString();
                    if (row["METHOD"] != null)
                        menuItem.AssemblyMethodName = row["METHOD"].ToString();                    
                    menuItem.OptionData = row["OPTIONDATA"].ToString();
                    
                    menuItem.GWType = row["GWTYPE"].ToString();

                    string strGwtype = row["GWTYPE"].ToString();
                    //Neu menu nay thuoc GW.SYSTEM
                    if (strGwtype.Trim() == "GWSYSTEM")
                    {
                        DataSet datMenus = new DataSet();
                        datMenus = ObjctrolGroup_menu.CheckEnable_Menu(Common.Userid,
                            menuItem.MenuID, "GWSYSTEM");
                        //khong co group nao duoc vao menu nay
                        if (datMenus.Tables[0].Rows.Count == 0)
                        {
                            if (menuItem.MenuID.Trim() == "1001" ||
                                menuItem.MenuID.Trim() == "5000" || menuItem.MenuID.Trim() == "1000" ||
                                menuItem.MenuID.Trim() == "1003" || menuItem.MenuID.Trim() == "5001")
                            {
                            }
                            else                            
                                menuItem.Enabled = false;                            
                        }
                        else                        
                            menuItem.Enabled = true;                        
                    }
                    //Neu menu nay ko thuoc GW.SYSTEM
                    else
                    {
                        //if (strGwtype.Trim() == Common.strGWTYPE.Trim())
                        //{
                            DataSet datGG = new DataSet();
                            datGG = ObjctrolGroup_menu.CheckEnable_Menu(Common.Userid, 
                                menuItem.MenuID, Common.strGWTYPE.Trim());
                            //group ma user truc thuoc co quyen vao menu
                            if (datGG.Tables[0].Rows.Count == 0)
                            {
                                if (menuItem.MenuID.Trim() == "1001" ||
                                    menuItem.MenuID.Trim() == "5000" || menuItem.MenuID.Trim() == "1000" ||
                                    menuItem.MenuID.Trim() == "1003" || menuItem.MenuID.Trim() == "5001")
                                {
                                }
                                else                                
                                    menuItem.Enabled = false;                               
                            }
                            else                            
                                menuItem.Enabled = true;                            
                        //}
                    }                                        
                    DataSet datGroup_menu = new DataSet();                    
                    mnu.Items.Add(menuItem);
                    AddSubMenuItem(menuItem, datDS, menuItem.MenuID.ToString());
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void AddSubMenuItem(DynamicMenuItem mnu, DataSet datDS, string Level)
        {
            try
            {
                DataView dv = new DataView(datDS.Tables[0], "parentid = " + Level.ToString(), 
                    "ordermenu", DataViewRowState.CurrentRows);

                foreach (DataRowView row in dv)
                {
                    if (row["CAPTION"].ToString() != "-")
                    {
                        DynamicMenuItem item;
                        if ((row["CTRL"].ToString() == "0") && (row["ALT"].ToString() == "0") &&
                            (row["KEY"].ToString().Trim() == string.Empty))
                            item = new DynamicMenuItem(row["CAPTION"].ToString(), null,
                                new EventHandler(this.MenuItem_Click));
                        else
                        {
                            item = new DynamicMenuItem(row["CAPTION"].ToString(), null,
                                new EventHandler(this.MenuItem_Click),
                                Common.getMultiKey(row["CTRL"].ToString(), row["ALT"].ToString(),
                                row["KEY"].ToString().Trim()));
                        }
                        item.MenuID = row["MENUID"].ToString();
                        if (row["ASSEMBLY"] != null)
                            item.AssemblyName = row["ASSEMBLY"].ToString();
                        if (row["ASSEMBLYFILE"] != null)
                            item.AssemblyFile = row["ASSEMBLYFILE"].ToString();
                        if (row["METHOD"] != null)
                            item.AssemblyMethodName = row["METHOD"].ToString();
                        item.Enabled = (row["ENABLE"].ToString().Equals("1") ? true : false);
                        item.ToolTipText = row["TOOLTIPTEXT"].ToString();
                        item.OptionData = row["OPTIONDATA"].ToString();
                        item.GWType = row["GWTYPE"].ToString();
                        string strGwtype1 = row["GWTYPE"].ToString();
                        
                        //Neu menu nay thuoc GW.SYSTEM
                        //----------------------------------
                        if (strGwtype1.Trim() == "GWSYSTEM")
                        {
                            DataSet datMenus = new DataSet();
                            datMenus = ObjctrolGroup_menu.CheckEnable_Menu(Common.Userid, 
                                item.MenuID, "GWSYSTEM");
                            //khong co group nao duoc vao menu nay
                            if (datMenus.Tables[0].Rows.Count == 0)
                            {
                                if (item.MenuID.Trim() == "1001" ||
                                    item.MenuID.Trim() == "5000" || item.MenuID.Trim() == "1000" ||
                                    item.MenuID.Trim() == "1003" || item.MenuID.Trim() == "5001")
                                {
                                }
                                else                                
                                    item.Enabled = false;                                
                            }
                            else                            
                                item.Enabled = true;                            
                        }
                        //Neu menu nay ko thuoc GW.SYSTEM
                        else
                        {
                            //if (strGwtype1.Trim() == Common.strGWTYPE.Trim())
                            //{
                                DataSet datGG = new DataSet();
                                datGG = ObjctrolGroup_menu.CheckEnable_Menu(Common.Userid, 
                                    item.MenuID, "");
                                //group ma user truc thuoc co quyen vao menu
                                if (datGG.Tables[0].Rows.Count == 0)
                                {
                                    if (item.MenuID.Trim() == "1001" ||
                                        item.MenuID.Trim() == "5000" || item.MenuID.Trim() == "1000" ||
                                        item.MenuID.Trim() == "1003" || item.MenuID.Trim() == "5001")
                                    {
                                    }
                                    else                                    
                                        item.Enabled = false;                                   
                                }
                                else                                
                                    item.Enabled = true;                                
                            //}
                        }                        
                        mnu.DropDownItems.Add(item);                        
                        AddSubMenuItem(item, datDS, item.MenuID.ToString());
                    }
                    else
                    {
                        ToolStripSeparator item = new ToolStripSeparator();
                        mnu.DropDownItems.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

       
    }
        
    
}
