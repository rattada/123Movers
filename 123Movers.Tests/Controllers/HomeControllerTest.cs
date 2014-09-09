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
        int? companyId = 1234;
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
            model.CompanyId = companyId;
            var companies = BusinessLayer.SearchCompany(model);

            Assert.IsNotNull(companies, "The request did not return any results.");
            Assert.IsTrue(companies.Count() > 0, "The returned collection is empty.");
            Assert.AreEqual(1, companies.Count());
        }

        [TestMethod]
        public void Search_SearchWithCompanyName_ReturnOneCompany()
        {
            SearchModel model = new SearchModel();
            model.CompanyName = "51 State Moving Corp";
            var companies = BusinessLayer.SearchCompany(model);

            Assert.IsNotNull(companies, "The request did not return any results.");
            Assert.IsTrue(companies.Count() > 0, "The returned collection is empty.");
            Assert.AreEqual(1, companies.Count());
        }

        [TestMethod]
        public void Search_SearchWith_Partial_CompanyName_ReturnMoreThanOneCompany()
        {
            SearchModel model = new SearchModel();
            model.CompanyName = "Movers";
            var companies = BusinessLayer.SearchCompany(model);

            Assert.IsNotNull(companies, "The request did not return any results.");
            Assert.IsTrue(companies.Count() > 1, "The returned collection is empty.");
        }

        [TestMethod]
        public void Search_SearchWithAXNumber_ReturnOneCompany()
        {
            SearchModel model = new SearchModel();
            model.AX = "10007121";
            var companies = BusinessLayer.SearchCompany(model);

            Assert.IsNotNull(companies, "The request did not return any results.");
            Assert.AreEqual(1, companies.Count());
        }
    }
}
