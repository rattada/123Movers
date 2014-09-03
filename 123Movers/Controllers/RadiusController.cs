using _123Movers.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using _123Movers.Models;
using log4net;

namespace _123Movers.Controllers
{
    public class RadiusController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(SpecificStatesController)); 

        /// <summary>
        /// Get Method for Radius
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public ActionResult Radius(int? serviceId)
        {
            var Service =GetServices(serviceId);
            if (Service.Count > 2)
                ViewBag.Services = Service.Take(2);
            else
                ViewBag.Services = Service;
            RadiusModel radius = new RadiusModel();
            radius._companyInfo = CompanyInfo;
            radius.ServiceId = serviceId == null ? (int)ServiceType.Both : serviceId;// Constants.BOTH : serviceId;
            return View(radius);
        }

        /// <summary>
        /// Add the Zip Codes to Company
        /// </summary>
        /// <param name="service"></param>
        /// <param name="zipcode"></param>
        /// <param name="radius"></param>
        /// <param name="category"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddZipCodesByRadius(int? service, int zipcode, decimal radius, string category)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddZipCodesByRadius(CompanyInfo.CompanyId, service, zipcode, radius, category);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            
            }
            return result;
        }

        public JsonResult GetZipCodesByRadius(int? service, int zipcode, decimal radius, string category)
        {
            return Json(BusinessLayer.GetZipCodesByRadius(CompanyInfo.CompanyId, service, zipcode, radius, category), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddAreaCodes(int? ServiceId, string areaCodes)
        {
            JsonResult result;
            try {
                BusinessLayer.AddCompanyAreaCodes(CompanyInfo.CompanyId, ServiceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}
