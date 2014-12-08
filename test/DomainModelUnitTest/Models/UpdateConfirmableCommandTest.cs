namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public class UpdateConfirmableCommandTest : IdiomaticTest<UpdateConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(UpdateConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncArticleWithInvalidRoleThrows(
            UpdateConfirmableCommand sut,
            Article article)
        {
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(article).Result);
        }

        [Test]
        public void ExecuteAsyncArticleWithAdministratorRoleDoesNotThrow(
            UpdateConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Administrator") == true);
            var actual = sut.ExecuteAsync(article).Result;
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteAsyncArticleWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            UpdateConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteAsyncArticleWithAuthorRoleAndIncorrectOwnerIdThrow(
            UpdateConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            Assert.Throws<UnauthorizedException>(() => sut.ExecuteAsync(article).Result);
        }

        [Test]
        public void ExecuteAsyncArticleIgnoresUserNameCase(
            UpdateConfirmableCommand sut,
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
            yield break;
        }
    }
}