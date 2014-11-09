namespace ArticleHarbor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment;
    using Jwc.Experiment.Idioms;
    using Jwc.Experiment.Xunit;
    using Ploeh.Albedo;

    public abstract class IdiomaticTest<T>
    {
        private readonly Properties<T> properties = new Properties<T>();
        private readonly Methods<T> methods = new Methods<T>();

        public Properties<T> Properties
        {
            get { return this.properties; }
        }

        public Methods<T> Methods
        {
            get { return this.methods; }
        }

        [Test]
        public IEnumerable<ITestCase> SutHasAppropriateGuards()
        {
            var members = typeof(T).GetIdiomaticMembers()
                .Except(this.ExceptToVerifyGuardClause());

            return TestCases.WithArgs(members).WithAuto<GuardClauseAssertion>()
                .Create((m, a) => a.Verify(m));
        }

        [Test]
        public IEnumerable<ITestCase> SutCorrectlyInitializesMembers()
        {
            var members = typeof(T).GetIdiomaticMembers()
                .Except(this.ExceptToVerifyInitialization());

            return TestCases.WithArgs(members).WithAuto<MemberInitializationAssertion>()
                .Create((m, a) => a.Verify(m));
        }

        protected virtual IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield break;
        }

        protected virtual IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield break;
        }
    }
}