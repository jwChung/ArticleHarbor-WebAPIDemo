namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using EFPersistenceModel;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class ArticleRepositoryTest : IdiomaticTest<ArticleRepository>
    {
        [Test]
        public void SutIsArticleRepository(ArticleRepository sut)
        {
            Assert.IsAssignableFrom<IArticleRepository>(sut);
        }

        [Test]
        public async Task InsertCorrectlyInsertsArticle(
            ArticleRepository sut,
            Article article)
        {
            var newArticle = sut.Insert(article);
            var expected = await sut.SelectAsync(newArticle.Id);
            newArticle.AsSource().OfLikeness<Article>().ShouldEqual(expected);
        }

        [Test]
        public async Task InsertAsyncCorrectlyInsertsArticle(
            ArticleRepository sut,
            Article article)
        {
            var newArticle = await sut.InsertAsync(article);
            var expected = await sut.SelectAsync(newArticle.Id);
            newArticle.AsSource().OfLikeness<Article>().ShouldEqual(expected);
        }

        [Test]
        public async Task SelectAsyncReturnsCorrectResult(
            ArticleRepository sut,
            Article[] articles)
        {
            var actual = await sut.SelectAsync();
            Assert.Equal(50, actual.Count());
        }

        [Test]
        public async Task InsertAsyncWithNullArticleThrows(ArticleRepository sut)
        {
            try
            {
                await sut.InsertAsync(null);
                throw new Exception();
            }
            catch (ArgumentNullException)
            {
            }
            catch
            {
                throw;
            }
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.InsertAsync(null));
        }
    }
}