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
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseController));
        
        private int? _companyId;
        private CompanyModel _companyInfo;
        
        /// <summary>
        /// Get CompanyId
        /// </summary>
        public int? CompanyId
        {
            get
            {
                if (_companyId == null) { RetrieveCurrentCompanyId(); }
                return _companyId;
            }
        }

        /// <summary>
        /// Store Company ID
        /// </summary>
        public void SaveCompanyId(int? id)
        {
            var cookie = new HttpCookie("CompanyId")
            {
                Value = Convert.ToString(id)
            };
            _companyId = id;

            cookie.Expires.AddDays(365);
            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Get Service from Cookie
        /// </summary>
        protected string RetrieveCurrentCompanyId()
        {
            var companyIdCookieVal = _companyId.ToString();

            if (_companyId == null)
            {

                if (Request.Cookies.AllKeys.Contains("CompanyId"))
                {
                    var httpCookie = Request.Cookies["CompanyId"];
                    if (httpCookie != null) companyIdCookieVal = httpCookie.Value;
                }

                _companyId = int.Parse(companyIdCookieVal);
            }

            return companyIdCookieVal;
        }


        /// <summary>
        /// 
        /// </summary>
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
            if (HttpContext.Session != null) HttpContext.Session.Add(Constants.CurrentCompanyInfo, companyInfo);

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
                    if (Session[Constants.CurrentCompanyInfo] != null)
                        companyCookieVal = (CompanyModel)Session[Constants.CurrentCompanyInfo];
                    else
                    {
                        Session[Constants.CurrentCompanyInfo] = BusinessLayer.GetCompany(_companyId);
                        companyCookieVal = (CompanyModel)Session[Constants.CurrentCompanyInfo];
                    }

                    _companyInfo = companyCookieVal;
                }
            }
            catch(Exception ex){
                Logger.Error(ex.ToString());
            }
            return companyCookieVal;
        }

        /// <summary>
        /// Generate Term Types (Recurring, Non Recurring and Recurring with Notice)
        /// </summary>
        /// <returns> List of Term Types</returns>
        public List<SelectListItem> GetTerms()
        {
            var terms = ConfigValues.Terms.Select(p => new SelectListItem
                {
                    Text = p.Key,
                    Value = p.Value
                }).ToList();

            return terms;
        }

        /// <summary>
        /// Generate Service Types(Local, Long and Both)
        /// </summary>
        /// <param name="serviceId">Select Service</param>
        /// <returns>List Of Services</returns>
        public List<SelectListItem> GetServices(int? serviceId = null)
        {
            SelectListItem listOption;
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
    }
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            if (controllerName.Contains("account") || controllerName.Contains("home")) return;
            var session = filterContext.HttpContext.Session;
            if (session == null) return;
            var company = session[Constants.CurrentCompanyInfo];
            if (((company != null) || (session.IsNewSession)) && (!session.IsNewSession)) return;
            //send them off to the login page
            var url = new UrlHelper(filterContext.RequestContext);
            var loginUrl = url.Content("~/Account/Login");

            filterContext.HttpContext.Response.Redirect(loginUrl, true);
        }
    }
}
