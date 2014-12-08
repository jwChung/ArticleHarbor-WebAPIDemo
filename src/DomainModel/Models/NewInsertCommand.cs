namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class NewInsertCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IRepositories repositories;
        private readonly IModelCommand<IEnumerable<IModel>> innerCommand;

        public NewInsertCommand(
            IRepositories repositories,
            IModelCommand<IEnumerable<IModel>> innerCommand,
            IEnumerable<IModel> value) : base(value)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.repositories = repositories;
            this.innerCommand = innerCommand;
        }

        public IModelCommand<IEnumerable<IModel>> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
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

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(User user)
        {
            var newUser = await this.repositories.Users.InsertAsync(user);
            var newInnerCommand = await this.innerCommand.ExecuteAsync(newUser);

            return new NewInsertCommand(
                this.repositories,
                this.innerCommand,
                this.Value.Concat(new IModel[] { newUser }).Concat(newInnerCommand.Value));
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Article article)
        {
            var newArticle = await this.repositories.Articles.InsertAsync(article);
            var newInnerCommand = await this.innerCommand.ExecuteAsync(newArticle);

            return new NewInsertCommand(
                this.repositories,
                this.innerCommand,
                this.Value.Concat(new IModel[] { newArticle }).Concat(newInnerCommand.Value));
        }
    }
}