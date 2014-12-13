namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class OrderByColumns : IOrderByColumns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "This can be suppressed as the 'None' value is immutable.")]
        public static readonly IOrderByColumns None = new NoOrderByColumns();
        private readonly IEnumerable<IOrderByColumn> columns;

        public OrderByColumns(params IOrderByColumn[] columns)
            : this((IEnumerable<IOrderByColumn>)columns)
        {
        }

        public OrderByColumns(IEnumerable<IOrderByColumn> columns)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");

            this.columns = columns;
        }

        public IEnumerable<IOrderByColumn> Columns
        {
            get { return this.columns; }
        }

        public IEnumerator<IOrderByColumn> GetEnumerator()
        {
            return this.columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((OrderByColumns)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.columns.Skip(1).Aggregate(
                    this.columns.First().GetHashCode(),
                    (h, p) => (h * 397) ^ p.GetHashCode());
            }
        }

        protected bool Equals(OrderByColumns other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return this.columns.SequenceEqual(other.columns);
        }
    }
}