using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
    public class RPTPARAMETER_BO
    {
        public DataTable GET_RPTPARAMETER(string pRPTNAME, out DataTable _dt)
        {
            return RPTPARAMETER_DAO.Instance().GET_RPTPARAMETER(pRPTNAME, out _dt);
        }

        public int Insert_RptParameter(RPTPARAMETER_Info objPara)
        {
            return RPTPARAMETER_DAO.Instance().Insert_RptParameter(objPara);
        }

        public int Update_RptParameter(RPTPARAMETER_Info objPara)
        {
            return RPTPARAMETER_DAO.Instance().Update_RptParameter(objPara);
        }

        public int Delete_RptParameter(RPTPARAMETER_Info objPara)
        {
            return RPTPARAMETER_DAO.Instance().Delete_RptParameter(objPara);
        }
    }
}
