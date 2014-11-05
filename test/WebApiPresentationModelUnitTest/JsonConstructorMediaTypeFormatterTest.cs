namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Jwc.Experiment.Xunit;
    using WebApiPresentationModel;
    using Xunit;

    public class JsonConstructorMediaTypeFormatterTest
    {
        [Test]
        public void SubIsMediaTypeFormatter(
            JsonConstructorMediaTypeFormatter sut)
        {
            Assert.IsAssignableFrom<MediaTypeFormatter>(sut);
        }

        [Test]
        public void CanWriteTypeWithAnyTypeReturnsFalse(
            JsonConstructorMediaTypeFormatter sut,
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
            return TestCases.WithArgs(simpleTypes).WithAuto<JsonConstructorMediaTypeFormatter>()
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
            return TestCases.WithArgs(simpleTypes).WithAuto<JsonConstructorMediaTypeFormatter>()
                .Create((type, sut) =>
                {
                    var actual = sut.CanReadType(type);
                    Assert.True(actual);
                });
        }

        public class Person
        {
            public Person(string name, int age)
            {
            }
        }
    }
}