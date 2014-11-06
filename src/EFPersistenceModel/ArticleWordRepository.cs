namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
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

        public void Insert(DomainModel.ArticleWord articleWord)
        {
            if (articleWord == null)
                throw new ArgumentNullException("articleWord");

            if (this.Select(articleWord.ArticleId, articleWord.Word) != null)
                return;

            this.context.ArticleWords.Add(articleWord.ToPersistence());
        }

        public DomainModel.ArticleWord Select(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var articleWord = this.context.ArticleWords.Find(articleId, word);
            return articleWord == null ? null : articleWord.ToDomain();
        }

        public void Delete(int articleId)
        {
            var articleWords = this.context.ArticleWords
                .Where(x => x.ArticleId == articleId).ToArray();

            foreach (var articleWord in articleWords)
                this.context.ArticleWords.Remove(articleWord);
        }
    }
}