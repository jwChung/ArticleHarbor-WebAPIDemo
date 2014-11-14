namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ArticleElementTest : IdiomaticTest<ArticleElement>
    {
        [Test]
        public void SutIsModelElement(ArticleElement sut)
        {
            Assert.IsAssignableFrom<IModelElement<Article>>(sut);
        }
    }
}