namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;

    public class PostArticleViewModel
    {
        public string Provider { get; set; }

        public string Guid { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "To contract to Database.")]
        public string Url { get; set; }
    }
}