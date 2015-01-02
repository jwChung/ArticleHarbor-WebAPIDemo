namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Queries;
    using Repositories;

    public class SelectBookmarkedArticlesCommand : EmptyCommand<IModel>
    {
        private readonly IRepositories repositories;
        
        public SelectBookmarkedArticlesCommand(IRepositories repositories)
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

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(User user)
        {
            var bookmarks = await this.repositories.Bookmarks
                .SelectAsync(Predicate.Equal("UserId", user.Id));

            var parameterValues = bookmarks.Select(b => b.ArticleId).Cast<object>();
            return await this.repositories.Articles
                .SelectAsync(new InClausePredicate("Id", parameterValues));
        }
    }
}