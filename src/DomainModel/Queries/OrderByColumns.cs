namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class OrderByColumns : IOrderByColumns
    {
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
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}