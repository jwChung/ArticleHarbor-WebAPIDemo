namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class EmptyCommand<TReturn> : IModelCommand<TReturn>
    {
        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<User> users)
        {
            if (users == null)
                throw new ArgumentNullException("users");

            return this.ExecuteAsyncWith(users);
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return this.ExecuteAsyncWith(articles);
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords");

            return this.ExecuteAsyncWith(keywords);
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            if (bookmarks == null)
                throw new ArgumentNullException("bookmarks");

            return this.ExecuteAsyncWith(bookmarks);
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            return Task.FromResult(Enumerable.Empty<TReturn>());
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(IEnumerable<User> users)
        {
            var result = Enumerable.Empty<TReturn>();
            foreach (var user in users)
                result = result.Concat(await this.ExecuteAsync(user));

            return result;
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(IEnumerable<Article> articles)
        {
            var result = Enumerable.Empty<TReturn>();
            foreach (var article in articles)
                result = result.Concat(await this.ExecuteAsync(article));

            return result;
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(IEnumerable<Keyword> keywords)
        {
            var result = Enumerable.Empty<TReturn>();
            foreach (var keyword in keywords)
                result = result.Concat(await this.ExecuteAsync(keyword));

            return result;
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(IEnumerable<Bookmark> bookmarks)
        {
            var result = Enumerable.Empty<TReturn>();
            foreach (var bookmark in bookmarks)
                result = result.Concat(await this.ExecuteAsync(bookmark));

            return result;
        }
    }
}