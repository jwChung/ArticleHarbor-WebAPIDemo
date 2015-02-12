namespace ArticleHarbor.Website
{
    using System.Security.Principal;
    using Jwc.Experiment;
    using Jwc.Experiment.AutoFixture;
    using Jwc.Experiment.Xunit;
    using Jwc.Funz;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class TestAttribute : TestBaseAttribute
    {
        private readonly RunOn runOn;

        public TestAttribute()
            : this(RunOn.Local)
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
            var customization = new CompositeCustomization(
                new AutoMoqCustomization(),
                new ContainerCustomization(),
                new TestParametersCustomization(context.ActualMethod.GetParameters()));

            return new TestFixture(new Fixture().Customize(customization));
        }

        private class ContainerCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() =>
                {
                    var container = new Container();
                    container.Register(c => fixture.Create<IPrincipal>());
                    return container;
                });
            }
        }
    }
}