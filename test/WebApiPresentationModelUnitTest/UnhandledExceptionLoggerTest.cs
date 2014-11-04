namespace WebApiPresentationModelUnitTest
{
    using System.Web.Http.ExceptionHandling;
    using WebApiPresentationModel;
    using Xunit;

    public class UnhandledExceptionLoggerTest
    {
        [Test]
        public void SutIsExceptionLogger(UnhandledExceptionLogger sut)
        {
            Assert.IsAssignableFrom<ExceptionLogger>(sut);
        }
    }
}