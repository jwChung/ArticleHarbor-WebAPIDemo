namespace WebApiPresentationModelUnitTest
{
    using System.Net.Http.Formatting;
    using WebApiPresentationModel;
    using Xunit;

    public class JsonConstructorMediaTypeFormatterTest
    {
        [Test]
        public void SubIsMediaTypeFormatter(
            JsonConstructorMediaTypeFormatter sut)
        {
            Assert.IsAssignableFrom<MediaTypeFormatter>(sut);
        }
    }
}