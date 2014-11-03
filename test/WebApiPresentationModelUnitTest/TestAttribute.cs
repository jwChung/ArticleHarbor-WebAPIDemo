namespace WebApiPresentationModelUnitTest
{
    using Jwc.Experiment;
    using Jwc.Experiment.AutoFixture;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class TestAttribute : TestBaseAttribute
    {
        protected override ITestFixture Create(ITestMethodContext context)
        {
            var customization = new CompositeCustomization(
                new AutoMoqCustomization(),
                new TestParametersCustomization(context.ActualMethod.GetParameters()),
                new OmitAutoPropertiesCustomization());

            return new TestFixture(new Fixture().Customize(customization));
        }
    }
}