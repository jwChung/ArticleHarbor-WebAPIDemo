namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ModelCommand<TReturn> : IModelCommand<TReturn>
    {
        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            ////return Task.FromResult<IModelCommand<TReturn>>(this);
            return null;
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            ////return Task.FromResult<IModelCommand<TReturn>>(this);
            return null;
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            ////return Task.FromResult<IModelCommand<TReturn>>(this);
            return null;
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            ////return Task.FromResult<IModelCommand<TReturn>>(this);
            return null;
        }
    }
}