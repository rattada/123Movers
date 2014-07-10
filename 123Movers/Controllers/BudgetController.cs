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


       
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetServices()
        {
            var services = BusinessLayer.GetServies();
            List<List<string>> list = ConfigValues.retListTable(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFilterResult(int? serviceId)
        {
            int? cmd = new CompanyModel().CurrentCompany.CompanyId;
            List<List<string>> list = ConfigValues.retListTable(BusinessLayer.GetFilterResult(cmd, serviceId));
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetBudget(CompanyModel CompanyInfo)
        {

            // MoversEntities dc = new MoversEntities();
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

            //Session["CurrentCompanyInfo"] = _companyInfo;

            //Session["CompanyId"] = _companyInfo.CompanyId;
            //Session["CompanyName"] = _companyInfo.CompanyName;
            //Session["Ax"] = _companyInfo.AX;
            //Session["IsActive"] = _companyInfo.IsActive;
            //Session["Suspended"] = _companyInfo.Suspended;
            //Session["ContactPerson"] = _companyInfo.ContactPerson;

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
        // [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddBudget(BudgetModel budget)
        {
            
            ViewBag.Terms = ConfigValues.Terms();
            ViewBag.Services = ConfigValues.Services();

           
            try
            {
                budget._companyInfo = new CompanyModel().CurrentCompany;
                //if (ModelState.IsValid)
                //{
               // var cmd = (string)Session["CompanyId"];
                budget.CompanyId = budget._companyInfo.CompanyId;
                //budget.CompanyName = (string)Session["CompanyName"];
                //budget.AX = (string)Session["Ax"];
                //budget.IsActive = (bool)Session["IsActive"];
                //budget.ContactPerson = (string)Session["ContactPerson"];

                if (budget.TermType == "0")
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = false;
                }
                else if (budget.TermType == "1")
                {
                    budget.IsRecurring = false;
                    budget.IsRequireNoticeToCharge = false;
                }
                else
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = true;
                }

                BusinessLayer.SaveBudget(budget);
                // ModelState.Clear();
                ViewBag.Success = "Budget saved successfully..";
                //return View();
                return RedirectToAction("GetBudget", budget._companyInfo.CurrentCompany);

                //}
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
            int Service = (serviceId == 1009) ? 1009 : (serviceId == 1000) ? 1000 : 999;
            string Recurring = (IsRecurring) ? (IsRequireNoticeToCharge) ? "2" : "0" : "1";
            budget.TotalBudget = TotalBudget;
            budget.IsRecurring = IsRecurring;
            budget.IsRequireNoticeToCharge = IsRequireNoticeToCharge;
            budget.ServiceId = Service;
            budget.MinDaysToCharge = minDaysToCharge;
            budget.AgreementNumber = agnumber;
            budget.TermType = Recurring;

            var cmd = (string)Session["CompanyId"];
            budget._companyInfo = new CompanyModel().CurrentCompany;
            budget.CompanyId = budget._companyInfo.CompanyId;
            //budget.CompanyName = (string)Session["CompanyName"];
            //budget.AX = (string)Session["Ax"];
            //budget.IsActive = (bool)Session["IsActive"];
            //budget.Suspended = (string)Session["Suspended"];
            //budget.ContactPerson = (string)Session["ContactPerson"];

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

                //budget.CompanyName = (string)Session["CompanyName"];
                //budget.AX = (string)Session["Ax"];
                //budget.IsActive = (bool)Session["IsActive"];
                //budget.Suspended = (string)Session["    "];
                //budget.ContactPerson = (string)Session["ContactPerson"];
                budget.BudgetAction = "RENEWAL INSERTION";
                //budget.Type = "EDIT";

                if (budget.TermType == "0")
                {

                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = false;
                }
                else if (budget.TermType == "1")
                {
                    budget.IsRecurring = false;
                    budget.IsRequireNoticeToCharge = false;
                }
                else
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = true;
                }

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
