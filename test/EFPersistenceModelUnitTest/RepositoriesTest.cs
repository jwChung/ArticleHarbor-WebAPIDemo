namespace ArticleHarbor.EFPersistenceModel
{
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel.Repositories;
    using Xunit;

    public class RepositoriesTest : IdiomaticTest<Repositories>
    {
        [Test]
        public void SutIsRepositories(Repositories sut)
        {
            Assert.IsAssignableFrom<IRepositories>(sut);
        }

        [Test]
        public void ArticlesIsCorrect(Repositories sut)
        {
            var actual = sut.Articles;

            var articles = Assert.IsAssignableFrom<ArticleRepository>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Articles, articles.DbSet);
        }

        [Test]
        public void KeywordsIsCorrect(Repositories sut)
        {
            var actual = sut.Keywords;

            var articles = Assert.IsAssignableFrom<KeywordRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Keywords, articles.DbSet);
        }

        [Test]
        public void BookmarksIsCorrect(Repositories sut)
        {
            var actual = sut.Bookmarks;

            var articles = Assert.IsAssignableFrom<BookmarkRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Bookmarks, articles.DbSet);
        }

        [Test]
        public void UsersIsCorrect(Repositories sut)
        {
            var actual = sut.Users;

            var articles = Assert.IsAssignableFrom<UserRepository2>(actual);
            Assert.Equal(sut.Context, articles.Context);
            Assert.Equal(sut.Context.Users, articles.DbSet);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Articles);
            yield return this.Properties.Select(x => x.Keywords);
            yield return this.Properties.Select(x => x.Bookmarks);
            yield return this.Properties.Select(x => x.Users);
        }
    }
}