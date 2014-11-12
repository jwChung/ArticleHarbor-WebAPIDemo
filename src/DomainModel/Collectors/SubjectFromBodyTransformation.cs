namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<Article> Convert(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return from article in articles
                   let newSubject = article.Body.Substring(
                       0,
                       Math.Min(article.Body.Length, this.subjectLength))
                   select article.WithSubject(newSubject);
        }
    }
}