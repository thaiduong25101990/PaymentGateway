using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.BRBusinessObject.ObjBesiness.BRSYSTEM;
using BR.BRBusinessObject.ObjectInfo.BRSYSTEM;
using System.Data;
using System.Data.OracleClient;
using BR.DataAccess;

namespace BR.BRBusinessObject.ObjectProcess.BRSYSTEM
{
    public class WorkingOffDay_Dao : WorkingOffDayServices 
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        #region WorkingOffDayServices Members

        public DataSet GetMsList()
        {
            DataSet ds = new DataSet();
            //OracleParameter[] oraParms = { new OracleParameter("offDay", OracleType.DateTime),
            //                             new OracleParameter("description",OracleType.VarChar)};
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                ds = OracleHelper.ExecuteDataset(oraConn,CommandType.Text, "select * from GW_WORKING_DAY");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

        public int CreateMsg(WorkingOffDay_info objWorkingOff)
        {
            string sqlExcute = "";
            try
            {
                sqlExcute = "Insert into GW_WORKING_DAY (offday,description) values(:offDay,:description)";
                OracleParameter[] oraParms = { new OracleParameter("offDay", OracleType.DateTime),
                                            new OracleParameter("description",OracleType.VarChar)};
                oraParms[0].Value = (Object)objWorkingOff._dtOffDay;
                oraParms[1].Value = objWorkingOff._sDescription;

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return 0;
                }
                int i = OracleHelper.ExecuteNonQuery(oraConn, CommandType.Text, sqlExcute, oraParms);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool DeleteMsg(int id)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return false;
                }
                int i = OracleHelper.ExecuteNonQuery(oraConn, CommandType.Text, "Delete from GW_WORKING_DAY where id=" + id);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            
        }

        public int EditMsg(WorkingOffDay_info objWorkingOff)
        {
            string sqlExcute = "";
            try
            {
                sqlExcute = "Update GW_WORKING_DAY set offday=:offDay,description=:description where id=" + objWorkingOff.id;
                OracleParameter[] oraParms = { new OracleParameter("offDay", OracleType.DateTime),
                                            new OracleParameter("description",OracleType.VarChar)};
                oraParms[0].Value = (Object)objWorkingOff._dtOffDay;
                oraParms[1].Value = objWorkingOff._sDescription;

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return 0;
                }
                int i = OracleHelper.ExecuteNonQuery(oraConn, CommandType.Text, sqlExcute, oraParms);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
