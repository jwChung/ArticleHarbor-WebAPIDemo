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
        private readonly IEnumerable<IModel> value;

        public InsertCommand(IRepositories repositories, IEnumerable<IModel> value)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            if (value == null)
                throw new ArgumentNullException("value");

            this.repositories = repositories;
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
            return new InsertCommand(this.repositories, this.Value.Concat(new[] { newUser }));
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Article article)
        {
            var newArticle = await this.repositories.Articles.InsertAsync(article);
            return new InsertCommand(this.repositories, this.Value.Concat(new[] { newArticle }));
        }
    }
}