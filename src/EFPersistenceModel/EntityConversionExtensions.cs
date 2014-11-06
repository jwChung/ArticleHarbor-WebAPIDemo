namespace EFPersistenceModel
{
    using DomainModel;
    using EFDataAccess;

    internal static class EntityConversionExtensions
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

        public static EFArticleWord ToEArticleWord(this ArticleWord articleWord)
        {
            return new EFArticleWord
            {
                Word = articleWord.Word,
                EFArticleId = articleWord.ArticleId
            };
        }

        public static ArticleWord ToArticleWord(this EFArticleWord efArticleWord)
        {
            return new ArticleWord(efArticleWord.Word, efArticleWord.EFArticleId);
        }
    }
}