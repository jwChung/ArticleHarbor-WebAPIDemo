namespace EFPersistenceModel
{
    using DomainModel;
    using EFDataAccess;

    internal static class ArticleExtensions
    {
        public static Article ToArticle(this EFArticle efArticle)
        {
            return new Article(
                efArticle.Id,
                efArticle.Provider,
                efArticle.No,
                efArticle.Subject,
                efArticle.Body,
                efArticle.Date,
                efArticle.Url);
        }

        public static EFArticle ToEFArticle(this Article article)
        {
            return new EFArticle
            {
                Id = article.Id,
                Provider = article.Provider,
                No = article.No,
                Subject = article.Subject,
                Body = article.Body,
                Date = article.Date,
                Url = article.Url
            };
        }
    }
}