namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;

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

        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.articleService.GetAsync();
        }
    }
}