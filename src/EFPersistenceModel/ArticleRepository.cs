namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task<IEnumerable<Article>> SelectAsync()
        {
            await this.efArticles.Take(50).LoadAsync();
            return this.efArticles.ToArray().Select(
                efa => new Article(
                    efa.Provider,
                    efa.No,
                    efa.Subject,
                    efa.Body,
                    efa.Date,
                    efa.Url)
                    .WithId(efa.Id));
        }
    }
}