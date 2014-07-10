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
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetMoveSizeLookup";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramService);

                //DataTable dtResults = new DataTable();

                //SqlDataReader drResults = cmd.ExecuteReader();
                //dtResults.Load(drResults);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

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
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveMoveWeight";

                SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
                SqlParameter paramService = new SqlParameter("serviceId", model.ServiceId);
                SqlParameter parammin = new SqlParameter("minMoveWeight", model.MinMoveWeightSeq);
                SqlParameter parammax = new SqlParameter("maxMoveWeight", model.MaxMoveWeightSeq);

                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramService);
                cmd.Parameters.Add(parammin);
                cmd.Parameters.Add(parammax);


                i = cmd.ExecuteNonQuery();


            }
            return true;
        }
    }
}