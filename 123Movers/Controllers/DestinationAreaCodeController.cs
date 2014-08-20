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


        public DestinationAreaCodeController()
        {
            logger = LogManager.GetLogger(typeof(SpecificOriginAreaCodesController));
        }

        public JsonResult GetCompanyDestAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetCompanyAreasCodes(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetAllAreaCodes(int? serviceId)
        //{
        //    return Json(BusinessLayer.GetAllAreaCodes(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetCompanyAreas(int? serviceId)
        //{            
        //   return Json(BusinessLayer.GetAvailSpcfcOriginDestAreas(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult DestinationAreaCode(int? serviceId)
        {
            var Services = GetServices(serviceId);
            if (Services.Count > 2)
                ViewBag.Services = Services.Take(2);
            else
                ViewBag.Services = Services;

            DestinationAreaCodeModel DestAreaCode = new DestinationAreaCodeModel();
            DestAreaCode._companyInfo = CompanyInfo;
            return View(DestAreaCode);
        }
        public JsonResult AddCompanyDestAreaCodes(int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyDestAreaCodes(CompanyInfo.CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }
        public JsonResult DeleteCompanyDestAreaCodes(int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanyDestAreaCodes(CompanyInfo.CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public JsonResult Turn_ON_OFF_CompanyDestAreaCodes(int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.Turn_ON_OFF_CompanyDestAreaCodes(CompanyInfo.CompanyId, serviceId, areaCodes.StrReplace());
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
