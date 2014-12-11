namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class UpdateCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;

        public UpdateCommand(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public async override Task<IEnumerable<IModel>> ExecuteAsync(User user)
        {
            await this.repositories.Users.UpdateAsync(user);
            return Enumerable.Empty<IModel>();
        }

        public async override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            await this.repositories.Articles.UpdateAsync(article);
            return Enumerable.Empty<IModel>();
        }

        public async override Task<IEnumerable<IModel>> ExecuteAsync(Keyword keyword)
        {
            await this.repositories.Keywords.UpdateAsync(keyword);
            return Enumerable.Empty<IModel>();
        }

        public async override Task<IEnumerable<IModel>> ExecuteAsync(Bookmark bookmark)
        {
            await this.repositories.Bookmarks.UpdateAsync(bookmark);
            return Enumerable.Empty<IModel>();
        }
    }
}