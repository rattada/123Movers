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
    public class DestZipCodeController : BaseController
    {
        //
        // GET: /DestZipCode/
        public DestZipCodeController()
        {
            logger = LogManager.GetLogger(typeof(SpecificOriginAreaCodesController));
        }

        public JsonResult GetAvailDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetAvailSpcfcOriginDestAreas(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllAreaCodes(int? serviceId)
        {
            return Json(BusinessLayer.GetAllAreaCodes(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyAreasDestinationZipCodes(int? serviceId, int? areaCode)
        {
            return Json(BusinessLayer.GetCompanyAreasDestinationZipCodes(CompanyId, serviceId, areaCode), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestZipCode(int? companyId, int? serviceId)
        {
            

            var Services =GetServices(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            DestZipCodeModel destZipCodes = new DestZipCodeModel();
            destZipCodes._companyInfo = RetrieveCurrentCompanyInfo(companyId);
            destZipCodes.ServiceId = serviceId == null ? (int)ServiceType.Local : serviceId;
            return View(destZipCodes);
        }


        public JsonResult GetAvailableDestinationZipCodes(int? serviceId, int? areaCode, int? destAreaCode)
        {
            return Json(BusinessLayer.GetAvailableDestinationZipCodes(CompanyId, serviceId, areaCode, destAreaCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetCompanyAreasCodes(CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }


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
