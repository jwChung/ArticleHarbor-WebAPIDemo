namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AndPredicate : IPredicate
    {
        private readonly IEnumerable<IPredicate> predicates;

        public AndPredicate(params IPredicate[] predicates) 
            : this((IEnumerable<IPredicate>)predicates)
        {
        }

        public AndPredicate(IEnumerable<IPredicate> predicates)
        {
            if (predicates == null)
                throw new ArgumentNullException("predicates");

            this.predicates = predicates;
        }

        public string SqlText
        {
            get { return string.Join(" AND ", this.predicates.Select(p => p.SqlText)); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { throw new System.NotImplementedException(); }
        }

        public IEnumerable<IPredicate> Predicates
        {
            get { return this.predicates; }
        }
    }
}