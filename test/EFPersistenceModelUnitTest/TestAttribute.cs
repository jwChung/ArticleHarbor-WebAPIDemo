namespace ArticleHarbor
{
    using System.Collections.Generic;
    using System.Linq;
    using Jwc.Experiment;
    using Ploeh.AutoFixture;

    public class TestAttribute : DbContextTestAttribute
    {
        protected override IEnumerable<ICustomization> GetCustomizations(ITestMethodContext context)
        {
            return base.GetCustomizations(context);
        }
    }
}