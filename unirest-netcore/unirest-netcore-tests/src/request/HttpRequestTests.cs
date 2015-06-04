namespace unirest_netcore_tests.request
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using unirest_netcore.request;

    using Xunit;

    public class HttpRequestTests
    {
        [Fact]
        public static void HttpRequest_Should_Construct()
        {
            Action Get = () => new HttpRequest(HttpMethod.Get, "http://localhost");
            Action Post = () => new HttpRequest(HttpMethod.Post, "http://localhost");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "http://localhost");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            Action Put = () => new HttpRequest(HttpMethod.Put, "http://localhost");

            Get();
            Post();
            Delete();
            Patch();
            Put();
        }

        [Fact]
        public static void HttpRequest_Should_Not_Construct_With_Invalid_URL()
        {
            Action Get = () => new HttpRequest(HttpMethod.Get, "http:///invalid");
            Action Post = () => new HttpRequest(HttpMethod.Post, "http:///invalid");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "http:///invalid");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "http:///invalid");
            Action Put = () => new HttpRequest(HttpMethod.Put, "http:///invalid");

            Assert.Throws<ArgumentException>(Get);
            Assert.Throws<ArgumentException>(Post);
            Assert.Throws<ArgumentException>(Delete);
            Assert.Throws<ArgumentException>(Patch);
            Assert.Throws<ArgumentException>(Put);
        }

        [Fact]
        public static void HttpRequest_Should_Not_Construct_With_None_HTTP_URL()
        {
            Action Get = () => new HttpRequest(HttpMethod.Get, "ftp://localhost");
            Action Post = () => new HttpRequest(HttpMethod.Post, "mailto:localhost");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "news://localhost");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "about:blank");
            Action Put = () => new HttpRequest(HttpMethod.Put, "about:settings");

            Assert.Throws<ArgumentException>(Get);
            Assert.Throws<ArgumentException>(Post);
            Assert.Throws<ArgumentException>(Delete);
            Assert.Throws<ArgumentException>(Patch);
            Assert.Throws<ArgumentException>(Put);
        }

        [Fact]
        public static void HttpRequest_Should_Construct_With_Correct_Verb()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Assert.Equal(HttpMethod.Get, Get.HttpMethod);
            Assert.Equal(HttpMethod.Post, Post.HttpMethod);
            Assert.Equal(HttpMethod.Delete, Delete.HttpMethod);
            Assert.Equal(new HttpMethod("PATCH"), Patch.HttpMethod);
            Assert.Equal(HttpMethod.Put, Put.HttpMethod);
        }

        [Fact]
        public static void HttpRequest_Should_Construct_With_Correct_URL()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Assert.Equal("http://localhost", Get.URL.OriginalString);
            Assert.Equal("http://localhost", Post.URL.OriginalString);
            Assert.Equal("http://localhost", Delete.URL.OriginalString);
            Assert.Equal("http://localhost", Patch.URL.OriginalString);
            Assert.Equal("http://localhost", Put.URL.OriginalString);
        }

        [Fact]
        public static void HttpRequest_Should_Construct_With_Headers()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Assert.NotNull(Get.Headers);
            Assert.NotNull(Post.Headers);
            Assert.NotNull(Delete.Headers);
            Assert.NotNull(Patch.Headers);
            Assert.NotNull(Put.Headers);
        }

        [Fact]
        public static void HttpRequest_Should_Add_Headers()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.header("User-Agent", "unirest-netcore/1.0");
            Post.header("User-Agent", "unirest-netcore/1.0");
            Delete.header("User-Agent", "unirest-netcore/1.0");
            Patch.header("User-Agent", "unirest-netcore/1.0");
            Put.header("User-Agent", "unirest-netcore/1.0");
        }

        [Fact]
        public static void HttpRequest_Should_Add_Headers_Dictionary()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Get.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Post.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Delete.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Patch.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Put.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });

            Assert.Contains("unirest-netcore/1.0", Get.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Post.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Delete.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Patch.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Put.Headers.Values);
        }

        [Fact]
        public static void HttpRequest_Should_Return_String()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com");
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com");
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com");

            Assert.NotEmpty(Get.asString().Body);
            Assert.NotEmpty(Post.asString().Body);
            Assert.NotEmpty(Delete.asString().Body);
            Assert.NotEmpty(Patch.asString().Body);
            Assert.NotEmpty(Put.asString().Body);
        }

        [Fact]
        public static void HttpRequest_Should_Return_Stream()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com");
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com");
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com");

            Assert.NotNull(Get.asBinary().Body);
            Assert.NotNull(Post.asBinary().Body);
            Assert.NotNull(Delete.asBinary().Body);
            Assert.NotNull(Patch.asBinary().Body);
            Assert.NotNull(Put.asBinary().Body);
        }

        [Fact]
        public static void HttpRequest_Should_Return_Parsed_JSON()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com");
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com");
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com");

            Assert.NotEmpty(Get.asJson<string>().Body);
            Assert.NotEmpty(Post.asJson<string>().Body);
            Assert.NotEmpty(Delete.asJson<string>().Body);
            Assert.NotEmpty(Patch.asJson<string>().Body);
            Assert.NotEmpty(Put.asJson<string>().Body);
        }

        [Fact]
        public static void HttpRequest_Should_Return_String_Async()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com").asStringAsync();
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com").asStringAsync();
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com").asStringAsync();
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com").asStringAsync();
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com").asStringAsync();

            Task.WaitAll(Get, Post, Delete, Patch, Put);

            Assert.NotEmpty(Get.Result.Body);
            Assert.NotEmpty(Post.Result.Body);
            Assert.NotEmpty(Delete.Result.Body);
            Assert.NotEmpty(Patch.Result.Body);
            Assert.NotEmpty(Put.Result.Body);
        }

        [Fact]
        public static void HttpRequest_Should_Return_Stream_Async()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com").asBinaryAsync();
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com").asBinaryAsync();
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com").asBinaryAsync();
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com").asBinaryAsync();
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com").asBinaryAsync();

            Task.WaitAll(Get, Post, Delete, Patch, Put);

            Assert.NotNull(Get.Result.Body);
            Assert.NotNull(Post.Result.Body);
            Assert.NotNull(Delete.Result.Body);
            Assert.NotNull(Patch.Result.Body);
            Assert.NotNull(Put.Result.Body);
        }

        [Fact]
        public static void HttpRequest_Should_Return_Parsed_JSON_Async()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://www.google.com").asJsonAsync<String>();
            var Post = new HttpRequest(HttpMethod.Post, "http://www.google.com").asJsonAsync<String>();
            var Delete = new HttpRequest(HttpMethod.Delete, "http://www.google.com").asJsonAsync<String>();
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://www.google.com").asJsonAsync<String>();
            var Put = new HttpRequest(HttpMethod.Put, "http://www.google.com").asJsonAsync<String>();

            Task.WaitAll(Get, Post, Delete, Patch, Put);

            Assert.NotEmpty(Get.Result.Body);
            Assert.NotEmpty(Post.Result.Body);
            Assert.NotEmpty(Delete.Result.Body);
            Assert.NotEmpty(Patch.Result.Body);
            Assert.NotEmpty(Put.Result.Body);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Construct()
        {
            Action Post = () => new HttpRequest(HttpMethod.Post, "http://localhost");
            Action Delete = () => new HttpRequest(HttpMethod.Delete, "http://localhost");
            Action Patch = () => new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            Action Put = () => new HttpRequest(HttpMethod.Put, "http://localhost");

            Post();
            Delete();
            Patch();
            Put();
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Not_Construct_With_Invalid_URL()
        {
            Action Post = () => new HttpRequest(HttpMethod.Post, "http:///invalid");
            Action delete = () => new HttpRequest(HttpMethod.Delete, "http:///invalid");
            Action patch = () => new HttpRequest(new HttpMethod("PATCH"), "http:///invalid");
            Action put = () => new HttpRequest(HttpMethod.Put, "http:///invalid");

            Assert.Throws<ArgumentException>(Post);
            Assert.Throws<ArgumentException>(delete);
            Assert.Throws<ArgumentException>(patch);
            Assert.Throws<ArgumentException>(put);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Not_Construct_With_None_HTTP_URL()
        {
            Action Post = () => new HttpRequest(HttpMethod.Post, "mailto:localhost");
            Action delete = () => new HttpRequest(HttpMethod.Delete, "news://localhost");
            Action patch = () => new HttpRequest(new HttpMethod("PATCH"), "about:blank");
            Action put = () => new HttpRequest(HttpMethod.Put, "about:settings");

            Assert.Throws<ArgumentException>(Post);
            Assert.Throws<ArgumentException>(delete);
            Assert.Throws<ArgumentException>(patch);
            Assert.Throws<ArgumentException>(put);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Construct_With_Correct_Verb()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Assert.Equal(HttpMethod.Post, Post.HttpMethod);
            Assert.Equal(HttpMethod.Delete, Delete.HttpMethod);
            Assert.Equal(new HttpMethod("PATCH"), Patch.HttpMethod);
            Assert.Equal(HttpMethod.Put, Put.HttpMethod);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Construct_With_Correct_URL()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Assert.Equal("http://localhost", Post.URL.OriginalString);
            Assert.Equal("http://localhost", Delete.URL.OriginalString);
            Assert.Equal("http://localhost", Patch.URL.OriginalString);
            Assert.Equal("http://localhost", Put.URL.OriginalString);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Construct_With_Headers()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Assert.NotNull(Post.URL.OriginalString);
            Assert.NotNull(Delete.URL.OriginalString);
            Assert.NotNull(Patch.URL.OriginalString);
            Assert.NotNull(Put.URL.OriginalString);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Add_Headers()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.header("User-Agent", "unirest-netcore/1.0");
            Delete.header("User-Agent", "unirest-netcore/1.0");
            Patch.header("User-Agent", "unirest-netcore/1.0");
            Put.header("User-Agent", "unirest-netcore/1.0");

            Assert.Contains("unirest-netcore/1.0", Post.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Delete.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Patch.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Put.Headers.Values);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Add_Headers_Dictionary()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Delete.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Patch.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });
            Put.headers(new Dictionary<string, string> { { "User-Agent", "unirest-netcore/1.0" } });

            Assert.Contains("unirest-netcore/1.0", Post.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Delete.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Patch.Headers.Values);
            Assert.Contains("unirest-netcore/1.0", Put.Headers.Values);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Encode_Fields()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.field("key", "value");
            Delete.field("key", "value");
            Patch.field("key", "value");
            Put.field("key", "value");

            Assert.NotEmpty(Post.Body);
            Assert.NotEmpty(Delete.Body);
            Assert.NotEmpty(Patch.Body);
            Assert.NotEmpty(Put.Body);
        }

        [Fact]
        public static void HttpRequest_With_Body_Should_Encode_File()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var stream = new MemoryStream();

            Post.field(stream);
            Delete.field(stream);
            Patch.field(stream);
            Put.field(stream);

            Assert.NotEmpty(Post.Body);
            Assert.NotEmpty(Delete.Body);
            Assert.NotEmpty(Patch.Body);
            Assert.NotEmpty(Put.Body);
        }

        [Fact]
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

            Post.fields(dict);
            Delete.fields(dict);
            Patch.fields(dict);
            Put.fields(dict);

            Assert.NotEmpty(Post.Body);
            Assert.NotEmpty(Delete.Body);
            Assert.NotEmpty(Patch.Body);
            Assert.NotEmpty(Put.Body);
        }

        [Fact]
        public static void HttpRequestWithBody_Should_Add_String_Body()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.body("test");
            Delete.body("test");
            Patch.body("test");
            Put.body("test");

            Assert.NotEmpty(Post.Body);
            Assert.NotEmpty(Delete.Body);
            Assert.NotEmpty(Patch.Body);
            Assert.NotEmpty(Put.Body);
        }

        [Fact]
        public static void HttpRequestWithBody_Should_Add_JSON_Body()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            Post.body(new List<int> { 1, 2, 3 });
            Delete.body(new List<int> { 1, 2, 3 });
            Patch.body(new List<int> { 1, 2, 3 });
            Put.body(new List<int> { 1, 2, 3 });

            Assert.NotEmpty(Post.Body);
            Assert.NotEmpty(Delete.Body);
            Assert.NotEmpty(Patch.Body);
            Assert.NotEmpty(Put.Body);
        }

        [Fact]
        public static void Http_Request_Shouldnt_Add_Fields_To_Get()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            Action addStringField = () => Get.field("name", "value");
            Action addKeyField = () => Get.field(new MemoryStream());
            Action addStringFields = () => Get.fields(new Dictionary<string, object> {{"name", "value"}});
            Action addKeyFields = () => Get.fields(new Dictionary<string, object> {{"key", new MemoryStream()}});

            Assert.Throws<InvalidOperationException>(addStringField);
            Assert.Throws<InvalidOperationException>(addKeyField);
            Assert.Throws<InvalidOperationException>(addStringFields);
            Assert.Throws<InvalidOperationException>(addKeyFields);
        }

        [Fact]
        public static void Http_Request_Shouldnt_Add_Body_To_Get()
        {
            var Get = new HttpRequest(HttpMethod.Get, "http://localhost");
            Action addStringBody = () => Get.body("string");
            Action addJSONBody = () => Get.body(new List<int> {1,2,3});

            Assert.Throws<InvalidOperationException>(addStringBody);
            Assert.Throws<InvalidOperationException>(addJSONBody);
        }

        [Fact]
        public static void HttpRequestWithBody_Should_Not_Allow_Body_For_Request_With_Field()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var stream = new MemoryStream();

            Post.field(stream);
            Delete.field(stream);
            Patch.field(stream);
            Put.field(stream);

            Action addBodyPost = () => Post.body("test");
            Action addBodyDelete = () => Delete.body("test");
            Action addBodyPatch = () => Patch.body("test");
            Action addBodyPut = () => Put.body("test");
            Action addObjectBodyPost = () => Post.body(1);
            Action addObjectBodyDelete = () => Delete.body(1);
            Action addObjectBodyPatch = () => Patch.body(1);
            Action addObjectBodyPut = () => Put.body(1);

            Assert.Throws<InvalidOperationException>(addBodyPost);
            Assert.Throws<InvalidOperationException>(addBodyDelete);
            Assert.Throws<InvalidOperationException>(addBodyPatch);
            Assert.Throws<InvalidOperationException>(addBodyPut);
            Assert.Throws<InvalidOperationException>(addObjectBodyPost);
            Assert.Throws<InvalidOperationException>(addObjectBodyDelete);
            Assert.Throws<InvalidOperationException>(addObjectBodyPatch);
            Assert.Throws<InvalidOperationException>(addObjectBodyPut);
        }

        [Fact]
        public static void HttpRequestWithBody_Should_Not_Allow_Fields_For_Request_With_Body()
        {
            var Post = new HttpRequest(HttpMethod.Post, "http://localhost");
            var Delete = new HttpRequest(HttpMethod.Delete, "http://localhost");
            var Patch = new HttpRequest(new HttpMethod("PATCH"), "http://localhost");
            var Put = new HttpRequest(HttpMethod.Put, "http://localhost");

            var stream = new MemoryStream();

            Post.body("test");
            Delete.body("test");
            Patch.body("test");
            Put.body("lalala");

            Action addFieldPost = () => Post.field("key", "value");
            Action addFieldDelete = () => Delete.field("key", "value");
            Action addFieldPatch = () => Patch.field("key", "value");
            Action addFieldPut = () => Put.field("key", "value");
            Action addStreamFieldPost = () => Post.field(stream);
            Action addStreamFieldDelete = () => Delete.field(stream);
            Action addStreamFieldPatch = () => Patch.field(stream);
            Action addStreamFieldPut = () => Put.field(stream);
            Action addFieldsPost = () => Post.fields(new Dictionary<string, object> {{"test", "test"}});
            Action addFieldsDelete = () => Delete.fields(new Dictionary<string, object> {{"test", "test"}});
            Action addFieldsPatch = () => Patch.fields(new Dictionary<string, object> {{"test", "test"}});
            Action addFieldsPut = () => Put.fields(new Dictionary<string, object> {{"test", "test"}});

            Assert.Throws<InvalidOperationException>(addFieldPost);
            Assert.Throws<InvalidOperationException>(addFieldDelete);
            Assert.Throws<InvalidOperationException>(addFieldPatch);
            Assert.Throws<InvalidOperationException>(addFieldPut);
            Assert.Throws<InvalidOperationException>(addStreamFieldPost);
            Assert.Throws<InvalidOperationException>(addStreamFieldDelete);
            Assert.Throws<InvalidOperationException>(addStreamFieldPatch);
            Assert.Throws<InvalidOperationException>(addStreamFieldPut);
            Assert.Throws<InvalidOperationException>(addFieldsPost);
            Assert.Throws<InvalidOperationException>(addFieldsDelete);
            Assert.Throws<InvalidOperationException>(addFieldsPatch);
            Assert.Throws<InvalidOperationException>(addFieldsPut);
        }
    }
}
