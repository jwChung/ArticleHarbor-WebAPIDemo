namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using Repositories;

    public class DeleteKeywordsCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;

        public DeleteKeywordsCommand(IRepositories repositories)
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
    }
}