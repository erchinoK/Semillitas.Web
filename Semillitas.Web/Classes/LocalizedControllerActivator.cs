﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Semillitas.Web.Classes
{
    public class LocalizedControllerActivator : IControllerActivator
    {
        private string _DefaultLanguage = "ca";

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            //Get the {language} parameter in the RouteData
            string lang = (String)requestContext.RouteData.Values["language"] ?? _DefaultLanguage;

            if (lang != _DefaultLanguage)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                }
                catch (Exception e)
                {
                    throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
                }
            }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}