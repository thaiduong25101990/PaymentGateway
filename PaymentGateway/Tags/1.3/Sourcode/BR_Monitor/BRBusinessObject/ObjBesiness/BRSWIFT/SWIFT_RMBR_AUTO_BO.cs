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
    public class SWIFT_RMBR_AUTOController
    {
        public int ADDSWIFT_RMBR_AUTO(SWIFT_RMBR_AUTOInfo objTable)
        {
            return SWIFT_RMBR_AUTODP.Instance().ADDSWIFT_RMBR_AUTO(objTable);
        }
        public DataTable Check_RMBr(string pORG_BRAN, string pRECEIVER_BRAN)
        {
            return SWIFT_RMBR_AUTODP.Instance().Check_RMBr(pORG_BRAN, pRECEIVER_BRAN);
        }

        public DataTable MAP_AUTO(string pORG_BRAN)
        {
            return SWIFT_RMBR_AUTODP.Instance().MAP_AUTO(pORG_BRAN);
        }

        public DataTable Get_Rmbr()
        {
            return SWIFT_RMBR_AUTODP.Instance().Get_Rmbr();
        }
        public DataTable Search(string pOrg_bran)
        {
            return SWIFT_RMBR_AUTODP.Instance().Search(pOrg_bran);
        }
        public int Delete(string pORG_BRANOLD, string pRECEIVER_BRANOLD)
        {
            return SWIFT_RMBR_AUTODP.Instance().Delete(pORG_BRANOLD, pRECEIVER_BRANOLD);
        }
        public int Update_Brauto(string pORG_BRANOLD, string pRECEIVER_BRANOLD, string pORG_BRAN, string pRECEIVER_BRAN)
        {
            return SWIFT_RMBR_AUTODP.Instance().Update_Brauto(pORG_BRANOLD, pRECEIVER_BRANOLD, pORG_BRAN, pRECEIVER_BRAN);
        }
    }
}
