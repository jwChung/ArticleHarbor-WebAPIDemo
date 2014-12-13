namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public class EqualPredicate : IPredicate
    {
        private readonly string columnName;
        private readonly object value;
        private readonly IParameter parameter;
        
        public EqualPredicate(string columnName, object value)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (value == null)
                throw new ArgumentNullException("value");

            if (columnName.Length == 0)
                throw new ArgumentException(
                    "The columnName should not be empty string.", "columnName");

            this.columnName = columnName;
            this.value = value;
            this.parameter = new Parameter(
                "@" + this.columnName + Guid.NewGuid().ToString("N"),
                this.value);
        }

        public string ColumnName
        {
            get { return this.columnName; }
        }

        public object Value
        {
            get { return this.value; }
        }

        public string SqlText
        {
            get { return this.columnName + " = " + this.parameter.Name; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { yield return this.parameter; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((EqualPredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.columnName.GetHashCode() * 397) ^ this.value.GetHashCode();
            }
        }

        protected bool Equals(EqualPredicate other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(
                this.columnName, other.columnName, StringComparison.CurrentCultureIgnoreCase)
                && this.value.Equals(other.value);
        }
    }
}