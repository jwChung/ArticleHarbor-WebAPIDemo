namespace DomainModel
{
    using System;

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

        public void Add(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            this.repository.Insert(article);
        }
    }
}