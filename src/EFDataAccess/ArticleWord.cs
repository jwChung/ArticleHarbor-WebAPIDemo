namespace EFDataAccess
{
    public class ArticleWord
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual EFArticle EFArticle { get; set; }
    }
}