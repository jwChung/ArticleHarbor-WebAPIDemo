namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.Albedo;
    using Xunit;

    public class CanModifyConfirmableCommandTest : IdiomaticTest<CanModifyConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanModifyConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<object>>(sut);
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
        public void ExecuteArticleWithAuthorRoleAndOwnIdDoesNotThrow(
            CanModifyConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.Execute(article);

            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithAuthorRoleAndIncorrectOwnIdThrow(
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

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Result);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.Execute(default(Article)));
            yield return this.Methods.Select(x => x.Execute(default(Keyword)));
            yield return this.Methods.Select(x => x.Execute(default(Bookmark)));
            yield return this.Methods.Select(x => x.Execute(default(User)));
        }
    }
}