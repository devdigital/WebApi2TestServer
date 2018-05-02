// <copyright file="MyTestServerFactory.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.IntegrationTests.Models
{
    using Xunit.Abstractions;

    /// <summary>
    /// Test server factory example.
    /// </summary>
    public class MyTestServerFactory : TestServerFactory<MyTestServerFactory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyTestServerFactory"/> class.
        /// </summary>
        public MyTestServerFactory()
            : base(new TestStartup())
        {
        }

        /// <summary>
        /// Add logging example.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>The test server factory.</returns>
        public MyTestServerFactory WithLogging(ITestOutputHelper output)
        {
            return this;
        }
    }
}