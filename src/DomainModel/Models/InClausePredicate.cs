namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InClausePredicate : IPredicate
    {
        private readonly string columnName;
        private readonly IEnumerable<IParameter> parameters;

        public InClausePredicate(string columnName, params IParameter[] parameters)
            : this(columnName, (IEnumerable<IParameter>)parameters)
        {
        }

        public InClausePredicate(string columnName, IEnumerable<IParameter> parameters)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            this.columnName = columnName;
            this.parameters = parameters;
        }

        public string SqlText
        {
            get { throw new System.NotImplementedException(); }
        }

        public string ColumnName
        {
            get { return this.columnName; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { return this.parameters; }
        }
    }
}