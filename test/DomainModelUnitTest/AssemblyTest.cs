namespace DomainModelUnitTest
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
                Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("NHanNanum, Version=1.0.5012.25310, Culture=neutral, PublicKeyToken=null")
            };
            new RestrictiveReferenceAssertion(restrictiveReferences)
                .Verify(Assembly.Load("DomainModel"));
        }

        [Test]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            var indirectReferences = new[]
            {
                Assembly.Load("NHanNanum, Version=1.0.5012.25310, Culture=neutral, PublicKeyToken=null")
            };
            new IndirectReferenceAssertion(indirectReferences)
                .Verify(Assembly.Load("DomainModel"));
        }
    }
}
