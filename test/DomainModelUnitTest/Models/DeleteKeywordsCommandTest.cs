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
        public void ValueIsEmpty(DeleteKeywordsCommand sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncArticleDeletesKeywordsRelatedWithArticle(
            DeleteKeywordsCommand sut,
            Article article)
        {
            var likeness = new EqualPredicate("articleId", article.Id).AsSource()
                .OfLikeness<EqualPredicate>()
                .Without(x => x.SqlText)
                .Without(x => x.Parameters);
            bool verifies = false;
            var task = Task.Run(() =>
            {
                Thread.Sleep(300);
                verifies = true;
            });
            sut.Repositories.Keywords.Of(
                x => x.ExecuteDeleteCommandAsync(It.Is<IPredicate>(p => likeness.Equals(p))) == task);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Equal(sut, actual);
            Assert.True(verifies);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}