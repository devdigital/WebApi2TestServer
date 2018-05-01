// <copyright file="IAppBuilderConfiguration.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using Owin;

    /// <summary>
    /// App builder configuration.
    /// </summary>
    public interface IAppBuilderConfiguration
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="builder">The builder.</param>
        void Configure(IAppBuilder builder);
    }
}