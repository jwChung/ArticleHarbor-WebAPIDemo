namespace AcceptanceTest
{
    using System.Threading.Tasks;
    using Xunit;

    public class ArticleControllerTest
    {
        [Test]
        public void GetReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("api/articles").Result;
                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }
    }
}