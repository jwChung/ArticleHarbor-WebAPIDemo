using System;
using System.Data.Entity;
using EFDataAccess;

public class ArticleHarborDbContextTestInitializer : DropCreateDatabaseAlways<ArticleHarborDbContext>
{
    public override void InitializeDatabase(ArticleHarborDbContext context)
    {
        this.InitializeArticles(context.Articles);
        this.InitializeArticles(context.ArticleWords);
        base.InitializeDatabase(context);
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
}