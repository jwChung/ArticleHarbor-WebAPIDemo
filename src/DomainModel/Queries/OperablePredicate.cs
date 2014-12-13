namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public class OperablePredicate : IPredicate
    {
        private readonly string columnName;
        private readonly string operatorName;
        private readonly object value;

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
        }

        public string SqlText
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { throw new NotImplementedException(); }
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
    }
}