using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;
using _123Movers.BusinessEntities;
using System.Data;
using System.Web.Script.Serialization;

namespace _123Movers.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

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
                ModelState.AddModelError("", ex.Message);
            }
            return View(search);
        }

    }
}
