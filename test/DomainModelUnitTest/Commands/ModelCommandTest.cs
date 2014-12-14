namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
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

        [Test]
        public IEnumerable<ITestCase> ExecuteAsyncWithModelsReturnsCorrectResult(TssModelCommand sut)
        {
            yield return TestCase.WithAuto<IEnumerable<User>>().Create(
                models =>
                {
                    var actual = sut.ExecuteAsync(models).Result;
                    Assert.Equal(models, actual);
                });
            yield return TestCase.WithAuto<IEnumerable<Article>>().Create(
                models =>
                {
                    var actual = sut.ExecuteAsync(models).Result;
                    Assert.Equal(models, actual);
                });
            yield return TestCase.WithAuto<IEnumerable<Bookmark>>().Create(
                models =>
                {
                    var actual = sut.ExecuteAsync(models).Result;
                    Assert.Equal(models, actual);
                });
            yield return TestCase.WithAuto<IEnumerable<Keyword>>().Create(
                models =>
                {
                    var actual = sut.ExecuteAsync(models).Result;
                    Assert.Equal(models, actual);
                });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
        }

        public class TssModelCommand : ModelCommand<object>
        {
            public override Task<IEnumerable<object>> ExecuteAsync(User user)
            {
                return Task.FromResult<IEnumerable<object>>(new object[] { user });
            }

            public override Task<IEnumerable<object>> ExecuteAsync(Article article)
            {
                return Task.FromResult<IEnumerable<object>>(new object[] { article });
            }

            public override Task<IEnumerable<object>> ExecuteAsync(Keyword keyword)
            {
                return Task.FromResult<IEnumerable<object>>(new object[] { keyword });
            }

            public override Task<IEnumerable<object>> ExecuteAsync(Bookmark bookmark)
            {
                return Task.FromResult<IEnumerable<object>>(new object[] { bookmark });
            }
        }
    }
}