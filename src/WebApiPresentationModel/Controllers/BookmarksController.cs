namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;

    [Authorize]
    public class BookmarksController : ApiController
    {
        private readonly IRepositories repositories;
        private readonly IModelCommand<IModel> selectArticlesCommand;
        private readonly IModelCommand<IModel> insertCommand;
        private readonly IModelCommand<IModel> deleteCommand;

        public BookmarksController(
            IRepositories repositories,
            IModelCommand<IModel> selectArticlesCommand,
            IModelCommand<IModel> insertCommand,
            IModelCommand<IModel> deleteCommand)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (selectArticlesCommand == null)
                throw new ArgumentNullException("selectArticlesCommand");

            if (insertCommand == null)
                throw new ArgumentNullException("insertCommand");

            if (deleteCommand == null)
                throw new ArgumentNullException("deleteCommand");

            this.repositories = repositories;
            this.selectArticlesCommand = selectArticlesCommand;
            this.insertCommand = insertCommand;
            this.deleteCommand = deleteCommand;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public IModelCommand<IModel> SelectArticlesCommand
        {
            get { return this.selectArticlesCommand; }
        }

        public IModelCommand<IModel> InsertCommand
        {
            get { return this.insertCommand; }
        }

        public IModelCommand<IModel> DeleteCommand
        {
            get { return this.deleteCommand; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is action method.")]
        public async Task<IEnumerable<Article>> GetAsync()
        {
            var userId = this.User.Identity.Name;
            var user = await this.repositories.Users.FindAsync(new Keys<string>(userId));

            return (await user.ExecuteAsync(this.selectArticlesCommand)).Cast<Article>();
        }

        public async Task PostAsync(int id)
        {
            var userId = this.User.Identity.Name;
            await new Bookmark(userId, id).ExecuteAsync(this.insertCommand);
        }

        public async Task DeleteAsync(int id)
        {
            var userId = this.User.Identity.Name;
            await new Bookmark(userId, id).ExecuteAsync(this.deleteCommand);
        }
    }
}