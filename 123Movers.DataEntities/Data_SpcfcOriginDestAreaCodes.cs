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
        public static DataTable GetAvailSpcfcOriginDestAreas(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY__SPCFC_AVAIL_ORIGINDEST_AREACODE_GET; 
                                
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.IfServiceNullLocal());

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
        public static void AddCompanySpcfcOriginDestAreaCodes(int? companyId, int? serviceId, int spcfcareacode, string areaCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_ADD;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramSpecificAreaCode = new SqlParameter("originAreaCode", spcfcareacode);
                SqlParameter paramAreaCode = new SqlParameter("destAreaCodes", areaCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramSpecificAreaCode);


                int  i = _cmd.ExecuteNonQuery();

            }
        }
        public static void DeleteCompanySpcfcOriginDestAreaCodes(int? companyId, int? serviceId, int spcfcareacode, string areaCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY__SPCFC_ORIGINDEST_AREACODE_DELETE;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("destAreaCodes", areaCodes);
                SqlParameter paramSpecificAreaCode = new SqlParameter("originAreaCode", spcfcareacode);
                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramSpecificAreaCode);

                int  i = _cmd.ExecuteNonQuery();

            }
        }
        public static List<List<string>> GetCompanySpcfcOriginDestAreas(int? companyId, int? serviceId, int spfcfAreaCode, bool originAreaCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY__SPCFC_ORIGINDEST_AREACODE_GET;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.IfServiceNullLocal());
                SqlParameter paramSpcfcAreacode = new SqlParameter("areaCode", spfcfAreaCode);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramSpcfcAreacode);

                DataTable dtResults = new DataTable();

                //SqlDataReader drResults = _cmd.ExecuteReader();
                //dtResults.Load(drResults);
                SqlDataAdapter da = new SqlDataAdapter(_cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                
                return ConfigValues.DataSetToList(ds);
                //if (originAreaCodes)
                //    return ds.Tables[1];
                //else
                //    return ds.Tables[0];

            }
        }
    }
}