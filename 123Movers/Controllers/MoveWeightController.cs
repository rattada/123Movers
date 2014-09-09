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
    public class MoveWeightController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(MoveWeightController));

        /// <summary>
        /// Display the Existing Data
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult MoveWeight(int? companyID, int? serviceId)
        {

            ViewBag.MinMoveWeight = BusinessLayer.GetMoveSizeLookup().Select(x => new SelectListItem { Value = x.MoveWeightSeq.ToString(), Text = x.MoveWeight.ToString()});

            var _moveWeight = BusinessLayer.GetMoveWeights(companyID, serviceId);

            if (GetServices(serviceId).Count > 2)
                ViewBag.Services = GetServices(serviceId).Take(2);
            else
                ViewBag.Services = GetServices(serviceId);

            _moveWeight._companyInfo = RetrieveCurrentCompanyInfo(companyID);

            return View(_moveWeight);
        }

        [HttpPost]
        public JsonResult MoveWeight(MoveWeightModel model)
        {
            JsonResult result = Json(new { success = false }, JsonRequestBehavior.AllowGet);

            ViewBag.MinMoveWeight = BusinessLayer.GetMoveSizeLookup().Select(x => new SelectListItem { Value = x.MoveWeightSeq.ToString(), Text = x.MoveWeight.ToString() }); 
            ViewBag.Services = GetServices().Take(2);

            try
            {
                model.CompanyId = CompanyId;
                bool success = BusinessLayer.SaveMoveWeight(model);
                if (success)
                {
                    result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
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
