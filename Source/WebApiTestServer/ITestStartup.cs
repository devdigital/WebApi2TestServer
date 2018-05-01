// <copyright file="ITestStartup.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using Owin;

    /// <summary>
    /// Test startup.
    /// </summary>
    public interface ITestStartup
    {
        /// <summary>
        /// Bootstraps the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="registrations">The registrations.</param>
        void Bootstrap(IAppBuilder app, Registrations registrations);
    }
}
