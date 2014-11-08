namespace EFDataAccess
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ArticleHarborDbContext : IdentityDbContext<User>
    {
        private readonly UserManager userManager;
        private readonly UserRoleManager userRoleManager;

        public ArticleHarborDbContext(
            IDatabaseInitializer<ArticleHarborDbContext> initializer)
        {
            Database.SetInitializer(initializer);

            this.userManager = new UserManager(new UserStore<User>(this));
            this.userRoleManager = new UserRoleManager(new RoleStore<UserRole>(this));
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleWord> ArticleWords { get; set; }

        public UserManager UserManager
        {
            get { return this.userManager; }
        }

        public UserRoleManager UserRoleManager
        {
            get { return this.userRoleManager; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}