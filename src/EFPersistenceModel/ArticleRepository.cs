namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DomainModel;
    using EFDataAccess;

    public class ArticleRepository : IArticleRepository
    {
        private readonly IDbSet<EFArticle> efArticles;

        public ArticleRepository(IDbSet<EFArticle> efArticles)
        {
            if (efArticles == null)
                throw new ArgumentNullException("efArticles");

            this.efArticles = efArticles;
        }

        public IDbSet<EFArticle> EFArticles
        {
            get { return this.efArticles; }
        }

        public Article Insert(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var efArticle = this.efArticles.Add(
                new EFArticle
                {
                    Provider = article.Provider,
                    No = article.No,
                    Subject = article.Subject,
                    Body = article.Body,
                    Date = article.Date,
                    Url = article.Url
                });

            return article.WithId(efArticle.Id);
        }

        public IEnumerable<Article> Select()
        {
            foreach (var article in this.efArticles.Take(50))
            {
                yield return new Article(
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