using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
    public class BRANCHController
    {
      

        public DataSet GetBRANCH_MAP(string pBANK_CODE)
        {
            return BRANCHDP.Instance().GetBRANCH_MAP(pBANK_CODE);
        }

        public DataTable CHECK_BRANCH_MAP(string pGW_BANK_CODE, string pSIBS_BANK_CODE)
        {
            return BRANCHDP.Instance().CHECK_BRANCH_MAP(pGW_BANK_CODE, pSIBS_BANK_CODE);
        }

        public DataSet GetData()
        {
            return BRANCHDP.Instance().GetData();
        }


        public DataTable GetData_Leave(string pSIBS_BANK_CODE)
        {
            return BRANCHDP.Instance().GetData_Leave(pSIBS_BANK_CODE);
        }
        public DataSet GetData(string strWhere)
        {
            return BRANCHDP.Instance().GetData(strWhere);
        }
        public DataSet Select(string strCondition)
        {
            return BRANCHDP.Instance().Select(strCondition);
        }
        public int Insert(BRANCHInfo obj)
        {
            return BRANCHDP.Instance().Insert(obj);
        }
        public int Update(BRANCHInfo obj)
        {
            return BRANCHDP.Instance().Update(obj);
        }
        public int Delete(string strSIBS_BANK_CODE)
        {
            return BRANCHDP.Instance().Delete(strSIBS_BANK_CODE);
        }
        public DataTable BRANCH_OtherBranch(string strBRANCH_TYPE,string strSIBS_BANK_CODE)
        {
            return BRANCHDP.Instance().BRANCH_OtherBranch(strBRANCH_TYPE,strSIBS_BANK_CODE);
        }
    }
}
