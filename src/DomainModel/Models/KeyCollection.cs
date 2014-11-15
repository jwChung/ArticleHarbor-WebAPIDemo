namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class KeyCollection<TKey> : IKeyCollection
    {
        private readonly TKey key;

        public KeyCollection(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.key = key;
        }

        public TKey Key
        {
            get { return this.key; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((KeyCollection<TKey>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TKey>.Default.GetHashCode(this.key);
        }

        public IEnumerator<object> GetEnumerator()
        {
            yield return this.key;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected bool Equals(KeyCollection<TKey> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return EqualityComparer<TKey>.Default.Equals(this.key, other.key);
        }
    }

    public class KeyCollection<TKey1, TKey2> : IKeyCollection
    {
        private readonly TKey1 key1;
        private readonly TKey2 key2;
        
        public KeyCollection(TKey1 key1, TKey2 key2)
        {
            if (key1 == null)
                throw new ArgumentNullException("key1");

            if (key2 == null)
                throw new ArgumentNullException("key2");

            this.key1 = key1;
            this.key2 = key2;
        }

        public TKey1 Key1
        {
            get { return this.key1; }
        }

        public TKey2 Key2
        {
            get { return this.key2; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((KeyCollection<TKey1, TKey2>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey1>.Default.GetHashCode(this.key1) * 397)
                    ^ EqualityComparer<TKey2>.Default.GetHashCode(this.key2);
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            yield return this.key1;
            yield return this.key2;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected bool Equals(KeyCollection<TKey1, TKey2> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return EqualityComparer<TKey1>.Default.Equals(this.key1, other.key1)
                && EqualityComparer<TKey2>.Default.Equals(this.key2, other.key2);
        }
    }
}