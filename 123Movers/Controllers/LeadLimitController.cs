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
    /// <summary>
    /// Lead Limit Controller
    /// </summary>
    public class LeadLimitController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LeadLimitController));

        /// <summary>
        /// Display the Leads Information of the Company
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult LeadLimit(int? companyId, int? serviceId)
        {

            var model = BusinessLayer.GetCompanyLeadLimit(companyId, serviceId);
            model._companyInfo = RetrieveCurrentCompanyInfo(companyId);
            model.ServiceId = serviceId;
            
            return View(model);
        }

        /// <summary>
        /// Save the Leads Information
        /// </summary>
        /// <param name="leadlimit">Lead Limit Model</param>
        [HttpPost]
        public JsonResult LeadLimit( List<List<LeadLimitModel>> leadlimit)
        {
            JsonResult result;
            try
            {
                foreach (var ld in leadlimit)
                {
                   ld[0].CompanyId = CompanyId;
                   BusinessLayer.AddCompanyLeadLimit(ld[0]);
                }
                ModelState.Clear();
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
