﻿namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Repositories;

    public class DeleteCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;

        public DeleteCommand(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public override IEnumerable<IModel> Value
        {
            get { yield break; }
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
            await this.Repositories.Users.DeleteAsync(new Keys<string>(user.Id));
            return this;
        }
    }
}