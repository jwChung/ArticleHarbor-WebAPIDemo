namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xunit;

    public abstract class TransformableCommandTest<TReturn> : IdiomaticTest<TransformableCommand<TReturn>>
    {
        [Test]
        public void SutIsModelCommand(TransformableCommand<TReturn> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<TReturn>>(sut);
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