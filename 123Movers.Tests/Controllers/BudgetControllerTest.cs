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
        [TestMethod]
        public void GetBudgetsForCompany()
        {
            IEnumerable<BudgetModel> budgetList = new List<BudgetModel>(); 
            budgetList = BusinessLayer.GetBudget(1234);

            Assert.IsNotNull(budgetList, "The request did not return any results.");
            Assert.IsTrue(budgetList.Count() > 0, "The returned collection is empty.");
        }

    }
}
