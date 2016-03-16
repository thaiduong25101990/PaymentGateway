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
    public class SWIFT_BANK_MAPController
    {
        public DataSet GetSWIFT_BANK_MAPA(string pSIBS_BANK_CODE)
        {
            return SWIFT_BANK_MAPDP.Instance().GetSWIFT_BANK_MAPA(pSIBS_BANK_CODE);
        }
        public DataSet GetSWIFT_BANK_MAPB(string pSWIFT_BANK_CODE)
        {
            return SWIFT_BANK_MAPDP.Instance().GetSWIFT_BANK_MAPB(pSWIFT_BANK_CODE);
        }
        public DataSet GetSWIFT_BANK_MAP_ReceivedBranch()
        {
            return SWIFT_BANK_MAPDP.Instance().GetSWIFT_BANK_MAP_ReceivedBranch();
        }
        public DataTable Search_bank_Map(string pSIBS_BANK_CODE)
        {
            return SWIFT_BANK_MAPDP.Instance().Search_bank_Map(pSIBS_BANK_CODE);
        }

        public DataSet GetSWIFT_BANK_MAP_ALL(string pWhere)
        {
            return SWIFT_BANK_MAPDP.Instance().GetSWIFT_BANK_MAP_ALL(pWhere);
        }

        public int AddSWIFT_BANK_MAP(SWIFT_BANK_MAPInfo objTable)
        {
            return SWIFT_BANK_MAPDP.Instance().AddSWIFT_BANK_MAP(objTable);
        }

        public int UpdateSWIFT_BANK_MAP(SWIFT_BANK_MAPInfo objTable)
        {
            return SWIFT_BANK_MAPDP.Instance().UpdateSWIFT_BANK_MAP(objTable);
        }

        public int DeleteSWIFT_BANK_MAP(SWIFT_BANK_MAPInfo objTable)
        {
            return SWIFT_BANK_MAPDP.Instance().DeleteSWIFT_BANK_MAP(objTable);
        }
      
    }
}
