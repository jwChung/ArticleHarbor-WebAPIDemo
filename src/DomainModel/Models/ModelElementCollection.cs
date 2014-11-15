namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class ModelElementCollection<TId, TModel>
        : IModelElementCollection<TId, TModel> where TId : IKeyCollection
    {
        public IModelElement this[TId keys]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public abstract IModelElement New(TModel model);

        public Task<IEnumerable<IModelElement>> ExecuteQueryAsync(IPredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            throw new NotImplementedException();
        }

        public IEnumerator<IModelElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}