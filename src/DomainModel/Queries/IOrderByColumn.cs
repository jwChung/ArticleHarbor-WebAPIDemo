namespace ArticleHarbor.DomainModel.Queries
{
    public interface IOrderByColumn
    {
        string Name { get; }

        OrderDirection OrderDirection { get; }
    }
}