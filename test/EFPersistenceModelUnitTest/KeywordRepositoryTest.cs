namespace ArticleHarbor.EFPersistenceModel
{
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Queries;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class KeywordRepositoryTest : IdiomaticTest<KeywordRepository>
    {
        [Test]
        public void SutIsRepository(KeywordRepository sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<int, string>, Keyword, EFDataAccess.Keyword>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectResult(
            KeywordRepository sut,
            EFDataAccess.Keyword keyword)
        {
            var actual = sut.ConvertToModelAsync(keyword).Result;
            keyword.AsSource().OfLikeness<Keyword>().ShouldEqual(actual);
        }

        [Test]
        public void ConvertToPersistenceAsyncReturnsCorrectResult(
            KeywordRepository sut,
            Keyword keyword)
        {
            var actual = sut.ConvertToPersistenceAsync(keyword).Result;
            actual.AsSource().OfLikeness<Keyword>().ShouldEqual(keyword);
        }
    }
}