using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRBusinessObject;
using System.Threading;

namespace BR.BRSYSTEM
{
    public partial class frmBasedata : Form
    {
        public static int iInquiry;
        public static bool iSupdate = false;
        public static bool iSadd = false;
        public DynamicMenuInvokeInfo dynInfo = new DynamicMenuInvokeInfo();

        private GROUP_MENUInfo objMenu = new GROUP_MENUInfo();
        private GROUP_MENUController objctrolMenu = new GROUP_MENUController();
        private SYSVARInfo ObjSysvar = new SYSVARInfo();
        private SYSVARController ObjctrlSys = new SYSVARController();
                
        private int bTimer;
        private int iTick;
        private static int iDelete;
        private static int iAdd;
        private static int iEdit;

        //protected string strTable;
        
                
        public DialogResult ShowDialog(string Caption, DynamicMenuInvokeInfo dynInfo)
        {
            base.Text = Caption;
            this.dynInfo = dynInfo;
            return base.ShowDialog(this.Parent);
        }

        public void Show(string Caption, DynamicMenuInvokeInfo dynInfo)
        {
            base.Text = Caption;
            this.dynInfo = dynInfo;
            this.Show();
        }

        public frmBasedata()
        {
            InitializeComponent();
        }
         
        private void CustomizeForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }
        

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string Msg = "Do you want to Delete ?";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)                
                    Common.iSconfirm = 1;//thuc hien xoa                
                else                
                    Common.iSconfirm = 0;//khong xoa                             
            }
            catch
            {

            }            
        }

        private void KeyDown_Time()
        {
            try
            {                
                Common.ShowError("anh da dong", 1, MessageBoxButtons.OK);
            }
            catch(Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmBasedata_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(KeyDown_Time));
            bTimer = 0;            
            cmdSave.Enabled = false;
        }

        //ham kiem tra thong tin dau vao cua mot User
        //de kiem tra cac chuc nang cua User duoc quyen su dung
        private void Checkuserlogin()
        {
            try
            {
                string strUserID = Common.Userid;
                string strMenuid = Common.strMenuid;
                DataSet datMenu = new DataSet();
                datMenu = objctrolMenu.CheckUserlogin(strUserID, strMenuid);
                string strInquiry = datMenu.Tables[0].Rows[0]["ISINQUIRY"].ToString();
                string strDelete = datMenu.Tables[0].Rows[0]["ISDELETE"].ToString();
                string strInsert = datMenu.Tables[0].Rows[0]["ISINSERT"].ToString();
                string strEdit = datMenu.Tables[0].Rows[0]["ISEDIT"].ToString();
                iInquiry = Convert.ToInt32(strInquiry);

                if (strInquiry == "1")
                {
                    if (strDelete == "1")
                    {
                        cmdDelete.Enabled = true;
                        iDelete = 1;
                    }
                    else
                    {
                        cmdDelete.Enabled = false;
                        iDelete = 0;
                    }
                    if (strInsert == "1")
                    {
                        cmdAdd.Enabled = true;
                        iAdd = 1;
                    }
                    else
                    {
                        cmdAdd.Enabled = false;
                        iAdd = 0;
                    }
                    if (strEdit == "1")
                    {
                        cmdEdit.Enabled = true;
                        iEdit = 1;
                    }
                    else
                    {
                        cmdEdit.Enabled = false;
                        iEdit = 0;
                    }
                    Common.iSelect = 1;
                }
                else
                {
                    Common.iSelect = 0;
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdAdd.Enabled = false;
                }
            }
            catch
            {
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            iSupdate = false;
            iSadd = true;
            cmdEdit.Enabled = false;
            cmdDelete.Enabled = false;
            cmdSave.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Msg = "Do you want to save data?";
                string title = Common.sCaption;

                if (iSupdate == true)
                {                    
                    DialogResult DlgResult = new DialogResult();
                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DlgResult == DialogResult.Yes)
                        Common.iSconfirm = 1;
                    else                    
                        Common.iSconfirm = 0;            
                }
                if (iSadd == true)
                {                    
                    DialogResult DlgResult = new DialogResult();
                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DlgResult == DialogResult.Yes)
                        Common.iSconfirm = 1;                    
                    else                    
                        Common.iSconfirm = 0;
                }
                if (iDelete == 1)
                {
                    cmdDelete.Enabled = true;                  
                }
                else
                {
                    cmdDelete.Enabled = false;                   
                }
                if (iAdd == 1)
                {
                    cmdAdd.Enabled = true;                   
                }
                else
                {
                    cmdAdd.Enabled = false;                  
                }
                if (iEdit == 1)
                {
                    cmdEdit.Enabled = true;
                }
                else
                {
                    cmdEdit.Enabled = false;
                }
            }
            catch
            { 
            }            
            cmdSave.Enabled = false;            
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            iSupdate = true;
            iSadd = false;
            cmdAdd.Enabled = false;
            cmdDelete.Enabled = false;
            cmdSave.Enabled = true;
            cmdEdit.Enabled = false;
        }

        private void frmBasedata_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                bTimer = 1;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmBasedata_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                bTimer = 1;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            //dinh thoi gian
            try
            {
                if (bTimer == 0)
                {
                    Timer_Log();
                }
                else if (bTimer == 1)
                {
                    iTick = 0;
                    Common.Ontimer1 = 0;
                    bTimer = 0;
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

        private void frmBasedata_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void Log_Timer()
        {
            try
            {
                
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message,2,MessageBoxButtons.OK);
            }
        }
        
    }
}
