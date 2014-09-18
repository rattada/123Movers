using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using _123Movers.Models;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        static SqlCommand _cmd;

        public static SqlConnection ConnectToDb()
        {
            string strCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection conDb = null;
            try
            {
                conDb = new SqlConnection(strCon);
                conDb.Open();
                return conDb;
            }
            catch (Exception)
            {
                return conDb;
            }
        }

        public static IEnumerable<SearchModel> SearchCompany(SearchModel search)
        {
            var list = new List<SearchModel>();
            var dtResults = new DataTable();
            using (var dbCon = ConnectToDb())
            {
               _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SEARCH
                    };

                _cmd.Parameters.AddWithValue("companyID", search.CompanyId);
                _cmd.Parameters.AddWithValue("companyName", search.CompanyName.TrimNullOrEmpty());
                _cmd.Parameters.AddWithValue("ax", search.Ax.TrimNullOrEmpty());

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);
            }

            list.AddRange(from DataRow row in dtResults.Rows
                              select new SearchModel()
                                  {
                                      CompanyId = row["CompanyID"].ToString().IntNullOrEmpty(),
                                      CompanyName = row["companyName"].ToString(),
                                      Ax = row["AbNumber"].ToString(),
                                      ContactPerson = row["contactPerson"].ToString(),
                                      IsActive = row["isActive"].ToString().BooleanNullOrEmpty(),
                                      Suspended = row["suspended"].ToString()
                                  });
            return list;

        }
        public static CompanyModel GetCompany(int? companyId)
        {
            var company = new CompanyModel();
            var dtResults = new DataTable();
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SEARCH
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);
            }

            if (dtResults.Rows.Count > 0)
            {
                company = new CompanyModel
                {
                    CompanyId = dtResults.Rows[0]["CompanyID"].ToString().IntNullOrEmpty(),
                    CompanyName = dtResults.Rows[0]["companyName"].ToString(),
                    Ax = dtResults.Rows[0]["AbNumber"].ToString(),
                    ContactPerson = dtResults.Rows[0]["contactPerson"].ToString(),
                    IsActive = dtResults.Rows[0]["isActive"].ToString().BooleanNullOrEmpty(),
                    Suspended = dtResults.Rows[0]["suspended"].ToString()
                };
            }

            return company;
        }
    }
}