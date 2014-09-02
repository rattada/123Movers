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
            //IEnumerable<BudgetModel> budgetList = new List<BudgetModel>();
            //SearchModel search = new SearchModel();

            SaveCompanyInfo(Company);

            //budgetList = BusinessLayer.GetBudget(CompanyInfo.CompanyId);

            var _currentBudgets = BusinessLayer.GetCureentBudgets(CompanyInfo.CompanyId);
            var _pastBudgets = BusinessLayer.GetPastBudgets(CompanyInfo.CompanyId);

            budget._currentBudgets = _currentBudgets;
            budget._pastBudgets = _pastBudgets;

            budget._companyInfo = CompanyInfo;
            //var tbilled = budgetList.Where(b => b.EndDate < DateTime.Now).Sum(b => b.TotalBudget);
            //var uamount = budgetList.Where(b => b.EndDate < DateTime.Now).Sum(b => b.UnchargedAmount);

            //ViewBag.TotalBilled =  String.Format("{0:C}", tbilled);
            //ViewBag.UnchargedAmount = String.Format("{0:C}", uamount);

            //search._companyInfo = CompanyInfo;
            //search.budget = budgetList;

            return View(budget);
        }

        
        [HttpGet]
        public ActionResult AddBudget()
        {
            ViewBag.Terms = GetTerms();// ConfigValues.Terms();
            ViewBag.Services = GetServices();

            BudgetModel budget = new BudgetModel();

            budget._companyInfo = CompanyInfo;
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
                    budget._companyInfo = CompanyInfo;
                    budget.CompanyId = budget._companyInfo.CompanyId;
                    budget.Type = Constants.NEW_BUDGET;

                    BusinessLayer.SaveBudget(budget);

                    return RedirectToAction("GetBudget", budget._companyInfo);
                }
            }
            catch (Exception ex)
            {
                //logger.Debug("Here is a debug log.");
                //logger.Info("... and an Info log.");
                //logger.Warn("... and a warning.");
                //logger.Error("... and an error.");
                //logger.Fatal("... and a fatal error.");
                logger.Error(ex.ToString());
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(budget);


        }
        

        [HttpGet]
        public ActionResult EditBudget(decimal? TotalBudget, bool IsRecurring, bool IsRequireNoticeToCharge, int? serviceId, string agnumber, int? minDaysToCharge, int? id)
        {

            BudgetModel budget = new BudgetModel();
            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();
           
            string Recurring = (IsRecurring) ? (IsRequireNoticeToCharge) ? Constants.RecurringWithNotice : Constants.Recurring : Constants.NonRecurring;
            budget.TotalBudget = TotalBudget;
            budget.IsRecurring = IsRecurring;
            budget.IsRequireNoticeToCharge = IsRequireNoticeToCharge;
            budget.ServiceId = serviceId == null ? (int)ServiceType.Both : serviceId;
            budget.MinDaysToCharge = minDaysToCharge;
            budget.AgreementNumber = agnumber;
            budget.TermType = Recurring;

            SaveSeviceId(budget.ServiceId);
           
            budget._companyInfo = CompanyInfo;
            budget.CompanyId = budget._companyInfo.CompanyId;

            return View(budget);
        }

        [HttpPost]
        public ActionResult EditBudget(BudgetModel budget)
        {

            ViewBag.Terms = GetTerms();
            ViewBag.Services = GetServices();

            try
            {
                budget._companyInfo = CompanyInfo;
                budget.CompanyId = budget._companyInfo.CompanyId;
                budget.BudgetAction = Constants.RENEWL_BUDGET;
                budget.Type = Constants.EDIT_BUDGET;
                budget.ServiceId = ServiceId;

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
        /// <summary>
        /// Get Budget filter information
        /// </summary>
       
        //public JsonResult GetBudgetFilterInfo()
        //{
        //    return Json(BusinessLayer.GetBudgetFilterInfo(CompanyInfo.CompanyId, ServiceId), JsonRequestBehavior.AllowGet);
        //}
       
    }
}
