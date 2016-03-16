using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRIBPS
{
    public class clsSTATUS
    {
        public static DataSet STATUS_ERROR_CODE()
        {
            try
            {
                STATUSController ControlStatus = new STATUSController();
                DataSet _dtStaus;
                string pGWTYPE = "SYSTEM";
                return _dtStaus = ControlStatus.STATUS_ERROR_CODE(pGWTYPE, out _dtStaus);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static DataSet GET_ALL_STATUS()
        {
            try
            {
                ALLCODEController ControlAllcode = new ALLCODEController();
                DataSet _dtStaus;
                //string pGWTYPE = "SYSTEM";
                return _dtStaus = ControlAllcode.IBPS_STATUS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        //lay trang thai staus va cong tad cho form forward
        public static DataSet STATUS_TAD(string pTELLERID)
        {
            try
            {
                ALLCODEController ControlAllcode = new ALLCODEController();
                DataSet _dtStaus;
                return _dtStaus = ControlAllcode.STATUS_TAD(pTELLERID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

    }
}
