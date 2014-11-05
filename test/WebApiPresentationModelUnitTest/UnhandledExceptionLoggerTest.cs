namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;
    using Jwc.Experiment.Xunit;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison;
    using Ploeh.SemanticComparison.Fluent;
    using WebApiPresentationModel;
    using Xunit;

    public class UnhandledExceptionLoggerTest : IdiomaticTest<UnhandledExceptionLogger>
    {
        [Test]
        public async Task LogAsyncCorrectlyLogs(
            [Frozen] Uri url,
            [Frozen] Exception exception,
            UnhandledExceptionLogger sut,
            ExceptionLoggerContext context,
            CancellationToken token)
        {
            Assert.Equal(url, context.Request.RequestUri);
            Assert.Equal(exception, context.Exception);
            var likeness = new LogContext(url, exception.ToString()).AsSource()
                .OfLikeness<LogContext>()
                .Without(x => x.Date);

            await sut.LogAsync(context, token);
            
            sut.Logger.ToMock().Verify(
                x => x.LogAsync(It.Is<LogContext>(p => likeness.Equals(p))));
        }
        
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