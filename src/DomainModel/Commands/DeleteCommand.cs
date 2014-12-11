namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Queries;
    using Repositories;

    public class DeleteCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;

        public DeleteCommand(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return this.ExecuteAsyncWith(user);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.ExecuteAsyncWith(bookmark);
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(User user)
        {
            await this.Repositories.Users.DeleteAsync(new Keys<string>(user.Id));
            return Enumerable.Empty<IModel>();
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Article article)
        {
            await this.Repositories.Articles.DeleteAsync(new Keys<int>(article.Id));
            return Enumerable.Empty<IModel>();
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Keyword keyword)
        {
            await this.Repositories.Keywords.DeleteAsync(
                new Keys<int, string>(keyword.ArticleId, keyword.Word));
            return Enumerable.Empty<IModel>();
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Bookmark bookmark)
        {
            await this.Repositories.Bookmarks.DeleteAsync(
                new Keys<string, int>(bookmark.UserId, bookmark.ArticleId));
            return Enumerable.Empty<IModel>();
        }
    }
}