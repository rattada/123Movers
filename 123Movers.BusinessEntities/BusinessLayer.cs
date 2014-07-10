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
            //if (search.CompanyId)
            //{
            //    search.CompanyId = search.CompanyId.Trim();
            //}
            if (!string.IsNullOrEmpty(search.CompanyName))
            {
                search.CompanyName = search.CompanyName.Trim();
            }

            if (!string.IsNullOrEmpty(search.AX))
            {
                search.AX = search.AX.Trim();
            }
             if (!string.IsNullOrEmpty(search.InsertionOrderId))
            {
                search.InsertionOrderId = search.InsertionOrderId.Trim();
            }
            return DataLayer.SearchCompany(search);
        }

        
       
        //public static DataTable GetCompanyPricePerLead(int? companyId, int? serviceId)
        //{
        //    return DataLayer.GetCompanyPricePerLead(companyId, serviceId);
        //}
       
  

      
      

     

        



    }
}
