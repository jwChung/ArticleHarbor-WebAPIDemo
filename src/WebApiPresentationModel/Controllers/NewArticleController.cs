namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using Models;

    public class NewArticleController : ApiController
    {
        private readonly IArticleService articleService;

        public NewArticleController(IArticleService articleService)
        {
            if (articleService == null)
                throw new ArgumentNullException("articleService");

            this.articleService = articleService;
        }

        public IArticleService ArticleService
        {
            get { return this.articleService; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method is controller action.")]
        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.articleService.GetAsync();
        }

        public Task<Article> PostAsync(PostArticleViewModel postArticle)
        {
            if (postArticle == null)
                throw new ArgumentNullException("postArticle");

            var article = new Article(
                -1,
                postArticle.Provider,
                postArticle.No,
                postArticle.Subject,
                postArticle.Body,
                postArticle.Date,
                postArticle.Url,
                this.User.Identity.Name);

            return this.articleService.AddAsync(article);
        }

        public Task PutAsync(PutArticleViewModel putArticle)
        {
            if (putArticle == null)
                throw new ArgumentNullException("putArticle");

            var actor = this.User.Identity.Name;
            var userId = this.articleService.GetUserId(putArticle.Id);

            var article = new Article(
                putArticle.Id,
                putArticle.Provider,
                putArticle.No,
                putArticle.Subject,
                putArticle.Body,
                putArticle.Date,
                putArticle.Url,
                userId);

            return this.articleService.ModifyAsync(actor, article);
        }
    }
}