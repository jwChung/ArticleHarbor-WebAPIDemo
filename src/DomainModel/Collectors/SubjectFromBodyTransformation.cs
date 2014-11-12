namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using Models;

    public class SubjectFromBodyTransformation : IArticleTransformation
    {
        private readonly int subjectLength;

        public SubjectFromBodyTransformation(int subjectLength)
        {
            this.subjectLength = subjectLength;
        }

        public int SubjectLength
        {
            get { return this.subjectLength; }
        }

        public Article Transform(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var newSubject = article.Body.Substring(
                0,
                Math.Min(article.Body.Length, this.subjectLength));

            return article.WithSubject(newSubject);
        }
    }
}