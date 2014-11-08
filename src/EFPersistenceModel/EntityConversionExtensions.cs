namespace EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;
    using EFDataAccess;

    internal static class EntityConversionExtensions
    {
        public static DomainModel.Article ToDomain(this EFDataAccess.Article article)
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

        public static EFDataAccess.Article ToPersistence(this DomainModel.Article article)
        {
            return new EFDataAccess.Article
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

        public static DomainModel.ArticleWord ToDomain(this EFDataAccess.ArticleWord articleWord)
        {
            return new DomainModel.ArticleWord(articleWord.ArticleId, articleWord.Word);
        }

        public static EFDataAccess.ArticleWord ToPersistence(
            this DomainModel.ArticleWord articleWord)
        {
            return new EFDataAccess.ArticleWord
            {
                ArticleId = articleWord.ArticleId,
                Word = articleWord.Word
            };
        }

        public static DomainModel.User ToDomain(this EFDataAccess.User user, IEnumerable<string> roleNames)
        {
            Roles roles = Roles.None;
            foreach (var roleName in roleNames)
                roles |= (Roles)Enum.Parse(typeof(Roles), roleName);

            return new DomainModel.User(user.UserName, roles);
        }
    }
}