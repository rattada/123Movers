using _123Movers.BusinessEntities;
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
        //protected ILog logger;
        public MoveWeightController() 
        {
            logger = LogManager.GetLogger(typeof(MoveWeightController)); 
        } 

        [HttpGet]
        public ActionResult MoveWeight(int? serviceId)
        {

            int? cid = CompanyInfo.CompanyId;
            DataSet ds = BusinessLayer.GetMoveWeights(cid, serviceId);

            ViewBag.MinMoveWeight = ConfigValues.DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
            if (ConfigValues.Services(serviceId).Count > 2)
                ViewBag.Services = ConfigValues.Services(serviceId).Take(2);
            else
                ViewBag.Services = ConfigValues.Services(serviceId);
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

        [HttpPost]
        public JsonResult MoveWeight(MoveWeightModel model)
        {
            JsonResult result;
            int? cid = CompanyInfo.CompanyId;
            DataSet ds = BusinessLayer.GetMoveWeights(cid, model.ServiceId);

            ViewBag.MinMoveWeight = ConfigValues.DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
            ViewBag.Services = ConfigValues.Services().Take(2);

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

        public JsonResult GetMoveWeight(int? serviceId)
        {
            DataSet ds = BusinessLayer.GetMoveWeights(CompanyInfo.CompanyId, serviceId);
            return Json(ConfigValues.TableToList(ds.Tables[1]), JsonRequestBehavior.AllowGet);
        }
    }
}
