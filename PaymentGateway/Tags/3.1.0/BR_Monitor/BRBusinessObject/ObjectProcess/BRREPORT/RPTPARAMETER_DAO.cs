using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.DataAccess;
using System.Data.OracleClient;
using System.Data;

namespace BR.BRBusinessObject
{
    public class RPTPARAMETER_DAO
    {
        public string strErr;
        OracleConnection oraConn = new OracleConnection();
        Connect_Process connect = new Connect_Process();
        public RPTPARAMETER_DAO()
        {
        }

        public static RPTPARAMETER_DAO Instance()
        {
            return new RPTPARAMETER_DAO();
        }

        ///////////////////////////////////////////////////////////
        // Mo ta:       Lay danh tham so cua mot bao cao
        // Tham so:     pRPTNAME: Ten bao cao
        //              _dt: Bang du lieu tham so bao cao tra ra
        // Tra ve:      Bang du lieu tham so bao cao tra ra
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        public DataTable GET_RPTPARAMETER(string pRPTNAME, out DataTable _dt)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dt = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pRPTNAME",OracleType.VarChar,12)
                                         ,new OracleParameter("pCursor",OracleType.Cursor)                                         
                                         };
            Oraparam[0].Value = pRPTNAME;
            Oraparam[1].Direction = ParameterDirection.Output;           

            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_RPTPARAMETER.GET_REPORTPARA", Oraparam).Tables[0];                
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch(Exception ex)
            {
                strErr = ex.Message;
                oraConn.Close();
                oraConn.Dispose();
                return _dt = null;
            }
        }

        ///////////////////////////////////////////////////////////
        // Mo ta:       Them moi mot tham so cho bao cao
        // Tham so:     objPara: Doi tuong tham so bao cao        
        // Tra ve:      Bang -1 :Not successfull
        //              
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        public int Insert_RptParameter(RPTPARAMETER_Info objPara)
        {
            string strSql = "Insert into RPTPARAMETER  (RPTNAME, CRLNAME, CRLTYPE, CRLLENGH, CAPTION," + 
                            " SQL,LSTORD,OPTALL) values (:pRPTNAME, :pCRLNAME, :pCRLTYPE, :pCRLLENGH," + 
                            " :pCAPTION, :pSQL,:pLSTORD,:pOPTALL)";
            OracleParameter[] oraParam = {new OracleParameter("pRPTNAME", OracleType.VarChar  , 12),
                                         new OracleParameter("pCRLNAME", OracleType.VarChar , 20),                                         
                                         new OracleParameter("pCRLTYPE", OracleType.VarChar , 10),
                                         new OracleParameter("pCRLLENGH", OracleType.Number  , 5),
                                         new OracleParameter("pCAPTION", OracleType.VarChar , 255),
                                         new OracleParameter("pSQL", OracleType.VarChar  ,2000),
                                         new OracleParameter("pLSTORD", OracleType.Number  , 2),
                                         new OracleParameter("pOPTALL", OracleType.Number  , 2)
                                         };
            oraParam[0].Value = objPara.RPTNAME;
            oraParam[1].Value = objPara.CRLNAME;
            oraParam[2].Value = objPara.CRLTYPE;
            oraParam[3].Value = objPara.CRLLENGH;
            oraParam[4].Value = objPara.CAPTION;
            oraParam[5].Value = objPara.SQL;
            oraParam[6].Value = objPara.LSTORD;
            if (objPara.OPTALL.ToString() == "")
                oraParam[7].Value = 0;
            else
                oraParam[7].Value = objPara.OPTALL;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                oraConn.Dispose();
                throw (ex);

            }            
        }


        ///////////////////////////////////////////////////////////
        // Mo ta:       Cap nhat lai tham so bao cao khi sua
        // Tham so:     objPara: Doi tuong tham so bao cao      
        // Tra ve:      Bang -1 :Not successfull
        //              
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        public int Update_RptParameter(RPTPARAMETER_Info objPara)
        {
            string strSql = "update RPTPARAMETER set RPTNAME=:pRPTNAME, CRLNAME=:pCRLNAME, " + 
                            "CRLTYPE=:pCRLTYPE, CRLLENGH=:pCRLLENGH, CAPTION=:pCAPTION,"+
                            " SQL=:pSQL,LSTORD=:pLSTORD,OPTALL=:pOPTALL WHERE ID=:pID";
            OracleParameter[] oraParam = {new OracleParameter("pID", OracleType.Number  , 19),
                                         new OracleParameter("pRPTNAME", OracleType.VarChar  , 12),
                                         new OracleParameter("pCRLNAME", OracleType.VarChar , 20),                                         
                                         new OracleParameter("pCRLTYPE", OracleType.VarChar , 10),
                                         new OracleParameter("pCRLLENGH", OracleType.Number  , 5),
                                         new OracleParameter("pCAPTION", OracleType.VarChar , 255),
                                         new OracleParameter("pSQL", OracleType.VarChar  ,2000),
                                         new OracleParameter("pLSTORD", OracleType.Number  , 2),
                                         new OracleParameter("pOPTALL", OracleType.Number  , 2)
                                         };
            oraParam[0].Value = objPara.ID;
            oraParam[1].Value = objPara.RPTNAME;
            oraParam[2].Value = objPara.CRLNAME;
            oraParam[3].Value = objPara.CRLTYPE;
            oraParam[4].Value = objPara.CRLLENGH;
            oraParam[5].Value = objPara.CAPTION;
            oraParam[6].Value = objPara.SQL;
            oraParam[7].Value = objPara.LSTORD;
            if (objPara.OPTALL.ToString() == "")
                oraParam[8].Value = 0;
            else
                oraParam[8].Value = objPara.OPTALL;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                oraConn.Dispose();
                throw (ex);

            }
        }

        ///////////////////////////////////////////////////////////
        // Mo ta:       Xoa mot tham so bao cao
        // Tham so:     objPara: Doi tuong tham so bao cao      
        // Tra ve:      Bang -1 :Not successfull
        //              
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        public int Delete_RptParameter(RPTPARAMETER_Info objPara)
        {
            string strSql = "Delete RPTPARAMETER WHERE ID=:pID";
            OracleParameter[] oraParam = {new OracleParameter("pID", OracleType.Number  , 19)
                                         };
            oraParam[0].Value = objPara.ID;            

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                oraConn.Dispose();
                throw (ex);

            }
        }
    }
}
