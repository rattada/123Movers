using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class NotificationController : Controller
    {
        //
        // GET: /Notification/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Notifications()
        {
            return View();
        }

    }
}
