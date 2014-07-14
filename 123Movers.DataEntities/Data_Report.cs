using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.Models;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static DataTable GetReports(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = "usp_GetCompanyPricePerLead";
               
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.ToString().IntNullOrEmpty());

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