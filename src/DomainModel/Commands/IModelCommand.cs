namespace ArticleHarbor.DomainModel.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IModelCommand<TReturn>
    {
        Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<User> users);

        Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Article> articles);

        Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Keyword> keywords);

        Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Bookmark> bookmarks);

        Task<IEnumerable<TReturn>> ExecuteAsync(User user);

        Task<IEnumerable<TReturn>> ExecuteAsync(Article article);

        Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword);

        Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark);
    }
}