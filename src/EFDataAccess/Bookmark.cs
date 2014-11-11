namespace ArticleHarbor.EFDataAccess
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bookmark
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ArticleId { get; set; }

        public virtual User User { get; set; }

        public virtual Article Article { get; set; }
    }
}