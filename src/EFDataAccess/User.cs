namespace ArticleHarbor.EFDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        [Index(IsUnique = true)]
        public Guid ApiKey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is specially treated by EF to support lazy loading.")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}