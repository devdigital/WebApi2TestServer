namespace WebApiTestServer.Api.Bootstrap
{
    using System;

    using Dawn.Owin;
    using Dawn.SampleApi.Bootstrap;

    using Owin;

    public class Bootstrapper
    {
        private readonly IAppBuilder app;

        private readonly Registrations registrations;

        public Bootstrapper(IAppBuilder app, Registrations registrations = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            this.app = app;
            this.registrations = registrations;
        }

        public void Run()
        {
            new OwinBootstrapper().Run(this.app, new[] { new ApiBootstrapTask(this.registrations) });
        }
    }
}