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
        public static DataTable GetAvailStates(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_AVAIL_STATES__GET;

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

        public static void AddCompanySpcfcStates(int? companyId, int? serviceId, string originState, string destStates)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_SPCFC_STATES_ADD;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramOriginState = new SqlParameter("originState", originState);
                SqlParameter paramDestStates = new SqlParameter("destStates", destStates);
                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramOriginState);
                _cmd.Parameters.Add(paramDestStates);

                int i = _cmd.ExecuteNonQuery();

            }
        }

        public static void DeleteCompanySpcfcStates(int? companyId, int? serviceId, string originState, string destStates)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY__SPCFC_STATES_DELETE;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramOriginState = new SqlParameter("originState", originState);
                SqlParameter paramDestStates = new SqlParameter("destStates", destStates);
                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramOriginState);
                _cmd.Parameters.Add(paramDestStates);

                int i = _cmd.ExecuteNonQuery();

            }
        }
        public static DataTable GetCompanySpcfcStates(int? companyId, int? serviceId, string originState, bool IsoriginState)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY__SPCFC_STATES_GET;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.IfServiceNullLocal());
                SqlParameter paramOriginState = new SqlParameter("orgState", originState);
                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramOriginState);
                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (IsoriginState)
                    return ds.Tables[1];
                else
                    return ds.Tables[0];

            }
        }
    }
}