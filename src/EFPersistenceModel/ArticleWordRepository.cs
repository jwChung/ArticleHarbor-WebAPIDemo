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

            this.context.ArticleWords.Add(articleWord.ToEArticleWord());
        }

        public ArticleWord Select(string word, int articleId)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var efArticleWord = this.context.ArticleWords.Find(word, articleId);
            return efArticleWord == null ? null : efArticleWord.ToArticleWord();
        }
    }
}