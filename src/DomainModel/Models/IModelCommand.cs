namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelCommand<TValue>
    {
        TValue Value { get; }

        IModelCommand<TValue> Execute(IEnumerable<User> users);

        IModelCommand<TValue> Execute(User user);

        IModelCommand<TValue> Execute(IEnumerable<Article> articles);

        IModelCommand<TValue> Execute(Article article);

        IModelCommand<TValue> Execute(IEnumerable<Keyword> keywords);

        IModelCommand<TValue> Execute(Keyword keywords);

        IModelCommand<TValue> Execute(IEnumerable<Bookmark> bookmarks);

        IModelCommand<TValue> Execute(Bookmark bookmark);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<User> users);

        Task<IModelCommand<TValue>> ExecuteAsync(User user);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Article> articles);

        Task<IModelCommand<TValue>> ExecuteAsync(Article article);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Keyword> keywords);

        Task<IModelCommand<TValue>> ExecuteAsync(Keyword keywords);

        Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks);

        Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark);
    }
}