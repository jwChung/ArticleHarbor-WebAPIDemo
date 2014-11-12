namespace ArticleHarbor
{
    using System;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using EFDataAccess;
    using Microsoft.AspNet.Identity;

    [SuppressMessage("Microsoft.Design", "CA1050:DeclareTypesInNamespaces", Justification = "This class needs to be shared with this persistence test project, which has a different namespace.")]
    public class ArticleHarborDbContextTestInitializer : DropCreateDatabaseAlways<ArticleHarborDbContext>
    {
        protected override void Seed(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var initializer = new DbContextTestInitializer(context);
            initializer.InitializeUserRols();
            initializer.InitializeUsers();
            initializer.InitializeArticles();
            initializer.InitializeKeywords();
            initializer.InitializeBookmarks();

            base.Seed(context);
        }

        private class DbContextTestInitializer
        {
            private readonly ArticleHarborDbContext context;

            public DbContextTestInitializer(ArticleHarborDbContext context)
            {
                this.context = context;
            }

            public void InitializeUserRols()
            {
                this.context.UserRoleManager.Create(new UserRole { Name = "Administrator" });
                this.context.UserRoleManager.Create(new UserRole { Name = "Author" });
                this.context.UserRoleManager.Create(new UserRole { Name = "User" });
            }

            public void InitializeUsers()
            {
                this.context.UserManager.Create(
                    new User
                    {
                        UserName = "user1",
                        ApiKey = Guid.Parse("692c7798206844b88ba9a660e3374eef")
                    },
                    "password1");
                this.context.UserManager.Create(
                    new User
                    {
                        UserName = "user2",
                        ApiKey = Guid.Parse("232494f5670943dfac807226449fe795")
                    },
                    "password2");
                this.context.UserManager.Create(
                    new User
                    {
                        UserName = "user3",
                        ApiKey = Guid.Parse("930eaf281412423592f35104836f2771")
                    },
                    "password3");

                this.context.UserManager.AddToRoles(
                    this.context.UserManager.FindByName("user1").Id, "Administrator");
                this.context.UserManager.AddToRoles(
                    this.context.UserManager.FindByName("user2").Id, "Author");
                this.context.UserManager.AddToRoles(
                    this.context.UserManager.FindByName("user3").Id, "User");
            }

            public void InitializeArticles()
            {
                this.context.Articles.Add(new Article
                {
                    Provider = "A",
                    No = "1",
                    Subject = "WordA1 WordA2 WordA3",
                    Body = "Body 1",
                    Date = DateTime.Now,
                    Url = "http://abc1.com",
                    UserId = this.context.Users.Local[0].Id
                });

                this.context.Articles.Add(new Article
                {
                    Provider = "B",
                    No = "2",
                    Subject = "WordB1 WordB2 WordB3",
                    Body = "Body 2",
                    Date = DateTime.Now,
                    Url = "http://abc2.com",
                    UserId = this.context.Users.Local[1].Id
                });

                this.context.Articles.Add(new Article
                {
                    Provider = "C",
                    No = "3",
                    Subject = "WordC1 WordC2 WordC3",
                    Body = "Body 3",
                    Date = DateTime.Now,
                    Url = "http://abc3.com",
                    UserId = this.context.Users.Local[1].Id
                });
            }

            public void InitializeKeywords()
            {
                this.context.Keywords.Add(new Keyword
                {
                    ArticleId = 1,
                    Word = "WordA1"
                });

                this.context.Keywords.Add(new Keyword
                {
                    ArticleId = 2,
                    Word = "WordB1"
                });

                this.context.Keywords.Add(new Keyword
                {
                    ArticleId = 3,
                    Word = "WordC1"
                });
            }

            public void InitializeBookmarks()
            {
                this.context.Bookmarks.Add(new Bookmark
                {
                    UserId = this.context.Users.Local[0].Id,
                    ArticleId = 1,
                });
                this.context.Bookmarks.Add(new Bookmark
                {
                    UserId = this.context.Users.Local[0].Id,
                    ArticleId = 2,
                });
                this.context.Bookmarks.Add(new Bookmark
                {
                    UserId = this.context.Users.Local[1].Id,
                    ArticleId = 3,
                });
            }
        }
    }
}