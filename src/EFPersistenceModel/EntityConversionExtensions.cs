namespace EFPersistenceModel
{
    using DomainModel;
    using EFDataAccess;
    using Article = EFDataAccess.Article;
    using ArticleWord = EFDataAccess.ArticleWord;

    internal static class EntityConversionExtensions
    {
        public static DomainModel.Article ToDomain(this Article article)
        {
            return new DomainModel.Article(
                article.Id,
                article.Provider,
                article.No,
                article.Subject,
                article.Body,
                article.Date,
                article.Url);
        }

        public static Article ToPersistence(this DomainModel.Article article)
        {
            return new Article
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

        public static DomainModel.ArticleWord ToDomain(this ArticleWord articleWord)
        {
            return new DomainModel.ArticleWord(articleWord.ArticleId, articleWord.Word);
        }

        public static ArticleWord ToPersistence(this DomainModel.ArticleWord articleWord)
        {
            return new ArticleWord
            {
                ArticleId = articleWord.ArticleId,
                Word = articleWord.Word
            };
        }
    }
}