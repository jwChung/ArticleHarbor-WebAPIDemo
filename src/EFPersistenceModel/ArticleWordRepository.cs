namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DomainModel;
    using EFDataAccess;
    using ArticleWord = DomainModel.ArticleWord;

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

            this.context.ArticleWords.Add(articleWord.ToDomain());
        }

        public ArticleWord Select(string word, int articleId)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var articleWord = this.context.ArticleWords.Find(word, articleId);
            return articleWord == null ? null : articleWord.ToPersistence();
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