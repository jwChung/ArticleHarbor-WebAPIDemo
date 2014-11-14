namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class Identity<TKey> : IIndentity
    {
        private readonly TKey key;
        private readonly object[] keys;

        public Identity(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.key = key;
            this.keys = new object[] { this.key };
        }

        public TKey Key
        {
            get { return this.key; }
        }

        public object[] Keys
        {
            get { return this.keys; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Identity<TKey>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TKey>.Default.GetHashCode(this.key);
        }

        protected bool Equals(Identity<TKey> other)
        {
            return EqualityComparer<TKey>.Default.Equals(this.key, other.key);
        }
    }
}