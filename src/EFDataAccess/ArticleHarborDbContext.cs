namespace ArticleHarbor.EFDataAccess
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ArticleHarborDbContext : IdentityDbContext<User>
    {
        private readonly UserManager userManager;
        private readonly UserRoleManager userRoleManager;
        private readonly UserStore<User> userStore;
        private readonly RoleStore<UserRole> roleStore;

        public ArticleHarborDbContext(
            IDatabaseInitializer<ArticleHarborDbContext> initializer)
            : base("ArticleHarborDbContext")
        {
            Database.SetInitializer(initializer);

            this.userStore = new UserStore<User>(this);
            this.roleStore = new RoleStore<UserRole>(this);
            this.userManager = new UserManager(this.userStore);
            this.userRoleManager = new UserRoleManager(this.roleStore);
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleWord> ArticleWords { get; set; }

        public IDbSet<Bookmark> Bookmarks { get; set; }

        public UserManager UserManager
        {
            get { return this.userManager; }
        }

        public UserRoleManager UserRoleManager
        {
            get { return this.userRoleManager; }
        }
    }
}