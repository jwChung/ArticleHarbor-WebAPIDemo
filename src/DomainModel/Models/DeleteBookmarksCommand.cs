namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Queries;
    using Repositories;

    public class DeleteBookmarksCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;

        public DeleteBookmarksCommand(IRepositories repositories)
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

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(User user)
        {
            await this.repositories.Bookmarks.ExecuteDeleteCommandAsync(
                new EqualPredicate("UserId", user.Id));

            return Enumerable.Empty<IModel>();
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Article article)
        {
            await this.repositories.Bookmarks.ExecuteDeleteCommandAsync(
                new EqualPredicate("ArticleId", article.Id));

            return Enumerable.Empty<IModel>();
        }
    }
}