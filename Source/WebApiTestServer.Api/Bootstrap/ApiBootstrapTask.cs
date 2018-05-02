// <copyright file="ApiBootstrapTask.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Bootstrap
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Dawn.Owin;
    using Dawn.WebApi;

    using Owin;

    using WebApiTestServer.Api.Bootstrap.Tasks;

    /// <summary>
    /// API bootstrap task.
    /// </summary>
    /// <seealso cref="Dawn.Owin.IOwinBootstrapTask" />
    public class ApiBootstrapTask : IOwinBootstrapTask
    {
        /// <summary>
        /// The registrations
        /// </summary>
        private readonly Registrations registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiBootstrapTask"/> class.
        /// </summary>
        /// <param name="registrations">The registrations.</param>
        public ApiBootstrapTask(Registrations registrations)
        {
            this.registrations = registrations;
        }

        /// <inheritdoc />
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