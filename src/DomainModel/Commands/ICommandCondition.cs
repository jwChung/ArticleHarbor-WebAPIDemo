namespace ArticleHarbor.DomainModel.Commands
{
    using System.Threading.Tasks;
    using Models;

    public interface ICommandCondition
    {
        Task<bool> CanExecuteAsync(User user);

        Task<bool> CanExecuteAsync(Article article);

        Task<bool> CanExecuteAsync(Bookmark bookmark);

        Task<bool> CanExecuteAsync(Keyword keyword);
    }
}