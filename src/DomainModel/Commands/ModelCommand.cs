namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class ModelCommand<TReturn> : IModelCommand<TReturn>
    {
        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<User> users)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Article> articles)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }
    }
}