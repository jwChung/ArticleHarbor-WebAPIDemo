namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using ArticleHarbor.DomainModel;

    internal static class EntityConversionExtensions
    {
        public static Article ToDomain(this ArticleHarbor.EFDataAccess.Article article)
        {
            return new Article(
                article.Id,
                article.Provider,
                article.No,
                article.Subject,
                article.Body,
                article.Date,
                article.Url);
        }

        public static ArticleHarbor.EFDataAccess.Article ToPersistence(this Article article)
        {
            return new ArticleHarbor.EFDataAccess.Article
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

        public static ArticleWord ToDomain(this ArticleHarbor.EFDataAccess.ArticleWord articleWord)
        {
            return new ArticleWord(articleWord.ArticleId, articleWord.Word);
        }

        public static ArticleHarbor.EFDataAccess.ArticleWord ToPersistence(
            this ArticleWord articleWord)
        {
            return new ArticleHarbor.EFDataAccess.ArticleWord
            {
                ArticleId = articleWord.ArticleId,
                Word = articleWord.Word
            };
        }

        public static User ToDomain(this ArticleHarbor.EFDataAccess.User user, IEnumerable<string> roleNames)
        {
            Roles roles = Roles.None;
            foreach (var roleName in roleNames)
                roles |= (Roles)Enum.Parse(typeof(Roles), roleName);

            return new User(user.UserName, roles);
        }
    }
}