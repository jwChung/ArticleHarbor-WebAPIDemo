namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using Models;

    public interface IArticleTransformation
    {
        Article Transform(Article article);

        IEnumerable<Article> Transform(IEnumerable<Article> articles);
    }
}