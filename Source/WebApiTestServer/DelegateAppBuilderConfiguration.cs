// <copyright file="DelegateAppBuilderConfiguration.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using System;
    using Owin;

    /// <summary>
    /// Delegate app builder configuration.
    /// </summary>
    public class DelegateAppBuilderConfiguration : IAppBuilderConfiguration
    {
        private readonly Action<IAppBuilder> appBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateAppBuilderConfiguration"/> class.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        public DelegateAppBuilderConfiguration(Action<IAppBuilder> appBuilder)
        {
            this.appBuilder = appBuilder ?? throw new ArgumentNullException(nameof(appBuilder));
        }

        /// <inheritdoc />
        public void Configure(IAppBuilder builder)
        {
            this.appBuilder(builder);
        }
    }
}