namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Moq;
    using Ploeh.AutoFixture;
    using Xunit;

    public abstract class CompositeModelCommandTest<TReturn> : IdiomaticTest<CompositeModelCommand<TReturn>>
    {
        [Test]
        public void SutIsModelCommand(CompositeModelCommand<TReturn> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<TReturn>>(sut);
        }

        [Test]
        public IEnumerable<ITestCase> ExecuteAsyncReturnsCorrectResult(
            IEnumerable<TReturn>[] values)
        {
            yield return TestCase.WithAuto<CompositeModelCommand<TReturn>, User>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) => 
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
            yield return TestCase.WithAuto<CompositeModelCommand<TReturn>, Article>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) =>
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
            yield return TestCase.WithAuto<CompositeModelCommand<TReturn>, Keyword>().Create((sut, model) =>
            {
                sut.Commands.Select((c, i) =>
                    c.Of(x => x.ExecuteAsync(model) == Task.FromResult(values[i]))).ToArray();
                var actual = sut.ExecuteAsync(model).Result;
                Assert.Equal(values.SelectMany(x => x), actual);
            });
            yield return TestCase.WithAuto<CompositeModelCommand<TReturn>, Bookmark>().Create((sut, model) =>
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

    public class CompositeModelCommandOfObjectTest : CompositeModelCommandTest<object>
    {
    }

    public class CompositeModelCommandOfInt32Test : CompositeModelCommandTest<int>
    {
    }

    public class CompositeModelCommandOfStringTest : CompositeModelCommandTest<string>
    {
    }
}