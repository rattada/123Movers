using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123Movers.Models;
using _123Movers.DataEntities;
using System.Data;
using System.Xml;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        //public static void RegisterUser(RegisterModel model)
        //{
        //    DataLayer.RegisterUser(model);
        //}
        //public static bool Login(LoginModel login)
        //{
        //    return DataLayer.Login(login);
        //}

        //public static void SaveBudget(BudgetModel budget)
        //{
        //    budget.RemainingBudget = budget.TotalBudget;
        //    DataLayer.SaveBudget(budget);
        //}
        public static IEnumerable<SearchModel> SearchCompany(SearchModel search)
        {
            return DataLayer.SearchCompany(search);
        }

    }
}
