namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;

    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleHarborContext context;

        public ArticleRepository(ArticleHarborContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborContext Context
        {
            get { return this.context; }
        }

        public async Task<IEnumerable<Article>> SelectAsync()
        {
            var result = await this.context.Articles.Take(50).ToArrayAsync();
            return result.Select(x => x.ToArticle());
        }

        public Article Select(int id)
        {
            var efArticle = this.context.Articles.Find(id);
            return efArticle == null ? null : efArticle.ToArticle();
        }

        public async Task<Article> InsertAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var efArticle = this.context.Articles.Add(article.ToEFArticle());
            await this.context.SaveChangesAsync();
            var test = this.context.Entry(efArticle).State;
            return efArticle.ToArticle();
        }

        public void Update(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var efArticle = this.context.Articles.Find(article.Id);
            if (efArticle != null)
            {
                ((IObjectContextAdapter)this.context).ObjectContext.Detach(efArticle);
                this.context.Entry(article.ToEFArticle()).State = EntityState.Modified;
            }
        }
    }
}