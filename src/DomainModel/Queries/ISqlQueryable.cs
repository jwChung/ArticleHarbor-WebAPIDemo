namespace ArticleHarbor.DomainModel.Queries
{
    public interface ISqlQueryable
    {
        ISqlQuery ProvideQuery();
    }
}