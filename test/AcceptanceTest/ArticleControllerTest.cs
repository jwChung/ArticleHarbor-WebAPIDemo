namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
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

        [Test]
        public async Task PostAsyncWithIncorrectAuthReturnsUnauthorizedCode(Article article)
        {
            using (var client = HttpClientFactory.Create())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Test]
        public async Task PostAsyncCorrectlyAddsArticleAndArticleWords(Article article)
        {
            article = article.WithSubject(
                "문장에서 단어만 추출해서 입력되는지 DB에서 확인필요.");

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795");
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
            }
        }

        [Test]
        public async Task PostAsyncCorrectlyModifiesArticleAndRenewsArticleWords(Article article)
        {
            article = article.WithId(2).WithSubject(
                "기존 단어들이 삭제되고, 새로운 단어들이 추가되었는지 DB에서 확인필요.");

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795");
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await GetMessageAsync(response));
            }
        }

        [Test]
        public async Task PostAsyncReturnsInternalSeverErrorWhenIncorrectUserModifiesArticle(Article article)
        {
            article = article.WithId(1); // owned by user 1

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795"); // user2
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        private static async Task<string> GetMessageAsync(HttpResponseMessage response)
        {
            return "Actual status code: " + response.StatusCode + Environment.NewLine
                + await GetStringMessage(response);
        }

        private static async Task<string> GetStringMessage(HttpResponseMessage response)
        {
            return response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;
        }
    }
}