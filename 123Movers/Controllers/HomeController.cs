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
        public IEnumerable<GeographyModel> OriginZipCodes { get; set; }

        public ActionResult Reports(string companyid, string companyName, string ax, string contactperson, string suspended, bool active = false)
        {
            HttpContext.Application["CompanyId"] = companyid;
            HttpContext.Application["CompanyName"] = companyName;
            HttpContext.Application["Ax"] = ax;
            HttpContext.Application["IsActive"] = active;
            HttpContext.Application["Suspended"] = suspended;
            HttpContext.Application["ContactPerson"] = contactperson;

            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
        public List<List<string>> retListTable(DataTable dt)
        {
            List<List<string>> lstTable = new List<List<string>>();
            foreach (DataRow row in dt.Rows)
            {
                List<string> lstRow = new List<string>();
                foreach (var item in row.ItemArray)
                {
                    lstRow.Add(item.ToString().Replace("\r\n", string.Empty));
                }
                lstTable.Add(lstRow);
            }

            return lstTable;

        }

        public JsonResult GetServices()
        {
            var services = BusinessLayer.GetServies();
            List<List<string>> list = retListTable(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAvailableAreas(int? companyId, int? serviceId)
        {
            var services = BusinessLayer.GetAvailableAreas(companyId, serviceId);
            List<List<string>> list = retListTable(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyAdByArea(int? companyId, int? serviceId)
        {
            var services = BusinessLayer.GetCompanyAdByArea(companyId, serviceId);
            List<List<string>> list = retListTable(services);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public List<SelectListItem> Terms(bool IsRecurring, bool IsRequireNoticeToCharge, bool forEdit)
        {
            var listOption = new SelectListItem();
            var terms = new List<SelectListItem>();

            //listOption = new SelectListItem { Text = "--Choose One--", Value = "Choose" };
            //terms.Add(listOption);

            listOption = new SelectListItem { Text = "Recurring", Value = "Recurring" };
            terms.Add(listOption);

            listOption = new SelectListItem { Text = "Non Recurring", Value = "NonRecurring" };
            terms.Add(listOption);

            listOption = new SelectListItem { Text = "Recurring With Notice", Value = "RecurringWithNotice" };

            terms.Add(listOption);

            int i = 0;
            if (forEdit == true)
            {
                foreach (var item in terms)
                {

                    if (IsRecurring == true)
                    {
                        if (IsRecurring && !IsRequireNoticeToCharge)
                        {
                            i = 1;
                            terms.RemoveAt(0);
                            break;

                        }

                        if (IsRecurring && IsRequireNoticeToCharge)
                        {
                            i = 3;
                            terms.RemoveAt(2);
                            break;
                        }
                    }
                    else
                    {
                        i = 2;
                        terms.RemoveAt(1);


                        break;
                    }
                    i = i++;
                }
                switch (i)
                {
                    case 1:
                        listOption = new SelectListItem { Text = "Recurring", Value = "Recurring" };
                        terms.Insert(0, listOption);
                        break;
                    case 2:
                        listOption = new SelectListItem { Text = "Non Recurring", Value = "NonRecurring" };
                        terms.Insert(0, listOption);
                        break;
                    case 3:
                        listOption = new SelectListItem { Text = "Recurring With Notice", Value = "RecurringWithNotice" };
                        terms.Insert(0, listOption);
                        break;
                }
            }
            return terms;
        }

        public List<SelectListItem> Services(int? serviceId, bool forEdit)
        {
            var listOption = new SelectListItem();
            var services = new List<SelectListItem>();


            listOption = new SelectListItem { Text = "Local", Value = "1009" };
            services.Add(listOption);

            listOption = new SelectListItem { Text = "Long", Value = "1000" };
            services.Add(listOption);

            listOption = new SelectListItem { Text = "Both", Value = "999" };
            services.Add(listOption);

            int i = 0;
            if (forEdit == true)
            {
                foreach (var item in services)
                {

                    if (serviceId != null)
                    {
                        if (serviceId == 1009)
                        {

                            i = 1;
                            services.RemoveAt(0);
                            break;

                        }

                        if (serviceId == 1000)
                        {
                            i = 2;
                            services.RemoveAt(1);
                            break;
                        }
                    }

                    else
                    {
                        i = 3;
                        services.RemoveAt(2);


                        break;
                    }
                    i = i++;

                }

                switch (i)
                {
                    case 1:
                        listOption = new SelectListItem { Text = "Local", Value = "1009" };
                        services.Insert(0, listOption);
                        break;
                    case 2:
                        listOption = new SelectListItem { Text = "Long", Value = "1000" };
                        services.Insert(0, listOption);
                        break;
                    case 3:
                        listOption = new SelectListItem { Text = "Both", Value = "999" };
                        services.Insert(0, listOption);
                        break;
                }



            }

            return services;
        }

        [HttpGet]
        public ActionResult AddBudget()
        {
            ViewBag.Terms = Terms(false, false, false);

            ViewBag.Services = Services(null, false);

            BudgetModel budgget = new BudgetModel();

            //budgget.CompanyId = search.CompanyId;
            //budgget.CompanyName = search.CompanyName;
            //budgget.AX = search.AX;
            //budgget.IsActive = search.IsActive;

            //budgget.DisplayName = search.DisplayName;
            //budgget.ContactPerson = search.ContactPerson;
            //budgget.CompanyHandle = search.CompanyHandle;

            //HttpContext.Application["CompanyId"] = budgget.CompanyId;
            //HttpContext.Application["CompanyName"] = budgget.CompanyName;
            //HttpContext.Application["Ax"] = budgget.AX;
            //HttpContext.Application["IsActive"] = budgget.IsActive;
            //HttpContext.Application["DisplayName"] = budgget.DisplayName;
            //HttpContext.Application["ContactPerson"] = budgget.ContactPerson;
            //HttpContext.Application["CompanyHandle"] = budgget.CompanyHandle;


            return View(budgget);
        }
        //  [HttpGet]
        //public ActionResult AddBudget(string companyid, string companyName, string ax, bool active, string contactperson, string companyHandle,string displayname)
        //  {
        //      ViewBag.Terms = Terms();

        //      ViewBag.Services = Services();

        //      BudgetModel budgget = new BudgetModel();

        //      budgget.CompanyId = Convert.ToInt32(companyid);
        //      budgget.CompanyName = companyName;
        //      budgget.AX = ax;
        //      budgget.IsActive = active;


        //      HttpContext.Application["CompanyId"] = budgget.CompanyId;
        //      HttpContext.Application["CompanyName"] = companyName;
        //      HttpContext.Application["Ax"] = ax;
        //      HttpContext.Application["IsActive"] = active;
        //      HttpContext.Application["DisplayName"] = displayname;
        //      HttpContext.Application["ContactPerson"] = contactperson;
        //      HttpContext.Application["CompanyHandle"] = companyHandle;

        //      return View(budgget);
        //  }

        [HttpPost]
        // [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddBudget(BudgetModel budget)
        {

            ViewBag.Terms = Terms(false, false, false);
            ViewBag.Services = Services(null, false);

            try
            {
                
                //if (ModelState.IsValid)
                //{
                var cmd = (string)HttpContext.Application["CompanyId"];
                budget.CompanyId = Convert.ToInt32(cmd);
                budget.CompanyName = (string)HttpContext.Application["CompanyName"];
                budget.AX = (string)HttpContext.Application["Ax"];
                budget.IsActive = (bool)HttpContext.Application["IsActive"];
                //budget.DisplayName = (string)HttpContext.Application["DisplayName"];
                budget.ContactPerson = (string)HttpContext.Application["ContactPerson"];
                // budget.CompanyHandle = (string)HttpContext.Application["CompanyHandle"];
                budget.Type = "NEW";

                if (budget.Terms == "Recurring")
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = false;
                }
                else if (budget.Terms == "NonRecurring")
                {
                    budget.IsRecurring = false;
                    budget.IsRequireNoticeToCharge = false;
                }
                else
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = true;
                }

                BusinessLayer.SaveBudget(budget);
                // ModelState.Clear();
                ViewBag.Success = "Budget saved successfully..";
                //return View();
                return RedirectToAction("GetBudget", "Home", new { budget.CompanyId, budget.CompanyName, budget.AX,  budget.ContactPerson, budget.Suspended, budget.IsActive });

                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(budget);


        }
        [HttpGet]
        public ActionResult EditBudget(decimal? TotalBudget, bool IsRecurring, bool IsRequireNoticeToCharge, int? serviceId, string agnumber, int? minDaysToCharge)
        {
            BudgetModel budget = new BudgetModel();

            budget.TotalBudget = TotalBudget;
            budget.IsRecurring = IsRecurring;
            budget.IsRequireNoticeToCharge = IsRequireNoticeToCharge;
            budget.ServiceId = serviceId;
            budget.MinDaysToCharge = minDaysToCharge;
            budget.AgreementNumber = agnumber;


            ViewBag.Terms = Terms(budget.IsRecurring, budget.IsRequireNoticeToCharge, true);
            ViewBag.Services = Services(budget.ServiceId, true);

            //var result = BusinessLayer.GetMoveWeights().AsEnumerable();
            //List<string> items = new List<string>();
            //foreach (var item in result)
            //{
            //    items.Add(item[0].ToString());
            //}

            //ViewBag.MoveWeights = BusinessLayer.ToJSON(items);

            var cmd = (string)HttpContext.Application["CompanyId"];
            budget.CompanyId = Convert.ToInt32(cmd);
            budget.CompanyName = (string)HttpContext.Application["CompanyName"];
            budget.AX = (string)HttpContext.Application["Ax"];
            budget.IsActive = (bool)HttpContext.Application["IsActive"];
            budget.Suspended = (string)HttpContext.Application["Suspended"];
            budget.ContactPerson = (string)HttpContext.Application["ContactPerson"];

            return View(budget);


        }
        [HttpPost]
        public ActionResult EditBudget(BudgetModel budget)
        {

            ViewBag.Terms = Terms(false, false, false);
            ViewBag.Services = Services(null, false);

            try
            {
                //if (ModelState.IsValid)
                //{
                var cmd = (string)HttpContext.Application["CompanyId"];
                budget.CompanyId = Convert.ToInt32(cmd);
                budget.CompanyName = (string)HttpContext.Application["CompanyName"];
                budget.AX = (string)HttpContext.Application["Ax"];
                budget.IsActive = (bool)HttpContext.Application["IsActive"];
                budget.Suspended = (string)HttpContext.Application["    "];
                budget.ContactPerson = (string)HttpContext.Application["ContactPerson"];
                budget.BudgetAction = "RENEWAL INSERTION";
                budget.Type = "EDIT";

                if (budget.Terms == "Recurring")
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = false;
                }
                else if (budget.Terms == "NonRecurring")
                {
                    budget.IsRecurring = false;
                    budget.IsRequireNoticeToCharge = false;
                }
                else
                {
                    budget.IsRecurring = true;
                    budget.IsRequireNoticeToCharge = true;
                }

                BusinessLayer.SaveBudget(budget);
                //ModelState.Clear();
              //  ViewBag.Success = "Budget saved successfully..";

                return RedirectToAction("GetBudget", "Home", new { budget.CompanyId, budget.CompanyName, budget.AX, budget.ContactPerson, budget.Suspended, budget.IsActive });

                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(budget);



        }


        [HttpGet]
        public ActionResult GetBudget(string companyid, string companyName, string ax, string contactperson, string suspended, bool active = false)
        {

            // MoversEntities dc = new MoversEntities();
            IEnumerable<BudgetModel> budgetList = new List<BudgetModel>();
            SearchModel search = new SearchModel();
            //var cmpid = Convert.ToInt32(companyid);
            budgetList = BusinessLayer.GetBudget(companyid);

            var tbilled = budgetList.Where(b => b.EndDate < DateTime.Now).Sum(b => b.TotalBudget);
            var uamount = budgetList.Where(b => b.EndDate < DateTime.Now).Sum(b => b.UnchargedAmount);

            ViewBag.TotalBilled = String.Format("{0:C}", tbilled);
            ViewBag.UnchargedAmount = String.Format("{0:C}", uamount);

            HttpContext.Application["CompanyId"] = companyid;
            HttpContext.Application["CompanyName"] = companyName;
            HttpContext.Application["Ax"] = ax;
            HttpContext.Application["IsActive"] = active;
            HttpContext.Application["Suspended"] = suspended;
            HttpContext.Application["ContactPerson"] = contactperson;

            search.budget = budgetList;
           
            return View(search);
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

        public ActionResult ManageAreaCodes(int? companyId, int? serviceId, string companyName)
        {
            ViewBag.CompanyID = companyId;
            ViewBag.CompanyName = companyName;
            ViewBag.ServiceID = serviceId;


            return View();
        }
        public ActionResult AddAreaCodes(int? companyId, int? serviceId, string companyName, string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            foreach (var areacode in areacodelist)
            {
                BusinessLayer.AddCompanyAdByArea(companyId, serviceId, Convert.ToInt16(areacode));
            }
            return RedirectToAction("ManageAreaCodes", "Home", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        }

        public ActionResult DeleteAreaCodes(int? companyId, int? serviceId, string companyName, string areaCodes)
        {
            IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

            foreach (var areacode in areacodelist)
            {
                BusinessLayer.DeleteCompanyAdByArea(companyId, serviceId, Convert.ToInt16(areacode));
            }
            return RedirectToAction("ManageAreaCodes", "Home", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        }

        [HttpPost]
        public JsonResult AddCompanyPricePerLead(int? companyId, int? serviceId, string companyName, string areaCodes)
        {
            
            JsonResult result;
            try
            {

                BusinessLayer.AddCompanyPricePerLead(companyId, serviceId, areaCodes, null);
                result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                //IList<string> areacodelist = new JavaScriptSerializer().Deserialize<IList<string>>(areaCodes);

                //foreach (var areacode in areacodelist)
                //{
                //    var s = areacode.Split('-');
                //    //int? acode = s[0] != null ?  Convert.ToInt32(s[0]) : null;
                //    if (s[0].ToString() == "null")
                //    {
                //        if (BusinessLayer.AddCompanyPricePerLead(companyId, serviceId, null, Convert.ToDecimal(s[1]), null))
                //        {
                //            result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            throw new ApplicationException(string.Format("An error occurred while saving"));
                //        }
                //    }
                //    else
                //    {
                //        if (BusinessLayer.AddCompanyPricePerLead(companyId, serviceId, Convert.ToInt16(s[0]), Convert.ToDecimal(s[1]), null))
                //        {
                //            result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                //        }
                //        else
                //        {
                //            throw new ApplicationException(string.Format("An error occurred while saving"));
                //        }
                //    }
                   
                //}

            }
            catch (Exception ex)
            {
                result = Json(new { success = false, message = "An error occurred while saving."  + ex.Message}, JsonRequestBehavior.AllowGet);
            }

            return result;

           // return RedirectToAction("ManageAreaCodes", "Home", new { companyId = companyId, serviceId = serviceId, companyName = companyName });
        }

        //[HttpGet]
        //public ActionResult CompanyLeadLimit(int? companyId, int? serviceId, string companyName)
        //{
        //    //ViewBag.CompanyID = companyId;
        //    //ViewBag.CompanyName = companyName;
        //    //ViewBag.ServiceID = serviceId;

        //    ViewBag.Services = Services(null, true);

        //    var avaAreaCodes = BusinessLayer.GetCompanyLeadLimit(companyId, serviceId);

        //    ViewBag.avaAreaCodes = DataTableToSelectList(avaAreaCodes, "areaCode", "state"); ;

        //    var services = BusinessLayer.GetServies();

        //    ViewBag.AreaCodes = DataTableToSelectList(services, "areaCode", "state"); ;

        //    LeadLimitModel ld = new LeadLimitModel();

        //    return View(ld);
        //}

        //[HttpPost]
        //public ActionResult CompanyLeadLimit(LeadLimitModel leadlimit)
        //{
        //    var cmd = (string)HttpContext.Application["CompanyId"];
        //    leadlimit.CompanyId = Convert.ToInt32(cmd);
        //    BusinessLayer.AddCompanyLeadLimit(leadlimit);
        //    ViewBag.Services = Services(Convert.ToInt32(leadlimit.Services), true);
            

        //    var services = BusinessLayer.GetServies();

        //    ViewBag.AreaCodes = DataTableToSelectList(services, "areaCode", "areaCode");

        //    ModelState.Clear();
        //    ViewBag.Success = "Lead saved successfully..";

        //    return View(leadlimit);
        //}

        [HttpGet]
        public ActionResult CompanyLeadLimit(int? serviceId)
        {
            int? companyId;
            companyId = Convert.ToInt32((string)HttpContext.Application["CompanyId"]);
            LeadLimitModel ld = new LeadLimitModel();
            var leadLimitData = BusinessLayer.GetCompanyLeadLimit(companyId, serviceId);

            return View(leadLimitData);
        }


        [HttpPost]
        public JsonResult CompanyLeadLimit(List<List<LeadLimitModel>> leadlimit)
        {

            JsonResult result;
            try
            {
                var cmd = (string)HttpContext.Application["CompanyId"];
                foreach (var ld in leadlimit)
                {
                    ld[0].CompanyId = Convert.ToInt32(cmd);
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


        //public JsonResult GetCompanyLeadLimitAreaCodeInfo(int? serviceId)
        //{


        //    int? companyId;
        //    var cmd = (string)HttpContext.Application["CompanyId"];
        //    companyId = Convert.ToInt32(cmd);
        //    var query = from r in BusinessLayer.GetCompanyLeadLimit(companyId, serviceId).AsEnumerable()
        //                //where r.Field<string>("areaCode") == area
        //                select r;
        //    var items = new List<LeadLimitModel>();
        //    if (query.Any())
        //    {
        //        DataTable conversions = query.CopyToDataTable();

        //        var Areacode = "";
        //        var ServiceId = "";
        //        //bool isMonth = false;
        //        //bool isDaily = false;
        //        //bool isTotal = false;
        //        foreach (DataRow row in conversions.Rows)
        //        {
        //            if (String.IsNullOrEmpty(row["areaCode"].ToString()))
        //            {
        //                Areacode = null;

        //            }
        //            else
        //            {
        //                Areacode = row["areaCode"].ToString();
        //            }
        //            if (String.IsNullOrEmpty(row["serviceID"].ToString()))
        //            {
        //                ServiceId = null;

        //            }
        //            else
        //            {
        //                ServiceId = row["serviceID"].ToString();
        //            }


        //            //if (!string.IsNullOrEmpty(row["isDailyLeadLimit"].ToString()))
        //            //{
        //            //    isDaily = Convert.ToBoolean(row["isDailyLeadLimit"]);
        //            //}

        //            LeadLimitModel obj = new LeadLimitModel()
        //            {

        //                AreaCodes = Areacode,
        //                ServiceId = Convert.ToInt32(ServiceId),
        //                LeadFrequency = Convert.ToInt32(row["leadFrequency"].ToString()),
        //                IsDailyLeadLimit = Convert.ToBoolean(row["isDailyLeadLimit"]),
        //                DailyLeadLimit = Convert.ToInt32(row["dailyLeadLimit"].ToString()),
        //                IsMonthlyLeadLimit = Convert.ToBoolean(row["isMonthlyLeadLimit"]),
        //                MonthlyLeadLimit = Convert.ToInt32(row["monthlyLeadLimit"].ToString()),
        //                IsTotalLeadLimit = Convert.ToBoolean(row["isTotalLeadLimit"]),
        //                TotalLeadLimit = Convert.ToInt32(row["totalLeadLimit"].ToString())



        //            };
        //            items.Add(obj);

        //        }
        //    }


        //    return Json(items, JsonRequestBehavior.AllowGet);

        //}
        public  SelectList DataTableToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    //Text = row[textField].ToString() + "-" + row[valueField].ToString(),
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

          [HttpPost]
        public JsonResult AddCompanyZipCodesPerAreaCodes(int serviceId, string areaCodes, int IsOrigin)
        {

            JsonResult result;
            try
            {
                string companyId = (string)HttpContext.Application["CompanyId"];
                BusinessLayer.AddCompanyZipCodesPerAreaCodes(Convert.ToInt32(companyId), serviceId, areaCodes, IsOrigin);
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

              var OriginAreaCodes = BusinessLayer.GetCompanyServiceAreaCodes(companyId, serviceId);
              OriginZipCodes = OriginAreaCodes.OriginZipCodes;
              return View(OriginAreaCodes);
          }
          public JsonResult GetAreaCodeZipCodes(int? companyId,int? serviceId, int? areaCode)
          {
              var OriginAreaCodes = BusinessLayer.GetCompanyAreasZipCodes(companyId, serviceId, areaCode);

                 List<List<string>> list = retListTable(OriginAreaCodes);
                 return Json(list, JsonRequestBehavior.AllowGet);


          }
          public JsonResult GetAvailableZipCodes(int? companyId, int? serviceId, int? areaCode)
          {
              var services = BusinessLayer.GetAvailableZipCodes(companyId, serviceId, areaCode);
              List<List<string>> list = retListTable(services);
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

              ViewBag.Services = Services(null, false).Take(2);
              var query = BusinessLayer.GetCompanyMoveDistance(companyId, serviceId);
              List<List<string>> list = retListTable(query);
              DistanceModel model = new DistanceModel();
              model.CompanyId = companyId;
              model.ServiceId = serviceId;
              if (list.Count > 0)
              {
                  model.MinMoveWeight = string.IsNullOrWhiteSpace(list[0][1].ToString()) ? 0 : Convert.ToDecimal(list[0][1]);
                  model.MaxMoveWeight = string.IsNullOrWhiteSpace(list[0][2].ToString()) ? 0 : Convert.ToDecimal(list[0][2]);
              }

              return View(model);

          }


          [HttpPost]
          public ActionResult Distance(DistanceModel model)
          {
              ViewBag.Services = Services(null, false).Take(2);
              try
              {
                  var cmd = (string)HttpContext.Application["CompanyId"];
                  model.CompanyId = Convert.ToInt32(cmd);
                  BusinessLayer.SaveMoveDistance(model);

                  ViewBag.Success = "Saved Sucessfully";
              }
              catch (Exception ex)
              {

              }
              return View(model);
              //return RedirectToAction("Distance", "Home", new { model.CompanyId, model.ServiceId });
          }

          [HttpGet]
          public ActionResult MoveWeight(int? ServiceId)
          {
              //var moveWeights = DataTableToSelectList(BusinessLayer.GetMoveWeights(), "moveWeightSeq", "moveweight");
              //var selectedItem = moveWeights.First(x => x.Value == "7");

              //ViewBag.MinMoveWeight = new SelectList(moveWeights, "Value", "Text", new { Value = 7 });

              var cmd = (string)HttpContext.Application["CompanyId"];

              DataSet ds = BusinessLayer.GetMoveWeights(Convert.ToInt32(cmd), ServiceId);

              ViewBag.MinMoveWeight = DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
              ViewBag.Services = Services(null, false).Take(2);
              MoveWeightModel model = new MoveWeightModel();
              model.CompanyId = Convert.ToInt32(cmd);
              model.ServiceId = ServiceId;
              model.MinMoveWeightSeq =Convert.ToInt32( ds.Tables[1].Rows[0][2].ToString());
              model.MaxMoveWeightSeq = Convert.ToInt32(ds.Tables[1].Rows[0][3].ToString());
              return View(model);
          }
        [HttpPost]
          public ActionResult MoveWeight(MoveWeightModel model)
          {
             var cmd = (string)HttpContext.Application["CompanyId"];
              DataSet ds = BusinessLayer.GetMoveWeights(Convert.ToInt32(cmd), model.ServiceId);

              ViewBag.MinMoveWeight = DataTableToSelectList(ds.Tables[0], "moveWeightSeq", "moveweight");
              ViewBag.Services = Services(null, false).Take(2);

              try
              {
                  model.CompanyId = Convert.ToInt32(cmd);
                  BusinessLayer.SaveMoveWeight(model);
                  ViewBag.Success = "Saved Sucessfully";
              }
              catch {
              }
              return View(model);
          }
        public JsonResult GetMoveWeight(int? serviceId)
        {
            var cmd = (string)HttpContext.Application["CompanyId"];
            DataSet ds = BusinessLayer.GetMoveWeights(Convert.ToInt32(cmd), serviceId);
            List<List<string>> list = retListTable(ds.Tables[1]);
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
                  var cmd = (string)HttpContext.Application["CompanyId"];

                  var query = BusinessLayer.GetCompanyMoveDistance(Convert.ToInt32(cmd), ServiceId);
                  List<List<string>> list = retListTable(query);

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
