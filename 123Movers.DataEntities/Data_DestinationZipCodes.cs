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
        public static DestinationZipModel GetCompanyDestinationServiceAreaCodes(int? companyId, int? serviceId)
        {

            DestinationZipModel DestinationAreaZip = new DestinationZipModel();
            List<DestinationZipModel> areacodes = new List<DestinationZipModel>();
            List<DestinationZipModel> zipcodes = new List<DestinationZipModel>();
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
   
                    DestinationZipModel s = new DestinationZipModel
                    {
                        CompanyId = row["companyID"].ToString().IntNullOrEmpty(),
                        ServiceId = row["serviceID"].ToString().IntNullOrEmpty(),
                        AreaCode = row["areaCode"].ToString().IntNullOrEmpty(),
                    };
                    areacodes.Add(s);
                }
                DestinationAreaZip.DestinationAreaCodes = areacodes;


                foreach (DataRow row in ds.Tables[2].Rows)
                {

                    DestinationZipModel s = new DestinationZipModel
                    {
                        CompanyId = row["companyID"].ToString().IntNullOrEmpty(),
                        ServiceId = row["serviceID"].ToString().IntNullOrEmpty(),
                        AreaCode = row["destinationAreaCode"].ToString().IntNullOrEmpty(),
                        ZipCode = row["destinationZipCode"].ToString().IntNullOrEmpty()
                    };
                    zipcodes.Add(s);
                }
                DestinationAreaZip.DestinationZipCodes = zipcodes;

            }
            return DestinationAreaZip;
        }




        public static bool AddCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_ADD_COMPANY_AREADEST_ZIPCODES;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceId", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                SqlParameter paramzipCodes = new SqlParameter("zipCodes", zipCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramzipCodes);


                i = _cmd.ExecuteNonQuery();


            }
            return true;
        }


        public static bool DeleteCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_DELETE_COMPANY_AREADEST_ZIPCODES;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceId", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                SqlParameter paramzipCodes = new SqlParameter("zipCodes", zipCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramzipCodes);


                i = _cmd.ExecuteNonQuery();


            }
            return true;
        }



        public static DataTable GetAvailableDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;

                _cmd.CommandText = Constants.SP_GET_COMPANY_AVAILABLE_AREADEST_ZIPCODES; //"usp_availableAreacoded";

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
        public static DataTable GetCompanyAreasDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_AREADEST_ZIPCODES; 
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
    }
}