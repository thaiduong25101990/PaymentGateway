using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	12/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class EXCELInfo
    {
        private int _MSG_ID; 
    private string _F1; 
    private string _F2; 
    private string _F3; 
    private string _F4;
    private string _F5; 
    private string _F6; 
    private string _F7; 
    private string _F8; 
    private string _F9; 
    private string _F10; 
    private string _F11;
    private string _F12; 
    private string _F13; 
    private string _F14; 
    private string _F15; 
    private string _F16; 
    private string _F17; 
    private string _FILE_NAME; 
    private DateTime _TRANS_DATE; 
    private int _STATUS; 
    private string _GWTYPE; 
    private string _MSG_DIRECTION;
    private string _TYPE;
    private string _TELLER_ID;
    private string _OFFICER_ID;

    
    public EXCELInfo() 
    { 
        
    } 
    
    public int MSG_ID { 
        get { return _MSG_ID; } 
        set { _MSG_ID = value; } 
    } 
    
    public string F1 { 
        get { return _F1; } 
        set { _F1 = value; } 
    } 
    
    public string F2 { 
        get { return _F2; } 
        set { _F2 = value; } 
    } 
    
    public string F3 { 
        get { return _F3; } 
        set { _F3 = value; } 
    } 
    
    public string F4 { 
        get { return _F4; } 
        set { _F4 = value; } 
    } 
    
    public string F5 { 
        get { return _F5; } 
        set { _F5 = value; } 
    } 
    
    public string F6 { 
        get { return _F6; } 
        set { _F6 = value; } 
    } 
    
    public string F7 { 
        get { return _F7; } 
        set { _F7 = value; } 
    } 
    
    public string F8 { 
        get { return _F8; } 
        set { _F8 = value; } 
    } 
    
    public string F9 { 
        get { return _F9; } 
        set { _F9 = value; } 
    } 
    
    public string F10 { 
        get { return _F10; } 
        set { _F10 = value; } 
    } 
    
    public string F11 { 
        get { return _F11; } 
        set { _F11 = value; } 
    } 
    
    public string F12 { 
        get { return _F12; } 
        set { _F12 = value; } 
    } 
    
    public string F13 { 
        get { return _F13; } 
        set { _F13 = value; } 
    } 
    
    public string F14 { 
        get { return _F14; } 
        set { _F14 = value; } 
    } 
    
    public string F15 { 
        get { return _F15; } 
        set { _F15 = value; } 
    } 
    
    public string F16 { 
        get { return _F16; } 
        set { _F16 = value; } 
    } 
    
    public string F17 { 
        get { return _F17; } 
        set { _F17 = value; } 
    } 
    
    public string FILE_NAME { 
        get { return _FILE_NAME; } 
        set { _FILE_NAME = value; } 
    } 
    
    public DateTime TRANS_DATE { 
        get { return _TRANS_DATE; } 
        set { _TRANS_DATE = value; } 
    } 
    
    public int STATUS { 
        get { return _STATUS; } 
        set { _STATUS = value; } 
    } 
    
    public string GWTYPE { 
        get { return _GWTYPE; } 
        set { _GWTYPE = value; } 
    } 
    
    public string MSG_DIRECTION { 
        get { return _MSG_DIRECTION; } 
        set { _MSG_DIRECTION = value; } 
    }
    public string TYPE
    {
        get { return _TYPE; }
        set { _TYPE = value; }
    } // private string _TELLER_ID;
    //private string _OFFICER_ID;
    public string TELLER_ID
    {
        get { return _TELLER_ID; }
        set { _TELLER_ID = value; }
    }

    public string OFFICER_ID
    {
        get { return _OFFICER_ID; }
        set { _OFFICER_ID = value; }
    }
    }
}
