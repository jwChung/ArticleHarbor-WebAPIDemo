namespace WebApiPresentationModelUnitTest
{
    using System;
    using System.Collections.Generic;
    using Jwc.Experiment;
    using Jwc.Experiment.Idioms;
    using Jwc.Experiment.Xunit;
    using Newtonsoft.Json;
    using Ploeh.AutoFixture.Xunit;
    using WebApiPresentationModel;
    using Xunit;

    public class JsonConstructorDeserializerTest
    {
        [Test]
        public IEnumerable<ITestCase> SutHasAppropriateGuards()
        {
            var members = typeof(JsonConstructorDeserializer).GetIdiomaticMembers();
            return TestCases.WithArgs(members).WithAuto<GuardClauseAssertion>()
                .Create((m, a) => a.Verify(m));
        }

        [Test]
        public void ReadReturnsCorrectResultUsingModestConstructor(Foo foo)
        {
            var json = JsonConvert.SerializeObject(foo);
            
            var actual = (Foo)JsonConstructorDeserializer.Read(typeof(Foo), json);

            Assert.Equal(foo.Name, actual.Name);
            Assert.Equal(-1, actual.Age);
            Assert.Equal(foo.Date, actual.Date);
        }

        [Test]
        public void ReadReturnsCorrectResultUsingGreedyConstructor([Greedy] Foo foo)
        {
            var json = JsonConvert.SerializeObject(foo);

            var actual = (Foo)JsonConstructorDeserializer.Read(typeof(Foo), json);

            Assert.Equal(foo.Age, actual.Age);
            Assert.Equal(foo.Name, actual.Name);
            Assert.Equal(foo.Date, actual.Date);
        }

        [Test]
        public void ReadDoesNotUseSameValueTwice(Bar bar)
        {
            var json = JsonConvert.SerializeObject(bar);

            var actual = (Bar)JsonConstructorDeserializer.Read(typeof(Bar), json);

            Assert.Null(actual.Name);
            Assert.Equal(bar.Name, actual.Name2);
        }

        [Test]
        public void ReadSetsDefaultValueToCtoArgumentWhenTypeCannotBeConvertedFromString(Baz baz)
        {
            var json = JsonConvert.SerializeObject(baz);
            var actual = (Baz)JsonConstructorDeserializer.Read(typeof(Baz), json);
            Assert.Null(actual.Type);
        }

        [Test]
        public void ReadSetsDefaultValueToPropertyWhenTypeCannotBeConvertedFromString(Abc baz)
        {
            var json = JsonConvert.SerializeObject(baz);
            var actual = (Abc)JsonConstructorDeserializer.Read(typeof(Abc), json);
            Assert.Null(actual.Type);
        }

        public class Foo
        {
            private readonly int age = -1;
            private readonly string name;

            public Foo(string name)
            {
                this.name = name;
            }

            public Foo(int age, string name)
            {
                this.age = age;
                this.name = name;
            }

            public string Name
            {
                get { return this.name; }
            }

            public int Age
            {
                get { return this.age; }
            }

            public int Date { get; set; }
        }

        public class Bar
        {
            private string name;

            public Bar(string name)
            {
                this.name = name;
            }

            public string Name
            {
                get;
                set;
            }

            public string Name2
            {
                get
                {
                    return this.name;
                }
            }
        }

        public class Baz
        {
            private readonly Type type;

            public Baz(Type type)
            {
                this.type = type;
            }

            public Type Type
            {
                get { return this.type; }
            }
        }

        public class Abc
        {
            public Type Type
            {
                get;
                set;
            }
        }
    }
}