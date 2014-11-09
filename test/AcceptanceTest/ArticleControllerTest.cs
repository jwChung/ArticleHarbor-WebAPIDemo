namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
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
                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
            }
        }

        [Test]
        public async Task GetAsyncReturnsJsonContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/articles");

                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
                Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
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

                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
                Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
            }
        }

        [Test(Skip = "Need Authentication")]
        public async Task PostAsyncCorrectlyAddsArticleAndArticleWords(Article article)
        {
            article = article.WithUserId("user2")
                .WithSubject("문장에서 단어만 추출해서 입력되는지 DB에서 확인필요.");

            using (var client = HttpClientFactory.Create())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(article.WithId(1000)));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
            }
        }

        [Test(Skip = "Need Authentication")]
        public async Task PostAsyncCorrectlyModifiesArticleAndRenewsArticleWords(Article article)
        {
            article = article.WithUserId("user2")
                .WithSubject("기존 단어들이 삭제되고, 새로운 단어들이 추가되었는지 DB에서 확인필요.");

            using (var client = HttpClientFactory.Create())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(article.WithId(1)));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
            }
        }

        private static async Task<string> GetMessageAsync(HttpResponseMessage response)
        {
            return "Actual status code: " + response.StatusCode
                + Environment.NewLine + await response.Content.ReadAsStringAsync();
        }
    }
}