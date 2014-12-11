namespace ArticleHarbor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DomainModel;
    using DomainModel.Queries;
    using Jwc.Experiment;
    using Jwc.Experiment.AutoFixture;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Kernel;

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
            var parameters = context.ActualMethod.GetParameters();

            return new TestFixture(new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization(),
                new ArticleCustomization(),
                new PermissionsCustomization(),
                new NameParameterCustomization(),
                new TestParametersCustomization(parameters))));
        }

        private class ArticleCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<Article>(
                    c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
            }
        }

        private class PermissionsCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var max = Enum.GetValues(typeof(Permissions)).Cast<int>().Max();
                var random = new Random();
                fixture.Customize<Permissions>(
                    c => c.FromFactory(() => (Permissions)random.Next(0, max + 1)));
            }
        }

        private class NameParameterCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customizations.Add(new NameParameterSpecimenBuilder());
            }

            private class NameParameterSpecimenBuilder : ISpecimenBuilder
            {
                public object Create(object request, ISpecimenContext context)
                {
                    var parameter = request as ParameterInfo;
                    if (parameter == null
                        || parameter.Member.ReflectedType != typeof(Parameter)
                        || parameter.ParameterType != typeof(string))
                        return new NoSpecimen(request);

                    return "@" + context.Resolve(
                        new SeededRequest(parameter.ParameterType, parameter.Name));
                }
            }
        }
    }
}