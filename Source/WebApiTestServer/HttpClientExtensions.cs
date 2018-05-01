// <copyright file="HttpClientExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// HTTP client extensions.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Posts a request with no content.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <returns>The HTTP response message.</returns>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri)
        {
            return PostAsync(httpClient, CreateUri(requestUri));
        }

        /// <summary>
        /// Posts a request with no content.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <returns>The HTTP response message.</returns>
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