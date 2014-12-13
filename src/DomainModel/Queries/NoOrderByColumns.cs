namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public sealed class NoOrderByColumns : IOrderByColumns
    {
        public IEnumerator<IOrderByColumn> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield break;
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