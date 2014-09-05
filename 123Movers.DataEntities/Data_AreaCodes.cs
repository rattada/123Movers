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
        public static List<List<string>> GetAvailableAreas(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_AVAILABLE_AREAS; 
                                
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.IfServiceNullLocal());

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                return ConfigValues.TableToList(dtResults); ;

            }
        }

        public static DataTable GetCompanyAreasWithPrices(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_STATE_AREACODE_PRICE; 
               
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

        //public static void AddCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        _cmd = new SqlCommand();
        //        _cmd.Connection = dbCon;
        //        _cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        _cmd.CommandText = Constants.SP_COMPANY_AREACODE_ADD;

        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);
        //        SqlParameter paramAreaCode = new SqlParameter("areacode", areaCode);

        //        _cmd.Parameters.Add(paramCompanyId);
        //        _cmd.Parameters.Add(paramService);
        //        _cmd.Parameters.Add(paramAreaCode);


        //        var i = _cmd.ExecuteNonQuery();

        //    }
        //}
        public static void AddCompanyAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            //using (MoversDBEntities db = new MoversDBEntities())
            //{
            //    var arrayAreas = areaCodes.Split(',');

            //    foreach (var areaCode in arrayAreas)
            //    {
            //        db.tbl_companyAreacode.AddObject(new tbl_companyAreacode {companyID = companyId.GetValueOrDefault(), serviceID = serviceId.GetValueOrDefault(), areaCode = short.Parse(areaCode), status = "queue" });
            //    }
            //    db.SaveChanges();
            //}

            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_AREACODE_ADD;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areacodes", areaCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);


                var i = _cmd.ExecuteNonQuery();

            }
        }

        //public static void DeleteCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        _cmd = new SqlCommand();
        //        _cmd.Connection = dbCon;
        //        _cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        _cmd.CommandText = Constants.SP_COMPANY_AREACODE_DELETE;

        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);
        //        SqlParameter paramAreaCode = new SqlParameter("areacode", areaCode);

        //        _cmd.Parameters.Add(paramCompanyId);
        //        _cmd.Parameters.Add(paramService);
        //        _cmd.Parameters.Add(paramAreaCode);


        //        var i = _cmd.ExecuteNonQuery();

        //    }
        //}

        public static void DeleteCompanyAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_COMPANY_AREACODE_DELETE;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCodes", areaCodes);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);


                var i = _cmd.ExecuteNonQuery();

            }
        }

        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightID)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_ADD_COMPANY_PRICE_PERLEAD;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areacodes", areaCodes);
                SqlParameter paramMoveWeight = new SqlParameter("moveWeightID", moveWeightID);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramMoveWeight);


                i = _cmd.ExecuteNonQuery();


            }
            return true;
        }
       
    }
}