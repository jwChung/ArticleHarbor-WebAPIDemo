namespace ArticleHarbor.DomainModel.Queries
{
    public interface IParameter
    {
        string Name { get; }

        object Value { get; }
    }
}