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
    public class VCB_TS_BO
    {
        public int Insert(VCB_TS_Info objTable)
        {
            return VCB_TS_DAO.Instance().Insert(objTable);
        }

        public DataSet SearchAdvance(string pWHERE,out DataSet _dtContent)
        {
            return VCB_TS_DAO.Instance().SearchAdvance(pWHERE, out _dtContent);
        }


        public DataSet ViewVCB_TS(string pID, out DataSet _dtContent)
        {
            return VCB_TS_DAO.Instance().ViewVCB_TS(pID, out _dtContent);
        }

        public int Update(VCB_TS_Info objTable)
        {
            return VCB_TS_DAO.Instance().Update(objTable);
        }

        //Ham lay thong tin dien can tra soat
        public DataSet GetMSGByRM(string pWHERE, string sField20, out DataSet _dtContent)
        {
            return VCB_TS_DAO.Instance().GetMSGByRM(pWHERE,sField20, out _dtContent);
        }

        public int CheckSubMsg(long sID)
        {
            return VCB_TS_DAO.Instance().CheckSubMsg(sID);
        }

        public int CheckMsgApprove(long sID)
        {
            return VCB_TS_DAO.Instance().CheckMsgApprove(sID);
        }
        public string GetSBT_ID_New()
        {
            return VCB_TS_DAO.Instance().GetSBT_ID_New();
        }        
    }
}
