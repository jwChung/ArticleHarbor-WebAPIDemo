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
            var parameters = context.ActualMethod.GetParameters();

            return new TestFixture(new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization(),
                new DomainModelCustomization(),
                new TestParametersCustomization(parameters))));
        }

        private class DomainModelCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<Article>(c => c.FromFactory(
                    new MethodInvoker(new GreedyConstructorQuery())));

                var articleRepository = new FakeArticleRepository(
                    fixture.Create<Generator<Article>>());
                fixture.Inject<FakeRepositoryBase<Article>>(articleRepository);
                fixture.Inject<IRepository<Article>>(articleRepository);
            }
        }

        private class FakeArticleRepository : FakeRepositoryBase<Article>
        {
            public FakeArticleRepository(Generator<Article> generator)
                : base(generator)
            {
            }

            public override object[] GetIndentity(Article item)
            {
                return new object[] { item.Id };
            }
        }
    }
}