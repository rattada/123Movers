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

        public ActionResult Reports(string companyid, string companyName, string ax, string contactperson, string suspended, bool active = false)
        {
            Session["CompanyId"] = companyid;
            Session["CompanyName"] = companyName;
            Session["Ax"] = ax;
            Session["IsActive"] = active;
            Session["Suspended"] = suspended;
            Session["ContactPerson"] = contactperson;

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

            //Session["CompanyId"] = budgget.CompanyId;
            //Session["CompanyName"] = budgget.CompanyName;
            //Session["Ax"] = budgget.AX;
            //Session["IsActive"] = budgget.IsActive;
            //Session["DisplayName"] = budgget.DisplayName;
            //Session["ContactPerson"] = budgget.ContactPerson;
            //Session["CompanyHandle"] = budgget.CompanyHandle;


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


        //      Session["CompanyId"] = budgget.CompanyId;
        //      Session["CompanyName"] = companyName;
        //      Session["Ax"] = ax;
        //      Session["IsActive"] = active;
        //      Session["DisplayName"] = displayname;
        //      Session["ContactPerson"] = contactperson;
        //      Session["CompanyHandle"] = companyHandle;

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
                var cmd = (string)Session["CompanyId"];
                budget.CompanyId = Convert.ToInt32(cmd);
                budget.CompanyName = (string)Session["CompanyName"];
                budget.AX = (string)Session["Ax"];
                budget.IsActive = (bool)Session["IsActive"];
                //budget.DisplayName = (string)Session["DisplayName"];
                budget.ContactPerson = (string)Session["ContactPerson"];
                // budget.CompanyHandle = (string)Session["CompanyHandle"];
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


            var cmd = (string)Session["CompanyId"];
            budget.CompanyId = Convert.ToInt32(cmd);
            budget.CompanyName = (string)Session["CompanyName"];
            budget.AX = (string)Session["Ax"];
            budget.IsActive = (bool)Session["IsActive"];
            budget.Suspended = (string)Session["Suspended"];
            budget.ContactPerson = (string)Session["ContactPerson"];

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
                var cmd = (string)Session["CompanyId"];
                budget.CompanyId = Convert.ToInt32(cmd);
                budget.CompanyName = (string)Session["CompanyName"];
                budget.AX = (string)Session["Ax"];
                budget.IsActive = (bool)Session["IsActive"];
                budget.Suspended = (string)Session["Suspended"];
                budget.ContactPerson = (string)Session["ContactPerson"];
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

            Session["CompanyId"] = companyid;
            Session["CompanyName"] = companyName;
            Session["Ax"] = ax;
            Session["IsActive"] = active;
            Session["Suspended"] = suspended;
            Session["ContactPerson"] = contactperson;

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
                //        Session["CompanyId"] = b.CompanyId;
                //        Session["CompanyName"] = b.CompanyName;
                //        Session["Ax"] = b.AX;
                //        Session["IsActive"] = b.IsActive;
                //        //Session["DisplayName"] = b.DisplayName;
                //        Session["ContactPerson"] = b.ContactPerson;
                //        // Session["CompanyHandle"] = b.CompanyHandle;
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

        [HttpGet]
        public ActionResult CompanyLeadLimit(int? companyId, int? serviceId, string companyName)
        {
            //ViewBag.CompanyID = companyId;
            //ViewBag.CompanyName = companyName;
            //ViewBag.ServiceID = serviceId;

            ViewBag.Services = Services(null, true);

            var services = BusinessLayer.GetServies();

            ViewBag.AreaCodes = DataTableToSelectList(services, "areaCode", "state"); ;

            LeadLimitModel ld = new LeadLimitModel();

            return View(ld);
        }

        [HttpPost]
        public ActionResult CompanyLeadLimit(LeadLimitModel leadlimit)
        {
            var cmd = (string)Session["CompanyId"];
            leadlimit.CompanyId = Convert.ToInt32(cmd);
            BusinessLayer.AddCompanyLeadLimit(leadlimit);
            ViewBag.Services = Services(Convert.ToInt32(leadlimit.Services), true);

            var services = BusinessLayer.GetServies();

            ViewBag.AreaCodes = DataTableToSelectList(services, "areaCode", "state");

            ModelState.Clear();
            ViewBag.Success = "Lead saved successfully..";

            return View(leadlimit);
        }
        public  SelectList DataTableToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString() + "-" + row[valueField].ToString(),
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
                string companyId = (string)Session["CompanyId"];
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
        //MoversEntities m = new MoversEntities();

        //public List<TreeViewModel> GetTreeVeiwList()
        //{
        //    List<TreeViewModel> rootNode = (from e1 in m.tbl_zip
        //                                    select new TreeViewModel()
        //                                     {
        //                                         // EmployeeCode = e1.areaCode,
        //                                         State = e1.longState
        //                                     }).Distinct().ToList<TreeViewModel>();

        //    foreach (var childRootNode in rootNode)
        //    {
        //        BuildChildNode(childRootNode);
        //    }


        //    return rootNode;
        //}


        //private void BuildChildNode(TreeViewModel rootNode)
        //{
        //    if (rootNode != null)
        //    {
        //        List<TreeViewModel> chidNode = (from e1 in m.tbl_zip
        //                                         where e1.longState == rootNode.State
        //                                        select new TreeViewModel()
        //                                         {
        //                                             AreaCode = e1.areaCode,
        //                                             //EmployeeName = e1.state
        //                                         }).Distinct().ToList<TreeViewModel>();
        //        if (chidNode.Count > 0)
        //        {
        //            foreach (var childRootNode in chidNode)
        //            {
        //                //BuildChildNode1(childRootNode);
        //                rootNode.ChildNode.Add(childRootNode);
        //            }
        //        }

        //    }
        //}
        //private void BuildChildNode1(TreeViewModel rootNode)
        //{
        //    if (rootNode != null)
        //    {
        //        List<TreeViewModel> chidNode = (from e1 in m.tbl_zip
        //                                         where e1.areaCode == rootNode.AreaCode
        //                                        select new TreeViewModel()
        //                                         {
        //                                             Zipcode = e1.zipCode,
        //                                             //EmployeeName = e1.state
        //                                         }).Distinct().ToList<TreeViewModel>();
        //        if (chidNode.Count > 0)
        //        {
        //            foreach (var childRootNode in chidNode)
        //            {
        //                rootNode.ChildNode.Add(childRootNode);
        //            }
        //        }

        //    }
        //}
    }
}
