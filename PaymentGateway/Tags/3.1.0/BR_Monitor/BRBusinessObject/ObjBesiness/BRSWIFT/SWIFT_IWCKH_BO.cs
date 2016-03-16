/*---------------------------------------------------------------
    * Muc dich         : Khai bao cac ham cap nhat tham so kiem tra dien ve tu Swift:dien Oldkey/Failure/Duplicate 
    * Ngay tao         : 18/06/2008
    * Nguoi tao        : Hantt10
 *--------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
   public class SWIFT_IWCKHController
    {

        public int AddSWIFT_IWCKH(SWIFT_IWCKHInfo objTable)
        {
            return SWIFT_IWCKHDP.Instance().AddSWIFT_IWCKH(objTable);
        }

        public int UpdateSWIFT_IWCKH(SWIFT_IWCKHInfo objTable)
        {
            return SWIFT_IWCKHDP.Instance().UpdateSWIFT_IWCKH(objTable);
        }

        public int DeleteSWIFT_IWCKH(string iID,string strValue)
        {
            return SWIFT_IWCKHDP.Instance().DeleteSWIFT_IWCKH(iID, strValue);
        }

        public DataSet GetSWIFT_IWCKH()
        {
            return SWIFT_IWCKHDP.Instance().GetSWIFT_IWCKH();
        }
    }
}
