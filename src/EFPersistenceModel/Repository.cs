namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using DomainModel.Repositories;

    public abstract class Repository<TKeys, TModel, TPersistence>
        : IRepository<TKeys, TModel>
        where TKeys : IKeys
        where TModel : IModel
        where TPersistence : class
    {
        private readonly DbContext context;
        private readonly DbSet<TPersistence> dbSet;

        protected Repository(DbContext context, DbSet<TPersistence> dbSet)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (dbSet == null)
                throw new ArgumentNullException("dbSet");

            this.context = context;
            this.dbSet = dbSet;
        }

        public DbContext Context
        {
            get { return this.context; }
        }

        public DbSet<TPersistence> DbSet
        {
            get { return this.dbSet; }
        }

        public Task<TModel> FindAsync(TKeys keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            return this.FindAsyncWith(keys);
        }

        public async Task<IEnumerable<TModel>> SelectAsync()
        {
            var persistences = await this.dbSet.AsNoTracking().ToListAsync();
            return persistences.Select(this.ConvertToModel);
        }

        public Task<TModel> InsertAsync(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            return this.InsertAsyncWith(model);
        }

        public Task UpdateAsync(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            throw new NotImplementedException();
        }

        public Task DeleteAsync(TKeys keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> ExecuteSelectCommandAsync(IPredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            throw new NotImplementedException();
        }

        public Task ExecuteDeleteCommandAsync(IPredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            throw new NotImplementedException();
        }

        public abstract TModel ConvertToModel(TPersistence persistence);

        public abstract TPersistence ConvertToPersistence(TModel model);

        private async Task<TModel> FindAsyncWith(TKeys keys)
        {
            var persistence = await this.dbSet.FindAsync(keys.ToArray());
            return this.ConvertToModel(persistence);
        }

        private async Task<TModel> InsertAsyncWith(TModel model)
        {
            var persistence = this.dbSet.Add(this.ConvertToPersistence(model));
            await this.context.SaveChangesAsync();
            return this.ConvertToModel(persistence);
        }
    }
}