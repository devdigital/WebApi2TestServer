namespace WebApiTestServer.Api.Bootstrap.Tasks
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Dawn.WebApi;

    using Newtonsoft.Json.Serialization;

    public class JsonBootstrapTask : IWebApiBootstrapTask
    {
        public void Run(HttpConfiguration configuration)
        {
            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}