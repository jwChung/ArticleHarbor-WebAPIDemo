namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Queries;

    public class CompositeModel : IModel
    {
        private readonly IEnumerable<IModel> models;

        public CompositeModel(IEnumerable<IModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");

            this.models = models;
        }

        public IEnumerable<IModel> Models
        {
            get { return this.models; }
        }

        public IKeys GetKeys()
        {
            return new Keys(this.models.SelectMany(m => m.GetKeys()));
        }

        public Task<IEnumerable<TReturn>> ExecuteAsync<TReturn>(IModelCommand<TReturn> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return this.ExecuteAsyncWith(command);
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith<TReturn>(
            IModelCommand<TReturn> command)
        {
            var value = Enumerable.Empty<TReturn>();
            foreach (var model in this.Models)
                value = value.Concat(await model.ExecuteAsync(command));

            return value;
        }
    }
}