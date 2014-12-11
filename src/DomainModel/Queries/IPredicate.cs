namespace ArticleHarbor.DomainModel.Queries
{
    using System.Collections.Generic;

    public interface IPredicate
    {
        string SqlText { get; }

        IEnumerable<IParameter> Parameters { get; }
    }
}