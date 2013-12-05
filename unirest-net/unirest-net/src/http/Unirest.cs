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

        public static HttpRequest post(string url)
        {
            return new HttpRequest(HttpMethod.Post, url);
        }

        public static HttpRequest delete(string url)
        {
            return new HttpRequest(HttpMethod.Delete, url);
        }

        public static HttpRequest patch(string url)
        {
            return new HttpRequest(new HttpMethod("PATCH"), url);
        }

        public static HttpRequest put(string url)
        {
            return new HttpRequest(HttpMethod.Put, url);
        }
    }
}
