using _123Movers.BusinessEntities;
using _123Movers.Models;
using log4net;
using System;
using System.Linq;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    /// <summary>
    /// Move Distance Controller
    /// </summary>
    public class MoveDistanceController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MoveDistanceController));

        /// <summary>
        /// Display the Existing Information
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult MoveDistance(int? companyId, int? serviceId)
        {
            ViewBag.Services = GetServices((serviceId)).Count > 2
                                   ? GetServices(serviceId).Take(2)
                                   : GetServices(serviceId);
            var model = BusinessLayer.GetCompanyMoveDistance(companyId, serviceId);
            model.CompanyInfo = RetrieveCurrentCompanyInfo(companyId);
            return View(model);
        }

        /// <summary>
        /// Save Distance for budget
        /// </summary>
        /// <param name="model"> Move Distance Model</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MoveDistance(MoveDistanceModel model)
        {
            JsonResult result = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            ViewBag.Services = GetServices().Take(2);
            try
            {
                model.CompanyId = CompanyId;
                if (ModelState.IsValid)
                {
                    if (BusinessLayer.SaveMoveDistance(model))
                    {
                        result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }


        /// <summary>
        /// Get the Move Distance information for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public JsonResult GetMoveDistance( int? serviceId)
        {
            return Json(BusinessLayer.GetCompanyMoveDistance(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }

    }
}
