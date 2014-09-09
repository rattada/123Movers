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
    public class DestinationZipCodeController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(DestinationZipCodeController));
        /// <summary>
        /// Get Method
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult DestinationZipCodes(int? companyId, int? serviceId)
        {
            DestinationZipModel Destination = new DestinationZipModel();

            Destination = BusinessLayer.GetCompanyDestinationServiceAreaCodes(companyId, serviceId);
            Destination._companyInfo = RetrieveCurrentCompanyInfo(companyId);

            return View(Destination);
        }

        /// <summary>
        /// Get Company existing Destincation Zip codes
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Selected Area Code for get the zip codes</param>
        /// <returns>List of Zip Codes by Area Code and Service</returns>
        public JsonResult GetCompanyAreasDestinationZipCodes(int? serviceId, int? areaCode)
        {
            return Json(BusinessLayer.GetCompanyAreasDestinationZipCodes(CompanyId, serviceId, areaCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Company all Destincation Zip codes
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Selected Area Code for get the zip codes</param>
        /// <returns>List of Zip Codes by Area Code and Service</returns>
        public JsonResult GetAvailableDestinationZipCodes(int? serviceId, int? areaCode)
        {
            return Json(BusinessLayer.GetAvailableDestinationZipCodes(CompanyId, serviceId, areaCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add Zip codes to company by service and area code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Destination Area Code</param>
        /// <param name="zipCodes">Selected Zip Codes</param>
        [HttpPost]
        public JsonResult AddCompanyAreaDestinationZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyAreaDestinationZipCodes(CompanyId, serviceId, areaCode, zipCodes.StrReplace());
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
        /// Delete Zip codes to company by service and area code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Destination Area Code</param>
        /// <param name="zipCodes">Selected Zip Codes</param>
        [HttpPost]
        public JsonResult DeleteCompanyAreaDestinationZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanyAreaDestinationZipCodes(CompanyId, serviceId, areaCode, zipCodes.StrReplace());
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
