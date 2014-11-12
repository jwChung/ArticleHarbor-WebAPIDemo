namespace ArticleHarbor.AcceptanceTest
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Xunit;

    public class BookmarksControllerTest
    {
        [Test]
        public async Task GetAsyncWithoutAuthReturnsUnauthorizedCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/bookmarks");
                Assert.True(
                    HttpStatusCode.Unauthorized == response.StatusCode,
                    response.GetMessageAsync().Result);
            }
        }

        [Test]
        public async Task GetAsyncWithAuthReturnsCorrectResult()
        {
            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "692c7798206844b88ba9a660e3374eef"); // user1

                var response = await client.GetAsync("api/bookmarks");

                Assert.True(
                    response.IsSuccessStatusCode,
                    response.GetMessageAsync().Result);
            }
        }

        [Test]
        public async Task PostAsyncWithAuthAddsBookmark()
        {
            using (var client = HttpClientFactory.Create())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "apikey", "930eaf281412423592f35104836f2771"); // user3

                var response = await client.PostAsync("api/bookmarks/1", new StringContent(string.Empty));

                Assert.True(
                    response.IsSuccessStatusCode,
                    response.GetMessageAsync().Result);
            }
        }
    }
}