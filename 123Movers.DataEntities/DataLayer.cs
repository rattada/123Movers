using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using _123Movers.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        static SqlCommand _cmd;

        public static SqlConnection ConnectToDb()
        {
            string strCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection conDB = null;
            try
            {
                conDB = new SqlConnection(strCon);
                conDB.Open();
                return conDB;
            }
            catch (Exception)
            {

                return conDB;
            }
        }

        //public static bool Login(LoginModel login)
        //{


        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        SqlCommand cmdGetDetailsByCompanyName = new SqlCommand();
        //        cmdGetDetailsByCompanyName.Connection = dbCon;
        //        cmdGetDetailsByCompanyName.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdGetDetailsByCompanyName.CommandText = "usp_CheckUser";



        //        SqlParameter paramUserName = new SqlParameter("UserName", login.UserName);
        //        SqlParameter paramPassword = new SqlParameter("Password", login.Password);

        //        cmdGetDetailsByCompanyName.Parameters.Add(paramUserName);
        //        cmdGetDetailsByCompanyName.Parameters.Add(paramPassword);

        //        SqlDataReader dr = cmdGetDetailsByCompanyName.ExecuteReader();
        //        if (dr.Read())
        //        {

        //            ret = dr.GetBoolean(0);

        //        }
        //    }
        //    return ret;

        //}

        //public static void RegisterUser(RegisterModel model)
        //{

        //    using (SqlConnection dbCon = ConnectToDb())
        //    {

        //        SqlCommand cmdGetDetailsByCompanyName = new SqlCommand();
        //        cmdGetDetailsByCompanyName.Connection = dbCon;
        //        cmdGetDetailsByCompanyName.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdGetDetailsByCompanyName.CommandText = "usp_AddUser";



        //        SqlParameter paramUserName = new SqlParameter("UserName", model.UserName);
        //        SqlParameter paramPassword = new SqlParameter("Password", model.Password);
        //        SqlParameter paramEmail = new SqlParameter("EmailId", model.EmailID);

        //        cmdGetDetailsByCompanyName.Parameters.Add(paramUserName);
        //        cmdGetDetailsByCompanyName.Parameters.Add(paramPassword);
        //        cmdGetDetailsByCompanyName.Parameters.Add(paramEmail);


        //        //var parms = new IDataParameter[3];
        //        //int i = 0;
        //        //parms[i++] = NewParameter("UserName", SqlDbType.VarChar, model.UserName);
        //        //parms[i++] = NewParameter("Password", SqlDbType.VarChar, model.Password);
        //        //parms[i++] = NewParameter("EmailId", SqlDbType.VarChar, model.EmailID);

        //        //cmdGetDetailsByCompanyName.Parameters.Add(parms);

        //        var i = cmdGetDetailsByCompanyName.ExecuteNonQuery();

        //    }

        //}


        public static IEnumerable<SearchModel> SearchCompany(SearchModel search)
        {

            List<SearchModel> list = new List<SearchModel>();
            DataTable dtResults = new DataTable();
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompany = new SqlCommand();
                cmdGetCompany.Connection = dbCon;
                cmdGetCompany.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompany.CommandText = Constants.SP_COMPANY_SEARCH;

                SqlParameter paramCompanyId = new SqlParameter("companyID", search.CompanyId);
                SqlParameter paramCompanyName = new SqlParameter("companyName", search.CompanyName.TrimNullOrEmpty());
                SqlParameter paramAx = new SqlParameter("ax", search.AX.TrimNullOrEmpty());
               // SqlParameter paramInsertion = new SqlParameter("insertionOrderId", search.InsertionOrderId.TrimNullOrEmpty());

                cmdGetCompany.Parameters.Add(paramCompanyId);
                cmdGetCompany.Parameters.Add(paramCompanyName);
                cmdGetCompany.Parameters.Add(paramAx);
               // cmdGetCompany.Parameters.Add(paramInsertion);


               

                SqlDataReader drResults = cmdGetCompany.ExecuteReader();
                dtResults.Load(drResults);
            }

            foreach (DataRow row in dtResults.Rows)
            {
                SearchModel s = new SearchModel
                {
                    CompanyId = row["CompanyID"].ToString().IntNullOrEmpty(),
                    CompanyName = row["companyName"].ToString(),
                    AX = row["AbNumber"].ToString(),
                    // InsertionOrderId = row["insertionOrderId"].ToString(),
                    ContactPerson = row["contactPerson"].ToString(),
                    IsActive = row["isActive"].ToString().BooleanNullOrEmpty(),
                    Suspended = row["suspended"].ToString()

                };
                list.Add(s);
            }


            return list;

        }
        public static CompanyModel GetCompany(int? companyId)
        {
            CompanyModel _company = new CompanyModel();
            DataTable dtResults = new DataTable();
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompany = new SqlCommand();
                cmdGetCompany.Connection = dbCon;
                cmdGetCompany.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompany.CommandText = Constants.SP_COMPANY_SEARCH;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);

                cmdGetCompany.Parameters.Add(paramCompanyId);
                
                SqlDataReader drResults = cmdGetCompany.ExecuteReader();
                dtResults.Load(drResults);
            }

            foreach (DataRow row in dtResults.Rows)
            {
                _company = new CompanyModel
                {
                    CompanyId = row["CompanyID"].ToString().IntNullOrEmpty(),
                    CompanyName = row["companyName"].ToString(),
                    AX = row["AbNumber"].ToString(),
                    ContactPerson = row["contactPerson"].ToString(),
                    IsActive = row["isActive"].ToString().BooleanNullOrEmpty(),
                    Suspended = row["suspended"].ToString()
                };
            }
            return _company;
        }


    }

}