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
    /// <summary>
    /// Destination Area Code Controller
    /// </summary>
    public class DestinationAreaCodeController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DestinationAreaCodeController)); 

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
        /// <param name="companyId">Company Id</param>
        /// <param name="serviceId">Type of Service(Local, Long and Both)</param>
        /// <returns></returns>
        public ActionResult DestinationAreaCode(int? companyId, int? serviceId)
        {
            var services = GetServices(serviceId);
            ViewBag.Services = services.Count > 2 ? services.Take(2) : services;

            var destAreaCode = new DestinationAreaCodeModel();
            destAreaCode.CompanyInfo = RetrieveCurrentCompanyInfo(companyId);
            return View(destAreaCode);
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
                Logger.Error(ex.ToString());
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
                Logger.Error(ex.ToString());
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
                Logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }


    }
}
