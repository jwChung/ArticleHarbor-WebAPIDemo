namespace DomainModel
{
    using System;

    public class ArticleWord
    {
        private readonly int id;
        private readonly string word;

        public ArticleWord(int id, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            this.id = id;
            this.word = word;
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Word
        {
            get { return this.word; }
        }
    }
}