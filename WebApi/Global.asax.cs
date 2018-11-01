using System.Web.Http;

namespace PeopleNet.MazeSolver
{
	public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure( WebApiConfig.Register );
		}
	}
}
