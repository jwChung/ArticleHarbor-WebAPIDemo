namespace DomainModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using Xunit;

    public class ArticleServiceTest : IdiomaticTest<ArticleService>
    {
        [Test]
        public async Task AddAsyncCorrectlyAddsArticle(
            ArticleService sut,
            Article article)
        {
            await sut.AddAsync(article);
            sut.Repository.ToMock().Verify(x => x.Insert(article));
        }
        
        [Test]
        public void AddAsyncThrowsWhenRepositoryThrows(
            ArticleService sut,
            Article article,
            Exception exception)
        {
            sut.Repository.ToMock().Setup(x => x.Insert(article)).Throws(exception);
            try
            {
                sut.AddAsync(article).Wait();
            }
            catch (AggregateException e)
            {
                Assert.Equal(1, e.InnerExceptions.Count);
                Assert.Equal(exception, e.InnerExceptions[0]);
                return;
            }

            Assert.False(true);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.AddAsync(null));
        }
    }
}