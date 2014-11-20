namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public interface IPredicate
    {
        string Condition { get; }

        IEnumerable<IParameter> Parameters { get; }
    }
}