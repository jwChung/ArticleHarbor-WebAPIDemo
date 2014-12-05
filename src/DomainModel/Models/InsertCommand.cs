namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class InsertCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;
        private readonly Func<string, IEnumerable<string>> nounExtractor;
        private readonly IEnumerable<IModel> value;

        public InsertCommand(
            IRepositories repositories,
            Func<string, IEnumerable<string>> nounExtractor,
            IEnumerable<IModel> value)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            if (value == null)
                throw new ArgumentNullException("value");

            this.repositories = repositories;
            this.nounExtractor = nounExtractor;
            this.value = value;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override IEnumerable<IModel> Value
        {
            get { return this.value; }
        }

        public Func<string, IEnumerable<string>> NounExtractor
        {
            get { return this.nounExtractor; }
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return this.ExecuteAsyncWith(user);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.ExecuteAsyncWith(bookmark);
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(User user)
        {
            var newUser = await this.repositories.Users.InsertAsync(user);
            return new InsertCommand(
                this.repositories, this.nounExtractor, this.Value.Concat(new[] { newUser }));
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Article article)
        {
            var newArticle = await this.repositories.Articles.InsertAsync(article);
            return new InsertCommand(
                this.repositories, this.nounExtractor, this.Value.Concat(new[] { newArticle }));
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Keyword keyword)
        {
            var newKeyword = await this.repositories.Keywords.InsertAsync(keyword);
            return new InsertCommand(
                this.repositories, this.nounExtractor, this.Value.Concat(new[] { newKeyword }));
        }
        
        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Bookmark bookmark)
        {
            var newBookmark = await this.repositories.Bookmarks.InsertAsync(bookmark);
            return new InsertCommand(
                this.repositories, this.nounExtractor, this.Value.Concat(new[] { newBookmark }));
        }
    }
}