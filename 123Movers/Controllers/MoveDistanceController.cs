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

            ViewBag.Services = ConfigValues.Services().Take(2);
            DataTable dt = BusinessLayer.GetCompanyMoveDistance(companyId, serviceId);
            //List<List<string>> list = ConfigValues.retListTable(query);
            MoveDistanceModel model = new MoveDistanceModel();
            model._companyInfo = new CompanyModel().CurrentCompany;
            model.CompanyId = companyId;
            if (dt.Rows.Count > 0)
            {
                model.ServiceId = string.IsNullOrWhiteSpace(dt.Rows[0][0].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0][0]);
                model.MinMoveDistance = string.IsNullOrWhiteSpace(dt.Rows[0][1].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0][1]);
                model.MaxMoveDistance = string.IsNullOrWhiteSpace(dt.Rows[0][2].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0][2]);
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

                // ViewBag.Success = "Saved Sucessfully";
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


        //public JsonResult GetCompanyMoveDistance(int ServiceId)
        //{

        //    JsonResult result;

        //    try
        //    {
        //        var query = BusinessLayer.GetCompanyMoveDistance(new CompanyModel().CurrentCompany.CompanyId, ServiceId);
        //        List<List<string>> list = ConfigValues.retListTable(query);

        //        result = Json(list, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
        //    }

        //    return result;


        //}

    }
}
