namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public class ModelInsertingCommandTest : IdiomaticTest<ModelInsertingCommand>
    {
        [Test]
        public void SutIsModelCommand(ModelInsertingCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<Task<IModel>>>>(sut);
        }

        [Test]
        public void ExecuteUserAddsUserToRepository(
            ModelInsertingCommand sut,
            User user,
            User newUser)
        {
            sut.UnitOfWork.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));
            
            var actual = sut.Execute(user);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            var model = command.Result.Last().Result;
            Assert.Equal(newUser, model);
        }

        [Test]
        public void ExecuteUserReturnsCorrectResult(
            [Frozen] IEnumerable<Task<IModel>> result,
            [Greedy] ModelInsertingCommand sut,
            User user)
        {
            var actual = sut.Execute(user);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            Assert.Equal(sut.UnitOfWork, command.UnitOfWork);
            Assert.True(result.All(x => command.Result.Contains(x)));
        }

        [Test]
        public void ExecuteArticleAddsArticleToRepository(
            ModelInsertingCommand sut,
            Article article,
            Article newArticle)
        {
            sut.UnitOfWork.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));

            var actual = sut.Execute(article);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            var model = command.Result.Last().Result;
            Assert.Equal(newArticle, model);
        }

        [Test]
        public void ExecuteArticleReturnsCorrectResult(
            [Frozen] IEnumerable<Task<IModel>> result,
            [Greedy] ModelInsertingCommand sut,
            Article article)
        {
            var actual = sut.Execute(article);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            Assert.Equal(sut.UnitOfWork, command.UnitOfWork);
            Assert.True(result.All(x => command.Result.Contains(x)));
        }

        [Test]
        public void ExecuteKeywordAddsKeywordToRepository(
            ModelInsertingCommand sut,
            Keyword keyword,
            Keyword newKeyword)
        {
            sut.UnitOfWork.Keywords.Of(x => x.InsertAsync(keyword) == Task.FromResult(newKeyword));

            var actual = sut.Execute(keyword);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            var model = command.Result.Last().Result;
            Assert.Equal(newKeyword, model);
        }

        [Test]
        public void ExecuteKeywordReturnsCorrectResult(
            [Frozen] IEnumerable<Task<IModel>> result,
            [Greedy] ModelInsertingCommand sut,
            Keyword keyword)
        {
            var actual = sut.Execute(keyword);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            Assert.Equal(sut.UnitOfWork, command.UnitOfWork);
            Assert.True(result.All(x => command.Result.Contains(x)));
        }

        [Test]
        public void ExecuteBookmarkAddsBookmarkToRepository(
            ModelInsertingCommand sut,
            Bookmark bookmark,
            Bookmark newBookmark)
        {
            sut.UnitOfWork.Bookmarks.Of(x => x.InsertAsync(bookmark) == Task.FromResult(newBookmark));

            var actual = sut.Execute(bookmark);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            var model = command.Result.Last().Result;
            Assert.Equal(newBookmark, model);
        }

        [Test]
        public void ExecuteBookmarkReturnsCorrectResult(
            [Frozen] IEnumerable<Task<IModel>> result,
            [Greedy] ModelInsertingCommand sut,
            Bookmark bookmark)
        {
            var actual = sut.Execute(bookmark);

            var command = Assert.IsAssignableFrom<ModelInsertingCommand>(actual);
            Assert.Equal(sut.UnitOfWork, command.UnitOfWork);
            Assert.True(result.All(x => command.Result.Contains(x)));
        }
    }
}