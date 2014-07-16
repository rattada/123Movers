using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class DestinationZipCodeController : BaseController
    {
        //
        // GET: /DestinationZipCode/

        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DestinationZipCodes(int? serviceId)
        {
            DestinationZipModel Destination = new DestinationZipModel();

            Destination = BusinessLayer.GetCompanyDestinationServiceAreaCodes(CompanyInfo.CompanyId, serviceId);
            Destination._companyInfo = CompanyInfo;

            return View(Destination);
        }

        public JsonResult GetCompanyAreasDestinationZipCodes(int? serviceId, int? areaCode)
        {
            var DestinationAreaCodes = BusinessLayer.GetCompanyAreasDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode);

            List<List<string>> list = ConfigValues.TableToList(DestinationAreaCodes);
            return Json(list, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetAvailableDestinationZipCodes(int? serviceId, int? areaCode)
        {
            var services = BusinessLayer.GetAvailableDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode);
            List<List<string>> list = ConfigValues.TableToList(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCompanyAreaDestinationZipCodes(int? serviceId, int? areaCode, string zipCodes)
        {

            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyAreaDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, zipCodes);
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
                BusinessLayer.DeleteCompanyAreaDestinationZipCodes(CompanyInfo.CompanyId, serviceId, areaCode, zipCodes);
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
