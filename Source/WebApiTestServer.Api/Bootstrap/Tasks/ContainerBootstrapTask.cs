// <copyright file="ContainerBootstrapTask.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Bootstrap.Tasks
{
    using System.Web.Http;

    using Autofac;
    using Autofac.Features.ResolveAnything;
    using Autofac.Integration.WebApi;

    using Dawn.WebApi;

    using WebApiTestServer.Api.Repositories;

    /// <summary>
    /// Container bootstrap task.
    /// </summary>
    /// <seealso cref="Dawn.WebApi.IWebApiBootstrapTask" />
    public class ContainerBootstrapTask : IWebApiBootstrapTask
    {
        /// <summary>
        /// The registrations
        /// </summary>
        private readonly Registrations registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBootstrapTask"/> class.
        /// </summary>
        /// <param name="registrations">The registrations.</param>
        public ContainerBootstrapTask(Registrations registrations)
        {
            this.registrations = registrations;
        }

        /// <inheritdoc />
        public void Run(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            builder.RegisterType<DefaultValuesRepository>().As<IValuesRepository>();

            this.RegisterAdditional(builder);

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void RegisterAdditional(ContainerBuilder builder)
        {
            if (this.registrations == null)
            {
                return;
            }

            foreach (var typeRegistration in this.registrations.TypeRegistrations)
            {
                builder.RegisterType(typeRegistration.Value).As(typeRegistration.Key);
            }

            foreach (var instanceRegistration in this.registrations.InstanceRegistrations)
            {
                builder.RegisterInstance(instanceRegistration.Value).As(instanceRegistration.Key);
            }
        }
    }
}