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
//' Author:	Nguyen duc quy
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class IBPS_TRANS_MAPInfo
    {
        private int _TRANS_MAP_ID;
        private string _SIBS_TRANS_CODE;
        private string _GW_TRANS_CODE;
        private int _HV_LV;
        private string _DESCRIPTION;
        public IBPS_TRANS_MAPInfo()
        {

        }
        public int TRANS_MAP_ID
        {
            get
            {
                return _TRANS_MAP_ID;
            }
            set
            {
                _TRANS_MAP_ID = value;
            }
        }
        public string SIBS_TRANS_CODE
        {
            get
            {
                return _SIBS_TRANS_CODE;
            }
            set
            {
                _SIBS_TRANS_CODE = value;
            }
        }
        public string GW_TRANS_CODE
        {
            get
            {
                return _GW_TRANS_CODE;
            }
            set
            {
                _GW_TRANS_CODE = value;
            }
        }
        public int HV_LV
        {
            get
            {
                return _HV_LV;
            }
            set
            {
                _HV_LV = value;
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
    }
}
