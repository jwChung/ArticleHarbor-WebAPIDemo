namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public static class Predicate
    {
        public static IPredicate Equal(string columnName, object value)
        {
            return new OperablePredicate(columnName, "=", value);
        }

        public static IPredicate And(params IPredicate[] predicates)
        {
            return new AndPredicate(predicates);
        }

        public static IPredicate InClause(string columnName, params object[] values)
        {
            return new InClausePredicate(columnName, values);
        }
    }
}