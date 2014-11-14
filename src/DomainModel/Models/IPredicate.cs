namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public interface IPredicate
    {
        string ConditionText { get; }

        ////IEnumerable<ISqlParameter> Parameters { get; }
    }
}