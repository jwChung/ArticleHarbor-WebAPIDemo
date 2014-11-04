namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DomainModel;
    using Article = EFDataAccess.Article;

    public class ArticleRepository : IArticleRepository
    {
        private readonly IDbSet<Article> articles;

        public ArticleRepository(IDbSet<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            this.articles = articles;
        }

        public IDbSet<Article> Articles
        {
            get { return this.articles; }
        }

        public DomainModel.Article Insert(DomainModel.Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var newArticle = this.articles.Add(
                new Article
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

        public IEnumerable<DomainModel.Article> Select()
        {
            foreach (var article in this.articles.Take(50))
            {
                yield return new DomainModel.Article(
                    article.Provider,
                    article.No,
                    article.Subject,
                    article.Body,
                    article.Date,
                    article.Url)
                    .WithId(article.Id);
            }
        }
    }
}