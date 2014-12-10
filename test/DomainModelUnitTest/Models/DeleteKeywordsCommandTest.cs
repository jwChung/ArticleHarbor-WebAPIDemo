namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class DeleteKeywordsCommandTest : IdiomaticTest<DeleteKeywordsCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteKeywordsCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncArticleDeletesKeywordsRelatedWithArticle(
            DeleteKeywordsCommand sut,
            Article article)
        {
            var likeness = new EqualPredicate("ArticleId", article.Id).AsSource()
                .OfLikeness<EqualPredicate>()
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);
            
            var actual = sut.ExecuteAsync(article).Result;

            ////Assert.Equal(sut, actual);
            sut.Repositories.Keywords.ToMock().Verify(
                x => x.ExecuteDeleteCommandAsync(It.Is<IPredicate>(p => likeness.Equals(p))));
        }
    }
}