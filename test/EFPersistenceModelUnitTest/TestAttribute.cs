using System;
using System.Data.Entity;
using System.Linq;
using EFDataAccess;
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

        fixture.Customize<Article>(c => c.Without(x => x.ArticleWords));
        fixture.Customize<ArticleWord>(c => c.Without(x => x.Article));

        var dbContext = new ArticleHarborContext(new TestDatabaseInitializer(fixture));
        fixture.Inject(dbContext);
        fixture.Inject(dbContext.Database.BeginTransaction());

        fixture.Customize(new TestParametersCustomization(
            context.ActualMethod.GetParameters()));
        return new TestFixture(fixture);
    }

    private class TestDatabaseInitializer : DropCreateDatabaseAlways<ArticleHarborContext>
    {
        private readonly IFixture fixture;

        public TestDatabaseInitializer(IFixture fixture)
        {
            this.fixture = fixture;
        }

        public override void InitializeDatabase(ArticleHarborContext context)
        {
            var articles = this.fixture.Create<Article[]>();
            foreach (var article in articles)
                context.Articles.Add(article);

            var articleWords = this.fixture.Create<ArticleWord[]>();
            for (int i = 0; i < articleWords.Length; i++)
            {
                articleWords[i].ArticleId = articles[i].Id;
                context.ArticleWords.Add(articleWords[i]);
            }

            base.InitializeDatabase(context);
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