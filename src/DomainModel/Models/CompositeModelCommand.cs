namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CompositeModelCommand<TValue> : IModelCommand<TValue>
    {
        private readonly TValue value;

        public CompositeModelCommand(TValue value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = value;
        }

        public TValue Value
        {
            get { return this.value; }
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Article> articles)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(Keyword keyword)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }
    }
}