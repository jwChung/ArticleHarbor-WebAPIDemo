namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using DomainModel.Repositories;
    using ArticleWord = DomainModel.Models.ArticleWord;

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

        public ArticleWord Select(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var articleWord = this.context.ArticleWords.Find(articleId, word);
            return articleWord == null ? null : articleWord.ToDomain();
        }

        public Task<ArticleWord> InsertAsync(ArticleWord article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.InsertAsyncImpl(article);
        }

        public Task<ArticleWord> FindAsync(int id, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            Func<EFDataAccess.ArticleWord, bool> expression =
                x => x.ArticleId == id
                && x.Word.Equals(word, StringComparison.CurrentCultureIgnoreCase);

            var articleWord = this.context.ArticleWords.Local.SingleOrDefault(expression);
            if(articleWord == null)
                articleWord = this.context.ArticleWords.SingleOrDefault(expression);

            return Task.FromResult(articleWord == null ? null : articleWord.ToDomain());
        }

        public Task DeleteAsync(int id)
        {
            // TODO: Improve performance.
            var articleWords = this.context.ArticleWords
                .Where(x => x.ArticleId == id).ToArray();

            foreach (var articleWord in articleWords)
                this.context.ArticleWords.Remove(articleWord);

            return Task.FromResult<object>(null);
        }

        private async Task<ArticleWord> InsertAsyncImpl(ArticleWord article)
        {
            if (await this.FindAsync(article.ArticleId, article.Word) == null)
                this.context.ArticleWords.Add(article.ToPersistence());

            return article;
        }
    }
}