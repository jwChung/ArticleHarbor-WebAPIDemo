﻿namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class SelectBookmarkedArticlesCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;
        private readonly IEnumerable<IModel> value;

        public SelectBookmarkedArticlesCommand(
            IRepositories repositories, IEnumerable<IModel> value)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (value == null)
                throw new ArgumentNullException("value");

            this.repositories = repositories;
            this.value = value;
        }

        public override IEnumerable<IModel> Value
        {
            get { return this.value; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return this.ExecuteAsyncWith(user);
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(User user)
        {
            var bookmarks = await this.repositories.Bookmarks
                .ExecuteSelectCommandAsync(new EqualPredicate("UserId", user.Id));

            var parameterValues = bookmarks.Select(b => b.ArticleId).Cast<object>();
            var articles = await this.repositories.Articles
                .ExecuteSelectCommandAsync(new InClausePredicate("Id", parameterValues));

            return new SelectBookmarkedArticlesCommand(
                this.repositories, this.value.Concat(articles));
        }
    }
}