namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections.Generic;

    public sealed class NoPredicate : IPredicate
    {
        public string SqlText
        {
            get { return string.Empty; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { yield break; }
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == this.GetType();
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}