using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
    public class IQS_MSG_CONTENTController
    {
        public int AddIQS_MSG_CONTENT(IQS_MSG_CONTENTInfo objTable)
        {
            return IQS_MSG_CONTENTDP.Instance().AddIQS_MSG_CONTENT(objTable);
        }
        public int AddIQS_MSG_CONTENT_TTSP_VCB(IQS_MSG_CONTENTInfo objTable)
        {
            return IQS_MSG_CONTENTDP.Instance().AddIQS_MSG_CONTENT_TTSP_VCB(objTable);
        }
        public int UpdateIQS_MSG_CONTENT_IBPS(IQS_MSG_CONTENTInfo objTable)
        {
            return IQS_MSG_CONTENTDP.Instance().UpdateIQS_MSG_CONTENT_IBPS(objTable);
        }
        public DataSet GetIQS_MSG_CONTENT(string Interface, int QueryID)
        {
            return IQS_MSG_CONTENTDP.Instance().GetIQS_MSG_CONTENT(Interface, QueryID);
        }
        //public DataSet GetIQS_MSG_CONTENT(string Interface, int QueryID)
        //{
        //    return IQS_MSG_CONTENTDP.Instance().GetIQS_MSG_CONTENT(Interface, QueryID);
        //}  
        public DataSet GetIQS_MSG_CONTENT(string Interface, List<int> QueryIDList)
        {
            return IQS_MSG_CONTENTDP.Instance().GetIQS_MSG_CONTENT(Interface, QueryIDList);
        }
        public DataSet GetIQS_MSG_CONTENTList(string Interface)
        {
            return IQS_MSG_CONTENTDP.Instance().GetIQS_MSG_CONTENTList(Interface);
        }
        //public int UpdateIQS_MSG_CONTENT_IBPS(IQS_MSG_CONTENTInfo objTable)
        //{
        //    return IQS_MSG_CONTENTDP.Instance().UpdateIQS_MSG_CONTENT_IBPS(objTable);
        //}
        //UpdateIQS_MSG_CONTENT_IBPS_New1

        public int UpdateIQS_MSG_CONTENT_IBPS_New1(IQS_MSG_CONTENTInfo objTable)
        {
            return IQS_MSG_CONTENTDP.Instance().UpdateIQS_MSG_CONTENT_IBPS_New1(objTable);
        }
        public DataTable GetIQS()
        {
            return IQS_MSG_CONTENTDP.Instance().GetIQS();
        }
        public DataTable Search_IQS(string strWHERE)
        {
            return IQS_MSG_CONTENTDP.Instance().Search_IQS(strWHERE);
        }
        public DataTable Get_MSGTYPE(int strQueryID)
        {
            return IQS_MSG_CONTENTDP.Instance().Get_MSGTYPE(strQueryID);
        }
        public string IQSGetTrannum(IQS_MSG_CONTENTInfo objTable)
        {
            return IQS_MSG_CONTENTDP.Instance().IQSGetTrannum(objTable);
        }
        public int AddIQS_MSG_CONTENT_TTSP_VCB(IQS_MSG_CONTENTInfo objTable, string strIQSTransNumber, string strMSG_TYPE)
        {
            return IQS_MSG_CONTENTDP.Instance().AddIQS_MSG_CONTENT_TTSP_VCB(objTable, strIQSTransNumber, strMSG_TYPE);
        }
    }
}
