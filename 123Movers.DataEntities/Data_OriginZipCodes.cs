using System.Linq;
using _123Movers.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static DataTable GetAvailableZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_COMPANY_AVAILABLE_AREA_ORIGIN_ZIPCODES
                    };

                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);
                _cmd.Parameters.AddWithValue("areaCode", areaCode);

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;
            }
        }
        public static DataTable GetCompanyAreasZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_COMPANY_AREA_ORIGIN_ZIPCODES
                    };
                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("areaCode", areaCode);

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;
            }
        }
        public static bool AddCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_ADD_COMPANY_AREA_ORIGIN_ZIPCODES
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceId", serviceId);
                _cmd.Parameters.AddWithValue("areaCode", areaCode);
                _cmd.Parameters.AddWithValue("zipCodes", zipCodes);

                _cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static bool DeleteCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_DELETE_COMPANY_AREA_ORIGIN_ZIPCODES
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceId", serviceId);
                _cmd.Parameters.AddWithValue("areaCode", areaCode);
                _cmd.Parameters.AddWithValue("zipCodes", zipCodes);

               _cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static OriginZipCodeModel GetCompanyServiceAreaCodes(int? companyId, int? serviceId)
        {
            var originAreaZip = new OriginZipCodeModel();
            var areacodes = new List<OriginZipCodeModel>();
            var zipcodes = new List<OriginZipCodeModel>();
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_COMPANY_SERVICE_AREACODES
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);
               
                var dtResults = new DataTable();

                var da = new SqlDataAdapter(_cmd);

                var ds = new DataSet();
                da.Fill(ds);

                areacodes.AddRange(from DataRow row in ds.Tables[0].Rows
                                   select new OriginZipCodeModel
                                       {
                                           CompanyId = row["companyID"].ToString().IntNullOrEmpty(), 
                                           ServiceId = row["serviceID"].ToString().IntNullOrEmpty(), 
                                           AreaCode = row["areaCode"].ToString().IntNullOrEmpty(),
                                       });
                originAreaZip.OriginAreaCodes = areacodes;

                zipcodes.AddRange(from DataRow row in ds.Tables[1].Rows
                                  select new OriginZipCodeModel()
                                      {
                                          CompanyId = row["companyID"].ToString().IntNullOrEmpty(),
                                          ServiceId = row["serviceID"].ToString().IntNullOrEmpty(),
                                          AreaCode = row["originAreaCode"].ToString().IntNullOrEmpty(),
                                          ZipCode = row["originZipCode"].ToString().IntNullOrEmpty()
                                      });

                originAreaZip.OriginZipCodes = zipcodes;
            }
            return originAreaZip;
        }
    }
}