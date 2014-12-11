namespace ArticleHarbor.DomainModel.Models
{
    using System.Threading.Tasks;

    public interface IModelTransformer
    {
        Task<User> TransformAsync(User user);

        Task<Article> TransformAsync(Article article);

        Task<Bookmark> TransformAsync(Bookmark bookmark);

        Task<Keyword> TransformAsync(Keyword keyword);
    }
}