namespace ArticleHarbor
{
    using EFDataAccess;
    using Ploeh.AutoFixture;

    public class DbContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var context = new ArticleHarborDbContext(
                new ArticleHarborDbContextTestInitializer());
            fixture.Inject(context);
            fixture.Inject(context.Database);
            fixture.Inject(context.Articles);
            fixture.Inject(context.Database.BeginTransaction());
        }
    }
}