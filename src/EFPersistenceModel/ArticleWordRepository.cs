namespace EFPersistenceModel
{
    using System;
    using DomainModel;
    using EFDataAccess;

    public class ArticleWordRepository : IArticleWordRepository
    {
        private readonly ArticleHarborContext context;

        public ArticleWordRepository(ArticleHarborContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborContext Context
        {
            get { return this.context; }
        }

        public void Insert(ArticleWord articleWord)
        {
            if (articleWord == null)
                throw new ArgumentNullException("articleWord");

            throw new System.NotImplementedException();
        }
    }
}