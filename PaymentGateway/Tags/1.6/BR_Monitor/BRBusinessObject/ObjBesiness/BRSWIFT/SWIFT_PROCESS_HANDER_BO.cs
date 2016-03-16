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
    public class SWIFT_PROCESS_HANDERController
    {
        public int UPDATE_SWIFT_PROCESS_HANDER(SWIFT_PROCESS_HANDERInfo objTable)
        {
            return SWIFT_PROCESS_HANDERDP.Instance().UPDATE_SWIFT_PROCESS_HANDER(objTable);
        }
        public int DELETE_SWIFT_PROCESS_HANDER(int pMSG_ID)
        {
            return SWIFT_PROCESS_HANDERDP.Instance().DELETE_SWIFT_PROCESS_HANDER(pMSG_ID);
        }
        public int DELETE_SWIFT_PROCESS(string pMSG_ID, string pUserid)
        {
            return SWIFT_PROCESS_HANDERDP.Instance().DELETE_SWIFT_PROCESS(pMSG_ID, pUserid);
        }
        public int DELETE_SWIFT_PROCESS_HANDER_MANUAL(string pUserID)
        {
            return SWIFT_PROCESS_HANDERDP.Instance().DELETE_SWIFT_PROCESS_HANDER_MANUAL(pUserID);
        }

        public int DELETE_SWIFT_PROCESS_HANDERT(string pUserID, string pNew_process)
        {
            return SWIFT_PROCESS_HANDERDP.Instance().DELETE_SWIFT_PROCESS_HANDERT(pUserID, pNew_process);
        }
    }
}
