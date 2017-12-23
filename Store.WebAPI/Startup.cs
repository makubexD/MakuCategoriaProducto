//using Microsoft.Owin;
//using Owin;
//using Store.WebAPI;
//using Store.WebAPI.App_Start;
//using System.Web.Http;
//[assembly: OwinStartup(typeof(Startup))]
//namespace Store.WebAPI
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            var http = GlobalConfiguration.Configuration;
           
//            var container = DependencyInjection.Configure(http);
//            GlobalConfiguration.Configure(WebApiConfig.Register);
//            GlobalConfiguration.Configuration.EnsureInitialized();

//        }
//    }
//}