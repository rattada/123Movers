using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;
using log4net;
using System.Data;
using _123Movers.BusinessEntities;

namespace _123Movers.Controllers
{
    public class BaseController : Controller
    {
        protected int? _serviceId;
        protected CompanyModel  _companyInfo;
        public ILog logger;
        public int? _companyId;
        public BaseController()
        {
            //logger = LogManager.GetLogger(typeof(controller));
        }

        public int? companyId { get; set; }
        
        public int? ServiceId
        {
            get
            {
                if (_serviceId == null) { RetrieveCurrentServiceId(); }
                return _serviceId;
            }
        }

        /// <summary>
        /// Store Service Type
        /// </summary>
        public void SaveSeviceId(int? id)
        {
            var cookie = new HttpCookie("ServiceId")
            {
                Value = Convert.ToString(id)
            };
            _serviceId = id;

            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Get Service from Cookie
        /// </summary>
        protected string RetrieveCurrentServiceId()
        {
            var companyCookieVal = _serviceId.ToString();

            if (_serviceId == null)
            {

                if (Request.Cookies.AllKeys.Contains("ServiceId"))
                {
                    companyCookieVal = Request.Cookies["ServiceId"].Value;
                }

                _serviceId = int.Parse(companyCookieVal);
            }

            return companyCookieVal;
        }



        public CompanyModel CompanyInfo
        {
            get
            {
                if (_companyInfo == null) { RetrieveCurrentCompanyInfo(_companyId); }
                return _companyInfo;
            }
        }
        /// <summary>
        /// Store Company Inforamtion
        /// </summary>
        /// <param name="companyInfo"></param>
        public void SaveCompanyInfo(CompanyModel companyInfo)
        {
            HttpContext.Session.Add("CurrentCompanyInfo", companyInfo);

            _companyInfo = companyInfo;

        }
        /// <summary>
        /// Get company information from Session
        /// </summary>
        protected CompanyModel RetrieveCurrentCompanyInfo(int? companyId)
        {
            _companyId = companyId;
            var companyCookieVal = _companyInfo;
            try
            {
                if (_companyInfo == null)
                {

                    if (Session["CurrentCompanyInfo"] != null)
                    {
                        companyCookieVal = (CompanyModel)Session["CurrentCompanyInfo"];
                    }
                    else {
                        Session["CurrentCompanyInfo"] = BusinessLayer.GetCompany(_companyId);
                        companyCookieVal = (CompanyModel)Session["CurrentCompanyInfo"];
                    }

                    _companyInfo = companyCookieVal;
                }
            }
            catch(Exception ex){
                logger.Error(ex.ToString());
            }
            return companyCookieVal;
        }
        /// <summary>
        /// Generate Term Types (Recurring, Non Recurring and Recurring with Notice)
        /// </summary>
        /// <returns> List of Term Types</returns>
        public List<SelectListItem> GetTerms()
        {
            //var values = Enum.GetValues(typeof(DayOfWeek))
            //                            .Cast<DayOfWeek>()
            //                            .Select(d => Tuple.Create(((int)d).ToString(), d.ToString()))
            //                            .ToList();

            var Terms = new List<SelectListItem>();

            Terms = ConfigValues.Terms.Select(p => new SelectListItem
                                                            {
                                                                Text = p.Key,
                                                                Value = p.Value
                                                            }).ToList();

            // var priceNames = ConfigValues.Terms1.Select(p => p.Value).ToList();
            return Terms;
        }

        /// <summary>
        /// Generate Service Types(Local, Long and Both)
        /// </summary>
        /// <param name="serviceId">Select Service</param>
        /// <returns>List Of Services</returns>
        public List<SelectListItem> GetServices(int? serviceId = null)
        {
            var listOption = new SelectListItem();
            var services = new List<SelectListItem>();

            if (serviceId == null || serviceId == Constants.LOCAL)
            {
                listOption = new SelectListItem { Text = "Local", Value = "1009" };

                services.Add(listOption);
            }
            if (serviceId == null || serviceId == Constants.LONG)
            {
                listOption = new SelectListItem { Text = "Long", Value = "1000" };
                services.Add(listOption);
            }
            if (serviceId == null)
            {
                listOption = new SelectListItem { Text = "Both", Value = "999" };
                services.Add(listOption);
            }

            return services;
        }
        /// <summary>
        /// Convert Datat Table to Select List
        /// </summary>
        public SelectList DataTableToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
        //public class CheckSessionOutAttribute : ActionFilterAttribute
        //{
        //    public override void OnActionExecuting(ActionExecutingContext filterContext)
        //    {
        //        string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
        //        if (!controllerName.Contains("account") && !controllerName.Contains("home"))
        //        {
        //            HttpSessionStateBase session = filterContext.HttpContext.Session;
        //            var user = session["CurrentCompanyInfo"]; //Key 2 should be User or UserName
        //            if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession))
        //            {
        //                //send them off to the login page
        //                var url = new UrlHelper(filterContext.RequestContext);
        //                var loginUrl = url.Content("~/Account/Login");
        //                filterContext.HttpContext.Response.Redirect(loginUrl, true);
        //            }
        //        }
        //    }
        //}

    }
}
