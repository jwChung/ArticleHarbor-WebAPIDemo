namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class OperablePredicate : IPredicate
    {
        private readonly string columnName;
        private readonly string operatorName;
        private readonly object value;
        private readonly IEnumerable<IParameter> parameter;

        public OperablePredicate(string columnName, string operatorName, object value)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (operatorName == null)
                throw new ArgumentNullException("operatorName");

            if (value == null)
                throw new ArgumentNullException("value");

            if (columnName.Length == 0)
                throw new ArgumentException(
                    "The columnName should not be empty string.", "columnName");

            if (operatorName.Length == 0)
                throw new ArgumentException(
                    "The operatorName should not be empty string.", "operatorName");

            this.columnName = columnName;
            this.operatorName = operatorName;
            this.value = value;
            this.parameter = new IParameter[]
            {
                new Parameter("@" + this.columnName + Guid.NewGuid().ToString("N"), this.value)
            };
        }

        public string SqlText
        {
            get
            {
                return this.columnName
                    + " " + this.operatorName
                    + " " + this.parameter.Single().Name;
            }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { return this.parameter; }
        }

        public string ColumnName
        {
            get { return this.columnName; }
        }

        public string OperatorName
        {
            get { return this.operatorName; }
        }

        public object Value
        {
            get { return this.value; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((OperablePredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.columnName.ToUpper(CultureInfo.CurrentCulture).GetHashCode();
                hashCode = (hashCode * 397) ^ this.operatorName
                    .ToUpper(CultureInfo.CurrentCulture).GetHashCode();
                hashCode = (hashCode * 397) ^ this.value.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(OperablePredicate other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(
                this.columnName,
                other.columnName,
                StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(
                    this.operatorName,
                    other.operatorName,
                    StringComparison.CurrentCultureIgnoreCase)
                && this.value.Equals(other.value);
        }
    }
}