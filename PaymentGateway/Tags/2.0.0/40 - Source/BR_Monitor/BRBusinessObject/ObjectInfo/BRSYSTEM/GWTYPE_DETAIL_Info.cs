using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
   public class GWTYPE_DETAILInfo
    {
       private int    _ID;
       private string _GWTYPE;
       private int    _FLDTYPE;
       private int    _DIRECTION;
       private string _FOLDER;
       private string _FTPPATH;
       private string _FTPUSER;
       private string _FTPPASS;
       private string _FILETYPE;
       private string _DESCRIPTION;
       private string _FTPSERVER;

        public GWTYPE_DETAILInfo()
		{
			
		}

        public int ID
		{
			get
			{
                return _ID;
			}
			set
			{
                _ID = value;
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

        public int DIRECTION
		{
			get
			{
                return _DIRECTION;
			}
			set
			{
                _DIRECTION = value;
			}
		}

        public int FLDTYPE
        {
            get
            {
                return _FLDTYPE;
            }
            set
            {
                _FLDTYPE = value;
            }
        }

        public string FOLDER
		{
			get
			{
                return _FOLDER;
			}
			set
			{
                _FOLDER = value;
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
		}

        public string FTPPATH
        {
            get
            {
                return _FTPPATH;
            }
            set
            {
                _FTPPATH = value;
            }
        }

        public string FTPUSER
        {
            get
            {
                return _FTPUSER;
            }
            set
            {
                _FTPUSER = value;
            }
        }

        public string FTPPASS
        {
            get
            {
                return _FTPPASS;
            }
            set
            {
                _FTPPASS = value;
            }
        }
        
        public string FILETYPE
        {
            get
            {
                return _FILETYPE;
            }
            set
            {
                _FILETYPE = value;
            }
        }

        public string FTPSERVER
        {
            get
            {
                return _FTPSERVER;
            }
            set
            {
                _FTPSERVER = value;
            }
        
        }
    }
}
