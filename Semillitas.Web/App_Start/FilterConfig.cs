﻿using Semillitas.Web.Classes;
using System.Web;
using System.Web.Mvc;

namespace Semillitas.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new LocalizationAttribute("en"), 0);
        }
    }
}
