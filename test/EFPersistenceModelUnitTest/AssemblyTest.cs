namespace ArticleHarbor.EFPersistenceModel
{
    using System.Reflection;
    using Jwc.Experiment.Idioms;

    public class AssemblyTest
    {
        [DbContextTest]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var restrictiveReferences = new[]
            {
                Assembly.Load("mscorlib"),
                Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                Assembly.Load("Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                Assembly.Load("ArticleHarbor.EFDataAccess"),
                Assembly.Load("ArticleHarbor.DomainModel"),
            };
            new RestrictiveReferenceAssertion(restrictiveReferences)
                .Verify(Assembly.Load("ArticleHarbor.EFPersistenceModel"));
        }

        [DbContextTest]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            var indirectReferences = new Assembly[]
            {
            };
            new IndirectReferenceAssertion(indirectReferences)
                .Verify(Assembly.Load("ArticleHarbor.EFPersistenceModel"));
        }
    }
}
