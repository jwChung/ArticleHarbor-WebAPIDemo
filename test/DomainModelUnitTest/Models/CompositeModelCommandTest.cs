namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public abstract class CompositeModelCommandTest<TValue> : IdiomaticTest<CompositeModelCommand<TValue>>
    {
        [Test]
        public void SutIsModelCommand(CompositeModelCommand<TValue> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<TValue>>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(IEnumerable<User>)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(IEnumerable<Article>)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(IEnumerable<Keyword>)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(IEnumerable<Bookmark>)));

            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
        }
    }

    public class CompositeModelCommandOfObjectTest : CompositeModelCommandTest<object>
    {
    }
}