namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelCommand<TValue>
    {
        TValue Value { get; }

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<User> users);

        Task<IModelCommand<TValue>> ExecuteAsync(User user);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Article> articles);

        Task<IModelCommand<TValue>> ExecuteAsync(Article article);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Keyword> keywords);

        Task<IModelCommand<TValue>> ExecuteAsync(Keyword keyword);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks);

        Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark);
    }
}