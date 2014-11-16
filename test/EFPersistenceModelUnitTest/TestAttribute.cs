﻿namespace ArticleHarbor
{
    using EFDataAccess;
    using Jwc.Experiment;
    using Jwc.Experiment.AutoFixture;
    using Jwc.Experiment.Xunit;
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
            return new TestFixture(new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization(),
                new PersistanceModelCustomization(),
                new TestParametersCustomization(context.ActualMethod.GetParameters()))));
        }

        private class PersistanceModelCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<Article>(c => c.Without(x => x.Keywords));
                fixture.Customize<Keyword>(c => c.Without(x => x.Article));

                var dbContext = new ArticleHarborDbContext(new ArticleHarborDbContextTestInitializer());
                fixture.Inject(dbContext);
                fixture.Inject(dbContext.Database.BeginTransaction());
            }
        }
    }
}