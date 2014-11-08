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
    using Article = DomainModel.Article;

    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleHarborDbContext context;

        public ArticleRepository(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public async Task<IEnumerable<Article>> SelectAsync()
        {
            var result = await this.context.Articles.Take(50).ToArrayAsync();
            return result.Select(x => x.ToDomain());
        }

        public Article Select(int id)
        {
            var article = this.context.Articles.Find(id);
            return article == null ? null : article.ToDomain();
        }

        public Task<Article> InsertAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.InsertAsyncImpl(article);
        }

        public void Update(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var persistence = this.context.Articles.Find(article.Id);
            if (persistence != null)
            {
                ((IObjectContextAdapter)this.context).ObjectContext.Detach(persistence);
                this.context.Entry(article.ToPersistence()).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var article = this.context.Articles.Find(id);
            if (article == null)
                return;

            this.context.Articles.Remove(article);
        }

        public Task UpdateAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Article> InsertAsyncImpl(Article article)
        {
            if (this.Select(article.Id) != null)
                return article;

            var persistence = this.context.Articles.Add(article.ToPersistence());
            await this.context.SaveChangesAsync();
            return persistence.ToDomain();
        }
    }
}