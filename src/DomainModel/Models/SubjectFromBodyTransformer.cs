namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class SubjectFromBodyTransformer : ModelTransformer
    {
        private readonly int length;

        public SubjectFromBodyTransformer(int length)
        {
            this.length = length;
        }

        public int Length
        {
            get { return this.length; }
        }

        public override Task<Article> TransformAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var min = Math.Min(this.length, article.Body.Length);
            var newSubject = Regex.Replace(
                article.Body.Substring(0, min), "[\r\n]+", " ");

            return Task.FromResult(article.WithSubject(newSubject));
        }
    }
}