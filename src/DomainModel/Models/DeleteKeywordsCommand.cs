namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Repositories;

    public class DeleteKeywordsCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;

        public DeleteKeywordsCommand(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            ////if (article == null)
            ////    throw new ArgumentNullException("article");

            ////return this.ExecuteAsyncWith(article);
            return null;
        }

        private Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Article article)
        {
            ////await this.repositories.Keywords.ExecuteDeleteCommandAsync(
            ////    new EqualPredicate("ArticleId", article.Id));

            ////return this;
            return null;
        }
    }
}