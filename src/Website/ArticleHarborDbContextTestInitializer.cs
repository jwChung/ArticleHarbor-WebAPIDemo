using System;
using System.Data.Entity;
using EFDataAccess;
using Microsoft.AspNet.Identity;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1050:DeclareTypesInNamespaces", Justification = "This class needs to be shared with this persistence test project, which has a different namespace.")]
public class ArticleHarborDbContextTestInitializer : DropCreateDatabaseAlways<ArticleHarborDbContext>
{
    protected override void Seed(ArticleHarborDbContext context)
    {
        if (context == null)
            throw new ArgumentNullException("context");

        this.InitializeArticles(context.Articles);
        this.InitializeArticles(context.ArticleWords);
        this.InitializeUserRols(context.UserRoleManager);
        this.InitializeUsers(context.UserManager);
        base.Seed(context);
    }

    private void InitializeArticles(IDbSet<Article> articles)
    {
        articles.Add(new Article
        {
            Provider = "A",
            No = "1",
            Subject = "WordA1 WordA2 WordA3",
            Body = "Body 1",
            Date = DateTime.Now,
            Url = "http://abc1.com"
        });

        articles.Add(new Article
        {
            Provider = "B",
            No = "2",
            Subject = "WordB1 WordB2 WordB3",
            Body = "Body 2",
            Date = DateTime.Now,
            Url = "http://abc2.com"
        });

        articles.Add(new Article
        {
            Provider = "C",
            No = "3",
            Subject = "WordC1 WordC2 WordC3",
            Body = "Body 3",
            Date = DateTime.Now,
            Url = "http://abc3.com"
        });
    }

    private void InitializeArticles(IDbSet<ArticleWord> articleWords)
    {
        articleWords.Add(new ArticleWord
        {
            ArticleId = 1,
            Word = "WordA1"
        });

        articleWords.Add(new ArticleWord
        {
            ArticleId = 2,
            Word = "WordB1"
        });

        articleWords.Add(new ArticleWord
        {
            ArticleId = 3,
            Word = "WordC1"
        });
    }

    private void InitializeUserRols(UserRoleManager userRoleManager)
    {
        userRoleManager.Create(new UserRole { Name = "Administrator" });
        userRoleManager.Create(new UserRole { Name = "Author" });
        userRoleManager.Create(new UserRole { Name = "User" });
    }

    private void InitializeUsers(UserManager userManager)
    {
        userManager.Create(new User { UserName = "user1" }, "password1");
        userManager.Create(new User { UserName = "user2" }, "password2");
        userManager.Create(new User { UserName = "user3" }, "password3");

        userManager.AddToRoles(userManager.FindByName("user1").Id, "Administrator", "Author", "User");
        userManager.AddToRoles(userManager.FindByName("user2").Id, "Author", "User");
        userManager.AddToRoles(userManager.FindByName("user3").Id, "User");
    }
}