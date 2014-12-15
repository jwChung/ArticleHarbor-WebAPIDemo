namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Commands;
    using Queries;

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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Keyword)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.word.ToUpper(CultureInfo.CurrentCulture).GetHashCode() * 397)
                    ^ this.articleId;
            }
        }

        protected bool Equals(Keyword other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(this.word, other.word, StringComparison.CurrentCultureIgnoreCase)
                && this.articleId == other.articleId;
        }
    }
}