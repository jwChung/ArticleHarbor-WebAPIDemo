namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Newtonsoft.Json;
    using WebApiPresentationModel.Models;
    using Xunit;

    public class ArticlesControllerTest
    {
        [Test]
        public async Task GetAsyncReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/articles");
                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test]
        public async Task GetAsyncReturnsJsonContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/articles");

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
                var response = await client.GetAsync("api/articles");

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
                Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
            }
        }

        [Test]
        public IEnumerable<ITestCase> PostAsyncWithoutAuthReturnsUnauthorizedCode()
        {
            var auths = new[]
            {
                null, // user2
                new AuthenticationHeaderValue("apikey", "930eaf281412423592f35104836f2771"), // user3
            };

            return TestCases.WithArgs(auths).WithAuto<PostArticleViewModel>().Create(
                (auth, article) =>
                {
                    using (var client = HttpClientFactory.Create())
                    {
                        client.DefaultRequestHeaders.Authorization = auth;
                        var content = new StringContent(
                            JsonConvert.SerializeObject(article));
                        content.Headers.ContentType.MediaType = "application/json";

                        var response = client.PostAsync("api/articles", content).Result;

                        Assert.False(
                            response.IsSuccessStatusCode,
                            response.GetMessageAsync().Result);
                    }
                });
        }
        
        [Test]
        public async Task PostAsyncWithAuthAddsArticleAndKeywords(
            PostArticleViewModel article)
        {
            article.Subject = "문장에서 단어만 추출해서 입력되는지 DB에서 확인필요.";

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795");
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test]
        public async Task PutAsyncWithAuthModifiesArticleAndRenewsKeywords(
            PutArticleViewModel article)
        {
            article.Id = 2;
            article.Subject = "기존 단어들이 삭제되고, 새로운 단어들이 추가되었는지 DB에서 확인필요.";

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795");
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PutAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test]
        public async Task PutAsyncWithIncorrectUserReturnsInternalSeverError(
            PutArticleViewModel article)
        {
            article.Id = 1; // owned by user 1 (administrator)

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795"); // user2 (author)
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PutAsync("api/articles", content);

                Assert.True(
                   HttpStatusCode.InternalServerError == response.StatusCode,
                   await response.GetMessageAsync());
            }
        }

        [Test]
        public async Task PutAsyncWithAdminRoleModifiesAnyArticles(
            PutArticleViewModel article)
        {
            article.Id = 2; // owned by user 2 (author)

            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "692c7798206844b88ba9a660e3374eef"); // user1 (administrator)
                var content = new StringContent(
                    JsonConvert.SerializeObject(article));
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PutAsync("api/articles", content);

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test(Skip = "Run explicitly as others test can be affected from this test.")]
        public async Task DeleteAsyncWitAuthRemovesArticle()
        {
            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "232494f5670943dfac807226449fe795"); // user2 (author)

                var response = await client.DeleteAsync("api/articles/2");

                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        }

        [Test]
        public IEnumerable<ITestCase> DeleteAsyncWitIncorrectUserReturnsInternalSeverError()
        {
            var auths = new[]
            {
                new AuthenticationHeaderValue("apikey", "232494f5670943dfac807226449fe795"), // user2
                new AuthenticationHeaderValue("apikey", "930eaf281412423592f35104836f2771"), // user3
            };

            return TestCases.WithArgs(auths).Create(auth =>
            {
                using (var client = HttpClientFactory.Create())
                {
                    client.DefaultRequestHeaders.Authorization = auth;

                    var response = client.DeleteAsync("api/articles/1").Result;

                    Assert.True(
                        HttpStatusCode.InternalServerError == response.StatusCode,
                        response.GetMessageAsync().Result);
                }
            });
        }
    }
}