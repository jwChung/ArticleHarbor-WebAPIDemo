namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class ArticleWordService : IArticleWordService
    {
        private readonly IArticleWordRepository articleWords;

        public ArticleWordService(IArticleWordRepository articleWords)
        {
            if (articleWords == null)
                throw new ArgumentNullException("articleWords");

            this.articleWords = articleWords;
        }

        public IArticleWordRepository ArticleWords
        {
            get { return this.articleWords; }
        }

        public Task AddWordsAsync(int id, string subject)
        {
            if (subject == null)
                throw new ArgumentNullException("subject");

            throw new NotImplementedException();
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