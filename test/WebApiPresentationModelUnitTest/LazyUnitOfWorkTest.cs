namespace EFPersistenceModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel;
    using Ploeh.AutoFixture;
    using WebApiPresentationModel;
    using Xunit;

    public class LazyUnitOfWorkTest : IdiomaticTest<LazyUnitOfWork>
    {
        [Test]
        public void ValueReturnsNewInstanceWhenFirstCalled(
            LazyUnitOfWork sut)
        {
            var expected = sut.UnitOfWorkFactory();
            var actual = sut.Value;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void ValueReturnsSavedInstanceWhenNonFirstCalled(
            IFixture fixture)
        {
            fixture.Inject<Func<IUnitOfWork>>(() => fixture.Create<IUnitOfWork>());
            var sut = fixture.Create<LazyUnitOfWork>();
            var expected = sut.Value;

            var actual = sut.Value;

            Assert.Same(expected, actual);
        }

        [Test]
        public void OptionalReturnsNullWhenValueForActionWasNotCalled(
            LazyUnitOfWork sut)
        {
            var actual = sut.Optional;
            Assert.Null(actual);
        }

        [Test]
        public void OptionalReturnsSavedInstanceWhenValueForActionWasCalled(
            IFixture fixture)
        {
            fixture.Inject<Func<IUnitOfWork>>(() => fixture.Create<IUnitOfWork>());
            var sut = fixture.Create<LazyUnitOfWork>();
            var expected = sut.Value;

            var actual = sut.Optional;

            Assert.Same(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
            yield return this.Properties.Select(x => x.Optional);
        }
    }
}