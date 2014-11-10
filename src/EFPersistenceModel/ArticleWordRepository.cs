namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
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

        public Task<ArticleWord> InsertAsync(ArticleWord item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (this.Select(item.ArticleId, item.Word) == null)
                this.context.ArticleWords.Add(item.ToPersistence());

            return Task.FromResult<ArticleWord>(item);
        }

        public Task<ArticleWord> FineAsync(params object[] identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleWord>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ArticleWord item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
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