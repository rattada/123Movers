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

        public ActionResult Reports(CompanyModel _companyInfo)
        {
            ReportModel report = new ReportModel();
            report._companyInfo = _companyInfo;
            return View(report);
        }

    }
}
