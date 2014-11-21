namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ploeh.Albedo;
    using Xunit;
    using Xunit.Extensions;

    public class CanCreateConfirmableCommandTest
        : IdiomaticTest<CanCreateConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanCreateConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<object>>(sut);
        }

        [Test]
        public void ResultThrows(CanCreateConfirmableCommand sut)
        {
            Assert.Throws<NotSupportedException>(() => sut.Result);
        }

        [Test]
        [InlineData("Author")]
        [InlineData("Administrator")]
        public void ExecuteArticleWithValidPermissionDoesNotThrow(
            string roleName,
            CanCreateConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole(roleName) == true);
            var actual = sut.Execute(article);
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ExecuteArticleWithInvalidPermissionThrows(
            CanCreateConfirmableCommand sut,
            Article article)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(article));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Result);
            yield return Constructors.Select(() => new CanCreateConfirmableCommand(null));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.Execute(default(Article)));
        }
    }
}