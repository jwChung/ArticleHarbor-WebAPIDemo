namespace ArticleHarbor.AcceptanceTest
{
    using System.Threading.Tasks;
    using Xunit;

    public class BookmarksControllerTest
    {
        [Test]
        public async Task GetAsyncReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/bookmarks");
                Assert.True(response.IsSuccessStatusCode, await response.GetMessageAsync());
            }
        } 
    }
}