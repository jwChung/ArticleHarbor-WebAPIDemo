namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jwc.Experiment;
    using Jwc.Experiment.Idioms;
    using Jwc.Experiment.Xunit;
    using Newtonsoft.Json;
    using Ploeh.AutoFixture.Xunit;
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
        public void DeserializeReturnsCorrectResultUsingModestConstructor(TypeA typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);
            
            var actual = (TypeA)JsonConstructorDeserializer.Deserialize(typeof(TypeA), json);

            Assert.Equal(typeA.Name, actual.Name);
            Assert.Equal(-1, actual.Age);
            Assert.Equal(typeA.Value, actual.Value);
        }

        [Test]
        public void DeserializeReturnsCorrectResultUsingGreedyConstructor([Greedy] TypeA typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);

            var actual = (TypeA)JsonConstructorDeserializer.Deserialize(typeof(TypeA), json);

            Assert.Equal(typeA.Age, actual.Age);
            Assert.Equal(typeA.Name, actual.Name);
            Assert.Equal(typeA.Value, actual.Value);
        }

        [Test]
        public void DeserializeDoesNotUseSameValueTwice(TypeB typeB)
        {
            var json = JsonConvert.SerializeObject(typeB);

            var actual = (TypeB)JsonConstructorDeserializer.Deserialize(typeof(TypeB), json);

            Assert.Null(actual.Name);
            Assert.Equal(typeB.Name, actual.Name2);
        }

        [Test]
        public void DeserializeSetsDefaultValueToCtorArgumentWhenTypeCannotBeConvertedFromString(TypeC typeC)
        {
            var json = JsonConvert.SerializeObject(typeC);
            var actual = (TypeC)JsonConstructorDeserializer.Deserialize(typeof(TypeC), json);
            Assert.Null(actual.TargetType);
        }

        [Test]
        public void DeserializeSetsDefaultValueToPropertyWhenTypeCannotBeConvertedFromString(TypeD typeD)
        {
            var json = JsonConvert.SerializeObject(typeD);
            var actual = (TypeD)JsonConstructorDeserializer.Deserialize(typeof(TypeD), json);
            Assert.Null(actual.TargetType);
        }

        [Test]
        public void DeserializeCorrectlyDeserializesRecursiveObject(TypeE typeE)
        {
            var json = JsonConvert.SerializeObject(typeE);

            var actual = (TypeE)JsonConstructorDeserializer.Deserialize(typeof(TypeE), json);

            Assert.Equal(typeE.Value, actual.Value);
            Assert.Equal(typeE.TypeA.Name, actual.TypeA.Name);
            Assert.Equal(-1, actual.TypeA.Age);
        }

        [Test]
        public void DeserializeArrayReturnsCorrectResult(TypeA[] typeAs)
        {
            var json = JsonConvert.SerializeObject(typeAs);

            var actual = (TypeA[])JsonConstructorDeserializer.Deserialize(typeof(TypeA[]), json);

            Assert.Equal(typeAs.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.Equal(typeAs[i].Name, actual[i].Name);
                Assert.Equal(-1, actual[i].Age);
                Assert.Equal(typeAs[i].Value, actual[i].Value);    
            }
        }

        [Test]
        public void DeserializeEnumerableReturnsCorrectResult(IEnumerable<TypeA> typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);

            var actual = ((IEnumerable<TypeA>)JsonConstructorDeserializer.Deserialize(
                typeof(IEnumerable<TypeA>), json)).ToArray();

            var fooArray = typeA.ToArray();
            Assert.Equal(fooArray.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.Equal(fooArray[i].Name, actual[i].Name);
                Assert.Equal(-1, actual[i].Age);
                Assert.Equal(fooArray[i].Value, actual[i].Value);
            }
        }

        [Test]
        public void DeserializeListReturnsCorrectResult(List<TypeA> typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);

            var actual = (List<TypeA>)JsonConstructorDeserializer.Deserialize(typeof(List<TypeA>), json);

            Assert.Equal(typeA.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(typeA[i].Name, actual[i].Name);
                Assert.Equal(-1, actual[i].Age);
                Assert.Equal(typeA[i].Value, actual[i].Value);
            }
        }

        [Test]
        public void DeserializeIListReturnsCorrectResult(IList<TypeA> typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);

            var actual = (IList<TypeA>)JsonConstructorDeserializer.Deserialize(typeof(IList<TypeA>), json);

            Assert.Equal(typeA.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(typeA[i].Name, actual[i].Name);
                Assert.Equal(-1, actual[i].Age);
                Assert.Equal(typeA[i].Value, actual[i].Value);
            }
        }

        [Test]
        public void DeserializeRecursiveArrayReturnsCorrectResult(TypeA[][] typeAs)
        {
            var json = JsonConvert.SerializeObject(typeAs);
            
            var actual = (TypeA[][])JsonConstructorDeserializer.Deserialize(typeof(TypeA[][]), json);

            Assert.NotNull(actual);
            foreach (TypeA[] arry in actual)
                Assert.NotNull(arry);
        }

        [Test]
        public void DeserializeCorrectionReturnsCorrectResult(ICollection<TypeA> typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);
            var actual = (ICollection<TypeA>)JsonConstructorDeserializer
                .Deserialize(typeof(ICollection<TypeA>), json);
            Assert.Equal(typeA.Count, actual.Count);
        }

        [Test]
        public void DeserializeWithInvalidTypeThrows(TypeA typeA)
        {
            var json = JsonConvert.SerializeObject(typeA);

            Assert.Throws<ArgumentException>(
                () => JsonConstructorDeserializer.Deserialize(typeof(TypeE), json));
        }

        [Test]
        public void DeserializeWithInvalidArrayTypeThrows(TypeA[] typeAs)
        {
            var json = JsonConvert.SerializeObject(typeAs);

            Assert.Throws<NotSupportedException>(
                () => JsonConstructorDeserializer.Deserialize(typeof(Dictionary<string, TypeA>), json));
        }

        public class TypeA
        {
            private readonly int age = -1;
            private readonly string name;

            public TypeA(string name)
            {
                this.name = name;
            }

            public TypeA(int age, string name)
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

            public int Value { get; set; }
        }

        public class TypeB
        {
            private string name;

            public TypeB(string name)
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

        public class TypeC
        {
            private readonly Type targetType;

            public TypeC(Type targetType)
            {
                this.targetType = targetType;
            }

            public Type TargetType
            {
                get { return this.targetType; }
            }
        }

        public class TypeD
        {
            public Type TargetType
            {
                get;
                set;
            }
        }

        public class TypeE
        {
            private readonly string value;
            private readonly TypeA typeA;

            public TypeE(string value, TypeA typeA)
            {
                this.value = value;
                this.typeA = typeA;
            }

            public string Value
            {
                get { return this.value; }
            }

            public TypeA TypeA
            {
                get { return this.typeA; }
            }
        }
    }
}