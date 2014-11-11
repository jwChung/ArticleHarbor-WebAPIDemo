namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;

    public class PutArticleViewModel
    {
        public int Id { get; set; }

        public string Provider { get; set; }

        public string No { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public string Url { get; set; }
    }
}