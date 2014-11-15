namespace ArticleHarbor.DomainModel.Models
{
    using System;

    public class Keyword : IModel
    {
        private readonly string word;
        private readonly int articleId;

        public Keyword(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            this.word = word;
            this.articleId = articleId;
        }

        public int ArticleId
        {
            get { return this.articleId; }
        }

        public string Word
        {
            get { return this.word; }
        }

        public IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.Execute(this);
        }
    }
}