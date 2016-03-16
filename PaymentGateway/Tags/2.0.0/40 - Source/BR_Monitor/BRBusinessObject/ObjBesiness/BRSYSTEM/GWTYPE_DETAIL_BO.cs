using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
//' Author:	Le Duc Quan
//' Create date:	20/06/2008

   public class GWTYPE_DETAILController
    {
       public int AddGWTYPE_DETAIL(GWTYPE_DETAILInfo objTable)
       {
           return GWTYPE_DETAILDP.Instance().AddGWTYPE_DETAIL(objTable);
       }

       public int UpdateGWTYPE_DETAIL(GWTYPE_DETAILInfo objTable)
       {
           return GWTYPE_DETAILDP.Instance().UpdateGWTYPE_DETAIL(objTable);
       }

       public bool DeleteGWTYPE_DETAIL(string strGWTYPE)
       {
           return GWTYPE_DETAILDP.Instance().DeleteGWTYPE_DETAIL(strGWTYPE);
       }
       public bool DeleteGWTYPE_DETAIL1(string strGWTYPE)
       {
           return GWTYPE_DETAILDP.Instance().DeleteGWTYPE_DETAIL1(strGWTYPE);
       }
       public DataSet GetGWTYPE_DETAIL(string GWType)
       {
           return GWTYPE_DETAILDP.Instance().GetGWTYPE_DETAIL(GWType);
       }
       public DataSet GetGWTYPE_DETAILExist(string GWType)
       {
           return GWTYPE_DETAILDP.Instance().GetGWTYPE_DETAILExist(GWType);
       }

       public DataTable GWTYPE_SELECT(int pID)
       {
           return GWTYPE_DETAILDP.Instance().GWTYPE_SELECT(pID);
       }
    }
}
