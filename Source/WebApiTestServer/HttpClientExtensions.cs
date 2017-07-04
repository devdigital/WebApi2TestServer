namespace WebApiTestServer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri)
        {
            return PostAsync(httpClient, CreateUri(requestUri));
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, Uri requestUri)
        {
            return httpClient.PostAsync(requestUri, content: null);
        }

        private static Uri CreateUri(string uri)
        {
            return string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);
        }
    }
}