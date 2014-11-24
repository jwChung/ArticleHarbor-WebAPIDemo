namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class CanDeleteConfirmableCommandTest : IdiomaticTest<CanDeleteConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanDeleteConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<Task>>(sut);
        }

        [Test]
        public void ResultIsEmpty(CanDeleteConfirmableCommand sut)
        {
            var actual = sut.Result;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteUserWithInvalidRoleThrows(
            CanDeleteConfirmableCommand sut,
            User user)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(user));
        }

        [Test]
        public void ExecuteUserWithAdministratorRoleDoesNotThrow(
            CanDeleteConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.Execute(user);
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteUserWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            CanDeleteConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == user.Id);

            var actual = sut.Execute(user);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteUserWithAuthorRoleAndIncorrectOwnerIdThrow(
            CanDeleteConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.Execute(user));
        }

        [Test]
        public void ExecuteUserIgnoresUserNameCase(
            CanDeleteConfirmableCommand sut,
            User user)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == user.Id.ToUpper());

            var actual = sut.Execute(user);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithInvalidRoleThrows(
            CanDeleteConfirmableCommand sut,
            Article article)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(article));
        }

        [Test]
        public void ExecuteArticleWithAdministratorRoleDoesNotThrow(
            CanDeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.Execute(article);
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            CanDeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.Execute(article);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithAuthorRoleAndIncorrectOwnerIdThrow(
            CanDeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.Execute(article));
        }

        [Test]
        public void ExecuteArticleIgnoresArticleNameCase(
            CanDeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId.ToUpper());

            var actual = sut.Execute(article);

            Assert.Equal(sut, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Result);
        }
    }
}