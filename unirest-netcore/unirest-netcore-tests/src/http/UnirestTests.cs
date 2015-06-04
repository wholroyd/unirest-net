namespace unirest_netcore_tests.http
{
    using System.Net.Http;

    using unirest_netcore.http;

    using Xunit;

    class UnirestTest
    {
        [Fact]
        public static void Unirest_Should_Return_Correct_Verb()
        {
            Assert.Equal(HttpMethod.Get, Unirest.get("http://localhost").HttpMethod);
            Assert.Equal(HttpMethod.Post, Unirest.post("http://localhost").HttpMethod);
            Assert.Equal(HttpMethod.Delete, Unirest.delete("http://localhost").HttpMethod);
            Assert.Equal(new HttpMethod("PATCH"), Unirest.patch("http://localhost").HttpMethod);
            Assert.Equal(HttpMethod.Put, Unirest.put("http://localhost").HttpMethod);
        }

        [Fact]
        public static void Unirest_Should_Return_Correct_URL()
        {
            Assert.Equal("http://localhost", Unirest.get("http://localhost").URL.OriginalString);
            Assert.Equal("http://localhost", Unirest.post("http://localhost").URL.OriginalString);
            Assert.Equal("http://localhost", Unirest.delete("http://localhost").URL.OriginalString);
            Assert.Equal("http://localhost", Unirest.patch("http://localhost").URL.OriginalString);
            Assert.Equal("http://localhost", Unirest.put("http://localhost").URL.OriginalString);
        }
    }
}