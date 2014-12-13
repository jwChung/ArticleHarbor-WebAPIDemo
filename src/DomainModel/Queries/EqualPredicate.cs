namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class EqualPredicate : OperablePredicate
    {
        public EqualPredicate(string columnName, object value) : base(columnName, "=", value)
        {
        }
   }
}