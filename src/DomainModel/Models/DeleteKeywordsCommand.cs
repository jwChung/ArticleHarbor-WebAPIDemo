namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Article article)
        {
            await this.repositories.Keywords.ExecuteDeleteCommandAsync(
                new EqualPredicate("articleId", article.Id));

            return this;
        }
    }
}