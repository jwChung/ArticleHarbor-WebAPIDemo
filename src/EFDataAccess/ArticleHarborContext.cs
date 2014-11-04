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

        public IDbSet<EFArticle> Articles { get; set; }

        public IDbSet<EFArticleWord> ArticleWords { get; set; }

        private class ArticlesInitializer : DropCreateDatabaseAlways<ArticleHarborContext>
        {
            public override void InitializeDatabase(ArticleHarborContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");

                ArticlesInitializer.InitializeArticles(context.Articles);
                base.InitializeDatabase(context);
            }

            private static void InitializeArticles(IDbSet<EFArticle> articles)
            {
                for (int i = 0; i < 100; i++)
                {
                    articles.Add(
                        new EFArticle
                        {
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