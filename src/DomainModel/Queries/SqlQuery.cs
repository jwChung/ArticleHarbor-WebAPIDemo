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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((SqlQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.top.GetHashCode();
                hashCode = (hashCode * 397) ^ this.columns.GetHashCode();
                hashCode = (hashCode * 397) ^ this.predicate.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(SqlQuery other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return this.top.Equals(other.top)
                && this.columns.Equals(other.columns)
                && this.predicate.Equals(other.predicate);
        }
    }
}