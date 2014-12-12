namespace ArticleHarbor.DomainModel.Queries
{
    public interface ISqlQueriable
    {
        ISqlQuery ProvideQuery();
    }
}