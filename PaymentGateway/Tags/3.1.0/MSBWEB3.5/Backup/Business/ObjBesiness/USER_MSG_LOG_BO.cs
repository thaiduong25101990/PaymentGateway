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


namespace BIDVWEB.Business
{
    public class USER_MSG_LOG_BO
    {
        public int AddUSER_MSG_LOG(USER_MSG_LOG_Info objTable)
        {
            return USER_MSG_LOG_DAO.Instance().AddUSER_MSG_LOG(objTable);
        }
        public DataSet GetUser_msg_log(string pUserid,DateTime pDateFrom,DateTime pDateTo)
        {
            return USER_MSG_LOG_DAO.Instance().GetUser_msg_log(pUserid, pDateFrom, pDateTo);
        }
        public DataSet GetUser_msg_log(DateTime pDateFrom, DateTime pDateTo)
        {
            return USER_MSG_LOG_DAO.Instance().GetUser_msg_log(pDateFrom, pDateTo);
        }
        public DataSet GetUser_log()
        {
            return USER_MSG_LOG_DAO.Instance().GetUser_log();
        }
    }
}
