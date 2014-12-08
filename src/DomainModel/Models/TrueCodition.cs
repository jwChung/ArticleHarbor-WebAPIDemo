namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Threading.Tasks;

    public class TrueCodition : IModelCondition
    {
        public virtual Task<bool> CanExecuteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> CanExecuteAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> CanExecuteAsync(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> CanExecuteAsync(Keyword keyword)
        {
            throw new NotImplementedException();
        }
    }
}