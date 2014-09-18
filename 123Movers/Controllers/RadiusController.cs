using _123Movers.BusinessEntities;
using System;
using System.Linq;
using System.Web.Mvc;
using _123Movers.Models;
using log4net;

namespace _123Movers.Controllers
{
    /// <summary>
    /// Radius Controller
    /// </summary>
    public class RadiusController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SpecificStatesController));

        /// <summary>
        /// Get Method for Radius
        /// </summary>
        /// <param name="companyId">CompanyId</param>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public ActionResult Radius(int? companyId, int? serviceId)
        {
            var service =GetServices(serviceId);
            ViewBag.Services = service.Count > 2 ? service.Take(2) : service;
            var radius = new RadiusModel
                {
                    CompanyInfo = RetrieveCurrentCompanyInfo(companyId),
                    ServiceId = serviceId ?? (int)ServiceType.Both
                };
            return View(radius);
        }

        /// <summary>
        /// Add the Zip Codes to Company
        /// </summary>
        /// <param name="service">Type of the Service(Local, Long Or Both)</param>
        /// <param name="zipcode">Zip Code</param>
        /// <param name="radius">Radius in miles</param>
        /// <param name="category">Lesser or Gratter</param>
        [HttpPost]
        public JsonResult AddZipCodesByRadius(int? service, int zipcode, decimal radius, string category)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddZipCodesByRadius(CompanyId, service, zipcode, radius, category);
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
        /// Get Zip Codes By Radius.
        /// </summary>
        /// <param name="service">Type of the Service(Local, Long Or Both)</param>
        /// <param name="zipcode">Zip Code</param>
        /// <param name="radius">Radius in miles</param>
        /// <param name="category">Lesser or Gratter</param>
        public JsonResult GetZipCodesByRadius(int? service, int zipcode, decimal radius, string category)
        {
            return Json(BusinessLayer.GetZipCodesByRadius(CompanyId, service, zipcode, radius, category), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Add Area Codes to budget
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
        public JsonResult AddAreaCodes( int? serviceId, string areaCodes)
        {
            JsonResult result;
            try {
                BusinessLayer.AddCompanyAreaCodes( CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}
