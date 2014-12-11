namespace ArticleHarbor.DomainModel.Commands
{
    using System.Threading.Tasks;

    public interface ICommandCondition
    {
        Task<bool> CanExecuteAsync(User user);

        Task<bool> CanExecuteAsync(Article article);

        Task<bool> CanExecuteAsync(Bookmark bookmark);

        Task<bool> CanExecuteAsync(Keyword keyword);
    }
}