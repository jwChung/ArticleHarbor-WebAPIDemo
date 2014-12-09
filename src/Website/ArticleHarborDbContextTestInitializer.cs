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
                        Id = "user1",
                        UserName = "user1Name",
                        ApiKey = Guid.Parse("692c7798206844b88ba9a660e3374eef")
                    },
                    "password1");
                this.context.UserManager.Create(
                    new User
                    {
                        Id = "user2",
                        UserName = "user2Name",
                        ApiKey = Guid.Parse("232494f5670943dfac807226449fe795")
                    },
                    "password2");
                this.context.UserManager.Create(
                    new User
                    {
                        Id = "user3",
                        UserName = "user3Name",
                        ApiKey = Guid.Parse("930eaf281412423592f35104836f2771")
                    },
                    "password3");

                this.context.UserManager.AddToRoles("user1", "Administrator");
                this.context.UserManager.AddToRoles("user2", "Author");
                this.context.UserManager.AddToRoles("user3", "User");
            }

            public void InitializeArticles()
            {
                this.context.Articles.Add(new Article
                {
                    Provider = "A",
                    Guid = "1",
                    Subject = "WordA1 WordA2 WordA3",
                    Body = "Body 1",
                    Date = DateTime.Now,
                    Url = "http://abc1.com",
                    UserId = "user1"
                });

                this.context.Articles.Add(new Article
                {
                    Provider = "B",
                    Guid = "2",
                    Subject = "WordB1 WordB2 WordB3",
                    Body = "Body 2",
                    Date = DateTime.Now,
                    Url = "http://abc2.com",
                    UserId = "user2"
                });

                this.context.Articles.Add(new Article
                {
                    Provider = "C",
                    Guid = "3",
                    Subject = "WordC1 WordC2 WordC3",
                    Body = "Body 3",
                    Date = DateTime.Now,
                    Url = "http://abc3.com",
                    UserId = "user2"
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
                    UserId = "user1",
                    ArticleId = 1,
                });
                this.context.Bookmarks.Add(new Bookmark
                {
                    UserId = "user1",
                    ArticleId = 2,
                });
                this.context.Bookmarks.Add(new Bookmark
                {
                    UserId = "user2",
                    ArticleId = 3,
                });
            }
        }
    }
}