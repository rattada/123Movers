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
    public class HomeControllerTest
    {
        [TestMethod]
        public void Search_SearchWithEmpty_ReturnZeroRecords()
        {
            SearchModel model = new SearchModel();
            var companies = BusinessLayer.SearchCompany(model);

            Assert.IsFalse(companies.Count() > 0, "The returned collection is not empty.");
            Assert.AreEqual(0, companies.Count());
        }
        [TestMethod]
        public void Search_SearchWithCompanyId_ReturnOneCompany()
        {
            SearchModel model = new SearchModel();
            model.CompanyId = 1234;
            var companies = BusinessLayer.SearchCompany(model);

            Assert.IsNotNull(companies, "The request did not return any results.");
            Assert.IsTrue(companies.Count() > 0, "The returned collection is empty.");
            Assert.AreEqual(1, companies.Count());
            Assert.Fail("test");
        }

    }
}
