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
        static SqlCommand _cmd;
        public static List<BudgetModel> GetBudget(int? companyid)
        {
            List<BudgetModel> list = new List<BudgetModel>();
            using (SqlConnection dbCon = DataLayer.ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = "usp_GetCompanyBudget";


                SqlParameter paramCompanyId = new SqlParameter("companyID", companyid);


                _cmd.Parameters.Add(paramCompanyId);



                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
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

        public static void SaveBudget(BudgetModel budget)
        {
            using (SqlConnection dbCon = DataLayer.ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = "usp_SaveBudget";



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
                SqlParameter paramServices = new SqlParameter("service", budget.ServiceId);
                //SqlParameter paramType = new SqlParameter("type", budget.Type);


                _cmd.Parameters.Add(paramCompanyId);
                //_cmd.Parameters.Add(paramCompanyName);
                //_cmd.Parameters.Add(paramCompanyHandle);
                //_cmd.Parameters.Add(paramActive);
                _cmd.Parameters.Add(paramTotalBudget);
                _cmd.Parameters.Add(paramRemainingBudget);
                _cmd.Parameters.Add(paramBudgetAction);
                _cmd.Parameters.Add(paramRecurring);
                _cmd.Parameters.Add(paramRequireNoticeToCharge);
                _cmd.Parameters.Add(paramAgreementNumber);
                _cmd.Parameters.Add(paramMinCharge);
                _cmd.Parameters.Add(paramServices);
                //_cmd.Parameters.Add(paramType);

                _cmd.ExecuteNonQuery();

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
        public static DataTable GetFilterResult(int? companyID, int? serviceID)
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