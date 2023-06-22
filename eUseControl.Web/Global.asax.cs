using eUseControl.BusinessLogic.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            CartDbInitializer.Seed();
            UserDbInitializer.Seed();
            SizeDbInitializer.Seed();
            ProductDbInitializer.Seed();

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
