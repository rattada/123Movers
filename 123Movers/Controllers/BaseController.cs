using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;
using log4net;

namespace _123Movers.Controllers
{
    public class BaseController : Controller
    {
        protected int? _serviceId;
        protected CompanyModel  _companyInfo;
        public ILog logger;

        public BaseController()
        {
            //logger = LogManager.GetLogger(typeof(controller));
        }
        public int? ServiceId
        {
            get
            {
                if (_serviceId == null) { RetrieveCurrentCompanyId(); }
                return _serviceId;
            }
        }

        public void SaveCompanyId(int? id)
        {
            var cookie = new HttpCookie("ServiceId")
            {
                Value = Convert.ToString(id)
            };
            _serviceId = id;

            Response.Cookies.Add(cookie);
        }

        protected string RetrieveCurrentCompanyId()
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
                if (_companyInfo == null) { RetrieveCurrentCompanyInfo(); }
                return _companyInfo;
            }
        }

        public void SaveCompanyInfo(CompanyModel companyInfo)
        {
            HttpContext.Session.Add("CurrentCompanyInfo", companyInfo);

            _companyInfo = companyInfo;

        }

        protected CompanyModel RetrieveCurrentCompanyInfo()
        {
            var companyCookieVal = _companyInfo;

            if (_companyInfo == null)
            {

                if (Session["CurrentCompanyInfo"] != null)
                {
                    companyCookieVal = (CompanyModel)Session["CurrentCompanyInfo"];
                }

                _companyInfo = companyCookieVal;
            }

            return companyCookieVal;
        }

    }
}
