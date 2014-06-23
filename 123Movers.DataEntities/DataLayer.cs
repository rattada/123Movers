using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.Models;
using _123Movers.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace _123Movers.DataEntities
{
    public static class DataLayer
    {
        static bool ret = false;
        static readonly string DBConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        static SqlConnection ConnectToDb(string strCon)
        {

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

        public static bool Login(LoginModel login)
        {


            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdGetDetailsByCompanyName = new SqlCommand();
                cmdGetDetailsByCompanyName.Connection = dbCon;
                cmdGetDetailsByCompanyName.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetDetailsByCompanyName.CommandText = "usp_CheckUser";



                SqlParameter paramUserName = new SqlParameter("UserName", login.UserName);
                SqlParameter paramPassword = new SqlParameter("Password", login.Password);

                cmdGetDetailsByCompanyName.Parameters.Add(paramUserName);
                cmdGetDetailsByCompanyName.Parameters.Add(paramPassword);

                SqlDataReader dr = cmdGetDetailsByCompanyName.ExecuteReader();
                if (dr.Read())
                {

                    ret = dr.GetBoolean(0);

                }
            }
            return ret;

        }

        public static void RegisterUser(RegisterModel model)
        {

            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {

                SqlCommand cmdGetDetailsByCompanyName = new SqlCommand();
                cmdGetDetailsByCompanyName.Connection = dbCon;
                cmdGetDetailsByCompanyName.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetDetailsByCompanyName.CommandText = "usp_AddUser";



                SqlParameter paramUserName = new SqlParameter("UserName", model.UserName);
                SqlParameter paramPassword = new SqlParameter("Password", model.Password);
                SqlParameter paramEmail = new SqlParameter("EmailId", model.EmailID);

                cmdGetDetailsByCompanyName.Parameters.Add(paramUserName);
                cmdGetDetailsByCompanyName.Parameters.Add(paramPassword);
                cmdGetDetailsByCompanyName.Parameters.Add(paramEmail);


                //var parms = new IDataParameter[3];
                //int i = 0;
                //parms[i++] = NewParameter("UserName", SqlDbType.VarChar, model.UserName);
                //parms[i++] = NewParameter("Password", SqlDbType.VarChar, model.Password);
                //parms[i++] = NewParameter("EmailId", SqlDbType.VarChar, model.EmailID);

                //cmdGetDetailsByCompanyName.Parameters.Add(parms);

                var i = cmdGetDetailsByCompanyName.ExecuteNonQuery();

            }

        }

        public static bool SaveBudget(BudgetModel budget)
        {
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdSaveBudget = new SqlCommand();
                cmdSaveBudget.Connection = dbCon;
                cmdSaveBudget.CommandType = System.Data.CommandType.StoredProcedure;
                cmdSaveBudget.CommandText = "usp_SaveBudget";



                SqlParameter paramCompanyId = new SqlParameter("companyID", budget.CompanyId);
                //SqlParameter paramCompanyName = new SqlParameter("companyName", budget.CompanyName);
                //SqlParameter paramCompanyHandle = new SqlParameter("companyHandle", budget.CompanyHandle);
               // SqlParameter paramActive = new SqlParameter("isActive", budget.IsActive);
                SqlParameter paramTotalBudget = new SqlParameter("totalBudget", budget.TotalBudget);
                SqlParameter paramRemainingBudget = new SqlParameter("remainingBudget", budget.RemainingBudget);
                SqlParameter paramBudgetAction = new SqlParameter("budgetAction", budget.BudgetAction);
                SqlParameter paramRecurring = new SqlParameter("isRecurring", budget.IsRecurring);
                SqlParameter paramRequireNoticeToCharge = new SqlParameter("isRequireNoticeToCharge", budget.IsRequireNoticeToCharge);
                SqlParameter paramAgreementNumber = new SqlParameter("agreementNumber", budget.AgreementNumber);
                SqlParameter paramMinCharge = new SqlParameter("minDaysToCharge", budget.MinDaysToCharge);
                SqlParameter paramServices = new SqlParameter("service", budget.Services);
                SqlParameter paramType = new SqlParameter("type", budget.Type);


                cmdSaveBudget.Parameters.Add(paramCompanyId);
                //cmdSaveBudget.Parameters.Add(paramCompanyName);
                //cmdSaveBudget.Parameters.Add(paramCompanyHandle);
                //cmdSaveBudget.Parameters.Add(paramActive);
                cmdSaveBudget.Parameters.Add(paramTotalBudget);
                cmdSaveBudget.Parameters.Add(paramRemainingBudget);
                cmdSaveBudget.Parameters.Add(paramBudgetAction);
                cmdSaveBudget.Parameters.Add(paramRecurring);
                cmdSaveBudget.Parameters.Add(paramRequireNoticeToCharge);
                cmdSaveBudget.Parameters.Add(paramAgreementNumber);
                cmdSaveBudget.Parameters.Add(paramMinCharge);
                cmdSaveBudget.Parameters.Add(paramServices);
                cmdSaveBudget.Parameters.Add(paramType);

                SqlDataReader dr = cmdSaveBudget.ExecuteReader();
                if (dr.Read())
                {

                    ret = dr.GetBoolean(0);

                }
            }
            return ret;

        }

        public static List<BudgetModel> GetBudget(string companyid)
        {
            List<BudgetModel> list = new List<BudgetModel>();
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdGetCompany = new SqlCommand();
                cmdGetCompany.Connection = dbCon;
                cmdGetCompany.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompany.CommandText = "usp_GetCompanyBudget";


                SqlParameter paramCompanyId = new SqlParameter("companyID", companyid);
             

                cmdGetCompany.Parameters.Add(paramCompanyId);
     


                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompany.ExecuteReader();
                dtResults.Load(drResults);

                foreach (DataRow row in dtResults.Rows)
                {
                    int? cid = null;
                    int? sid = null;
                    int? dcharge = null;
                    DateTime? sdate = null;
                    DateTime? edate = null;
                    decimal? tbudget = null;
                    decimal? rbudget = null;
                    decimal? uAmount = null;
                    bool recurring = false;
                    bool notice = false;

                    if (!string.IsNullOrEmpty(row["CompanyID"].ToString()))
                    {
                        cid = Convert.ToInt32(row["CompanyID"]);
                    }

                    if (!string.IsNullOrEmpty(row["ServiceID"].ToString()))
                    {
                        sid = Convert.ToInt32(row["ServiceID"]);
                    }

                    if (!string.IsNullOrEmpty(row["minDaysToCharge"].ToString()))
                    {
                        dcharge = Convert.ToInt32(row["minDaysToCharge"]);
                    }

                    if (!string.IsNullOrEmpty(row["Budget Start Date"].ToString()))
                    {
                        sdate = Convert.ToDateTime(row["Budget Start Date"]).Date;
                    }

                    if (!string.IsNullOrEmpty(row["Budget End Date"].ToString()))
                    {
                        edate = Convert.ToDateTime(row["Budget End Date"]).Date;
                    }
                    if (!string.IsNullOrEmpty(row["Total Budget"].ToString()))
                    {
                        tbudget = Convert.ToDecimal(row["Total Budget"]);
                    }
                    if (!string.IsNullOrEmpty(row["Remaining Budget"].ToString()))
                    {
                        rbudget = Convert.ToDecimal(row["Remaining Budget"]);
                    }
                    if (!string.IsNullOrEmpty(row["Uncharged Amount"].ToString()))
                    {
                        uAmount = Convert.ToDecimal(row["Uncharged Amount"]);
                    }
                    if (!string.IsNullOrEmpty(row["IsReccurring"].ToString()))
                    {
                        recurring = Convert.ToBoolean(row["IsReccurring"]);
                    }

                    if (!string.IsNullOrEmpty(row["IsRequireNoticeToCharge"].ToString()))
                    {
                        notice = Convert.ToBoolean(row["IsRequireNoticeToCharge"]);
                    }

                    BudgetModel s = new BudgetModel
                    {

                        CompanyId = cid,
                        CompanyName = row["Company Name"].ToString(),
                        AX = row["AX Number"].ToString(),
                        InsertionOrderId = row["Budget Insertion ID"].ToString(),
                        AgreementNumber = row["agreementNumber"].ToString(),
                        AreaCodes = row["Area Code"].ToString(),
                        StartDate = sdate,
                        EndDate = edate,
                        TotalBudget = tbudget,
                        RemainingBudget = rbudget,
                        UnchargedAmount = uAmount,
                        ServiceId = sid,
                        MinDaysToCharge = dcharge,
                        IsRecurring = recurring,
                        IsRequireNoticeToCharge = notice,
                        ContactPerson = row["contactPerson"].ToString()
                    };
                    list.Add(s);
                }


                return list;
            }
        }

        public static IEnumerable<SearchModel> SearchCompany(SearchModel search)
        {
            List<SearchModel> list = new List<SearchModel>();
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdGetCompany = new SqlCommand();
                cmdGetCompany.Connection = dbCon;
                cmdGetCompany.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetCompany.CommandText = "usp_GetCompanyDetails";
                cmdGetCompany.CommandText = "usp_companySearchv3";

                SqlParameter paramCompanyId = new SqlParameter("companyID", search.CompanyId);
                SqlParameter paramCompanyName = new SqlParameter("companyName", search.CompanyName);
                //SqlParameter paramAreacode = new SqlParameter("areaCode", search.AreaCodes);
                //SqlParameter paramAgreement = new SqlParameter("agreement", search.AgreementNumber);
                SqlParameter paramAx = new SqlParameter("ax", search.AX);
                SqlParameter paramInsertion = new SqlParameter("insertionOrderId", search.InsertionOrderId);

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

        public static DataTable GetServices()
        {
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdGetService = new SqlCommand();
                cmdGetService.Connection = dbCon;
                cmdGetService.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetService.CommandText = "usp_getAreaCodesStates";

                SqlParameter paramType = new SqlParameter("queryType", 1);

                cmdGetService.Parameters.Add(paramType);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetService.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;
                
            }

        }
        public static DataTable GetAvailableAreas(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
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
        public static DataTable GetCompanyAdByArea(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdGetCompanyAdByArea = new SqlCommand();
                cmdGetCompanyAdByArea.Connection = dbCon;
                cmdGetCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetCompanyAdByArea.CommandText = "usp_getCompanyStateAreacodePrice"; //"usp_getCompanyStateAreacode";
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
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
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
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
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

        //public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, int? areaCode, decimal? price, int? moveWeightID)
        //{
        //    int i = 0;
        //    using (SqlConnection dbCon = ConnectToDb(DBConnString))
        //    {
        //        SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
        //        cmdAddCompanyAdByArea.Connection = dbCon;
        //        cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdAddCompanyAdByArea.CommandText = "usp_AddCompanyPricePerLead1";

        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);
        //        SqlParameter paramAreaCode = new SqlParameter("areacode", areaCode);
        //        SqlParameter paramPrice = new SqlParameter("price", price);
        //        SqlParameter paramMoveWeight = new SqlParameter("moveWeightID", moveWeightID);

        //        cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramService);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramPrice);
        //        cmdAddCompanyAdByArea.Parameters.Add(paramMoveWeight);


        //        i = cmdAddCompanyAdByArea.ExecuteNonQuery();
               

        //    }
        //    return true;
        //}
        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightID)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAdByArea.CommandText = "usp_AddCompanyPricePerLead1";

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

        public static DataTable GetCompanyPricePerLead(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb(DBConnString))
            {
                SqlCommand cmdGetCompanyPricePerLead = new SqlCommand();
                cmdGetCompanyPricePerLead.Connection = dbCon;
                cmdGetCompanyPricePerLead.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompanyPricePerLead.CommandText = "usp_GetCompanyPricePerLead";
                if (serviceId == null)
                {
                    serviceId = 1009;
                }
                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyPricePerLead.Parameters.Add(paramCompanyId);
                cmdGetCompanyPricePerLead.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyPricePerLead.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
        
    }
}