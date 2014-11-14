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

    public class Identity<TKey1, TKey2> : IIndentity
    {
        private readonly TKey1 key1;
        private readonly TKey2 key2;
        private readonly object[] objects;

        public Identity(TKey1 key1, TKey2 key2)
        {
            if (key1 == null)
                throw new ArgumentNullException("key1");

            if (key2 == null)
                throw new ArgumentNullException("key2");

            this.key1 = key1;
            this.key2 = key2;
            this.objects = new object[] { this.key1, this.key2 };
        }

        public TKey1 Key1
        {
            get { return this.key1; }
        }

        public TKey2 Key2
        {
            get { return this.key2; }
        }

        public object[] Keys
        {
            get { return this.objects; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Identity<TKey1, TKey2>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey1>.Default.GetHashCode(this.key1) * 397)
                    ^ EqualityComparer<TKey2>.Default.GetHashCode(this.key2);
            }
        }

        protected bool Equals(Identity<TKey1, TKey2> other)
        {
            return EqualityComparer<TKey1>.Default.Equals(this.key1, other.key1)
                && EqualityComparer<TKey2>.Default.Equals(this.key2, other.key2);
        }
    }
}