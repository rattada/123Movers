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

        public static DataTable GetServies()
        {
            return DataLayer.GetServices();
        }
        
        public static DataTable GetFilterResult(int? companyID, int? serviceID)
        {
            return DataLayer.GetFilterResult(companyID, serviceID);
        }
    }
}