namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;

    public class ArticleRepository2 : IArticleRepository
    {
        private readonly IDbSet<EFArticle> efArticles;

        public ArticleRepository2(IDbSet<EFArticle> efArticles)
        {
            if (efArticles == null)
                throw new ArgumentNullException("efArticles");

            this.efArticles = efArticles;
        }

        public IDbSet<EFArticle> EFArticles
        {
            get { return this.efArticles; }
        }

        public async Task<IEnumerable<Article>> SelectAsync()
        {
            await this.efArticles.Take(50).LoadAsync();
            return this.efArticles.ToArray().Select(efa => efa.ToArticle());
        }

        public async Task<Article> SelectAsync(int id)
        {
            var result = await Task.Run<EFArticle>(() => this.efArticles.Find(id))
                .ConfigureAwait(false);

            return result != null ? result.ToArticle() : null;
        }

        public Article Insert(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var efArticle = this.efArticles.Add(article.ToEFArticle());
            return article.WithId(efArticle.Id);
        }
    }
}