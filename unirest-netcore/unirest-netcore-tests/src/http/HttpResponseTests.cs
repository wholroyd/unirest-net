using System;
using System.IO;
using System.Net.Http;
using unirest_netcore.http;
using Xunit;

namespace unirest_netcore_tests.http
{
    public class HttpResponseTests
    {
        [Fact]
        public static void HttpResponse_Should_Construct()
        {
            Action stringResponse = () => new HttpResponse<string>(new HttpResponseMessage { Content = new StringContent("test") });
            Action streamResponse = () => new HttpResponse<Stream>(new HttpResponseMessage { Content = new StreamContent(new MemoryStream())});
            Action objectResponse = () => new HttpResponse<int>(new HttpResponseMessage { Content = new StringContent("1")});

            stringResponse();
            streamResponse();
            objectResponse();
        }
    }
}
