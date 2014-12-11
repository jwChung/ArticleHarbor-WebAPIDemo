namespace ArticleHarbor.DomainModel.Commands
{
    using Queries;
    using Xunit;

    public class DeleteCommandTest : IdiomaticTest<DeleteCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyDeletes(
            DeleteCommand sut,
            User user)
        {
            var actual = sut.ExecuteAsync(user).Result;

            Assert.Empty(actual);
            sut.Repositories.Users.ToMock().Verify(x => x.DeleteAsync(new Keys<string>(user.Id)));
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyDeletes(
            DeleteCommand sut,
            Article article)
        {
            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
            sut.Repositories.Articles.ToMock().Verify(
                x => x.DeleteAsync(new Keys<int>(article.Id)));
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyDeletes(
            DeleteCommand sut,
            Keyword keyword)
        {
            var actual = sut.ExecuteAsync(keyword).Result;

            Assert.Empty(actual);
            sut.Repositories.Keywords.ToMock().Verify(
                x => x.DeleteAsync((Keys<int, string>)keyword.GetKeys()));
        }

        [Test]
        public void ExecuteAsyncBookmarkCorrectlyDeletes(
            DeleteCommand sut,
            Bookmark bookmark)
        {
            var actual = sut.ExecuteAsync(bookmark).Result;

            Assert.Empty(actual);
            sut.Repositories.Bookmarks.ToMock().Verify(
                x => x.DeleteAsync((Keys<string, int>)bookmark.GetKeys()));
        }
    }
}