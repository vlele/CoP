using System;
using Owin;
using System.Web.Http;

namespace CoP.WebAPI
{
    public class Startup : IOwinAppBuilder
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            FormatterConfig.ConfigureFormatters(config.Formatters);
            RouteConfig.RegisterRoutes(config.Routes);

            config.EnableCors();
            appBuilder.UseWebApi(config);
        }
    }
}
