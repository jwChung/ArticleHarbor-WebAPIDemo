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

    public abstract class CompositeModelCommandTest<TValue> : IdiomaticTest<CompositeModelCommand<TValue>>
    {
        [Test]
        public void SutIsModelCommand(CompositeModelCommand<TValue> sut)
        {
            Assert.IsAssignableFrom<IModelCommand<TValue>>(sut);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.ExecuteAsync(default(User)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Article)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Keyword)));
            yield return this.Methods.Select(x => x.ExecuteAsync(default(Bookmark)));
        }
    }

    public class CompositeModelCommandOfEnumerableObjectTest : CompositeModelCommandTest<IEnumerable<object>>
    {
    }

    public class CompositeModelCommandOfInt32Test : CompositeModelCommandTest<int>
    {
        [Test]
        public IEnumerable<ITestCase> ExecuteAsyncReturnsCorrectResult(
            int[] values,
            IFixture fixture)
        {
            ////fixture.Inject<Func<int, int, int>>((x, y) => x + y);
            ////var sut = fixture.Create<CompositeModelCommand<int>>();
            ////var expected = sut.Value + values.Sum();

            ////yield return TestCase.WithAuto<User>().Create(model =>
            ////{
            ////    sut.Commands.Select((c, i) => c.Of(
            ////        x => x.ExecuteAsync(model) == Task.FromResult(
            ////            Mock.Of<IModelCommand<int>>(m => m.Value == values[i]))))
            ////        .ToArray();

            ////    var actual = sut.ExecuteAsync(model).Result;

            ////    VerifyCompositeModelCommand(sut, expected, actual);
            ////});
            ////yield return TestCase.WithAuto<Article>().Create(model =>
            ////{
            ////    sut.Commands.Select((c, i) => c.Of(
            ////        x => x.ExecuteAsync(model) == Task.FromResult(
            ////            Mock.Of<IModelCommand<int>>(m => m.Value == values[i]))))
            ////        .ToArray();

            ////    var actual = sut.ExecuteAsync(model).Result;

            ////    VerifyCompositeModelCommand(sut, expected, actual);
            ////});
            ////yield return TestCase.WithAuto<Keyword>().Create(model =>
            ////{
            ////    sut.Commands.Select((c, i) => c.Of(
            ////        x => x.ExecuteAsync(model) == Task.FromResult(
            ////            Mock.Of<IModelCommand<int>>(m => m.Value == values[i]))))
            ////        .ToArray();

            ////    var actual = sut.ExecuteAsync(model).Result;

            ////    VerifyCompositeModelCommand(sut, expected, actual);
            ////});
            ////yield return TestCase.WithAuto<Bookmark>().Create(model =>
            ////{
            ////    sut.Commands.Select((c, i) => c.Of(
            ////        x => x.ExecuteAsync(model) == Task.FromResult(
            ////            Mock.Of<IModelCommand<int>>(m => m.Value == values[i]))))
            ////        .ToArray();

            ////    var actual = sut.ExecuteAsync(model).Result;

            ////    VerifyCompositeModelCommand(sut, expected, actual);
            ////});
            return null;
        }

        private static void VerifyCompositeModelCommand(
            CompositeModelCommand<int> sut,
            int expected,
            IModelCommand<int> actual)
        {
            ////var compositeModelCommand = Assert
            ////    .IsAssignableFrom<CompositeModelCommand<int>>(actual);
            ////Assert.Equal(sut.Commands, compositeModelCommand.Commands);
            ////Assert.Equal(expected, compositeModelCommand.Value);
        }
    }
}