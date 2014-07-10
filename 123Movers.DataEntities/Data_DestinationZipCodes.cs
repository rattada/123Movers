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
                SqlCommand cmdGetCompanyServiceAreaCodes = new SqlCommand();
                cmdGetCompanyServiceAreaCodes.Connection = dbCon;
                cmdGetCompanyServiceAreaCodes.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompanyServiceAreaCodes.CommandText = "usp_GetCompanyServiceAreaCodes";


                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyServiceAreaCodes.Parameters.Add(paramCompanyId);
                cmdGetCompanyServiceAreaCodes.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmdGetCompanyServiceAreaCodes);

                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int? sid = null;
                    int? areacode = null;
                    int? cid = null;
                    //int? zid = null;

                    if (!string.IsNullOrEmpty(row["companyID"].ToString()))
                    {
                        cid = Convert.ToInt32(row["companyID"]);
                    }

                    if (!string.IsNullOrEmpty(row["serviceID"].ToString()))
                    {
                        sid = Convert.ToInt32(row["serviceID"]);
                    }
                    if (!string.IsNullOrEmpty(row["areaCode"].ToString()))
                    {
                        areacode = Convert.ToInt32(row["areaCode"]);
                    }

                    DestinationZipModel s = new DestinationZipModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                    };
                    areacodes.Add(s);
                }
                DestinationAreaZip.DestinationAreaCodes = areacodes;


                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    int? sid = null;
                    int? areacode = null;
                    int? cid = null;
                    int? zid = null;

                    if (!string.IsNullOrEmpty(row["companyID"].ToString()))
                    {
                        cid = Convert.ToInt32(row["companyID"]);
                    }
                    if (!string.IsNullOrEmpty(row["serviceID"].ToString()))
                    {
                        sid = Convert.ToInt32(row["serviceID"]);
                    }
                    if (!string.IsNullOrEmpty(row["destinationAreaCode"].ToString()))
                    {
                        areacode = Convert.ToInt32(row["destinationAreaCode"]);
                    }
                    if (!string.IsNullOrEmpty(row["destinationZipCode"].ToString().Trim()))
                    {
                        zid = Convert.ToInt32(row["destinationZipCode"]);
                    }
                    DestinationZipModel s = new DestinationZipModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                        ZipCode = zid
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
                SqlCommand cmdAddCompanyAreaZipCodes = new SqlCommand();
                cmdAddCompanyAreaZipCodes.Connection = dbCon;
                cmdAddCompanyAreaZipCodes.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAreaZipCodes.CommandText = "usp_AddCompanyAreasDestZipCodes";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceId", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                SqlParameter paramzipCodes = new SqlParameter("zipCodes", zipCodes);

                cmdAddCompanyAreaZipCodes.Parameters.Add(paramCompanyId);
                cmdAddCompanyAreaZipCodes.Parameters.Add(paramService);
                cmdAddCompanyAreaZipCodes.Parameters.Add(paramAreaCode);
                cmdAddCompanyAreaZipCodes.Parameters.Add(paramzipCodes);


                i = cmdAddCompanyAreaZipCodes.ExecuteNonQuery();


            }
            return true;
        }


        public static bool DeleteCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleteCompanyAreasDestZipCodes";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceId", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                SqlParameter paramzipCodes = new SqlParameter("zipCodes", zipCodes);

                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramService);
                cmd.Parameters.Add(paramAreaCode);
                cmd.Parameters.Add(paramzipCodes);


                i = cmd.ExecuteNonQuery();


            }
            return true;
        }



        public static DataTable GetAvailableDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetAvailableAreas.CommandText = "usp_GetCompanyAvailableAreasDestincationZipCodes"; //"usp_availableAreacoded";
                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);

                cmdGetAvailableAreas.Parameters.Add(paramCompanyId);
                cmdGetAvailableAreas.Parameters.Add(paramService);
                cmdGetAvailableAreas.Parameters.Add(paramAreaCode);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetAvailableAreas.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
        public static DataTable GetCompanyAreasDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetAvailableAreas.CommandText = "usp_GetCompanyAreasDestinationZipCodes"; //"usp_availableAreacoded";
                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);

                cmdGetAvailableAreas.Parameters.Add(paramCompanyId);
                cmdGetAvailableAreas.Parameters.Add(paramService);
                cmdGetAvailableAreas.Parameters.Add(paramAreaCode);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetAvailableAreas.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
    }
}