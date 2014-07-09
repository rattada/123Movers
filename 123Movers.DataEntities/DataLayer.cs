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

        //public static bool SaveBudget(BudgetModel budget)
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        SqlCommand cmdSaveBudget = new SqlCommand();
        //        cmdSaveBudget.Connection = dbCon;
        //        cmdSaveBudget.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdSaveBudget.CommandText = "usp_SaveBudget";



        //        SqlParameter paramCompanyId = new SqlParameter("companyID", budget.CompanyId);
        //        //SqlParameter paramCompanyName = new SqlParameter("companyName", budget.CompanyName);
        //        //SqlParameter paramCompanyHandle = new SqlParameter("companyHandle", budget.CompanyHandle);
        //       // SqlParameter paramActive = new SqlParameter("isActive", budget.IsActive);
        //        SqlParameter paramTotalBudget = new SqlParameter("totalBudget", budget.TotalBudget);
        //        SqlParameter paramRemainingBudget = new SqlParameter("remainingBudget", budget.RemainingBudget);
        //        SqlParameter paramBudgetAction = new SqlParameter("budgetAction", budget.BudgetAction);
        //        SqlParameter paramRecurring = new SqlParameter("isRecurring", budget.IsRecurring);
        //        SqlParameter paramRequireNoticeToCharge = new SqlParameter("isRequireNoticeToCharge", budget.IsRequireNoticeToCharge);
        //        SqlParameter paramAgreementNumber = new SqlParameter("agreementNumber", budget.AgreementNumber);
        //        SqlParameter paramMinCharge = new SqlParameter("minDaysToCharge", budget.MinDaysToCharge);
        //        SqlParameter paramServices = new SqlParameter("service", budget.ServiceId);
        //        //SqlParameter paramType = new SqlParameter("type", budget.Type);


        //        cmdSaveBudget.Parameters.Add(paramCompanyId);
        //        //cmdSaveBudget.Parameters.Add(paramCompanyName);
        //        //cmdSaveBudget.Parameters.Add(paramCompanyHandle);
        //        //cmdSaveBudget.Parameters.Add(paramActive);
        //        cmdSaveBudget.Parameters.Add(paramTotalBudget);
        //        cmdSaveBudget.Parameters.Add(paramRemainingBudget);
        //        cmdSaveBudget.Parameters.Add(paramBudgetAction);
        //        cmdSaveBudget.Parameters.Add(paramRecurring);
        //        cmdSaveBudget.Parameters.Add(paramRequireNoticeToCharge);
        //        cmdSaveBudget.Parameters.Add(paramAgreementNumber);
        //        cmdSaveBudget.Parameters.Add(paramMinCharge);
        //        cmdSaveBudget.Parameters.Add(paramServices);
        //        //cmdSaveBudget.Parameters.Add(paramType);

        //        var i = cmdSaveBudget.ExecuteNonQuery();
                
        //    }
        //    return true;

        //}

        //public static List<BudgetModel> GetBudget(string companyid)
        //{
        //    List<BudgetModel> list = new List<BudgetModel>();
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        SqlCommand cmdGetCompany = new SqlCommand();
        //        cmdGetCompany.Connection = dbCon;
        //        cmdGetCompany.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmdGetCompany.CommandText = "usp_GetCompanyBudget";


        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyid);
             

        //        cmdGetCompany.Parameters.Add(paramCompanyId);
     


        //        DataTable dtResults = new DataTable();

        //        SqlDataReader drResults = cmdGetCompany.ExecuteReader();
        //        dtResults.Load(drResults);

        //        foreach (DataRow row in dtResults.Rows)
        //        {
        //            int? cid = null;
        //            int? sid = null;
        //            int? dcharge = null;
        //            DateTime? sdate = null;
        //            DateTime? edate = null;
        //            decimal? tbudget = null;
        //            decimal? rbudget = null;
        //            decimal? uAmount = null;
        //            bool recurring = false;
        //            bool notice = false;

        //            if (!string.IsNullOrEmpty(row["CompanyID"].ToString()))
        //            {
        //                cid = Convert.ToInt32(row["CompanyID"]);
        //            }

        //            if (!string.IsNullOrEmpty(row["ServiceID"].ToString()))
        //            {
        //                sid = Convert.ToInt32(row["ServiceID"]);
        //            }

        //            if (!string.IsNullOrEmpty(row["minDaysToCharge"].ToString()))
        //            {
        //                dcharge = Convert.ToInt32(row["minDaysToCharge"]);
        //            }

        //            if (!string.IsNullOrEmpty(row["Budget Start Date"].ToString()))
        //            {
        //                sdate = Convert.ToDateTime(row["Budget Start Date"]).Date;
        //            }

        //            if (!string.IsNullOrEmpty(row["Budget End Date"].ToString()))
        //            {
        //                edate = Convert.ToDateTime(row["Budget End Date"]).Date;
        //            }
        //            if (!string.IsNullOrEmpty(row["Total Budget"].ToString()))
        //            {
        //                tbudget = Convert.ToDecimal(row["Total Budget"]);
        //            }
        //            if (!string.IsNullOrEmpty(row["Remaining Budget"].ToString()))
        //            {
        //                rbudget = Convert.ToDecimal(row["Remaining Budget"]);
        //            }
        //            if (!string.IsNullOrEmpty(row["Uncharged Amount"].ToString()))
        //            {
        //                uAmount = Convert.ToDecimal(row["Uncharged Amount"]);
        //            }
        //            if (!string.IsNullOrEmpty(row["IsReccurring"].ToString()))
        //            {
        //                recurring = Convert.ToBoolean(row["IsReccurring"]);
        //            }

        //            if (!string.IsNullOrEmpty(row["IsRequireNoticeToCharge"].ToString()))
        //            {
        //                notice = Convert.ToBoolean(row["IsRequireNoticeToCharge"]);
        //            }

        //            BudgetModel s = new BudgetModel
        //            {

        //                CompanyId = cid,
        //                CompanyName = row["Company Name"].ToString(),
        //                AX = row["AX Number"].ToString(),
        //                InsertionOrderId = row["Budget Insertion ID"].ToString(),
        //                AgreementNumber = row["agreementNumber"].ToString(),
        //                AreaCodes = row["Area Code"].ToString(),
        //                StartDate = sdate,
        //                EndDate = edate,
        //                TotalBudget = tbudget,
        //                RemainingBudget = rbudget,
        //                UnchargedAmount = uAmount,
        //                ServiceId = sid,
        //                MinDaysToCharge = dcharge,
        //                IsRecurring = recurring,
        //                IsRequireNoticeToCharge = notice,
        //                ContactPerson = row["contactPerson"].ToString()
        //            };
        //            list.Add(s);
        //        }


        //        return list;
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
            using (SqlConnection dbCon = ConnectToDb())
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
      
        public static DataTable GetAvailableZipCodes(int? companyId, int? serviceId,int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
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
        
        

        //public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, int? areaCode, decimal? price, int? moveWeightID)
        //{
        //    int i = 0;
        //    using (SqlConnection dbCon = ConnectToDb())
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
        
        public static bool AddCompanyLeadLimit(LeadLimitModel leadlimit)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAdByArea.CommandText = "usp_AddCompanyLeadLimit";

                int? serviceId = null;
                int? areaCode = null;

                if (leadlimit.ServiceId != null)
                {
                    serviceId = Convert.ToInt32(leadlimit.ServiceId);
                }

                if (!string.IsNullOrWhiteSpace(leadlimit.AreaCodes))
                {
                    areaCode = Convert.ToInt32(leadlimit.AreaCodes);
                }
                if (leadlimit.DailyLeadLimit != null && leadlimit.DailyLeadLimit != 0)
                {
                    leadlimit.IsDailyLeadLimit = true;
                }
                else
                {
                    leadlimit.IsDailyLeadLimit = false;

                }
                if (leadlimit.MonthlyLeadLimit != null && leadlimit.MonthlyLeadLimit != 0)
                {
                    leadlimit.IsMonthlyLeadLimit = true;
                }
                else
                {
                    leadlimit.IsMonthlyLeadLimit = false;

                }
                if (leadlimit.TotalLeadLimit != null && leadlimit.TotalLeadLimit != 0)
                {
                    leadlimit.IsTotalLeadLimit = true;
                }
                else
                {
                    leadlimit.IsTotalLeadLimit = false;

                }

                SqlParameter paramCompanyId = new SqlParameter("companyID", leadlimit.CompanyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                //SqlParameter paramMoveWeight = new SqlParameter("moveWeightID", leadlimit.MoveWeightID);
                SqlParameter paramisDailyLeadLimit = new SqlParameter("isDailyLeadLimit", leadlimit.IsDailyLeadLimit);
                SqlParameter paramisMonthlyLeadLimit = new SqlParameter("isMonthlyLeadLimit", leadlimit.IsMonthlyLeadLimit);
                SqlParameter paramisTotalLeadLimit = new SqlParameter("isTotalLeadLimit", leadlimit.IsTotalLeadLimit);
                SqlParameter paramdailyLeadLimit = new SqlParameter("dailyLeadLimit", leadlimit.DailyLeadLimit);
                SqlParameter parammontlyLeadLimit = new SqlParameter("montlyLeadLimit", leadlimit.MonthlyLeadLimit);
                SqlParameter paramtotalLeadLimit = new SqlParameter("totalLeadLimit", leadlimit.TotalLeadLimit);
               // SqlParameter paramprice = new SqlParameter("price", leadlimit.Price);
                SqlParameter paramleadFrq = new SqlParameter("leadFrq", leadlimit.LeadFrequency);


                cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdAddCompanyAdByArea.Parameters.Add(paramService);
                cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);
               // cmdAddCompanyAdByArea.Parameters.Add(paramMoveWeight);
                cmdAddCompanyAdByArea.Parameters.Add(paramisDailyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramisMonthlyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramisTotalLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramdailyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(parammontlyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramtotalLeadLimit);
                //cmdAddCompanyAdByArea.Parameters.Add(paramprice);
                cmdAddCompanyAdByArea.Parameters.Add(paramleadFrq);


                i = cmdAddCompanyAdByArea.ExecuteNonQuery();


            }
            return true;
        }

        public static LeadLimitModel GetCompanyLeadLimit(int? companyId, int? serviceId)
        {
            LeadLimitModel ldModel = new LeadLimitModel();

            List<LeadLimitModel> leadLimitData = new List<LeadLimitModel>();


            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompanyLeadLimit = new SqlCommand();
                cmdGetCompanyLeadLimit.Connection = dbCon;
                cmdGetCompanyLeadLimit.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompanyLeadLimit.CommandText = "usp_GetCompanyLeadLimit";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyLeadLimit.Parameters.Add(paramCompanyId);
                cmdGetCompanyLeadLimit.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyLeadLimit.ExecuteReader();
                dtResults.Load(drResults);

                if (dtResults.Rows.Count > 0)
                {

                    var Areacode = "";
                    var ServiceId = "";
                    foreach (DataRow row in dtResults.Rows)
                    {
                        if (String.IsNullOrEmpty(row["areaCode"].ToString()))
                        {
                            Areacode = null;

                        }
                        else
                        {
                            Areacode = row["areaCode"].ToString();
                        }
                        if (String.IsNullOrEmpty(row["serviceID"].ToString()))
                        {
                            ServiceId = null;

                        }
                        else
                        {
                            ServiceId = row["serviceID"].ToString();
                        }


                        LeadLimitModel obj = new LeadLimitModel()
                        {

                            AreaCodes = Areacode,
                            ServiceId = Convert.ToInt32(ServiceId),
                            LeadFrequency = Convert.ToInt32(row["leadFrequency"].ToString()),
                            IsDailyLeadLimit = Convert.ToBoolean(row["isDailyLeadLimit"]),
                            DailyLeadLimit = Convert.ToInt32(row["dailyLeadLimit"].ToString()),
                            IsMonthlyLeadLimit = Convert.ToBoolean(row["isMonthlyLeadLimit"]),
                            MonthlyLeadLimit = Convert.ToInt32(row["monthlyLeadLimit"].ToString()),
                            IsTotalLeadLimit = Convert.ToBoolean(row["isTotalLeadLimit"]),
                            TotalLeadLimit = Convert.ToInt32(row["totalLeadLimit"].ToString())



                        };
                        leadLimitData.Add(obj);

                    }
                    ldModel.LeadLimitData = leadLimitData;
                }


            }
            return ldModel;
        }



        public static DataTable GetCompanyPricePerLead(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
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
        public static DataTable GetReports(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
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
        public static bool AddCompanyZipCodesPerAreaCodes(int companyId, int serviceId, string areaCodes, int IsOrigin)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAdByArea.CommandText = "usp_SaveCompanyOriginDestinationZipCodes";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("AreaCodes", areaCodes);
                SqlParameter paramIsOrigin = new SqlParameter("IsOrigin", IsOrigin);

                cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdAddCompanyAdByArea.Parameters.Add(paramService);
                cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);
                cmdAddCompanyAdByArea.Parameters.Add(paramIsOrigin);
                i = cmdAddCompanyAdByArea.ExecuteNonQuery();


            }
            return true;
        }
        public static GeographyModel GetCompanyServiceAreaCodes(int? companyId, int? serviceId)
        {

            GeographyModel OriginAreaZip = new GeographyModel();
            List<GeographyModel> areacodes = new List<GeographyModel>();
            List<GeographyModel> zipcodes = new List<GeographyModel>();
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
                    GeographyModel s = new GeographyModel
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
                    GeographyModel s = new GeographyModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                        ZipCode =zid
                    };
                    zipcodes.Add(s);
                }
                OriginAreaZip.OriginZipCodes = zipcodes;





                //SqlDataReader drResults = cmdGetCompanyServiceAreaCodes.ExecuteReader();
                //dtResults.Load(drResults);

                //foreach (DataRow row in dtResults.Rows)
                //{
                //    int? sid = null;
                //    int? areacode = null;
                //    int? cid = null;

                //    if (!string.IsNullOrEmpty(row["companyID"].ToString()))
                //    {
                //        cid = Convert.ToInt32(row["companyID"]);
                //    }

                //    if (!string.IsNullOrEmpty(row["serviceID"].ToString()))
                //    {
                //        sid = Convert.ToInt32(row["serviceID"]);
                //    }
                //    if (!string.IsNullOrEmpty(row["areaCode"].ToString()))
                //    {
                //        areacode = Convert.ToInt32(row["areaCode"]);
                //    }
                //    GeographyModel s = new GeographyModel
                //    {
                //        CompanyId = cid,
                //        ServiceId = sid,
                //        AreaCode = areacode
                //    };
                //    list.Add(s);
                //}

            }
            return OriginAreaZip;
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


        public static bool SaveMoveDistance(DistanceModel model)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveMoveDistance";



                SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
                SqlParameter paramServiceID = new SqlParameter("serviceID", model.ServiceId);
                SqlParameter paramMinWeight = new SqlParameter("minMoveWeight", model.MinMoveDistance);
                SqlParameter paramMaxWeight = new SqlParameter("maxMoveWeight", model.MaxMoveDistance);


                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramServiceID);
                cmd.Parameters.Add(paramMinWeight);
                cmd.Parameters.Add(paramMaxWeight);



                i = cmd.ExecuteNonQuery();


            }
            return true;
        }


        public static DataTable GetCompanyMoveDistance(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompanyLeadLimit = new SqlCommand();
                cmdGetCompanyLeadLimit.Connection = dbCon;
                cmdGetCompanyLeadLimit.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompanyLeadLimit.CommandText = "usp_GetCompanyMoveDistance";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyLeadLimit.Parameters.Add(paramCompanyId);
                cmdGetCompanyLeadLimit.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyLeadLimit.ExecuteReader();
                dtResults.Load(drResults);

                return dtResults;

            }
        }
        public static DataSet GetMoveWeights(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetMoveSizeLookup";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramService);

                //DataTable dtResults = new DataTable();

                //SqlDataReader drResults = cmd.ExecuteReader();
                //dtResults.Load(drResults);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }

        }
        public static bool SaveMoveWeight(MoveWeightModel model)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveMoveWeight";

                SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
                SqlParameter paramService = new SqlParameter("serviceId", model.ServiceId);
                SqlParameter parammin = new SqlParameter("minMoveWeight", model.MinMoveWeightSeq);
                SqlParameter parammax = new SqlParameter("maxMoveWeight", model.MaxMoveWeightSeq);

                cmd.Parameters.Add(paramCompanyId);
                cmd.Parameters.Add(paramService);
                cmd.Parameters.Add(parammin);
                cmd.Parameters.Add(parammax);


                i = cmd.ExecuteNonQuery();


            }
            return true;
        }

        public static DestinationZipModel GetCompanyDestinationServiceAreaCodes(int? companyId, int? serviceId)
        {

            DestinationZipModel DestinationAreaZip = new DestinationZipModel();
            List<DestinationZipModel> areacodes = new List<DestinationZipModel>();
            List<DestinationZipModel> zipcodes = new List<DestinationZipModel>();
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

                    DestinationZipModel s = new DestinationZipModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                    };
                    areacodes.Add(s);
                }
                DestinationAreaZip.DestinationAreaCodes = areacodes;


                foreach (DataRow row in ds.Tables[2].Rows)
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
                    if (!string.IsNullOrEmpty(row["destinationAreaCode"].ToString()))
                    {
                        areacode = Convert.ToInt32(row["destinationAreaCode"]);
                    }
                    if (!string.IsNullOrEmpty(row["destinationZipCode"].ToString().Trim()))
                    {
                        zid = Convert.ToInt32(row["destinationZipCode"]);
                    }
                    DestinationZipModel s = new DestinationZipModel
                    {
                        CompanyId = cid,
                        ServiceId = sid,
                        AreaCode = areacode,
                        ZipCode = zid
                    };
                    zipcodes.Add(s);
                }
                DestinationAreaZip.DestinationZipCodes = zipcodes;

            }
            return DestinationAreaZip;
        }




        public static bool AddCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAreaZipCodes = new SqlCommand();
                cmdAddCompanyAreaZipCodes.Connection = dbCon;
                cmdAddCompanyAreaZipCodes.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAreaZipCodes.CommandText = "usp_AddCompanyAreasDestZipCodes";

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


        public static bool DeleteCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleteCompanyAreasDestZipCodes";

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



        public static DataTable GetAvailableDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetAvailableAreas.CommandText = "usp_GetCompanyAvailableAreasDestincationZipCodes"; //"usp_availableAreacoded";
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
        public static DataTable GetCompanyAreasDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetAvailableAreas = new SqlCommand();
                cmdGetAvailableAreas.Connection = dbCon;
                cmdGetAvailableAreas.CommandType = System.Data.CommandType.StoredProcedure;
                //cmdGetService.CommandText = "usp_GetAreaCodesAndStates";
                cmdGetAvailableAreas.CommandText = "usp_GetCompanyAreasDestinationZipCodes"; //"usp_availableAreacoded";
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
        public static DataTable GetFilterResult(int companyID, int? serviceID)
        {
            DataTable dtResults = new DataTable();

            try
            {
                using (SqlConnection dbCon = ConnectToDb())
                {
                    SqlCommand cmdGetService = new SqlCommand();
                    cmdGetService.Connection = dbCon;
                    cmdGetService.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdGetService.CommandText = "usp_FilterResult";

                    cmdGetService.Parameters.Add(new SqlParameter("companyID", companyID));
                    cmdGetService.Parameters.Add(new SqlParameter("serviceID", serviceID));


                    SqlDataReader drResults = cmdGetService.ExecuteReader();

                    dtResults.Load(drResults);

                }

            }
            catch (Exception ex)
            {

            }
            return dtResults;

        }

    }

}