namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Xunit;

    public class JsonCustomMediaTypeFormatterTest : IdiomaticTest<JsonCustomMediaTypeFormatter>
    {
        [Test]
        public void SubIsJsonMediaTypeFormatter(
            JsonCustomMediaTypeFormatter sut)
        {
            Assert.IsAssignableFrom<JsonMediaTypeFormatter>(sut);
        }

        [Test]
        public void CanWriteTypeWithAnyTypeReturnsFalse(
            JsonCustomMediaTypeFormatter sut,
            Type type)
        {
            bool actual = sut.CanWriteType(type);
            Assert.False(actual);
        }

        [Test]
        public IEnumerable<ITestCase> CanReadTypeWithSimpleTypeReturnsFalse()
        {
            var simpleTypes = new[]
            {
                typeof(TimeSpan),
                typeof(DateTime),
                typeof(Guid),
                typeof(string),
                typeof(char),
                typeof(bool),
                typeof(int),
                typeof(uint),
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal)
            };
            return TestCases.WithArgs(simpleTypes).WithAuto<JsonCustomMediaTypeFormatter>()
                .Create((type, sut) =>
                {
                    var actual = sut.CanReadType(type);
                    Assert.False(actual);
                });
        }

        [Test]
        public IEnumerable<ITestCase> CanReadTypeWithNonSimpleTypeReturnsTrue()
        {
            var simpleTypes = new[]
            {
                typeof(object),
                this.GetType()
            };
            return TestCases.WithArgs(simpleTypes).WithAuto<JsonCustomMediaTypeFormatter>()
                .Create((type, sut) =>
                {
                    var actual = sut.CanReadType(type);
                    Assert.True(actual);
                });
        }

        [Test]
        public async Task ReadFromStreamAsyncReturnsCorrectResult(
            string value,
            object expected,
            Type type,
            StreamContent content,
            IFormatterLogger formatterLogger,
            IFixture fixture)
        {
            Func<Type, string, object> formatter = (t, s) =>
            {
                Assert.Equal(type, t);
                Assert.Equal(value, s);
                return expected;
            };
            fixture.Inject(formatter);
            var sut = fixture.Create<JsonCustomMediaTypeFormatter>();
            var stream = new MemoryStream(Encoding.Unicode.GetBytes(value));
            content.Headers.Add("Content-Type", "text/html; charset=utf-16");
            content.Headers.Add("Content-Length", "100");

            var actual = await sut.ReadFromStreamAsync(type, stream, content, formatterLogger);

            Assert.Equal(expected, actual);
        }
        
        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.CanWriteType(null));
        }
    }
}