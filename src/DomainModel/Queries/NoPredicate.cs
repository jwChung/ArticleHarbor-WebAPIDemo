namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public class NoPredicate : IPredicate
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