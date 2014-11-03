﻿namespace WebApiPresentationModelUnitTest
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
                Assembly.Load("System.Web.Http, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
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
