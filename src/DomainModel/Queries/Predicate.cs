namespace ArticleHarbor.DomainModel.Queries
{
    public class Predicate
    {
        public static IPredicate Equal(string columnName, object value)
        {
            return new OperablePredicate(columnName, "=", value);
        }

        public static IPredicate And(params IPredicate[] predicates)
        {
            return new AndPredicate(predicates);
        }
    }
}