// <copyright file="Startup.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

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

    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
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

        /// <summary>
        /// Configures the routing.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void ConfigureRouting(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }

        /// <summary>
        /// Configures the serialization.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
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

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
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
