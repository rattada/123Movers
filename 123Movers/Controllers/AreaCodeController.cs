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
    public class AreaCodeController : BaseController
    {
        //
        // GET: /AreaCode/

        //protected readonly ILog logger = LogManager.GetLogger(typeof(AreaCodeController)); 

        public AreaCodeController() 
        {
            logger = LogManager.GetLogger(typeof(AreaCodeController)); 
        }
      
        public JsonResult GetAvailableAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetAvailableAreas(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyAreasWithPrices( int? serviceId)
        {
            var services = BusinessLayer.GetCompanyAreasWithPrices(CompanyInfo.CompanyId, serviceId);
            return Json(ConfigValues.TableToList(services), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaCodes(int? serviceId)
        {
            ViewBag.ServiceId = serviceId;
            AreaCodeModel areaCode = new AreaCodeModel();
            areaCode._companyInfo = CompanyInfo;
            return View(areaCode);
        }
        //public ActionResult AddAreaCodes(int? serviceId, string areaCodes)
        //{
        //    IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);
        //    try
        //    {
        //        foreach (var areacode in areacodelist)
        //        {
        //            BusinessLayer.AddCompanyAdByArea(CompanyInfo.CompanyId, serviceId, Convert.ToInt16(areacode));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.ToString());
        //    }
            
        //    return RedirectToAction("AreaCodes", new { serviceId = serviceId });
        //}

        public JsonResult AddAreaCodes(int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyAreaCodes(CompanyInfo.CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        public ActionResult DeleteAreaCodes(int? serviceId,string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            try
            {
                foreach (var areacode in areacodelist)
                {
                    BusinessLayer.DeleteCompanyAdByArea(CompanyInfo.CompanyId, serviceId, Convert.ToInt16(areacode));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return RedirectToAction("AreaCodes", new { serviceId = serviceId });
        }

        [HttpPost]
        public JsonResult AddCompanyPricePerLead(int? serviceId,string areaCodes)
        {

            JsonResult result;
            try
            {
                BusinessLayer.AddCompanyPricePerLead(CompanyInfo.CompanyId, serviceId, areaCodes.StrReplace(), null);
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
