namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
            ////if (command == null)
            ////    throw new ArgumentNullException("command");

            ////return command.ExecuteAsync(this);
            return null;
        }
    }
}