namespace EFDataAccess
{
    using System.Data.Entity;

    public class ArticleHarborContext : DbContext
    {
        public ArticleHarborContext(
            IDatabaseInitializer<ArticleHarborContext> initializer)
        {
            Database.SetInitializer(initializer);
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleWord> ArticleWords { get; set; }
    }
}