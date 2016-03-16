using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace BR.BRBusinessObject
{
    public class MSG_FIELDController
    {
        //public DataSet GetMSG_FIELD(string pMSGType, string pGWTYPE)
        //{
        //    return MSG_FIELDDP.Instance().GetMSG_FIELD(pMSGType, pGWTYPE);
        //}
        public DataSet GetGWTYPE(string pGWTYPE)
        {
            return MSG_FIELDDP.Instance().GetGWTYPE(pGWTYPE);
        }
        public DataSet GetMSGType(string pMsgType)
        {
            return MSG_FIELDDP.Instance().GetMSGType(pMsgType);
        }
    }
}
