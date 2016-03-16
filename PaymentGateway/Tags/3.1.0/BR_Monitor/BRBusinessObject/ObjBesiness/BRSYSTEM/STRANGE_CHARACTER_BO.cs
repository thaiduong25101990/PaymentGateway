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
namespace  BR.BRBusinessObject
{
	public class STRANGE_CHARACTERController
	{
		
		public int AddSTRANGE_CHARACTER(STRANGE_CHARACTERInfo objTable)
		{
			return STRANGE_CHARACTERDP.Instance().AddSTRANGE_CHARACTER(objTable);
		}
		
		public int UpdateSTRANGE_CHARACTER(STRANGE_CHARACTERInfo objTable)
		{
			return STRANGE_CHARACTERDP.Instance().UpdateSTRANGE_CHARACTER(objTable);
		}
		
		public int DeleteSTRANGE_CHARACTER(int ID)
		{
			return STRANGE_CHARACTERDP.Instance().DeleteSTRANGE_CHARACTER(ID);
		}

        public DataSet GetSTRANGE_CHARACTER()
        {
            return STRANGE_CHARACTERDP.Instance().GetSTRANGE_CHARACTER();
        }
        //GetGWTYPESearch
        public DataSet GetSTRANGE_CHARACTERSearch(string strSQL)
        {
            return STRANGE_CHARACTERDP.Instance().GetSTRANGE_CHARACTERSearch(strSQL);
        }
        public bool IDIsExisting(string strStrangeChar, string strMSGType, string strDepartment, string strDirection,string strField)
        {
            return STRANGE_CHARACTERDP.Instance().IDIsExisting(strStrangeChar, strMSGType, strDepartment, strDirection,strField);
        }
        // BichNN add 03/08/2008
        // Check data exist
        public int CheckSTRANGE_CHARACTER_Record(STRANGE_CHARACTERInfo objTable)
        {
            return STRANGE_CHARACTERDP.Instance().GetData(objTable);
        }

        public int CheckSTRANGE_CHARACTER_Dif(STRANGE_CHARACTERInfo objTable)
        {
            return STRANGE_CHARACTERDP.Instance().CheckSTRANGE_CHARACTER_Dif(objTable);
        }

        public int CheckSTRANGE_CHARACTER_AtLeast(STRANGE_CHARACTERInfo objTable)
        {
            return STRANGE_CHARACTERDP.Instance().CheckSTRANGE_CHARACTER_AtLeast(objTable);
        }
	}
	
	
}
