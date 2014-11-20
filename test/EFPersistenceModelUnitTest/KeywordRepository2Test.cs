namespace ArticleHarbor.EFPersistenceModel
{
    using DomainModel.Models;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class KeywordRepository2Test : IdiomaticTest<KeywordRepository2>
    {
        [Test]
        public void SutIsRepository(KeywordRepository2 sut)
        {
            Assert.IsAssignableFrom<Repository<Keys<int, string>, Keyword, EFDataAccess.Keyword>>(sut);
        }

        [Test]
        public void ConvertToModelAsyncReturnsCorrectResult(
            KeywordRepository2 sut,
            EFDataAccess.Keyword keyword)
        {
            var actual = sut.ConvertToModelAsync(keyword).Result;
            keyword.AsSource().OfLikeness<Keyword>().ShouldEqual(actual);
        }
    }
}