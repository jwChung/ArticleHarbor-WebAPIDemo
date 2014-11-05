namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        public Task<IEnumerable<Article>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Article AddOrModify(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }

        public void Remove(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            throw new NotImplementedException();
        }
    }
}