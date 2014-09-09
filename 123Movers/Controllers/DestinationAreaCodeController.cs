using _123Movers.BusinessEntities;
using _123Movers.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class DestinationAreaCodeController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(DestinationAreaCodeController)); 

        /// <summary>
        /// Get Company Destination Area Codes by service
        /// </summary>
        /// <param name="serviceId">Type of Service</param>
        /// <returns>List of Destination Area Codes</returns>
        public JsonResult GetCompanyDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetCompanyAreasCodes(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Display the Destination Area Codes
        /// </summary>
        /// <param name="serviceId">Type of Service(Local, Long and Both)</param>
        /// <returns></returns>
        public ActionResult DestinationAreaCode(int? companyID, int? serviceId)
        {
            var Services = GetServices(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            DestinationAreaCodeModel DestAreaCode = new DestinationAreaCodeModel();
            DestAreaCode._companyInfo = RetrieveCurrentCompanyInfo(companyID);
            return View(DestAreaCode);
        }
        
        /// <summary>
        /// Add Selected Area Codes to company by Service
        /// </summary>
        /// <param name="serviceId">Type of Service(Local, Long and Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
        public JsonResult AddCompanyDestAreaCodes( int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyDestAreaCodes(CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }
        /// <summary>
        /// Delete Selected Area Codes to company by Service
        /// </summary>
        /// <param name="serviceId">Type of Service(Local, Long and Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
        public JsonResult DeleteCompanyDestAreaCodes(int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanyDestAreaCodes(CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
        /// <summary>
        /// Turn on/off the filter 
        /// </summary>
        /// <param name="serviceId">Type of Service(Local, Long and Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
        public JsonResult Turn_ON_OFF_CompanyDestAreaCodes( int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.Turn_ON_OFF_CompanyDestAreaCodes(CompanyId, serviceId, areaCodes.StrReplace());
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
