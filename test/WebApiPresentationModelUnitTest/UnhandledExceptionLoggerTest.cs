namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.ExceptionHandling;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using WebApiPresentationModel;
    using Xunit;

    public class UnhandledExceptionLoggerTest : IdiomaticTest<UnhandledExceptionLogger>
    {
        [Test]
        public void SutIsExceptionLogger(UnhandledExceptionLogger sut)
        {
            Assert.IsAssignableFrom<ExceptionLogger>(sut);
        }

        [Test]
        public void ShouldLogReturnsTrueWithNonArgumentException(
            UnhandledExceptionLogger sut,
            ExceptionLoggerContext context)
        {
            var actual = sut.ShouldLog(context);
            Assert.True(actual);
        }

        [Test]
        public IEnumerable<ITestCase> ShouldLogReturnsFlaseWithArgumentException()
        {
            var testData = new[]
            {
                new ArgumentException(),
                new ArgumentNullException(),
                new ArgumentOutOfRangeException()
            };

            return TestCases.WithArgs(testData).WithAuto<IFixture>()
                .Create((exception, fixture) =>
                {
                    fixture.Inject<Exception>(exception);
                    var context = fixture.Create<ExceptionLoggerContext>();
                    var sut = fixture.Create<UnhandledExceptionLogger>();
                        
                    var actual = sut.ShouldLog(context);

                    Assert.False(actual);
                });
        }
    }
}