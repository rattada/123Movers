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
    }
}
