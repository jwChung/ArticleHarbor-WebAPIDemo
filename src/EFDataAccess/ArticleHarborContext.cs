namespace EFDataAccess
{
    using System.Data.Entity;

    public class ArticleHarborContext : DbContext
    {
        public ArticleHarborContext()
            : this(new NullDatabaseInitializer<ArticleHarborContext>())
        {
        }

        public ArticleHarborContext(
            IDatabaseInitializer<ArticleHarborContext> initializer)
        {
            Database.SetInitializer(initializer);
        }

        public IDbSet<EFArticle> Articles { get; set; }

        public IDbSet<EFArticleWord> ArticleWords { get; set; }
    }
}