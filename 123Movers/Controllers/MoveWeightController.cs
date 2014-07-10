using _123Movers.BusinessEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class MoveWeightController : Controller
    {
        //
        // GET: /MoveWeight/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MoveWeight(int? ServiceId)
        {
            //var moveWeights = DataTableToSelectList(BusinessLayer.GetMoveWeights(), "moveWeightSeq", "moveweight");
            //var selectedItem = moveWeights.First(x => x.Value == "7");

            //ViewBag.MinMoveWeight = new SelectList(moveWeights, "Value", "Text", new { Value = 7 });

            int? cid = new CompanyModel().CurrentCompany.CompanyId;
            DataSet ds = BusinessLayer.GetMoveWeights(cid, ServiceId);

            ViewBag.MinMoveWeight = ConfigValues.DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
            ViewBag.Services = ConfigValues.Services().Take(2);
            MoveWeightModel model = new MoveWeightModel();
            model._companyInfo = new CompanyModel().CurrentCompany;
            model.CompanyId = cid;
            if (ds.Tables[1].Rows.Count > 0)
            {
                model.ServiceId = Convert.ToInt32(ds.Tables[1].Rows[0][1].ToString()); ;
                model.MinMoveWeightSeq = Convert.ToInt32(ds.Tables[1].Rows[0][2].ToString());
                model.MaxMoveWeightSeq = Convert.ToInt32(ds.Tables[1].Rows[0][3].ToString());
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult MoveWeight(MoveWeightModel model)
        {
            JsonResult result;
            int? cid = new CompanyModel().CurrentCompany.CompanyId;
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

            DataSet ds = BusinessLayer.GetMoveWeights(new CompanyModel().CurrentCompany.CompanyId, serviceId);
            List<List<string>> list = ConfigValues.retListTable(ds.Tables[1]);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
