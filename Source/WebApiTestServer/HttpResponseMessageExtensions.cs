namespace WebApiTestServer
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Newtonsoft.Json;

    public static class HttpResponseMessageExtensions
    {
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

        public static IEnumerable<TData> AsCollection<TData>(this HttpResponseMessage message)
        {
            return As<IEnumerable<TData>>(message);
        }
    }
}
