namespace Dawn.SampleApi.Bootstrap
{    
    using System.Collections.Generic;
    using System.Web.Http;
 
    using global::Dawn.Owin;
    using global::Dawn.WebApi;

    using global::Owin;

    using WebApiTestServer.Api;
    using WebApiTestServer.Api.Bootstrap.Tasks;

    public class ApiBootstrapTask : IOwinBootstrapTask
    {        
        private readonly Registrations registrations;

        public ApiBootstrapTask(Registrations registrations)
        {
            this.registrations = registrations;
        }

        public void Run(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            new ContainerBootstrapTask(this.registrations).Run(configuration);
            var tasks = new List<IWebApiBootstrapTask>
            {
                new RoutingWebApiBootstrapTask(),
                new JsonBootstrapTask()
            };

            new WebApiBootstrapper().Run(configuration, tasks);
            app.UseWebApi(configuration);
        }
    }
}