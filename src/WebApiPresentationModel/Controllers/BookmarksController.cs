namespace ArticleHarbor.WebApiPresentationModel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using DomainModel.Services;

    [Authorize]
    public class BookmarksController : ApiController
    {
        private readonly IBookmarkService bookmarkService;
        private readonly IRepositories repositories;

        public BookmarksController(IBookmarkService bookmarkService, IRepositories repositories)
        {
            if (bookmarkService == null)
                throw new ArgumentNullException("bookmarkService");

            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.bookmarkService = bookmarkService;
            this.repositories = repositories;
        }

        public IBookmarkService BookmarkService
        {
            get { return this.bookmarkService; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is action method.")]
        public Task<IEnumerable<Article>> GetAsync()
        {
            var actor = this.User.Identity.Name;
            return this.bookmarkService.GetAsync(actor);
        }

        public Task PostAsync(int id)
        {
            var actor = this.User.Identity.Name;
            var bookmark = new Bookmark(actor, id);
            return this.bookmarkService.AddAsync(bookmark);
        }

        public Task DeleteAsync(int id)
        {
            var actor = this.User.Identity.Name;
            var bookmark = new Bookmark(actor, id);
            return this.bookmarkService.RemoveAsync(bookmark);
        }
    }
}