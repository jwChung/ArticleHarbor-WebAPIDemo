﻿namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateCommandTest : IdiomaticTest<UpdateCommand>
    {
        [Test]
        public void SutIsModelCommand(UpdateCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsEmpty(UpdateCommand sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyUpdates(
            UpdateCommand sut,
            User user)
        {
            var actual = sut.ExecuteAsync(user).Result;
            Assert.Equal(sut, actual);
            sut.Repositories.Users.ToMock().Verify(x => x.UpdateAsync(user));
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyUpdates(
            UpdateCommand sut,
            Article article)
        {
            var actual = sut.ExecuteAsync(article).Result;
            Assert.Equal(sut, actual);
            sut.Repositories.Articles.ToMock().Verify(x => x.UpdateAsync(article));
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyUpdates(
            UpdateCommand sut,
            Keyword keyword)
        {
            var actual = sut.ExecuteAsync(keyword).Result;
            Assert.Equal(sut, actual);
            sut.Repositories.Keywords.ToMock().Verify(x => x.UpdateAsync(keyword));
        }

        [Test]
        public void ExecuteAsyncBookmarkCorrectlyUpdates(
            UpdateCommand sut,
            Bookmark bookmark)
        {
            var actual = sut.ExecuteAsync(bookmark).Result;
            Assert.Equal(sut, actual);
            sut.Repositories.Bookmarks.ToMock().Verify(x => x.UpdateAsync(bookmark));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
        }
    }
}