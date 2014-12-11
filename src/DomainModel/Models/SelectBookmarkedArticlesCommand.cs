﻿namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Queries;
    using Repositories;

    public class SelectBookmarkedArticlesCommand : ModelCommand<IModel>
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
                .ExecuteSelectCommandAsync(new EqualPredicate("UserId", user.Id));

            var parameterValues = bookmarks.Select(b => b.ArticleId).Cast<object>();
            return await this.repositories.Articles
                .ExecuteSelectCommandAsync(new InClausePredicate("Id", parameterValues));
        }
    }
}