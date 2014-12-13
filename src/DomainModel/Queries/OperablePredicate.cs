namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public class OperablePredicate : IPredicate
    {
        public string SqlText
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { throw new NotImplementedException(); }
        }
    }
}