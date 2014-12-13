namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class InClausePredicate : IPredicate
    {
        private readonly string columnName;
        private readonly IEnumerable<object> values;
        private readonly IEnumerable<IParameter> parameters;

        public InClausePredicate(string columnName, params object[] values)
            : this(columnName, (IEnumerable<object>)values)
        {
        }

        public InClausePredicate(string columnName, IEnumerable<object> values)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (values == null)
                throw new ArgumentNullException("values");

            this.columnName = columnName;
            this.values = values;
            this.parameters = this.values.Select(v =>
                new Parameter("@" + this.columnName + Guid.NewGuid().ToString("N"), v))
                .ToArray();
        }

        public string SqlText
        {
            get
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} IN ({1})",
                    this.columnName,
                    string.Join(", ", this.parameters.Select(p => p.Name)));
            }
        }

        public string ColumnName
        {
            get { return this.columnName; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { return this.parameters; }
        }

        public IEnumerable<object> Values
        {
            get { return this.values; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((InClausePredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.values.Aggregate(
                    this.columnName.ToUpper(CultureInfo.CurrentCulture).GetHashCode(),
                    (h, p) => (h * 397) ^ p.GetHashCode());    
            }
        }

        protected bool Equals(InClausePredicate other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(
                this.columnName, other.columnName, StringComparison.CurrentCultureIgnoreCase)
                && this.values.SequenceEqual(other.values);
        }
    }
}