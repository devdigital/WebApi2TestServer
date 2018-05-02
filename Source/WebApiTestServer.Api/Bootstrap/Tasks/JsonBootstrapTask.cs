// <copyright file="JsonBootstrapTask.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Bootstrap.Tasks
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Dawn.WebApi;

    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// JSON bootstrap task.
    /// </summary>
    /// <seealso cref="Dawn.WebApi.IWebApiBootstrapTask" />
    public class JsonBootstrapTask : IWebApiBootstrapTask
    {
        /// <inheritdoc />
        public void Run(HttpConfiguration configuration)
        {
            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}