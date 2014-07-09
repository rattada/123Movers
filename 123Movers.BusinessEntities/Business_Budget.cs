using _123Movers.DataEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static List<BudgetModel> GetBudget(string companyid)
        {
            return DataLayer.GetBudget(companyid);
        }

        public static void SaveBudget(BudgetModel budget)
        {
            budget.RemainingBudget = budget.TotalBudget;
            DataLayer.SaveBudget(budget);
        }
    }
}