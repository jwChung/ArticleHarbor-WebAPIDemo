namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public abstract class TransformableCommandTest<TReturn> : IdiomaticTest<TransformableCommand<TReturn>>
    {
        [Test]
        public void SutIsModelCommand(TransformableCommand<TReturn> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<TReturn>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserReturnsCorrectResult(
            TransformableCommand<TReturn> sut,
            User user,
            User newUser,
            IEnumerable<TReturn> expected)
        {
            sut.Transformer.Of(x => x.TransformAsync(user) == Task.FromResult(newUser));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newUser) == Task.FromResult(expected));

            var actual = sut.ExecuteAsync(user).Result;

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ExecuteAsyncArticleReturnsCorrectResult(
            TransformableCommand<TReturn> sut,
            Article article,
            Article newArticle,
            IEnumerable<TReturn> expected)
        {
            sut.Transformer.Of(x => x.TransformAsync(article) == Task.FromResult(newArticle));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newArticle) == Task.FromResult(expected));

            var actual = sut.ExecuteAsync(article).Result;

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ExecuteAsyncKeywordReturnsCorrectResult(
            TransformableCommand<TReturn> sut,
            Keyword keyword,
            Keyword newKeyword,
            IEnumerable<TReturn> expected)
        {
            sut.Transformer.Of(x => x.TransformAsync(keyword) == Task.FromResult(newKeyword));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newKeyword) == Task.FromResult(expected));

            var actual = sut.ExecuteAsync(keyword).Result;

            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
        }
    }

    public class TransformableCommandOfObjectTest : TransformableCommandTest<object>
    {
    }

    public class TransformableCommandOfInt32Test : TransformableCommandTest<int>
    {
    }
}