namespace AcceptanceTest
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Xunit;

    public class ArticleControllerTest
    {
        [Test]
        public async Task GetReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/articles");
                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }

        [Fact]
        public async Task GetReturnsJsonContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/articles");

                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
                Assert.Equal(
                    "application/json",
                    response.Content.Headers.ContentType.MediaType);
                var json = await response.Content.ReadAsStringAsync()
                    .ContinueWith(t => JsonConvert.DeserializeObject(t.Result));
                Assert.NotNull(json);
            }
        }
        
        [Fact]
        public async Task GetDoesNotSupportXmlContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Accept.ParseAdd("application/xml");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json;q=0.8");
                var response = await client.GetAsync("api/articles");

                Assert.Equal(
                    "application/json",
                    response.Content.Headers.ContentType.MediaType);
                var json = await response.Content.ReadAsStringAsync()
                    .ContinueWith(t => JsonConvert.DeserializeObject(t.Result));
                Assert.NotNull(json);
            }
        }
    }
}