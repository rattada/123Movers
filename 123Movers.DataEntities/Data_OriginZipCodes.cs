using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static DataTable GetAvailableZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_AVAILABLE_AREA_ORIGIN_ZIPCODES;

                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;
            }
        }
        public static DataTable GetCompanyAreasZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_AREA_ORIGIN_ZIPCODES; 
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.IfServiceNullLocal());
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;
            }
        }
        public static bool AddCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_ADD_COMPANY_AREA_ORIGIN_ZIPCODES;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceId", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                SqlParameter paramzipCodes = new SqlParameter("zipCodes", zipCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramzipCodes);

                 _cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static bool DeleteCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_DELETE_COMPANY_AREA_ORIGIN_ZIPCODES;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceId", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                SqlParameter paramzipCodes = new SqlParameter("zipCodes", zipCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramzipCodes);

               _cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static OriginZipCodeModel GetCompanyServiceAreaCodes(int? companyId, int? serviceId)
        {
            OriginZipCodeModel OriginAreaZip = new OriginZipCodeModel();
            List<OriginZipCodeModel> areacodes = new List<OriginZipCodeModel>();
            List<OriginZipCodeModel> zipcodes = new List<OriginZipCodeModel>();
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_SERVICE_AREACODES;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    OriginZipCodeModel s = new OriginZipCodeModel
                    {
                        CompanyId = row["companyID"].ToString().IntNullOrEmpty(),
                        ServiceId = row["serviceID"].ToString().IntNullOrEmpty(),
                        AreaCode = row["areaCode"].ToString().IntNullOrEmpty(),
                    };
                    areacodes.Add(s);
                }
                OriginAreaZip.OriginAreaCodes = areacodes;

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    OriginZipCodeModel s = new OriginZipCodeModel
                    {
                        CompanyId = row["companyID"].ToString().IntNullOrEmpty(),
                        ServiceId = row["serviceID"].ToString().IntNullOrEmpty(),
                        AreaCode = row["originAreaCode"].ToString().IntNullOrEmpty(),
                        ZipCode = row["originZipCode"].ToString().IntNullOrEmpty()
                    };
                    zipcodes.Add(s);
                }
                OriginAreaZip.OriginZipCodes = zipcodes;
            }
            return OriginAreaZip;
        }
    }
}