namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class InClausePredicate : IPredicate
    {
        private readonly string columnName;

        public InClausePredicate(string columnName)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            this.columnName = columnName;
        }

        public string SqlText
        {
            get { throw new System.NotImplementedException(); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public string ColumnName
        {
            get { return this.columnName; }
        }
    }
}