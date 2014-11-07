namespace WebApiPresentationModelUnitTest
{
    using System.Web.Http.Filters;
    using WebApiPresentationModel;
    using Xunit;

    public class SaveUnitOfWorkActionFilterTest : IdiomaticTest<SaveUnitOfWorkActionFilter>
    {
        [Test]
        public void SutIsActionFilterAttribute(
            SaveUnitOfWorkActionFilter sut)
        {
            Assert.IsAssignableFrom<ActionFilterAttribute>(sut);
        }
    }
}