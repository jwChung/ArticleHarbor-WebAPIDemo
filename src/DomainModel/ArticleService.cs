namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository repository;

        public ArticleService(IArticleRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.repository = repository;
        }

        public IArticleRepository Repository
        {
            get { return this.repository; }
        }

        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.repository.SelectAsync();
        }

        public Task<Article> AddOrModifyAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var oldArticle = this.repository.Select(article.Id);
            if (oldArticle == null)
                return this.repository.InsertAsync(article);

            this.repository.Update(article);
            return Task.FromResult(article);
        }

        public void Remove(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }
    }
}