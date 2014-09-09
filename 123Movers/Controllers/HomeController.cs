using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;
using _123Movers.BusinessEntities;
using System.Data;
using System.Web.Script.Serialization;
using log4net;

namespace _123Movers.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(HomeController)); 

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
                logger.Error(ex.ToString());
                ModelState.AddModelError("", ex.Message);
            }
            return View(search);
        }

        /// <summary>
        /// Get All budgets for company
        /// </summary>
        /// <param name="Company">Company Details</param>
        /// <returns>List Of BudgetModel</returns>
        [HttpGet]
        public ActionResult GetBudget(CompanyModel Company)
        {
            BudgetModel budget = new BudgetModel();

            //Store CompanyId in cookie for use entire application
            SaveCompanyId(Company.CompanyId);
            //Store Company Information in Session for use entire application
            SaveCompanyInfo(Company);

            var _currentBudgets = BusinessLayer.GetCureentBudgets(CompanyId);
            var _pastBudgets = BusinessLayer.GetPastBudgets(CompanyId);

            budget._currentBudgets = _currentBudgets;
            budget._pastBudgets = _pastBudgets;

            budget._companyInfo = CompanyInfo;

            return View(budget);
        }

    }
}
