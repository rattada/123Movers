﻿using System;
using System.Web.Mvc;
using _123Movers.Models;
using _123Movers.BusinessEntities;
using log4net;

namespace _123Movers.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    [Authorize]
    public class HomeController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(HomeController)); 

        /// <summary>
        /// Get Method
        /// </summary>
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// Search the Company Information
        /// </summary>
        /// <param name="search">Search Model</param>
        /// <returns>List of Companies</returns>
        [HttpPost]
        public ActionResult Search(SearchModel search)
        {
            try
            {
                var companies = BusinessLayer.SearchCompany(search);
                search.Companies = companies;

                return View(search);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(search);
        }

        /// <summary>
        /// Get All budgets for company
        /// </summary>
        /// <param name="company">Company Details</param>
        /// <returns>List Of BudgetModel</returns>
        [HttpGet]
        public ActionResult GetBudget(CompanyModel company)
        {
            var budget = new BudgetModel();

            //Store CompanyId in cookie for use entire application
            SaveCompanyId(company.CompanyId);
            //Store Company Information in Session for use entire application
            SaveCompanyInfo(company);

            var currentBudgets = BusinessLayer.GetCureentBudgets(CompanyId);
            var pastBudgets = BusinessLayer.GetPastBudgets(CompanyId);

            budget.CurrentBudgets = currentBudgets;
            budget.PastBudgets = pastBudgets;

            budget.CompanyInfo = CompanyInfo;

            return View(budget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchstring"></param>
        /// <returns></returns>
        public JsonResult AutocompleteSuggestions(string searchstring)
        {
            return Json(BusinessLayer.AutocompleteSuggestions(searchstring), JsonRequestBehavior.AllowGet);
        }

    }
}
