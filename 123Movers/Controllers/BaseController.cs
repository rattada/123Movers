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
        //protected int? _companyId;
        protected CompanyModel  _companyInfo;
        public ILog logger;

        public BaseController()
        {
            //logger = LogManager.GetLogger(typeof(controller));
        }
        //public int? CompanyId
        //{
        //    get
        //    {
        //        if (_companyId == null) { RetrieveCurrentCompanyId(); }
        //        return _companyId;
        //    }
        //}

        //public void SaveCompanyId(int? id)
        //{
        //    var cookie = new HttpCookie("CompanyId")
        //    {
        //        Value = Convert.ToString(id)
        //    };
        //    _companyId = id;

        //    Response.Cookies.Add(cookie);
        //}

        //protected string RetrieveCurrentCompanyId()
        //{
        //    var companyCookieVal = _companyId.ToString();

        //    if (_companyId == null)
        //    {

        //        if (Request.Cookies.AllKeys.Contains("CompanyId"))
        //        {
        //            companyCookieVal = Request.Cookies["CompanyId"].Value;
        //        }

        //        _companyId = int.Parse(companyCookieVal);
        //    }

        //    return companyCookieVal;
        //}



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
