// <copyright file="RoutingWebApiBootstrapTask.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Bootstrap.Tasks
{
    using System.Web.Http;

    using Dawn.WebApi;

    /// <summary>
    /// Routing Web API bootstrap task.
    /// </summary>
    /// <seealso cref="Dawn.WebApi.IWebApiBootstrapTask" />
    public class RoutingWebApiBootstrapTask : IWebApiBootstrapTask
    {
        /// <inheritdoc />
        public void Run(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }
    }
}