// <copyright file="CompositeAppBuilderConfiguration.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using System.Collections.Generic;
    using Owin;

    /// <summary>
    /// Composite app builder configuration.
    /// </summary>
    /// <seealso cref="WebApiTestServer.IAppBuilderConfiguration" />
    public class CompositeAppBuilderConfiguration : IAppBuilderConfiguration
    {
        private readonly ICollection<IAppBuilderConfiguration> configurations;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAppBuilderConfiguration"/> class.
        /// </summary>
        public CompositeAppBuilderConfiguration()
        {
            this.configurations = new List<IAppBuilderConfiguration>();
        }

        /// <summary>
        /// Adds an application builder configuration.
        /// </summary>
        /// <param name="appBuilderConfiguration">The application builder configuration.</param>
        public void Add(IAppBuilderConfiguration appBuilderConfiguration)
        {
            this.configurations.Add(appBuilderConfiguration);
        }

        /// <inheritdoc />
        public void Configure(IAppBuilder builder)
        {
            foreach (var configuration in this.configurations)
            {
                configuration.Configure(builder);
            }
        }
    }
}