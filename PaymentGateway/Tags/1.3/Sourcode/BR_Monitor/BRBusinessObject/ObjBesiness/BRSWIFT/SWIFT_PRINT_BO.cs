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
    public class SWIFTPRINTController
    {
        //public int InsertSwiftPrint(SWIFTPRINTInfo objTable)
        //{
        //    return SWIFTPRINTDP.Instance().InsertSwiftPrint(objTable);
        //}
        //public DataTable GetSWIFT_IW_STATEMENT()
        //{
        //    return SWIFTPRINTDP.Instance().GetSWIFT_IW_STATEMENT();
        //}
        public DataSet Search(string pCondition, DateTime pDateFrom, DateTime pDateTo)
        {
            return SWIFTPRINTDP.Instance().Search(pCondition, pDateFrom, pDateTo);
        }
        public DataSet LoaddataGrid(DateTime pDateFrom, DateTime pDateTo)
        {
            return SWIFTPRINTDP.Instance().LoaddataGrid(pDateFrom, pDateTo);
        }
        public DataSet LoadSWIFT_IN_RM(DateTime pDateFrom, DateTime pDateTo)
        {
            return SWIFTPRINTDP.Instance().LoadSWIFT_IN_RM(pDateFrom, pDateTo);
        }
        //public int Delete(string pQUERYID)
        //{
        //    return SWIFTPRINTDP.Instance().Delete(pQUERYID);
        //}
        public int Delete_Resend(string pQUERYID)
        {
            return SWIFTPRINTDP.Instance().Delete_Resend(pQUERYID);
        }
        //public int INSERT_SWIFT_IW_STATEMENT(string strQuery_id, string teller)
        //{
        //    return SWIFTPRINTDP.Instance().INSERT_SWIFT_IW_STATEMENT(strQuery_id, teller);
        //}
        ////update ham insert vao bang SWIFT_IW_STATEMENT de ko phai dung bang tam 
        ////08/09/2008
        //public int INSERT_SWIFT_IW_STATEMENT_UPDATE( string teller)
        //{
        //    return SWIFTPRINTDP.Instance().INSERT_SWIFT_IW_STATEMENT_UPDATE(teller);
        //}
        //public int INSERT_SWIFT_IW_STATEMENT_RESEND(string teller)
        //{
        //    return SWIFTPRINTDP.Instance().INSERT_SWIFT_IW_STATEMENT_RESEND(teller);
        //}
        //public DataTable GET_PRINT_STATEMENT(string teller)
        //{
        //    return SWIFTPRINTDP.Instance().GET_PRINT_STATEMENT(teller);
        //}
        //public DataTable GET_PRINT_STATEMENT_RESEND(string teller)
        //{
        //    return SWIFTPRINTDP.Instance().GET_PRINT_STATEMENT_RESEND(teller);
        //}
        ////Xu ly thu cong kieu moi bang cach goi ham package
        ////DatHm
        ////Add 09/01/2009
        public DataTable PRINT_STATEMENT_MANUAL(string teller)
        {
            return SWIFTPRINTDP.Instance().PRINT_STATEMENT_MANUAL(teller);
        }
        public DataTable PRINT_STATEMENT_RESEND(string teller)
        {
            return SWIFTPRINTDP.Instance().PRINT_STATEMENT_RESEND(teller);
        }
        ////end 
        //public int INSERT_SWIFT_IW_STATEMENT_TEMP(string strQuery_id, string teller)
        //{
        //    return SWIFTPRINTDP.Instance().INSERT_SWIFT_IW_STATEMENT_TEMP(strQuery_id, teller);
        //}
        //public int DELETE_SWIFT_IW_STATEMENT_TEMP()
        //{
        //    return SWIFTPRINTDP.Instance().DELETE_SWIFT_IW_STATEMENT_TEMP();
        //}
        public DataTable Search_QueryID(string pQUERY_ID)
        {
            return SWIFTPRINTDP.Instance().Search_QueryID(pQUERY_ID);
        }
        //public DataTable GetSYSDATE()
        //{
        //    return SWIFTPRINTDP.Instance().GetSYSDATE();
        //}
        public DataSet SearchSWIFT_IN_RM(string pCondition, DateTime pDateFrom, DateTime pDateTo)
        {
            return SWIFTPRINTDP.Instance().SearchSWIFT_IN_RM(pCondition, pDateFrom, pDateTo);
        }
        public DataTable GetData_Reprint(int StatementID, string time_print)
        {
            return SWIFTPRINTDP.Instance().GetData_Reprint(StatementID, time_print);
        }
        public DataTable GetData_Reprint_RM(int StatementID, string time_print)
        {
            return SWIFTPRINTDP.Instance().GetData_Reprint_RM(StatementID, time_print);
        }
        public int Update_statement_type(string pQuery_id)
        {
            return SWIFTPRINTDP.Instance().Update_statement_type(pQuery_id);
        }
    }
}
