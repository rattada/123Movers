using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _123Movers;
using _123Movers.Controllers;
using _123Movers.BusinessEntities;
using _123Movers.Models;

namespace _123Movers.Tests.Controllers
{
    [TestClass]
    public class BudgetControllerTest
    {
        int? companyId = 1234;

        private BudgetModel CreateBudget(int serviceId, string agreement, bool isRecurring, bool isRequireNoticeCharge, string action)
        {
            var budget = new BudgetModel {
                CompanyId = companyId,
                TotalBudget = 10000,
                ServiceId = serviceId,
                MinDaysToCharge = 45,
                AgreementNumber = agreement,
                IsRecurring = isRecurring,
                IsRequireNoticeToCharge = isRequireNoticeCharge,
                BudgetAction = action
            };
            return budget;
        }

        //[TestMethod]
        //public void GetBudgetsForCompany()
        //{
        //    IEnumerable<BudgetModel> budgetList = new List<BudgetModel>();
        //    budgetList = BusinessLayer.GetBudget(companyId);

        //    Assert.IsNotNull(budgetList, "The request did not return any results.");
        //    Assert.IsTrue(budgetList.Count() > 0, "The returned collection is empty.");
        //}

        //[TestMethod]
        //public void AddBudgetForCompany()
        //{
        //    var newBudget = CreateBudget(1000, "123456", true, false, "NEW INSERTION");

        //    BusinessLayer.SaveBudget(newBudget);

        //    BudgetModel budget = new BudgetModel();
        //    budget = BusinessLayer.GetBudget(companyId).Where(b => b.CompanyId == companyId && b.ServiceId == newBudget.ServiceId && b.AgreementNumber == newBudget.AgreementNumber).FirstOrDefault();

        //    Assert.IsNotNull(budget, "The request did not return any results.");
        //    Assert.AreEqual(budget.AgreementNumber, newBudget.AgreementNumber);
        //}
    }
}
