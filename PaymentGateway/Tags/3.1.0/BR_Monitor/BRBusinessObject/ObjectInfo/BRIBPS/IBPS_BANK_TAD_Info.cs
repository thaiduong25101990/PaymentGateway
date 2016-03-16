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
namespace BR.BRBusinessObject
{
    public class IBPS_BANK_TADInfo
    {

        private double _BANK_MAP_ID;
        private string _GW_BANK_CODE;
        private string _TAD_ID;
        private int _MAIN;
        private int _TAD;


        public IBPS_BANK_TADInfo()
        {

        }

        public double BANK_MAP_ID
        {
            get
            {
                return _BANK_MAP_ID;
            }
            set
            {
                _BANK_MAP_ID = value;
            }
        }

        public string GW_BANK_CODE
        {
            get
            {
                return _GW_BANK_CODE;
            }
            set
            {
                _GW_BANK_CODE = value;
            }
        }

        public string TAD_ID
        {
            get
            {
                return _TAD_ID;
            }
            set
            {
                _TAD_ID = value;
            }
        }

        public int MAIN
        {
            get
            {
                return _MAIN;
            }
            set
            {
                _MAIN = value;
            }
        }

        public int TAD
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


    }


}

