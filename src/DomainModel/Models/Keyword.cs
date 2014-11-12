namespace ArticleHarbor.DomainModel.Models
{
    using System;

    public class Keyword
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
    }
}