namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleCollector
    {
        Task<IEnumerable<Article>> CollectAsync();
    }
}