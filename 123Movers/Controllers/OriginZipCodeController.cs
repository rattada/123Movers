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
       // protected ILog logger;
        public OriginZipCodeController() 
        {
            logger = LogManager.GetLogger(typeof(OriginZipCodeController)); 
        } 

        [HttpGet]
        public ActionResult OriginZipCodes(int? serviceId)
        {
            OriginZipCodeModel Origin = new OriginZipCodeModel();

            Origin = BusinessLayer.GetCompanyServiceAreaCodes(CompanyInfo.CompanyId, serviceId);
            Origin._companyInfo = CompanyInfo;

            return View(Origin);
        }

        public JsonResult GetAreaCodeZipCodes(int? serviceId, int? areaCode)
        {
            var OriginAreaCodes = BusinessLayer.GetCompanyAreasZipCodes(CompanyInfo.CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(OriginAreaCodes), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAvailableZipCodes(int? serviceId, int? areaCode)
        {
            var services = BusinessLayer.GetAvailableZipCodes(CompanyInfo.CompanyId, serviceId, areaCode);
            return Json(ConfigValues.TableToList(services), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCompanyAreaZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {

            JsonResult result;
            try
            {

                BusinessLayer.AddCompanyAreaZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, zipCodes);
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
        public JsonResult DeleteCompanyAreaZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {

            JsonResult result;
            try
            {

                BusinessLayer.DeleteCompanyAreaZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, zipCodes);
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
