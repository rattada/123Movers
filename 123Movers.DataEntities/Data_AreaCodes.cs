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
        public static DataTable GetAvailableAreas(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetAvailableAreas.CommandText = "usp_availableAreas"; //"usp_availableAreacoded";
                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetAvailableAreas.Parameters.Add(paramCompanyId);
                cmdGetAvailableAreas.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetAvailableAreas.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }

        public static DataTable GetCompanyAreasWithPrices(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompanyAdByArea = new SqlCommand();
                cmdGetCompanyAdByArea.Connection = dbCon;
                cmdGetCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetCompanyAdByArea.CommandText = "usp_GetCompanyStateAreacodePrice"; //"usp_getCompanyStateAreacode";
                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdGetCompanyAdByArea.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyAdByArea.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }

        public static void AddCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAdByArea.CommandText = "up_companyAreacodeAdd";//"usp_companyAdByAreaAdd";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areacode", areaCode);

                cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdAddCompanyAdByArea.Parameters.Add(paramService);
                cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);


                var i = cmdAddCompanyAdByArea.ExecuteNonQuery();

            }
        }

        public static void DeleteCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdDeleteCompanyAdByArea = new SqlCommand();
                cmdDeleteCompanyAdByArea.Connection = dbCon;
                cmdDeleteCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdDeleteCompanyAdByArea.CommandText = "up_companyAreacodeDelete";//"usp_companyAdByAreaDelete";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areacode", areaCode);

                cmdDeleteCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdDeleteCompanyAdByArea.Parameters.Add(paramService);
                cmdDeleteCompanyAdByArea.Parameters.Add(paramAreaCode);


                var i = cmdDeleteCompanyAdByArea.ExecuteNonQuery();

            }
        }

        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightID)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAdByArea.CommandText = "usp_AddCompanyPricePerLead";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areacodes", areaCodes);
                SqlParameter paramMoveWeight = new SqlParameter("moveWeightID", moveWeightID);

                cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdAddCompanyAdByArea.Parameters.Add(paramService);
                cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);
                cmdAddCompanyAdByArea.Parameters.Add(paramMoveWeight);


                i = cmdAddCompanyAdByArea.ExecuteNonQuery();


            }
            return true;
        }
    }
}