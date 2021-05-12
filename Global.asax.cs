﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Laeridae_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //public void Application_BeginRequest()
        //{
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "https://localhost:44376");
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
        //}
    }
}
