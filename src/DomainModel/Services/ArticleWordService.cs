namespace ArticleHarbor.DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Repositories;

    public class KeywordService : IKeywordService
    {
        private readonly IKeywordRepository keywords;
        private readonly IArticleRepository articles;
        private readonly Func<string, IEnumerable<string>> nounExtractor;

        public KeywordService(
            IKeywordRepository keywords,
            IArticleRepository articles,
            Func<string, IEnumerable<string>> nounExtractor)
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords");

            if (articles == null)
                throw new ArgumentNullException("articles");

            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            this.keywords = keywords;
            this.articles = articles;
            this.nounExtractor = nounExtractor;
        }

        public IKeywordRepository Keywords
        {
            get { return this.keywords; }
        }

        public Func<string, IEnumerable<string>> NounExtractor
        {
            get { return this.nounExtractor; }
        }

        public IArticleRepository Articles
        {
            get { return this.articles; }
        }

        public Task AddWordsAsync(int id, string subject)
        {
            var tasks = this.nounExtractor(subject)
                .Select(w => this.keywords.InsertAsync(new Keyword(id, w))).ToArray();

            return Task.WhenAll(tasks);
        }

        public async Task ModifyWordsAsync(int id, string subject)
        {
            var article = await this.articles.FindAsync(id);
            if (article == null)
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "There is no id '{0}' in article repository.",
                        id),
                    "id");

            if (subject == article.Subject)
                return;
            
            await this.RemoveWordsAsync(id);
            await this.AddWordsAsync(id, subject);
        }

        public Task RemoveWordsAsync(int id)
        {
            return this.keywords.DeleteAsync(id);
        }
    }
}