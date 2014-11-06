namespace EFDataAccess
{
    using System.ComponentModel.DataAnnotations;

    public class EFArticleWord
    {
        [Key]
        public string Word { get; set; }

        public int EFArticleId { get; set; }

        public virtual EFArticle EFArticle { get; set; }
    }
}