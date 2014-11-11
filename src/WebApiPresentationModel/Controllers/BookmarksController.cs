namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Web.Http;
    using DomainModel.Services;

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
    }
}