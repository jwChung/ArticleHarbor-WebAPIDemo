namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        public IKeys GetKeys()
        {
            return new Keys<int, string>(this.articleId, this.word);
        }

        public Task<IEnumerable<TReturn>> ExecuteAsync<TReturn>(IModelCommand<TReturn> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.ExecuteAsync(this);
        }
    }
}