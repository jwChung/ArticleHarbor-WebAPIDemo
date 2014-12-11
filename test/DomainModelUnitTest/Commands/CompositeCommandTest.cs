namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Xunit;

    public abstract class CompositeCommandTest<TReturn> : IdiomaticTest<CompositeCommand<TReturn>>
    {
        [Test]
        public void SutIsModelCommand(CompositeCommand<TReturn> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<TReturn>>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteAsyncReturnsCorrectResult(
            IEnumerable<TReturn>[] values)
        {
            yield return TestCase.WithAuto<CompositeCommand<TReturn>, User>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) => 
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
            yield return TestCase.WithAuto<CompositeCommand<TReturn>, Article>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) =>
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
            yield return TestCase.WithAuto<CompositeCommand<TReturn>, Keyword>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) =>
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
            yield return TestCase.WithAuto<CompositeCommand<TReturn>, Bookmark>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) =>
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
        }
    }

    public class CompositeCommandOfObjectTest : CompositeCommandTest<object>
    {
    }

    public class CompositeCommandOfInt32Test : CompositeCommandTest<int>
    {
    }

    public class CompositeCommandOfStringTest : CompositeCommandTest<string>
    {
    }
}