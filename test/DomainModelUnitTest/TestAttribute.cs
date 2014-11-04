using Jwc.Experiment;
using Jwc.Experiment.AutoFixture;
using Jwc.Experiment.Xunit;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

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
        var customization = new CompositeCustomization(
            new AutoMoqCustomization(),
            new TestParametersCustomization(context.ActualMethod.GetParameters()));

        return new TestFixture(new Fixture().Customize(customization));
    }
}