namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using Models;

    public interface IArticleConvertor
    {
        IEnumerable<Article> Convert(IEnumerable<Article> articles);
    }
}