using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.BRBusinessObject.ObjectProcess.BRIBPS;
using System.Data;
using BR.BRBusinessObject.ObjectInfo.BRIBPS;

namespace BR.BRBusinessObject
{
    public class IBPSTADMAPController
    {
        public DataTable GetComboboxData()
        {
            return IBPS_TAD_MAP_DP.Instance().GetComboboxData();
        }

        public DataTable GetGata(string gwBankCode)
        {
            return IBPS_TAD_MAP_DP.Instance().GetData(gwBankCode);
        }

        public int DELETE(string ID)
        {
            return IBPS_TAD_MAP_DP.Instance().DELETE(ID);
        }

        public int ADD(IBPS_TAD_MAP_Info ibps_tad_map_info)
        {
            return IBPS_TAD_MAP_DP.Instance().ADD(ibps_tad_map_info);
        }

        public int UPDATE(IBPS_TAD_MAP_Info ibps_tad_map_info)
        {
            return IBPS_TAD_MAP_DP.Instance().UPDATE(ibps_tad_map_info);
        }
    }
}
