namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;

    public abstract class FakeRepositoryBase<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private readonly Generator<TItem> generator;

        public FakeRepositoryBase(Generator<TItem> generator)
        {
            this.generator = generator;

            for (int i = 0; i < 3; i++)
                this.Add(this.New());
        }

        public TItem New()
        {
            foreach (var item in this.generator)
            {
                if (!this.Contains(this.GetKeyForItem(item)))
                    return item;
            }

            throw new InvalidOperationException();
        }

        public Task<TItem> FindAsync(TKey key)
        {
            if (this.Contains(key))
                return Task.FromResult(this[key]);
            return Task.FromResult<TItem>(default(TItem));
        }

        public Task<IEnumerable<TItem>> SelectAsync()
        {
            return Task.FromResult<IEnumerable<TItem>>(this);
        }

        public Task<TItem> InsertAsync(TItem article)
        {
            this.Add(article);
            return Task.FromResult(article);
        }

        public Task UpdateAsync(TItem article)
        {
            this.Remove(this.GetKeyForItem(article));
            this.Add(article);
            return Task.FromResult<TItem>(default(TItem));
        }

        public Task DeleteAsync(TKey key)
        {
            this.Remove(key);
            return Task.FromResult<TItem>(default(TItem));
        }
    }
}