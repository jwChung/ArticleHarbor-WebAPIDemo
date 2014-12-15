namespace ArticleHarbor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using DomainModel.Repositories;
    using Jwc.Experiment;
    using Jwc.Experiment.AutoFixture;
    using Jwc.Experiment.Xunit;
    using Microsoft.Owin;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Kernel;
    using WebApiPresentationModel.Models;

    public class TestAttribute : TestBaseAttribute
    {
        private readonly RunOn runOn;

        public TestAttribute()
            : this(RunOn.Any)
        {
        }

        public TestAttribute(RunOn runOn)
        {
            this.runOn = runOn;
        }

        public RunOn RunOn
        {
            get { return this.runOn; }
        }

        public override string Skip
        {
            get
            {
#if !CI
                if (base.Skip == null && this.runOn == RunOn.CI)
                    return "Run this test only on CI server.";
#else
            if (base.Skip == null && this.runOn == RunOn.Local)
                return "Run this test only on Local machine.";
#endif

                return base.Skip;
            }

            set
            {
                base.Skip = value;
            }
        }

        protected override ITestFixture Create(ITestMethodContext context)
        {
            return new TestFixture(new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization(),
                new WebApiPresentationModelCustomization(),
                new TestParametersCustomization(context.ActualMethod.GetParameters()))));
        }

        private class WebApiPresentationModelCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customizations.Add(
                    new OmitAutoPropertiesBuilder(
                        typeof(X509Certificate2),
                        typeof(ArticleQueryViewModel)));

                fixture.Register<Stream>(() => new MemoryStream());

                fixture.Register(() => CultureInfo.CurrentCulture);

                fixture.Register(() => new PathString("/" + fixture.Create<string>()));

                fixture.Register(() => new Lazy<IUnitOfWork>(() => fixture.Create<IUnitOfWork>()));
            }
        }

        private class OmitAutoPropertiesBuilder : ISpecimenBuilder
        {
            private readonly Type[] targets;

            public OmitAutoPropertiesBuilder(params Type[] targets)
            {
                this.targets = targets;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var type = request as Type;
                if (type == null
                    || type.IsAbstract
                    || this.targets.All(t => !t.IsAssignableFrom(type)))
                    return new NoSpecimen(request);

                return new MethodInvoker(new ModestConstructorQuery())
                    .Create(request, context);
            }
        }
    }
}