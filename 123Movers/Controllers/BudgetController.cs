using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class BudgetController : Controller
    {
        //
        // GET: /Budget/


        public JsonResult GetServices()
        {
            return Json(BusinessLayer.GetServies(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFilterResult(int? serviceId)
        {
            return Json(BusinessLayer.GetFilterResult(new CompanyModel().CurrentCompany.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetBudget(CompanyModel CompanyInfo)
        {

            IEnumerable<BudgetModel> budgetList = new List<BudgetModel>();
            SearchModel search = new SearchModel();
            CompanyModel company = new CompanyModel();

            budgetList = BusinessLayer.GetBudget(CompanyInfo.CompanyId);

            var tbilled = budgetList.Where(b => b.EndDate < DateTime.Now).Sum(b => b.TotalBudget);
            var uamount = budgetList.Where(b => b.EndDate < DateTime.Now).Sum(b => b.UnchargedAmount);

            ViewBag.TotalBilled = String.Format("{0:C}", tbilled);
            ViewBag.UnchargedAmount = String.Format("{0:C}", uamount);

            company.CurrentCompany = CompanyInfo;
            search._companyInfo = company.CurrentCompany;

            search.budget = budgetList;

            return View(search);
        }

        [HttpGet]
        public ActionResult AddBudget()
        {
            ViewBag.Terms = ConfigValues.Terms();
            ViewBag.Services = ConfigValues.Services();

            BudgetModel budget = new BudgetModel();

            budget._companyInfo = new CompanyModel().CurrentCompany;
            return View(budget);
        }

        [HttpPost]
        public ActionResult AddBudget(BudgetModel budget)
        {
            
            ViewBag.Terms = ConfigValues.Terms();
            ViewBag.Services = ConfigValues.Services();

           
            try
            {
                //if (ModelState.IsValid)
                //{
                    budget._companyInfo = new CompanyModel().CurrentCompany;
                    budget.CompanyId = budget._companyInfo.CompanyId;
                    budget.Type = "NEW";
                    BusinessLayer.SaveBudget(budget);
                    return RedirectToAction("GetBudget", budget._companyInfo.CurrentCompany);

               // }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(budget);


        }
        

        [HttpGet]
        public ActionResult EditBudget(decimal? TotalBudget, bool IsRecurring, bool IsRequireNoticeToCharge, int? serviceId, string agnumber, int? minDaysToCharge)
        {
            BudgetModel budget = new BudgetModel();
            ViewBag.Terms = ConfigValues.Terms();
            ViewBag.Services = ConfigValues.Services();
            if(serviceId == null)
            {
                serviceId = 999;
            }
            string Recurring = (IsRecurring) ? (IsRequireNoticeToCharge) ? "2" : "0" : "1";
            budget.TotalBudget = TotalBudget;
            budget.IsRecurring = IsRecurring;
            budget.IsRequireNoticeToCharge = IsRequireNoticeToCharge;
            budget.ServiceId = serviceId;
            budget.MinDaysToCharge = minDaysToCharge;
            budget.AgreementNumber = agnumber;
            budget.TermType = Recurring;

           
            budget._companyInfo = new CompanyModel().CurrentCompany;
            budget.CompanyId = budget._companyInfo.CompanyId;

            return View(budget);


        }
        [HttpPost]
        public ActionResult EditBudget(BudgetModel budget)
        {

            ViewBag.Terms = ConfigValues.Terms();
            ViewBag.Services = ConfigValues.Services();

            try
            {
                budget._companyInfo = new CompanyModel().CurrentCompany;
                budget.CompanyId = budget._companyInfo.CompanyId;
                budget.BudgetAction = Constants.RENEWL_BUDGET;
                budget.Type = "EDIT";
                BusinessLayer.SaveBudget(budget);

                return RedirectToAction("GetBudget", budget._companyInfo.CurrentCompany);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(budget);

        }

    }
}
