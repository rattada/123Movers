using _123Movers.BusinessEntities;
using _123Movers.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    /// <summary>
    /// Move Weight Controller
    /// </summary>
    public class MoveWeightController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MoveWeightController));

        /// <summary>
        /// Display the Existing Data
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult MoveWeight(int? companyId, int? serviceId)
        {

            ViewBag.MinMoveWeight = BusinessLayer.GetMoveSizeLookup().Select(x => new SelectListItem { Value = x.MoveWeightSeq.ToString(), Text = x.MoveWeight.ToString()});
            ViewBag.Services = GetServices(serviceId).Count > 2
                                   ? GetServices(serviceId).Take(2)
                                   : GetServices(serviceId);

            var moveWeight = BusinessLayer.GetMoveWeights(companyId, serviceId);

            moveWeight._companyInfo = RetrieveCurrentCompanyInfo(companyId);

            return View(moveWeight);
        }

        /// <summary>
        /// Save Move Weight for budget
        /// </summary>
        /// <param name="model">Move Weight Model</param>
        [HttpPost]
        public JsonResult MoveWeight(MoveWeightModel model)
        {
            JsonResult result = Json(new { success = false }, JsonRequestBehavior.AllowGet);

            ViewBag.MinMoveWeight = BusinessLayer.GetMoveSizeLookup().Select(x => new SelectListItem { Value = x.MoveWeightSeq.ToString(), Text = x.MoveWeight.ToString() }); 
            ViewBag.Services = GetServices().Take(2);

            try
            {
                model.CompanyId = CompanyId;
                if (BusinessLayer.SaveMoveWeight(model))
                {
                    result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
        /// Get Move Weight Information for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public JsonResult GetMoveWeight( int? serviceId)
        {
            return Json(BusinessLayer.GetMoveWeights(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
    }
}
