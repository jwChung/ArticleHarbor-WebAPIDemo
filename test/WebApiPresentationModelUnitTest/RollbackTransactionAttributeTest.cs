namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Web.Http.Filters;
    using Xunit;

    public class RollbackTransactionAttributeTest : IdiomaticTest<RollbackTransactionAttribute>
    {
        [Test]
        public void SutIsExceptionFilterAttribute(RollbackTransactionAttribute sut)
        {
            Assert.IsAssignableFrom<ExceptionFilterAttribute>(sut);
        }
    }
}