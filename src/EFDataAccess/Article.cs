namespace EFDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public int Id { get; set; }

        public string Provider { get; set; }

        public string No { get; set; }

        public string Subject { get; set; }

        public string Summary { get; set; }

        public DateTime Date { get; set; }

        public string Url { get; set; }

        public virtual ICollection<ArticleWord> ArticleWords { get; set; }
    }
}