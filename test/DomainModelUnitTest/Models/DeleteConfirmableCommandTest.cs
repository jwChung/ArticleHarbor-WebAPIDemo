namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class DeleteConfirmableCommandTest : IdiomaticTest<DeleteConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }
        
        [Test]
        public void ValueIsEmpty(DeleteConfirmableCommand sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
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
            Assert.Equal(sut, actual);
        }
        
        [Test]
        public void ExecuteAsyncArticleWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            DeleteConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Equal(sut, actual);
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

            Assert.Equal(sut, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
        }
    }
}