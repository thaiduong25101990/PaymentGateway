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
//' Create date:	27/05/2008
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class USER_MSG_LOGController
    {
        public int AddUSER_MSG_LOG(USER_MSG_LOGInfo objTable)
        {
            return USER_MSG_LOGDP.Instance().AddUSER_MSG_LOG(objTable);
        }
        public int AddUSER_MSG_LOG1(USER_MSG_LOGInfo objTable)
        {
            return USER_MSG_LOGDP.Instance().AddUSER_MSG_LOG1(objTable);
        }
        public DataSet GetUser_msg_log(string pUserid, DateTime pDateFrom, DateTime pDateTo, string pContent)
        {
            return USER_MSG_LOGDP.Instance().GetUser_msg_log(pUserid, pDateFrom, pDateTo, pContent);
        }
        public DataSet GetUser_msg_log(DateTime pDateFrom, DateTime pDateTo,string pContent)
        {
            return USER_MSG_LOGDP.Instance().GetUser_msg_log(pDateFrom, pDateTo, pContent);
        }
        public DataSet GetUser_log()
        {
            return USER_MSG_LOGDP.Instance().GetUser_log();
        }
        public DataSet GetViewLogInfo(string pContent)
        {
            return USER_MSG_LOGDP.Instance().GetViewLogInfo(pContent);
        }
        public DataTable History_User_Log(string pTable)
        {
            return USER_MSG_LOGDP.Instance().History_User_Log(pTable);
        }
    }
}
