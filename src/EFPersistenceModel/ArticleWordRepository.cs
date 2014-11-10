namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using Article = DomainModel.Article;
    using ArticleWord = DomainModel.ArticleWord;

    public class ArticleWordRepository : IRepository<ArticleWord>
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

        public ArticleWord Select(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var articleWord = this.context.ArticleWords.Find(articleId, word);
            return articleWord == null ? null : articleWord.ToDomain();
        }

        public Task<ArticleWord> InsertAsync(ArticleWord articleWord)
        {
            if (articleWord == null)
                throw new ArgumentNullException("articleWord");

            if (this.Select(articleWord.ArticleId, articleWord.Word) == null)
                this.context.ArticleWords.Add(articleWord.ToPersistence());

            return Task.FromResult<ArticleWord>(articleWord);
        }

        public Task DeleteAsync(int articleId)
        {
            var articleWords = this.context.ArticleWords
                .Where(x => x.ArticleId == articleId).ToArray();

            foreach (var articleWord in articleWords)
                this.context.ArticleWords.Remove(articleWord);

            return Task.FromResult<object>(null);
        }

        public Task<DomainModel.Article> FineAsync(params object[] identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<ArticleWord>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        Task<Article> IRepository<ArticleWord>.InsertAsync(ArticleWord article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }

        public Task UpdateAsync(ArticleWord article)
        {
            if (article == null)
                throw new ArgumentNullException("article");
            throw new NotImplementedException();
        }

        public Task DeleteAsync(params object[] identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            var id = (int)identity[0];
            var articleWords = this.context.ArticleWords
                .Where(x => x.ArticleId == id).ToArray();

            foreach (var articleWord in articleWords)
                this.context.ArticleWords.Remove(articleWord);

            return Task.FromResult<object>(null);
        }
    }
}