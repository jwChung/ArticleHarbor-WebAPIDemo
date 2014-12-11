namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using System.Collections.Generic;
    using DomainModel;
    using Newtonsoft.Json;

    public class ArticleDetailViewModel
    {
        private readonly Article article;
        private readonly IEnumerable<Keyword> keywords;

        public ArticleDetailViewModel(Article article, IEnumerable<Keyword> keywords)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            if (keywords == null)
                throw new ArgumentNullException("keywords");

            this.article = article;
            this.keywords = keywords;
        }

        [JsonIgnore]
        public Article Article
        {
            get { return this.article; }
        }

        public IEnumerable<Keyword> Keywords
        {
            get { return this.keywords; }
        }

        public int Id
        {
            get { return this.article.Id; }
        }

        public string Provider
        {
            get { return this.article.Provider; }
        }

        public string Guid
        {
            get { return this.article.Guid; }
        }

        public string Subject
        {
            get { return this.article.Subject; }
        }

        public string Body
        {
            get { return this.article.Body; }
        }

        public DateTime Date
        {
            get { return this.article.Date; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "To contract to Database.")]
        public string Url
        {
            get { return this.article.Url; }
        }

        public string UserId
        {
            get { return this.article.UserId; }
        }
    }
}