using Stripe;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClinicaVeterinaria
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var secretKey = WebConfigurationManager.AppSettings["StripeSecretKey"];
            StripeConfiguration.SetApiKey(secretKey);
            //StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["sk_test_51Oxndp2NJW70IYiho1mbPVj6sJHFGFO0kUVyqW7zVXUMZ4rZdyQLnY5Ydoz0qcJsRGZFl8oa65Nr4Df2ISW3LbsM00s90wHdcC"];
        }

    }
}
