// <copyright file="HttpResponseMessageExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Newtonsoft.Json;

    /// <summary>
    /// HTTP response message extensions.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Converts the response content to the specified type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>An instance of the data type.</returns>
        public static TData As<TData>(this HttpResponseMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var responseBody = message.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(responseBody))
            {
                return default(TData);
            }

            return JsonConvert.DeserializeObject<TData>(responseBody);
        }

        /// <summary>
        /// Converts the response content to a collection of the specified type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>The collection of the data type.</returns>
        public static IEnumerable<TData> AsCollection<TData>(this HttpResponseMessage message)
        {
            return As<IEnumerable<TData>>(message);
        }
    }
}
