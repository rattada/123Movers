using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class OriginZipCodeController : Controller
    {
        //
        // GET: /OriginZipCode/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult OriginZipCodes(int? companyId, int? serviceId)
        {
            OriginZipCodeModel Origin = new OriginZipCodeModel();

            Origin = BusinessLayer.GetCompanyServiceAreaCodes(companyId, serviceId);
            Origin._companyInfo = new CompanyModel().CurrentCompany;

            return View(Origin);
        }

        public JsonResult GetAreaCodeZipCodes(int? serviceId, int? areaCode)
        {
            var OriginAreaCodes = BusinessLayer.GetCompanyAreasZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode);

            List<List<string>> list = ConfigValues.retListTable(OriginAreaCodes);
            return Json(list, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetAvailableZipCodes(int? serviceId, int? areaCode)
        {
            var services = BusinessLayer.GetAvailableZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode);
            List<List<string>> list = ConfigValues.retListTable(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCompanyAreaZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {

            JsonResult result;
            try
            {

                BusinessLayer.AddCompanyAreaZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode, zipCodes);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
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

                BusinessLayer.DeleteCompanyAreaZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode, zipCodes);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        //[HttpPost]
        //public JsonResult AddCompanyZipCodesPerAreaCodes(int serviceId, string areaCodes, int IsOrigin)
        //{

        //    JsonResult result;
        //    try
        //    {

        //        BusinessLayer.AddCompanyZipCodesPerAreaCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCodes, IsOrigin);
        //        result = Json(new { success = true }, JsonRequestBehavior.AllowGet);


        //    }
        //    catch (Exception ex)
        //    {
        //        result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
        //    }

        //    return result;

        //    // return RedirectToAction("ManageAreaCodes", "Home", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        //}

    }
}
