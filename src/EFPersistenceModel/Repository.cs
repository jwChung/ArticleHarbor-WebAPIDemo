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
        where TKeys : IKeyCollection
        where TModel : IModel
        where TPersistence : class
    {
        private readonly Database database;
        private readonly DbSet<TPersistence> dbSet;

        protected Repository(Database database, DbSet<TPersistence> dbSet)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            if (dbSet == null)
                throw new ArgumentNullException("dbSet");

            this.database = database;
            this.dbSet = dbSet;
        }

        public Database Database
        {
            get { return this.database; }
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

            throw new NotImplementedException();
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
    }
}