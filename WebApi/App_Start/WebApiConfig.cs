using System.Web.Http;

namespace PeopleNet.MazeSolver
{
    public static class WebApiConfig
    {
        // Web API configuration and services
        public static void Register( HttpConfiguration config )
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}",
				defaults: new { action = "Default" }
			);
        }
    }
}
