namespace ArticleHarbor.DomainModel.Collectors
{
    using Models;

    public interface IArticleTransformation
    {
        Article Transform(Article article);
    }
}