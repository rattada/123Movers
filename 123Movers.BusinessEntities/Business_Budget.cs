using _123Movers.DataEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static List<BudgetModel> GetBudget(int? companyid)
        {
            return DataLayer.GetBudget(companyid);
        }

        public static void SaveBudget(BudgetModel budget)
        {
            budget.RemainingBudget = budget.TotalBudget;
            DataLayer.SaveBudget(budget);
        }

        //public static List<List<string>> GetServies()
        //{
        //    return DataLayer.GetServices();
        //}

        public static List<List<string>> GetFilterResult(int? companyID, int? serviceID)
        {
            return DataLayer.GetFilterResult(companyID, serviceID);
        }
        public static void RenewBudget(int? companyId, int? serviceId)
        {
            DataLayer.RenewBudget(companyId, serviceId);
        }
    }
}