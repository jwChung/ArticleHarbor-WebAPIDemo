namespace EFPersistenceModelUnitTest
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
                Assembly.Load("EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                Assembly.Load("EFDataAccess"),
                Assembly.Load("DomainModel"),
            };
            new RestrictiveReferenceAssertion(restrictiveReferences)
                .Verify(Assembly.Load("EFPersistenceModel"));
        }

        [Test]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            var indirectReferences = new Assembly[]
            {
            };
            new IndirectReferenceAssertion(indirectReferences)
                .Verify(Assembly.Load("EFPersistenceModel"));
        }
    }
}
