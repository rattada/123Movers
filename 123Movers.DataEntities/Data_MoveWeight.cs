using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static DataSet GetMoveWeights(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_MOVESIZE_LOOKUP;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                SqlDataAdapter da = new SqlDataAdapter(_cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }

        }
        public static bool SaveMoveWeight(MoveWeightModel model)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_ADD_COMPANY_MOVEWEIGHT;

                SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
                SqlParameter paramService = new SqlParameter("serviceId", model.ServiceId);
                SqlParameter parammin = new SqlParameter("minMoveWeight", model.MinMoveWeightSeq);
                SqlParameter parammax = new SqlParameter("maxMoveWeight", model.MaxMoveWeightSeq);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(parammin);
                _cmd.Parameters.Add(parammax);


                i = _cmd.ExecuteNonQuery();


            }
            return true;
        }
    }
}