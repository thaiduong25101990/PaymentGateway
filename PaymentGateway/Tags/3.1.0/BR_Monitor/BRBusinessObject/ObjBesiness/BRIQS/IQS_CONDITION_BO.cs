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
    public class IQS_CONDITIONController
    {
        public int ADDIQS(IQS_CONDITIONInfo objTable)
        {
            return IQS_CONDITIONDP.Instance().ADDIQS(objTable);
        }
        public int DELETE(string ID)
        {
            return IQS_CONDITIONDP.Instance().DELETE(ID);
        }
        public DataTable GetIQS()
        {
            return IQS_CONDITIONDP.Instance().GetIQS();
        }
        public DataTable GetIQS_DISTIN()
        {
            return IQS_CONDITIONDP.Instance().GetIQS_DISTIN();
        }
        public DataSet GetIQS_DISTIN1()
        {
            return IQS_CONDITIONDP.Instance().GetIQS_DISTIN1();
        }
        public DataTable Search(string Gwtype)
        {
            return IQS_CONDITIONDP.Instance().Search(Gwtype);
        }
        public DataTable Search1(string Gwtype,string Type)
        {
            return IQS_CONDITIONDP.Instance().Search1(Gwtype, Type);
        }
        public DataTable GetKT(string Type,string Gwtype)
        {
            return IQS_CONDITIONDP.Instance().GetKT(Type, Gwtype);
        }
    }
}
