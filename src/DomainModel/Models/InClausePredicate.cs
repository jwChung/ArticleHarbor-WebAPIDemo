namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
    }
}