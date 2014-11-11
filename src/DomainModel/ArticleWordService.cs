namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Threading.Tasks;

    public class ArticleWordService : IArticleWordService
    {
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
            throw new NotImplementedException();
        }
    }
}