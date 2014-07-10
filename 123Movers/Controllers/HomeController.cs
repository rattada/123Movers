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
    public class HomeController : Controller
    {

        public ActionResult Notifications()
        {
            return View();
        }


      

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
                if (search.CompanyId == null && search.CompanyName == null && search.InsertionOrderId == null && search.AX == null)
                {
                    return View(search);
                }
                
                var companies = BusinessLayer.SearchCompany(search);
                search.Companies = companies;
                //if (budget.Count() > 0)
                //{
                //    foreach (var b in budget)
                //    {
                //        HttpContext.Application["CompanyId"] = b.CompanyId;
                //        HttpContext.Application["CompanyName"] = b.CompanyName;
                //        HttpContext.Application["Ax"] = b.AX;
                //        HttpContext.Application["IsActive"] = b.IsActive;
                //        //HttpContext.Application["DisplayName"] = b.DisplayName;
                //        HttpContext.Application["ContactPerson"] = b.ContactPerson;
                //        // HttpContext.Application["CompanyHandle"] = b.CompanyHandle;
                //        break;
                //    }

                //}

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
