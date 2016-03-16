using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRTTSB
{
    public class clsStatus
    {
        // Lay trang thai cua dien
        public static DataSet GET_ALL_STATUS()
        {
            try
            {
                ALLCODEController ControlAllcode = new ALLCODEController();
                DataSet _dtStaus;
                return _dtStaus = ControlAllcode.TTSP_STATUS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        // Lay cac ma loi cua dien
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

    }
}
