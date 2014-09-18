using System.Web.Mvc;

namespace _123Movers.Controllers
{
    /// <summary>
    /// Notification Controller
    /// </summary>
    public class NotificationController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Notifications()
        {
            return View();
        }

    }
}
