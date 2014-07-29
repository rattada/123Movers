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
            return Json(BusinessLayer.GetAvailSpcfcOriginDestAreas(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyAreasDestinationZipCodes(int? serviceId, int? areaCode)
        {
            var DestinationAreaCodes = BusinessLayer.GetCompanyAreasDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(DestinationAreaCodes), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestZipCode(int? serviceId)
        {
            var Services = ConfigValues.Services(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            DestZipCodeModel destZipCodes = new DestZipCodeModel();
            destZipCodes._companyInfo = CompanyInfo;
            destZipCodes.ServiceId = serviceId == null ? Constants.LOCAL : serviceId;
            return View(destZipCodes);
        }


        public JsonResult GetAvailableDestinationZipCodes(int? serviceId, int? areaCode, int? destAreaCode)
        {
            return Json(BusinessLayer.GetAvailableDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, destAreaCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetCompanyAreasCodes(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddCompanyAreaDestinationZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyAreaDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, zipCodes.StrReplace());
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
                BusinessLayer.DeleteCompanyAreaDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, zipCodes.StrReplace());
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
