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
    public class MoveDistanceController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(MoveDistanceController));

        /// <summary>
        /// Display the Existing Information
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult MoveDistance(int? companyID, int? serviceId)
        {
            if (GetServices(serviceId).Count > 2)
                ViewBag.Services =GetServices(serviceId).Take(2);
            else
                ViewBag.Services =GetServices(serviceId);

            MoveDistanceModel model = BusinessLayer.GetCompanyMoveDistance(companyID, serviceId);
            model._companyInfo = RetrieveCurrentCompanyInfo(companyID);
            return View(model);

        }

        /// <summary>
        /// Save Modified Data
        /// </summary>
        /// <param name="model">Move Distance Model</param>
        //[HttpPost]
        //public JsonResult MoveDistance(MoveDistanceModel model)
        //{
        //    JsonResult result;
        //    ViewBag.Services = GetServices().Take(2);
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            model.CompanyId = CompanyInfo.CompanyId;
        //            BusinessLayer.SaveMoveDistance(model);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.ToString());
        //    }

        //    return result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult MoveDistance(MoveDistanceModel model)
        {
            JsonResult result = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            ViewBag.Services = GetServices().Take(2);
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = BusinessLayer.SaveMoveDistance(model);
                    if (success)
                    {
                        result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
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
        /// Get the Move Distance information for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public JsonResult GetMoveDistance(int? companyID, int? serviceId)
        {
            return Json(BusinessLayer.GetCompanyMoveDistance(companyID, serviceId), JsonRequestBehavior.AllowGet);
        }

    }
}
