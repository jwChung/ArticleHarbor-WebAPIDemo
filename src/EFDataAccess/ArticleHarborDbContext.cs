namespace EFDataAccess
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ArticleHarborDbContext : IdentityDbContext<User>
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