namespace ArticleHarbor.DomainModel.Queries
{
    using System;

    public class Top : ITop
    {
        private readonly int count;

        public Top(int count)
        {
            this.count = count;
        }

        public static ITop None
        {
            get { return new NoTop(); }
        }

        public int Count
        {
            get { return this.count; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Top)obj);
        }

        public override int GetHashCode()
        {
            return this.count;
        }

        protected bool Equals(Top other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return this.count == other.count;
        }
    }
}