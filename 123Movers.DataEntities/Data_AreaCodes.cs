using _123Movers.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _123Movers.DataEntities
{
    public partial class DataLayer 
    {
        public static List<List<string>> GetAvailableAreas(int? companyId, int? serviceId)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_AVAILABLE_AREAS
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return ConfigValues.TableToList(dtResults); 
            }
        }

        public static DataTable GetCompanyAreasWithPrices(int? companyId, int? serviceId)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_COMPANY_STATE_AREACODE_PRICE
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }

        public static void AddCompanyAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_AREACODE_ADD
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("areacodes", areaCodes);

                _cmd.ExecuteNonQuery();
            }
        }


        public static void DeleteCompanyAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_AREACODE_DELETE
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("areacodes", areaCodes);

                 _cmd.ExecuteNonQuery();

            }
        }

        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightId)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandText = Constants.SP_ADD_COMPANY_PRICE_PERLEAD
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("areacodes", areaCodes);
                _cmd.Parameters.AddWithValue("moveWeightID", moveWeightId);

                _cmd.ExecuteNonQuery();
            }
            return true;
        }
       
    }
}