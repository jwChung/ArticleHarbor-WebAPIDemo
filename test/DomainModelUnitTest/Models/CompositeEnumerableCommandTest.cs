namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Ploeh.AutoFixture.Xunit;
    using Xunit;

    public abstract class CompositeEnumerableCommandTest<TValueElement>
        : IdiomaticTest<CompositeEnumerableCommand<TValueElement>>
    {
        [Test]
        public void SutIsCompositeModelCommand(CompositeEnumerableCommand<TValueElement> sut)
        {
            Assert.IsAssignableFrom<CompositeModelCommand<IEnumerable<TValueElement>>>(sut);
        }

        [Test]
        public void ValueIsEmptyWhenInitializedWithArray(
            [FavorArrays] CompositeEnumerableCommand<TValueElement> sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Test]
        public void ValueIsEmptyWhenInitializedWithEnumerable(
            [FavorEnumerables] CompositeEnumerableCommand<TValueElement> sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Test]
        public void ConcatIsCorrectWhenInitializedWithArray(
            [FavorArrays] CompositeEnumerableCommand<TValueElement> sut,
            IEnumerable<TValueElement> value1,
            IEnumerable<TValueElement> value2)
        {
            var actual = sut.Concat;

            var result = actual(value1, value2);
            Assert.Equal(value1.Count() + value2.Count(), result.Count());
            Assert.Empty(result.Except(value1).Except(value2));
        }

        [Test]
        public void ConcatIsCorrectWhenInitializedWithEnumerable(
            [FavorEnumerables] CompositeEnumerableCommand<TValueElement> sut,
            IEnumerable<TValueElement> value1,
            IEnumerable<TValueElement> value2)
        {
            var actual = sut.Concat;

            var result = actual(value1, value2);
            Assert.Equal(value1.Count() + value2.Count(), result.Count());
            Assert.Empty(result.Except(value1).Except(value2));
        }
    }

    public class CompositeEnumerableCommandOfObjectTest : CompositeEnumerableCommandTest<object>
    {
    }

    public class CompositeEnumerableCommandOfInt32Test : CompositeEnumerableCommandTest<int>
    {
    }

    public class CompositeEnumerableCommandOfStringTest : CompositeEnumerableCommandTest<string>
    {
    }
}