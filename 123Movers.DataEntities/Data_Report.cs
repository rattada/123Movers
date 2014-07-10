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
        public static DataTable GetReports(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompanyPricePerLead = new SqlCommand();
                cmdGetCompanyPricePerLead.Connection = dbCon;
                cmdGetCompanyPricePerLead.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetCompanyPricePerLead.CommandText = "usp_GetCompanyPricePerLead";
                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyPricePerLead.Parameters.Add(paramCompanyId);
                cmdGetCompanyPricePerLead.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyPricePerLead.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
    }
}