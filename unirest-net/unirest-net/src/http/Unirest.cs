using System.Net.Http;

using unirest_net.request;

namespace unirest_net.http
{
    public class Unirest
    {
        // Should add overload that takes URL object
        public static HttpRequestMessage get(string url)
        {
            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        public static HttpRequestMessage post(string url)
        {
            return new HttpRequestMessage(HttpMethod.Post, url);
        }

        public static HttpRequestMessage delete(string url)
        {
            return new HttpRequestMessage(HttpMethod.Delete, url);
        }

        public static HttpRequestMessage patch(string url)
        {
            return new HttpRequestMessage(new HttpMethod("PATCH"), url);
        }

        public static HttpRequestMessage put(string url)
        {
            return new HttpRequestMessage(HttpMethod.Put, url);
        }
    }
}
