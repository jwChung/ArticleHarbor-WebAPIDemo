namespace Jwc.CIBuild
{
    using Experiment;
    using Experiment.AutoFixture;
    using Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class TestAttribute : TestBaseAttribute
    {
        protected override ITestFixture Create(ITestMethodContext context)
        {
            var customization = new CompositeCustomization(
                new AutoMoqCustomization(),
                new TestParametersCustomization(context.ActualMethod.GetParameters()));

            return new TestFixture(new Fixture().Customize(customization));
        }
    }
}