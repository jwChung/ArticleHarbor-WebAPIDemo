namespace ArticleHarbor.DomainModel.Queries
{
    using System;

    public class SqlQuery : ISqlQuery
    {
        private readonly ITop top;
        private readonly IOrderByColumns columns;
        private readonly IPredicate predicate;

        public SqlQuery(ITop top, IOrderByColumns columns, IPredicate predicate)
        {
            if (top == null)
                throw new ArgumentNullException("top");

            if (columns == null)
                throw new ArgumentNullException("columns");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            this.top = top;
            this.columns = columns;
            this.predicate = predicate;
        }

        public ITop Top
        {
            get { return this.top; }
        }

        public IOrderByColumns OrderByColumns
        {
            get { return this.columns; }
        }

        public IPredicate Predicate
        {
            get { return this.predicate; }
        }
    }
}