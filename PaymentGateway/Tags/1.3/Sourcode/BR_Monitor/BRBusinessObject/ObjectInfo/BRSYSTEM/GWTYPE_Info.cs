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


//' Template: InfoClass.xslt 17/10/2006
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GWTYPEInfo
	{

        private int _GWTYPEID;
		private string _GWTYPE;
		private int _ENCRYPT;
        private int _CONNECTION;
        //private string _ENCRYPTFUNCTION;
        //private string _DECRYPTFUNCTION;
        private int _GWTYPESTS;
        //private int _FILETIME;
        private int _MSG_NO;
        //private string _FOLDER;
        private string _DESCRIPTION;
        private string _DBLINK;
        //--------------------------------
		public GWTYPEInfo()
		{
			
		}

        public int GWTYPEID
		{
			get
			{
                return _GWTYPEID;
			}
			set
			{
                _GWTYPEID = value;
			}
		}
		
		public string GWTYPE
		{
			get
			{
				return _GWTYPE;
			}
			set
			{
				_GWTYPE = value;
			}
		}
		
		public int ENCRYPT
		{
			get
			{
				return _ENCRYPT;
			}
			set
			{
				_ENCRYPT = value;
			}
		}

        public int CONNECTION
        {
            get
            {
                return _CONNECTION;
            }
            set
            {
                _CONNECTION = value;
            }
        }

        
        public int GWTYPESTS
        {
            get
            {
                return _GWTYPESTS;
            }
            set
            {
                _GWTYPESTS = value;
            }
        }

        public int MSG_NO
        {
            get
            {
                return _MSG_NO;
            }
            set
            {
                _MSG_NO = value;
            }
        }
      
        public string DESCRIPTION
        {
            get
            {
                return _DESCRIPTION;
            }
            set
            {
                _DESCRIPTION = value;
            }
        }//_DBLINK
        public string DBLINK
        {
            get
            {
                return _DBLINK;
            }
            set
            {
                _DBLINK = value;
            }
        }
	}
	
	
}
