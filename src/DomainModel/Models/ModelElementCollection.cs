namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class ModelElementCollection<TIndentity, TModel>
        : IModelElementCollection<TIndentity, TModel> where TIndentity : IIdentity
    {
        public IModelElement<TModel> this[TIndentity indentity]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public abstract IModelElement<TModel> New(TModel model);

        public Task<IEnumerable<IModelElement<TModel>>> ExecuteSqlQueryAsync(IPredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            throw new NotImplementedException();
        }

        public IEnumerator<IModelElement<TModel>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}