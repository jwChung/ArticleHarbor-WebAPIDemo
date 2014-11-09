namespace ArticleHarbor.EFDataAccess
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public virtual ICollection<Article> Articles { get; set; }
    }
}