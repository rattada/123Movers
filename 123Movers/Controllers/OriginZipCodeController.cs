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
    public class OriginZipCodeController : BaseController
    {
       private static ILog logger = LogManager.GetLogger(typeof(OriginZipCodeController)); 

        /// <summary>
        /// Display the Origin Zip Codes for Company by Service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
       public ActionResult OriginZipCodes(int? companyID, int? serviceId)
        {
            OriginZipCodeModel Origin = new OriginZipCodeModel();
            Origin = BusinessLayer.GetCompanyServiceAreaCodes(companyID, serviceId);
            Origin._companyInfo = RetrieveCurrentCompanyInfo(companyID);
            return View(Origin);
        }

        /// <summary>
        /// Get Existing Origin Zip Codes for Service by Area Code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Selected Area Code</param>
        /// <retun>List of Origin Zip Codes</retun>
        public JsonResult GetAreaCodeZipCodes(int? serviceId, int? areaCode)
        {
            var OriginAreaCodes = BusinessLayer.GetCompanyAreasZipCodes(CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(OriginAreaCodes), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get All Origin Zip Codes for Service by Area Code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Selected Area Code</param>
        /// <retun>List of Origin Zip Codes</retun>
        public JsonResult GetAvailableZipCodes( int? serviceId, int? areaCode)
        {
            var services = BusinessLayer.GetAvailableZipCodes(CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(services), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add Origin Zip codes to company by service and area code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Origin Area Code</param>
        /// <param name="zipCodes">Selected Zip Codes</param>
        [HttpPost]
        public JsonResult AddCompanyAreaZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyAreaZipCodes(CompanyId, serviceId, areaCode, zipCodes.StrReplace());
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
        /// Delete Origin Zip codes to company by service and area code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Origin Area Code</param>
        /// <param name="zipCodes">Selected Zip Codes</param>
        [HttpPost]
        public JsonResult DeleteCompanyAreaZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanyAreaZipCodes(CompanyId, serviceId, areaCode, zipCodes.StrReplace());
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
