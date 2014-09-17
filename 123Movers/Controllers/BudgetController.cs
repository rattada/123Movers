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
    /// <summary>
    /// Budget related controller
    /// </summary>
    public class BudgetController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BudgetController));
     
        /// <summary>
        /// Get Filter information
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <returns>Updated Filter Information</returns>
        public JsonResult GetFilterResult(int? serviceId)
        {
            return Json(BusinessLayer.GetFilterResult(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
        
       

        
        /// <summary>
        /// Enter Budget Information
        /// </summary>
        /// <param name="company">Company Model</param>
        [HttpGet]
        public ActionResult AddBudget(CompanyModel company)
        {
            ViewBag.Terms = GetTerms();// ConfigValues.Terms();
            ViewBag.Services = GetServices();
            
            BudgetModel budget = new BudgetModel();

            budget._companyInfo = company;
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
            try
            {
                if (ModelState.IsValid)
                {
                    budget._companyInfo = RetrieveCurrentCompanyInfo(budget.CompanyId);
                    budget.CompanyId = budget._companyInfo.CompanyId;
                    budget.Type = Constants.NEW_BUDGET;

                    BusinessLayer.SaveBudget(budget);

                    return RedirectToAction("GetBudget", "Home", budget._companyInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(budget);


        }
        

        [HttpGet]
        public ActionResult EditBudget(int? id)
        {
            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();

            var budget = BusinessLayer.GetBudgetById(id);
            string Recurring = ((bool)budget.IsRecurring) ? ((bool)budget.IsRequireNoticeToCharge) ? Constants.RecurringWithNotice : Constants.Recurring : Constants.NonRecurring;
            budget.TermType = Recurring;
            budget.ServiceId = budget.ServiceId == null ? (int)ServiceType.Both : budget.ServiceId;

            budget._companyInfo = CompanyInfo;

            return View(budget);
        }

        [HttpPost]
        public ActionResult EditBudget(BudgetModel budget)
        {

            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();

            try
            {
                budget._companyInfo = RetrieveCurrentCompanyInfo(CompanyId);
                budget.Type = Constants.EDIT_BUDGET;

                BusinessLayer.SaveBudget(budget);

                return RedirectToAction("GetBudget", "Home", budget._companyInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
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
                BusinessLayer.RenewBudget(CompanyId, ServiceId);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;

        }

        [HttpGet]
        public ActionResult BudgetFilterInfo(int? companyId, int? serviceId)
        {
            AreaCodeModel areaCode = new AreaCodeModel();
            areaCode._companyInfo = RetrieveCurrentCompanyInfo(companyId);
            areaCode._areaCodes = BusinessLayer.GetBudgetFilterInfo(companyId, serviceId);
            return View(areaCode);
        }
    }
}
