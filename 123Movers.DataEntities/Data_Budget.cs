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
        static SqlCommand cmd;
        public static List<BudgetModel> GetBudget(string companyid)
        {
            List<BudgetModel> list = new List<BudgetModel>();
            using (SqlConnection dbCon = DataLayer.ConnectToDb())
            {
                cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetCompanyBudget";


                SqlParameter paramCompanyId = new SqlParameter("companyID", companyid);


                cmd.Parameters.Add(paramCompanyId);



                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmd.ExecuteReader();
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
                cmd = new SqlCommand();
                cmd.Connection = dbCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_SaveBudget";



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


                cmd.Parameters.Add(paramCompanyId);
                //cmd.Parameters.Add(paramCompanyName);
                //cmd.Parameters.Add(paramCompanyHandle);
                //cmd.Parameters.Add(paramActive);
                cmd.Parameters.Add(paramTotalBudget);
                cmd.Parameters.Add(paramRemainingBudget);
                cmd.Parameters.Add(paramBudgetAction);
                cmd.Parameters.Add(paramRecurring);
                cmd.Parameters.Add(paramRequireNoticeToCharge);
                cmd.Parameters.Add(paramAgreementNumber);
                cmd.Parameters.Add(paramMinCharge);
                cmd.Parameters.Add(paramServices);
                //cmd.Parameters.Add(paramType);

                cmd.ExecuteNonQuery();

            }

        }

    }
}