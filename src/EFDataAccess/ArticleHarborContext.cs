namespace EFDataAccess
{
    using System.Data.Entity;

    public class ArticleHarborContext : DbContext
    {
        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleWord> ArticleWords { get; set; }
    }
}