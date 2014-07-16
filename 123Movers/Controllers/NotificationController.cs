using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123Movers.Controllers
{
    public class NotificationController : BaseController
    {
        //
        // GET: /Notification/
        //protected NotificationController() 
        //{
        //    //logger = LogManager.GetLogger(typeof(NotificationController)); 
        //}

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
