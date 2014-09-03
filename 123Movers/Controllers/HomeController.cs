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

    }
}
