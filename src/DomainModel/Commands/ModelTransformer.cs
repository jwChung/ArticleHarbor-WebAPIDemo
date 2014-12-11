namespace ArticleHarbor.DomainModel.Commands
{
    using System.Threading.Tasks;
    using Models;

    public class ModelTransformer : IModelTransformer
    {
        public virtual Task<User> TransformAsync(User user)
        {
            return Task.FromResult(user);
        }

        public virtual Task<Article> TransformAsync(Article article)
        {
            return Task.FromResult(article);
        }

        public virtual Task<Bookmark> TransformAsync(Bookmark bookmark)
        {
            return Task.FromResult(bookmark);
        }

        public virtual Task<Keyword> TransformAsync(Keyword keyword)
        {
            return Task.FromResult(keyword);
        }
    }
}