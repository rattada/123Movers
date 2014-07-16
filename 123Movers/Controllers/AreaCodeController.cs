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
    public class AreaCodeController : BaseController
    {
        //
        // GET: /AreaCode/
        
       
      
        public JsonResult GetAvailableAreas(int? serviceId)
        {
            var services = BusinessLayer.GetAvailableAreas(CompanyInfo.CompanyId, serviceId);
            List<List<string>> list = ConfigValues.TableToList(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyAreasWithPrices( int? serviceId)
        {
            var services = BusinessLayer.GetCompanyAreasWithPrices(CompanyInfo.CompanyId, serviceId);
            List<List<string>> list = ConfigValues.TableToList(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaCodes(int? serviceId)
        {
            ViewBag.ServiceId = serviceId;
            AreaCodeModel areaCode = new AreaCodeModel();
            areaCode._companyInfo = CompanyInfo;
            return View(areaCode);
        }
        public ActionResult AddAreaCodes(int? serviceId, string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            foreach (var areacode in areacodelist)
            {
                BusinessLayer.AddCompanyAdByArea(CompanyInfo.CompanyId, serviceId, Convert.ToInt16(areacode));
            }
            return RedirectToAction("AreaCodes", new { serviceId = serviceId });
        }

        public ActionResult DeleteAreaCodes(int? serviceId,string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            foreach (var areacode in areacodelist)
            {
                BusinessLayer.DeleteCompanyAdByArea(CompanyInfo.CompanyId, serviceId, Convert.ToInt16(areacode));
            }
            return RedirectToAction("AreaCodes", new { serviceId = serviceId });
        }

        [HttpPost]
        public JsonResult AddCompanyPricePerLead(int? serviceId,string areaCodes)
        {

            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyPricePerLead(CompanyInfo.CompanyId, serviceId, areaCodes, null);
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
