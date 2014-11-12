namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
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
        public void TransformReturnsCorrectResult(
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
    }
}