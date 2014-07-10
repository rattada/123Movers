using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;

namespace _123Movers.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Reports(string companyid, string companyName, string ax, string contactperson, string suspended, bool active = false)
        public ActionResult Reports(CompanyModel _companyInfo)
        {
            Session["CompanyId"] = _companyInfo.CompanyId;
            Session["CompanyName"] = _companyInfo.CompanyName;
            Session["Ax"] = _companyInfo.AX;
            Session["IsActive"] = _companyInfo.IsActive;
            Session["Suspended"] = _companyInfo.Suspended;
            Session["ContactPerson"] = _companyInfo.ContactPerson;

            return View();
        }

    }
}
