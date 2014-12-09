namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using Repositories;

    public class UpdateKeywordsCondition : TrueCondition
    {
        private readonly IRepositories repositories;

        public UpdateKeywordsCondition(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }
    }
}