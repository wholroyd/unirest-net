using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using FluentAssertions;

using unirest_net;
using unirest_net.http;
using unirest_net.request;

using System.Net.Http;

namespace unicorn_net_tests.request
{
    [TestFixture]
    class HttpRequestTests
    {
        [Test]
        public static void HttpRequest_Should_Construct()
        {
            Action Get = () => new HttpRequest(HttpMethod.Get, "http://localhost");
            Action Post = () => new HttpRequest(HttpMethod.Post, "http://localhost");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "http://localhost");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            Action Put = () => new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.ShouldNotThrow();
            Post.ShouldNotThrow();
            Delete.ShouldNotThrow();
            Patch.ShouldNotThrow();
            Put.ShouldNotThrow();
        }

        [Test]
        public static void HttpRequest_Should_Not_Construct_With_Invalid_URL()
        {
            Action Get = () => new HttpRequest(HttpMethod.Get, "http:///invalid");
            Action Post = () => new HttpRequest(HttpMethod.Post, "http:///invalid");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "http:///invalid");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "http:///invalid");
            Action Put = () => new HttpRequest(HttpMethod.Put, "http:///invalid");

            Get.ShouldThrow<ArgumentException>();
            Post.ShouldThrow<ArgumentException>();
            Delete.ShouldThrow<ArgumentException>();
            Patch.ShouldThrow<ArgumentException>();
            Put.ShouldThrow<ArgumentException>();
        }

        [Test]
        public static void HttpRequest_Should_Not_Construct_With_None_HTTP_URL()
        {
            Action Get = () => new HttpRequest(HttpMethod.Get, "ftp://localhost");
            Action Post = () => new HttpRequest(HttpMethod.Post, "mailto:localhost");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "news://localhost");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "about:blank");
            Action Put = () => new HttpRequest(HttpMethod.Put, "about:settings");

            Get.ShouldThrow<ArgumentException>();
            Post.ShouldThrow<ArgumentException>();
            Delete.ShouldThrow<ArgumentException>();
            Patch.ShouldThrow<ArgumentException>();
            Put.ShouldThrow<ArgumentException>();
        }

        [Test]
        public static void HttpRequest_Should_Construct_With_Correct_Verb()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.HttpMethod.Should().Be(HttpMethod.Get);
            Post.HttpMethod.Should().Be(HttpMethod.Post);
            Delete.HttpMethod.Should().Be(HttpMethod.Delete);
            Patch.HttpMethod.Should().Be(new HttpMethod("PATCH"));
            Put.HttpMethod.Should().Be(HttpMethod.Put);
        }

        [Test]
        public static void HttpRequest_Should_Construct_With_Correct_URL()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.URL.OriginalString.Should().Be("http://localhost");
            Post.URL.OriginalString.Should().Be("http://localhost");
            Delete.URL.OriginalString.Should().Be("http://localhost");
            Patch.URL.OriginalString.Should().Be("http://localhost");
            Put.URL.OriginalString.Should().Be("http://localhost");
        }

        [Test]
        public static void HttpRequest_Should_Construct_With_Headers()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.Headers.Should().NotBeNull();
            Post.URL.OriginalString.Should().NotBeNull();
            Delete.URL.OriginalString.Should().NotBeNull();
            Patch.URL.OriginalString.Should().NotBeNull();
            Put.URL.OriginalString.Should().NotBeNull();
        }

        [Test]
        public static void HttpRequest_Should_Add_Headers()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.WithHeader("User-Agent", "unirest-net/1.0");
            Post.WithHeader("User-Agent", "unirest-net/1.0");
            Delete.WithHeader("User-Agent", "unirest-net/1.0");
            Patch.WithHeader("User-Agent", "unirest-net/1.0");
            Put.WithHeader("User-Agent", "unirest-net/1.0");

            Get.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Post.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Delete.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Patch.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Put.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
        }

        [Test]
        public static void HttpRequest_Should_Add_Headers_Dictionary()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Post.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Delete.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Patch.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Put.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });

            Get.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Post.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Delete.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Patch.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Put.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
        }

        [Test]
        public static void HttpRequest_Should_Return_String()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com");
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com");
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com");

            Get.AsString().Body.Should().NotBeBlank();
            Post.AsString().Body.Should().NotBeBlank();
            Delete.AsString().Body.Should().NotBeBlank();
            Patch.AsString().Body.Should().NotBeBlank();
            Put.AsString().Body.Should().NotBeBlank();
        }

        [Test]
        public static void HttpRequest_Should_Return_Stream()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com");
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com");
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com");

            Get.AsBinary().Body.Should().NotBeNull();
            Post.AsBinary().Body.Should().NotBeNull();
            Delete.AsBinary().Body.Should().NotBeNull();
            Patch.AsBinary().Body.Should().NotBeNull();
            Put.AsBinary().Body.Should().NotBeNull();
        }

        [Test]
        public static void HttpRequest_Should_Return_Parsed_JSON()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com");
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com");
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com");

            Get.AsJson<String>().Body.Should().NotBeBlank();
            Post.AsJson<String>().Body.Should().NotBeBlank();
            Delete.AsJson<String>().Body.Should().NotBeBlank();
            Patch.AsJson<String>().Body.Should().NotBeBlank();
            Put.AsJson<String>().Body.Should().NotBeBlank();
        }

        [Test]
        public static void HttpRequest_Should_Return_String_Async()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com").AsStringAsync();
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com").AsStringAsync();
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com").AsStringAsync();
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com").AsStringAsync();
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com").AsStringAsync();

            Task.WaitAll(Get, Post, Delete, Patch, Put);

            Get.Result.Body.Should().NotBeBlank();
            Post.Result.Body.Should().NotBeBlank();
            Delete.Result.Body.Should().NotBeBlank();
            Patch.Result.Body.Should().NotBeBlank();
            Put.Result.Body.Should().NotBeBlank();
        }

        [Test]
        public static void HttpRequest_Should_Return_Stream_Async()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com").AsBinaryAsync();
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com").AsBinaryAsync();
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com").AsBinaryAsync();
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com").AsBinaryAsync();
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com").AsBinaryAsync();

            Task.WaitAll(Get, Post, Delete, Patch, Put);

            Get.Result.Body.Should().NotBeNull();
            Post.Result.Body.Should().NotBeNull();
            Delete.Result.Body.Should().NotBeNull();
            Patch.Result.Body.Should().NotBeNull();
            Put.Result.Body.Should().NotBeNull();
        }

        [Test]
        public static void HttpRequest_Should_Return_Parsed_JSON_Async()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com").AsJsonAsync<String>();
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com").AsJsonAsync<String>();
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com").AsJsonAsync<String>();
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com").AsJsonAsync<String>();
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com").AsJsonAsync<String>();

            Task.WaitAll(Get, Post, Delete, Patch, Put);

            Get.Result.Body.Should().NotBeBlank();
            Post.Result.Body.Should().NotBeBlank();
            Delete.Result.Body.Should().NotBeBlank();
            Patch.Result.Body.Should().NotBeBlank();
            Put.Result.Body.Should().NotBeBlank();
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Construct()
        {
            Action Post = () => new HttpRequest(HttpMethod.Post, "http://localhost");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "http://localhost");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            Action Put = () => new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.ShouldNotThrow();
            Delete.ShouldNotThrow();
            Patch.ShouldNotThrow();
            Put.ShouldNotThrow();
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Not_Construct_With_Invalid_URL()
        {
            Action Post = () => new HttpRequest(HttpMethod.Post, "http:///invalid");
            Action delete = () => new HttpRequest(HttpMethod.Delete, "http:///invalid");
            Action patch = () => new HttpRequest(new HttpMethod("PATCH"), "http:///invalid");
            Action put = () => new HttpRequest(HttpMethod.Put, "http:///invalid");

            Post.ShouldThrow<ArgumentException>();
            delete.ShouldThrow<ArgumentException>();
            patch.ShouldThrow<ArgumentException>();
            put.ShouldThrow<ArgumentException>();
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Not_Construct_With_None_HTTP_URL()
        {
            Action Post = () => new HttpRequest(HttpMethod.Post, "mailto:localhost");
            Action delete = () => new HttpRequest(HttpMethod.Delete, "news://localhost");
            Action patch = () => new HttpRequest(new HttpMethod("PATCH"), "about:blank");
            Action put = () => new HttpRequest(HttpMethod.Put, "about:settings");

            Post.ShouldThrow<ArgumentException>();
            delete.ShouldThrow<ArgumentException>();
            patch.ShouldThrow<ArgumentException>();
            put.ShouldThrow<ArgumentException>();
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Construct_With_Correct_Verb()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.HttpMethod.Should().Be(HttpMethod.Post);
            Delete.HttpMethod.Should().Be(HttpMethod.Delete);
            Patch.HttpMethod.Should().Be(new HttpMethod("PATCH"));
            Put.HttpMethod.Should().Be(HttpMethod.Put);
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Construct_With_Correct_URL()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.URL.OriginalString.Should().Be("http://localhost");
            Delete.URL.OriginalString.Should().Be("http://localhost");
            Patch.URL.OriginalString.Should().Be("http://localhost");
            Put.URL.OriginalString.Should().Be("http://localhost");
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Construct_With_Headers()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.URL.OriginalString.Should().NotBeNull();
            Delete.URL.OriginalString.Should().NotBeNull();
            Patch.URL.OriginalString.Should().NotBeNull();
            Put.URL.OriginalString.Should().NotBeNull();
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Add_Headers()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.WithHeader("User-Agent", "unirest-net/1.0");
            Delete.WithHeader("User-Agent", "unirest-net/1.0");
            Patch.WithHeader("User-Agent", "unirest-net/1.0");
            Put.WithHeader("User-Agent", "unirest-net/1.0");

            Post.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Delete.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Patch.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Put.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Add_Headers_Dictionary()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Delete.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Patch.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });
            Put.WithHeaders(new Dictionary<string, string> { { "User-Agent", "unirest-net/1.0" } });

            Post.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Delete.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Patch.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
            Put.Headers.Should().Contain("User-Agent", "unirest-net/1.0");
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Encode_Fields()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.WithField("key", "value");
            Delete.WithField("key", "value");
            Patch.WithField("key", "value");
            Put.WithField("key", "value");

            Post.Body.Should().NotBeEmpty();
            Delete.Body.Should().NotBeEmpty();
            Patch.Body.Should().NotBeEmpty();
            Put.Body.Should().NotBeEmpty();
        }

        [Test]
        public static void HttpRequest_With_Body_Should_Encode_File()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var stream = new MemoryStream();

            Post.WithField(stream);
            Delete.WithField(stream);
            Patch.WithField(stream);
            Put.WithField(stream);

            Post.Body.Should().NotBeEmpty();
            Delete.Body.Should().NotBeEmpty();
            Patch.Body.Should().NotBeEmpty();
            Put.Body.Should().NotBeEmpty();
        }

        [Test]
        public static void HttpRequestWithBody_Should_Encode_Multiple_Fields()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var dict = new Dictionary<string, object>
                {
                    {"key", "value"},
                    {"key2", "value2"},
                    {"key3", new MemoryStream()}
                };

            Post.WithFields(dict);
            Delete.WithFields(dict);
            Patch.WithFields(dict);
            Put.WithFields(dict);

            Post.Body.Should().NotBeEmpty();
            Delete.Body.Should().NotBeEmpty();
            Patch.Body.Should().NotBeEmpty();
            Put.Body.Should().NotBeEmpty();
        }

        [Test]
        public static void HttpRequestWithBody_Should_Add_String_Body()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.WithBody("test");
            Delete.WithBody("test");
            Patch.WithBody("test");
            Put.WithBody("test");

            Post.Body.Should().NotBeEmpty();
            Delete.Body.Should().NotBeEmpty();
            Patch.Body.Should().NotBeEmpty();
            Put.Body.Should().NotBeEmpty();
        }

        [Test]
        public static void HttpRequestWithBody_Should_Add_JSON_Body()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.WithBody(new List<int> { 1, 2, 3 });
            Delete.WithBody(new List<int> { 1, 2, 3 });
            Patch.WithBody(new List<int> { 1, 2, 3 });
            Put.WithBody(new List<int> { 1, 2, 3 });

            Post.Body.Should().NotBeEmpty();
            Delete.Body.Should().NotBeEmpty();
            Patch.Body.Should().NotBeEmpty();
            Put.Body.Should().NotBeEmpty();
        }

        [Test]
        public static void Http_Request_Shouldnt_Add_Fields_To_Get()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            Action addStringField = () => Get.WithField("name", "value");
            Action addKeyField = () => Get.WithField(new MemoryStream());
            Action addStringFields = () => Get.WithFields(new Dictionary<string, object> {{"name", "value"}});
            Action addKeyFields = () => Get.WithFields(new Dictionary<string, object> {{"key", new MemoryStream()}});

            addStringField.ShouldThrow<InvalidOperationException>();
            addKeyField.ShouldThrow<InvalidOperationException>();
            addStringFields.ShouldThrow<InvalidOperationException>();
            addKeyFields.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public static void Http_Request_Shouldnt_Add_Body_To_Get()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            Action addStringBody = () => Get.WithBody("string");
            Action addJSONBody = () => Get.WithBody(new List<int> {1,2,3});

            addStringBody.ShouldThrow<InvalidOperationException>();
            addJSONBody.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public static void HttpRequestWithBody_Should_Not_Allow_Body_For_Request_With_Field()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var stream = new MemoryStream();

            Post.WithField(stream);
            Delete.WithField(stream);
            Patch.WithField(stream);
            Put.WithField(stream);

            Action addBodyPost = () => Post.WithBody("test");
            Action addBodyDelete = () => Delete.WithBody("test");
            Action addBodyPatch = () => Patch.WithBody("test");
            Action addBodyPut = () => Put.WithBody("test");
            Action addObjectBodyPost = () => Post.WithBody(1);
            Action addObjectBodyDelete = () => Delete.WithBody(1);
            Action addObjectBodyPatch = () => Patch.WithBody(1);
            Action addObjectBodyPut = () => Put.WithBody(1);

            addBodyPost.ShouldThrow<InvalidOperationException>();
            addBodyDelete.ShouldThrow<InvalidOperationException>();
            addBodyPatch.ShouldThrow<InvalidOperationException>();
            addBodyPut.ShouldThrow<InvalidOperationException>();
            addObjectBodyPost.ShouldThrow<InvalidOperationException>();
            addObjectBodyDelete.ShouldThrow<InvalidOperationException>();
            addObjectBodyPatch.ShouldThrow<InvalidOperationException>();
            addObjectBodyPut.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public static void HttpRequestWithBody_Should_Not_Allow_Fields_For_Request_With_Body()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var stream = new MemoryStream();

            Post.WithBody("test");
            Delete.WithBody("test");
            Patch.WithBody("test");
            Put.WithBody("lalala");

            Action addFieldPost = () => Post.WithField("key", "value");
            Action addFieldDelete = () => Delete.WithField("key", "value");
            Action addFieldPatch = () => Patch.WithField("key", "value");
            Action addFieldPut = () => Put.WithField("key", "value");
            Action addStreamFieldPost = () => Post.WithField(stream);
            Action addStreamFieldDelete = () => Delete.WithField(stream);
            Action addStreamFieldPatch = () => Patch.WithField(stream);
            Action addStreamFieldPut = () => Put.WithField(stream);
            Action addFieldsPost = () => Post.WithFields(new Dictionary<string, object> {{"test", "test"}});
            Action addFieldsDelete = () => Delete.WithFields(new Dictionary<string, object> {{"test", "test"}});
            Action addFieldsPatch = () => Patch.WithFields(new Dictionary<string, object> {{"test", "test"}});
            Action addFieldsPut = () => Put.WithFields(new Dictionary<string, object> {{"test", "test"}});

            addFieldPost.ShouldThrow<InvalidOperationException>();
            addFieldDelete.ShouldThrow<InvalidOperationException>();
            addFieldPatch.ShouldThrow<InvalidOperationException>();
            addFieldPut.ShouldThrow<InvalidOperationException>();
            addStreamFieldPost.ShouldThrow<InvalidOperationException>();
            addStreamFieldDelete.ShouldThrow<InvalidOperationException>();
            addStreamFieldPatch.ShouldThrow<InvalidOperationException>();
            addStreamFieldPut.ShouldThrow<InvalidOperationException>();
            addFieldsPost.ShouldThrow<InvalidOperationException>();
            addFieldsDelete.ShouldThrow<InvalidOperationException>();
            addFieldsPatch.ShouldThrow<InvalidOperationException>();
            addFieldsPut.ShouldThrow<InvalidOperationException>();
        }
    }
}
