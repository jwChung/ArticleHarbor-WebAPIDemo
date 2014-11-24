namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class CanModifyConfirmableCommandTest : IdiomaticTest<CanModifyConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanModifyConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<Task>>(sut);
        }

        [Test]
        public void ExecuteArticleWithInvalidRoleThrows(
            CanModifyConfirmableCommand sut,
            Article article)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(article));
        }

        [Test]
        public void ExecuteArticleWithAdministratorRoleDoesNotThrow(
            CanModifyConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.Execute(article);
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            CanModifyConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.Execute(article);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithAuthorRoleAndIncorrectOwnerIdThrow(
            CanModifyConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.Execute(article));
        }

        [Test]
        public void ExecuteArticleIgnoresUserNameCase(
            CanModifyConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId.ToUpper());

            var actual = sut.Execute(article);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteKeywordCallsExecuteWithCorrespondingArticle(
            Mock<CanModifyConfirmableCommand> mock,
            Keyword keyword,
            Article article)
        {
            var sut = mock.Object;
            sut.UnitOfWork.Articles.Of(
                x => x.FindAsync(new Keys<int>(keyword.ArticleId)) == Task.FromResult(article));
            mock.Setup(x => x.Execute(article)).Returns(sut);

            IModelCommand<Task> actual = sut.Execute(keyword);

            actual.Result.Single().Wait();
            mock.Verify(x => x.Execute(article));
        }

        [Test]
        public void ExecuteKeywordReturnsCorrectCommand(
            CanModifyConfirmableCommand sut,
            Keyword keyword)
        {
            var actual = sut.Execute(keyword);

            var command = Assert.IsAssignableFrom<CanModifyConfirmableCommand>(actual);
            Assert.Equal(sut.Principal, command.Principal);
            Assert.Equal(sut.UnitOfWork, command.UnitOfWork);
        }

        [Test]
        public void ExecuteBookmarkWithInvalidRoleThrows(
            CanModifyConfirmableCommand sut,
            Bookmark bookmark)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(bookmark));
        }

        [Test]
        public void ExecuteBookmarkWithAdministratorRoleDoesNotThrow(
            CanModifyConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.Execute(bookmark);
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteBookmarkWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            CanModifyConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == bookmark.UserId);

            var actual = sut.Execute(bookmark);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteBookmarkWithAuthorRoleAndIncorrectOwnerIdThrow(
            CanModifyConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.Execute(bookmark));
        }

        [Test]
        public void ExecuteBookmarkIgnoresUserNameCase(
            CanModifyConfirmableCommand sut,
            Bookmark bookmark)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == bookmark.UserId.ToUpper());

            var actual = sut.Execute(bookmark);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteUserWithInvalidRoleThrows(
            CanModifyConfirmableCommand sut,
            User user)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(user));
        }

        [Test]
        public void ExecuteUserWithAdministratorRoleDoesNotThrow(
            CanModifyConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.Execute(user);
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteUserWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            CanModifyConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == user.Id);

            var actual = sut.Execute(user);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteUserWithAuthorRoleAndIncorrectOwnerIdThrow(
            CanModifyConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.Execute(user));
        }

        [Test]
        public void ExecuteUserIgnoresUserNameCase(
            CanModifyConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == user.Id.ToUpper());

            var actual = sut.Execute(user);

            Assert.Equal(sut, actual);
        }
    }
}