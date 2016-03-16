
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom;
using System.Drawing;
using System.Security.Cryptography;
using System.Collections;
using System.Data;

namespace BR.BRLib
{
   
        public static class Common
        {
            ///////////////////////////////////////////-/
            // Khai bao hang so caption tren MessageBox            
            public static string sCaption = "FPT.BRIDGE";
            /////////////////////////////////////////////

            // Khai bao bien SWMSTS dung cho cac form: frmSwiftMsgManual, frmSwiftMsgManualDup, frmSwiftMsgManualInfo                        
            /*Hai hang so de tao du lieu bang ke*/
            public const string PROCESSSTS_MN = "99";
            public const string PROCESSSTS_RS = "88";
            
            /*Trang thai dien PROCESSSTS*/
            public const string PROCESSSTS_AUTO = "0";
            public const string PROCESSSTS_ACK = "1";
            public const string PROCESSSTS_NACK = "2";
            public const string PROCESSSTS_ACKWAIT = "3";
            public const string PROCESSSTS_FAILED = "4";
            public const string PROCESSSTS_CLOSED = "5";
            public const string PROCESSSTS_WAITING = "6";
            public const string PROCESSSTS_WAPPROVING = "7";
            public const string PROCESSSTS_WAPPROVING_RESENT = "8";
            public const string PROCESSSTS_APPROVED = "9";
            public const string PROCESSSTS_REPAIR = "10";
            public const string PROCESSSTS_OLD_ACKWAIT = "11";
            /*Trang thai SWMSTS*/
            public const string SWMSTS_NORMAL = "1";
            public const string SWMSTS_POSS_DUP = "2";
            public const string SWMSTS_OLDKEY_FAILURE = "3";
            /*Trang thai STATUS*/
            public const string STATUS_CONVERTED = "0";
            public const string STATUS_ERROR = "-1";
            public const string STATUS_SENT = "1";
            public const string STATUS_PENDING = "2";            
            //----------------------------------------------
            public static string Form_Status;
            public static string IpLocal;
            public static int Ontimer1;
            public static int bTimer;
            public const string FILEWAIT = "0";
            public const string ACK = "1";
            public const string NACK = "2";
            public const string ACKWAIT = "3";
            public const string FAILED = "4";
            public const string CLOSED = "5";
            public const string NPROCESS = "6";
            public const string WAITING = "7";
            public const string WAPPROVING = "8";
            public const string SIBSWAIT = "9";
            public const string WAPPROVING_RESENT = "10";
            public static int iCode_key;
            public static string strExcel;
            //Bien luu gia tri kenh thanh toan nguoi truy cap vao lua chon
            public static int iLogout=1;
            public static int iLogin=1;
            public static int iClose = 1;
            public static int iCloseT = 1;
            public static int iSelectType = 1;
            public static bool bTrue;
            public static string strUserID1;
            public static string strGWTYPE ="";//kenh thanh toan
            public static string gGWTYPE = "";//kenh thanh toan
            public static string strUsername;//userdang nhap
            public static string Userid;            
            public static int Groupid;
            public static int CountGWtype;//so kenh thanh toan
            public static string strGroupname;
            public static ArrayList Arygroupname = new ArrayList();
            public static int iID;
            public static string strSTATUS_IN;
            public static string strDatabase;
            public static string strOracleServer;
            public static string strUserDB;
            public static string strPassword;
            public static string strMenuid;            
            public static int iSelect;
            public static string strQuery_id;
            public static int iSconfirm;
            public static string strPasswordUser;
            public static string strApprove;
            public static string strTTSP_Resend;
            public static int iClose_Log;
            public static int iClose_Exit;
            public static int Export_excel;
            public static string Schema;
            //Hang so luu format so tien
            public const string FORMAT_CURRENCY_VND = "{0:#,###}";
            public const string FORMAT_CURRENCY_NT = "{0:#,###.##}";
            public const string FORMAT_CURRENCY = "{0:###,###,###,###.00}";//bien truyen format cho moi loai tien
            public const string FORMAT_CURRENCY_DATAGRID = "###,###,###,###,###.00";
            public const string NUMBER_FORMAT = "{0:#,###.##}";
            public const string File_key = "ispress.ini";
            public const string FilePath = "C:\\WINDOWS\\system32";
            //Hang so phuc vu phan doi chieu
             
            public const string GW_TYPE = "IBPS";
            public const string GW_IBPS_INTERFACE_TYPE = "IBPS";
            public const string GW_SWIFT_INTERFACE_TYPE = "SWIFT";
            public const string GW_IBS_INTERFACE_TYPE = "IBS";
            public const string GW_IBPS_TYPE = "IBPS-SIBS";
            public const string GW_SIBS_TYPE = "SIBS-IBPS";
            public const string GW_SIBS_SYSTEM = "SIBS";
            public const string GW_GW_SYSTEM = "FPT";
            public const string GW_IBPS_SYSTEM = "IBPS";
            public const string GW_SIBS_REC_TYPE = "SIBS";
            public const string GW_IBPS_REC_TYPE = "IBPS";
            public const int GW_CONNECT_FAILURE = -4;
            public const int GW_FILE_NOT_EXIST = -15;
            public const int GW_READ_FILE_FAILURE = -16;
            public const int GW_READ_FILE_SUCCESS = 0;
            public const int GW_RECONCILE_FAILURE = -17;
            public const int GW_RECONCILE_SUCCESS = 0;
            public const int GW_WRITE_FILE_FAILURE = -18;
            public const int GW_INVALID_FILE_FORMAT = -10;
            public const int GW_WRITE_FILE_SUCCESS = 0;
            public const string GW_RECONCILE_LOG_DIR = "Log";
            public const string GW_RECONCILE_ERROR_DIR = "Error";
            public const string GW_RECONCILE_FILE_TIME = "FILETIME";

            public const string GW_USERS_STATUS_ACTIVE = "1";
            public const string GW_USERS_STATUS_PENDING = "2";//User phai change pass khi dang nhap
            public const string GW_USERS_STATUS_LOCKED = "3";

            public static string pUser_Addmin;
            public static string strFullName;

            public const string GW_BKTBL_BKTIME_EOD = "EOD";
            public const string GW_BKTBL_BKTIME_EOM = "EOM";
            public const string GW_BKTBL_BKTIME_EOY = "EOY";

            public const string GW_BKTBL_BKTYPE_DUMP = "1";//Back up ra file .dump
            public const string GW_BKTBL_BKTYPE_DB = "2";//back up tu bang ngay -> bang thang, tu bang thang -> bang nam

            public const string GW_CHANNEL_IBPS = "IBPS";
            public const string GW_CHANNEL_IQS = "IQS";
            public const string GW_CHANNEL_SWIFT = "SWIFT";
            public const string GW_CHANNEL_SYSTEM = "SYSTEM";
            public const string GW_CHANNEL_TTSP = "TTSP";
            public const string GW_CHANNEL_VCB = "VCB";

            public const string GW_SYSVAR_IBPS_MSG_CONTENT = "ISIBPS_BAK";
            public const string GW_IBPS_MSG_CONTENT = "IBPS_MSG_CONTENT";

            public const string GW_SYSVAR_IBPS_MSGDTL = "ISIBPS_DTL_BAK";
            public const string GW_IBPS_MSGDTL = "IBPS_MSGDTL";

            public const string GW_SYSVAR_SWIFT_MSG_CONTENT = "ISSWIFT_BAK";
            public const string GW_SWIFT_MSG_CONTENT = "SWIFT_MSG_CONTENT";

            public const string GW_SYSVAR_SWIFT_MSGDTL = "ISSWIFT_DTL_BAK";
            public const string GW_SWIFT_MSGDTL = "SWIFT_MSGDTL";

            public const string GW_SYSVAR_TTSP_MSG_CONTENT = "ISTTSP_BAK";
            public const string GW_TTSP_MSG_CONTENT = "TTSP_MSG_CONTENT";

            public const string GW_SYSVAR_TTSP_MSGDTL = "ISTTSP_DTL_BAK";
            public const string GW_TTSP_MSGDTL = "TTSP_MSGDTL";

            public const string GW_SYSVAR_VCB_MSG_CONTENT = "ISVCB_BAK";
            public const string GW_VCB_MSG_CONTENT = "VCB_MSG_CONTENT";

            public const string GW_SYSVAR_VCB_MSGDTL = "ISVCB_DTL_BAK";
            public const string GW_VCB_MSGDTL = "VCB_MSGDTL";

            public const string GW_SYSVAR_IQS_MSG_CONTENT = "ISIQS_BAK";
            public const string GW_IQS_MSG_CONTENT = "IQS_MSG_CONTENT";

            public const string GW_SYSVAR_BACKUP_STATUS_OK = "0";// Chua/Da backup
            public const string GW_SYSVAR_BACKUP_STATUS_PENDING = "1";//Dang back up
            public static string Ontime;
            public static int iCancel;
            public static int iOk;
            public const string GW_BRANCH_BRAN_TYPE_TTSP = "2"; //BRAN_TYPE= 2:  Ngan hang tham gia TTSP trong bang Branch


            public static object[] RequestObject(string AssemblyName, string AssemblyMethodName, object[] Params)
            {
                object[] obj = new object[3];
                obj[0] = AssemblyName;
                obj[1] = AssemblyMethodName;
                obj[2] = Params;
                return obj;
            }


            public static DataGridViewCellStyle GetCelltypeNumber(string strFormat)
            {
                DataGridViewCellStyle ReturnTypeNum = new DataGridViewCellStyle();
                ReturnTypeNum.Alignment = DataGridViewContentAlignment.MiddleRight;
                ReturnTypeNum.Format = strFormat;
                return ReturnTypeNum;
            }
            public static DataGridViewCellStyle GetCell_Center()
            {
                DataGridViewCellStyle ReturnTypeNum = new DataGridViewCellStyle();
                ReturnTypeNum.Alignment = DataGridViewContentAlignment.MiddleCenter;                
                return ReturnTypeNum;
            }
            



            public static Keys getMultiKey(string CTRL, string ALT, string KEY)
            {
                if (CTRL == "0")
                {
                    if (ALT == "0")
                    {
                        return (Keys)(getSingleKey(KEY));
                    }
                    else
                    {
                        return (Keys)(Keys.Alt | getSingleKey(KEY));
                    }
                }
                else
                {
                    if (ALT == "0")
                    {
                        return (Keys)(Keys.Control | getSingleKey(KEY));
                    }
                    else
                    {
                        return (Keys)(Keys.Control | Keys.Alt | getSingleKey(KEY));
                    }
                }               
            }
            private static Keys getSingleKey(string KEY)
            {

                switch (KEY)
                {
                    case "A":
                        return Keys.A;
                    case "B":
                        return Keys.B ;
                    case "C":
                        return Keys.C;
                    case "D":
                        return Keys.D;
                    case "E":
                        return Keys.E;
                    case "F":
                        return Keys.F;
                    case "G":
                        return Keys.G;
                    case "H":
                        return Keys.H;
                    case "I":
                        return Keys.I;
                    case "J":
                        return Keys.J;
                    case "K":
                        return Keys.K;
                    case "L":
                        return Keys.L;
                    case "M":
                        return Keys.M;
                    case "N":
                        return Keys.N;
                    case "O":
                        return Keys.O;
                    case "P":
                        return Keys.P;
                    case "Q":
                        return Keys.Q;
                    case "R":
                        return Keys.R;
                    case "S":
                        return Keys.S;
                    case "T":
                        return Keys.T;
                    case "U":
                        return Keys.U;
                    case "V":
                        return Keys.V;
                    case "W":
                        return Keys.W;
                    case "X":
                        return Keys.X;                                        
                    case "Y":
                        return Keys.Y;
                    case "Z":
                        return Keys.Z;
                    case "F1":
                        return Keys.F1;
                    case "F2":
                        return Keys.F2;
                    case "F3":
                        return Keys.F3;
                    case "F4":
                        return Keys.F4;
                    case "F5":
                        return Keys.F5;
                    case "F6":
                        return Keys.F6;
                    case "F7":
                        return Keys.F7;
                    case "F8":
                        return Keys.F8;
                    case "F9":
                        return Keys.F9;
                    case "F10":
                        return Keys.F10;
                    case "F11":
                        return Keys.F11;
                    case "F12":
                        return Keys.F12;
                    default:
                        return Keys.Scroll;
                }
            }
            public static string Encrypt(string cleanString)
            {
                Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
                Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
                return BitConverter.ToString(hashedBytes);
            }

            ///*ham load Icon cua chuong trinh len*/
            //public static 

            /////////////////////////////////////////////////////////-/
            // Mo ta:       Ham kiem tra chuoi ky tu la so?
            // Tham so:     s: Chuoi so can kiem tra
            // Tra ve:      True: sucessfull, False: not successfull
            // Ngay tao:    01/2009
            // Nguoi tao:   
            ///////////////////////////////////////////////////////////
            public static bool IsNumeric(string s)
            {
                try
                {
                    Int32.Parse(s);
                }
                catch
                {
                    return false;
                }
                return true;
            }

            /////////////////////////////////////////////////////////-/
            // Mo ta:       Ham kiem tra chuoi ky tu la so
            // Tham so:     s: Chuoi so can kiem tra
            //              bPure:  True: Kiem tra chuoi so la so nguyen?
            //                      False: La so thap phan
            // Tra ve:      True: sucessfull, False: not successfull
            // Ngay tao:    01/2009
            // Nguoi tao:   
            ///////////////////////////////////////////////////////////
            public static bool IsNumeric(string s, bool bPure)
            {
                try
                {
                    if (bPure == true)
                        Int32.Parse(s);
                    else
                        double.Parse(s);
                }
                catch
                {
                    return false;
                }
                return true;
            }

            public static string FormatNumber(string Number)
            {
                try
                {
                    if (double.Parse(Number) == 0)
                        return "0";
                    else
                        return string.Format(NUMBER_FORMAT, double.Parse(Number));
                }
                catch
                {
                    return "#VALUE!";
                }
            }

            public static string FormatCurrency(string Number,string strFormat)
            {
                try
                {
                    if (double.Parse(Number) == 0)
                        return "0";
                    else
                        return string.Format(strFormat, double.Parse(Number));
                }
                catch
                {
                    return "#VALUE!";
                }
            }

            public static string FormatDate(string Date, string Format)
            {
                try
                {
                    return DateTime.ParseExact(Date, "dd/MM/yyyy", null).ToString(Format);
                }
                catch
                {
                    return "#VALUE!";
                }
            }

            public static string FormatDate(string Date, string FromFormat, string ToFormat)
            {
                try
                {
                    return DateTime.ParseExact(Date, FromFormat, null).ToString(ToFormat);
                }
                catch
                {
                    return "#VALUE!";
                }
            }

            public class DGVColumnHeader : DataGridViewColumnHeaderCell
            {
                private Rectangle CheckBoxRegion;
                private bool checkAll = false;

                protected override void Paint(Graphics graphics,
                    Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                    DataGridViewElementStates dataGridViewElementState,
                    object value, object formattedValue, string errorText,
                    DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
                    DataGridViewPaintParts paintParts)
                {

                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                        formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                    graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);

                    CheckBoxRegion = new Rectangle(
                        cellBounds.Location.X + 1,
                        cellBounds.Location.Y + 2,
                        25, cellBounds.Size.Height - 4);


                    if (this.checkAll)
                        ControlPaint.DrawCheckBox(graphics, CheckBoxRegion, ButtonState.Checked);
                    else
                        ControlPaint.DrawCheckBox(graphics, CheckBoxRegion, ButtonState.Normal);

                    Rectangle normalRegion =
                        new Rectangle(
                        cellBounds.Location.X + 1 + 25,
                        cellBounds.Location.Y,
                        cellBounds.Size.Width - 26,
                        cellBounds.Size.Height);

                    graphics.DrawString(value.ToString(), cellStyle.Font, new SolidBrush(cellStyle.ForeColor), normalRegion);
                }

                protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
                {
                    //Convert the CheckBoxRegion 
                    Rectangle rec = new Rectangle(new Point(0, 0), this.CheckBoxRegion.Size);
                    this.checkAll = !this.checkAll;
                    if (rec.Contains(e.Location))
                    {
                        this.DataGridView.Invalidate();
                    }
                    base.OnMouseClick(e);
                }

                public bool CheckAll
                {
                    get { return this.checkAll; }
                    set { this.checkAll = value; }
                }
            }
            

            public static void DisableControl(Control parent)
            {
                if (parent.HasChildren)
                {
                    for (int i = 0; i < parent.Controls.Count; i++)
                    {
                        DisableControl(parent.Controls[i]);
                    }
                }
                else
                {
                    if (parent.GetType() == typeof(TextBox))
                    {
                        TextBox ctr = (TextBox)parent;
                        ctr.BackColor = Color.White;
                        ctr.ReadOnly = true;
                    }
                    else if (parent.GetType() == typeof(RadioButton))
                    {
                        RadioButton ctr = (RadioButton)parent;
                        ctr.AutoCheck = false;
                    }
                    else if (parent.GetType() == typeof(CheckBox))
                    {
                        CheckBox ctr = (CheckBox)parent;
                        ctr.AutoCheck = false;
                    }
                    else if (parent.GetType() == typeof(ComboBox))
                    {
                        ComboBox ctr = (ComboBox)parent;
                        ctr.DropDownStyle = ComboBoxStyle.DropDownList;
                        ctr.BackColor = Color.White;
                        ctr.Enabled = false;
                    }
                    else if (parent.GetType() == typeof(MaskedTextBox))
                    {
                        MaskedTextBox ctr = (MaskedTextBox)parent;
                        ctr.BackColor = Color.White;
                        ctr.ReadOnly = true;
                    }
                    else if (parent.GetType() == typeof(DateTimePicker))
                    {
                        DateTimePicker ctr = (DateTimePicker)parent;
                        ctr.Enabled = false;
                        //ctr.BackColor = Color.Red;
                    }
                    else if (parent.GetType() == typeof(Button) && parent.Text != "Close" && parent.Text != "&Close")
                    {
                        parent.Enabled = false;
                    }
                }
            }
            /* Reset cac control tren 1 form 
             * BichNN
             * 12/08/2008
             */
            public static void ClearControl(Control parent)
            {
                if (parent.HasChildren)
                {
                    for (int i = 0; i < parent.Controls.Count; i++)
                    {
                        ClearControl(parent.Controls[i]);
                    }
                }
                else
                {
                    if (parent.GetType() == typeof(TextBox))
                    {
                        TextBox ctr = (TextBox)parent;
                        ctr.Text = "";
                    }
                    else if (parent.GetType() == typeof(RadioButton))
                    {
                        RadioButton ctr = (RadioButton)parent;
                        ctr.Checked = false;
                    }
                    else if (parent.GetType() == typeof(CheckBox))
                    {
                        CheckBox ctr = (CheckBox)parent;
                        ctr.Checked = false;
                    }
                    else if (parent.GetType() == typeof(ComboBox))
                    {
                        ComboBox ctr = (ComboBox)parent;
                        ctr.DropDownStyle = ComboBoxStyle.DropDownList;
                        ctr.BackColor = Color.White;
                        //ctr.Enabled = false;
                    }
                    else if (parent.GetType() == typeof(MaskedTextBox))
                    {
                        MaskedTextBox ctr = (MaskedTextBox)parent;
                        ctr.Text = "";
                    }
                    else if (parent.GetType() == typeof(DateTimePicker))
                    {
                        DateTimePicker ctr = (DateTimePicker)parent;
                        //ctr.Enabled = false;
                        //ctr.BackColor = Color.Red;
                    }                 
                }
            }
            

            /////////////////////////////////////////////////////////-/
            // Mo ta:       Hien thi thong bao
            // Tham so:     strErr: Error or Message
            // Tra ve:      Show sucessfull
            // Ngay tao:    01/2009
            // Nguoi tao:   Huypq7
            ///////////////////////////////////////////////////////////
            public static void ShowError(string sError,int iType, MessageBoxButtons msgBox)
            {
                //Information
                if (iType == 1)
                    MessageBox.Show(sError, sCaption, msgBox,MessageBoxIcon.Information);
                //Error
                else if (iType == 2)
                    MessageBox.Show(sError, sCaption, msgBox,MessageBoxIcon.Error);
                //Warning
                else if (iType == 3)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.Warning);
                //Exclamation
                else if (iType == 4)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.Exclamation);
                //Question
                else if (iType == 5)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.Question);
                //Stop
                else if (iType == 6)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.Stop);
                //Asterisk
                else if (iType == 7)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.Asterisk);
                //Hand
                else if (iType == 8)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.Hand);
                //None
                else if (iType == 9)
                    MessageBox.Show(sError, sCaption, msgBox, MessageBoxIcon.None);
            }


            /////////////////////////////////////////////////////////-/
            // Mo ta:       Ham xu ly SQL Injection 
            // Tham so:     sContent: Noi dung can xy ly 
            // Tra ve:      Show sucessfull
            // Ngay tao:    01/2009
            // Nguoi tao:   Huypq7
            ///////////////////////////////////////////////////////////
            public static string ConvertString(string sContent)
            {
                if (string.IsNullOrEmpty(sContent))
                    return "";
                //Ky tu "'"
                return sContent.Replace("'", "''");
                //Ky tu 
            }
           
        }
    
}

