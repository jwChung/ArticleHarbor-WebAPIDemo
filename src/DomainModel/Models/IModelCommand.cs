namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelCommand<TResult>
    {
        TResult Result { get; }

        IModelCommand<TResult> Execute(IEnumerable<User> users);

        IModelCommand<TResult> Execute(User user);

        IModelCommand<TResult> Execute(IEnumerable<Article> articles);

        IModelCommand<TResult> Execute(Article article);

        IModelCommand<TResult> Execute(IEnumerable<Keyword> keywords);

        IModelCommand<TResult> Execute(Keyword keywords);

        IModelCommand<TResult> Execute(IEnumerable<Bookmark> bookmarks);

        IModelCommand<TResult> Execute(Bookmark bookmark);

        Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<User> users);

        Task<IModelCommand<TResult>> ExecuteAsync(User user);

        Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<Article> articles);

        Task<IModelCommand<TResult>> ExecuteAsync(Article article);

        Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<Keyword> keywords);

        Task<IModelCommand<TResult>> ExecuteAsync(Keyword keywords);

        Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<Bookmark> bookmarks);

        Task<IModelCommand<TResult>> ExecuteAsync(Bookmark bookmark);
    }
}