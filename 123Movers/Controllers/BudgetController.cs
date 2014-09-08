using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace _123Movers.Controllers
{
    public class BudgetController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(BudgetController));
     
        /// <summary>
        /// Get Filter information
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <returns>Updated Filter Information</returns>
        public JsonResult GetFilterResult(int? serviceId)
        {
            return Json(BusinessLayer.GetFilterResult(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
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

            SaveCompanyInfo(Company);
            _companyId = Company.CompanyId;

            var _currentBudgets = BusinessLayer.GetCureentBudgets(CompanyInfo.CompanyId);
            var _pastBudgets = BusinessLayer.GetPastBudgets(CompanyInfo.CompanyId);

            budget._currentBudgets = _currentBudgets;
            budget._pastBudgets = _pastBudgets;

            budget._companyInfo = CompanyInfo;

            return View(budget);
        }

        
        [HttpGet]
        public ActionResult AddBudget(CompanyModel Company)
        {
            ViewBag.Terms = GetTerms();// ConfigValues.Terms();
            ViewBag.Services = GetServices();
            
            BudgetModel budget = new BudgetModel();

            budget._companyInfo = Company;
            return View(budget);
        }

        /// <summary>
        /// Add new budget to company
        /// </summary>
        /// <param name="budget">Budget Model</param>
        [HttpPost]
        public ActionResult AddBudget(BudgetModel budget)
        {
            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();
            Session["CurrentCompanyInfo"] = null;
            try
            {
                if (ModelState.IsValid)
                {
                    budget._companyInfo = RetrieveCurrentCompanyInfo(budget.CompanyId);// CompanyInfo;
                    budget.CompanyId = budget._companyInfo.CompanyId;
                    budget.Type = Constants.NEW_BUDGET;

                    BusinessLayer.SaveBudget(budget);

                    return RedirectToAction("GetBudget", budget._companyInfo);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(budget);


        }
        

        //[HttpGet]
        public ActionResult EditBudget(int? companyId, decimal? TotalBudget, bool IsRecurring, bool IsRequireNoticeToCharge, int? serviceId, string agnumber, int? minDaysToCharge, int? id)
        {

            BudgetModel budget = new BudgetModel();
            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();

            Session["CurrentCompanyInfo"] = null;


            string Recurring = (IsRecurring) ? (IsRequireNoticeToCharge) ? Constants.RecurringWithNotice : Constants.Recurring : Constants.NonRecurring;
            budget.TotalBudget = TotalBudget;
            budget.IsRecurring = IsRecurring;
            budget.IsRequireNoticeToCharge = IsRequireNoticeToCharge;
            budget.ServiceId = serviceId == null ? (int)ServiceType.Both : serviceId;
            budget.MinDaysToCharge = minDaysToCharge;
            budget.AgreementNumber = agnumber;
            budget.TermType = Recurring;


            SaveSeviceId(budget.ServiceId);

            budget._companyInfo = RetrieveCurrentCompanyInfo(companyId);
            budget.CompanyId = companyId;

            return View(budget);
        }

        //[HttpGet]
        //public ActionResult EditBudget(BudgetModel budget)
        //{

        //    //BudgetModel budget = new BudgetModel();
        //    ViewBag.Terms = GetTerms();
        //    ViewBag.Services = GetServices();

        //    Session["CurrentCompanyInfo"] = null;


        //    string Recurring = ((bool)budget.IsRecurring) ? ((bool)budget.IsRequireNoticeToCharge) ? Constants.RecurringWithNotice : Constants.Recurring : Constants.NonRecurring;
        //    budget.ServiceId = budget.ServiceId == null ? (int)ServiceType.Both : budget.ServiceId;
        //    budget.TermType = Recurring;


        //    SaveSeviceId(budget.ServiceId);

        //    budget._companyInfo = CompanyInfo;
        //    budget.CompanyId = budget._companyInfo.CompanyId;

        //    return View(budget);
        //}

        [HttpPost]
        public ActionResult EditBudget(BudgetModel budget)
        {

            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();
            Session["CurrentCompanyInfo"] = null;

            try
            {
                budget._companyInfo = RetrieveCurrentCompanyInfo(budget.CompanyId);
                //budget.CompanyId = budget._companyInfo.CompanyId;
                budget.BudgetAction = Constants.RENEWL_BUDGET;
                budget.Type = Constants.EDIT_BUDGET;
                //budget.ServiceId = ServiceId;

                BusinessLayer.SaveBudget(budget);

                return RedirectToAction("GetBudget", budget._companyInfo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                ModelState.AddModelError("", ex.Message);
            }
            return View(budget);
        }

        /// <summary>
        /// Renewal the budget
        /// </summary>
        /// <param name="ServiceId">Type of Service</param>
        [HttpPost]
        public JsonResult RenewBudget(int? ServiceId)
        {
            JsonResult result;
            try
            {
                BusinessLayer.RenewBudget(CompanyInfo.CompanyId, ServiceId);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;

        }

        [HttpGet]
        public ActionResult BudgetFilterInfo(int? serviceId)
        {
            AreaCodeModel areaCode = new AreaCodeModel();
            areaCode._companyInfo = CompanyInfo;
            areaCode._areaCodes = BusinessLayer.GetBudgetFilterInfo(CompanyInfo.CompanyId, serviceId);
            return View(areaCode);
        }
    }
}
