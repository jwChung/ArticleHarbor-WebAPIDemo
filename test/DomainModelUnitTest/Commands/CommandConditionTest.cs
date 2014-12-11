namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Xunit;

    public class CommandConditionTest : IdiomaticTest<CommandCondition>
    {
        [Test]
        public void SutIsModelCondition(CommandCondition sut)
        {
            Assert.IsAssignableFrom<ICommandCondition>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> CanExecuteAsyncReturnsTrue()
        {
            yield return TestCase.WithAuto<CommandCondition, User>().Create(
                (sut, user) => Assert.True(sut.CanExecuteAsync(user).Result));

            yield return TestCase.WithAuto<CommandCondition, Article>().Create(
                (sut, article) => Assert.True(sut.CanExecuteAsync(article).Result));

            yield return TestCase.WithAuto<CommandCondition, Bookmark>().Create(
                (sut, bookmark) => Assert.True(sut.CanExecuteAsync(bookmark).Result));

            yield return TestCase.WithAuto<CommandCondition, Keyword>().Create(
                (sut, keyword) => Assert.True(sut.CanExecuteAsync(keyword).Result));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.CanExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.CanExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.CanExecuteAsync(default(Bookmark)));
            yield return this.Methods.Select(x => x.CanExecuteAsync(default(Keyword)));
        }
    }
}