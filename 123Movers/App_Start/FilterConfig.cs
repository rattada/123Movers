using System.Web;
using System.Web.Mvc;
using _123Movers.Controllers;

namespace _123Movers
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new  CustomHandleErrorAttribute());
            filters.Add(new  CheckSessionOutAttribute());
        }
    }
}