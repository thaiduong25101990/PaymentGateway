using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class RPTPARAMETER_Info
    {
        private long _ID;           //ID
        private string _RPTNAME;    //Ten bao cao
        private string _CRLNAME;    //Kieu control
        private string _CRLTYPE;    //Kieu du lieu
        private int _CRLLENGH;      //Max length cua control la textbox
        private string _CAPTION;    //Ten nhan con trol
        private string _SQL;        //Chuoi lenh SQL
        private int _LSTORD;        //Thu tu
        private int _OPTALL;        //Co chon all

        public RPTPARAMETER_Info()
        {

        }

        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string RPTNAME
        {
            get { return _RPTNAME; }
            set { _RPTNAME = value; }
        }

        public string CRLNAME
        {
            get { return _CRLNAME; }
            set { _CRLNAME = value; }
        }

        public string CRLTYPE
        {
            get { return _CRLTYPE; }
            set { _CRLTYPE = value; }
        }

        public int CRLLENGH
        {
            get { return _CRLLENGH; }
            set { _CRLLENGH = value; }
        }

        public string CAPTION
        {
            get { return _CAPTION; }
            set { _CAPTION = value; }
        }

        public string SQL
        {
            get { return _SQL; }
            set { _SQL = value; }
        }

        public int LSTORD
        {
            get { return _LSTORD; }
            set { _LSTORD = value; }
        }

        public int OPTALL
        {
            get { return _OPTALL; }
            set { _OPTALL = value; }
        }
        
    }
}
