namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "Select 의미전달에 적합해서 사용함.")]
        IEnumerable<Article> Select();

        Article Insert(Article article);
    }
}