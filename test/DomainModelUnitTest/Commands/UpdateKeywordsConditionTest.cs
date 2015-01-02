namespace ArticleHarbor.DomainModel.Commands
{
    using System.Threading.Tasks;
    using Models;
    using Queries;
    using Xunit;

    public class UpdateKeywordsConditionTest : IdiomaticTest<UpdateKeywordsCondition>
    {
        [Test]
        public void SutIsTrueCondition(UpdateKeywordsCondition sut)
        {
            Assert.IsAssignableFrom<TrueCondition>(sut);
        }

        [Test]
        public void CanExecuteAsyncArticleWithSameSubjectReturnsFalse(
            UpdateKeywordsCondition sut,
            Article article,
            Article articleOfRepo)
        {
            articleOfRepo = articleOfRepo.WithSubject(article.Subject);
            sut.Repositories.Articles.Of(x => x.FindAsync((Keys<int>)article.GetKeys())
                == Task.FromResult(articleOfRepo));

            var actual = sut.CanExecuteAsync(article).Result;

            Assert.False(actual);
        }

        [Test]
        public void CanExecuteAsyncArticleWithDifferentSubjectReturnsTrue(
            UpdateKeywordsCondition sut,
            Article article,
            Article articleOfRepo)
        {
            sut.Repositories.Articles.Of(x => x.FindAsync((Keys<int>)article.GetKeys())
                == Task.FromResult(articleOfRepo));

            var actual = sut.CanExecuteAsync(article).Result;

            Assert.True(actual);
        }
    }
}