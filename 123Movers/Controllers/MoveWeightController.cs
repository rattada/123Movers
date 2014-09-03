﻿using _123Movers.BusinessEntities;
using _123Movers.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class MoveWeightController : BaseController
    {
        private static ILog logger = LogManager.GetLogger(typeof(MoveWeightController));

        /// <summary>
        /// Display the Existing Data
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        [HttpGet]
        public ActionResult MoveWeight(int? serviceId)
        {

            int? cid = CompanyInfo.CompanyId;
            DataSet ds = BusinessLayer.GetMoveWeights(cid, serviceId);

            ViewBag.MinMoveWeight = DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
            if (GetServices(serviceId).Count > 2)
                ViewBag.Services = GetServices(serviceId).Take(2);
            else
                ViewBag.Services = GetServices(serviceId);
            MoveWeightModel model = new MoveWeightModel();
            model._companyInfo = CompanyInfo;
            model.CompanyId = cid;
            if (ds.Tables[1].Rows.Count > 0)
            {
                model.ServiceId = ds.Tables[1].Rows[0][1].ToString().IntNullOrEmpty();
                model.MinMoveWeightSeq = ds.Tables[1].Rows[0][2].ToString().IntNullOrEmpty();
                model.MaxMoveWeightSeq = ds.Tables[1].Rows[0][3].ToString().IntNullOrEmpty();
            }

            return View(model);
        }
        /// <summary>
        /// Save Move Weight Data
        /// </summary>
        /// <param name="model">Move Weight Model</param>
        [HttpPost]
        public JsonResult MoveWeight(MoveWeightModel model)
        {
            JsonResult result;
            int? cid = CompanyInfo.CompanyId;
            DataSet ds = BusinessLayer.GetMoveWeights(cid, model.ServiceId);

            ViewBag.MinMoveWeight = DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
            ViewBag.Services = GetServices().Take(2);

            try
            {
                model.CompanyId = cid;
                BusinessLayer.SaveMoveWeight(model);
            }
            catch
            {
            }
            return result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Move Weight Information for company by service
        /// </summary>
        /// <param name="serviceId">Type of the Service(Local, Long Or Both)</param>
        public JsonResult GetMoveWeight(int? serviceId)
        {
            DataSet ds = BusinessLayer.GetMoveWeights(CompanyInfo.CompanyId, serviceId);
            return Json(ConfigValues.TableToList(ds.Tables[1]), JsonRequestBehavior.AllowGet);
        }
    }
}
