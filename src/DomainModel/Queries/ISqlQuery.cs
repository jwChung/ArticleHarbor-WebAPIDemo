namespace ArticleHarbor.DomainModel.Queries
{
    public interface ISqlQuery
    {
        ITop Top { get; }

        IOrderByColumns OrderByColumns { get; }

        IPredicate Predicate { get; }
    }
}