namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class UpdateConfirmableCommandTest : IdiomaticTest<UpdateConfirmableCommand>
    {
        [Test]
        public void SutIsModelCommand(UpdateConfirmableCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IModel>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserThrows(
            UpdateConfirmableCommand sut,
            User user)
        {
            Assert.Throws<NotSupportedException>(() => sut.ExecuteAsync(user).Result);
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
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncArticleWithAuthorRoleAndCorrectOwnerIdDoesNotThrow(
            UpdateConfirmableCommand sut,
            Article article)
        {
            sut.Principal.Of(x => x.IsInRole("Author") == true);
            sut.Principal.Identity.Of(x => x.Name == article.UserId);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Empty(actual);
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

            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncKeywordRelaysExecuteAsyncArticle(
            Mock<UpdateConfirmableCommand> mock,
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
        public void ExecuteAsyncBookmarkThrows(
            UpdateConfirmableCommand sut,
            Bookmark bookmark)
        {
            Assert.Throws<NotSupportedException>(() => sut.ExecuteAsync(bookmark).Result);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
        }
    }
}