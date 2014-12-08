namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ModelCommand<TValue> : IModelCommand<TValue>
    {
        public abstract TValue Value { get; }
        
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

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(Keyword keyword)
        {
            return Task.FromResult<IModelCommand<TValue>>(this);
        }

        public virtual Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("bookmarks");

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