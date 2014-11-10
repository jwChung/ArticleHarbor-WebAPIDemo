namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;

    public abstract class FakeRepositoryBase<T> : IRepository<T>
    {
        private readonly IList<T> items;

        public FakeRepositoryBase(Generator<T> generator)
        {
            this.items = new List<T>();
            foreach (var item in generator)
            {
                this.InsertAsync(item).Wait();
                if (this.items.Count == 3)
                    break;
            }
        }

        public IList<T> Items
        {
            get { return this.items; }
        }

        public Task<T> FineAsync(params object[] identity)
        {
            return Task.FromResult<T>(
                this.Items.LastOrDefault(x => this.GetIdentity(x).SequenceEqual(identity)));
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            return Task.FromResult<IEnumerable<T>>(this.Items);
        }

        public Task<T> InsertAsync(T item)
        {
            this.Items.Add(item);
            return Task.FromResult<T>(item);
        }

        public async Task UpdateAsync(T item)
        {
            var oldItem = await this.FineAsync(this.GetIdentity(item));
            if (oldItem == null)
                return;

            this.Items.Remove(oldItem);
            this.Items.Add(item);
        }

        public async Task DeleteAsync(params object[] identity)
        {
            var item = await this.FineAsync(identity);
            if (item == null)
                return;

            this.Items.Remove(item);
        }

        public abstract object[] GetIdentity(T item);
    }
}