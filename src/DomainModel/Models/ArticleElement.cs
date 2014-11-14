namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using Repositories;

    public class ArticleElement : IModelElement
    {
        private readonly Article article;
        private readonly IRepository<Id<int>, Article> repository;
        private readonly IModelElementCollection<Id<string>, User> users;
        private readonly IModelElementCollection<Id<int, string>, Keyword> keywords;

        public ArticleElement(
            Article article,
            IRepository<Id<int>, Article> repository,
            IModelElementCollection<Id<string>, User> users,
            IModelElementCollection<Id<int, string>, Keyword> keywords)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            if (repository == null)
                throw new ArgumentNullException("repository");

            if (users == null)
                throw new ArgumentNullException("users");

            if (keywords == null)
                throw new ArgumentNullException("keywords");

            this.article = article;
            this.repository = repository;
            this.users = users;
            this.keywords = keywords;
        }

        public Article Article
        {
            get { return this.article; }
        }

        public IRepository<Id<int>, Article> Repository
        {
            get { return this.repository; }
        }

        public IModelElementCollection<Id<string>, User> Users
        {
            get { return this.users; }
        }

        public IModelElementCollection<Id<int, string>, Keyword> Keywords
        {
            get { return this.keywords; }
        }

        public IModelElement UserElement
        {
            get { return this.users[new Id<string>(this.article.UserId)]; }
        }

        public IModelElementCommand<TResult> Execute<TResult>(
            IModelElementCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.Execute(this);
        }
    }
}