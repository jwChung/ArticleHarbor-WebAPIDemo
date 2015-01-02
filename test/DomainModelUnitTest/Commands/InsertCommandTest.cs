namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<EmptyCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyInsertsUser(
            InsertCommand sut,
            User user,
            User newUser,
            IEnumerable<IModel> value)
        {
            sut.Repositories.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newUser) == Task.FromResult(value));
            
            var actual = sut.ExecuteAsync(user).Result;

            Assert.Equal(new IModel[] { newUser }.Concat(value), actual);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyInsertsArticle(
            InsertCommand sut,
            Article article,
            Article newArticle,
            IEnumerable<IModel> value)
        {
            sut.Repositories.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newArticle) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Equal(new IModel[] { newArticle }.Concat(value), actual);
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyInsertsKeyword(
            InsertCommand sut,
            Keyword keyword,
            Keyword newKeyword,
            IEnumerable<IModel> value)
        {
            sut.Repositories.Keywords.Of(x => x.InsertAsync(keyword) == Task.FromResult(newKeyword));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newKeyword) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(keyword).Result;

            Assert.Equal(new IModel[] { newKeyword }.Concat(value), actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkCorrectlyInsertsBookmark(
            InsertCommand sut,
            Bookmark bookmark,
            Bookmark newBookmark,
            IEnumerable<IModel> value)
        {
            sut.Repositories.Bookmarks.Of(x => x.InsertAsync(bookmark) == Task.FromResult(newBookmark));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newBookmark) == Task.FromResult(value));

            var actual = sut.ExecuteAsync(bookmark).Result;

            Assert.Equal(new IModel[] { newBookmark }.Concat(value), actual);
        }
    }
}