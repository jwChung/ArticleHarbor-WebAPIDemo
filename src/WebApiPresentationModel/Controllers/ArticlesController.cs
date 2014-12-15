namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using DomainModel.Commands;
    using DomainModel.Models;
    using DomainModel.Queries;
    using DomainModel.Repositories;
    using Models;

    public class ArticlesController : ApiController
    {
        private readonly IRepositories repositories;
        private readonly IModelCommand<IModel> insertCommand;
        private readonly IModelCommand<IModel> updateCommand;
        private readonly IModelCommand<IModel> deleteCommand;

        public ArticlesController(
            IRepositories repositories,
            IModelCommand<IModel> insertCommand,
            IModelCommand<IModel> updateCommand,
            IModelCommand<IModel> deleteCommand)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (insertCommand == null)
                throw new ArgumentNullException("insertCommand");

            if (updateCommand == null)
                throw new ArgumentNullException("updateCommand");

            if (deleteCommand == null)
                throw new ArgumentNullException("deleteCommand");

            this.repositories = repositories;
            this.insertCommand = insertCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public IModelCommand<IModel> InsertCommand
        {
            get { return this.insertCommand; }
        }

        public IModelCommand<IModel> UpdateCommand
        {
            get { return this.updateCommand; }
        }

        public IModelCommand<IModel> DeleteCommand
        {
            get { return this.deleteCommand; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is action method.")]
        public Task<IEnumerable<Article>> GetAsync(ArticleQueryViewModel query)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            var sqlQuery = query.ProvideQuery();
            return this.repositories.Articles.SelectAsync(sqlQuery);
        }

        [Authorize]
        public Task<ArticleDetailViewModel> PostAsync(PostArticleViewModel postArticle)
        {
            if (postArticle == null)
                throw new ArgumentNullException("postArticle");

            return this.PostAsyncWith(postArticle);
        }

        [Authorize]
        public Task PutAsync(PutArticleViewModel putArticle)
        {
            if (putArticle == null)
                throw new ArgumentNullException("putArticle");

            return this.PutAsyncWith(putArticle);
        }

        [Authorize]
        public async Task DeleteAsync(int id)
        {
            var userId = (await this.Repositories.Articles.FindAsync(new Keys<int>(id))).UserId;
            string none = Guid.NewGuid().ToString("N");

            var article = new Article(id, none, none, none, none, default(DateTime), none, userId);

            await this.deleteCommand.ExecuteAsync(article);
        }

        private async Task<ArticleDetailViewModel> PostAsyncWith(PostArticleViewModel postArticle)
        {
            var article = new Article(
                -1,
                postArticle.Provider,
                postArticle.Guid,
                postArticle.Subject,
                postArticle.Body,
                postArticle.Date,
                postArticle.Url,
                this.User.Identity.Name);

            var models = await article.ExecuteAsync(this.insertCommand);

            return new ArticleDetailViewModel(
                models.OfType<Article>().Single(), models.OfType<Keyword>());
        }

        private async Task PutAsyncWith(PutArticleViewModel putArticle)
        {
            var userId = (await this.Repositories.Articles.FindAsync(
                new Keys<int>(putArticle.Id))).UserId;

            var article = new Article(
                putArticle.Id,
                putArticle.Provider,
                putArticle.Guid,
                putArticle.Subject,
                putArticle.Body,
                putArticle.Date,
                putArticle.Url,
                userId);

            await this.updateCommand.ExecuteAsync(article);
        }
    }
}