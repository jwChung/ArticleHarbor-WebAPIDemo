namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Xunit;

    public class ModelCommandTest : IdiomaticTest<ModelCommand<object>>
    {
        [Test]
        public void SutIsModelCommand(ModelCommand<object> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<object>>(sut);
        }

        [Test]
        public void ValueReturnsDefaultValueOfGivenType(
            ModelCommand<object> sut1,
            ModelCommand<int> sut2)
        {
            Assert.Null(sut1.Value);
            Assert.Equal(0, sut2.Value);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteModelReturnsSutItself(ModelCommand<object> sut)
        {
            yield return TestCase.WithAuto<User>().Create(
                x =>
                {
                    var actual = sut.Execute(x);
                    Assert.Equal(sut, actual);
                });
            yield return TestCase.WithAuto<Article>().Create(
                x =>
                {
                    var actual = sut.Execute(x);
                    Assert.Equal(sut, actual);
                });
            yield return TestCase.WithAuto<Bookmark>().Create(
                x =>
                {
                    var actual = sut.Execute(x);
                    Assert.Equal(sut, actual);
                });
            yield return TestCase.WithAuto<Keyword>().Create(
               x =>
               {
                   var actual = sut.Execute(x);
                   Assert.Equal(sut, actual);
               });
        }

        [Test]
        public void ExecuteUsersRespectivelyCallsExecuteUser(
            ModelCommand<object> sut,
            User[] users,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.Execute(users[0]) == commands[0]);
            commands[0].Of(x => x.Execute(users[1]) == commands[1]);
            commands[1].Of(x => x.Execute(users[2]) == commands[2]);

            var actual = sut.Execute(users);

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void ExecuteArticlesRespectivelyCallsExecuteArticle(
            ModelCommand<object> sut,
            Article[] articles,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.Execute(articles[0]) == commands[0]);
            commands[0].Of(x => x.Execute(articles[1]) == commands[1]);
            commands[1].Of(x => x.Execute(articles[2]) == commands[2]);

            var actual = sut.Execute(articles);

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void ExecuteBookmarksRespectivelyCallsExecuteBookmark(
            ModelCommand<object> sut,
            Bookmark[] bookmarks,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.Execute(bookmarks[0]) == commands[0]);
            commands[0].Of(x => x.Execute(bookmarks[1]) == commands[1]);
            commands[1].Of(x => x.Execute(bookmarks[2]) == commands[2]);

            var actual = sut.Execute(bookmarks);

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void ExecuteKeywordsRespectivelyCallsExecuteKeyword(
            ModelCommand<object> sut,
            Keyword[] keywords,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.Execute(keywords[0]) == commands[0]);
            commands[0].Of(x => x.Execute(keywords[1]) == commands[1]);
            commands[1].Of(x => x.Execute(keywords[2]) == commands[2]);

            var actual = sut.Execute(keywords);

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteAsyncReturnsSutItself(ModelCommand<object> sut)
        {
            yield return TestCase.WithAuto<User>().Create(
                x =>
                {
                    var actual = sut.ExecuteAsync(x).Result;
                    Assert.Equal(sut, actual);
                });
            yield return TestCase.WithAuto<Article>().Create(
                x =>
                {
                    var actual = sut.ExecuteAsync(x).Result;
                    Assert.Equal(sut, actual);
                });
            yield return TestCase.WithAuto<Bookmark>().Create(
                x =>
                {
                    var actual = sut.ExecuteAsync(x).Result;
                    Assert.Equal(sut, actual);
                });
            yield return TestCase.WithAuto<Keyword>().Create(
               x =>
               {
                   var actual = sut.ExecuteAsync(x).Result;
                   Assert.Equal(sut, actual);
               });
        }

        [Test]
        public void ExecuteAsyncUsersRespectivelyCallsExecuteUser(
            ModelCommand<object> sut,
            User[] users,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.ExecuteAsync(users[0]) == Task.FromResult(commands[0]));
            commands[0].Of(x => x.ExecuteAsync(users[1]) == Task.FromResult(commands[1]));
            commands[1].Of(x => x.ExecuteAsync(users[2]) == Task.FromResult(commands[2]));

            var actual = sut.ExecuteAsync(users).Result;

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void ExecuteAsyncArticlesRespectivelyCallsExecuteArticle(
            ModelCommand<object> sut,
            Article[] articles,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.ExecuteAsync(articles[0]) == Task.FromResult(commands[0]));
            commands[0].Of(x => x.ExecuteAsync(articles[1]) == Task.FromResult(commands[1]));
            commands[1].Of(x => x.ExecuteAsync(articles[2]) == Task.FromResult(commands[2]));

            var actual = sut.ExecuteAsync(articles).Result;

            Assert.Equal(commands[2], actual);
        }
        
        [Test]
        public void ExecuteAsyncBookmarksRespectivelyCallsExecuteBookmark(
            ModelCommand<object> sut,
            Bookmark[] bookmarks,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.ExecuteAsync(bookmarks[0]) == Task.FromResult(commands[0]));
            commands[0].Of(x => x.ExecuteAsync(bookmarks[1]) == Task.FromResult(commands[1]));
            commands[1].Of(x => x.ExecuteAsync(bookmarks[2]) == Task.FromResult(commands[2]));

            var actual = sut.ExecuteAsync(bookmarks).Result;

            Assert.Equal(commands[2], actual);
        }

        [Test]
        public void ExecuteAsyncKeywordsRespectivelyCallsExecuteKeyword(
            ModelCommand<object> sut,
            Keyword[] keywords,
            IModelCommand<object>[] commands)
        {
            sut.Of(x => x.ExecuteAsync(keywords[0]) == Task.FromResult(commands[0]));
            commands[0].Of(x => x.ExecuteAsync(keywords[1]) == Task.FromResult(commands[1]));
            commands[1].Of(x => x.ExecuteAsync(keywords[2]) == Task.FromResult(commands[2]));

            var actual = sut.ExecuteAsync(keywords).Result;

            Assert.Equal(commands[2], actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.Execute(default(User)));
            yield return this.Methods.Select(x => x.Execute(default(Article)));
            yield return this.Methods.Select(x => x.Execute(default(Bookmark)));
            yield return this.Methods.Select(x => x.Execute(default(Keyword)));

            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}