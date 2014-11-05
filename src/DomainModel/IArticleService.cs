namespace DomainModel
{
    using System.Collections.Generic;

    public interface IArticleService
    {
        IEnumerable<Article> Get();

        Article AddOrModify(Article article);

        Article Remove(Article article);
    }
}