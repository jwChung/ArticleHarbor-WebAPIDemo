namespace WebApiPresentationModelUnitTest
{
    using WebApiPresentationModel;
    using Xunit;

    public class FileLoggerTest : IdiomaticTest<FileLogger>
    {
        [Test]
        public void SutIsLogger(FileLogger sut)
        {
            Assert.IsAssignableFrom<ILogger>(sut);
        }
    }
}