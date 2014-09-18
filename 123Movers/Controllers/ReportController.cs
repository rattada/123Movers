using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Movers.Models;

namespace _123Movers.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <returns></returns>
        public ActionResult Reports(CompanyModel companyInfo)
        {
            var report = new ReportModel
                {
                    _companyInfo = companyInfo
                };
            return View(report);
        }

    }
}
