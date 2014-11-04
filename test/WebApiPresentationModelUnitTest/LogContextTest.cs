namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using WebApiPresentationModel;
    using Xunit;

    public class LogContextTest : IdiomaticTest<LogContext>
    {
        [Test]
        public void DateReturnsNow(LogContext sut)
        {
            var actual = sut.Date;

            TimeSpan timeSpane = DateTime.Now - actual;
            Assert.True(timeSpane.Milliseconds < 10);
        }

        [Test]
        public void DateAlwaysReturnsSameValue(LogContext sut)
        {
            var actual = sut.Date;
            Thread.Sleep(10);
            Assert.Equal(sut.Date, actual);
        }

        [Test]
        public void ToStringReturnsCorrectMessage(LogContext sut)
        {
            var expected = "Request URL: " + sut.RequestUrl + Environment.NewLine
                + "Date: " + sut.Date + Environment.NewLine
                + "Message:" + Environment.NewLine + sut.Message;

            var actual = sut.ToString();
            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Date);
        }
    }
}