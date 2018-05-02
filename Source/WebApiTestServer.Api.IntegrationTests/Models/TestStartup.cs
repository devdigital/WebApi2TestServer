// <copyright file="TestStartup.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.IntegrationTests.Models
{
    using Owin;

    using WebApiTestServer.Api.Bootstrap;

    /// <summary>
    /// Test startup
    /// </summary>
    /// <seealso cref="WebApiTestServer.ITestStartup" />
    public class TestStartup : ITestStartup
    {
        /// <inheritdoc />
        public void Bootstrap(IAppBuilder app, WebApiTestServer.Registrations registrations)
        {
            var domainRegistrations = new Registrations(
                registrations.TypeRegistrations,
                registrations.InstanceRegistrations);

            new Bootstrapper(app, domainRegistrations).Run();
        }
    }
}