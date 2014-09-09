using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;

namespace _123Movers.Controllers
{
    public class ReportController : BaseController
    {
        public ActionResult Reports(CompanyModel _companyInfo)
        {
            ReportModel report = new ReportModel();
            report._companyInfo = _companyInfo;
            return View(report);
        }

    }
}
