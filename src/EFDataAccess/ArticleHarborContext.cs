namespace EFDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Globalization;

    public class ArticleHarborContext : DbContext
    {
        public ArticleHarborContext()
            : this(new ArticlesInitializer())
        {
        }

        public ArticleHarborContext(
            IDatabaseInitializer<ArticleHarborContext> initializer)
        {
            Database.SetInitializer(initializer);
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<ArticleWord> ArticleWords { get; set; }

        private class ArticlesInitializer : DropCreateDatabaseAlways<ArticleHarborContext>
        {
            public override void InitializeDatabase(ArticleHarborContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");

                ArticlesInitializer.InitializeArticles(context.Articles);
                base.InitializeDatabase(context);
            }

            private static void InitializeArticles(IDbSet<Article> articles)
            {
                for (int i = 0; i < 100; i++)
                {
                    articles.Add(
                        new Article
                        {
                            Id = -1,
                            No = i.ToString(CultureInfo.CurrentCulture),
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
}