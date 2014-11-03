namespace DomainModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using DomainModel;
    using Moq;
    using Xunit;

    public class ArticleServiceTest : IdiomaticTest<ArticleService>
    {
        [Test]
        public async Task AddAsyncCorrectlyAddsArticle(
            ArticleService sut,
            Article article)
        {
            sut.Repository.ToMock().DefaultValue = DefaultValue.Empty;
            await sut.AddAsync(article);
            sut.Repository.ToMock().Verify(x => x.Insert(article));
        }
        
        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.AddAsync(null));
        }
    }
}