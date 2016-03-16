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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class MSG_EXCELController
    {

        public int AddMSG_EXCEL(MSG_EXCELInfo objTable)
        {
            return MSG_EXCELDP.Instance().AddMSG_EXCEL(objTable);
        }

        public int UpdateMSG_EXCEL(MSG_EXCELInfo objTable)
        {
            return MSG_EXCELDP.Instance().UpdateMSG_EXCEL(objTable);
        }

        public int DeleteMSG_EXCEL(int iFieldID)
        {
            return MSG_EXCELDP.Instance().DeleteMSG_EXCEL(iFieldID);
        }

        public DataSet GetMSG_EXCEL()
        {
            return MSG_EXCELDP.Instance().GetMSG_EXCEL();
        }

        public DataSet GetMSG_EXCELSearch(string chrGWType, string charMsgType)
        {
            return MSG_EXCELDP.Instance().GetMSG_EXCELSearch(chrGWType,charMsgType);
        }

        public DataSet EXCELSearch(string chrGWType, string charMsgType,string chrDirection)
        {
            return MSG_EXCELDP.Instance().EXCELSearch(chrGWType, charMsgType, chrDirection);
        }

        public DataSet GetMSG_EXCEL_GWType(string chrGWType)
        {
            return MSG_EXCELDP.Instance().GetMSG_EXCEL_GWType(chrGWType);
        }

        public DataSet GetMSG_EXCEL_MsgType(string chrGWType)
        {
            return MSG_EXCELDP.Instance().GetMSG_EXCEL_MsgType(chrGWType);
        }

    }


}
