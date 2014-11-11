namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleWordService : IArticleWordService
    {
        private readonly IArticleWordRepository articleWords;
        private readonly Func<string, IEnumerable<string>> nounExtractor;

        public ArticleWordService(
            IArticleWordRepository articleWords, Func<string, IEnumerable<string>> nounExtractor)
        {
            if (articleWords == null)
                throw new ArgumentNullException("articleWords");

            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            this.articleWords = articleWords;
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

        public Task AddWordsAsync(int id, string subject)
        {
            var tasks = this.nounExtractor(subject)
                .Select(w => this.articleWords.InsertAsync(new ArticleWord(id, w))).ToArray();

            return Task.WhenAll(tasks);
        }

        public Task ModifyWordsAsync(int id, string subject)
        {
            if (subject == null)
                throw new ArgumentNullException("subject");

            throw new NotImplementedException();
        }

        public Task RemoveWordsAsync(int id)
        {
            return this.articleWords.DeleteAsync(id);
        }
    }
}