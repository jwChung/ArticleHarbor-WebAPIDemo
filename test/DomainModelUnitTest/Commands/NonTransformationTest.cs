namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Models;
    using Xunit;

    public class NonTransformationTest : IdiomaticTest<NonTransformation>
    {
        [Test]
        public void SutIsModelTransformer(NonTransformation sut)
        {
            Assert.IsAssignableFrom<IModelTransformation>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> TransformAsyncReturnsArgumentItself()
        {
            yield return TestCase.WithAuto<NonTransformation, User>().Create((sut, user) =>
            {
                var actual = sut.TransformAsync(user).Result;
                Assert.Equal(user, actual);
            });
            yield return TestCase.WithAuto<NonTransformation, Article>().Create((sut, article) =>
            {
                var actual = sut.TransformAsync(article).Result;
                Assert.Equal(article, actual);
            });
            yield return TestCase.WithAuto<NonTransformation, Keyword>().Create((sut, keyword) =>
            {
                var actual = sut.TransformAsync(keyword).Result;
                Assert.Equal(keyword, actual);
            });
            yield return TestCase.WithAuto<NonTransformation, Bookmark>().Create((sut, bookmark) =>
            {
                var actual = sut.TransformAsync(bookmark).Result;
                Assert.Equal(bookmark, actual);
            });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.TransformAsync(default(User)));
            yield return this.Methods.Select(x => x.TransformAsync(default(Article)));
            yield return this.Methods.Select(x => x.TransformAsync(default(Keyword)));
            yield return this.Methods.Select(x => x.TransformAsync(default(Bookmark)));
        }
    }
}