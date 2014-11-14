namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using Repositories;

    public class ArticleElement : IModelElement
    {
        private readonly Article article;
        private readonly IRepository<Id<int>, Article> repository;
        private readonly Lazy<IModelElement> userElement;
        private readonly Lazy<IEnumerable<IModelElement>> keywordElements;

        public ArticleElement(
            Article article,
            IRepository<Id<int>, Article> repository,
            Lazy<IModelElement> userElement,
            Lazy<IEnumerable<IModelElement>> keywordElements)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            if (repository == null)
                throw new ArgumentNullException("repository");

            if (userElement == null)
                throw new ArgumentNullException("userElement");

            if (keywordElements == null)
                throw new ArgumentNullException("keywordElements");

            this.article = article;
            this.repository = repository;
            this.userElement = userElement;
            this.keywordElements = keywordElements;
        }

        public Article Article
        {
            get { return this.article; }
        }

        public IRepository<Id<int>, Article> Repository
        {
            get { return this.repository; }
        }

        public Lazy<IModelElement> UserElement
        {
            get { return this.userElement; }
        }

        public Lazy<IEnumerable<IModelElement>> KeywordElements
        {
            get { return this.keywordElements; }
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