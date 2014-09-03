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
    public class SpecificStatesController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(SpecificStatesController)); 
        
        /// <summary>
        /// Get All States for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <returns>List of States</returns>
        public JsonResult GetAvailStates(int? serviceId)
        {
            var services = BusinessLayer.GetAvailStates(CompanyInfo.CompanyId, serviceId);
            return Json(ConfigValues.TableToList(services), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Existing States for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <returns>List of States</returns>
        public JsonResult GetCompanySpcfcOriginDestStates(int? serviceId, string originState, bool IsOriginState)
        {
            return Json(BusinessLayer.GetCompanySpcfcStates(CompanyInfo.CompanyId, serviceId, originState, IsOriginState), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Display the Specific States by Service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public ActionResult SpecificStates(int? serviceId)
        {
            var Service = GetServices(serviceId);
            if (Service.Count > 2)
                ViewBag.Services = Service.Take(2);
            else
                ViewBag.Services = Service;

            SpecificStatesModel spcfcstates = new SpecificStatesModel();
            spcfcstates._companyInfo = CompanyInfo;
            spcfcstates.ServiceId = serviceId == null ? (int)ServiceType.Local : serviceId;
            return View(spcfcstates);
        }


       /// <summary>
       /// Add Specific States to Company
       /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
       /// <param name="originState">Specific State</param>
       /// <param name="destStates">Selected Destination States</param>
        public JsonResult AddCompanySpcfcOriginDeststates(int? serviceId, string originState, string destStates)
        {
            JsonResult result;
            try
            {

                BusinessLayer.AddCompanySpcfcStates(CompanyInfo.CompanyId, serviceId, originState, destStates.StrReplace());
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
        /// Delete Specific States to Company
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="originState">Specific State</param>
        /// <param name="destStates">Selected Destination States</param>
        public JsonResult DeleteCompanySpcfcOriginDeststates(int? serviceId, string originState, string destStates)
        {
            JsonResult result;

            try
            {
                BusinessLayer.DeleteCompanySpcfcStates(CompanyInfo.CompanyId, serviceId, originState, destStates.StrReplace());
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
