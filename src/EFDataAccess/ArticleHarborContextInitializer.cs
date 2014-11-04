namespace EFDataAccess
{
    using System;
    using System.Data.Entity;

    public class ArticleHarborContextInitializer : DropCreateDatabaseAlways<ArticleHarborContext>
    {
        public override void InitializeDatabase(ArticleHarborContext context)
        {
            base.InitializeDatabase(context);
        }

        private void InitializeArticles(IDbSet<Article> articles)
        {
            for (int i = 0; i < 100; i++)
            {
                articles.Add(new Article
                {
                    No = i.ToString(),
                    Provider = "뉴스",
                    Subject = "제목" + i,
                    Body = "본문" + i,
                    Url = "url" + i,
                    Date = DateTime.Now
                });
            }
        }
    }
}