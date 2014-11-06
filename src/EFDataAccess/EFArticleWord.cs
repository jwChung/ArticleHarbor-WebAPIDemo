namespace EFDataAccess
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EFArticleWord
    {
        [Key]
        [Column(Order = 0)]
        public string Word { get; set; }

        [Key]
        [Column(Order = 1)]
        public int EFArticleId { get; set; }

        public virtual EFArticle EFArticle { get; set; }
    }
}