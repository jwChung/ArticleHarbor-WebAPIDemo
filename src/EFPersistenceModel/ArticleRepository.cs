namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
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

        public Task<IEnumerable<Article>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Article> SelectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Article Insert(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }
    }
}