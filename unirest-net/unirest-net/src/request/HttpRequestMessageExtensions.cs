using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace unirest_net
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage header(this HttpRequestMessage request, string name, string value)
        {
            request.Headers.Add(name, value);
            return request;
        }

        public static HttpRequestMessage headers(this HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return request;
        }
    }
}
