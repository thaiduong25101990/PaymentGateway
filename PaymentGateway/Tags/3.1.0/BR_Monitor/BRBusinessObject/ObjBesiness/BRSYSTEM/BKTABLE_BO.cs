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

//' =============================================
//' Template: BusinessObject.xslt 17/10/2006
//' Author:	Nguyen duc quy
//' Create date:	09/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================

namespace BR.BRBusinessObject
{
    public class BKTABLEController
    {
        //public int AddBKTABLE(BKTABLEInfo objTable)
        //{

        //    //return DEPARTMENTDP.Instance().AddBKTABLE(objTable);
        //}

        //public int UpdateBKTABLE(BKTABLEInfo objTable)
        //{
        //    //return BKTABLEDP.Instance().UpdateBKTABLE(objTable);
        //}

        //public int DeleteBKTABLE(int iID)
        //{
        //    //return BKTABLEDP.Instance().DeleteBKTABLE(iID);
        //}string pBKTYPE,string pBKTIME

        public DataSet Get_data()
        {
            return BKTABLEDP.Instance().Get_data();
        }
        public DataSet Get_data1(string strBKTYPE)
        {
            return BKTABLEDP.Instance().Get_data1(strBKTYPE);
        }
        public DataSet GetBKTABLE(string pBKTYPE)
        {
            return BKTABLEDP.Instance().GetBKTABLE(pBKTYPE);
        }
        public DataSet GetBKTABLE1(string pBKTYPE, string pBKTIME)
        {
            return BKTABLEDP.Instance().GetBKTABLE1(pBKTYPE, pBKTIME);
        }
        public DataSet GetBk_Search(string pBKTYPE,string pBKTIME)
        {
            return BKTABLEDP.Instance().GetBk_Search(pBKTYPE, pBKTIME);
        }
        public DataSet GetBk_SearchBktype(string pBKTYPE)
        {
            return BKTABLEDP.Instance().GetBk_SearchBktype(pBKTYPE);
        }
        public DataSet GetBk_Bktime(string pBKTIME)
        {
            return BKTABLEDP.Instance().GetBk_Bktime(pBKTIME);
        }
        public DataSet GetBk_SearchBktype1(string pBKTYPE,string pBKTIME)
        {
            return BKTABLEDP.Instance().GetBk_SearchBktype1(pBKTYPE, pBKTIME);
        }
        public DataSet GetBk_SearchDestbl(string pBKTYPE)
        {
            return BKTABLEDP.Instance().GetBk_SearchDestbl(pBKTYPE);
        }
        public DataSet GetBKTABLE_DATA(string pTable1, string pTable2)
        {
            return BKTABLEDP.Instance().GetBKTABLE_DATA(pTable1, pTable2);
        }
        public int UpdateLASTEXP(DateTime dtLastExp, string strSOURCETBL)
        {
            return BKTABLEDP.Instance().UpdateLASTEXP(dtLastExp, strSOURCETBL);
        }
        public int UpdateField(string strFieldUpdate, string strValueUpdate, string strFieldKey, string strValueKey)
        {
            return BKTABLEDP.Instance().UpdateField(strFieldUpdate, strValueUpdate, strFieldKey, strValueKey);
        }
    }
}
