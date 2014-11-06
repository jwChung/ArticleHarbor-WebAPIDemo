namespace AcceptanceTest
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using DomainModel;
    using Newtonsoft.Json;
    using Xunit;

    public class ArticleControllerTest
    {
        [Test]
        public async Task GetAsyncReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/articles");
                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }

        [Test]
        public async Task GetAsyncReturnsJsonContent()
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

        [Test]
        public async Task GetAsyncDoesNotSupportXmlContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Accept.ParseAdd("application/xml");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json;q=0.8");
                var response = await client.GetAsync("api/articles");

                Assert.True(
                   response.IsSuccessStatusCode,
                   "Actual status code: " + response.StatusCode);
                Assert.Equal(
                    "application/json",
                    response.Content.Headers.ContentType.MediaType);
            }
        }

        [Test]
        public async Task PostAsyncCanAddCorrectArticle(Article article)
        {
            using (var client = HttpClientFactory.Create())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(article.WithId(1000)));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.True(
                   response.IsSuccessStatusCode,
                   "Actual status code: " + response.StatusCode);
            }
        }
    }
}