namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using Models;

    public interface IArticleTransformation
    {
        IEnumerable<Article> Convert(IEnumerable<Article> articles);
    }
}