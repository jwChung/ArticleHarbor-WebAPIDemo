namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
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
        private readonly IRepository<Keys<int>, Article> repository;
        private readonly IModelCommand<IEnumerable<IModel>> insertCommand;

        public ArticlesController(
            IArticleService articleService,
            IRepository<Keys<int>, Article> repository,
            IModelCommand<IEnumerable<IModel>> insertCommand)
        {
            if (articleService == null)
                throw new ArgumentNullException("articleService");

            if (repository == null)
                throw new ArgumentNullException("repository");

            if (insertCommand == null)
                throw new ArgumentNullException("insertCommand");

            this.articleService = articleService;
            this.repository = repository;
            this.insertCommand = insertCommand;
        }

        public IArticleService ArticleService
        {
            get { return this.articleService; }
        }

        public IRepository<Keys<int>, Article> Repository
        {
            get { return this.repository; }
        }

        public IModelCommand<IEnumerable<IModel>> InsertCommand
        {
            get { return this.insertCommand; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is action method.")]
        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.repository.SelectAsync();
        }

        [Authorize] // TODO: return unauthorized code when Unauthorized exceptin is thrown.
        public Task<Article> PostAsync(PostArticleViewModel postArticle)
        {
            if (postArticle == null)
                throw new ArgumentNullException("postArticle");

            var article = new Article(
                -1,
                postArticle.Provider,
                postArticle.Guid,
                postArticle.Subject,
                postArticle.Body,
                postArticle.Date,
                postArticle.Url,
                this.User.Identity.Name);

            return this.articleService.AddAsync(article);
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