namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class ModelElementCollection<TModel> : IModelElementCollection<TModel>
    {
        public IModelElement<TModel> this[IIndentity indentity]
        {
            get
            {
                if (indentity == null)
                    throw new ArgumentNullException("indentity");

                throw new NotImplementedException();
            }
        }

        public IModelElement<TModel> New(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            throw new NotImplementedException();
        }

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