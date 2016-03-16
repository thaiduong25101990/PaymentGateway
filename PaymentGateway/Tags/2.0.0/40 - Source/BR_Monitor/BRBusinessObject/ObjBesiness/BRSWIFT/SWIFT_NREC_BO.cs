using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
    public class SWIFT_NRECController
    {
        public int AddSWIFT_NREC(SWIFT_NRECInfo objTable)
        {
            return SWIFT_NRECDP.Instance().AddSWIFT_NREC(objTable);
        }

        //public int UpdateSWIFT_NREC(SWIFT_NRECInfo objTable)
        //{
        //    return SWIFT_NRECDP.Instance().UpdateSWIFT_NREC(objTable);
        //}

        public int DeleteSWIFT_NREC(int iID)
        {
            return SWIFT_NRECDP.Instance().DeleteSWIFT_NREC(iID);
        }

        public DataSet GetSWIFT_NREC()
        {
            return SWIFT_NRECDP.Instance().GetSWIFT_NREC();
        }
        public DataSet GetSWIFT_NRECSearch(string MSGType)
        {
            return SWIFT_NRECDP.Instance().GetSWIFT_NRECSearch(MSGType);
        }
    }
}
