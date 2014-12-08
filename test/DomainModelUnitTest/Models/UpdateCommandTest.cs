namespace ArticleHarbor.DomainModel.Models
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
            bool verifies = false;
            var task = Task.Run(() =>
            {
                Thread.Sleep(300);
                verifies = true;
            });
            sut.Repositories.Users.Of(x => x.UpdateAsync(user) == task);

            var actual = sut.ExecuteAsync(user).Result;

            Assert.True(verifies);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyUpdates(
            UpdateCommand sut,
            Article article)
        {
            bool verifies = false;
            var task = Task.Run(() =>
            {
                Thread.Sleep(300);
                verifies = true;
            });
            sut.Repositories.Articles.Of(x => x.UpdateAsync(article) == task);

            var actual = sut.ExecuteAsync(article).Result;

            Assert.True(verifies);
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyUpdates(
            UpdateCommand sut,
            Keyword keyword)
        {
            bool verifies = false;
            var task = Task.Run(() =>
            {
                Thread.Sleep(300);
                verifies = true;
            });
            sut.Repositories.Keywords.Of(x => x.UpdateAsync(keyword) == task);

            var actual = sut.ExecuteAsync(keyword).Result;

            Assert.True(verifies);
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
        }
    }
}