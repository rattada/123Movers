using _123Movers.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using _123Movers.Entity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static void SaveBudget(BudgetModel budget)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_SAVE_BUDGET
                    };

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

                _cmd.Parameters.AddWithValue("companyID", budget.CompanyId);
                _cmd.Parameters.AddWithValue("totalBudget", budget.TotalBudget);
                _cmd.Parameters.AddWithValue("remainingBudget", budget.RemainingBudget);
                _cmd.Parameters.AddWithValue("budgetAction", budget.BudgetAction);
                _cmd.Parameters.AddWithValue("isRecurring", budget.IsRecurring);
                _cmd.Parameters.AddWithValue("isRequireNoticeToCharge", budget.IsRequireNoticeToCharge);
                _cmd.Parameters.AddWithValue("agreementNumber", budget.AgreementNumber.TrimNullOrEmpty());
                _cmd.Parameters.AddWithValue("minDaysToCharge", budget.MinDaysToCharge);
                _cmd.Parameters.AddWithValue("service", budget.ServiceId == Constants.BOTH ? null : budget.ServiceId);
                _cmd.Parameters.AddWithValue("type", budget.Type);

               _cmd.ExecuteNonQuery();
            }

        }
      
        public static List<List<string>> GetFilterResult(int? companyId, int? serviceId)
        {
            var dtResults = new DataTable();
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_FILTER_RESULT
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);

                var drResults = _cmd.ExecuteReader();

                dtResults.Load(drResults);
            }
            return ConfigValues.TableToList(dtResults);
        }
        public static void RenewBudget(int? companyId, int? serviceId)
        {
            using (var db = new MoversDBEntities())
            {
                tbl_companyBudget budget;
                budget = serviceId == null ? db.CompanyBudget.FirstOrDefault(b => b.companyID == companyId && b.serviceID == null) : db.CompanyBudget.FirstOrDefault(b => b.companyID == companyId && b.serviceID == serviceId);
                if (budget == null) return;
                budget.isOneTimeRenew = true;

                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static List<AreaCodeModel> GetBudgetFilterInfo(int? companyId, int? serviceId)
        {

            using (var db = new MoversDBEntities())
            {
                var budgetFilter = serviceId == null ? db.CompanyAreacode.Where(a => a.companyID == companyId).OrderByDescending(a => a.serviceID).ToList() : db.CompanyAreacode.Where(a => a.companyID == companyId && a.serviceID == serviceId).ToList();

                return budgetFilter.Select(areaCode => new AreaCodeModel
                    {
                        companyID = areaCode.companyID, serviceID = areaCode.serviceID, areaCode = areaCode.areaCode, isForceSelect = areaCode.isForceSelect, isDestinationAreaCode = areaCode.isDestinationAreaCode, isMoveDistanceSelect = areaCode.isMoveDistanceSelect, isMoveWeightSelect = areaCode.isMoveWeightSelect, isOriginZipCode = areaCode.isOriginZipCode, isSpecificOriginDestinationAreacode = areaCode.isSpecificOriginDestinationAreacode, isSpecificOriginDestinationState = areaCode.isSpecificOriginDestinationState
                    }).ToList();
            }
        }

        public static BudgetModel GetBudgetById(int? id)
        {
            var budget = new BudgetModel();
            using (var db = new MoversDBEntities())
            {
                var _budget = db.CompanyBudget.FirstOrDefault(b => b.tid == id);

                if (_budget != null)
                {
                    budget = new BudgetModel
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
                }
            }
            return budget;
        }

        public static List<BudgetModel> GetCureentBudgets(int? companyId)
        {
            var budgets = new List<BudgetModel>();
            using(var db = new MoversDBEntities())
            {
                var _budgets = db.CompanyBudget.Where(b => b.companyID == companyId).ToList();
                budgets.AddRange(_budgets.Select(budget => new BudgetModel
                    {
                        tId = budget.tid, CompanyId = budget.companyID, ServiceId = budget.serviceID, TotalBudget = budget.totalBudget, RemainingBudget = budget.remainingBudget, StartDate = budget.stampDate, EndDate = budget.lastModified, IsRecurring = budget.isRecurring, IsRequireNoticeToCharge = budget.isRequireNoticeToCharge, IsOneTimeRenew = budget.isOneTimeRenew, AgreementNumber = budget.agreementNumber, MinDaysToCharge = budget.minDaysToCharge
                    }));
            }
            return budgets;
        }

        public static List<BudgetModel> GetPastBudgets(int? companyId)
        {
            var budgets = new List<BudgetModel>();
            using (var db = new MoversDBEntities())
            {
                var count = db.CompanyBudget.Count(cb => cb.companyID == companyId);
                var _budgets = db.tl_companyBudget.Where(pb => pb.companyID == companyId && pb.action == "insert").OrderByDescending(pb => pb.stampDate).Skip(count).ToList();

                budgets.AddRange(_budgets.Select(budget => new BudgetModel
                    {
                        tId = budget.tid, CompanyId = budget.companyID, ServiceId = budget.serviceID, TotalBudget = budget.totalBudget, RemainingBudget = budget.remainingBudget, StartDate = budget.stampDate, EndDate = budget.lastModified, IsRecurring = budget.isRecurring.ToString().BooleanNullOrEmpty(), IsRequireNoticeToCharge = budget.isRequireNoticeToCharge.ToString().BooleanNullOrEmpty(),
                    }));
            }
            return budgets;
        }
    }
}