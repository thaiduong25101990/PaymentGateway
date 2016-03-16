using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace BIDVWEB.Business.Reports
{
    public class clsMSReport
    {
        //simulate a database call that reutns a datatable
        public DataTable GetCustomerData(string strRN)
        {
            DataSet newDataSet = new DataSet();
            DataTable returnValue = new DataTable();
            newDataSet.Tables.Add(returnValue);

            //create the columns
            returnValue.Columns.Add("Id");
            returnValue.Columns.Add("FirstName");
            returnValue.Columns.Add("LastName");

            //add some data
            returnValue.Rows.Add(new object[] { 1, "Clark", "Kent" });
            returnValue.Rows.Add(new object[] { 2, "Lois", "Lane" });
            returnValue.Rows.Add(new object[] { 3, "Bruce", "Wayne" });

            //return the data
            return returnValue;
        }
    }
}
