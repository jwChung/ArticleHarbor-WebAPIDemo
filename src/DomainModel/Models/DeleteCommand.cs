namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Repositories;

    public class DeleteCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;

        public DeleteCommand(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(User user)
        {
            ////if (user == null)
            ////    throw new ArgumentNullException("user");

            ////return this.ExecuteAsyncWith(user);
            return null;
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            ////if (article == null)
            ////    throw new ArgumentNullException("article");

            ////return this.ExecuteAsyncWith(article);
            return null;
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Keyword keyword)
        {
            ////if (keyword == null)
            ////    throw new ArgumentNullException("keyword");

            ////return this.ExecuteAsyncWith(keyword);
            return null;
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Bookmark bookmark)
        {
            ////if (bookmark == null)
            ////    throw new ArgumentNullException("bookmark");

            ////return this.ExecuteAsyncWith(bookmark);
            return null;
        }

        private Task<IEnumerable<IModel>> ExecuteAsyncWith(Article article)
        {
            ////await this.Repositories.Articles.DeleteAsync(new Keys<int>(article.Id));
            ////return this;
            return null;
        }

        private Task<IEnumerable<IModel>> ExecuteAsyncWith(User user)
        {
            ////await this.Repositories.Users.DeleteAsync(new Keys<string>(user.Id));
            ////return this;
            return null;
        }

        private Task<IEnumerable<IModel>> ExecuteAsyncWith(Keyword keyword)
        {
            ////await this.Repositories.Keywords.DeleteAsync(
            ////    new Keys<int, string>(keyword.ArticleId, keyword.Word));
            ////return this;
            return null;
        }

        private Task<IEnumerable<IModel>> ExecuteAsyncWith(Bookmark bookmark)
        {
            ////await this.Repositories.Bookmarks.DeleteAsync(
            ////    new Keys<string, int>(bookmark.UserId, bookmark.ArticleId));
            ////return this;
            return null;
        }
    }
}