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
    public class BIDV_ACCOUNTController
    {
       public DataSet SelectAccount(BIDV_ACCOUNT_Info objTable)
        {
            return BIDV_ACCOUNTDP.Instance().SelectAccount(objTable);//DataTable Get_BIDV()//QUYND
        }
       public DataTable Get_BIDV(string pWHERE)//QUYND
       {
           return BIDV_ACCOUNTDP.Instance().Get_BIDV(pWHERE);//QUYND
       }

       public int AddACCOUNT(BIDV_ACCOUNT_Info objTable)
       {
           return BIDV_ACCOUNTDP.Instance().AddACCOUNT(objTable);
       }
       public int UpdateACCOUNT(BIDV_ACCOUNT_Info objTable)
       {
           return BIDV_ACCOUNTDP.Instance().UpdateACCOUNT(objTable);
       }

       public int DeleteACCOUNT(BIDV_ACCOUNT_Info objTable)
       {
           return BIDV_ACCOUNTDP.Instance().DeleteACCOUNT(objTable);
       }
       public int Insert_BIDV(BIDV_ACCOUNT_Info objTable)
       {
           return BIDV_ACCOUNTDP.Instance().Insert_BIDV(objTable);
       }
       public int Update_BIDV(BIDV_ACCOUNT_Info objTable)
       {
           return BIDV_ACCOUNTDP.Instance().Update_BIDV(objTable);
       }
       public int Delete_BIDV(BIDV_ACCOUNT_Info objTable)
       {
           return BIDV_ACCOUNTDP.Instance().Delete_BIDV(objTable);
       }
       public DataTable GET_BIDV(string strField, string pCCYCD)
       {
           return BIDV_ACCOUNTDP.Instance().GET_BIDV(strField,pCCYCD);
       }
    }
}
