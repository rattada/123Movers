﻿using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class MoveDistanceController : Controller
    {
        //
        // GET: /MoveDistance/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MoveDistance(int? companyId, int? serviceId)
        {

            if (ConfigValues.Services(serviceId).Count > 2)
                ViewBag.Services = ConfigValues.Services(serviceId).Take(2);
            else
                ViewBag.Services = ConfigValues.Services(serviceId);
            List<List<string>> lstGetMoveDistance = ConfigValues.TableToList(BusinessLayer.GetCompanyMoveDistance(companyId, serviceId));
            MoveDistanceModel model = new MoveDistanceModel();
            model._companyInfo = new CompanyModel().CurrentCompany;
            model.CompanyId = companyId;

            if (lstGetMoveDistance.Count > 0)
            {
                model.ServiceId = lstGetMoveDistance[0][0].ToString().IntNullOrEmptyReturn0();
                model.MinMoveDistance = lstGetMoveDistance[0][1].ToString().IntNullOrEmptyReturn0();
                model.MaxMoveDistance = lstGetMoveDistance[0][2].ToString().IntNullOrEmptyReturn0();
            }

            return View(model);

        }


        [HttpPost]
        public JsonResult MoveDistance(MoveDistanceModel model)
        {
            JsonResult result;
            ViewBag.Services = ConfigValues.Services().Take(2);
            try
            {
                model.CompanyId = new CompanyModel().CurrentCompany.CompanyId;
                BusinessLayer.SaveMoveDistance(model);

            }
            catch (Exception ex)
            {

            }

            return result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMoveDistance(int? serviceId)
        {

            DataTable dt = BusinessLayer.GetCompanyMoveDistance(new CompanyModel().CurrentCompany.CompanyId, serviceId);
            List<List<string>> list = ConfigValues.TableToList(dt);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}