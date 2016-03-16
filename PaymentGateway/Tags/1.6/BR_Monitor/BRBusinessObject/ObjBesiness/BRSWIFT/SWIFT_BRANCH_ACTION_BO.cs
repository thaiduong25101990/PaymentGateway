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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_BRANCH_ACTIONController
    {

        public int AddSWIFT_BRANCH_ACTION(SWIFT_BRANCH_ACTION_Info objTable)
        {
            return SWIFT_BRANCH_ACTIONDP.Instance().AddSWIFT_BRANCH_ACTION(objTable);
        }

        public int UpdateSWIFT_BRANCH_ACTION(SWIFT_BRANCH_ACTION_Info objTable)
        {
            return SWIFT_BRANCH_ACTIONDP.Instance().UpdateSWIFT_BRANCH_ACTION(objTable);
        }

        public int DeleteSWIFT_BRANCH_ACTION(int intID)
        {
            return SWIFT_BRANCH_ACTIONDP.Instance().DeleteSWIFT_BRANCH_ACTION(intID);
        }

        public DataSet GetSWIFT_BRANCH_ACTION()
        {
            return SWIFT_BRANCH_ACTIONDP.Instance().GetSWIFT_BRANCH_ACTION();
        }

        public int ValidateSWIFT_BRANCH_ACTION(string strSql)
        {
            return SWIFT_BRANCH_ACTIONDP.Instance().ValidateSWIFT_BRANCH_ACTION(strSql);
        }
        public bool IDIsExisting(string strCriteriaName, string strPriority, string strBranch)
        {
            return SWIFT_BRANCH_ACTIONDP.Instance().IDIsExisting(strCriteriaName,strPriority, strBranch);
        }

        //Muc dich: Ham check Keyword, Message
        //Nguoi tao: Huypq
        //Ngay tao: 01/08/2008
        public bool CheckKeyword(string strKeyword, string strMessage, 
            out string strError, out int iCheck)
        {            
            return SWIFT_BRANCH_ACTIONDP.Instance().CheckKeyword(strKeyword,
                strMessage, out strError, out iCheck);
        }

    }
}
