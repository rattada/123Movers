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
        public static DataTable GetAvailableZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetAvailableAreas.CommandText = "usp_GetCompanyAvailableAreasOrignZipCodes"; //"usp_availableAreacoded";
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
        public static DataTable GetCompanyAreasZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetAvailableAreas.CommandText = "usp_GetCompanyAreasZipCodes"; //"usp_availableAreacoded";
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
        public static bool AddCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAreaZipCodes = new SqlCommand();
                cmdAddCompanyAreaZipCodes.Connection = dbCon;
                cmdAddCompanyAreaZipCodes.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAreaZipCodes.CommandText = "usp_AddCompanyAreasOriginZipCodes";

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

        public static bool DeleteCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleteCompanyAreasOriginZipCodes";

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

        public static OriginZipCodeModel GetCompanyServiceAreaCodes(int? companyId, int? serviceId)
        {

            OriginZipCodeModel OriginAreaZip = new OriginZipCodeModel();
            List<OriginZipCodeModel> areacodes = new List<OriginZipCodeModel>();
            List<OriginZipCodeModel> zipcodes = new List<OriginZipCodeModel>();
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
                    //if (!string.IsNullOrEmpty(row["zipCode"].ToString()))
                    //{
                    //    zid = Convert.ToInt32(row["zipCode"]);
                    //}
                    OriginZipCodeModel s = new OriginZipCodeModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                        // ZipCode = zid
                    };
                    areacodes.Add(s);
                }
                OriginAreaZip.OriginAreaCodes = areacodes;


                foreach (DataRow row in ds.Tables[1].Rows)
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
                    if (!string.IsNullOrEmpty(row["originAreaCode"].ToString()))
                    {
                        areacode = Convert.ToInt32(row["originAreaCode"]);
                    }
                    if (!string.IsNullOrEmpty(row["originZipCode"].ToString().Trim()))
                    {
                        zid = Convert.ToInt32(row["originZipCode"]);
                    }
                    OriginZipCodeModel s = new OriginZipCodeModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                        ZipCode = zid
                    };
                    zipcodes.Add(s);
                }
                OriginAreaZip.OriginZipCodes = zipcodes;

            }
            return OriginAreaZip;
        }

        //public static bool AddCompanyZipCodesPerAreaCodes(int? companyId, int serviceId, string areaCodes, int IsOrigin)
        //{
        //    int i = 0;
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
        //        cmdAddCompanyAdByArea.Connection = dbCon;
        //        cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdAddCompanyAdByArea.CommandText = "usp_SaveCompanyOriginDestinationZipCodes";

        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);
        //        SqlParameter paramAreaCode = new SqlParameter("AreaCodes", areaCodes);
        //        SqlParameter paramIsOrigin = new SqlParameter("IsOrigin", IsOrigin);

        //        cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramService);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramIsOrigin);
        //        i = cmdAddCompanyAdByArea.ExecuteNonQuery();


        //    }
        //    return true;
        //}
    }
}