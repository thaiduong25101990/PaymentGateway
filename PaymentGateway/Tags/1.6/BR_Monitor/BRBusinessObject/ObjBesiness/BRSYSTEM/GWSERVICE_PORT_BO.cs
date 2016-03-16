using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRBusinessObject
{
    public class GWSERVICE_PORTController
    {
        public int AddGWSERVICE_PORT(GWSERVICE_PORTInfo objTable)
        {
            return GWSERVICE_PORTDP.Instance().AddGWSERVICE_PORT(objTable);
        }

        public int UpdateGWSERVICE_PORT(GWSERVICE_PORTInfo objTable)
        {
            return GWSERVICE_PORTDP.Instance().UpdateGWSERVICE_PORT(objTable);
        }

        public int DeleteGWSERVICE_PORT(int iID)
        {
            return GWSERVICE_PORTDP.Instance().DeleteGWSERVICE_PORT(iID);
        }

        public DataSet GetGWSERVICE_PORT()
        {
            return GWSERVICE_PORTDP.Instance().GetGWSERVICE_PORT();
        }
    }
}
