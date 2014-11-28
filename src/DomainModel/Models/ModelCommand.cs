namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ModelCommand<TValue> : IModelCommand<TValue>
    {
        public virtual TValue Value
        {
            get { return default(TValue); }
        }

        public virtual IModelCommand<TValue> Execute(IEnumerable<User> users)
        {
            if (users == null)
                throw new ArgumentNullException("users");

            return users.Aggregate<User, IModelCommand<TValue>>(
                this, (c, u) => c.Execute(u));
        }

        public virtual IModelCommand<TValue> Execute(User user)
        {
            return this;
        }

        public virtual IModelCommand<TValue> Execute(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return articles.Aggregate<Article, IModelCommand<TValue>>(
                this, (c, a) => c.Execute(a));
        }

        public virtual IModelCommand<TValue> Execute(Article article)
        {
            return this;
        }

        public virtual IModelCommand<TValue> Execute(IEnumerable<Keyword> keywords)
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords");

            return keywords.Aggregate<Keyword, IModelCommand<TValue>>(
                this, (c, k) => c.Execute(k));
        }

        public virtual IModelCommand<TValue> Execute(Keyword keywords)
        {
            return this;
        }

        public virtual IModelCommand<TValue> Execute(IEnumerable<Bookmark> bookmarks)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("bookmarks");

            return bookmarks.Aggregate<Bookmark, IModelCommand<TValue>>(
                this, (c, b) => c.Execute(b));
        }

        public virtual IModelCommand<TValue> Execute(Bookmark bookmark)
        {
            return this;
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<User> users)
        {
            if (users == null)
                throw new ArgumentNullException("users");

            return this.ExecuteAsyncWith(users);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(User user)
        {
            return Task.FromResult<IModelCommand<TValue>>(this);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return this.ExecuteAsyncWith(articles);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(Article article)
        {
            return Task.FromResult<IModelCommand<TValue>>(this);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords");

            return this.ExecuteAsyncWith(keywords);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(Keyword keywords)
        {
            return Task.FromResult<IModelCommand<TValue>>(this);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("articles");

            return this.ExecuteAsyncWith(bookmarks);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark)
        {
            return Task.FromResult<IModelCommand<TValue>>(this);
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(IEnumerable<User> users)
        {
            IModelCommand<TValue> command = this;
            foreach (var user in users)
                command = await command.ExecuteAsync(user);

            return command;
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(IEnumerable<Article> articles)
        {
            IModelCommand<TValue> command = this;
            foreach (var article in articles)
                command = await command.ExecuteAsync(article);

            return command;
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(IEnumerable<Bookmark> bookmarks)
        {
            IModelCommand<TValue> command = this;
            foreach (var bookmark in bookmarks)
                command = await command.ExecuteAsync(bookmark);

            return command;
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(IEnumerable<Keyword> keywords)
        {
            IModelCommand<TValue> command = this;
            foreach (var keyword in keywords)
                command = await command.ExecuteAsync(keyword);

            return command;
        }
    }
}