namespace ArticleHarbor.DomainModel.Models
{
    public interface IParameter
    {
        string Name { get; }

        object Value { get; }
    }
}