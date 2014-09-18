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
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_AVAIL_STATES__GET
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;
            }
        }

        public static void AddCompanySpcfcStates(int? companyId, int? serviceId, string originState, string destStates)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType =CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_STATES_ADD
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);
                _cmd.Parameters.AddWithValue("originState", originState);
                _cmd.Parameters.AddWithValue("destStates", destStates);
               
                _cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCompanySpcfcStates(int? companyId, int? serviceId, string originState, string destStates)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_STATES_DELETE
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);
                _cmd.Parameters.AddWithValue("originState", originState);
                _cmd.Parameters.AddWithValue("destStates", destStates);

                _cmd.ExecuteNonQuery();
            }
        }
        public static List<List<string>> GetCompanySpcfcStates(int? companyId, int? serviceId, string originState, bool isoriginState)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_COMPANY_SPCFC_STATES_GET
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("orgState", originState);
               
                var da = new SqlDataAdapter(_cmd);
                var ds = new DataSet();
                da.Fill(ds);

                return ConfigValues.DataSetToList(ds);
            }
        }
    }
}