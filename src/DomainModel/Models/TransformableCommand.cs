namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TransformableCommand<TReturn> : IModelCommand<TReturn>
    {
        public Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }
    }
}