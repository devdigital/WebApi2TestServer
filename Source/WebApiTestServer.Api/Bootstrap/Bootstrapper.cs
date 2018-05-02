// <copyright file="Bootstrapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Bootstrap
{
    using System;

    using Dawn.Owin;

    using Owin;

    /// <summary>
    /// Bootstrapper.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// The application
        /// </summary>
        private readonly IAppBuilder app;

        /// <summary>
        /// The registrations
        /// </summary>
        private readonly Registrations registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="registrations">The registrations.</param>
        public Bootstrapper(IAppBuilder app, Registrations registrations = null)
        {
            this.app = app ?? throw new ArgumentNullException(nameof(app));
            this.registrations = registrations;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            new OwinBootstrapper().Run(this.app, new[] { new ApiBootstrapTask(this.registrations) });
        }
    }
}