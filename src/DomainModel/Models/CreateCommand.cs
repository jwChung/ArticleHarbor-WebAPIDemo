namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using Repositories;

    public class CreateCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;
        private readonly IEnumerable<IModel> value;

        public CreateCommand(IRepositories repositories, IEnumerable<IModel> value)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (value == null)
                throw new ArgumentNullException("value");

            this.repositories = repositories;
            this.value = value;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override IEnumerable<IModel> Value
        {
            get { return this.value; }
        }
    }
}