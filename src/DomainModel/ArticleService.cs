namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class ArticleService
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

        public Task AddAsync(Article article)
        {
            return Task.Run(() => this.repository.Insert(article));
        }
    }
}