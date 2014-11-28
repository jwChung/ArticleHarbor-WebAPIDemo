namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ModelCommand<TResult> : IModelCommand<TResult>
    {
        public abstract TResult Value { get; }

        public virtual IModelCommand<TResult> Execute(IEnumerable<User> users)
        {
            if (users == null)
                throw new ArgumentNullException("users");

            return users.Aggregate<User, IModelCommand<TResult>>(
                this, (c, u) => c.Execute(u));
        }

        public virtual IModelCommand<TResult> Execute(User user)
        {
            return this;
        }

        public virtual IModelCommand<TResult> Execute(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return articles.Aggregate<Article, IModelCommand<TResult>>(
                this, (c, a) => c.Execute(a));
        }

        public virtual IModelCommand<TResult> Execute(Article article)
        {
            return this;
        }

        public virtual IModelCommand<TResult> Execute(IEnumerable<Keyword> keywords)
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords");

            return keywords.Aggregate<Keyword, IModelCommand<TResult>>(
                this, (c, k) => c.Execute(k));
        }

        public virtual IModelCommand<TResult> Execute(Keyword keywords)
        {
            return this;
        }

        public virtual IModelCommand<TResult> Execute(IEnumerable<Bookmark> bookmarks)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("bookmarks");

            return bookmarks.Aggregate<Bookmark, IModelCommand<TResult>>(
                this, (c, b) => c.Execute(b));
        }

        public virtual IModelCommand<TResult> Execute(Bookmark bookmark)
        {
            return this;
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<Article> articles)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(Keyword keywords)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TResult>> ExecuteAsync(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }
    }
}