namespace ArticleHarbor.DomainModel.Models
{
    using System;

    public class Bookmark
    {
        private readonly string userId;
        private readonly int articleId;

        public Bookmark(string userId, int articleId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            if (userId.Length == 0)
                throw new ArgumentException("The userId should not be empty.", "uesrId");

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
    }
}