namespace EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;
    using ArticleWord = DomainModel.ArticleWord;

    public class ArticleWordRepository : IArticleWordRepository
    {
        private readonly ArticleHarborDbContext context;

        public ArticleWordRepository(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public DomainModel.ArticleWord Select(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var articleWord = this.context.ArticleWords.Find(articleId, word);
            return articleWord == null ? null : articleWord.ToDomain();
        }

        public Task InsertAsync(ArticleWord articleWord)
        {
            if (articleWord == null)
                throw new ArgumentNullException("articleWord");

            if (this.Select(articleWord.ArticleId, articleWord.Word) == null)
                this.context.ArticleWords.Add(articleWord.ToPersistence());

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int articleId)
        {
            var articleWords = this.context.ArticleWords
                .Where(x => x.ArticleId == articleId).ToArray();

            foreach (var articleWord in articleWords)
                this.context.ArticleWords.Remove(articleWord);

            return Task.FromResult<object>(null);
        }
    }
}