using System.Web;
using System.Web.Mvc;

namespace _123Movers.BusinessEntities
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}