namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public static class Predicate
    {
        public static IPredicate None
        {
            get { return new NoPredicate(); }
        }

        public static IPredicate And(params IPredicate[] predicates)
        {
            return new AndPredicate(predicates);
        }

        public static IPredicate InClause(string columnName, params object[] values)
        {
            return new InClausePredicate(columnName, values);
        }

        public static IPredicate Equal(string columnName, object value)
        {
            return new OperablePredicate(columnName, "=", value);
        }

        public static IPredicate NotEqual(string columnName, object value)
        {
            return new OperablePredicate(columnName, "<>", value);
        }

        public static IPredicate Like(string columnName, string value)
        {
            return new OperablePredicate(columnName, "LIKE", value);
        }

        public static IPredicate GreatThan(string columnName, object value)
        {
            return new OperablePredicate(columnName, ">", value);
        }

        public static IPredicate Contains(string columnName, string word)
        {
            return new ContainsPredicate(columnName, word);
        }
    }
}