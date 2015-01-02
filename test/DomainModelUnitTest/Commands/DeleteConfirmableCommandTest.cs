namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Models;
    using Moq;
    using Queries;
    using Xunit;

    public class DeleteConfirmableCommandTest : IdiomaticTest<DeleteConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<EmptyCommand<IModel>>(sut);
        }
        
        [Test]
        public void ExecuteAsyncUserThrows(
            DeleteConfirmableCommand sut,
            User user)
        {
            Assert.Throws<NotSupportedException>(() => sut.ExecuteAsync(user).Result);
        }

        [Test]
        public void ExecuteAsyncArticleWithInvalidRoleThrows(
            DeleteConfirmableCommand sut,
            Article article)
        {
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(article).Result);
        }

        [Test]
        public void ExecuteAsyncArticleWithAdministratorRoleDoesNotThrow(
            DeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.ExecuteAsync(article).Result;
            Assert.Empty(actual);
        }
        
        [Test]
        public void ExecuteAsyncArticleWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            DeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncArticleWithAuthorRoleAndIncorrectOwnerIdThrow(
            DeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(article).Result);
        }

        [Test]
        public void ExecuteAsyncArticleIgnoresUserNameCase(
            DeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId.ToUpper());

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
        }
        
        [Test]
        public void ExecuteAsyncKeywordRelaysExecuteAsyncArticle(
            Mock<DeleteConfirmableCommand> mock,
            Keyword keyword,
            Article article)
        {
            var sut = mock.Object;
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            sut.Repositories.Articles.Of(
                x => x.FindAsync(new Keys<int>(keyword.ArticleId)) == Task.FromResult(article));

            var actual = sut.ExecuteAsync(keyword).Result;

            mock.Verify(x => x.ExecuteAsync(article));
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkWithInvalidRoleThrows(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(bookmark).Result);
        }

        [Test]
        public void ExecuteAsyncBookmarkWithAdministratorRoleDoesNotThrow(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.ExecuteAsync(bookmark).Result;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == bookmark.UserId);

            var actual = sut.ExecuteAsync(bookmark).Result;

            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkWithAuthorRoleAndIncorrectOwnerIdThrow(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(bookmark).Result);
        }

        [Test]
        public void ExecuteAsyncBookmarkWithUserRoleAndCorrectOwnerIdDoesNotThrow(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("User") == true);
            sut.Principal.Identity.Of(x => x.Name == bookmark.UserId);

            var actual = sut.ExecuteAsync(bookmark).Result;

            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncBookmarkWithUserRoleAndIncorrectOwnerIdThrow(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("User") == true);
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(bookmark).Result);
        }

        [Test]
        public void ExecuteAsyncBookmarkIgnoresUserNameCase(
            DeleteConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == bookmark.UserId.ToUpper());

            var actual = sut.ExecuteAsync(bookmark).Result;

            Assert.Empty(actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
        }
    }
}