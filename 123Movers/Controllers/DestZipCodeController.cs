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
        public ActionResult DestZipCode(int? serviceId)
        {
            var Services = ConfigValues.Services(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            DestZipCodeModel destZipCodes = new DestZipCodeModel();
            destZipCodes._companyInfo = CompanyInfo;
            return View(destZipCodes);
        }


        public JsonResult GetAvailableDestinationZipCodes(int? serviceId, int? areaCode)
        {
            var services = BusinessLayer.GetAvailableDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(services), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanySpcfcOriginDestAreas(int? serviceId,int spcfcareacode,bool originAreaCode)
        {
            return Json(BusinessLayer.GetCompanySpcfcOriginDestAreas(CompanyInfo.CompanyId, serviceId, spcfcareacode, originAreaCode), JsonRequestBehavior.AllowGet);
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
