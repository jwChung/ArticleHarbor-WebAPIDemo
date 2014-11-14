namespace ArticleHarbor.DomainModel.Models
{
    using System;

    public class ArticleElement : IModelElement<Article>
    {
        private readonly Article article;

        public ArticleElement(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            this.article = article;
        }

        public Article Model
        {
            get { return this.article; }
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