﻿using _123Movers.Controllers;
using System.Web;
using System.Web.Mvc;

namespace _123Movers
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CheckSessionOutAttribute());
        }
    }
}