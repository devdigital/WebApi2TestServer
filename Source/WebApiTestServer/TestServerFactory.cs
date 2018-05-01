// <copyright file="TestServerFactory.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Owin.Testing;
    using Owin;

    /// <summary>
    /// Test server factory.
    /// </summary>
    /// <typeparam name="TServerFactory">The type of the server factory.</typeparam>
    public abstract class TestServerFactory<TServerFactory>
        where TServerFactory : TestServerFactory<TServerFactory>
    {
        private readonly ITestStartup testStartup;

        private readonly CompositeAppBuilderConfiguration appBuilderConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestServerFactory{TServerFactory}"/> class.
        /// </summary>
        /// <param name="testStartup">The test startup.</param>
        protected TestServerFactory(ITestStartup testStartup)
        {
            this.testStartup = testStartup ?? throw new ArgumentNullException(nameof(testStartup));

            this.TypeRegistrations = new Dictionary<Type, Type>();
            this.InstanceRegistrations = new Dictionary<Type, object>();

            this.appBuilderConfiguration = new CompositeAppBuilderConfiguration();
        }

        private Dictionary<Type, Type> TypeRegistrations { get; }

        private Dictionary<Type, object> InstanceRegistrations { get; }

        /// <summary>
        /// Adds a type registration.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>The server factory.</returns>
        public TServerFactory With<TInterface, TImplementation>()
        {
            if (this.TypeRegistrations.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException($"The type {typeof(TInterface).Name} has already been registered");
            }

            this.TypeRegistrations[typeof(TInterface)] = typeof(TImplementation);
            return this as TServerFactory;
        }

        /// <summary>
        /// Adds an instance registration.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>The server factory.</returns>
        public TServerFactory With<TInterface>(object instance)
        {
            if (this.InstanceRegistrations.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException($"The type {typeof(TInterface).Name} has already been registered");
            }

            this.InstanceRegistrations[typeof(TInterface)] = instance ?? throw new ArgumentNullException(nameof(instance));
            return this as TServerFactory;
        }

        /// <summary>
        /// Adds middleware.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        /// <returns>The server factory.</returns>
        public TServerFactory WithMiddleware(Action<IAppBuilder> appBuilder)
        {
            this.appBuilderConfiguration.Add(new DelegateAppBuilderConfiguration(appBuilder));
            return this as TServerFactory;
        }

        /// <summary>
        /// Creates the test server.
        /// </summary>
        /// <returns>The test server.</returns>
        public virtual TestServer Create()
        {
            this.With<IAppBuilderConfiguration>(this.appBuilderConfiguration);

            var registrations = new Registrations(this.TypeRegistrations, this.InstanceRegistrations);
            return TestServer.Create(app => this.testStartup.Bootstrap(app, registrations));
        }
    }
}