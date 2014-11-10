namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class FakeRepositoryBase<T> : IRepository<T>
    {
        private readonly ICollection<T> items;

        public FakeRepositoryBase(ICollection<T> items)
        {
            this.items = items;
        }

        public Task<T> FineAsync(params object[] identity)
        {
            return Task.FromResult<T>(
                this.items.Single(x => this.GetIndentity(x).SequenceEqual(identity)));
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            return Task.FromResult<IEnumerable<T>>(this.items);
        }

        public Task<T> InsertAsync(T item)
        {
            this.items.Add(item);
            return Task.FromResult<T>(item);
        }

        public async Task UpdateAsync(T item)
        {
            await this.DeleteAsync(item);
            this.items.Add(item);
        }

        public async Task DeleteAsync(params object[] identity)
        {
            var item = await this.FineAsync(identity);
            items.Remove(item);
        }

        public abstract object[] GetIndentity(T item);
    }
}