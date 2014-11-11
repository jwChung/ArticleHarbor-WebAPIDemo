namespace ArticleHarbor.AcceptanceTest
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
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
    }
}