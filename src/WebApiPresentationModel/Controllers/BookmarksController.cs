namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel.Models;
    using DomainModel.Services;

    [Authorize]
    public class BookmarksController : ApiController
    {
        private readonly IBookmarkService bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            if (bookmarkService == null)
                throw new ArgumentNullException("bookmarkService");

            this.bookmarkService = bookmarkService;
        }

        public IBookmarkService BookmarkService
        {
            get { return this.bookmarkService; }
        }

        public Task<IEnumerable<Article>> GetAsync()
        {
            var actor = this.User.Identity.Name;
            return this.bookmarkService.GetAsync(actor);
        }
    }
}