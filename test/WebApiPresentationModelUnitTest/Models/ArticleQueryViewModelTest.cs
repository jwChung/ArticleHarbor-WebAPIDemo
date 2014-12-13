namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using DomainModel.Queries;
    using Xunit;

    public class ArticleQueryViewModelTest : IdiomaticTest<ArticleQueryViewModel>
    {
        [Test]
        public void SutIsSqlQueryable(ArticleQueryViewModel sut)
        {
            Assert.IsAssignableFrom<ISqlQueryable>(sut);
        }
    }
}