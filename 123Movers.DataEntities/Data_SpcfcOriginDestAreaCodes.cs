using _123Movers.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static List<List<string>> GetAvailSpcfcOriginDestAreas(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_AVAIL_ORIGINDEST_AREACODE_GET
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return ConfigValues.TableToList(dtResults);
            }
        }
        public static void AddCompanySpcfcOriginDestAreaCodes(int? companyId, int? serviceId, int spcfcareacode, string areaCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_ADD
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);
                _cmd.Parameters.AddWithValue("originAreaCode", spcfcareacode);
                _cmd.Parameters.AddWithValue("destAreaCodes", areaCodes);

                _cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteCompanySpcfcOriginDestAreaCodes(int? companyId, int? serviceId, int spcfcareacode, string areaCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_DELETE
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);
                _cmd.Parameters.AddWithValue("destAreaCodes", areaCodes);
                _cmd.Parameters.AddWithValue("originAreaCode", spcfcareacode);
 
                _cmd.ExecuteNonQuery();
            }
        }
        public static List<List<string>> GetCompanySpcfcOriginDestAreas(int? companyId, int? serviceId, int spfcfAreaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_GET
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("areaCode", spfcfAreaCode);

                var dtResults = new DataTable();

                var da = new SqlDataAdapter(_cmd);

                var ds = new DataSet();
                da.Fill(ds);
                
                return ConfigValues.DataSetToList(ds);
            }
        }
    }
}