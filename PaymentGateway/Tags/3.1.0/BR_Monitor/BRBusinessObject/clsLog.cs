using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class clsLog
    {

        USER_MSG_LOGInfo objUserLog_Info = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objUserLog = new USER_MSG_LOGController();
        

       /*---------------------------------------------------------------
       * Method           : Ghiloguser(DateTime Logdate, string strUsername, 
       *                    string strContent, int Log_level, string strWorked, 
       *                    string strTale_Access, string strOld_value, 
       *                    string strNew_value)
       * Muc dich         : Ham ghi log User dang nhap vao he thong
       * Tham so          : cac gia tri tuong ung voi bang User_msg_log
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
       *--------------------------------------------------------------*/
        public void GhiLogUser(DateTime Logdate, string strUsername, string strContent, 
            int Log_level, string strWorked, string strTale_Access, string strOld_value, 
            string strNew_value)
        {
            objUserLog_Info.LOG_DATE = Logdate;
            objUserLog_Info.USERID = strUsername;
            objUserLog_Info.CONTENT = strContent;
            objUserLog_Info.STATUS = Log_level;
            objUserLog_Info.WORKED = strWorked;
            objUserLog_Info.TABLE_ACCESS = strTale_Access;
            objUserLog_Info.OLD_VALUE = strOld_value;
            objUserLog_Info.NEW_VALUE = strNew_value;
            //Save
            objUserLog.AddUSER_MSG_LOG1(objUserLog_Info);
        }
    }
}
