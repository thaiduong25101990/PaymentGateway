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
//' Create date:	06/006/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class DATABASE_CLEANController
    {
        public DataSet Delete_table(string pTableName)
        {
            return DATABASE_CLEANDP.Instance().Delete_table(pTableName);
        }//GetTable
        public DataSet GetTable()
        {
            return DATABASE_CLEANDP.Instance().GetTable();
        }
        public DataSet GetColumns_table(string pTableName)
        {
            return DATABASE_CLEANDP.Instance().GetColumns_table(pTableName);
        }
        public DataSet GetValue_colum(string pTableName, string pColumnName)
        {
            return DATABASE_CLEANDP.Instance().GetValue_colum(pTableName, pColumnName);
        }
        public int Check_Query(string pQuery)
        {
            return DATABASE_CLEANDP.Instance().Check_Query(pQuery);
        }
        public DataSet GetComment_table(string strTable)
        {
            return DATABASE_CLEANDP.Instance().GetComment_table(strTable);
        }
        
    }
}
