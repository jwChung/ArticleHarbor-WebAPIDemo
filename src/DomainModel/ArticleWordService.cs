namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleWordService : IArticleWordService
    {
        private readonly IArticleWordRepository articleWords;
        private readonly IRepository<Article> articles;
        private readonly Func<string, IEnumerable<string>> nounExtractor;

        public ArticleWordService(
            IArticleWordRepository articleWords,
            IRepository<Article> articles,
            Func<string, IEnumerable<string>> nounExtractor)
        {
            if (articleWords == null)
                throw new ArgumentNullException("articleWords");

            if (articles == null)
                throw new ArgumentNullException("articles");

            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            this.articleWords = articleWords;
            this.articles = articles;
            this.nounExtractor = nounExtractor;
        }

        public IArticleWordRepository ArticleWords
        {
            get { return this.articleWords; }
        }

        public Func<string, IEnumerable<string>> NounExtractor
        {
            get { return this.nounExtractor; }
        }

        public IRepository<Article> Articles
        {
            get { return this.articles; }
        }

        public Task AddWordsAsync(int id, string subject)
        {
            var tasks = this.nounExtractor(subject)
                .Select(w => this.articleWords.InsertAsync(new ArticleWord(id, w))).ToArray();

            return Task.WhenAll(tasks);
        }

        public async Task ModifyWordsAsync(int id, string subject)
        {
            await this.RemoveWordsAsync(id);
            await this.AddWordsAsync(id, subject);
        }

        public Task RemoveWordsAsync(int id)
        {
            return this.articleWords.DeleteAsync(id);
        }
    }
}