namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class InClausePredicate : IPredicate
    {
        private readonly string columnName;
        private readonly object[] parameterValues;
        private readonly IEnumerable<IParameter> parameters;

        public InClausePredicate(string columnName, params object[] parameterValues)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (parameterValues == null)
                throw new ArgumentNullException("parameterValues");

            this.columnName = columnName;
            this.parameterValues = parameterValues;
        }

        public InClausePredicate(string columnName, params IParameter[] parameters)
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
                    string.Join(", ", this.GetParameterNames()));
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

        public IEnumerable<object> ParameterValues
        {
            get
            {
                return this.parameters != null
                    ? this.parameters.Select(x => x.Value)
                    : this.parameterValues;
            }
        }

        private IEnumerable<string> GetParameterNames()
        {
            return this.parameters != null
                ? this.parameters.Select(p => p.Name)
                : this.parameterValues.Select(
                    (v, i) => this.columnName + (i + 1).ToString().PadLeft(2, '0'));
        }
    }
}