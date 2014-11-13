namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Ploeh.AutoFixture;
    using Xunit;

    public class DelegateTransformationTest : IdiomaticTest<DelegateTransformation>
    {
        [Test]
        public void SutIsArticleTransformation(DelegateTransformation sut)
        {
            Assert.IsAssignableFrom<IArticleTransformation>(sut);
        }

        [Test]
        [Obsolete]
        public void ObsoleteTransformReturnsCorrectResult(
            Article article,
            Article newArticle,
            IFixture fixture)
        {
            fixture.Inject<Func<Article, Article>>(x =>
            {
                Assert.Equal(article, x);
                return newArticle;
            });
            var sut = fixture.Create<DelegateTransformation>();

            var actual = sut.Transform(article);

            Assert.Equal(newArticle, actual);
        }

        [Test]
        public void TransformReturnsCorrectResult(
            IEnumerable<Article> articles,
            IList<Article> newArticles,
            IFixture fixture)
        {
            var transformed = new List<Article>();
            int index = 0;
            fixture.Inject<Func<Article, Article>>(x =>
            {
                transformed.Add(x);
                return newArticles[index++];
            });
            var sut = fixture.Create<DelegateTransformation>();

            var actual = sut.Transform(articles);

            Assert.Equal(newArticles, actual);
            Assert.Equal(articles, transformed);
        }
    }
}