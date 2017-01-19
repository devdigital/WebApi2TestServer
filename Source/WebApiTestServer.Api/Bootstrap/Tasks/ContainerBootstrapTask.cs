namespace WebApiTestServer.Api.Bootstrap.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Autofac;
    using Autofac.Features.ResolveAnything;
    using Autofac.Integration.WebApi;

    using Dawn.WebApi;

    public class ContainerBootstrapTask : IWebApiBootstrapTask
    {        
        private readonly Registrations registrations;

        public ContainerBootstrapTask(Registrations registrations)
        {
            this.registrations = registrations;
        }

        public void Run(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            
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
                builder.RegisterInstance(instanceRegistration.Value);
            }
        }
    }
}