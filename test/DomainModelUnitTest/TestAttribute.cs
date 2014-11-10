namespace ArticleHarbor
{
    using System.Collections.Generic;
    using DomainModel;
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
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            fixture.Customize<Article>(
                c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));

            var articleRepository = new FakeArticleRepository(fixture.Create<IList<Article>>());
            fixture.Inject<FakeRepositoryBase<Article>>(articleRepository);
            fixture.Inject<IRepository<Article>>(articleRepository);
            
            fixture.Customize(new TestParametersCustomization(
                context.ActualMethod.GetParameters()));
            return new TestFixture(fixture);
        }

        private class FakeArticleRepository : FakeRepositoryBase<Article>
        {
            public FakeArticleRepository(IList<Article> items)
                : base(items)
            {
            }

            public override object[] GetIndentity(Article item)
            {
                return new object[] { item.Id };
            }
        }
    }
}