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

        private static ILog logger = LogManager.GetLogger(typeof(AreaCodeController)); 

        /// <summary>
        /// Get Available Areas for compnay by service
        /// </summary>
        /// <param name="serviceId">Type of Service</param>
        /// <returns>List of Area Codes</returns>
        public JsonResult GetAvailableAreas(int? serviceId)
        {
            return Json(BusinessLayer.GetAvailableAreas(CompanyInfo.CompanyId, serviceId), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Prices Per Lead
        /// </summary>
        /// <param name="serviceId">Type of Service</param>
        /// <returns>Area Codes With Prices</returns>
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

        /// <summary>
        /// Add Area Codes to Active Budget
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
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
        ///// <summary>
        ///// Delete Area Codes from Active Budget
        ///// </summary>
        ///// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        ///// <param name="areaCodes">Selected Area Codes</param>
        //public ActionResult DeleteAreaCodes(int? serviceId,string areaCodes)
        //{
        //    IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

        //    try
        //    {
        //        foreach (var areacode in areacodelist)
        //        {
        //            BusinessLayer.DeleteCompanyAdByArea(CompanyInfo.CompanyId, serviceId, Convert.ToInt16(areacode));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.ToString());
        //    }
        //    return RedirectToAction("AreaCodes", new { serviceId = serviceId });
        //}

        /// <summary>
        /// Delete Area Codes from Active Budget
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
        public ActionResult DeleteAreaCodes(int? serviceId, string areaCodes)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanyAreaCodes(CompanyInfo.CompanyId, serviceId, areaCodes.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }
        /// <summary>
        /// Add price per lead to budget
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="areaCodes">Selected Area Codes</param>
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
