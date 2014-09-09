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
    public class SpecificOriginAreaCodesController : BaseController
    {
       private static ILog logger = LogManager.GetLogger(typeof(SpecificOriginAreaCodesController)); 
        
        /// <summary>
        /// Get All Specific Origin Destination Area Codes
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
       public JsonResult GetAvailSpcfcOriginDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetAvailSpcfcOriginDestAreas(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Existing Specific Origin Destination Area Codes
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="spcfcareacode"> Selected Area Code</param>
        /// <param name="originAreaCode"></param>
        /// <returns></returns>
       public JsonResult GetCompanySpcfcOriginDestAreas( int? serviceId, int spcfcareacode)
        {
            return Json(BusinessLayer.GetCompanySpcfcOriginDestAreas(CompanyId, serviceId, spcfcareacode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Display the Specific Origin Area Codes by Service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
       public ActionResult SpecificOriginAreaCodes( int? serviceId)
        {
            var Services = GetServices(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            SpecificOriginAreaCode spcfcOriginAreaCodes = new SpecificOriginAreaCode();
            spcfcOriginAreaCodes._companyInfo = RetrieveCurrentCompanyInfo(CompanyId);
            spcfcOriginAreaCodes.ServiceId = serviceId == null ? (int)ServiceType.Local : serviceId; ;
            return View(spcfcOriginAreaCodes);
        }

        /// <summary>
        /// Add Specific Origin Destination Area Codes to Company
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="spcfcareacode">Specific Origin Area Code</param>
        /// <param name="areaCodes">Selected Destincation Area Codes</param>
       public JsonResult AddCompanySpcfcOriginDestAreaCodes( int? serviceId, int spcfcareacode, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanySpcfcOriginAreaCodes(CompanyId, serviceId, spcfcareacode, areaCodes.StrReplace());
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
        /// Delete Specific Origin Destination Area Codes from Company
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="spcfcareacode">Specific Origin Area Code</param>
        /// <param name="areaCodes">Selected Destincation Area Codes</param>
       public JsonResult DeleteCompanySpcfcOriginDestAreaCodes(int? serviceId, int spcfcareacode, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanySpcfcOriginDestAreaCodes(CompanyId, serviceId, spcfcareacode, areaCodes.StrReplace());
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
