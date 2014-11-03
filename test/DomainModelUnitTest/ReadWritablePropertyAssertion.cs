namespace DomainModelUnitTest
{
    using System;
    using System.Reflection;
    using Jwc.Experiment;
    using Jwc.Experiment.Idioms;
    using Xunit;

    public class ReadWritablePropertyAssertion : IdiomaticAssertion
    {
        private readonly ITestFixture fixture;

        public ReadWritablePropertyAssertion(ITestFixture fixture)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");

            this.fixture = fixture;
        }

        public override void Verify(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            var value = fixture.Create(property.PropertyType);
            var target = fixture.Create(property.ReflectedType);
            property.SetValue(target, value);
            var atcual = property.GetValue(target);

            Assert.Equal(value, atcual);
        }
    }
}