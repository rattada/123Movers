using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;
using _123Movers.BusinessEntities;
using System.Data;
using System.Web.Script.Serialization;

namespace _123Movers.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Notifications()
        {
            return View();
        }


      

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchModel search)
        {
            try
            {
                if (search.CompanyId == null && search.CompanyName == null && search.InsertionOrderId == null && search.AX == null)
                {
                    return View(search);
                }
                
                var companies = BusinessLayer.SearchCompany(search);
                search.Companies = companies;
                //if (budget.Count() > 0)
                //{
                //    foreach (var b in budget)
                //    {
                //        HttpContext.Application["CompanyId"] = b.CompanyId;
                //        HttpContext.Application["CompanyName"] = b.CompanyName;
                //        HttpContext.Application["Ax"] = b.AX;
                //        HttpContext.Application["IsActive"] = b.IsActive;
                //        //HttpContext.Application["DisplayName"] = b.DisplayName;
                //        HttpContext.Application["ContactPerson"] = b.ContactPerson;
                //        // HttpContext.Application["CompanyHandle"] = b.CompanyHandle;
                //        break;
                //    }

                //}

                return View(search);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(search);
        }

        

    

        [HttpGet]
        public ActionResult CompanyLeadLimit(int? serviceId)
        {
                        
            LeadLimitModel model = new LeadLimitModel();
            model = BusinessLayer.GetCompanyLeadLimit(new CompanyModel().CurrentCompany.CompanyId, serviceId);
            model._companyInfo = new CompanyModel().CurrentCompany;

            return View(model);
        }


        [HttpPost]
        public JsonResult CompanyLeadLimit(List<List<LeadLimitModel>> leadlimit)
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


        

          [HttpPost]
        public JsonResult AddCompanyZipCodesPerAreaCodes(int serviceId, string areaCodes, int IsOrigin)
        {

            JsonResult result;
            try
            {
                
                BusinessLayer.AddCompanyZipCodesPerAreaCodes(new CompanyModel().CurrentCompany.CompanyId, serviceId, areaCodes, IsOrigin);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
         

            }
            catch (Exception ex)
            {
                result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;

            // return RedirectToAction("ManageAreaCodes", "Home", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        }
          [HttpGet]
          public ActionResult Geography(int? companyId, int? serviceId)
          {
              GeographyModel Origin = new GeographyModel();
              
              Origin = BusinessLayer.GetCompanyServiceAreaCodes(companyId, serviceId);
              Origin._companyInfo = new CompanyModel().CurrentCompany;

              return View(Origin);
          }


          [HttpGet]
          public ActionResult DestinationZipCodes(int? companyId, int? serviceId)
          {
              DestinationZipModel Destination = new DestinationZipModel();

              Destination = BusinessLayer.GetCompanyDestinationServiceAreaCodes(companyId, serviceId);
              Destination._companyInfo = new CompanyModel().CurrentCompany;

              return View(Destination);
          }

          public JsonResult GetCompanyAreasDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
          {
              var DestinationAreaCodes = BusinessLayer.GetCompanyAreasDestinationZipCodes(companyId, serviceId, areaCode);

              List<List<string>> list = ConfigValues.retListTable(DestinationAreaCodes);
              return Json(list, JsonRequestBehavior.AllowGet);


          }

          public JsonResult GetAvailableDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
          {
              var services = BusinessLayer.GetAvailableDestinationZipCodes(companyId, serviceId, areaCode);
              List<List<string>> list = ConfigValues.retListTable(services);
              return Json(list, JsonRequestBehavior.AllowGet);
          }

          [HttpPost]
          public JsonResult AddCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
          {

              JsonResult result;
              try
              {

                  BusinessLayer.AddCompanyAreaDestinationZipCodes(companyId, serviceId, areaCode, zipCodes);
                  result = Json(new { success = true }, JsonRequestBehavior.AllowGet);


              }
              catch (Exception ex)
              {
                  result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
              }

              return result;
          }

          [HttpPost]
          public JsonResult DeleteCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
          {

              JsonResult result;
              try
              {

                  BusinessLayer.DeleteCompanyAreaDestinationZipCodes(companyId, serviceId, areaCode, zipCodes);
                  result = Json(new { success = true }, JsonRequestBehavior.AllowGet);


              }
              catch (Exception ex)
              {
                  result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
              }

              return result;
          }
          public JsonResult GetAreaCodeZipCodes(int? companyId,int? serviceId, int? areaCode)
          {
              var OriginAreaCodes = BusinessLayer.GetCompanyAreasZipCodes(companyId, serviceId, areaCode);

                 List<List<string>> list = ConfigValues.retListTable(OriginAreaCodes);
                 return Json(list, JsonRequestBehavior.AllowGet);


          }
          public JsonResult GetAvailableZipCodes(int? companyId, int? serviceId, int? areaCode)
          {
              var services = BusinessLayer.GetAvailableZipCodes(companyId, serviceId, areaCode);
              List<List<string>> list = ConfigValues.retListTable(services);
              return Json(list, JsonRequestBehavior.AllowGet);
          }

          [HttpPost]
          public JsonResult AddCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
          {

              JsonResult result;
              try
              {

                  BusinessLayer.AddCompanyAreaZipCodes(companyId, serviceId, areaCode, zipCodes);
                  result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                  

              }
              catch (Exception ex)
              {
                  result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
              }

              return result;
          }
          [HttpPost]
          public JsonResult DeleteCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
          {

              JsonResult result;
              try
              {

                  BusinessLayer.DeleteCompanyAreaZipCodes(companyId, serviceId, areaCode, zipCodes);
                  result = Json(new { success = true }, JsonRequestBehavior.AllowGet);


              }
              catch (Exception ex)
              {
                  result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
              }

              return result;
          }

          [HttpGet]  
          public ActionResult Distance(int? companyId,int? serviceId)
          {

              ViewBag.Services = ConfigValues.Services().Take(2);
             DataTable dt = BusinessLayer.GetCompanyMoveDistance(companyId, serviceId);
             //List<List<string>> list = ConfigValues.retListTable(query);
              DistanceModel model = new DistanceModel();
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
          public JsonResult Distance(DistanceModel model)
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
        public JsonResult GetDistance(int? serviceId)
        {
            
            DataTable dt = BusinessLayer.GetCompanyMoveDistance(new CompanyModel().CurrentCompany.CompanyId, serviceId);
            List<List<string>> list = ConfigValues.retListTable(dt);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
          //[HttpPost]
          //public JsonResult AddCompanyMoveDistance(int ServiceId, int? MinWeight, int? MaxWeight)
          //{

          //    JsonResult result;
          //    try
          //    {
          //        var cmd = (string)HttpContext.Application["CompanyId"];
          //        BusinessLayer.AddCompanyMoveDistance(Convert.ToInt32(cmd), ServiceId, MinWeight, MaxWeight);
          //        result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
          //    }
          //    catch (Exception ex)
          //    {
          //        result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
          //    }

          //    return result;


          //}


          public JsonResult GetCompanyMoveDistance(int ServiceId)
          {

              JsonResult result;

              try
              {

                  var query = BusinessLayer.GetCompanyMoveDistance(new CompanyModel().CurrentCompany.CompanyId, ServiceId);
                  List<List<string>> list = ConfigValues.retListTable(query);

                  result = Json(list, JsonRequestBehavior.AllowGet);

              }
              catch (Exception ex)
              {
                  result = Json(new { success = false, message = "An error occurred while saving." + ex.Message }, JsonRequestBehavior.AllowGet);
              }

              return result;


          }

         

    }
}
