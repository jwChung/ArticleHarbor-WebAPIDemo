namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        IEnumerable<Article> Select();

        Article Insert(Article article);
    }
}