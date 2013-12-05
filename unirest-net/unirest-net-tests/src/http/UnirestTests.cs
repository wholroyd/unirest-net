﻿using System;
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
            Unirest.get("http://localhost").Method.Should().Be(HttpMethod.Get);
            Unirest.post("http://localhost").Method.Should().Be(HttpMethod.Post);
            Unirest.delete("http://localhost").Method.Should().Be(HttpMethod.Delete);
            Unirest.patch("http://localhost").Method.Should().Be(new HttpMethod("PATCH"));
            Unirest.put("http://localhost").Method.Should().Be(HttpMethod.Put);
        }

        [Test]
        public static void Unicorn_Should_Return_Correct_URL()
        {
            Unirest.get("http://localhost").RequestUri.OriginalString.Should().Be("http://localhost");
            Unirest.post("http://localhost").RequestUri.OriginalString.Should().Be("http://localhost");
            Unirest.delete("http://localhost").RequestUri.OriginalString.Should().Be("http://localhost");
            Unirest.patch("http://localhost").RequestUri.OriginalString.Should().Be("http://localhost");
            Unirest.put("http://localhost").RequestUri.OriginalString.Should().Be("http://localhost");
        }
    }
}
