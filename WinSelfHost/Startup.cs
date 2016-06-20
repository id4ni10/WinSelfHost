using Owin;
using Microsoft.Owin;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(WinSelfHost.Startup))]

namespace WinSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ManipularRequisicaoHttp(config);

            app.UseWebApi(config);
        }

        private void ManipularRequisicaoHttp(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(name: "Error404", routeTemplate: "{*url}", defaults: new { controller = "Error", action = "Handle404" });

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.Indent = true;

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
