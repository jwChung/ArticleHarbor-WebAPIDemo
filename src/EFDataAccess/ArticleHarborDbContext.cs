namespace EFDataAccess
{
    using System.Data.Entity;

    public class ArticleHarborDbContext : DbContext
    {
        public ArticleHarborDbContext(
            IDatabaseInitializer<ArticleHarborDbContext> initializer)
        {
            Database.SetInitializer(initializer);
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleWord> ArticleWords { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}