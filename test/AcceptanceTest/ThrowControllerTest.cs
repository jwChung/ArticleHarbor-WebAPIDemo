#if !CI
namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class ThrowControllerTest
    {
        [Test]
        public async Task ThrowArgumentExceptionDoesNotLog()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync("api/throw/ArgumentException");
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                Assert.False(Directory.GetFiles(Environment.CurrentDirectory, "*.log").Any());
            }
        }

        [Test]
        public async Task ThrowNonArgumentExceptionCorrectlyLogs()
        {
            try
            {
                using (var client = HttpClientFactory.Create())
                {
                    var response = await client.GetAsync("api/throw/InvalidOperationException");
                    Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                    Assert.DoesNotThrow(
                        () => Directory.GetFiles(Environment.CurrentDirectory, "*.log").Single());
                }
            }
            finally
            {
                foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "*.log"))
                    File.Delete(file);
            }
        }
    }
}
#endif