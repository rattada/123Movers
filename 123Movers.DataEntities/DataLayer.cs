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

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
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
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompany = new SqlCommand();
                cmdGetCompany.Connection = dbCon;
                cmdGetCompany.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetCompany.CommandText = "usp_GetCompanyDetails";
                cmdGetCompany.CommandText = "usp_companySearchv3";

                SqlParameter paramCompanyId = new SqlParameter("companyID", search.CompanyId);
                SqlParameter paramCompanyName = new SqlParameter("companyName", !string.IsNullOrWhiteSpace(search.CompanyName) ? search.CompanyName.Trim() : null);
                //SqlParameter paramAreacode = new SqlParameter("areaCode", search.AreaCodes);
                //SqlParameter paramAgreement = new SqlParameter("agreement", search.AgreementNumber);
                SqlParameter paramAx = new SqlParameter("ax", !string.IsNullOrWhiteSpace(search.AX) ? search.AX.Trim() : null);
                SqlParameter paramInsertion = new SqlParameter("insertionOrderId", !string.IsNullOrWhiteSpace(search.InsertionOrderId) ? search.InsertionOrderId.Trim() : null);

                cmdGetCompany.Parameters.Add(paramCompanyId);
                cmdGetCompany.Parameters.Add(paramCompanyName);
                //cmdGetCompany.Parameters.Add(paramAreacode);
               // cmdGetCompany.Parameters.Add(paramAgreement);
                cmdGetCompany.Parameters.Add(paramAx);
                cmdGetCompany.Parameters.Add(paramInsertion);


                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompany.ExecuteReader();
                dtResults.Load(drResults);

                foreach (DataRow row in dtResults.Rows)
                {
                     int? cid = 0;
                     bool isActive = false;

                     if (!string.IsNullOrEmpty(row["CompanyID"].ToString()))
                     {
                         cid = Convert.ToInt32(row["CompanyID"]);
                     }
                     if (!string.IsNullOrEmpty(row["isActive"].ToString()))
                     {
                         isActive = Convert.ToBoolean(row["isActive"]);
                     }
                     SearchModel s = new SearchModel
                     {
                      //  CompanyId = row[""],

                        CompanyId = cid,
                        CompanyName = row["companyName"].ToString(),
                        AX = row["AbNumber"].ToString(),
                        InsertionOrderId = row["insertionOrderId"].ToString(),
                        //DisplayName = row["displayName"].ToString(),
                       // CompanyHandle = row["companyHandle"].ToString(),
                        ContactPerson = row["contactPerson"].ToString(),
                        IsActive = isActive,
                        Suspended = row["suspended"].ToString()
                        //AgreementNumber = row["agreementNumber"].ToString(),
                       // AreaCodes = row["Area Code"].ToString()

                    };
                    list.Add(s);
                }

              
                return list;
            }


           
        }

      
      
       
        //public static DataTable GetCompanyPricePerLead(int? companyId, int? serviceId)
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        SqlCommand cmdGetCompanyPricePerLead = new SqlCommand();
        //        cmdGetCompanyPricePerLead.Connection = dbCon;
        //        cmdGetCompanyPricePerLead.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdGetCompanyPricePerLead.CommandText = "usp_GetCompanyPricePerLead";
        //        if (serviceId == null)
        //        {
        //            serviceId = 1009;
        //        }
        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);

        //        cmdGetCompanyPricePerLead.Parameters.Add(paramCompanyId);
        //        cmdGetCompanyPricePerLead.Parameters.Add(paramService);

        //        DataTable dtResults = new DataTable();

        //        SqlDataReader drResults = cmdGetCompanyPricePerLead.ExecuteReader();
        //        dtResults.Load(drResults);

        //        return dtResults;

        //    }
        //}
      
       
       
       


       
       

        
       

    }

}