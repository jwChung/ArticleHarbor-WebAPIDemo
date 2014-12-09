namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Threading.Tasks;
    using Repositories;

    public class UpdateKeywordsCondition : TrueCondition
    {
        private readonly IRepositories repositories;

        public UpdateKeywordsCondition(IRepositories repositories)
        {
            if (repositories == null)
                throw new ArgumentNullException("repositories");

            this.repositories = repositories;
        }

        public IRepositories Repositories
        {
            get { return this.repositories; }
        }

        public override Task<bool> CanExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.CanExecuteAsyncWith(article);
        }

        private async Task<bool> CanExecuteAsyncWith(Article article)
        {
            var articleOfRepo = await this.repositories.Articles
                .FindAsync(new Keys<int>(article.Id));

            return article.Subject != articleOfRepo.Subject;
        }
    }
}