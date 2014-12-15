namespace ArticleHarbor.DomainModel.Queries
{
    using System;

    public class ContainsPredicate : OperablePredicate
    {
        private readonly string word;

        public ContainsPredicate(string columnName, string word)
            : base(columnName, "LIKE", "%" + EnsureNotNull(word).Replace("%", "[%]") + "%")
        {
            this.word = word;
        }

        public string Word
        {
            get { return this.word; }
        }

        private static string EnsureNotNull(string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            return word;
        }
    }
}