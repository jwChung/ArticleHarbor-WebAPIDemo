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
    using DomainModel.Services;
    using Models;

    public class ArticlesController : ApiController
    {
        private readonly IArticleService articleService;
        private readonly IRepositories repositories;
        private readonly IModelCommand<IEnumerable<IModel>> insertCommand;
        private readonly IModelCommand<IEnumerable<IModel>> updateCommand;

        public ArticlesController(
            IArticleService articleService,
            IRepositories repositories,
            IModelCommand<IEnumerable<IModel>> insertCommand,
            IModelCommand<IEnumerable<IModel>> updateCommand)
        {
            if (articleService == null)
                throw new ArgumentNullException("articleService");

            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (insertCommand == null)
                throw new ArgumentNullException("insertCommand");

            if (updateCommand == null)
                throw new ArgumentNullException("updateCommand");

            this.articleService = articleService;
            this.repositories = repositories;
            this.insertCommand = insertCommand;
            this.updateCommand = updateCommand;
        }

        public IArticleService ArticleService
        {
            get { return this.articleService; }
        }

        public IModelCommand<IEnumerable<IModel>> InsertCommand
        {
            get { return this.insertCommand; }
        }

        public IModelCommand<IEnumerable<IModel>> UpdateCommand
        {
            get { return this.updateCommand; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is action method.")]
        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.repositories.Articles.SelectAsync();
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

            return this.PutAsyncImpl(putArticle);
        }

        [Authorize]
        public Task DeleteAsync(int id)
        {
            var actor = this.User.Identity.Name;
            return this.articleService.RemoveAsync(actor, id);
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

            var models = (await article.ExecuteAsync(this.insertCommand)).Value;

            return new ArticleDetailViewModel(
                models.OfType<Article>().Single(), models.OfType<Keyword>());
        }

        private async Task PutAsyncImpl(PutArticleViewModel putArticle)
        {
            var actor = this.User.Identity.Name;
            var userId = await this.articleService.GetUserIdAsync(putArticle.Id);

            var article = new Article(
                putArticle.Id,
                putArticle.Provider,
                putArticle.Guid,
                putArticle.Subject,
                putArticle.Body,
                putArticle.Date,
                putArticle.Url,
                userId);

            await this.articleService.ModifyAsync(actor, article);
        }
    }
}