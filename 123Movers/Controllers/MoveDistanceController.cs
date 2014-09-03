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
        public ActionResult MoveDistance(int? serviceId)
        {

            if (GetServices(serviceId).Count > 2)
                ViewBag.Services =GetServices(serviceId).Take(2);
            else
                ViewBag.Services =GetServices(serviceId);
            List<List<string>> lstGetMoveDistance = ConfigValues.TableToList(BusinessLayer.GetCompanyMoveDistance(CompanyInfo.CompanyId, serviceId));
            MoveDistanceModel model = new MoveDistanceModel();
            model._companyInfo = CompanyInfo;
            model.CompanyId = model._companyInfo.CompanyId;

            if (lstGetMoveDistance.Count > 0)
            {
                model.ServiceId = lstGetMoveDistance[0][0].ToString().IntNullOrEmptyReturn0();
                model.MinMoveDistance = lstGetMoveDistance[0][1].ToString().IntNullOrEmptyReturn0();
                model.MaxMoveDistance = lstGetMoveDistance[0][2].ToString().IntNullOrEmptyReturn0();
            }

            return View(model);

        }

        /// <summary>
        /// Save Modified Data
        /// </summary>
        /// <param name="model">Move Distance Model</param>
        [HttpPost]
        public JsonResult MoveDistance(MoveDistanceModel model)
        {
            JsonResult result;
            ViewBag.Services = GetServices().Take(2);
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = CompanyInfo.CompanyId;
                    BusinessLayer.SaveMoveDistance(model);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get the Move Distance information for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public JsonResult GetMoveDistance(int? serviceId)
        {
            DataTable dt = BusinessLayer.GetCompanyMoveDistance(CompanyInfo.CompanyId, serviceId);
            return Json(ConfigValues.TableToList(dt), JsonRequestBehavior.AllowGet);
        }

    }
}
