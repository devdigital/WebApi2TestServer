namespace WebApiTestServer.Api.Bootstrap.Tasks
{
    using System.Web.Http;

    using Dawn.WebApi;

    public class RoutingWebApiBootstrapTask : IWebApiBootstrapTask
    {
        public void Run(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }
    }
}