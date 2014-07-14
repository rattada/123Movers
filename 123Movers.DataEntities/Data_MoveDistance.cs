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
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_ADD_COMPANY_MOVEDISTANCE;



                SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
                SqlParameter paramServiceID = new SqlParameter("serviceID", model.ServiceId);
                SqlParameter paramMinWeight = new SqlParameter("minMoveWeight", model.MinMoveDistance);
                SqlParameter paramMaxWeight = new SqlParameter("maxMoveWeight", model.MaxMoveDistance);


                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramServiceID);
                _cmd.Parameters.Add(paramMinWeight);
                _cmd.Parameters.Add(paramMaxWeight);



                i = _cmd.ExecuteNonQuery();


            }
            return true;
        }


        public static DataTable GetCompanyMoveDistance(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_MOVEDISTANCE;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
    }
}