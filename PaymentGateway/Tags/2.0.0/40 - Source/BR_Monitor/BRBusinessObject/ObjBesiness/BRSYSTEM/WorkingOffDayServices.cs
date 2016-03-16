using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BR.BRBusinessObject.ObjectInfo.BRSYSTEM;

namespace BR.BRBusinessObject.ObjBesiness.BRSYSTEM
{
   public interface  WorkingOffDayServices
    {
        DataSet GetMsList();
        int CreateMsg(WorkingOffDay_info objWorkingOff);
        bool DeleteMsg(int id);
        int EditMsg(WorkingOffDay_info objWorkingOff);

    }
}
