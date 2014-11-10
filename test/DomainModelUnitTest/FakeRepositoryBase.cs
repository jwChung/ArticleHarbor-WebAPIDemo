namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class FakeRepositoryBase<T> : IRepository<T>
    {
        private readonly IList<T> items;

        public FakeRepositoryBase(IList<T> items)
        {
            this.items = items;
        }

        public IList<T> Items
        {
            get { return this.items; }
        }

        public Task<T> FineAsync(params object[] identity)
        {
            return Task.FromResult<T>(
                this.Items.SingleOrDefault(x => this.GetIndentity(x).SequenceEqual(identity)));
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
            var oldItem = await this.FineAsync(GetIndentity(item));
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

        public abstract object[] GetIndentity(T item);
    }
}