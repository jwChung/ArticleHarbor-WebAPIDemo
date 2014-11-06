namespace DomainModel
{
    using System;

    public class ArticleWord
    {
        private readonly string word;
        private readonly int articleId;

        public ArticleWord(string word, int articleId)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            this.word = word;
            this.articleId = articleId;
        }

        public string Word
        {
            get { return this.word; }
        }

        public int ArticleId
        {
            get { return this.articleId; }
        }
    }
}