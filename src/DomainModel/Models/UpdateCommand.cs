namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using Repositories;

    public class UpdateCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;

        public UpdateCommand(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public override IEnumerable<IModel> Value
        {
            get { throw new NotImplementedException(); }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }
    }
}