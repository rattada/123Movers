using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;
using log4net;
using System.Data;
using _123Movers.BusinessEntities;
using System.Web.Routing;

namespace _123Movers.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseController));

        public int? _companyId;
        protected CompanyModel _companyInfo;

        public int? CompanyId
        {
            get
            {
                if (_companyId == null) { RetrieveCurrentCompanyId(); }
                return _companyId;
            }
        }

        /// <summary>
        /// Store Service Type
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
                    companyIdCookieVal = Request.Cookies["CompanyId"].Value;
                }

                _companyId = int.Parse(companyIdCookieVal);
            }

            return companyIdCookieVal;
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
                    else
                    {
                        Session["CurrentCompanyInfo"] = BusinessLayer.GetCompany(_companyId);
                        companyCookieVal = (CompanyModel)Session["CurrentCompanyInfo"];
                    }

                    _companyInfo = companyCookieVal;
                }
            }
            catch (Exception ex)
            {
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



    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            if (!controllerName.Contains("account") && !controllerName.Contains("home"))
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                var _company = session["CurrentCompanyInfo"];
                if (((_company == null) && (!session.IsNewSession)) || (session.IsNewSession))
                {
                    //send them off to the login page
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.Content("~/Account/Login");

                    filterContext.HttpContext.Response.Redirect(loginUrl, true);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private static ILog logger = LogManager.GetLogger(typeof(BaseController));
        public override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    return;
            //}

            //if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            //{
            //    return;
            //}

            //if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            //{
            //    return;
            //}

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,  //ViewName = "CustomError"
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            // log the error by using your own method
            logger.Error(filterContext.Exception.Message, filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
