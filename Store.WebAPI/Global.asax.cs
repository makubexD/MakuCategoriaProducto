using Store.WebAPI.App_Start;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Store.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var http = GlobalConfiguration.Configuration;

            var container = DependencyInjection.Configure(http);
            GlobalConfiguration.Configure(WebApiConfig.Register);            
            
        }
    }
}
