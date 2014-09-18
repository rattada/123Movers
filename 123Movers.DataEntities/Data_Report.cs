using System.Data;
using System.Data.SqlClient;
using _123Movers.Models;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static DataTable GetReports(int? companyId, int? serviceId)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_AREACODE_DELETE
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.ToString().IntNullOrEmpty());

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
    }
}