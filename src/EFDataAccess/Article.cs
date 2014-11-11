namespace ArticleHarbor.EFDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Provider { get; set; }

        public string No { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Uri는 데이터베이스 지원하는 형식이 아님.")]
        public string Url { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "EntityFramework의 Lazy Loading을 위한 Property.")]
        public virtual ICollection<ArticleWord> ArticleWords { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }
}