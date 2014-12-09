namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class EqualPredicate : IPredicate
    {
        private readonly string columnName;
        private readonly object value;
        
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
            get { return this.columnName + " = @" + this.columnName; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { yield return new Parameter("@" + this.columnName, this.value); }
        }
    }
}