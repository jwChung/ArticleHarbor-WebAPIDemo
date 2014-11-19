namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using DomainModel.Repositories;

    public abstract class Repository<TKeys, TModel, TPersistence>
        : IRepository<TKeys, TModel>
        where TKeys : IKeyCollection
        where TModel : IModel
    {
        public Task<TModel> Find(TKeys keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> SelectAsync()
        {
            throw new NotImplementedException();
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

        public abstract TModel ToModel(TPersistence persistence);

        public abstract TPersistence ToPersistence(TModel model);
    }
}