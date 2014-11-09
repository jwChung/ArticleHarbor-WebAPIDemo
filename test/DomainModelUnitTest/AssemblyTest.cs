namespace ArticleHarbor.DomainModel
{
    using System.Reflection;
    using Jwc.Experiment.Idioms;

    public class AssemblyTest
    {
        [Test]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var restrictiveReferences = new[]
            {
                Assembly.Load("mscorlib"),
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("NHanNanum")
            };
            new RestrictiveReferenceAssertion(restrictiveReferences)
                .Verify(Assembly.Load("ArticleHarbor.DomainModel"));
        }

        [Test]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            var indirectReferences = new[]
            {
                Assembly.Load("NHanNanum")
            };
            new IndirectReferenceAssertion(indirectReferences)
                .Verify(Assembly.Load("ArticleHarbor.DomainModel"));
        }
    }
}
