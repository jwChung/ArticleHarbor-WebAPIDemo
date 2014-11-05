namespace WebApiPresentationModelUnitTest
{
    using System.Collections.Generic;
    using Jwc.Experiment;
    using Jwc.Experiment.Idioms;
    using Jwc.Experiment.Xunit;
    using Newtonsoft.Json;
    using Ploeh.AutoFixture.Xunit;
    using WebApiPresentationModel;
    using Xunit;

    public class JsonConstructorFormatterTest
    {
        [Test]
        public IEnumerable<ITestCase> SutHasAppropriateGuards()
        {
            var members = typeof(JsonConstructorFormatter).GetIdiomaticMembers();
            return TestCases.WithArgs(members).WithAuto<GuardClauseAssertion>()
                .Create((m, a) => a.Verify(m));
        }

        [Test]
        public void ReadReturnsCorrectResultUsingModestConstructor(Foo foo)
        {
            var json = JsonConvert.SerializeObject(foo);
            
            var actual = (Foo)JsonConstructorFormatter.Read(typeof(Foo), json);

            Assert.Equal(foo.Name, actual.Name);
            Assert.Equal(-1, actual.Age);
            Assert.Equal(foo.Date, actual.Date);
        }

        [Test]
        public void ReadReturnsCorrectResultUsingGreedyConstructor([Greedy] Foo foo)
        {
            var json = JsonConvert.SerializeObject(foo);

            var actual = (Foo)JsonConstructorFormatter.Read(typeof(Foo), json);

            Assert.Equal(foo.Age, actual.Age);
            Assert.Equal(foo.Name, actual.Name);
            Assert.Equal(foo.Date, actual.Date);
        }

        [Test]
        public void ReadDoesNotUseSameValueTwice(Bar bar)
        {
            var json = JsonConvert.SerializeObject(bar);

            var actual = (Bar)JsonConstructorFormatter.Read(typeof(Bar), json);

            Assert.Null(actual.Name);
            Assert.Equal(bar.Name, actual.Name2);
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
                    return name;
                }
            }
        }
    }
}