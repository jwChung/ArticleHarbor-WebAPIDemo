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

        var dbContext = new ArticleHarborContext(new DropCreateDatabaseAlways<ArticleHarborContext>());
        fixture.Inject(dbContext);
        fixture.Inject(dbContext.Database.BeginTransaction());

        fixture.Customize(new TestParametersCustomization(
            context.ActualMethod.GetParameters()));

        return new TestFixture(fixture);
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