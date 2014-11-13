namespace ArticleHarbor.DomainModel.Collectors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleCommand
    {
        Task ExecuteAsync(IEnumerable<Article> articles);
    }
}