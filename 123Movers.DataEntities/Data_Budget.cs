using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.DataEntities;
using _123MoversEntity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static void SaveBudget(BudgetModel budget)
        {
            using (SqlConnection dbCon = ConnectToDb())
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
                SqlParameter paramAgreementNumber = new SqlParameter("agreementNumber", budget.AgreementNumber.TrimNullOrEmpty());
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
            tbl_companyBudget budget;
            using (MoversDBEntities db = new MoversDBEntities())
            {
                if (serviceId == null)
                {
                    budget = db.tbl_companyBudget.FirstOrDefault(b => b.companyID == companyId && b.serviceID == null);
                }
                else
                {
                    budget = db.tbl_companyBudget.FirstOrDefault(b => b.companyID == companyId && b.serviceID == serviceId);
                }
                if (budget != null)
                {
                    budget.isOneTimeRenew = true;

                    db.ObjectStateManager.ChangeObjectState(budget, System.Data.EntityState.Modified);
                    db.SaveChanges();
                }
            }
        }

        public static List<AreaCodeModel> GetBudgetFilterInfo(int? companyID, int? serviceID)
        {

            using (MoversDBEntities db = new MoversDBEntities())
            {
                List<tbl_companyAreacode> budgetFilter;
                if (serviceID == null)
                {
                    budgetFilter = db.tbl_companyAreacode.Where(a => a.companyID == companyID).OrderByDescending(a => a.serviceID).ToList();
                }
                else { 
                    budgetFilter = db.tbl_companyAreacode.Where(a => a.companyID == companyID && a.serviceID == serviceID).ToList();
                }
                
                List<AreaCodeModel> _areaCodes = new List<AreaCodeModel>();
                foreach (var areaCode in budgetFilter)
                {
                    AreaCodeModel _areaCode = new AreaCodeModel { 
                        companyID = areaCode.companyID,
                        serviceID = areaCode.serviceID,
                        areaCode = areaCode.areaCode,
                        isForceSelect = areaCode.isForceSelect,
                        isDestinationAreaCode = areaCode.isDestinationAreaCode,
                        isMoveDistanceSelect = areaCode.isMoveDistanceSelect,
                        isMoveWeightSelect = areaCode.isMoveWeightSelect,
                        isOriginZipCode = areaCode.isOriginZipCode,
                        isSpecificOriginDestinationAreacode = areaCode.isSpecificOriginDestinationAreacode,
                        isSpecificOriginDestinationState = areaCode.isSpecificOriginDestinationState
                    };
                    _areaCodes.Add(_areaCode);
                }
                return _areaCodes;
            }
        }

        public static BudgetModel GetBudgetById(int? id)
        {
            BudgetModel budget = new BudgetModel();
            using (MoversDBEntities db = new MoversDBEntities())
            {
                var _budget = db.tbl_companyBudget.Where(b => b.tid == id).FirstOrDefault();

                budget.tId = _budget.tid;
                budget.CompanyId = _budget.companyID;
                budget.ServiceId = _budget.serviceID;
            }
            return budget;
        }

        public static List<BudgetModel> GetCureentBudgets(int? companyID)
        {
            List<BudgetModel> budgets = new List<BudgetModel>();
            using(MoversDBEntities db = new MoversDBEntities())
            {
                var _budgets = db.tbl_companyBudget.Where(b => b.companyID == companyID).ToList();
                foreach (var _budget in _budgets)
                {
                    BudgetModel budget = new BudgetModel
                    {
                        tId = _budget.tid,
                        CompanyId = _budget.companyID,
                        ServiceId = _budget.serviceID,
                        TotalBudget = _budget.totalBudget,
                        RemainingBudget = _budget.remainingBudget,
                        StartDate = _budget.stampDate,
                        EndDate = _budget.lastModified,
                        IsRecurring = _budget.isRecurring,
                        IsRequireNoticeToCharge = _budget.isRequireNoticeToCharge,
                        IsOneTimeRenew = _budget.isOneTimeRenew,
                        AgreementNumber = _budget.agreementNumber,
                        MinDaysToCharge = _budget.minDaysToCharge
                    };
                    budgets.Add(budget);
                }
            }
            return budgets;
        }

        public static List<BudgetModel> GetPastBudgets(int? companyID)
        {
            List<BudgetModel> budgets = new List<BudgetModel>();
            using (MoversDBEntities db = new MoversDBEntities())
            {
                var count = db.tbl_companyBudget.Where(cb => cb.companyID == companyID).ToList().Count();
                var _budgets = db.tl_companyBudget.Where(pb => pb.companyID == companyID && pb.action == "insert").OrderByDescending(pb => pb.stampDate).Skip(count).ToList();
                              
                foreach (var _budget in _budgets)
                {
                    BudgetModel budget = new BudgetModel
                    {
                        tId = _budget.tid,
                        CompanyId = _budget.companyID,
                        ServiceId = _budget.serviceID,
                        TotalBudget = _budget.totalBudget,
                        RemainingBudget = _budget.remainingBudget,
                        StartDate = _budget.stampDate,
                        EndDate = _budget.lastModified,
                        IsRecurring = _budget.isRecurring.ToString().BooleanNullOrEmpty(),
                        IsRequireNoticeToCharge = _budget.isRequireNoticeToCharge.ToString().BooleanNullOrEmpty(),
                    };
                    budgets.Add(budget);
                }
            }
            return budgets;
        }
    }
}