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
    using Keyword = DomainModel.Models.Keyword;

    public class KeywordRepository : IKeywordRepository
    {
        private readonly ArticleHarborDbContext context;

        public KeywordRepository(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public Keyword Select(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            var keyword = this.context.Keywords.Find(articleId, word);
            return keyword == null ? null : keyword.ToDomain();
        }

        public Task<Keyword> InsertAsync(Keyword article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.InsertAsyncImpl(article);
        }

        public Task<Keyword> FindAsync(int id, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            Func<EFDataAccess.Keyword, bool> expression =
                x => x.ArticleId == id
                && x.Word.Equals(word, StringComparison.CurrentCultureIgnoreCase);

            var keyword = this.context.Keywords.Local.SingleOrDefault(expression);
            if (keyword == null)
                keyword = this.context.Keywords.SingleOrDefault(expression);

            return Task.FromResult(keyword == null ? null : keyword.ToDomain());
        }

        public Task DeleteAsync(int id)
        {
            // TODO: Improve performance.
            var keywords = this.context.Keywords
                .Where(x => x.ArticleId == id).ToArray();

            foreach (var keyword in keywords)
                this.context.Keywords.Remove(keyword);

            return Task.FromResult<object>(null);
        }

        private async Task<Keyword> InsertAsyncImpl(Keyword article)
        {
            if (await this.FindAsync(article.ArticleId, article.Word) == null)
                this.context.Keywords.Add(article.ToPersistence());

            return article;
        }
    }
}