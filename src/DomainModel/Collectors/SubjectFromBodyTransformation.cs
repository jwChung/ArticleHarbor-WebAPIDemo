namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<Article> Transform(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return this.TransfromWith(articles);
        }

        private IEnumerable<Article> TransfromWith(IEnumerable<Article> articles)
        {
            foreach (var article in articles)
            {
                var newSubject = article.Body.Substring(
                    0,
                    Math.Min(article.Body.Length, this.subjectLength));

                yield return article.WithSubject(newSubject);
            }
        }
    }
}