using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class DestinationZipCodeController : Controller
    {
        //
        // GET: /DestinationZipCode/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DestinationZipCodes(int? companyId, int? serviceId)
        {
            DestinationZipModel Destination = new DestinationZipModel();

            Destination = BusinessLayer.GetCompanyDestinationServiceAreaCodes(companyId, serviceId);
            Destination._companyInfo = new CompanyModel().CurrentCompany;

            return View(Destination);
        }

        public JsonResult GetCompanyAreasDestinationZipCodes(int? serviceId, int? areaCode)
        {
            var DestinationAreaCodes = BusinessLayer.GetCompanyAreasDestinationZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode);

            List<List<string>> list = ConfigValues.TableToList(DestinationAreaCodes);
            return Json(list, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetAvailableDestinationZipCodes(int? serviceId, int? areaCode)
        {
            var services = BusinessLayer.GetAvailableDestinationZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode);
            List<List<string>> list = ConfigValues.TableToList(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCompanyAreaDestinationZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {

            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyAreaDestinationZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode, zipCodes);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
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
                BusinessLayer.DeleteCompanyAreaDestinationZipCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCode, zipCodes);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

    }
}
