using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiTestServer.Api.Startup))]

namespace WebApiTestServer.Api
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Autofac;
    using Autofac.Features.ResolveAnything;
    using Autofac.Integration.WebApi;

    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new Bootstrap.Bootstrapper(app).Run();
            var configuration = new HttpConfiguration();
            ConfigureRouting(configuration);
            ConfigureSerialization(configuration);
            ConfigureContainer(configuration);

            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);

            app.UseWebApi(configuration);
        }

        private static void ConfigureRouting(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }

        private static void ConfigureSerialization(HttpConfiguration configuration)
        {
            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            if (jsonFormatter == null)
            {
                return;
            }

            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        private static void ConfigureContainer(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
