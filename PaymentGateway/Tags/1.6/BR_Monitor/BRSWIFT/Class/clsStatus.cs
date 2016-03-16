using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRSWIFT
{
    public class clsStatus
    {
        public static DataTable Get_Status()
        {
            try
            {
                STATUSController ControlStatus = new STATUSController();
                DataTable _dtStaus;
                string pGWTYPE = "SYSTEM";
                return _dtStaus = ControlStatus.GET_STATUS(pGWTYPE, out _dtStaus);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        
        public static DataTable Get_Error_code()
        {
            try
            {
                ERROR_CODEContrller ControlError_code = new ERROR_CODEContrller();      
                DataTable _dtError_code;               
                _dtError_code = ControlError_code.GET_ERROR_CODE("SYSTEM", out _dtError_code);
                return _dtError_code;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        public static DataSet Get_Processsts_Swmsts()
        {
            try
            {
                ALLCODEController ControlAll_code = new ALLCODEController();
                DataSet _dtError_code;
                _dtError_code = ControlAll_code.SWMSTS_PROCESSSTS("SWMSTS","PROCESSSTS");
                return _dtError_code;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        
    }
}
