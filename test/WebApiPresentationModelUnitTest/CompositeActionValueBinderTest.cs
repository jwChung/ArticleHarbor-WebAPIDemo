namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Web.Http.Controllers;
    using Xunit;

    public class CompositeActionValueBinderTest : IdiomaticTest<CompositeActionValueBinder>
    {
        [Test]
        public void SutIsActionValueBinder(CompositeActionValueBinder sut)
        {
            Assert.IsAssignableFrom<IActionValueBinder>(sut);
        }
    }
}