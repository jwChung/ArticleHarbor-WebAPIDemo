namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
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

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.Execute(default(User)));
            yield return this.Methods.Select(x => x.Execute(default(Article)));
            yield return this.Methods.Select(x => x.Execute(default(Bookmark)));
            yield return this.Methods.Select(x => x.Execute(default(Keyword)));
        }
    }
}