using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace YQMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 移除 WebForm 视图引擎
        /// </summary>
        void RemoveFormEngines()
        {
            var viewEngines = ViewEngines.Engines;              // 获取系统中所有的 视图引擎
            var webFormEngines = viewEngines.OfType<WebFormViewEngine>().FirstOrDefault();      // 获得 webFormEngines 引擎
            if (webFormEngines != null)         
            {
                viewEngines.Remove(webFormEngines);             // 移除相对应的视图引擎
            }
        }
        protected void Application_Start()
        {
            RemoveFormEngines();            // 移除 WebForm 视图引擎
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
