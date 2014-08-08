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
        public SpecificOriginAreaCodesController() 
        {
            logger = LogManager.GetLogger(typeof(SpecificOriginAreaCodesController)); 
        }
        // GET: /SpecificOriginAreaCode/
        public JsonResult GetAvailSpcfcOriginDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetAvailSpcfcOriginDestAreas(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanySpcfcOriginDestAreas(int? serviceId,int spcfcareacode,bool originAreaCode)
        {
            return Json(BusinessLayer.GetCompanySpcfcOriginDestAreas(CompanyInfo.CompanyId, serviceId, spcfcareacode, originAreaCode), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SpecificOriginAreaCodes(int? serviceId)
        {
            var Services = GetServices(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            SpecificOriginAreaCode spcfcOriginAreaCodes = new SpecificOriginAreaCode();
            spcfcOriginAreaCodes._companyInfo = CompanyInfo;
            spcfcOriginAreaCodes.ServiceId = serviceId;
            return View(spcfcOriginAreaCodes);
        }
        public JsonResult AddCompanySpcfcOriginDestAreaCodes(int? serviceId,int spcfcareacode, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanySpcfcOriginAreaCodes(CompanyInfo.CompanyId, serviceId, spcfcareacode, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }
        public JsonResult DeleteCompanySpcfcOriginDestAreaCodes(int? serviceId, int spcfcareacode, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanySpcfcOriginDestAreaCodes(CompanyInfo.CompanyId, serviceId,spcfcareacode, areaCodes.StrReplace());
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
