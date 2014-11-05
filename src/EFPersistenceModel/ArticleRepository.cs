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

        public async Task<Article> SelectAsync(int id)
        {
            var efArticle = await Task.Run<EFArticle>(() => this.context.Articles.Find(id));
            return efArticle.ToArticle();
        }

        public Article Insert(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var efArticle = this.context.Articles.Add(article.ToEFArticle());
            this.context.SaveChanges();
            return efArticle.ToArticle();
        }

        public async Task<Article> InsertAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var efArticle = this.context.Articles.Add(article.ToEFArticle());
            await this.context.SaveChangesAsync();
            return efArticle.ToArticle();
        }
    }
}