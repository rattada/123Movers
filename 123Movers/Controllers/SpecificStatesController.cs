using _123Movers.BusinessEntities;
using System;
using System.Linq;
using System.Web.Mvc;
using _123Movers.Models;
using log4net;


namespace _123Movers.Controllers
{
    /// <summary>
    /// Specific States Controller
    /// </summary>
    public class SpecificStatesController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SpecificStatesController)); 
        
        /// <summary>
        /// Get All States for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <returns>List of States</returns>
        public JsonResult GetAvailStates( int? serviceId)
        {
            var services = BusinessLayer.GetAvailStates(CompanyId, serviceId);
            return Json(ConfigValues.TableToList(services), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Existing States for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        /// <param name="originState"></param>
        /// <param name="isOriginState"></param>
        /// <returns>List of States</returns>
        public JsonResult GetCompanySpcfcOriginDestStates( int? serviceId, string originState, bool isOriginState)
        {
            return Json(BusinessLayer.GetCompanySpcfcStates(CompanyId, serviceId, originState, isOriginState), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Display the Specific States by Service
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public ActionResult SpecificStates(int? companyId, int? serviceId)
        {
            var service = GetServices(serviceId);
            ViewBag.Services = service.Count > 2 ? service.Take(2) : service;

            var spcfcstates = new SpecificStatesModel
                {
                    _companyInfo = RetrieveCurrentCompanyInfo(companyId),
                    ServiceId = serviceId ?? (int) ServiceType.Local
                };
            return View(spcfcstates);
        }


       /// <summary>
       /// Add Specific States to Company
       /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
       /// <param name="originState">Specific State</param>
       /// <param name="destStates">Selected Destination States</param>
        public JsonResult AddCompanySpcfcOriginDeststates( int? serviceId, string originState, string destStates)
        {
            JsonResult result;
            try
            {

                BusinessLayer.AddCompanySpcfcStates(CompanyId, serviceId, originState, destStates.StrReplace());
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
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
        public JsonResult DeleteCompanySpcfcOriginDeststates( int? serviceId, string originState, string destStates)
        {
            JsonResult result;
            try
            {
                BusinessLayer.DeleteCompanySpcfcStates( CompanyId, serviceId, originState, destStates.StrReplace());
                 result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);

            }
            return result;
        }

    }
}
