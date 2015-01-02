namespace ArticleHarbor.DomainModel.Commands
{
    using System.Threading.Tasks;
    using Models;

    public class TrueCondition : ICommandCondition
    {
        public virtual Task<bool> CanExecuteAsync(User user)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanExecuteAsync(Article article)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanExecuteAsync(Bookmark bookmark)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanExecuteAsync(Keyword keyword)
        {
            return Task.FromResult(true);
        }
    }
}