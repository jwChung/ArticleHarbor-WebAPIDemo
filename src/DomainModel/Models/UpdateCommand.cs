namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
            get { yield break; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public async override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(User user)
        {
            await this.repositories.Users.UpdateAsync(user);
            return this;
        }

        public async override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
        {
            await this.repositories.Articles.UpdateAsync(article);
            return this;
        }

        public async override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Keyword keyword)
        {
            await this.repositories.Keywords.UpdateAsync(keyword);
            return this;
        }

        public async override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Bookmark bookmark)
        {
            await this.repositories.Bookmarks.UpdateAsync(bookmark);
            return this;
        }
    }
}