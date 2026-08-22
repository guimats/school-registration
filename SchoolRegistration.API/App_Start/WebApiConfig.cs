using SchoolRegistration.API.Filters;
using System.Web.Http;

namespace SchoolRegistration.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Registra o interceptador de erros global
            config.Filters.Add(new CustomExceptionFilterAttribute());

            // Remove o formatador XML (força JSON)
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
