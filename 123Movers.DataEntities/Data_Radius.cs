using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace _123Movers.DataEntities
{
    public  partial class DataLayer
    {
        public static void  AddZipCodesByRadius(int? companyId, int? serviceId, int zipcode, decimal radius, string category, string type)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_ZIPCODES_BY_RADIUS_ADD;
                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("zipCode", zipcode);
                _cmd.Parameters.AddWithValue("radius", radius);
                _cmd.Parameters.AddWithValue("category", category);

                //SqlParameter precords = new SqlParameter();
                //precords.ParameterName = "records";
                //precords.DbType = DbType.Int32;
                //precords.Direction = ParameterDirection.Output;
                //_cmd.Parameters.Add(precords);

                _cmd.ExecuteNonQuery();
            }
        }

        public static List<List<string>> GetZipCodesByRadius(int? companyId, int? serviceId, int zipcode, decimal radius, string category)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_ZIPCODES_BY_RADIUS_GET;
                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());
                _cmd.Parameters.AddWithValue("zipCode", zipcode);
                _cmd.Parameters.AddWithValue("radius", radius);
                _cmd.Parameters.AddWithValue("category", category);

                DataTable dtResults = new DataTable();
                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return ConfigValues.TableToList(dtResults);
            }
        }
    }
}