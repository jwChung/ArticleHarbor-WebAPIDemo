namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using DomainModel;

    public class ArticleRepository : IArticleRepository
    {
        private readonly IDbSet<EFDataAccess.Article> articles;

        public ArticleRepository(IDbSet<EFDataAccess.Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            this.articles = articles;
        }

        public IDbSet<EFDataAccess.Article> Articles
        {
            get { return this.articles; }
        }

        public Article Insert(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var newArticle = this.articles.Add(
                new EFDataAccess.Article
                {
                    Provider = article.Provider,
                    No = article.No,
                    Subject = article.Subject,
                    Body = article.Body,
                    Date = article.Date,
                    Url = article.Url
                });

            return article.WithId(newArticle.Id);
        }
    }
}