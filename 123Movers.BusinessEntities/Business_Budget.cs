using _123Movers.DataEntities;
using _123Movers.Models;
using System.Collections.Generic;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static void SaveBudget(BudgetModel budget)
        {
            budget.RemainingBudget = budget.TotalBudget;
            DataLayer.SaveBudget(budget);
        }
        public static BudgetModel GetBudgetById(int? id)
        {
            return DataLayer.GetBudgetById(id);
        }
        public static List<List<string>> GetFilterResult(int? companyId, int? serviceId)
        {
            return DataLayer.GetFilterResult(companyId, serviceId);
        }
        public static void RenewBudget(int? companyId, int? serviceId)
        {
            DataLayer.RenewBudget(companyId, serviceId);
        }
        public static List<AreaCodeModel> GetBudgetFilterInfo(int? companyId, int? serviceId)
        {
            return DataLayer.GetBudgetFilterInfo(companyId, serviceId);
        }
        public static List<BudgetModel> GetCureentBudgets(int? companyId)
        {
            return DataLayer.GetCureentBudgets(companyId);
        }
        public static List<BudgetModel> GetPastBudgets(int? companyId)
        {
            return DataLayer.GetPastBudgets(companyId);
        }
    }
}