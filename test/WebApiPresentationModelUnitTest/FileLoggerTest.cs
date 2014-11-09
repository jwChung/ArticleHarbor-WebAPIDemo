namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Xunit;

    public class FileLoggerTest : IdiomaticTest<FileLogger>
    {
        [Test]
        public void SutIsLogger(FileLogger sut)
        {
            Assert.IsAssignableFrom<ILogger>(sut);
        }

        [Test]
        public async Task LogAsyncCorrectlyLogs(LogContext context, IFixture fixture)
        {
            try
            {
                fixture.Inject<string>(Environment.CurrentDirectory);
                var sut = fixture.Create<FileLogger>();

                await sut.LogAsync(context);

                var files = Directory.GetFiles(Environment.CurrentDirectory, "*.log");
                Assert.Equal(File.ReadAllText(files.Single()), context.ToString());
            }
            finally
            {
                foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "*.log"))
                    File.Delete(file);
            }
        }
    }
}