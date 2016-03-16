using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace BR.BRLib
{
    public class DynamicMenu 
    {
        
        
        //hien thi thong bao khong cho dong chuong trinh
        public static bool ConfirmBox(string Msg, string title)
        {
            DialogResult DlgResult = new DialogResult();
            DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (DlgResult == DialogResult.OK)
                return true;
            else return false;
        }
        //hien thi thong bao hoi co muon dong chuong trinh hay khong
        public static bool ConfirmBox_Box(string Msg, string title)
        {
            DialogResult DlgResult = new DialogResult();
            DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DlgResult == DialogResult.No)
                return true;
            else return false;
        }
        //hien thi thong bao hoi co muon dong chuong trinh hay khong
        public static bool ConfirmBox_Save(string Msg, string title)
        {
            DialogResult DlgResult = new DialogResult();
            DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DlgResult == DialogResult.No)
                return true;
            else return false;
        } 
	
    }

    //internal
    public class DynamicMenuItem : ToolStripMenuItem
    {
        public string GWType = string.Empty;     
        public string MenuID = string.Empty;
        public string MenuCaption = string.Empty;
        public string AssemblyName = string.Empty;
        public string AssemblyFile = string.Empty;
        public string AssemblyMethodName = string.Empty;
        public string OptionData = string.Empty;
        

        public DynamicMenuItem()
            : base()
        {
        }
        public DynamicMenuItem(string text)
            : base(text)
        {
        }
        public DynamicMenuItem(string text, Image image)
            : base(text, image)
        {
        }
        public DynamicMenuItem(string text, Image image, EventHandler onClick)
            : base(text, image, onClick)
        {
        }
        public DynamicMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys)
            : base(text, image, onClick, shortcutKeys)
        {
        }
        public DynamicMenuItem(string text, Image image, params ToolStripItem[] dropDownItems)
            : base(text, image, dropDownItems)
        {
        }
    }    

    public class DynamicMenuInvokeInfo
    {
        public static class StateAttribute
        {
            public const int ADD = 0;
            public const int VIEW = 1;
            public const int MODIFY = 2;
            public const int DEL = 3;
        }
        public string OprionData = string.Empty;
        public class SearchCommonInnerObject
        {
            public string PrimaryKey = string.Empty;
            public string PrimaryKeyValue = string.Empty;
            public string AssemblyName = string.Empty;
            public string AssemblyFile = string.Empty;
            public string AssemblyMethodName = string.Empty;
            public int State = 0;
        }
        public readonly SearchCommonInnerObject SearchCommonInnerInfo = new SearchCommonInnerObject();
    }

  

    public static class MainInfo
    {
        public static class FormInfo
        {
            public static string Caption = string.Empty;
            public static bool FormDirty = false;
        }
        public static class UserInfo
        {
            public static Int64 ID = -1;
            public static string UserID = string.Empty;
            public static string Group = string.Empty;
            public static string FullName = string.Empty;
            public static string State = string.Empty;
            public static string Password = string.Empty;
            

            public static DateTime BeginDate;
            public static DateTime ExpireDate;
            public static string Logged = string.Empty;
        }
        public static class ProgramInfo
        {
            public static string Caption = string.Empty;
            public static string ProgramLicense = string.Empty;
            public static string TranDate = string.Empty;
        }
        public static class OwnerInfo
        {
            public static string CompID = string.Empty;
            public static string CompName = string.Empty;
            public static string CompAdd = string.Empty;
            public static string CompTel = string.Empty;
            public static string CompFax = string.Empty;
        }
    }
}


