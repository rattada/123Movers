using _123Movers.Controllers;
using System.Web.Mvc;

namespace _123Movers
{
    /// <summary>
    /// Filter config class
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CheckSessionOutAttribute());
        }
    }
}