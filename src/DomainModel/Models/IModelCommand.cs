namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

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
    }
}