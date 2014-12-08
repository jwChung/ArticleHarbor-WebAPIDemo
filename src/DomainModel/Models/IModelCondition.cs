namespace ArticleHarbor.DomainModel.Models
{
    using System.Threading.Tasks;

    public interface IModelCondition
    {
        Task<bool> CanExecuteAsync(User user);

        Task<bool> CanExecuteAsync(Article article);

        Task<bool> CanExecuteAsync(Bookmark bookmark);

        Task<bool> CanExecuteAsync(Keyword keyword);
    }
}