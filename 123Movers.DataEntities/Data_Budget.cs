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
        public static List<BudgetModel> GetBudget(int? companyid)
        {
            List<BudgetModel> list = new List<BudgetModel>();
            using (SqlConnection dbCon = DataLayer.ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_BUDGET;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyid);

                _cmd.Parameters.Add(paramCompanyId);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);

                foreach (DataRow row in dtResults.Rows)
                {
                    BudgetModel budget = new BudgetModel
                    {
                        CompanyId = row["CompanyId"].ToString().IntNullOrEmpty(),
                        CompanyName = row["Company Name"].ToString(),
                        AX = row["AX Number"].ToString(),
                        InsertionOrderId = row["Budget Insertion ID"].ToString(),
                        AgreementNumber = row["agreementNumber"].ToString(),
                        //AreaCodes = row["Area Code"].ToString(),
                        StartDate = row["Budget Start Date"].ToString().DateNullOrEmpty(),
                        EndDate = row["Budget End Date"].ToString().DateNullOrEmpty(),
                        TotalBudget = row["Total Budget"].ToString().DecimalNullOrEmpty(),
                        RemainingBudget = row["Remaining Budget"].ToString().DecimalNullOrEmpty(),
                        UnchargedAmount = row["Uncharged Amount"].ToString().DecimalNullOrEmpty(),
                        ServiceId = row["ServiceID"].ToString().IntNullOrEmpty(),
                        MinDaysToCharge = row["minDaysToCharge"].ToString().IntNullOrEmpty(),
                        IsRecurring = row["IsReccurring"].ToString().BooleanNullOrEmpty(),
                        IsRequireNoticeToCharge = row["IsRequireNoticeToCharge"].ToString().BooleanNullOrEmpty(),
                        //ContactPerson = row["contactPerson"].ToString(),
                        IsOneTimeRenew = row["isOneTimeRenew"].ToString().BooleanNullOrEmpty(),
                        IsActive = row["isActive"].ToString().BooleanNullOrEmpty()
                    };
                    list.Add(budget);
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
                _cmd.CommandText = Constants.SP_SAVE_BUDGET;

                if (budget.TermType == Constants.Recurring)
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = false;
                }
                else if (budget.TermType == Constants.NonRecurring)
                {
                    budget.IsRecurring = false;
                    budget.IsRequireNoticeToCharge = false;
                }
                else
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = true;
                }

                SqlParameter paramCompanyId = new SqlParameter("companyID", budget.CompanyId);
                SqlParameter paramTotalBudget = new SqlParameter("totalBudget", budget.TotalBudget);
                SqlParameter paramRemainingBudget = new SqlParameter("remainingBudget", budget.RemainingBudget);
                SqlParameter paramBudgetAction = new SqlParameter("budgetAction", budget.BudgetAction);
                SqlParameter paramRecurring = new SqlParameter("isRecurring", budget.IsRecurring);
                SqlParameter paramRequireNoticeToCharge = new SqlParameter("isRequireNoticeToCharge", budget.IsRequireNoticeToCharge);
                SqlParameter paramAgreementNumber = new SqlParameter("agreementNumber", budget.AgreementNumber);
                SqlParameter paramMinCharge = new SqlParameter("minDaysToCharge", budget.MinDaysToCharge);
                SqlParameter paramServices = new SqlParameter("service", budget.ServiceId == Constants.BOTH ? null:budget.ServiceId);
                SqlParameter paramType = new SqlParameter("type", budget.Type);


                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramTotalBudget);
                _cmd.Parameters.Add(paramRemainingBudget);
                _cmd.Parameters.Add(paramBudgetAction);
                _cmd.Parameters.Add(paramRecurring);
                _cmd.Parameters.Add(paramRequireNoticeToCharge);
                _cmd.Parameters.Add(paramAgreementNumber);
                _cmd.Parameters.Add(paramMinCharge);
                _cmd.Parameters.Add(paramServices);
                _cmd.Parameters.Add(paramType);

                _cmd.ExecuteNonQuery();
            }

        }
        //public static List<List<string>> GetServices()
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        _cmd = new SqlCommand();
        //        _cmd.Connection = dbCon;
        //        _cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        _cmd.CommandText = Constants.SP_GET_AREACODES_STATES;

        //        SqlParameter paramType = new SqlParameter("queryType", 1);

        //        _cmd.Parameters.Add(paramType);

        //        DataTable dtResults = new DataTable();

        //        SqlDataReader drResults = _cmd.ExecuteReader();
        //        dtResults.Load(drResults);

        //        return ConfigValues.TableToList(dtResults);

        //    }

        //}
        public static List<List<string>> GetFilterResult(int? companyID, int? serviceID)
        {
            DataTable dtResults = new DataTable();
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_FILTER_RESULT;

                _cmd.Parameters.Add(new SqlParameter("companyID", companyID));
                _cmd.Parameters.Add(new SqlParameter("serviceID", serviceID));

                SqlDataReader drResults = _cmd.ExecuteReader();

                dtResults.Load(drResults);
            }
            return ConfigValues.TableToList(dtResults);
        }
        public static void RenewBudget(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_RENEWAL_BUDGET;

                _cmd.Parameters.Add(new SqlParameter("companyID", companyId));
                _cmd.Parameters.Add(new SqlParameter("serviceID", serviceId));

               _cmd.ExecuteNonQuery();
            }
        }

        public static List<List<string>> GetBudgetFilterInfo(int? companyID, int? serviceID)
        {
            DataTable dtResults = new DataTable();
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_BUDGET_FILTER;

                _cmd.Parameters.Add(new SqlParameter("companyID", companyID));
                _cmd.Parameters.Add(new SqlParameter("serviceID", serviceID));

                SqlDataReader drResults = _cmd.ExecuteReader();

                dtResults.Load(drResults);
            }
            return ConfigValues.TableToList(dtResults);
        }

    }
}