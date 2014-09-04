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
    public class LeadLimitController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(LeadLimitController));

        /// <summary>
        /// Display the Leads Information of the Company
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult LeadLimit(int? serviceId)
        {

            LeadLimitModel model = new LeadLimitModel();

            model = BusinessLayer.GetCompanyLeadLimit(CompanyInfo.CompanyId, serviceId);
            model._companyInfo = CompanyInfo;
            model.ServiceId = serviceId;
            
            return View(model);
        }

        /// <summary>
        /// Save the Leads Information
        /// </summary>
        /// <param name="leadlimit">Lead Limit Model</param>
        [HttpPost]
        public JsonResult LeadLimit(List<List<LeadLimitModel>> leadlimit)
        {

            JsonResult result;
            try
            {

                foreach (var ld in leadlimit)
                {
                    ld[0].CompanyId = CompanyInfo.CompanyId;
                    BusinessLayer.AddCompanyLeadLimit(ld[0]);
                }
                ModelState.Clear();
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
