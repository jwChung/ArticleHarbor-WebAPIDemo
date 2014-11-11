namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using DomainModel;
    using Newtonsoft.Json;
    using Xunit;

    public class ArticlesControllerTest
    {
        [Test]
        public async Task GetAsyncReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/newarticles");
                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test]
        public async Task GetAsyncReturnsJsonContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/newarticles");

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
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
                var response = await client.GetAsync("api/newarticles");

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
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

                var response = await client.PostAsync("api/newarticles", content);

                Assert.True(
                    HttpStatusCode.Unauthorized == response.StatusCode,
                    await response.GetMessageAsync());
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

                var response = await client.PostAsync("api/newarticles", content);

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
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

                var response = await client.PutAsync("api/newarticles", content);

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test]
        public async Task PutAsyncReturnsInternalSeverErrorWhenIncorrectUserModifiesArticle(Article article)
        {
            article = article.WithId(1); // owned by user 1 (administrator)

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795"); // user2 (author)
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PutAsync("api/newarticles", content);

                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [Test]
        public async Task PutAsyncWithAdminRoleModifiesAnyArticles(Article article)
        {
            article = article.WithId(2); // owned by user 2 (author)

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "692c7798206844b88ba9a660e3374eef"); // user1 (administrator)
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PutAsync("api/newarticles", content);

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }
    }
}