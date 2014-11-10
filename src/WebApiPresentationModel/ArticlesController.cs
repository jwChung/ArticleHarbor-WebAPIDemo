namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ArticleHarbor.DomainModel;

    public class ArticlesController : ApiController
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService)
        {
            if (articleService == null)
                throw new ArgumentNullException("articleService");

            this.articleService = articleService;
        }

        public IArticleService ArticleService
        {
            get { return this.articleService; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Method is more appropriate.")]
        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.articleService.GetAsync();
        }

        [PermissionAuthorizationFilter(Permissions.ModifyOwnArticle | Permissions.CreateArticle)]
        public Task<Article> PostAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.articleService.SaveAsync(
                article.WithUserId(this.User.Identity.Name));
        }
    }
}