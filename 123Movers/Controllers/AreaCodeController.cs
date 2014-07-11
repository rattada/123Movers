using _123Movers.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using _123Movers.Models;

namespace _123Movers.Controllers
{
    public class AreaCodeController : Controller
    {
        //
        // GET: /AreaCode/

        public ActionResult Index()
        {
            return View();
        }

      
        public JsonResult GetAvailableAreas(int? companyId, int? serviceId)
        {
            var services = BusinessLayer.GetAvailableAreas(companyId, serviceId);
            List<List<string>> list = ConfigValues.TableToList(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyAreasWithPrices(int? companyId, int? serviceId)
        {
            var services = BusinessLayer.GetCompanyAreasWithPrices(companyId, serviceId);
            List<List<string>> list = ConfigValues.TableToList(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaCodes(int? companyId, int? serviceId, string companyName)
        {
            ViewBag.CompanyID = companyId;
            ViewBag.CompanyName = companyName;
            ViewBag.ServiceID = serviceId;
            AreaCodeModel areaCode = new AreaCodeModel();
            areaCode._companyInfo = new CompanyModel().CurrentCompany;
            return View(areaCode);
        }
        public ActionResult AddAreaCodes(int? companyId, int? serviceId, string companyName, string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            foreach (var areacode in areacodelist)
            {
                BusinessLayer.AddCompanyAdByArea(companyId, serviceId, Convert.ToInt16(areacode));
            }
            return RedirectToAction("AreaCodes", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        }

        public ActionResult DeleteAreaCodes(int? companyId, int? serviceId, string companyName, string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            foreach (var areacode in areacodelist)
            {
                BusinessLayer.DeleteCompanyAdByArea(companyId, serviceId, Convert.ToInt16(areacode));
            }
            return RedirectToAction("AreaCodes", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        }

        [HttpPost]
        public JsonResult AddCompanyPricePerLead(int? companyId, int? serviceId, string companyName, string areaCodes)
        {

            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyPricePerLead(companyId, serviceId, areaCodes, null);
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
