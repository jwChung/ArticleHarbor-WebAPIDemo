namespace ArticleHarbor.DomainModel.Queries
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

            if (!predicates.Any())
                throw new ArgumentException("The predicates parameter should not be emtpy.", "predicates");

            this.predicates = predicates;
        }

        public string SqlText
        {
            get { return string.Join(" AND ", this.predicates.Select(p => p.SqlText)); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { return this.predicates.SelectMany(x => x.Parameters); }
        }

        public IEnumerable<IPredicate> Predicates
        {
            get { return this.predicates; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((AndPredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.predicates.Aggregate(0, (h, p) => (h * 397) ^ p.GetHashCode());    
            }
        }

        protected bool Equals(AndPredicate other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return this.predicates.SequenceEqual(other.predicates);
        }
    }
}