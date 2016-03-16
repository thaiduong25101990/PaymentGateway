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


namespace BR.BRBusinessObject
{
    public class TADMAPInfo
    {
        private int _ID;
        private string _TAD;
        private string _TADHO;
        private string _NOTE;

        public TADMAPInfo()
		{			
		}

        public string NOTE
        {
            get
            {
                return _NOTE;
            }
            set
            {
                _NOTE = value;
            }
        }

        public string TADHO
        {
            get
            {
                return _TADHO;
            }
            set
            {
                _TADHO = value;
            }
        }

        public string TAD
        {
            get
            {
                return _TAD;
            }
            set
            {
                _TAD = value;
            }
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
    }
}
