namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Ploeh.Albedo;
    using Xunit;

    public class CanCreateConfirmableCommandTest
        : IdiomaticTest<CanCreateConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(CanCreateConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<Task>>>(sut);
        }

        [Test]
        public void ResultReturnsEmpty(CanCreateConfirmableCommand sut)
        {
            Assert.Empty(sut.Result);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteWithValidRoleDoesNotThrow()
        {
            var roleNames = new[]
            {
                "Author",
                "Administrator"
            };

            var articleCases = TestCases.WithArgs(roleNames)
                .WithAuto<CanCreateConfirmableCommand, Article>()
                .Create((roleName, sut, model) =>
                {
                    sut.Principal.Of(x => x.IsInRole(roleName));
                    var actual = sut.Execute(model);
                    Assert.Equal(sut, actual);
                });

            var keywordCases = TestCases.WithArgs(roleNames)
                .WithAuto<CanCreateConfirmableCommand, Keyword>()
                .Create((roleName, sut, model) =>
                {
                    sut.Principal.Of(x => x.IsInRole(roleName));
                    var actual = sut.Execute(model);
                    Assert.Equal(sut, actual);
                });

            var bookmarkCases = TestCases.WithArgs(roleNames)
                .WithAuto<CanCreateConfirmableCommand, Bookmark>()
                .Create((roleName, sut, model) =>
                {
                    sut.Principal.Of(x => x.IsInRole(roleName));
                    var actual = sut.Execute(model);
                    Assert.Equal(sut, actual);
                });

            return articleCases.Concat(keywordCases).Concat(bookmarkCases);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteWithInvalidRoleThrows()
        {
            yield return TestCase.WithAuto<CanCreateConfirmableCommand, Article>()
                .Create((sut, model) =>
                {
                    Assert.Throws<UnauthorizedException>(() => sut.Execute(model));
                });

            yield return TestCase.WithAuto<CanCreateConfirmableCommand, Keyword>()
                .Create((sut, model) =>
                {
                    Assert.Throws<UnauthorizedException>(() => sut.Execute(model));
                });

            yield return TestCase.WithAuto<CanCreateConfirmableCommand, Bookmark>()
                .Create((sut, model) =>
                {
                    Assert.Throws<UnauthorizedException>(() => sut.Execute(model));
                });
        }

        [Test]
        public void ExecuteUserAlwaysThrows(
            CanCreateConfirmableCommand sut,
            User user)
        {
            Assert.Throws<UnauthorizedException>(() => sut.Execute(user));
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