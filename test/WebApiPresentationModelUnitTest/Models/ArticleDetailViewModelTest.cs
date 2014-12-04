namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;
    using Xunit;

    public class ArticleDetailViewModelTest : IdiomaticTest<ArticleDetailViewModel>
    {
        [Test]
        public void ArticleHasJsonIgnoreAttribute()
        {
            var attribute = this.Properties.Select(x => x.Article)
                .GetCustomAttribute<JsonIgnoreAttribute>();
            Assert.NotNull(attribute);
        }

        [Test]
        public void IdIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Id;
            Assert.Equal(sut.Article.Id, actual);
        }

        [Test]
        public void ProviderIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Provider;
            Assert.Equal(sut.Article.Provider, actual);
        }

        [Test]
        public void GuidIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Guid;
            Assert.Equal(sut.Article.Guid, actual);
        }

        [Test]
        public void SubjectIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Subject;
            Assert.Equal(sut.Article.Subject, actual);
        }

        [Test]
        public void BodyIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Body;
            Assert.Equal(sut.Article.Body, actual);
        }

        [Test]
        public void DateIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Date;
            Assert.Equal(sut.Article.Date, actual);
        }

        [Test]
        public void UrlIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.Url;
            Assert.Equal(sut.Article.Url, actual);
        }

        [Test]
        public void UserIdIsCorrect(ArticleDetailViewModel sut)
        {
            var actual = sut.UserId;
            Assert.Equal(sut.Article.UserId, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Id);
            yield return this.Properties.Select(x => x.Provider);
            yield return this.Properties.Select(x => x.Guid);
            yield return this.Properties.Select(x => x.Subject);
            yield return this.Properties.Select(x => x.Body);
            yield return this.Properties.Select(x => x.Date);
            yield return this.Properties.Select(x => x.Url);
            yield return this.Properties.Select(x => x.UserId);
        }
    }
}