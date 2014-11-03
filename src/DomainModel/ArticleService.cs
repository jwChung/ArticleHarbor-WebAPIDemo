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

        public async Task AddAsync(Article article)
        {
            await Task.Run(() => this.repository.Insert(article)).ConfigureAwait(false);
        }
    }
}