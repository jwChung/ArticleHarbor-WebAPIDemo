namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Models;
    using Xunit;

    public class ModelCommandTest : IdiomaticTest<ModelCommand<object>>
    {
        [Test]
        public void SutIsModelCommand(ModelCommand<object> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<object>>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteAsyncReturnsEmpty(ModelCommand<object> sut)
        {
            yield return TestCase.WithAuto<User>().Create(
                x =>
                {
                    var actual = sut.ExecuteAsync(x).Result;
                    Assert.Empty(actual);
                });
            yield return TestCase.WithAuto<Article>().Create(
                x =>
                {
                    var actual = sut.ExecuteAsync(x).Result;
                    Assert.Empty(actual);
                });
            yield return TestCase.WithAuto<Bookmark>().Create(
                x =>
                {
                    var actual = sut.ExecuteAsync(x).Result;
                    Assert.Empty(actual);
                });
            yield return TestCase.WithAuto<Keyword>().Create(
               x =>
               {
                   var actual = sut.ExecuteAsync(x).Result;
                   Assert.Empty(actual);
               });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
        }
    }
}