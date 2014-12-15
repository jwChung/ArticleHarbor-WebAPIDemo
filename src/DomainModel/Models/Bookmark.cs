namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Commands;
    using Queries;

    public class Bookmark : IModel
    {
        private readonly string userId;
        private readonly int articleId;

        public Bookmark(string userId, int articleId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            if (userId.Length == 0)
                throw new ArgumentException("The userId should not be empty.", "userId");

            this.userId = userId;
            this.articleId = articleId;
        }

        public string UserId
        {
            get { return this.userId; }
        }

        public int ArticleId
        {
            get { return this.articleId; }
        }

        public IKeys GetKeys()
        {
            return new Keys<string, int>(this.userId, this.articleId);
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
            return this.Equals((Bookmark)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.userId.ToUpper(CultureInfo.CurrentCulture).GetHashCode() * 397)
                    ^ this.articleId;
            }
        }

        protected bool Equals(Bookmark other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(this.userId, other.userId, StringComparison.CurrentCultureIgnoreCase)
                && this.articleId == other.articleId;
        }
    }
}