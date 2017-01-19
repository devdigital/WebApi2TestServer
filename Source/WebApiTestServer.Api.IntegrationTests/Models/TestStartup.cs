namespace WebApiTestServer.Api.IntegrationTests.Models
{
    using Owin;

    using WebApiTestServer.Api.Bootstrap;

    public class TestStartup : ITestStartup
    {
        public void Bootstrap(IAppBuilder app, WebApiTestServer.Registrations registrations)
        {
            var domainRegistrations = new Registrations(
                registrations.TypeRegistrations,
                registrations.InstanceRegistrations);

            new Bootstrapper(app, domainRegistrations).Run();
        }
    }
}