namespace WebApiPresentationModelUnitTest
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
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                Assembly.Load("System.Web.Http, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                Assembly.Load("System.Net.Http.Formatting, Version=5.2.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                Assembly.Load("Newtonsoft.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed"),
                Assembly.Load("DomainModel"),
            };
            new RestrictiveReferenceAssertion(restrictiveReferences)
                .Verify(Assembly.Load("WebApiPresentationModel"));
        }

        [Test]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            var indirectReferences = new Assembly[]
            {
            };
            new IndirectReferenceAssertion(indirectReferences)
                .Verify(Assembly.Load("WebApiPresentationModel"));
        }
    }
}
