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
    /// Origin Zip Code Controller
    /// </summary>
    public class OriginZipCodeController : BaseController
    {
       private static readonly ILog Logger = LogManager.GetLogger(typeof(OriginZipCodeController));

        /// <summary>
        /// Display the Origin Zip Codes for Company by Service
        /// </summary>
       /// <param name="companyId">Company Id</param>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
       public ActionResult OriginZipCodes(int? companyId, int? serviceId)
        {
            var origin = BusinessLayer.GetCompanyServiceAreaCodes(companyId, serviceId);
            origin.CompanyInfo = RetrieveCurrentCompanyInfo(companyId);
            return View(origin);
        }

        /// <summary>
        /// Get Existing Origin Zip Codes for Service by Area Code
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCode">Selected Area Code</param>
        /// <retun>List of Origin Zip Codes</retun>
        public JsonResult GetAreaCodeZipCodes(int? serviceId, int? areaCode)
        {
            var originAreaCodes = BusinessLayer.GetCompanyAreasZipCodes(CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(originAreaCodes), JsonRequestBehavior.AllowGet);
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
                Logger.Error(ex.ToString());
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
                Logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

    }
}
