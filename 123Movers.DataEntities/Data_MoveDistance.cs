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
        public static bool SaveMoveDistance(MoveDistanceModel model)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveMoveDistance";



                SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
                SqlParameter paramServiceID = new SqlParameter("serviceID", model.ServiceId);
                SqlParameter paramMinWeight = new SqlParameter("minMoveWeight", model.MinMoveDistance);
                SqlParameter paramMaxWeight = new SqlParameter("maxMoveWeight", model.MaxMoveDistance);


                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramServiceID);
                cmd.Parameters.Add(paramMinWeight);
                cmd.Parameters.Add(paramMaxWeight);



                i = cmd.ExecuteNonQuery();


            }
            return true;
        }


        public static DataTable GetCompanyMoveDistance(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompanyLeadLimit = new SqlCommand();
                cmdGetCompanyLeadLimit.Connection = dbCon;
                cmdGetCompanyLeadLimit.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompanyLeadLimit.CommandText = "usp_GetCompanyMoveDistance";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyLeadLimit.Parameters.Add(paramCompanyId);
                cmdGetCompanyLeadLimit.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyLeadLimit.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
    }
}