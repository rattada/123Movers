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

            //DataSet ds = BusinessLayer.GetMoveWeights(cid, serviceId);

            //ViewBag.MinMoveWeight = DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");

            ViewBag.MinMoveWeight = BusinessLayer.GetMoveSizeLookup().Select(x => new SelectListItem { Value = x.MoveWeightSeq.ToString(), Text = x.MoveWeight.ToString()});

            var _moveWeight = BusinessLayer.GetMoveWeights(companyID, serviceId);

            if (GetServices(serviceId).Count > 2)
                ViewBag.Services = GetServices(serviceId).Take(2);
            else
                ViewBag.Services = GetServices(serviceId);

            _moveWeight._companyInfo = RetrieveCurrentCompanyInfo(companyID);

            return View(_moveWeight);
        }
        /// <summary>
        /// Save Move Weight Data
        /// </summary>
        /// <param name="model">Move Weight Model</param>
        //[HttpPost]
        //public JsonResult MoveWeight(MoveWeightModel model)
        //{
        //    JsonResult result;
        //    int? cid = CompanyInfo.CompanyId;
        //   // DataSet ds = BusinessLayer.GetMoveWeights(cid, model.ServiceId);

        //    ViewBag.MinMoveWeight = BusinessLayer.GetMoveWeights(cid, model.ServiceId); // DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
        //    ViewBag.Services = GetServices().Take(2);

        //    try
        //    {
        //        model.CompanyId = cid;
        //        BusinessLayer.SaveMoveWeight(model);
        //    }
        //    catch
        //    {
        //    }
        //    return result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult MoveWeight(MoveWeightModel model)
        {
            JsonResult result = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            // DataSet ds = BusinessLayer.GetMoveWeights(cid, model.ServiceId);

            ViewBag.MinMoveWeight = BusinessLayer.GetMoveWeights(model.CompanyId, model.ServiceId); // DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
            ViewBag.Services = GetServices().Take(2);

            try
            {
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
        public JsonResult GetMoveWeight(int? companyID, int? serviceId)
        {
            //DataSet ds = BusinessLayer.GetMoveWeights(CompanyInfo.CompanyId, serviceId);
            return Json(BusinessLayer.GetMoveWeights(companyID, serviceId), JsonRequestBehavior.AllowGet);
        }
    }
}
