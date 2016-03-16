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
//' Template: BusinessObject.xslt 17/10/2006
//' Author:	Nguyen duc quy
//' Create date:	06/006/2008 21:40
//' Description:
//' Revise History:
//' =============================================

namespace BR.BRBusinessObject
{
    public class VCB_PARAMETERSController
    {
        public int Add(VCB_PARAMETERSInfo objTable)
        {
            return VCB_PARAMETERSDP.Instance().Add(objTable);
        }
        public int Update(VCB_PARAMETERSInfo objTable)
        {
            return VCB_PARAMETERSDP.Instance().Update(objTable);
        }
        public int Delete(string pID)
        {
            return VCB_PARAMETERSDP.Instance().Delete(pID);
        }
        public DataTable Search()
        {
            return VCB_PARAMETERSDP.Instance().Search();
        }
        public DataTable check(string Depart,string Channel,string Msg_type,string Bank_code)
        {
            return VCB_PARAMETERSDP.Instance().check(Depart, Channel, Msg_type, Bank_code);
        }
    }
}
