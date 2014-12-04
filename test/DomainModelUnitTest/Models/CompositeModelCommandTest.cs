namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
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

    public class CompositeModelCommandOfEnumerableObjectTest : CompositeModelCommandTest<IEnumerable<object>>
    {
        [Test]
        public void ExecuteAsyncUsersReturnsCorrectResult(
            IEnumerable<User> users,
            IEnumerable<object>[] values,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<IEnumerable<object>, IEnumerable<object>, IEnumerable<object>>>(
                (x, y) => x.Concat(y));
            var sut = fixture.Create<CompositeModelCommand<IEnumerable<object>>>();

            sut.Commands.Select((c, i) => c.Of(
                x => x.ExecuteAsync(users) == Task.FromResult(
                    Mock.Of<IModelCommand<IEnumerable<object>>>(m => m.Value == values[i]))))
                .ToArray();

            var expected = sut.Value.Concat(
                sut.Commands.SelectMany(c => c.ExecuteAsync(users).Result.Value));

            // Exercise system
            var actual = sut.ExecuteAsync(users).Result;

            // Verify outcome
            var compositeModelCommand = Assert
                .IsAssignableFrom<CompositeModelCommand<IEnumerable<object>>>(actual);
            Assert.Equal(sut.Commands, compositeModelCommand.Commands);
            Assert.Equal(expected.Count(), compositeModelCommand.Value.Count());
            Assert.Empty(expected.Except(compositeModelCommand.Value));
        }
    }
}