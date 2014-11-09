namespace ArticleHarbor.EFDataAccess
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is specially treated by EF to support lazy loading.")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}