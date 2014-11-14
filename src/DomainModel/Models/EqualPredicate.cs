namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class EqualPredicate : IPredicate
    {
        public string Condition
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { throw new NotImplementedException(); }
        }
    }
}