namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class InsertCommand : ModelCommand<IModel>
    {
        private readonly IRepositories repositories;
        private readonly IModelCommand<IModel> innerCommand;
        
        public InsertCommand(
            IRepositories repositories,
            IModelCommand<IModel> innerCommand)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.repositories = repositories;
            this.innerCommand = innerCommand;
        }

        public IModelCommand<IModel> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return this.ExecuteAsyncWith(user);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.ExecuteAsyncWith(bookmark);
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(User user)
        {
            var newUser = await this.repositories.Users.InsertAsync(user);
            var values = await this.innerCommand.ExecuteAsync(newUser);
            return new IModel[] { newUser }.Concat(values);
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Article article)
        {
            var newArticle = await this.repositories.Articles.InsertAsync(article);
            var values = await this.innerCommand.ExecuteAsync(newArticle);
            return new IModel[] { newArticle }.Concat(values);
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Keyword keyword)
        {
            var newKeyword = await this.repositories.Keywords.InsertAsync(keyword);
            var values = await this.innerCommand.ExecuteAsync(newKeyword);
            return new IModel[] { newKeyword }.Concat(values);
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Bookmark bookmark)
        {
            var newBookmark = await this.repositories.Bookmarks.InsertAsync(bookmark);
            var values = await this.innerCommand.ExecuteAsync(newBookmark);
            return new IModel[] { newBookmark }.Concat(values);
        }
    }
}