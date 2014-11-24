namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ModelCommand<TResult> : IModelCommand<TResult>
    {
        public abstract TResult Result { get; }

        public abstract IEnumerable<TResult> Result2 { get; }

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

        public virtual IModelCommand<TResult> Execute(Keyword keyword)
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
    }
}