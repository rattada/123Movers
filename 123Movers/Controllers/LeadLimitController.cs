﻿using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class LeadLimitController : Controller
    {
        //
        // GET: /LeadLimit/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LeadLimit(int? serviceId)
        {

            LeadLimitModel model = new LeadLimitModel();

            model = BusinessLayer.GetCompanyLeadLimit(new CompanyModel().CurrentCompany.CompanyId, serviceId);
            model._companyInfo = new CompanyModel().CurrentCompany;
            
            return View(model);
        }


        [HttpPost]
        public JsonResult LeadLimit(List<List<LeadLimitModel>> leadlimit)
        {

            JsonResult result;
            try
            {

                foreach (var ld in leadlimit)
                {
                    ld[0].CompanyId = new CompanyModel().CurrentCompany.CompanyId;
                    BusinessLayer.AddCompanyLeadLimit(ld[0]);
                }
                ModelState.Clear();
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