using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
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

            budget.CompanyInfo = company;
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
                    budget.CompanyInfo = RetrieveCurrentCompanyInfo(budget.CompanyId);
                    budget.CompanyId = budget.CompanyInfo.CompanyId;
                    budget.Type = Constants.NEW_BUDGET;

                    BusinessLayer.SaveBudget(budget);

                    return RedirectToAction("GetBudget", "Home", budget.CompanyInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(budget);


        }
        

        /// <summary>
        /// Modify the Budget details
        /// </summary>
        /// <param name="id">Budget Id</param>
        [HttpGet]
        public ActionResult EditBudget(int? id)
        {
            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();

            var budget = BusinessLayer.GetBudgetById(id);
            string recurring = ((bool)budget.IsRecurring) ? ((bool)budget.IsRequireNoticeToCharge) ? Constants.RecurringWithNotice : Constants.Recurring : Constants.NonRecurring;
            budget.TermType = recurring;
            budget.ServiceId = budget.ServiceId ?? (int)ServiceType.Both;

            budget.CompanyInfo = CompanyInfo;

            return View(budget);
        }
        /// <summary>
        /// Save modified budget
        /// </summary>
        /// <param name="budget">Budget Model</param>
        [HttpPost]
        public ActionResult EditBudget(BudgetModel budget)
        {
            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();
            try
            {
                budget.CompanyInfo = RetrieveCurrentCompanyInfo(CompanyId);
                budget.Type = Constants.EDIT_BUDGET;

                BusinessLayer.SaveBudget(budget);

                return RedirectToAction("GetBudget", "Home", budget.CompanyInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(budget);
        }

        /// <summary>
        /// Renewal the budget
        /// </summary>
        /// <param name="serviceId">Type of Service</param>
        [HttpPost]
        public JsonResult RenewBudget(int? serviceId)
        {
            JsonResult result;
            try
            {
                BusinessLayer.RenewBudget(CompanyId, serviceId);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;

        }

        /// <summary>
        /// Get Budget Filter Information
        /// </summary>
        /// <param name="companyId">CompanyId</param>
        /// <param name="serviceId">Type of Service</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BudgetFilterInfo(int? companyId, int? serviceId)
        {
            var areaCode = new AreaCodeModel
                {
                    CompanyInfo = RetrieveCurrentCompanyInfo(companyId),
                    AreaCodes = BusinessLayer.GetBudgetFilterInfo(companyId, serviceId)
                };
            return View(areaCode);
        }
    }
}
