using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit;
using NUnit.Framework;
using FluentAssertions;

using unirest_net;
using unirest_net.http;
using unirest_net.request;

using System.Net.Http;

namespace unirest_net.http
{
    [TestFixture]
    class UnicornTest
    {
        [Test]
        public static void Unicorn_Should_Return_Correct_Verb()
        {
            Unirest.Get("http://localhost").HttpMethod.Should().Be(HttpMethod.Get);
            Unirest.Post("http://localhost").HttpMethod.Should().Be(HttpMethod.Post);
            Unirest.Delete("http://localhost").HttpMethod.Should().Be(HttpMethod.Delete);
            Unirest.Patch("http://localhost").HttpMethod.Should().Be(new HttpMethod("PATCH"));
            Unirest.Put("http://localhost").HttpMethod.Should().Be(HttpMethod.Put);
        }

        [Test]
        public static void Unicorn_Should_Return_Correct_URL()
        {
            Unirest.Get("http://localhost").URL.OriginalString.Should().Be("http://localhost");
            Unirest.Post("http://localhost").URL.OriginalString.Should().Be("http://localhost");
            Unirest.Delete("http://localhost").URL.OriginalString.Should().Be("http://localhost");
            Unirest.Patch("http://localhost").URL.OriginalString.Should().Be("http://localhost");
            Unirest.Put("http://localhost").URL.OriginalString.Should().Be("http://localhost");
        }
    }
}
